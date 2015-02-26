using System.Collections.Generic;
using GoodM8s.TheCup.Models;
using Orchard;

namespace GoodM8s.TheCup.Services {
    public interface ITeamScoreService : IDependency {
        void Create(TeamScoreRecord entity);
        void Delete(TeamScoreRecord entity);
        IEnumerable<TeamScoreRecord> GetByScore(int scoreId);
    }
}