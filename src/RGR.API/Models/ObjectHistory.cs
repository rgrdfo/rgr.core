using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectHistory
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public short HistoryStatus { get; set; }
        public long ClientId { get; set; }
        public long CompanyId { get; set; }
        public string CustomerName { get; set; }
        public DateTime? DelayDate { get; set; }
        public long? DelayReason { get; set; }
        public DateTime? DateCreated { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? AdvanceEndDate { get; set; }
        public long RdvagentId { get; set; }
        public long NonRdvagentId { get; set; }

        public virtual NonRdvAgents NonRdvagent { get; set; }
        public virtual EstateObjects Object { get; set; }
        public virtual Users Rdvagent { get; set; }
    }
}
