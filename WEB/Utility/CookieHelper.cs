using System;
using System.Web.Helpers;
using WEB.Enviroment;

namespace WEB.Utility
{
    internal static class CookieHelper
    {
        internal static string EncodeUserData(UserData data)
        {
            if (data == null)
                return null;

            var encodedData = string.Format("Role={0};Id={1}", data.Role, data.Id);

            return encodedData;
        }

        internal static UserData DecodeUserData(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return null;

            var lines = data.Split(';');
            var decodedData = new UserData
            {
                Role = lines[0].Split('=')[1],
                Id = Guid.Parse(lines[1].Split('=')[1])
            };

            return decodedData;
        }
    }
}