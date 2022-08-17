using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Common.Domain
{
    public interface IBaseJsonRepo<TDomain> where TDomain : new()
    {
        TDomain Get();
        void Set(TDomain command);
        static void SetJson(string newJson) => File.WriteAllText("info.json", newJson);
        static JObject GetJson() => JsonConvert.DeserializeObject(File.ReadAllText("info.json")) as JObject;
    }
}
