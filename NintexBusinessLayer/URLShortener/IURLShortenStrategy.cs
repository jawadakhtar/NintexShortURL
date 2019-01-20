using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.BusinessLayer.URLShortener
{
    interface IURLShortenStrategy
    {
        string GenerateShortURL(string aLongUrl, int aDividend);
    }
}
