namespace GoodM8s.TheCup.Models {
    public class TeamScoreRecord {
        public virtual int Id { get; set; }
        public virtual ScorePartRecord ScorePartRecord { get; set; }
        public virtual TeamPartRecord TeamPartRecord { get; set; }
        public virtual int? Score { get; set; }
        public virtual int? Penalties { get; set; }
    }
}