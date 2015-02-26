using GoodM8s.TheCup.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace GoodM8s.TheCup.Drivers {
    public class EventDriver : ContentPartDriver<EventPart> {
        protected override string Prefix {
            get { return "Event"; }
        }

        protected override DriverResult Editor(EventPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Event_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/Event", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(EventPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}