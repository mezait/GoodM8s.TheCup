using System.Collections.Generic;
using System.Linq;
using GoodM8s.TheCup.Models;
using Orchard.Data;

namespace GoodM8s.TheCup.Services {
    public class CupPlaceService : ICupPlaceService {
        private readonly IRepository<CupPlaceRecord> _cupPlaceRepository;

        public CupPlaceService(IRepository<CupPlaceRecord> cupPlaceRepository) {
            _cupPlaceRepository = cupPlaceRepository;
        }

        public void Create(CupPlaceRecord entity) {
            _cupPlaceRepository.Create(entity);
        }

        public void Delete(CupPlaceRecord entity) {
            _cupPlaceRepository.Delete(entity);
        }

        public IEnumerable<CupPlaceRecord> GetByCup(int cupId) {
            return _cupPlaceRepository
                .Fetch(s => s.CupPartRecord.Id == cupId)
                .OrderBy(s => s.Place);
        }
    }
}