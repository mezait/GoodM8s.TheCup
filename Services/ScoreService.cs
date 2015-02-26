using System.Collections.Generic;
using System.Linq;
using GoodM8s.Core.Services;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;

namespace GoodM8s.TheCup.Services {
    public class ScoreService : BaseService<ScorePart, ScorePartRecord>, IScoreService {
        private readonly ITeamScoreService _teamScoreService;
        private readonly ITeamService _teamService;

        public ScoreService(IOrchardServices orchardServices, ITeamScoreService teamScoreService, ITeamService teamService)
            : base(orchardServices) {
            _teamScoreService = teamScoreService;
            _teamService = teamService;
        }

        public void UpdateForContentItem(IContent item, ScoreEditViewModel model) {
            var scorePart = item.As<ScorePart>();

            scorePart.CupId = model.CupId;
            scorePart.EventId = model.EventId;
            scorePart.Notes = model.Notes;

            var oldTeamScores = _teamScoreService.GetByScore(scorePart.Id).ToList();

            // Make sure this is never null
            if (model.TeamScores == null) {
                model.TeamScores = new List<TeamScoreEditViewModel>();
            }

            foreach (var oldTeamScore in oldTeamScores) {
                var teamScoreModel = model.TeamScores.SingleOrDefault(m => m.Id == oldTeamScore.Id);

                if (teamScoreModel != null) {
                    // Update existing team scores
                    oldTeamScore.TeamPartRecord = teamScoreModel.TeamId.HasValue
                        ? _teamService.Get(teamScoreModel.TeamId.Value).Record
                        : null;
                    oldTeamScore.Score = teamScoreModel.Score;
                    oldTeamScore.Penalties = teamScoreModel.Penalties;
                }
                else {
                    // Delete the team scores that no longer exist
                    _teamScoreService.Delete(oldTeamScore);
                }
            }

            // Add the new team scores
            foreach (var teamScore in from teamScore in model.TeamScores
                let oldTeamScore = oldTeamScores.SingleOrDefault(m => m.Id == teamScore.Id)
                where oldTeamScore == null
                select teamScore) {
                _teamScoreService.Create(
                    new TeamScoreRecord {
                        Id = teamScore.Id,
                        ScorePartRecord = scorePart.Record,
                        TeamPartRecord = teamScore.TeamId.HasValue
                            ? _teamService.Get(teamScore.TeamId.Value).Record
                            : null,
                        Score = teamScore.Score,
                        Penalties = teamScore.Penalties
                    });
            }
        }

        public IEnumerable<ScorePart> GetByCup(int cupId) {
            return GetQuery().Where(s => s.CupId == cupId).List();
        }
    }
}