using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didactica
{
    public static class Globals
    {
        // openAI input
        public static string openAIapiKey = ReadEnvironmentVariables()["OPENAI_API_KEY"];
        public static string openAIauth = "Bearer " + openAIapiKey;


        public static string openAIorganizationID = ReadEnvironmentVariables()["OPENAI_ORGANIZATION_ID"];
        public const string model = Models.gpt4o_mini;
        public const string contentType = "application/json";
        public const string host = "localhost"; // Use this in production

        //Embeddings
        public const string embeddingApiUrlLocal = $"http://{host}:8004/embeddings";
        public const string embeddingApiUrlOpenai = "https://api.openai.com/v1/embeddings";

        //LLMs

        public const string localInstructApiUrl = "http://10.128.17.11:6000/v1/chat/completions"; //This needs to be updated against new 10.128.29.2-address
        public const string openAIurl = $"https://api.openai.com/v1/chat/completions";

        //Vectorstores
        public const string libraryApiUrlOpenai = $"http://{host}:7000/similar";
        public const string libraryApiUrlLocal = $"http://{host}:7002/similar";
        public const string libraryApiUrlTvistesak = $"http://{host}:7001/similar";
        public const string localLibraryUploadApiUrl = $"http://{host}:7000/upload";

        //SQL
        public const string sqlIP = "10.128.29.2";



        public const int maxTokens = 4000;

        public const string dataDirectory = "C:/library/common";
        public const string pdfDirectory = dataDirectory + "/pdf";
        public const string standardLibrary = pdfDirectory + "/standard";
        public const string optionalLibrary = pdfDirectory + "/optional";
        public const string embeddingDirectory = dataDirectory + "/embeddings/text-embedding-3-small";
        //public const string pdfDirectory = "C:\\Users\\JHN\\source\\repos\\AAJGPT\\vectorstoreAPI\\data\\pdf";


        // Claude input, not in use at the moment, but kept for future testing
        // This is old, may not be working still
        // new api key:
        public static string apiKeyClaude = ReadEnvironmentVariables()["API_KEY_CLAUDE"];

        public static Dictionary<string,string> ReadEnvironmentVariables()
        {
            Dictionary<string,string> variables = new Dictionary<string, string>();
            string[] lines = System.IO.File.ReadAllLines("C:/Users/JHN/source/repos/AAJGPT/Didactica/Didactica/EnvironmentVariables.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split("=");
                variables.Add(parts[0], parts[1]);
            }
            return variables;
        }

    }

    public static class Models
    {
        public const string gpt4 = "gpt-4"; // Den nyeste og beste, begrenset antall spørringer, dyreste
        public const string gpt35 = "gpt-3.5-turbo"; // Standard som anbefales til vanlig bruk pr. 2023-07-26
        public const string gpt4_32k = "gpt-4-32k"; // Samme som gpt-4, men med 32k tokens i stedet for 8k, altså mer omfattende spørringer.
        public const string gpt35_16k = "gpt-3.5-turbo-16k"; // Samme som gpt-3.5-turbo, men med mer omfattende spørringer.
        public const string gpt4_turbo_preview = "gpt-4-turbo-preview";
        public const string gpt4o = "gpt-4o";
        public const string gpt4o_mini = "gpt-4o-mini";


        public const string openAIembeddingAda = "text-embedding-ada-002";
        public const string openAIembedding3Large = "text-embedding-3-large";
    }


   

}
