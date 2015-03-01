using System.Collections.Generic;
using GoodM8s.TheCup.Models;

namespace GoodM8s.TheCup.ViewModels {
    public class CupResultsViewModel {
        public CupPart Cup { get; set; }
        public List<EventScoresViewModel> EventScores { get; set; }
        public IDictionary<TeamPartRecord, TeamScoreTotalsViewModel> TeamTotals { get; set; }
    }
}