using OrcamentoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace OrcamentoApp.Filters
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity (Usuario u, string AuthenticationType)
        {
            Name = u.Login;
            this.AuthenticationType = AuthenticationType;
            IsAuthenticated = true;
            Usuario = u;
        }

        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public Usuario Usuario { get; set; }
    }
}