using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;

namespace GoodM8s.TheCup.Models {
    public class ScorePart : ContentPart<ScorePartRecord> {
        private readonly LazyField<CupPart> _cupPart = new LazyField<CupPart>();
        private readonly LazyField<EventPart> _eventPart = new LazyField<EventPart>();

        public LazyField<CupPart> CupField {
            get { return _cupPart; }
        }

        public LazyField<EventPart> EventField {
            get { return _eventPart; }
        }

        [Required]
        public int? CupId {
            get { return Record.CupId; }
            set { Record.CupId = value; }
        }

        [Required]
        public int? EventId {
            get { return Record.EventId; }
            set { Record.EventId = value; }
        }

        public string Notes {
            get { return Record.Notes; }
            set { Record.Notes = value; }
        }

        public CupPart Cup {
            get { return _cupPart.Value; }
            set { _cupPart.Value = value; }
        }

        public EventPart Event {
            get { return _eventPart.Value; }
            set { _eventPart.Value = value; }
        }

        public IList<TeamScoreRecord> TeamScores {
            get { return Record.TeamScores; }
        }
    }
}