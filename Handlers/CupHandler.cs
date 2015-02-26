using GoodM8s.TheCup.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace GoodM8s.TheCup.Handlers {
    public class CupHandler : ContentHandler {
        public CupHandler(IContentManager contentManager, IRepository<CupPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));

            OnRemoved<CupPart>((context, part) => repository.Delete(part.Record));
        }
    }
}