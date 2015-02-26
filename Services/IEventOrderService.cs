using System.Collections.Generic;
using GoodM8s.TheCup.Models;
using Orchard;

namespace GoodM8s.TheCup.Services {
    public interface IEventOrderService : IDependency {
        void Create(EventOrderRecord entity);
        void Delete(EventOrderRecord entity);
        IEnumerable<EventOrderRecord> GetByCup(int cupId);
    }
}