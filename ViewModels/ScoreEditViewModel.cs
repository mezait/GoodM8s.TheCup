using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoodM8s.TheCup.Models;

namespace GoodM8s.TheCup.ViewModels {
    public class ScoreEditViewModel {
        [Required]
        public int? CupId { get; set; }

        [Required]
        public int? EventId { get; set; }

        public string Notes { get; set; }

        public IEnumerable<CupPart> Cups { get; set; }
        public IEnumerable<EventPart> Events { get; set; }
        public IEnumerable<TeamScoreEditViewModel> TeamScores { get; set; }
    }
}