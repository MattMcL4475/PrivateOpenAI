namespace PrivateOpenAI
{
    /// <summary>
    /// To use:
    /// 1.  Create an Azure Open AI resource from the Azure Portal, for example "example123".
    /// 2.  Deploy a model and give it a name, for example "gpt4"
    /// 3.  Get the key, create a keyvault named "kvexample123", and create a secret named "example123" with the key as the value
    /// 4.  Set the const variables below to the above values
    /// </summary>
    public partial class formMain : Form
    {
        private Utility utility = new();
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private long requestId = 0;

        private static GptClient? client;

        public formMain(string azureOpenAiResourceName, string keyvaultName, string modelDeploymentName)
        {
            InitializeComponent();
            utility.UpdateControlProperty(labelStatus, "Text", $"Authenticating with Azure...");
            client = new GptClient();
            client.InitializationFailed += Client_InitializationFailed;
            client.ResponseTime += Client_ResponseTime;
            client.InitializeAsync(azureOpenAiResourceName, keyvaultName, modelDeploymentName, "You are an expert software engineer.")
                .ContinueWith(t => utility.UpdateControlProperty(labelStatus, "Text", $"Ready."));
        }

        private void Client_ResponseTime(object? sender, TimeSpan e)
        {
            utility.UpdateControlProperty(labelStatus, "Text", $"Response received in {e.TotalSeconds:n3} seconds.");
        }

        private void Client_InitializationFailed(object? sender, string e)
        {
            MessageBox.Show($"Auth failed: {e}");
            Environment.Exit(1);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (buttonGo.Text == "Go")
            {
                _ = Task.Run(async () =>
                {
                    var localId = Interlocked.Increment(ref requestId);
                    utility.InvokeControlMethod(textboxPrompt, textboxPrompt.Select, 0, 0);
                    utility.UpdateControlProperty(labelStatus, "Text", "Waiting on API...");
                    utility.UpdateControlProperty(textboxResponse, "Text", string.Empty);
                    utility.UpdateControlProperty(buttonGo, "Text", "Cancel");

                    try
                    {
                        var content = await client.GptAsync(textboxPrompt.Text, Convert.ToSingle(textboxTemp.Text), tokenSource.Token);
                        utility.UpdateControlProperty(textboxResponse, "Text", utility.ConvertToWindowsLineEndings(content));
                    }
                    catch (TaskCanceledException)
                    {
                        if (localId == Interlocked.Read(ref requestId))
                        {
                            utility.UpdateControlProperty(labelStatus, "Text", "Idle.");
                        }
                    }
                    catch (Exception exc)
                    {
                        if (localId == Interlocked.Read(ref requestId))
                        {
                            utility.UpdateControlProperty(labelStatus, "Text", $"Error: {exc.Message}");
                        }
                    }

                    if (localId == Interlocked.Read(ref requestId))
                    {
                        utility.UpdateControlProperty(buttonGo, "Text", "Go");
                    }
                });
            }
            else
            {
                _ = Task.Run(() =>
                {
                    utility.UpdateControlProperty(labelStatus, "Text", "Idle.");
                    utility.UpdateControlProperty(buttonGo, "Text", "Go");

                    if (!tokenSource.IsCancellationRequested)
                    {
                        tokenSource.Cancel();
                        tokenSource = new CancellationTokenSource();
                    }
                });
            }
        }

        private async void textboxPrompt_DoubleClick(object sender, EventArgs e)
        {
            textboxPrompt.Clear();
        }

        private async void textboxResponse_DoubleClick(object sender, EventArgs e)
        {
            var originalColor = textboxResponse.BackColor;
            textboxResponse.BackColor = Color.Green;

            utility.UpdateControlProperty(textboxResponse, "BackColor", Color.Green);
            Clipboard.SetText(utility.ConvertToUnixLineEndings(textboxResponse.Text));
            utility.InvokeControlMethod(textboxResponse, textboxResponse.Select, 0, 0);
            await Task.Delay(100);
            utility.UpdateControlProperty(textboxResponse, "BackColor", originalColor);
        }

        private void textboxPrompt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                buttonGo.PerformClick();
                e.SuppressKeyPress = true; // Stops the "Ding" that happens when Enter key is pressed
            }
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            textboxPrompt.Focus();
        }

        private void textboxTemp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
