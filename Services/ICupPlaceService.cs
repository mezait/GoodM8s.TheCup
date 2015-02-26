using System.Collections.Generic;
using GoodM8s.TheCup.Models;
using Orchard;

namespace GoodM8s.TheCup.Services {
    public interface ICupPlaceService : IDependency {
        void Create(CupPlaceRecord entity);
        void Delete(CupPlaceRecord entity);
        IEnumerable<CupPlaceRecord> GetByCup(int cupId);
    }
}