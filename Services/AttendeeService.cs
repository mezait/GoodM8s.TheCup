using System.Collections.Generic;
using GoodM8s.Core.Services;
using GoodM8s.TheCup.Models;
using Orchard;

namespace GoodM8s.TheCup.Services {
    public class AttendeeService : BaseService<AttendeePart, AttendeePartRecord>, IAttendeeService {
        public AttendeeService(IOrchardServices orchardServices) : base(orchardServices) {}

        public new IEnumerable<AttendeePart> Get() {
            return GetQuery()
                .OrderBy(attendee => attendee.LastName)
                .OrderBy(attendee => attendee.FirstName)
                .List();
        }
    }
}