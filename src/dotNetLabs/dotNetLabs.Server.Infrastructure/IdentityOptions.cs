using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLabs.Server.Infrastructure
{
    public class IdentityOptions
    {

        public string UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }

    }
}
