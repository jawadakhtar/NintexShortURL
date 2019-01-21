using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.BusinessLayer.URLShortener
{
    /// <summary>
    /// A Base64 implementation to generate the Short URL from Long URL
    /// </summary>
    class Base64Shortener : IURLShortenStrategy
    {        
        public string GenerateShortURL(string aLongURL, int aDividend)
        {
            return generateHash(aLongURL, aDividend);
        }

        private string generateHash(string aLongURL, Double aDividend)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(aDividend.ToString());
            string IdHash = System.Convert.ToBase64String(plainTextBytes);

            return IdHash;
        }
    }
}