using System.Collections.Generic;
using GoodM8s.Core.Services;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.ViewModels;
using Orchard.ContentManagement;

namespace GoodM8s.TheCup.Services {
    public interface ITeamService : IBaseService<TeamPart, TeamPartRecord> {
        IEnumerable<TeamPart> GetByCup(int cupId);
        void UpdateForContentItem(IContent item, TeamEditViewModel model);
    }
}