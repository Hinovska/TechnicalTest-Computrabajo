using Redarbor.DataRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using sfEntities = Redarbor.SystemFramework.Entities;

namespace Redarbor.DataRepository.DataModels
{
    public class Employee : IDatabase<sfEntities.Employee.Employee, sfEntities.Find.Employee>
    {
        public sfEntities.Find.Employee sfFind { get; set; }
        private IConfiguration _config;

        public Employee(IConfiguration config)
        {
            this._config = config;
        }

        public sfEntities.Employee.Employee Insert(sfEntities.Employee.Employee objEmployee)
        {
            sfEntities.Employee.Employee result = null;
            using (var context = new DbModels.SqlServer.RedarborContext(_config))
            {
                DbModels.SqlServer.Employee newEmployee = new DbModels.SqlServer.Employee()
                {
                    Name = objEmployee.Name,
                    CompanyId = objEmployee.CompanyId,
                    PortalId = objEmployee.PortalId,
                    RoleId = objEmployee.RoleId,
                    StatusId = objEmployee.StatusId,
                    Email = objEmployee.Email,
                    Fax = objEmployee.Fax,
                    Telephone = objEmployee.Telephone,
                    Username = objEmployee.Username,
                    Password = objEmployee.Password,
                    CreatedOn = objEmployee.CreatedOn,
                    UpdatedOn = objEmployee.UpdatedOn,
                    DeletedOn = objEmployee.DeletedOn,
                    LastLogin = objEmployee.LastLogin
                };
                context.Employee.Add(newEmployee);
                context.SaveChanges();
                context.Entry(newEmployee).GetDatabaseValues();
                objEmployee.EmployeeId = newEmployee.EmployeeId;
                result = objEmployee;
            }
            return result;
        }

        public sfEntities.Employee.Employee Update(sfEntities.Employee.Employee objEmployee)
        {
            sfEntities.Employee.Employee result = null;
            using (var context = new DbModels.SqlServer.RedarborContext(_config))
            {
                var std = context.Employee.Single(a => a.EmployeeId == objEmployee.EmployeeId);
                std.Name = objEmployee.Name;
                std.CompanyId = objEmployee.CompanyId;
                std.PortalId = objEmployee.PortalId;
                std.RoleId = objEmployee.RoleId;
                std.StatusId = objEmployee.StatusId;
                std.Email = objEmployee.Email;
                std.Fax = objEmployee.Fax;
                std.Telephone = objEmployee.Telephone;
                std.Username = objEmployee.Username;
                std.Password = objEmployee.Password;
                std.UpdatedOn = objEmployee.UpdatedOn;
                std.DeletedOn = objEmployee.DeletedOn;
                std.LastLogin = objEmployee.LastLogin;
                context.Employee.Update(std);
                context.SaveChanges();
                result = new sfEntities.Employee.Employee()
                {
                    EmployeeId = std.EmployeeId,
                    Name = std.Name,
                    CompanyId = std.CompanyId,
                    PortalId = std.PortalId,
                    RoleId = std.RoleId,
                    StatusId = std.StatusId,
                    Email = std.Email,
                    Fax = std.Fax,
                    Telephone = std.Telephone,
                    Username = std.Username,
                    Password = std.Password,
                    CreatedOn = std.CreatedOn,
                    UpdatedOn = std.UpdatedOn,
                    DeletedOn = std.DeletedOn,
                    LastLogin = std.LastLogin
                };
            }
            return result;
        }

        public bool Delete(sfEntities.Employee.Employee objEmployee)
        {
            bool result = false;
            using (var context = new DbModels.SqlServer.RedarborContext(_config))
            {
                Expression<Func<DbModels.SqlServer.Employee, bool>> whereClause = a => a.EmployeeId.Equals(objEmployee.EmployeeId);
                var item = context.Employee.FirstOrDefault(whereClause);
                if (item != null)
                {
                    context.Remove(item);
                    context.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        public sfEntities.Employee.Employee Get()
        {
            sfEntities.Employee.Employee result = null;
            using (var context = new DbModels.SqlServer.RedarborContext(_config))
            {
                DbModels.SqlServer.Employee item = null;
                if (sfFind.EmployeeId != null && sfFind.EmployeeId != Guid.Empty)
                {
                    Expression<Func<DbModels.SqlServer.Employee, bool>> whereClause = a => a.EmployeeId.Equals(sfFind.EmployeeId);
                    item = context.Employee.FirstOrDefault(whereClause);
                }
                else if (!string.IsNullOrEmpty(sfFind.Username))
                {
                    Expression<Func<DbModels.SqlServer.Employee, bool>> whereClause = a => a.Username.Equals(sfFind.Username);
                    item = context.Employee.FirstOrDefault(whereClause);
                }
                else if (!string.IsNullOrEmpty(sfFind.Email))
                {
                    Expression<Func<DbModels.SqlServer.Employee, bool>> whereClause = a => a.Email.Equals(sfFind.Email);
                    item = context.Employee.FirstOrDefault(whereClause);
                }
                if (item != null)
                {
                    result = new sfEntities.Employee.Employee()
                    {
                        EmployeeId = item.EmployeeId,
                        Name = item.Name,
                        CompanyId = item.CompanyId,
                        PortalId = item.PortalId,
                        RoleId = item.RoleId,
                        StatusId = item.StatusId,
                        Email = item.Email,
                        Fax = item.Fax,
                        Telephone = item.Telephone,
                        Username = item.Username,
                        Password = item.Password,
                        CreatedOn = item.CreatedOn,
                        UpdatedOn = item.UpdatedOn,
                        DeletedOn = item.DeletedOn,
                        LastLogin = item.LastLogin
                    };
                }
            }
            return result;
        }

        public List<sfEntities.Employee.Employee> List()
        {
            List<sfEntities.Employee.Employee> result = new List<sfEntities.Employee.Employee>();
            using (var context = new DbModels.SqlServer.RedarborContext(_config))
            {
                if (sfFind == null) sfFind = new sfEntities.Find.Employee();

                var employees = context.Employee;
                IQueryable<DbModels.SqlServer.Employee> query = null;

                if (sfFind.StatusId > 0) query = employees.Where(emp => emp.StatusId == sfFind.StatusId);
                if (!string.IsNullOrEmpty(sfFind.Username)) query = query.Where(f => f.Username == sfFind.Username);
                if (!string.IsNullOrEmpty(sfFind.Email)) query = query.Where(f => f.Email == sfFind.Email);
                if (sfFind.EmployeeId != null && sfFind.EmployeeId != Guid.Empty) query = query.Where(f => f.EmployeeId == sfFind.EmployeeId);

                if (query == null) query = employees.Where(emp => emp.StatusId != (short)sfEntities.Enumerator.Status.Deleted);
                var std = query.Select(f => f)
                    .OrderBy(p => p.CreatedOn)
                    .Skip(sfFind.PageSize * (sfFind.PageNumber - 1))
                    .Take(sfFind.PageSize).ToList();
                result = new List<sfEntities.Employee.Employee>();
                foreach (var item in std)
                {
                    result.Add(new sfEntities.Employee.Employee()
                    {
                        EmployeeId = item.EmployeeId,
                        Name = item.Name,
                        CompanyId = item.CompanyId,
                        PortalId = item.PortalId,
                        RoleId = item.RoleId,
                        StatusId = item.StatusId,
                        Email = item.Email,
                        Fax = item.Fax,
                        Telephone = item.Telephone,
                        Username = item.Username,
                        Password = item.Password,
                        CreatedOn = item.CreatedOn,
                        UpdatedOn = item.UpdatedOn,
                        DeletedOn = item.DeletedOn,
                        LastLogin = item.LastLogin
                    });
                }
            }
            return result;
        }
    }
}
