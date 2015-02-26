using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoodM8s.TheCup.Models;

namespace GoodM8s.TheCup.ViewModels {
    public class TeamEditViewModel {
        [Required]
        public int? CupId { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<CupPart> Cups { get; set; }
        public IEnumerable<TeamAttendeeEditViewModel> TeamAttendees { get; set; }
    }
}