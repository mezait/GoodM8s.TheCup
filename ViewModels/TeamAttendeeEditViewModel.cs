using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoodM8s.TheCup.Models;

namespace GoodM8s.TheCup.ViewModels {
    public class TeamAttendeeEditViewModel {
        [Required]
        public int Id { get; set; }

        [Required]
        public int? AttendeeId { get; set; }

        public IEnumerable<AttendeePart> Attendees { get; set; }
    }
}