using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalajiPortfolio.Models.Entity
{
    [Table("mstskillcategory")]
    public class mstSkillCategory
    {
        [Key]
        public int mstSkillCategoryId { get; set; }

        #region Category Details
        public string? categoryName { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
}
