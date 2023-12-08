using RealEstate.Data;
using RealEstate.Models.ViewModels.EstatesViewModels;
using RealEstate.Services.Interface;

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

        public async Task<bool> CreateEstatesAsync(EstateViewModel model)
        {
            await _context.Estate.AddAsync(model.Estate);
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        { 
            await _context.SaveChangesAsync(); 
            return true;
        }

        public bool UpdateChangesAsync(EstateViewModel model)
        {
            _context.Update(model.Estate);
            return true;
        }


        public void Upload(EstateViewModel model)
        {
            string saveDir = "wwwroot/images/Estates";
            if (!Directory.Exists(saveDir))
                Directory.CreateDirectory(saveDir);
            model.Estate.Image = Guid.NewGuid().ToString() + Path.GetExtension(model.ImgUp.FileName);
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, model.Estate.Image);
            using var filestream = new FileStream(savePath, FileMode.Create);
            model.ImgUp.CopyTo(filestream);
        }

        public void deleteImg(EstateViewModel model)
        {
            string saveDir = "wwwroot/images/Estates";
            string deletePath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, model.Estate.Image);
            if (System.IO.File.Exists(deletePath))
                System.IO.File.Delete(deletePath);
        }



    }
}
