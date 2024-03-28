namespace PrivateOpenAI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var a1 = Properties.Settings.Default.AzureOpenAiResourceName;
            var a2 = Properties.Settings.Default.KeyvaultName;
            var a3 = Properties.Settings.Default.ModelDeploymentName;

            if (!string.IsNullOrWhiteSpace(a1) && !string.IsNullOrWhiteSpace(a2) && !string.IsNullOrWhiteSpace(a3))
            {
                new formMain(a1, a2, a3).Show();
            }
            else
            {
                new Configuration().Show();
            }
               
            Application.Run();
        }
    }
}