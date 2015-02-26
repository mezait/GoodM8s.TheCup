using System.Collections.Generic;
using System.Linq;
using GoodM8s.TheCup.Models;
using Orchard.Data;

namespace GoodM8s.TheCup.Services {
    public class EventOrderService : IEventOrderService {
        private readonly IRepository<EventOrderRecord> _eventOrderRepository;

        public EventOrderService(IRepository<EventOrderRecord> eventOrderRepository) {
            _eventOrderRepository = eventOrderRepository;
        }

        public void Create(EventOrderRecord entity) {
            _eventOrderRepository.Create(entity);
        }

        public void Delete(EventOrderRecord entity) {
            _eventOrderRepository.Delete(entity);
        }

        public IEnumerable<EventOrderRecord> GetByCup(int cupId) {
            return _eventOrderRepository
                .Fetch(s => s.CupPartRecord.Id == cupId)
                .OrderBy(s => s.SortOrder);
        }
    }
}