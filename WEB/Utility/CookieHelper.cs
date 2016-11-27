using System.Web.Helpers;
using WEB.Enviroment;

namespace WEB.Utility
{
    internal static class CookieHelper
    {
        internal static string EncodeData<T>(T data)
        {
            if (data == null)
                return null;

            var encodedData = Json.Encode(data);

            return encodedData;
        }

        internal static T DecodeUserData<T>(string data) where T : class
        {
            if (string.IsNullOrWhiteSpace(data))
                return null;

            var decodedData = Json.Decode<T>(data);

            return decodedData;
        }
    }
}