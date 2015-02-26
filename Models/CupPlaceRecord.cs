namespace GoodM8s.TheCup.Models {
    public class CupPlaceRecord {
        public virtual int Id { get; set; }
        public virtual CupPartRecord CupPartRecord { get; set; }
        public virtual int? Place { get; set; }
        public virtual int? Points { get; set; }
    }
}