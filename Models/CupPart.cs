using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace GoodM8s.TheCup.Models {
    public class CupPart : ContentPart<CupPartRecord> {
        public DateTime? Date {
            get { return Record.Date; }
            set { Record.Date = value; }
        }

        [Required]
        public string Title {
            get { return Record.Title; }
            set { Record.Title = value; }
        }

        public IList<EventOrderRecord> EventOrders
        {
            get { return Record.EventOrder; }
        }

        public IList<CupPlaceRecord> Placement {
            get { return Record.Placement; }
        }
    }
}