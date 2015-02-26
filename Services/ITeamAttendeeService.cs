using System.Collections.Generic;
using GoodM8s.TheCup.Models;
using Orchard;

namespace GoodM8s.TheCup.Services {
    public interface ITeamAttendeeService : IDependency {
        void Create(TeamAttendeeRecord entity);
        void Delete(TeamAttendeeRecord entity);
        IEnumerable<TeamAttendeeRecord> GetByTeam(int teamId);
    }
}