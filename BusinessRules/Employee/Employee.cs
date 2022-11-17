﻿using Redarbor.DataRepository.Factory;
using Redarbor.DataRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using sfEntities = Redarbor.SystemFramework.Entities;
using sfSecurity = Redarbor.SystemFramework.Utils.Security;
using sfValidate = Redarbor.SystemFramework.Utils.Validator;

namespace Redarbor.BusinessRules.Employee
{
    public class Employee
    {

        #region Properties

        private EmployeeFactory factory;
        private IDatabase<sfEntities.Employee.Employee, sfEntities.Find.Employee> employee;

        #endregion

        #region Constructors

        public Employee(IConfiguration config)
        {
            this.factory = new AgentEmployeeFactory();
            this.employee = factory.GetInstance(config);
        }

        #endregion

        #region Cruds

        /// <summary>
        /// Use for system administrator to add a employee without email verificartion
        /// </summary>
        /// <param name="objEmployee"></param>
        /// <returns></returns>
        private sfEntities.Employee.Employee Insert(sfEntities.Employee.Employee objEmployee)
        {
            objEmployee.CreatedOn = DateTime.UtcNow;
            sfEntities.Employee.Employee result = employee.Insert(objEmployee);
            return result;
        }

        /// <summary>
        /// Use for get a employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private sfEntities.Employee.Employee Get(Guid employeeId)
        {
            employee.sfFind = new sfEntities.Find.Employee() { EmployeeId = employeeId };
            sfEntities.Employee.Employee result = employee.Get();
            return result;
        }

        /// <summary>
        /// Use for get a employee by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private sfEntities.Employee.Employee GetByEmail(string email)
        {
            employee.sfFind = new sfEntities.Find.Employee() { Email = email };
            sfEntities.Employee.Employee result = employee.Get();
            return result;
        }

        /// <summary>
        /// Get employee by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private sfEntities.Employee.Employee GetByUserName(string userName)
        {
            employee.sfFind = new sfEntities.Find.Employee() { Username = userName };
            sfEntities.Employee.Employee result = employee.Get();
            return result;
        }

        /// <summary>
        /// Use for change employee data for a system administrator
        /// </summary>
        /// <param name="objEmployee"></param>
        /// <returns></returns>
        private sfEntities.Employee.Employee Update(sfEntities.Employee.Employee objEmployee)
        {
            sfEntities.Employee.Employee result = employee.Update(objEmployee);
            return result;
        }

        /// <summary>
        /// Use for delete a employee
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private bool Delete(Guid employeeId)
        {
            bool result = employee.Delete(new sfEntities.Employee.Employee() { EmployeeId = employeeId });
            return result;
        }

        /// <summary>
        /// List of employees by specific query
        /// </summary>
        /// <param name="sfFind"></param>
        /// <returns></returns>
        private List<sfEntities.Employee.Employee> List(sfEntities.Find.Employee sfFind)
        {
            employee.sfFind = sfFind;
            List<sfEntities.Employee.Employee> result = employee.List();
            return result;
        }

        #endregion

        #region Public Metods

        /// <summary>
        /// Use for get a employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public sfEntities.Employee.Employee Load(Guid employeeId)
        {
            sfEntities.Employee.Employee result = this.Get(employeeId);
            return result;
        }

        /// <summary>
        /// Use for delete a employee
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public bool Remove(Guid employeeId)
        {
            return this.Delete(employeeId);
        }

        /// <summary>
        /// Use for add a employee with email verification
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public sfEntities.Employee.Employee Registrer(sfEntities.Employee.Employee objEmployee)
        {
            sfEntities.Employee.Employee result = null;
            if (this.IsValidData(objEmployee))
            {
                objEmployee.StatusId = (short)sfEntities.Enumerator.Status.Active;
                objEmployee.Password = sfSecurity.EncodePassword(objEmployee.Password);
                result = this.Insert(objEmployee);
            }
            return result;
        }

        /// <summary>
        /// Use for login a employee
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public sfEntities.Employee.Employee Login(string login, string password)
        {
            sfEntities.Employee.Employee employeeData = this.ValidateCredentials(login, password);
            if (this.IsValidData(employeeData))
            {
                employeeData.LastLogin = DateTime.Now;
                employeeData = this.Update(employeeData);
            }
            return employeeData;
        }

        /// <summary>
        /// Use for validate a employee credentials
        /// </summary>
        /// <param name="objEmployee"></param>
        /// <returns></returns>
        public sfEntities.Employee.Employee ValidateCredentials(sfEntities.Employee.Employee objEmployee)
        {
            bool isValid = false;
            sfEntities.Employee.Employee employee = this.Get(objEmployee.EmployeeId);
            if (this.IsValidData(employee)) isValid = sfSecurity.VerifyPassword(employee.Password, objEmployee.Password);
            if (!isValid) employee = null;            
            return employee;
        }

        /// <summary>
        /// Use for system to upodate date of last access
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public sfEntities.Employee.Employee UpdateLastAccess(Guid employeeId, sfEntities.Employee.Employee objEmployee)
        {
            sfEntities.Employee.Employee result = null;
            sfEntities.Employee.Employee employee = this.Get(employeeId);
            if (this.IsValidData(employee))
            {
                employee.Telephone = objEmployee.Telephone;
                employee.Fax = objEmployee.Fax;
                employee.PortalId = objEmployee.PortalId;
                employee.Email = objEmployee.Email;
                employee.Email = objEmployee.Email;
                employee.CompanyId = objEmployee.CompanyId;
                employee.LastLogin = objEmployee.LastLogin;
                employee.UpdatedOn = objEmployee.UpdatedOn;
                employee.Username = objEmployee.Username;
                result = this.Update(employee);
            }
            return result;
        }


        /// <summary>
        /// Use for system to upodate date of last access
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public sfEntities.Employee.Employee UpdateLastAccess(Guid employeeId, DateTime currentDate)
        {
            sfEntities.Employee.Employee result = null;
            sfEntities.Employee.Employee employee = this.Get(employeeId);
            if (this.IsValidData(employee))
            {
                employee.LastLogin = currentDate;
                result = this.Update(employee);
            }
            return result;
        }

        /// <summary>
        /// Use for change password of a employee
        /// </summary>
        /// <param name="objEmployee"></param>
        /// <returns></returns>
        public sfEntities.Employee.Employee ChangePassword(sfEntities.Employee.Employee objEmployee, string newPassword)
        {
            sfEntities.Employee.Employee result = this.ValidateCredentials(objEmployee);
            if (this.IsValidData(result) && !string.IsNullOrEmpty(newPassword))
            {
                result.Password = sfSecurity.EncodePassword(newPassword);
                result = employee.Update(result);
            }
            return result;
        }

        /// <summary>
        /// Use for search Employees with parameters on sfFind
        /// </summary>
        /// <param name="sfFind"></param>
        /// <returns></returns>
        public List<sfEntities.Employee.Employee> Search(sfEntities.Find.Employee sfFind)
        {
            List<sfEntities.Employee.Employee> result = this.List(sfFind);
            return result;
        }

        /// <summary>
        /// Use for verify if exists a employee by username or email
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public sfEntities.Employee.Employee Exists(string login)
        {
            sfEntities.Employee.Employee result = this.FindEmployee(login);
            return result;
        }

        /// <summary>
        /// Use for validate input values of a employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool IsValidData(sfEntities.Employee.Employee employee)
        {
            return (employee != null && employee != new sfEntities.Employee.Employee()
                && !string.IsNullOrEmpty(employee.Username) && !string.IsNullOrEmpty(employee.Password)
                && !string.IsNullOrEmpty(employee.Email) && sfValidate.IsValidEmailAddress(employee.Email));
        }

        #endregion

        #region Private Metods

        /// <summary>
        /// Use for search employee by username or email
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private sfEntities.Employee.Employee FindEmployee(string login)
        {
            sfEntities.Employee.Employee employee = null;
            if (!string.IsNullOrEmpty(login)) employee = this.GetByUserName(login);            
            if (employee == null) employee = this.GetByEmail(login);            
            return employee;
        }

        /// <summary>
        /// Use for validate employee credentialss
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private sfEntities.Employee.Employee ValidateCredentials(string login, string password)
        {
            bool isValid = false;
            sfEntities.Employee.Employee employee = this.FindEmployee(login);
            if (this.IsValidData(employee)) isValid = sfSecurity.VerifyPassword(employee.Password, password);
            if (!isValid) employee = null;
            return employee;
        }

        #endregion

    }
}