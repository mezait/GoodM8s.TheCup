using System.Collections.Generic;
using System.Linq;
using GoodM8s.Core.Services;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.ViewModels;
using Orchard;
using Orchard.ContentManagement;

namespace GoodM8s.TheCup.Services {
    public class TeamService : BaseService<TeamPart, TeamPartRecord>, ITeamService {
        private readonly ITeamAttendeeService _teamAttendeeService;
        private readonly IAttendeeService _attendeeService;

        public TeamService(IOrchardServices orchardServices, ITeamAttendeeService teamAttendeeService, IAttendeeService attendeeService)
            : base(orchardServices) {
            _teamAttendeeService = teamAttendeeService;
            _attendeeService = attendeeService;
        }

        public new IEnumerable<TeamPart> Get() {
            return GetQuery().OrderBy(team => team.Name).List();
        }

        public IEnumerable<TeamPart> GetByCup(int cupId) {
            return GetQuery()
                .Where(team => team.CupId == cupId)
                .OrderBy(team => team.Name)
                .List();
        }

        public void UpdateForContentItem(IContent item, TeamEditViewModel model) {
            var teamPart = item.As<TeamPart>();

            teamPart.CupId = model.CupId;
            teamPart.Name = model.Name;

            var oldAttendees = _teamAttendeeService.GetByTeam(teamPart.Id).ToList();

            // Make sure this is never null
            if (model.TeamAttendees == null) {
                model.TeamAttendees = new List<TeamAttendeeEditViewModel>();
            }

            foreach (var attendee in oldAttendees) {
                var attendeeModel = model.TeamAttendees.SingleOrDefault(m => m.Id == attendee.Id);

                if (attendeeModel != null) {
                    // Update existing attendees
                    attendee.AttendeePartRecord = attendeeModel.AttendeeId.HasValue
                        ? _attendeeService.Get(attendeeModel.AttendeeId.Value).Record
                        : null;
                }
                else {
                    // Delete the attendees that no longer exist
                    _teamAttendeeService.Delete(attendee);
                }
            }

            // Add the new attendees
            foreach (var attendee in from attendee in model.TeamAttendees
                let oldAttendee = oldAttendees.SingleOrDefault(m => m.Id == attendee.Id)
                where oldAttendee == null
                select attendee) {
                _teamAttendeeService.Create(
                    new TeamAttendeeRecord {
                        TeamPartRecord = teamPart.Record,
                        Id = attendee.Id,
                        AttendeePartRecord = attendee.AttendeeId.HasValue
                            ? _attendeeService.Get(attendee.AttendeeId.Value).Record
                            : null
                    });
            }
        }
    }
}