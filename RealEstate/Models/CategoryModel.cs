using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name = "Title")]
        [MaxLength(50, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [MaxLength(1000, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string? Description { get; set; }

        #region Relation
        public ICollection<EstateModel>? Estates { get; set; }
        #endregion

    }
}
