using System;
using System.Collections.Generic;
using Orchard.ContentManagement.Records;

namespace GoodM8s.TheCup.Models {
    public class CupPartRecord : ContentPartRecord {
        public CupPartRecord() {
// ReSharper disable DoNotCallOverridableMethodsInConstructor
            EventOrder = new List<EventOrderRecord>();
            Placement = new List<CupPlaceRecord>();
// ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public virtual DateTime? Date { get; set; }
        public virtual string Title { get; set; }

        public virtual IList<EventOrderRecord> EventOrder { get; set; }
        public virtual IList<CupPlaceRecord> Placement { get; set; }
    }
}