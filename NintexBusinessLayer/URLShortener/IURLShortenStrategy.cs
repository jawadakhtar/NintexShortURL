using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.BusinessLayer.URLShortener
{
    /// <summary>
    /// An Interface for the Strategy pattern to generate the Short URLs
    /// </summary>
    interface IURLShortenStrategy
    {
        string GenerateShortURL(string aLongUrl, int aDividend);
    }
}
