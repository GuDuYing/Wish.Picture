using Newtonsoft.Json;

namespace Wish.Picture.Web.Helpers
{
    public static class JsonHelper
    {
        public static T ToObject<T>(this string Json)
        {
            return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
        }
    }
}