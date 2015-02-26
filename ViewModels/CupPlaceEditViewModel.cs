using System.ComponentModel.DataAnnotations;

namespace GoodM8s.TheCup.ViewModels {
    public class CupPlaceEditViewModel {
        [Required]
        public int Id { get; set; }

        [Required]
        public int? Place { get; set; }

        [Required]
        public int? Points { get; set; }
    }
}