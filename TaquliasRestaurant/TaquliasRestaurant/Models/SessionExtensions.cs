using System.Text.Json;

namespace TaquliasRestaurant.Models
{
    public static class SessionExtensions
    {
        public static void set<T>(this ISession session, string key, T value)
        {
            session.SetString(key,JsonSerializer.Serialize(value));
        }
        public static T get<T>(this ISession session, string key) 
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
