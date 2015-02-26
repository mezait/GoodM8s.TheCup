using GoodM8s.TheCup.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace GoodM8s.TheCup.Handlers {
    public class ScoreHandler : ContentHandler {
        public ScoreHandler(IContentManager contentManager, IRepository<ScorePartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));

            OnRemoved<ScorePart>((context, part) => repository.Delete(part.Record));

            OnLoading<ScorePart>((context, part) => part.CupField.Loader(item => part.CupId != null
                ? contentManager.Get<CupPart>(part.CupId.Value)
                : null));

            OnLoading<ScorePart>((context, part) => part.EventField.Loader(item => part.EventId != null
                ? contentManager.Get<EventPart>(part.EventId.Value)
                : null));
        }
    }
}