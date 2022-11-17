using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using brAplication = Redarbor.BusinessRules;
using sfEntities = Redarbor.SystemFramework.Entities;

namespace Redarbor.WebApp.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RedarborController : ControllerBase
    {
        private brAplication.Employee.Employee brEmployee;
        private IConfiguration _config;
        private readonly ILogger<RedarborController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public RedarborController(ILogger<RedarborController> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            this.brEmployee = new brAplication.Employee.Employee(_config);
        }

        // GET: api/v1/<RedarborController>
        [HttpGet]
        public IActionResult Get()
        {
            sfEntities.OperationAPI<List<sfEntities.Employee.Employee>> objResponse = new sfEntities.OperationAPI<List<sfEntities.Employee.Employee>>();
            try
            {
                objResponse.Data = brEmployee.Search(null);
                objResponse.StatusCode = HttpStatusCode.OK.ToString();
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                objResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                objResponse.Message = ex.Message;
                return NotFound(objResponse);
            }
        }

        // GET: api/v1/<RedarborController>
        [HttpGet("filter")]
        public IActionResult Get([FromBody] sfEntities.Find.Employee sfFind)
        {
            if (sfFind == null) sfFind = new sfEntities.Find.Employee();
            sfEntities.OperationAPI<List<sfEntities.Employee.Employee>> objResponse = new sfEntities.OperationAPI<List<sfEntities.Employee.Employee>>();
            try
            {
                objResponse.Data = brEmployee.Search(sfFind);
                objResponse.StatusCode = HttpStatusCode.OK.ToString();
                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                objResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                objResponse.Message = ex.Message;
                return NotFound(objResponse);
            }
        }

        // GET api/v1/<RedarborController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                sfEntities.OperationAPI<sfEntities.Employee.Employee> objResponse = new sfEntities.OperationAPI<sfEntities.Employee.Employee>();
                try
                {
                    objResponse.Data = brEmployee.Load(id);
                    objResponse.StatusCode = HttpStatusCode.OK.ToString();
                    return Ok(objResponse);
                }
                catch (Exception ex)
                {
                    objResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                    objResponse.Message = ex.Message;
                    return NotFound(objResponse);
                }
            }
            return BadRequest();
        }

        // POST api/v1/<RedarborController>
        [HttpPost()]
        public IActionResult Register([FromBody] sfEntities.Employee.Employee objNewEmployee)
        {
            if (brEmployee.IsValidData(objNewEmployee))
            {
                sfEntities.OperationAPI<sfEntities.Employee.Employee> objResponse = new sfEntities.OperationAPI<sfEntities.Employee.Employee>();
                try
                {
                    if (brEmployee.Exists(objNewEmployee.Username) == null && brEmployee.Exists(objNewEmployee.Email) == null)
                    {
                        objResponse.Data = brEmployee.Registrer(objNewEmployee);
                        objResponse.Message = "Employee " + objNewEmployee.Name + "added.";
                        objResponse.StatusCode = HttpStatusCode.OK.ToString();
                        return Ok(objResponse);
                    }
                    else
                    {
                        objResponse.Message = "Employee already exists";
                        objResponse.StatusCode = HttpStatusCode.Conflict.ToString();
                        return Conflict(objResponse);
                    }
                }
                catch (Exception ex)
                {
                    objResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                    objResponse.Message = ex.Message;
                    return NotFound(objResponse);
                }
            }
            return BadRequest();
        }

        // POST api/v1/<RedarborController>
        [HttpPost("auth")]
        public IActionResult Login([FromBody] sfEntities.Authentication.Credentials objCredentials)
        {
            if (!string.IsNullOrEmpty(objCredentials.Login) && !string.IsNullOrEmpty(objCredentials.Password))
            {
                sfEntities.OperationAPI<sfEntities.Employee.Employee> objResponse = new sfEntities.OperationAPI<sfEntities.Employee.Employee>();
                try
                {
                    objResponse.Data = brEmployee.Login(objCredentials.Login, objCredentials.Password);
                    if (objResponse.Data != null)
                    {
                        objResponse.Message = "Welcome " + objResponse.Data.Username + "!!!";
                        objResponse.StatusCode = HttpStatusCode.OK.ToString();
                        return Ok(objResponse);
                    }
                    else
                    {
                        objResponse.Message = "Wrong credetials try again !!!";
                        objResponse.StatusCode = HttpStatusCode.Unauthorized.ToString();
                        return Unauthorized(objResponse);
                    }
                }
                catch (Exception ex)
                {
                    objResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                    objResponse.Message = ex.Message;
                    return NotFound(objResponse);
                }
            }
            return BadRequest();
        }

        // PUT api/v1/<RedarborController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] sfEntities.Employee.Employee objEmployee)
        {
            if (brEmployee.IsValidData(objEmployee) && id != null && id != Guid.Empty)
            {
                sfEntities.OperationAPI<sfEntities.Employee.Employee> objResponse = new sfEntities.OperationAPI<sfEntities.Employee.Employee>();
                try
                {
                    objResponse.Data = brEmployee.Update(id, objEmployee);
                    if (objResponse.Data != null)
                    {
                        objResponse.Message = "Employee " + id.ToString() + " is updated";
                        objResponse.StatusCode = HttpStatusCode.OK.ToString();
                        return Ok(objResponse);
                    }
                    else
                    {
                        objResponse.Data = null;
                        objResponse.StatusCode = HttpStatusCode.Unauthorized.ToString();
                        return Unauthorized(objResponse);
                    }
                }
                catch (Exception ex)
                {
                    objResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                    objResponse.Message = ex.Message;
                    return NotFound(objResponse);
                }
            }
            return BadRequest();
        }

        // DELETE api/v1/<RedarborController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                sfEntities.OperationAPI<sfEntities.Employee.Employee> objResponse = new sfEntities.OperationAPI<sfEntities.Employee.Employee>();
                try
                {
                    if (brEmployee.Remove(id))
                    {
                        objResponse.Data = null;
                        objResponse.Message = "Employee " + id.ToString() + " is deleted";
                        objResponse.StatusCode = HttpStatusCode.OK.ToString();
                        return Ok(objResponse);
                    }
                    else
                    {
                        objResponse.Data = null;
                        objResponse.StatusCode = HttpStatusCode.NotFound.ToString();
                        return NotFound(objResponse);
                    }
                }
                catch (Exception ex)
                {
                    objResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                    objResponse.Message = ex.Message;
                    return NotFound(objResponse);
                }
            }
            return BadRequest();
        }
    }
}
