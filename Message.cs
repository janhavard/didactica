using Newtonsoft.Json;

namespace Didactica
{


    public class Message
    {
        public Message(Role role, string content)
        {
            Role = role.ToString();
            Content = content;
        }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        public override string ToString()
        {
            string role;
            if (Role == "user")
                role = "Bruker";
            else if (Role == "assistant")
                role = "Mímir";
            else if (Role == "system")
                role = "system";
            else if (Role == "systemMessageNotSentToAPI")
                role = "";
            else
                role = "";
            
            if (role == "")
                return this.Content;
            else
                return role + ": " + this.Content;
        }

        public Role RoleEnum
        {
            get
            {
                return (Role)Enum.Parse(typeof(Role), Role);
            }
        }
    }

    // These are the three roles used by openAI api
    public enum Role
    {
        user,
        assistant,
        system,
        systemMessageNotSentToAPI
    }

}
