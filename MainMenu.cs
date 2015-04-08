using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Navigation;

namespace GoodM8s.TheCup {
    public class MainMenu : IMenuProvider {

        public MainMenu()
        {
            T = NullLocalizer.Instance;
        }

        private Localizer T { get; set; }

        public void GetMenu(IContent menu, NavigationBuilder builder) {
            builder.Add(T("The Cup"), "3", GetActiveCup);
        }

        private void GetActiveCup(NavigationBuilder builder) {
            builder.Add(T("Results"), "3.1", item =>
                item.Action("Results", "Home", new {area = "GoodM8s.TheCup"}));
        }
    }
}