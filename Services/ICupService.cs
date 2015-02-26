using GoodM8s.Core.Services;
using GoodM8s.TheCup.Models;
using GoodM8s.TheCup.ViewModels;
using Orchard.ContentManagement;

namespace GoodM8s.TheCup.Services {
    public interface ICupService : IBaseService<CupPart, CupPartRecord> {
        CupPart GetMostRecent();
        void UpdateForContentItem(IContent item, CupEditViewModel model);
    }
}