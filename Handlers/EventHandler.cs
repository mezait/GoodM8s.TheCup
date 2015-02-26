using GoodM8s.TheCup.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace GoodM8s.TheCup.Handlers {
    public class EventHandler : ContentHandler {
        public EventHandler(IRepository<EventPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));

            OnRemoved<EventPart>((context, part) => repository.Delete(part.Record));
        }
    }
}