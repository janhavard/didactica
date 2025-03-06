using System.Text;

namespace Didactica
{
    public partial class Didactica : Form
    {
        List<Message> messages = new List<Message>();
        public Didactica()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            messages = new List<Message>();
            messages.Add(new Message(Role.user, textToEvaluateTextBox.Text));
            string requestBody = RequestBody.CreateDefaultRequestBody(messages, taskDescriptionTextBox.Text);
            string response = GPTAPI.GetResponse(requestBody);
            string content = GetResponse.GetContent(response);
            responseTextBox.Text = content;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Copy contents to clipboard:
            Clipboard.SetText(responseTextBox.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // User inputs a folder path
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            string folderPath = folderBrowserDialog.SelectedPath;
            // Get all files in the folder
            string[] files = Directory.GetFiles(folderPath);
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("Følgende filer vil bli bearbeidet iht. oppgavebeskrivelsen:");
            foreach (string file in files)
            {
                sb.AppendLine(file);
            }
            
            textToEvaluateTextBox.Text = sb.ToString();
        }


    }
}
