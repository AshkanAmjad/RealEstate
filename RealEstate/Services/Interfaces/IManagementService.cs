using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;

namespace RealEstate.Services.Interface
{
    public interface IManagementService
    {
        #region Users
        IQueryable<UserModel> GetUsers();
        IQueryable<UserModel> FilterUsers(string? searchContext, int? selectedFilter);
        Task<UserModel> GetUserWithIdAsync(string id);
        Task<bool> DeleteUserAsync(UserModel model);
        Task<bool> UpdateUserAsync(UserModel model);
        #endregion
        Task<bool> AddComment(CommentModel model);
        Task<List<CommentModel>> GetComments(int estateId);
        #region Images
        void Upload(EstateViewModel model);
        void deleteImg(EstateViewModel model);
        void deleteImg(EstateModel model);

        #endregion

        #region Comments

        #endregion

        #region DB
        Task<bool> SaveChangesAsync();

        #region Estates
        Task<bool> AddEstatesToDBAsync(EstateViewModel model);
        bool UpdateChanges(EstateViewModel model);
        bool DeleteEstate(EstateModel model);
        Task<bool> CreateEstatesAsync(EstateViewModel model);
        IQueryable<EstateModel> GetEstates();
        IQueryable<EstateModel> FilterEstates(string? searchContext, int? selectedFilter);
        EstateModel GetEstateAndCategoryWithId(int id);
        EstateModel GetEstateWithId(int id);
        #endregion

        #region Categories
        Task<bool> AddCategoriesToDBAsync(CategoryModel model);
        bool UpdateChanges(CategoryModel model);
        bool DeleteCategory(CategoryModel model);
        IQueryable<CategoryModel> GetCategories();
        CategoryModel GetCategoryWithId(int? id);
        #endregion

        #region Favories
        IQueryable<FavoriteModel> GetFavoritesByUserId(UserModel user);
        #endregion
        #endregion
    }
}
