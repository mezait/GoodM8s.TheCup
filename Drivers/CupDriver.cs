using System.Linq;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.Services;
using GoodM8s.TheCup.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace GoodM8s.TheCup.Drivers {
    public class CupDriver : ContentPartDriver<CupPart> {
        private readonly ICupService _gameService;
        private readonly IEventService _eventService;

        public CupDriver(ICupService gameService, IEventService eventService) {
            _gameService = gameService;
            _eventService = eventService;
        }

        protected override string Prefix {
            get { return "Cup"; }
        }

        private CupEditViewModel BuildEditorViewModel(CupPart part) {
            var events = _eventService.Get();

            var eventOrders = part.EventOrders.Select(eventOrder => new EventOrderEditViewModel {
                Id = eventOrder.Id,
                EventId = eventOrder.EventPartRecord.Id,
                Events = events,
                SortOrder = eventOrder.SortOrder
            }).ToList();

            var placement = part.Placement.Select(place => new CupPlaceEditViewModel {
                Id = place.Id,
                Place = place.Place,
                Points = place.Points
            }).ToList();

            return new CupEditViewModel {
                Date = part.Date,
                Title = part.Title,
                EventOrders = eventOrders,
                Placement = placement
            };
        }

        protected override DriverResult Editor(CupPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Cup_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/Cup", Model: BuildEditorViewModel(part), Prefix: Prefix));
        }

        protected override DriverResult Editor(CupPart part, IUpdateModel updater, dynamic shapeHelper) {
            var model = new CupEditViewModel();

            updater.TryUpdateModel(model, Prefix, null, null);

            if (part.ContentItem.Id != 0) {
                _gameService.UpdateForContentItem(part.ContentItem, model);
            }

            return Editor(part, shapeHelper);
        }
    }
}