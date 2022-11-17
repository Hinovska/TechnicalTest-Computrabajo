using System;

namespace Redarbor.SystemFramework.Entities.Employee
{
    public class Employee
    {

        #region Properties

        public Guid EmployeeId { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int PortalId { get; set; }
        public int RoleId { get; set; }
        public short StatusId { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Telephone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public DateTime? LastLogin { get; set; }

        #endregion

    }
}
