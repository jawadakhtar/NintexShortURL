using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Nintex.BusinessLayer
{
    public class NintexIdentity : IIdentity
    {
        Client m_Client;
        public NintexIdentity(Client aClient)
        {
            m_Client = aClient;
            IsAuthenticated = false;
        }

        public string AuthenticationType
        {
            get
            {
                return "Nintex Identity Server";
            }
        }

        public bool IsAuthenticated
        {
            get;
            private set;
        }

        public string Name
        {
            get
            {
                return m_Client.Name;
            }
        }
    }
}