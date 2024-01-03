﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Convertors;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;
using RealEstate.Services.Interface;
using RealEstate.Utilities;


namespace RealEstate.Services.Implementation
{
    public class ManagementService : IManagementService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public ManagementService(ApplicationDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> AddEstatesToDBAsync(EstateViewModel? model)
        {
            await _context.AddAsync(model.Estate);
            return true;
        }

        public async Task<bool> AddCategoriesToDBAsync(CategoryModel? model)
        {
            await _context.AddAsync(model);
            return true;
        }


        public async Task<bool> CreateEstatesAsync(EstateViewModel model)
        {
            if (model.Estate.Image == null)
            {
                model.Estate.Image = "default-image.png";
            }
            await _context.Estate.AddAsync(model.Estate);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public bool UpdateChanges(EstateViewModel model)
        {
            _context.Update(model.Estate);
            return true;
        }


        public void Upload(EstateViewModel model)
        {
            string saveDir = "wwwroot/images/Estates";
            string saveDirThumb = "wwwroot/images/Thumb";
            bool dir1 = Directory.Exists(saveDir);
            bool dir2 = Directory.Exists(saveDirThumb);
            if (!Directory.Exists(saveDir))
                Directory.CreateDirectory(saveDir);

            if (!Directory.Exists(saveDirThumb))
                Directory.CreateDirectory(saveDirThumb);

            model.Estate.Image = Guid.NewGuid().ToString() + Path.GetExtension(model.ImgUp.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, model.Estate.Image);
            using (var filestream = new FileStream(savePath, FileMode.Create))
                model.ImgUp.CopyTo(filestream);


            #region ThumbImg
            if (model.Estate.Image != "default-image.png")
            {
                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), saveDirThumb, model.Estate.Image);
                imgResizer.ResizeImage(savePath, thumbPath, 100, 100);
            }
            #endregion
        }

        public void deleteImg(EstateModel model)
        {
            string saveDir = "wwwroot/images/Estates";
            string saveDirThumb = "wwwroot/images/Thumb";
            string deletePath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, model.Image);
            if (System.IO.File.Exists(deletePath))
                System.IO.File.Delete(deletePath);
            if (Directory.Exists(saveDirThumb))
            {
                string deletePathThumb = Path.Combine(Directory.GetCurrentDirectory(), saveDirThumb, model.Image);
                if (System.IO.File.Exists(deletePathThumb))
                {
                    if (model.Image != "default-image.png")
                        System.IO.File.Delete(deletePathThumb);
                }
            }
        }
        public void deleteImg(EstateViewModel model)
        {
            string saveDir = "wwwroot/images/Estates";
            string saveDirThumb = "wwwroot/images/Thumb";
            string deletePath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, model.Estate.Image);
            if (System.IO.File.Exists(deletePath))
                System.IO.File.Delete(deletePath);
            if (Directory.Exists(saveDirThumb))
            {
                string deletePathThumb = Path.Combine(Directory.GetCurrentDirectory(), saveDirThumb, model.Estate.Image);
                if (System.IO.File.Exists(deletePathThumb))
                {
                    if (model.Estate.Image != "default-image.png")
                        System.IO.File.Delete(deletePathThumb);
                }
            }
        }

        public bool DeleteEstate(EstateModel model)
        {
            _context.Remove(model);
            return true;
        }




        public bool DeleteCategory(CategoryModel model)
        {
            _context.Remove(model);
            return true;
        }

        public bool UpdateChanges(CategoryModel model)
        {
            _context.Update(model);
            return true;
        }

        public IQueryable<EstateModel> GetEstates()
            => _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).AsQueryable();

        public IQueryable<EstateModel> FilterEstates(string? searchContext, int? selectedFilter)
        {
            if (selectedFilter == 1)
                return _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).Where(e => e.Category.Title == searchContext);
            else if (selectedFilter == 2)
                return _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).Where(e => e.Address == searchContext);
            else if (selectedFilter == 3)
                return _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).Where(e => e.Metrage.ToString() == searchContext);
            else if (selectedFilter == 4)
                return _context.Estate.OrderByDescending(e => e.DateCreated).Include(c => c.Category).Where(e => e.Price.ToString() == searchContext);
            return GetEstates();
        }

        public IQueryable<CategoryModel> GetCategories()
            => _context.Category.AsQueryable();

        public CategoryModel GetCategoryWithId(int? id)
            => _context.Category.FirstOrDefault(m => m.Id == id);

        public EstateModel GetEstateAndCategoryWithId(int id)
             => _context.Estate.Include(c => c.Category).FirstOrDefault(e => e.Id == id);

        public EstateModel GetEstateWithId(int id)
            => _context.Estate.Find(id);

        public IQueryable<FavoriteModel> GetFavoritesByUserId(UserModel user)
            => _context.Favorites.Include(e => e.Estate).Where(f => f.UserId == user.Id).OrderByDescending(f => f.LikedDate).AsQueryable();

        public IQueryable<UserModel> GetUsers()
            => _context.Users.OrderByDescending(u => u.SignUpDate).AsQueryable();

        public IQueryable<UserModel> FilterUsers(string? searchContext, int? selectedFilter)
        {
            if (selectedFilter == 1)
                return _context.Users.OrderByDescending(e => e.FullName).Where(e => e.FullName == searchContext);
            else if (selectedFilter == 2)
                return _context.Users.OrderByDescending(e => e.PhoneNumber).Where(e => e.PhoneNumber == searchContext);
            else if (selectedFilter == 3)
                return _context.Users.OrderByDescending(e => e.Email).Where(e => e.Email == searchContext);
            else if (selectedFilter == 4)
                return _context.Users.OrderByDescending(e => e.SignUpDate).Where(e => e.SignUpDate.ToString() == searchContext);
            return GetUsers();
        }
        public async Task<UserModel> GetUserWithIdAsync(string id)
            => await _userManager.FindByIdAsync(id);

        public async Task<bool> DeleteUserAsync(UserModel model)
        {
            var deleteResult = await _userManager.DeleteAsync(model);
            if (deleteResult.Succeeded)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateUserAsync(UserModel model)
        {
            var updateResult = await _userManager.UpdateAsync(model);
            if (updateResult.Succeeded)
                return true;
            else
                return false;
        }




    }
}