using System.Collections.Generic;
using GoodM8s.TheCup.Models;
using Orchard.Data;

namespace GoodM8s.TheCup.Services {
    public class TeamScoreService : ITeamScoreService {
        private readonly IRepository<TeamScoreRecord> _teamScoreRepository;

        public TeamScoreService(IRepository<TeamScoreRecord> teamScoreRepository) {
            _teamScoreRepository = teamScoreRepository;
        }

        public void Create(TeamScoreRecord entity) {
            _teamScoreRepository.Create(entity);
        }

        public void Delete(TeamScoreRecord entity) {
            _teamScoreRepository.Delete(entity);
        }

        public IEnumerable<TeamScoreRecord> GetByScore(int scoreId) {
            return _teamScoreRepository.Fetch(s => s.ScorePartRecord.Id == scoreId);
        }
    }
}