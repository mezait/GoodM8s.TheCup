using Orchard.Localization;
using Orchard.UI.Navigation;

namespace GoodM8s.TheCup {
    public class AdminMenu : INavigationProvider {
        public AdminMenu() {
            T = NullLocalizer.Instance;
        }

        private Localizer T { get; set; }

        public string MenuName {
            get { return "admin"; }
        }

        public void GetNavigation(NavigationBuilder builder) {
            builder
                .AddImageSet("goodm8s")
                .Add(menu => menu
                    .Caption(T("GoodM8s Cup"))
                    .Position("21")
                    .LinkToFirstChild(false)
                    .Add(submenu => submenu
                        .Caption(T("Attendees"))
                        .Position("21.1")
                        .Action("Index", "AttendeeAdmin", new {area = "GoodM8s.TheCup"}))
                    .Add(submenu => submenu
                        .Caption(T("Events"))
                        .Position("21.2")
                        .Action("Index", "EventAdmin", new { area = "GoodM8s.TheCup" }))
                    .Add(submenu => submenu
                        .Caption(T("Cups"))
                        .Position("21.3")
                        .Action("Index", "CupAdmin", new { area = "GoodM8s.TheCup" }))
                    .Add(submenu => submenu
                        .Caption(T("Teams"))
                        .Position("21.4")
                        .Action("Index", "TeamAdmin", new { area = "GoodM8s.TheCup" }))
                    .Add(submenu => submenu
                        .Caption(T("Scores"))
                        .Position("21.5")
                        .Action("Index", "ScoreAdmin", new { area = "GoodM8s.TheCup" }))
                );
        }
    }
}