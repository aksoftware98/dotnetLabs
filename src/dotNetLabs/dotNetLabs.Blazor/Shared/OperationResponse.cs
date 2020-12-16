using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLabs.Shared
{
    public class OperationResponse<T> : BaseResponse
    {

        public T Data { get; set; }

    }
}
