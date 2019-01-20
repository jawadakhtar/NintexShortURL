using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.BusinessLayer.URLShortener
{
    class Base64Shortener : IURLShortenStrategy
    {
        char[] BASE64_ALPHABET = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_' };

        public string GenerateShortURL(string aLongURL, int aDividend)
        {
            return generateHash(aLongURL, aDividend);
        }

        private string generateHash(string aLongURL, Double aDividend)
        {
            //string hash = "";
            //List<Double> hashDigits = new List<Double>();
            //Double remainder = 0;

            //while (aDividend > 0) {
            //    remainder = Math.Floor(aDividend % 62);
            //    aDividend = Math.Floor(aDividend / 62);
            //    hashDigits.Insert(0, aDividend);
            //}

            //foreach (int digit in hashDigits ) {
            //    hash += BASE64_ALPHABET[digit];
            //}

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(aDividend.ToString());
            string IdHash = System.Convert.ToBase64String(plainTextBytes);

            var longUrlBase = System.Convert.FromBase64String(IdHash);
            string longUrl = System.Text.Encoding.UTF8.GetString(longUrlBase);

            plainTextBytes = System.Text.Encoding.UTF8.GetBytes(aLongURL);
            string urlHash = System.Convert.ToBase64String(plainTextBytes);

            return IdHash;
        }
    }
}