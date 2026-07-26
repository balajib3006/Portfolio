using System.ComponentModel.DataAnnotations;

namespace BalajiPortfolio.Models.Entity
{
    public class mstSkill
    {
        [Key]
        public int mstSkillId { get; set; }

        #region Skill Details
        public int mstSkillCategoryId { get; set; }
        public string? skillName { get; set; }
        public string? proficiencyLevel { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
}
