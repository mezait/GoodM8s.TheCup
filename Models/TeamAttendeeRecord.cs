namespace GoodM8s.TheCup.Models {
    public class TeamAttendeeRecord {
        public virtual int Id { get; set; }
        public virtual TeamPartRecord TeamPartRecord { get; set; }
        public virtual AttendeePartRecord AttendeePartRecord { get; set; }
    }
}