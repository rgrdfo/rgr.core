using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class MailNotificationMessages
    {
        public long Id { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool Sended { get; set; }
        public DateTime? DateEnqued { get; set; }
        public DateTime? DateSended { get; set; }
    }
}
