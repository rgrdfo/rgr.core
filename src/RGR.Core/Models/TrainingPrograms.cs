using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class TrainingPrograms
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime? TrainingDate { get; set; }
        public string ProgramName { get; set; }
        public string Organizer { get; set; }
        public string TrainingPlace { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public string CertificateFile { get; set; }

        public virtual Users User { get; set; }
    }
}
