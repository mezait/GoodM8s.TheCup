using System.Collections.Generic;
using Orchard.ContentManagement.Records;

namespace GoodM8s.TheCup.Models {
    public class ScorePartRecord : ContentPartRecord {
        public ScorePartRecord() {
// ReSharper disable once DoNotCallOverridableMethodsInConstructor
            TeamScores = new List<TeamScoreRecord>();
        }

        public virtual int? CupId { get; set; }
        public virtual int? EventId { get; set; }
        public virtual string Notes { get; set; }

        public virtual IList<TeamScoreRecord> TeamScores { get; set; }
    }
}