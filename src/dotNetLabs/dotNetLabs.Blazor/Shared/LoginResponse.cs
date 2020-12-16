using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLabs.Shared
{
    public class LoginResponse : BaseResponse
    {

        public string AccessToken { get; set; }
        public DateTime? ExpiryDate { get; set; }
       

    }
}
