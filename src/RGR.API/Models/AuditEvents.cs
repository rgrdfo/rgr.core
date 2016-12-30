using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class AuditEvents
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime EventDate { get; set; }
        public short EventType { get; set; }
        public string Message { get; set; }
        public string Ip { get; set; }
        public string BrowserInfo { get; set; }
        public string AdditionalInformation { get; set; }

        public virtual Users User { get; set; }
    }
}
