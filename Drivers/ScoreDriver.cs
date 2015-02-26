using System.Linq;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.Services;
using GoodM8s.TheCup.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace GoodM8s.TheCup.Drivers {
    public class ScoreDriver : ContentPartDriver<ScorePart> {
        private readonly IScoreService _scoreService;
        private readonly ICupService _cupService;
        private readonly IEventService _eventService;
        private readonly ITeamService _teamService;

        public ScoreDriver(IScoreService scoreService, ICupService cupService, IEventService eventService, ITeamService teamService) {
            _scoreService = scoreService;
            _cupService = cupService;
            _eventService = eventService;
            _teamService = teamService;
        }

        protected override string Prefix {
            get { return "Score"; }
        }

        private ScoreEditViewModel BuildEditorViewModel(ScorePart part) {
            var cups = _cupService.Get();
            var events = _eventService.Get();
            var teams = _teamService.Get();

            var teamScores = part.TeamScores.Select(teamScore => new TeamScoreEditViewModel {
                Id = teamScore.Id,
                Score = teamScore.Score,
                TeamId = teamScore.TeamPartRecord.Id,
                Teams = teams,
                Penalties = teamScore.Penalties
            }).ToList();

            return new ScoreEditViewModel {
                CupId = part.CupId,
                Cups = cups,
                EventId = part.EventId,
                Events = events,
                Notes = part.Notes,
                TeamScores = teamScores
            };
        }

        protected override DriverResult Editor(ScorePart part, dynamic shapeHelper) {
            return ContentShape("Parts_Score_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/Score", Model: BuildEditorViewModel(part), Prefix: Prefix));
        }

        protected override DriverResult Editor(ScorePart part, IUpdateModel updater, dynamic shapeHelper) {
            var model = new ScoreEditViewModel();

            updater.TryUpdateModel(model, Prefix, null, null);

            if (part.ContentItem.Id != 0) {
                _scoreService.UpdateForContentItem(part.ContentItem, model);
            }

            return Editor(part, shapeHelper);
        }
    }
}