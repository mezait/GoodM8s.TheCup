using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace GoodM8s.TheCup.Models {
    public class EventPart : ContentPart<EventPartRecord> {
        [Required]
        public string Name {
            get { return Record.Name; }
            set { Record.Name = value; }
        }

        [Required]
        public string Description {
            get { return Record.Description; }
            set { Record.Description = value; }
        }
    }
}