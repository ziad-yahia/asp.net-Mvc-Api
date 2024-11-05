using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Isca_Travels.Models
{
    public static class  SessionExtensions
    {
        public static void SetS<T>(this ISession session ,string key,T value)
        {
             session.SetString(key,JsonSerializer.Serialize(value));
        }
        public static T GetTS<T>(this ISession session, string key)
        {
           var json= session.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            else 
            {
               return JsonSerializer.Deserialize<T>(json);
            }
        }
    }
}
