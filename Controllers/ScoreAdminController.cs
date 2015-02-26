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
    public class ScoreAdminController : Controller, IUpdateModel {
        private readonly IOrchardServices _orchardServices;
        private readonly IScoreService _scoreService;
        private readonly ITeamService _teamService;
        private readonly ISiteService _siteService;

        public ScoreAdminController(IOrchardServices orchardServices, IScoreService scoreService, ITeamService teamService, ISiteService siteService, IShapeFactory shapeFactory) {
            _orchardServices = orchardServices;
            _siteService = siteService;
            _scoreService = scoreService;
            _teamService = teamService;

            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        private dynamic Shape { get; set; }

        private Localizer T { get; set; }

        public ActionResult Index(PagerParameters pagerParameters) {
            var scoreProjection = from score in _scoreService.Get()
                select Shape.Cup
                    (
                        Id: score.Id,
                        Cup: score.Cup.Title,
                        Event: score.Event.Name
                    );

            var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters.Page, pagerParameters.PageSize);

            var model = new DynamicIndexViewModel(
                scoreProjection.Skip(pager.GetStartIndex()).Take(pager.PageSize),
                Shape.Pager(pager).TotalItemCount(_scoreService.Count()));

            return View(model);
        }

        public ActionResult Create(int? cupId) {
            var score = _orchardServices.ContentManager.New<ScorePart>("Score");

            score.CupId = cupId;

            var model = _orchardServices.ContentManager.BuildEditor(score);

            return View((object) model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePOST() {
            var score = _orchardServices.ContentManager.New<ScorePart>("Score");

            _orchardServices.ContentManager.Create(score);

            var model = _orchardServices.ContentManager.UpdateEditor(score, this);

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error creating score!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Score created successfully."));

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var score = _scoreService.Get(id);
            var model = _orchardServices.ContentManager.BuildEditor(score);

            return View((object) model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPOST(int id) {
            var score = _scoreService.Get(id);
            var model = _orchardServices.ContentManager.UpdateEditor(score, this);

            if (!ModelState.IsValid) {
                _orchardServices.TransactionManager.Cancel();

                _orchardServices.Notifier.Error(T("Error updating score!"));

                return View((object) model);
            }

            _orchardServices.Notifier.Information(T("Score updated successfully."));

            return RedirectToAction("Edit", new {id});
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            var score = _scoreService.Get(id);

            if (score == null) {
                return HttpNotFound();
            }

            _orchardServices.ContentManager.Remove(score.ContentItem);

            _orchardServices.Notifier.Information(T("Score deleted successfully."));

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Add a team score to the list
        /// </summary>
        /// <returns>TeamScore view</returns>
        public ActionResult AddTeamScore() {
            return PartialView("EditorTemplates/TeamScoreEditViewModel", new TeamScoreEditViewModel() {
                Teams = _teamService.Get()
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