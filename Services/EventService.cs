using System.Collections.Generic;
using GoodM8s.Core.Services;
using GoodM8s.TheCup.Models;
using Orchard;

namespace GoodM8s.TheCup.Services {
    public class EventService : BaseService<EventPart, EventPartRecord>, IEventService {
        public EventService(IOrchardServices orchardServices) : base(orchardServices) {}

        public new IEnumerable<EventPart> Get() {
            return GetQuery().OrderBy(e => e.Name).List();
        }
    }
}