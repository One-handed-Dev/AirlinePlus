using System;
using Common.Domain;
using Newtonsoft.Json.Linq;

namespace Common.Infrastructure
{
    public class BaseJsonRepo<TDomain> : IBaseJsonRepo<TDomain> where TDomain : new()
    {
        #region Init
        private static string Section => typeof(TDomain).Name;
        private static JObject Json => IBaseJsonRepo<TDomain>.GetJson();
        #endregion

        public void Set(TDomain command)
        {
            var json = Json;
            var domainProperties = command.GetType().GetProperties();

            foreach (var domainProperty in domainProperties)
            {
                if (domainProperty.GetValue(command) is null or "") continue;

                var type = domainProperty.PropertyType;
                var token = json.SelectToken($"{Section}.{domainProperty.Name}");
                if (type == typeof(int)) token?.Replace((int)domainProperty.GetValue(command));
                else if (type == typeof(long)) token?.Replace((long)domainProperty.GetValue(command));
                else if (type == typeof(bool)) token?.Replace((bool)domainProperty.GetValue(command));
                else if (type == typeof(string)) token?.Replace((string)domainProperty.GetValue(command));
                else if (type == typeof(DateTime)) token?.Replace((DateTime)domainProperty.GetValue(command));
                else token?.Replace(domainProperty.GetValue(command).ToString());
            }

            IBaseJsonRepo<TDomain>.SetJson(json.ToString());
        }

        public TDomain Get() => Json.TryGetValue(Section, out JToken token) ? token.ToObject<TDomain>() : new();
    }
}
