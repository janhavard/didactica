using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Didactica
{


    public class GetResponse
    {
        public static string GetContent(string jsonString)
        {
            if (jsonString == "")
                return "<Error: No response>";
            // Deserialize the JSON string into ApiResponse object
            ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(jsonString);

            //int tokens = response.Usage.TotalTokens;

            // Extract the "content" value as a string
            string content = response?.Choices?[0]?.Message?.Content;

            // Output the content
            return content;
        }


        public static string MessagesToChatHistory(IEnumerable<Message> messages)
        {
            string chat = "";
            foreach (var message in messages)
            {
                string role;
                if (message.Role == "user")
                    role = "You";
                else
                    role = "GPT";
                chat += role + ":\n\r    " + message.Content + "\n\r";
            }
            return chat;
        }


        public static string GetReferenceTitles(string references)
        {
            List<string> extractedStrings = new List<string>();
            string[] lines = references.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                var match = Regex.Match(line, @"^(.*?/Side\d+):");
                if (match.Success)
                {
                    string extracted = match.Groups[1].Value;
                    extractedStrings.Add(extracted);
                }
            }

            return String.Join(Environment.NewLine, extractedStrings);

        }

    }

}
