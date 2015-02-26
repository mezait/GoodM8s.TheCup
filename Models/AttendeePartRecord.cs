using Orchard.ContentManagement.Records;

namespace GoodM8s.TheCup.Models {
    public class AttendeePartRecord : ContentPartRecord {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}