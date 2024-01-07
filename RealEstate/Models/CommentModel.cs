using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        public int EstateId { get; set; }
        public string UserId { get; set; }
        [MaxLength(700)]
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdminRead { get; set; }
        #region Relations
        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }
        [ForeignKey(nameof(EstateId))]
        public EstateModel Estate { get; set; }
        #endregion


    }
}
