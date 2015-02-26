using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoodM8s.TheCup.Models;

namespace GoodM8s.TheCup.ViewModels {
    public class EventOrderEditViewModel {
        [Required]
        public int Id { get; set; }

        [Required]
        public int? EventId { get; set; }

        [Required]
        public int? SortOrder { get; set; }

        public IEnumerable<EventPart> Events { get; set; }
    }
}