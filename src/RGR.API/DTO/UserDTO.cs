using System;

namespace RGR.API.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public long? CompanyId { get; set; }
        public long? RoleId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CertificateNumber { get; set; }
        public DateTime? CertificationDate { get; set; }
    }
}
