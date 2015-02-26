using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using GoodM8s.Core.ViewModels;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Settings;
using Orchard.UI.Admin;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;

namespace GoodM8s.TheCup.Controllers {
    [Admin]
    public class EventAdminController : Controller, IUpdateModel {
        private readonly IOrchardServices _orchardServices;
        private readonly IEventService _eventService;
        private readonly ISiteService _siteService;

        public EventAdminController(IOrchardServices orchardServices, IEventService eventService, ISiteService siteService, IShapeFactory shapeFactory) {
            _orchardServices = orchardServices;
            _eventService = eventService;
            _siteService = siteService;

            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        private dynamic Shape { get; set; }

        private Localizer T { get; set; }

        public ActionResult Index(PagerParameters pagerParameters) {
            var eventsProjection = from e in _eventService.Get()
                select Shape.Event
                    (
                        Id: e.Id,
                        Name: e.Name,
                        Description: e.Description
                    );

            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters.Page, pagerParameters.PageSize);

            var model = new DynamicIndexViewModel(
                eventsProjection.Skip(pager.GetStartIndex()).Take(pager.PageSize),
                Shape.Pager(pager).TotalItemCount(_eventService.Count()));

            return View(model);
        }

        public ActionResult Create() {
            var e = _orchardServices.ContentManager.New<EventPart>("Event");
            var model = _orchardServices.ContentManager.BuildEditor(e)
                ;

            return View((object) model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePOST() {
            var e = _orchardServices.ContentManager.New<EventPart>("Event");

            _orchardServices.ContentManager.Create(e)
                ;

            var model = _orchardServices.ContentManager.UpdateEditor(e,
                this)
                ;

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error creating event!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Event created successfully."));

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var e = _eventService.Get(id);
            var model = _orchardServices.ContentManager.BuildEditor(e)
                ;

            return View((object) model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPOST(int id) {
            var e = _eventService.Get(id);
            var model = _orchardServices.ContentManager.UpdateEditor(e,
                this)
                ;

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error updating event!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Event updated successfully."));

            return RedirectToAction("Edit", new {id});
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            var e = _eventService.Get(id);

            if (e == null) {
                return HttpNotFound();
            }

            _orchardServices.ContentManager.Remove(e.ContentItem);

            _orchardServices.Notifier.Information(T("Event deleted successfully."));

            return RedirectToAction("Index");
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}