using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;

namespace GoodM8s.TheCup.Models {
    public class TeamPart : ContentPart<TeamPartRecord> {
        private readonly LazyField<CupPart> _cupPart = new LazyField<CupPart>();

        public LazyField<CupPart> CupField {
            get { return _cupPart; }
        }

        [Required]
        public int? CupId {
            get { return Record.CupId; }
            set { Record.CupId = value; }
        }

        [Required]
        public string Name {
            get { return Record.Name; }
            set { Record.Name = value; }
        }

        public CupPart Cup {
            get { return _cupPart.Value; }
            set { _cupPart.Value = value; }
        }

        public IList<TeamAttendeeRecord> TeamMates {
            get { return Record.TeamMates; }
        }
    }
}