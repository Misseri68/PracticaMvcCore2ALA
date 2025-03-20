
using Newtonsoft.Json;

namespace PracticaMvcCore2ALA.Extensions
{
    public static class SessionExtension
    {

        public static T GetObject<T>(this ISession session, string key)
        {
            string datos = session.GetString(key);
            if(datos == null)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(datos);
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            string datos = JsonConvert.SerializeObject(value);
            session.SetString(key, datos);
        }


    }
}
