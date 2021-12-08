using AchiveClub.Server.Models;
using AchiveClub.Shared.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClub.Server.Mappers
{
    public class SupervisorToSupervisorInfoMapper
    {

        static public SupervisorInfo Map(Supervisor supervisor)
        {
            return new SupervisorInfo
            {
                Id = supervisor.Id,
                Name = supervisor.Name,
                Key = supervisor.Key
            };
        }

        static public Supervisor Revert(SupervisorInfo supervisorInfo)
        {
            return new Supervisor
            {
                Id = supervisorInfo.Id,
                Name = supervisorInfo.Name,
                Key = supervisorInfo.Key
            };
        }
    }
}
