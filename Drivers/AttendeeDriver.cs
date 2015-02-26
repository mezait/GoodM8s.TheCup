using GoodM8s.TheCup.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace GoodM8s.TheCup.Drivers {
    public class AttendeeDriver : ContentPartDriver<AttendeePart> {
        protected override string Prefix {
            get { return "Attendee"; }
        }

        protected override DriverResult Editor(AttendeePart part, dynamic shapeHelper) {
            return ContentShape("Parts_Attendee_Edit", () => shapeHelper.EditorTemplate(TemplateName: "Parts/Attendee", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(AttendeePart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, Prefix, null, null);

            return Editor(part, shapeHelper);
        }
    }
}