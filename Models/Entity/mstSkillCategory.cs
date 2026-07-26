using System.ComponentModel.DataAnnotations;

namespace BalajiPortfolio.Models.Entity
{
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
