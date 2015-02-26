using System.Collections.Generic;
using GoodM8s.TheCup.Models;
using Orchard.Data;

namespace GoodM8s.TheCup.Services {
    public class TeamAttendeeService : ITeamAttendeeService {
        private readonly IRepository<TeamAttendeeRecord> _teamAttendeeRepository;

        public TeamAttendeeService(IRepository<TeamAttendeeRecord> teamAttendeeRepository) {
            _teamAttendeeRepository = teamAttendeeRepository;
        }

        public void Create(TeamAttendeeRecord entity) {
            _teamAttendeeRepository.Create(entity);
        }

        public void Delete(TeamAttendeeRecord entity) {
            _teamAttendeeRepository.Delete(entity);
        }

        public IEnumerable<TeamAttendeeRecord> GetByTeam(int teamId) {
            return _teamAttendeeRepository.Fetch(s => s.TeamPartRecord.Id == teamId);
        }
    }
}