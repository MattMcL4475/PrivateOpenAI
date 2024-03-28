using Azure;
using Azure.AI.OpenAI;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Diagnostics;

namespace PrivateOpenAI
{
    public class GptClient
    {
        private OpenAIClient client;
        private bool isInitialized;

        public string ModelName { get; set; }
        public string AzureOpenAiResourceName { get; set; }
        public string KeyvaultName { get; set; }
        public string ModelDeploymentName { get; set; }
        public string SystemMessage { get; set; }

        public event EventHandler<string> InitializationFailed = delegate { };
        public event EventHandler<TimeSpan> ResponseTime = delegate { };

        public async Task InitializeAsync(string azureOpenAiResourceName, string keyvaultName, string modelDeploymentName, string systemMessage = null)
        {
            AzureOpenAiResourceName = azureOpenAiResourceName;
            KeyvaultName = keyvaultName;
            ModelDeploymentName = modelDeploymentName;
            SystemMessage = systemMessage;

            try
            {
                string key = "";

                if (string.IsNullOrWhiteSpace(key))
                {
                    var secretClient = new SecretClient(new Uri($"https://{keyvaultName}.vault.azure.net/"), new DefaultAzureCredential());
                    var secret = await secretClient.GetSecretAsync(azureOpenAiResourceName);
                    key = secret.Value.Value;
                }

                client = new OpenAIClient(new Uri($"https://{azureOpenAiResourceName}.openai.azure.com/"), new AzureKeyCredential(key));
                isInitialized = true;
            }
            catch (Exception exc)
            {
                InitializationFailed(this, exc.Message);
                Debugger.Break();
            }
        }

        public async Task<string> GptAsync(string prompt, float temperature = 1.0f, CancellationToken cancellationToken = default)
        {
            while (!isInitialized)
            {
                await Task.Delay(50);
            }

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = ModelDeploymentName,
                Temperature = temperature
            };

            if (!string.IsNullOrWhiteSpace(SystemMessage))
            {
                chatCompletionsOptions.Messages.Add(new ChatRequestSystemMessage(SystemMessage));
            }

            chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(prompt));

            var sw = Stopwatch.StartNew();
            var response = await client.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
            ResponseTime(this, sw.Elapsed);
            var responseMessage = response.Value.Choices[0].Message;
            return responseMessage.Content;
        }
    }
}
