using System;

namespace Redarbor.SystemFramework.Entities.Find
{
    public class Employee : FindBase
    {

        #region Properties
        public Guid? EmployeeId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public short StatusId { get; set; }

        #endregion
    }
}
