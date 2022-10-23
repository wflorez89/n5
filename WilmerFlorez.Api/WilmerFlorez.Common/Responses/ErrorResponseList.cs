using System.Collections.Generic;

namespace WilmerFlorez.Common.Responses
{
    public class ErrorResponseList
    {
        public int Status { get; set; }

        public List<string> MessageList { get; set; }
    }
}
