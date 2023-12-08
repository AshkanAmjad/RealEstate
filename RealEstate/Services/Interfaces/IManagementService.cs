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
        #endregion

        #region DB
        Task<bool> SaveChangesAsync();
        Task<bool> AddEstatesToDBAsync(EstateViewModel model);
        bool UpdateChangesAsync(EstateViewModel model);

        #endregion
    }
}
