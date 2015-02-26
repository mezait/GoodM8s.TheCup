using System.Collections.Generic;
using GoodM8s.Core.Services;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.ViewModels;
using Orchard.ContentManagement;

namespace GoodM8s.TheCup.Services {
    public interface IScoreService : IBaseService<ScorePart, ScorePartRecord> {
        void UpdateForContentItem(IContent item, ScoreEditViewModel model);
        IEnumerable<ScorePart> GetByCup(int cupId);
    }
}