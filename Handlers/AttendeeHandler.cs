using GoodM8s.TheCup.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Users.Models;

namespace GoodM8s.TheCup.Handlers {
    public class AttendeeHandler : ContentHandler {
        public AttendeeHandler(IRepository<AttendeePartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<UserPart>("Attendee"));

            OnRemoved<AttendeePart>((context, part) => repository.Delete(part.Record));
        }
    }
}