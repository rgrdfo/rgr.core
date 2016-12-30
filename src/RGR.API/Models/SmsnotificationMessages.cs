using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class SmsnotificationMessages
    {
        public long Id { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
        public bool Sended { get; set; }
        public DateTime? DateEnqueued { get; set; }
        public DateTime? DateSended { get; set; }
    }
}
