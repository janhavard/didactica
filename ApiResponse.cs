namespace Didactica
{


    public class ApiResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public Choice[] Choices { get; set; }
        public Usage Usage { get; set; }

        // You can add other properties if needed, e.g., "Usage"
    }


    public class MessageLog
    {
        public Message[] messages { get; set; }

        // You can add other properties if needed, e.g., "Usage"
    }

    public class Choice
    {
        public int Index { get; set; }
        public Message Message { get; set; }
        public string FinishReason { get; set; }
    }

    public class Usage
    {
        public int TotalTokens { get; set; }
    }

}
