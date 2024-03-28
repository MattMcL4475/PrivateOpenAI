namespace PrivateOpenAI
{
    public partial class Configuration : Form
    {
        public Configuration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AzureOpenAiResourceName = textBox1.Text;
            Properties.Settings.Default.KeyvaultName = textBox2.Text;
            Properties.Settings.Default.ModelDeploymentName = textBox3.Text;
            Properties.Settings.Default.Save();
            new formMain(textBox1.Text, textBox2.Text, textBox3.Text).Show();
            this.Close();
        }
    }
}
