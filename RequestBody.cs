using Newtonsoft.Json;

namespace Didactica
{

    public class RequestBody
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("temperature")]
        public double Temperature { get; set; }

        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        public RequestBody(string model, double temperature, int maxTokens)
        {
            Model = model;
            Temperature = temperature;
            MaxTokens = maxTokens;
            Messages = new List<Message>();
        }
        public RequestBody(double temperature, int maxTokens)
        {
            Temperature = temperature;
            MaxTokens = maxTokens;
            Messages = new List<Message>();
        }


        public void AddMessage(Role role, string content)
        {
            Messages.Add(new Message(role, content));
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }



        public static string CreateDefaultRequestBody(IEnumerable<Message> messages, string promptTemplate)
        {
            int maxTokens = Globals.maxTokens;
            double temperature = 0.05;
            RequestBody requestBody = new RequestBody(Globals.model, temperature, maxTokens);
            // Dette er nå satt opp i LM-studio
            requestBody.AddMessage(Role.system, promptTemplate);
            foreach (var message in messages.Where(m => m.RoleEnum != Role.systemMessageNotSentToAPI))
                requestBody.AddMessage(message.RoleEnum, message.Content);
            return requestBody.ToJsonString();
        }

    }

}
