using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Redarbor.DataRepository.DbModels.SqlServer
{
    public partial class Portal
    {
        public Portal()
        {
            Employee = new HashSet<Employee>();
        }

        public int PortalId { get; set; }
        public string Name { get; set; }
        public short StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
