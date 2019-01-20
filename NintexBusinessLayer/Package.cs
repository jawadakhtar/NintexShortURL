using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nintex.BusinessLayer
{
    public class Package : BusinessEntities
    {
        public enum PackageType
        {
            Monthly= 1, Yearly = 2
        }
        public string Title { get; set; }
        public PackageType Type { get; set; }
        public Double Charges { get; set; }

        public bool Active { get; set; }

        public Package()
        {
            Active = true;
        }
    }
}