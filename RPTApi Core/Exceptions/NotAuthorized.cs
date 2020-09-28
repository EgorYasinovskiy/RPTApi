using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Text;

namespace RPTApi.Exceptions
{
    public class NotAuthorizedException:Exception
    {
        public NotAuthorizedException(string message):base(message){}
        public NotAuthorizedException():base("You are not authorized to make this request"){}
    }
}
