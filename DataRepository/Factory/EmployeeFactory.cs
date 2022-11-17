using Redarbor.DataRepository.Interface;
using Microsoft.Extensions.Configuration;
using sfEntities = Redarbor.SystemFramework.Entities;

namespace Redarbor.DataRepository.Factory
{
    public abstract class EmployeeFactory
    {
        public abstract IDatabase<sfEntities.Employee.Employee, sfEntities.Find.Employee> GetInstance(IConfiguration config);
    }

    public class AgentEmployeeFactory : EmployeeFactory
    {
        public override IDatabase<sfEntities.Employee.Employee, sfEntities.Find.Employee> GetInstance(IConfiguration config)
        {
            return new DataRepository.DataModels.Employee(config);
        }

    }
}
