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

namespace GoodM8s.TheCup.Controllers
{
    [Admin]
    public class AttendeeAdminController : Controller, IUpdateModel {
        private readonly IOrchardServices _orchardServices;
        private readonly IAttendeeService _attendeeService;
        private readonly ISiteService _siteService;

        public AttendeeAdminController(IOrchardServices orchardServices, IAttendeeService attendeeService, ISiteService siteService, IShapeFactory shapeFactory) {
            _orchardServices = orchardServices;
            _attendeeService = attendeeService;
            _siteService = siteService;

            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        private dynamic Shape { get; set; }

        private Localizer T { get; set; }

        public ActionResult Index(PagerParameters pagerParameters) {
            var attendeesProjection = from attendee in _attendeeService.Get()
                                    select Shape.Attendee
                                        (
                                            Id: attendee.Id,
                                            FirstName: attendee.FirstName,
                                            LastName: attendee.LastName
                                        );

            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters.Page, pagerParameters.PageSize);

            var model = new DynamicIndexViewModel(
                attendeesProjection.Skip(pager.GetStartIndex()).Take(pager.PageSize),
                Shape.Pager(pager).TotalItemCount(_attendeeService.Count()));

            return View(model);
        }

        public ActionResult Create() {
            var attendee = _orchardServices.ContentManager.New<AttendeePart>("Attendee");
            var model = _orchardServices.ContentManager.BuildEditor(attendee);

            return View((object) model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePOST() {
            var attendee = _orchardServices.ContentManager.New<AttendeePart>("Attendee");

            _orchardServices.ContentManager.Create(attendee);

            var model = _orchardServices.ContentManager.UpdateEditor(attendee, this);

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error creating attendee!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Attendee created successfully."));

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var attendee = _attendeeService.Get(id);
            var model = _orchardServices.ContentManager.BuildEditor(attendee);

            return View((object) model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPOST(int id) {
            var attendee = _attendeeService.Get(id);
            var model = _orchardServices.ContentManager.UpdateEditor(attendee, this);

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error updating attendee!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Attendee updated successfully."));

            return RedirectToAction("Edit", new {id});
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            var attendee = _attendeeService.Get(id);

            if (attendee == null) {
                return HttpNotFound();
            }

            _orchardServices.ContentManager.Remove(attendee.ContentItem);

            _orchardServices.Notifier.Information(T("Attendee deleted successfully."));

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