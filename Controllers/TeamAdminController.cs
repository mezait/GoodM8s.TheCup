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

namespace GoodM8s.TheCup.Controllers
{
    [Admin]
    public class TeamAdminController : Controller, IUpdateModel {
        private readonly IOrchardServices _orchardServices;
        private readonly ITeamService _teamService;
        private readonly IAttendeeService _attendeeService;
        private readonly ISiteService _siteService;

        public TeamAdminController(IOrchardServices orchardServices, ITeamService teamService, IAttendeeService attendeeService, ISiteService siteService, IShapeFactory shapeFactory)
        {
            _orchardServices = orchardServices;
            _siteService = siteService;
            _teamService = teamService;
            _attendeeService = attendeeService;

            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        private dynamic Shape { get; set; }

        private Localizer T { get; set; }

        public ActionResult Index(PagerParameters pagerParameters) {
            var teamProjection = from team in _teamService.Get()
                select Shape.Cup
                    (
                        Id: team.Id,
                        Name: team.Name,
                        Cup: team.Cup.Title
                    );

            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters.Page, pagerParameters.PageSize);

            var model = new DynamicIndexViewModel(
                teamProjection.Skip(pager.GetStartIndex()).Take(pager.PageSize),
                Shape.Pager(pager).TotalItemCount(_teamService.Count()));

            return View(model);
        }

        public ActionResult Create() {
            var team =_orchardServices.ContentManager.New<TeamPart>("Team");
            var model = _orchardServices.ContentManager.BuildEditor(team);

            return View((object) model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePOST() {
            var team =_orchardServices.ContentManager.New<TeamPart>("Team");

            _orchardServices.ContentManager.Create(team);

            var model = _orchardServices.ContentManager.UpdateEditor(team, this);

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error creating team!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Team created successfully."));

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var team = _teamService.Get(id);
            var model = _orchardServices.ContentManager.BuildEditor(team);

            return View((object) model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPOST(int id) {
            var team =_teamService.Get(id);
            var model = _orchardServices.ContentManager.UpdateEditor(team, this);

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error updating team!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Team updated successfully."));

            return RedirectToAction("Edit", new {id});
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            var team =_teamService.Get(id);

            if (team == null) {
                return HttpNotFound();
            }

            _orchardServices.ContentManager.Remove(team.ContentItem);

            _orchardServices.Notifier.Information(T("Team deleted successfully."));

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Add a team order to the list
        /// </summary>
        /// <returns>TeamOrder view</returns>
        public ActionResult AddTeamAttendee() {
            return PartialView("EditorTemplates/TeamAttendeeEditViewModel", new TeamAttendeeEditViewModel() {
                Attendees = _attendeeService.Get()
            });
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage) {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}