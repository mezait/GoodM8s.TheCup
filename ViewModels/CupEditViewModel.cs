using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodM8s.TheCup.ViewModels {
    public class CupEditViewModel {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Required]
        public string Title { get; set; }

        public IEnumerable<CupPlaceEditViewModel> Placement { get; set; }
        public IEnumerable<EventOrderEditViewModel> EventOrders { get; set; }
    }
}