using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoodM8s.TheCup.Models;

namespace GoodM8s.TheCup.ViewModels {
    public class TeamScoreEditViewModel {
        [Required]
        public int Id { get; set; }

        [Required]
        public int? TeamId { get; set; }

        [Required]
        public int? Score { get; set; }

        public int? Penalties { get; set; }

        public IEnumerable<TeamPart> Teams { get; set; }
    }
}