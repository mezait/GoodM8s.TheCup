using System.Collections.Generic;
using System.Linq;
using GoodM8s.Core.Services;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.ViewModels;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Contents.ViewModels;

namespace GoodM8s.TheCup.Services {
    public class CupService : BaseService<CupPart, CupPartRecord>, ICupService {
        private readonly ICupPlaceService _cupPlaceService;
        private readonly IEventOrderService _eventOrderService;
        private readonly IEventService _eventService;

        public CupService(IOrchardServices orchardServices, ICupPlaceService cupPlaceService, IEventOrderService eventOrderService, IEventService eventService)
            : base(orchardServices) {
            _cupPlaceService = cupPlaceService;
            _eventOrderService = eventOrderService;
            _eventService = eventService;
        }

        public new IEnumerable<CupPart> Get() {
            return GetQuery().OrderBy(cup => cup.Date).List();
        }

        public CupPart GetMostRecent() {
            return GetQuery()
                .OrderByDescending(cup => cup.Date)
                .Slice(0, 1)
                .FirstOrDefault();
        }

        public void UpdateForContentItem(IContent item, CupEditViewModel model) {
            var cupPart = item.As<CupPart>();

            cupPart.Date = model.Date;
            cupPart.Title = model.Title;

            #region Event Orders

            var oldOrders = _eventOrderService.GetByCup(cupPart.Id).ToList();

            // Make sure these are never null
            if (model.EventOrders == null) {
                model.EventOrders = new List<EventOrderEditViewModel>();
            }
            if (model.Placement == null) {
                model.Placement = new List<CupPlaceEditViewModel>();
            }

            foreach (var order in oldOrders) {
                var orderModel = model.EventOrders.SingleOrDefault(m => m.Id == order.Id);

                if (orderModel != null) {
                    // Update existing orders
                    order.SortOrder = orderModel.SortOrder;
                    order.EventPartRecord = orderModel.EventId.HasValue
                        ? _eventService.Get(orderModel.EventId.Value).Record
                        : null;
                }
                else {
                    // Delete the orders that no longer exist
                    _eventOrderService.Delete(order);
                }
            }

            // Add the new orders
            foreach (var order in from order in model.EventOrders
                let oldOrder = oldOrders.SingleOrDefault(m => m.Id == order.Id)
                where oldOrder == null
                select order) {
                _eventOrderService.Create(
                    new EventOrderRecord {
                        SortOrder = order.SortOrder,
                        CupPartRecord = cupPart.Record,
                        Id = order.Id,
                        EventPartRecord = order.EventId.HasValue
                            ? _eventService.Get(order.EventId.Value).Record
                            : null,
                    });
            }

            #endregion

            #region Placement

            var oldPlaces = _cupPlaceService.GetByCup(cupPart.Id).ToList();

            // Make sure this is never null
            if (model.Placement == null) {
                model.Placement = new List<CupPlaceEditViewModel>();
            }

            foreach (var place in oldPlaces) {
                var placeModel = model.Placement.SingleOrDefault(m => m.Id == place.Id);

                if (placeModel != null) {
                    // Update existing places
                    place.Place = placeModel.Place;
                    place.Points = placeModel.Points;
                }
                else {
                    // Delete the places that no longer exist
                    _cupPlaceService.Delete(place);
                }
            }

            // Add the new places
            foreach (var place in from place in model.Placement
                let oldPlace = oldPlaces.SingleOrDefault(m => m.Id == place.Id)
                where oldPlace == null
                select place) {
                _cupPlaceService.Create(
                    new CupPlaceRecord {
                        CupPartRecord = cupPart.Record,
                        Id = place.Id,
                        Place = place.Place,
                        Points = place.Points
                    });
            }

            #endregion
        }
    }
}