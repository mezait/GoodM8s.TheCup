using GoodM8s.TheCup.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace GoodM8s.TheCup.Handlers {
    public class TeamHandler : ContentHandler {
        public TeamHandler(IContentManager contentManager, IRepository<TeamPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));

            OnRemoved<TeamPart>((context, part) => repository.Delete(part.Record));

            OnLoading<TeamPart>((context, part) => part.CupField.Loader(item => part.CupId != null
                ? contentManager.Get<CupPart>(part.CupId.Value)
                : null));
        }
    }
}