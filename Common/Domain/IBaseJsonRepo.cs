using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Domain
{
    public interface IBaseJsonRepo<TDomain> where TDomain : new()
    {
        TDomain Get();
        void Set(TDomain command);
        static void SetJson(string newJson) => File.WriteAllText("Meta.json", newJson);
        static JObject GetJson() => JsonConvert.DeserializeObject(File.ReadAllText("Meta.json")) as JObject;
    }
}
