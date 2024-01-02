using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.Models
{
    public class FavoriteModel
    {
        public int Id { get; set; } 
        public int EstateId { get; set; }  
        public string UserId {  get; set; }
        public DateTime LikedDate { get; set; } = DateTime.Now;

        #region Relation
        [ForeignKey(nameof(EstateId))]
        public EstateModel Estate { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }
        #endregion
    }
}
