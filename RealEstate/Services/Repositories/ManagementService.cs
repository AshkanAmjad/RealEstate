using RealEstate.Convertors;
using RealEstate.Data;
using RealEstate.Models;
using RealEstate.Models.ViewModels.EstatesViewModels;
using RealEstate.Security;
using RealEstate.Services.Interface;
using System.Drawing;

namespace RealEstate.Services.Implementation
{
    public class ManagementService : IManagementService
    {
        private readonly ApplicationDbContext _context;

        public ManagementService(ApplicationDbContext context)
        {
            _context = context;
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
            if(model.Estate.Image == null)
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
            using (var filestream =new FileStream(savePath, FileMode.Create)) 
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
                if (System.IO.File.Exists(deletePathThumb)) { 
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

    }
}
