using System.Collections.Generic;
using Orchard.ContentManagement.Records;

namespace GoodM8s.TheCup.Models {
    public class TeamPartRecord : ContentPartRecord {
        public TeamPartRecord() {
// ReSharper disable once DoNotCallOverridableMethodsInConstructor
            TeamMates = new List<TeamAttendeeRecord>();
        }

        public virtual int? CupId { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<TeamAttendeeRecord> TeamMates { get; set; }
    }
}