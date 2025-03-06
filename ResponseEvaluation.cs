using Newtonsoft.Json;

namespace Didactica
{


    public class ResponseEvaluation
    {

        [JsonProperty("evaluation")]
        public double Evaluation { get; set; }

        [JsonProperty("relevantReferences")]
        public bool RelevantReferences { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }



    }

}

