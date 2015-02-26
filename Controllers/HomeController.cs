using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.Services;
using GoodM8s.TheCup.ViewModels;
using Orchard.Themes;

namespace GoodM8s.TheCup.Controllers {
    [Themed]
    public class HomeController : Controller {
        private readonly ICupService _cupService;
        private readonly IScoreService _scoreService;
        private readonly ITeamService _teamService;

        public HomeController(ICupService cupService, IScoreService scoreService, ITeamService teamService) {
            _cupService = cupService;
            _scoreService = scoreService;
            _teamService = teamService;
        }

        public ActionResult Results(int? cupId) {
            var cup = !cupId.HasValue
                ? _cupService.GetMostRecent()
                : _cupService.Get(cupId.Value);

            // var teams = _teamService.GetByCup(cup.Id);
            var scores = _scoreService.GetByCup(cup.Id);

            var results = new CupResultsViewModel {
                Cup = cup,
                EventScores = new List<EventScoresViewModel>(),
                TeamTotals = new Dictionary<TeamPartRecord, TeamScoreTotalsViewModel>()
            };

            var placePoints = cup.Placement.ToDictionary(t => t.Place ?? 0, t => t.Points ?? 0);

            foreach (var e in cup.EventOrders.OrderBy(o => o.SortOrder)) {
                foreach (var score in scores.Where(score => score.Event.Id == e.EventPartRecord.Id)) {
                    var eventScores = new EventScoresViewModel {
                        Event = score.Event,
                        TeamScores = score.TeamScores.OrderByDescending(s => s.Score).ToList()
                    };

                    results.EventScores.Add(eventScores);

                    var i = 1;

                    // Tally points
                    foreach (var teamScore in eventScores.TeamScores) {
                        var key = teamScore.TeamPartRecord;

                        if (!results.TeamTotals.ContainsKey(key)) {
                            results.TeamTotals.Add(key, new TeamScoreTotalsViewModel
                            {
                                TotalPenalties = teamScore.Penalties ?? 0,
                                TotalScore = placePoints[i]
                            });
                        }
                        else {
                            results.TeamTotals[key].TotalPenalties += teamScore.Penalties ?? 0;
                            results.TeamTotals[key].TotalScore += placePoints[i];
                        }

                        i++;
                    }
                }
            }

            return View(results);
        }
    }
}