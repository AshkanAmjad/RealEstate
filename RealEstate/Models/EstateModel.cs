using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class EstateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name = "Title")]
        [MaxLength(200, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Title { get; set; }

        [Display(Name ="Description")]
        [MaxLength(1000, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name = "Metrage")]
        public int Metrage { get; set; }

        public string? Image {  get; set; }

        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name ="Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Completion of this entry is required.")]
        [Display(Name ="Address")]
        [MaxLength(1000, ErrorMessage = "{0} cannot be longer than {1} characters.")]
        public string Address { get; set; }

        public int CategoryId {  get; set; }
        #region Relation
        [ForeignKey(nameof(CategoryId))]
        public CategoryModel? Category { get; set; }
        #endregion
    }
}
