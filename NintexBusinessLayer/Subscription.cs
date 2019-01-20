using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nintex.BusinessLayer
{
    public class Subscription : BusinessEntities
    {
        public Client Client { get; set; }
        public Package Package { get; set; }
        
        public DateTime SubscriptionDate { get; set; }

        public DateTime Expiry
        {
            get
            {
                if (this.Package.Type == Package.PackageType.Monthly)
                    return SubscriptionDate.AddDays(30);
                else //for yearly package
                    return SubscriptionDate.AddDays(365);
            }
        }

        public bool IsValid
        {
            get
            {
                return IsPackageValid();
            }
        }

        private bool IsPackageValid()
        {
            if (Package.Type == Package.PackageType.Monthly)
            {
                return SubscriptionDate.AddDays(30) >= DateTime.Now;
            }
            else //for Package.Type.Yearly
            {
                return SubscriptionDate.AddDays(365) >= DateTime.Now;
            }
        }
    }
}