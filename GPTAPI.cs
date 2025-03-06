using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;

namespace Didactica
{

    public class GPTAPI
    {
        public static string GetResponse(string requestbody)
        {
            string apiUrl = Globals.openAIurl;
            string apiKey = Globals.openAIapiKey;

            try
            {
                // Create the HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = "Bearer " + apiKey;

                //string requestBody = $"{{\"prompt\": \"{prompt}\", \"model\": \"{modelName}\"}}";

                // Write the request body data to the request stream
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(requestbody);
                }

                // Get the response from the server
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Read the response data
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseJson = reader.ReadToEnd();
                        // Process the response JSON here (e.g., deserialize and extract the result)
                        Console.WriteLine("API Response:");
                        Console.WriteLine(responseJson);
                        return responseJson;
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle any exceptions here
                Console.WriteLine("Error: " + ex.Message);
                return "";
            }
        }


        public static async Task<ResponseEvaluation?> GetStructuredResponseAsync(string systemPrompt, string userQuestion, string assistantResponse)
        {
            string apiUrl = Globals.openAIurl;
            string apiKey = Globals.openAIapiKey;

            try
            {
                string taskDescription =
                    """
                    Du skal evaluere kvaliteten på assistentens (som heter Mímir) svar på brukerens spørsmål. 
                    Evalueringen er et desimaltall mellom 0 og 1, der 1 er et perfekt svar og 0 er et svar som ikke hjelper brukeren i det hele tatt. 
                    relevanteReferanser skal være enten 'true' eller 'false' avhengig av om referansene i systemPrompt er relevante for brukerens spørsmål. svar 'false' hvis ikke en eneste referanse er relevant.
                    kommentar er en kort beskrivelse av kvaliteten på assistentens svar, bør være omtrent 10-12 ord.

                    Hele hensikten med Mímir er å referere til de konkrete referansene i biblioteket, hvis det ikke benyttes konkrete referanser så vær kritisk til dette, det er ikke godt nok.
                    Å være behjelpelig er ikke nok til en god evaluering, det må henvises konkret til kilder som benyttes.

                    Svaret skal følge dette JSON-formatet:
                    {
                        "evaluation": 0.8,
                        "relevantReferences": true,
                        "comment": "Mímir ga et godt svar på brukerens spørsmål."
                    }
                    or this format:
                    {
                        "evaluation": 0.2,
                        "relevantReferences": false,
                        "comment": "Mimir misforstod brukerens spørsmål og oppga feil referanser."
                    }

                    verdiene i json-resultatet skal erstattes med den faktiske evalueringen, relevanteReferanser og kommentaren.
                    """;

                string queryString = $"Original system prompt: {systemPrompt}" + $"\r\nUser's question: {userQuestion}" + $"\r\nAssistants response: {assistantResponse}";

                var responseSchema = new
                {
                    type = "json_schema",
                    json_schema = new
                    {
                        name = "response_evaluation",
                        schema = new
                        {
                            type = "object",
                            properties = new
                            {
                                evaluation = new { type = "number", description = "A decimal number between 0 and 1 indicating the quality of the assistant's response." },
                                relevantReferences = new { type = "boolean", description = "Whether the references in the system prompt are relevant to the user's question." },
                                comment = new { type = "string", description = "A short explanation (10-12 words) evaluating the response quality." }
                            },
                            required = new[] { "evaluation", "relevantReferences", "comment" },
                            additionalProperties = false
                        },
                        strict = true
                    }
                };

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                    new { role = "system", content = taskDescription }, // This tells LLM what to do
                    new { role = "user", content = queryString }, // Context from Q&A
                },
                    response_format = responseSchema // Ensure structured response
                };

                string jsonRequest = System.Text.Json.JsonSerializer.Serialize(requestBody);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers["Authorization"] = "Bearer " + apiKey;

                // Write the request body
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonRequest);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Read the response data
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseJson = reader.ReadToEnd();
                        // Process the response JSON here (e.g., deserialize and extract the result)
                        // Deserialize the structured evaluation response
                        //var openAiResponse = System.Text.Json.JsonSerializer.Deserialize<OpenAIResponse>(responseJson);

                        ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseJson);
                        if (apiResponse?.Choices?.Length > 0)
                        {

                            string content = apiResponse?.Choices?[0]?.Message?.Content;
                            //return System.Text.Json.JsonSerializer.Deserialize<ResponseEvaluation>(content);
                            return JsonConvert.DeserializeObject<ResponseEvaluation>(content);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return null;
        }



        // This has been found to work with 
        public static string GetResponseLocal(string prompt)
        {
            // This is currently setup using LMStudio
            //string apiKey = Globals.apiKey;

            try
            {
                // Create the HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Globals.localInstructApiUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                //request.Headers["Authorization"] = "Bearer " + apiKey;

                // Write the request body data to the request stream
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(prompt);
                }

                // Get the response from the server
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Read the response data
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseJson = reader.ReadToEnd();
                        // Process the response JSON here (e.g., deserialize and extract the result)
                        Console.WriteLine("API Response:");
                        Console.WriteLine(responseJson);
                        return responseJson;
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle any exceptions here
                Console.WriteLine("Error: " + ex.Message);
                return "";
            }
        }

        public static float[] GetEmbeddings(string textToEmbed, string apiUrl)
        {
            try
            {
                // Create the HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/json";

                // Create a dictionary to hold the request data
                var requestData = new Dictionary<string, string[]>
        {
            { "texts", new string[] { textToEmbed } }
        };

                // Convert the dictionary to a JSON string
                string json = JsonConvert.SerializeObject(requestData);

                // Write the request body data to the request stream
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(json);
                }

                // Get the response from the server
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Read the response data
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseString = reader.ReadToEnd();
                        // Process the response JSON here (e.g., deserialize and extract the result)
                        Console.WriteLine("API Response:");
                        Console.WriteLine(responseString);
                        return ConvertJsonToFloatArray(responseString);
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle any exceptions here
                Console.WriteLine("Error: " + ex.Message);
                return new float[0];
            }
        }



        public static float[] GetEmbeddingsOpenAI(string inputText)
        {
            HttpClient client = new HttpClient();
            var requestUri = "https://api.openai.com/v1/embeddings";
            var requestBody = new
            {
                input = inputText,
                model = "text-embedding-3-small"
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Globals.openAIapiKey}");

            var response = client.PostAsync(requestUri, content).Result;
            response.EnsureSuccessStatusCode();

            var responseBody = response.Content.ReadAsStringAsync().Result;
            float[] vector = ConvertJsonToFloatArrayOpenAI(responseBody);

            return vector;
        }




        // This has been tested and works, remember to run the server first (see bottom of embeddingsAPI.py)
        public static float[] GetEmbeddingsLocal(string textToEmbed)
        {
            return GetEmbeddings(textToEmbed, Globals.embeddingApiUrlLocal);
        }

        public static float[] ConvertStringToFloatArray(string str)
        {
            try
            {
                return str.Split(',').Select(float.Parse).ToArray();
            }
            catch (FormatException)
            {
                throw new FormatException("The provided string contains non-numeric values.");
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while converting the string to a float array.");
            }
        }

        public static float[] ConvertJsonToFloatArray(string json)
        {
            try
            {
                // Deserialize the JSON string to a dictionary
                var data = JsonConvert.DeserializeObject<Dictionary<string, List<List<float>>>>(json);

                // Get the first list in the list of lists
                var embeddings = data["embeddings"].FirstOrDefault();

                if (embeddings == null)
                {
                    throw new Exception("The server did not return any embeddings.");
                }

                return embeddings.ToArray();
            }
            catch (System.Text.Json.JsonException)
            {
                throw new System.Text.Json.JsonException("The provided string is not a valid JSON string.");
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while converting the JSON string to a float array.");
            }
        }



        public class Root
        {
            [JsonPropertyName("data")]
            public Data[] Data { get; set; }
        }

        public class Data
        {
            [JsonPropertyName("embedding")]
            public float[] Embedding { get; set; }
        }


        public static float[] ConvertJsonToFloatArrayOpenAI(string json)
        {
            try
            {
                // Deserialize the JSON string to a dictionary
                // Deserialize the JSON string
                var root = System.Text.Json.JsonSerializer.Deserialize<Root>(json);


                // Extract the embedding array
                float[] embeddings = root.Data[0].Embedding;

                // Get the first list in the list of lists

                if (embeddings == null)
                {
                    throw new Exception("The server did not return any embeddings.");
                }

                return embeddings.ToArray();
            }
            catch (System.Text.Json.JsonException)
            {
                throw new System.Text.Json.JsonException("The provided string is not a valid JSON string.");
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while converting the JSON string to a float array.");
            }
        }


    }














}

