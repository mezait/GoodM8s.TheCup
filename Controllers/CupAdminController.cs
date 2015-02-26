using System.Linq;
using System.Web.Mvc;
using GoodM8s.Core.ViewModels;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.Services;
using GoodM8s.TheCup.ViewModels;
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
    public class CupAdminController : Controller, IUpdateModel {
        private readonly IOrchardServices _orchardServices;
        private readonly ICupService _cupService;
        private readonly IEventService _eventService;
        private readonly ISiteService _siteService;

        public CupAdminController(IOrchardServices orchardServices, ICupService cupService, IEventService eventService, ISiteService siteService, IShapeFactory shapeFactory) {
            _orchardServices = orchardServices;
            _siteService = siteService;
            _cupService = cupService;
            _eventService = eventService;

            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        private dynamic Shape { get; set; }

        private Localizer T { get; set; }

        public ActionResult Index(PagerParameters pagerParameters) {
            var cupsProjection = from cup in _cupService.Get()
                select Shape.Cup
                    (
                        Id: cup.Id,
                        Date: cup.Date,
                        Title: cup.Title
                    );

            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters.Page, pagerParameters.PageSize);

            var model = new DynamicIndexViewModel(
                cupsProjection.Skip(pager.GetStartIndex()).Take(pager.PageSize),
                Shape.Pager(pager).TotalItemCount(_cupService.Count()));

            return View(model);
        }

        public ActionResult Create() {
            var cup = _orchardServices.ContentManager.New<CupPart>("Cup");
            var model = _orchardServices.ContentManager.BuildEditor(cup);

            return View((object) model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePOST() {
            var cup = _orchardServices.ContentManager.New<CupPart>("Cup");

            _orchardServices.ContentManager.Create(cup);

            var model = _orchardServices.ContentManager.UpdateEditor(cup, this);

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error creating cup!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Cup created successfully."));

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var cup = _cupService.Get(id);
            var model = _orchardServices.ContentManager.BuildEditor(cup);

            return View((object) model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPOST(int id) {
            var cup = _cupService.Get(id);
            var model = _orchardServices.ContentManager.UpdateEditor(cup, this);

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error updating cup!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Cup updated successfully."));

            return RedirectToAction("Edit", new {id});
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            var cup = _cupService.Get(id);

            if (cup == null) {
                return HttpNotFound();
            }

            _orchardServices.ContentManager.Remove(cup.ContentItem);

            _orchardServices.Notifier.Information(T("Cup deleted successfully."));

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Add a event order to the list
        /// </summary>
        /// <returns>EventOrder view</returns>
        public ActionResult AddEventOrder() {
            return PartialView("EditorTemplates/EventOrderEditViewModel", new EventOrderEditViewModel {
                Events = _eventService.Get()
            });
        }

        /// <summary>
        /// Add a placement to the list
        /// </summary>
        /// <returns>EventOrder view</returns>
        public ActionResult AddPlace()
        {
            return PartialView("EditorTemplates/CupPlaceEditViewModel", new CupPlaceEditViewModel());
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}