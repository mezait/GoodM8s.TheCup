using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace GoodM8s.TheCup.Models {
    public class AttendeePart : ContentPart<AttendeePartRecord> {
        [Required]
        public string FirstName {
            get { return Record.FirstName; }
            set { Record.FirstName = value; }
        }

        [Required]
        public string LastName {
            get { return Record.LastName; }
            set { Record.LastName = value; }
        }
    }
}