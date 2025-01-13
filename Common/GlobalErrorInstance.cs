using System.Text.Json;

namespace ProductData.Common
{
    public class GlobalErrorInstance : IGlobalErrorInstance
    {
        public GlobalErrorInstance()
        {
            GlobalClientErrorCode = JsonSerializer.Deserialize<GlobalClientErrorCodeModel>(File.ReadAllText(@"error_code.json"));
        }
        public GlobalClientErrorCodeModel GlobalClientErrorCode { get; set; }

    }

    public interface IGlobalErrorInstance
    {
        public GlobalClientErrorCodeModel GlobalClientErrorCode { get; set; }
    }

    public class GlobalClientErrorCodeModel
    {
        public string client_id { get; set; }
        public List<GlobalErrorCodeModel> error_codes { get; set; }

    }

    public class GlobalErrorCodeModel
    {
        public string error_code { get; set; }
        public string exception_name { get; set; }
        public string description_standard { get; set; }
        public string description_en { get; set; }
        public string description_th { get; set; }
        public string exception_namespace { get; set; }
    }
}
