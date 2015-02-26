using Orchard.ContentManagement.Records;

namespace GoodM8s.TheCup.Models {
    public class EventPartRecord : ContentPartRecord {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}