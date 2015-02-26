namespace GoodM8s.TheCup.Models {
    public class EventOrderRecord {
        public virtual int Id { get; set; }
        public virtual CupPartRecord CupPartRecord { get; set; }
        public virtual EventPartRecord EventPartRecord { get; set; }
        public virtual int? SortOrder { get; set; }
    }
}