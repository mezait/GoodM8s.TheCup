using System.Collections.Generic;
using GoodM8s.TheCup.Models;

namespace GoodM8s.TheCup.ViewModels {
    public class EventScoresViewModel {
        public EventPart Event { get; set; }
        public IList<TeamScoreRecord> TeamScores { get; set; }
    }

    public class TeamScoreTotalsViewModel {
        public int TotalPenalties { get; set; }
        public int TotalScore { get; set; }
    }
}