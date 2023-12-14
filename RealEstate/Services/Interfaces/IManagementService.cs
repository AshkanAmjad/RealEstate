using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;

namespace RealEstate.Services.Interface
{
    public interface IManagementService
    {
        #region Estates
        Task<bool> CreateEstatesAsync(EstateViewModel model);
        #endregion

        #region Images
        void Upload(EstateViewModel model);
        void deleteImg(EstateViewModel model);
        void deleteImg(EstateModel model);

        #endregion

        #region DB
        Task<bool> SaveChangesAsync();

        #region Estates
        Task<bool> AddEstatesToDBAsync(EstateViewModel model);
        bool UpdateChanges(EstateViewModel model);
        bool DeleteEstate(EstateModel model);
        #endregion

        #region Categories
        Task<bool> AddCategoriesToDBAsync(CategoryModel model);
        bool UpdateChanges(CategoryModel model);
        bool DeleteCategory(CategoryModel model);
        #endregion

        #endregion
    }
}
