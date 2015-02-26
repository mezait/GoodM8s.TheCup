using System.Linq;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.Services;
using GoodM8s.TheCup.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace GoodM8s.TheCup.Drivers {
    public class TeamDriver : ContentPartDriver<TeamPart> {
        private readonly ITeamService _teamService;
        private readonly IAttendeeService _attendeeService;
        private readonly ICupService _cupService;

        public TeamDriver(ITeamService teamService, IAttendeeService attendeeService, ICupService cupService)
        {
            _teamService = teamService;
            _attendeeService = attendeeService;
            _cupService = cupService;
        }

        protected override string Prefix {
            get { return "Team"; }
        }

        private TeamEditViewModel BuildEditorViewModel(TeamPart part) {
            var attendees = _attendeeService.Get();
            var cups = _cupService.Get();

            var teamAttendees = part.TeamMates.Select(teamMate => new TeamAttendeeEditViewModel {
                Id = teamMate.Id,
                AttendeeId = teamMate.AttendeePartRecord.Id,
                Attendees = attendees
            }).ToList();

            return new TeamEditViewModel {
                Cups = cups,
                CupId = part.CupId,
                Name = part.Name,
                TeamAttendees = teamAttendees
            };
        }

        protected override DriverResult Editor(TeamPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Team_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/Team", Model: BuildEditorViewModel(part), Prefix: Prefix));
        }

        protected override DriverResult Editor(TeamPart part, IUpdateModel updater, dynamic shapeHelper) {
            var model = new TeamEditViewModel();

            updater.TryUpdateModel(model, Prefix, null, null);

            if (part.ContentItem.Id != 0) {
                _teamService.UpdateForContentItem(part.ContentItem, model);
            }

            return Editor(part, shapeHelper);
        }
    }
}