using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalajiPortfolio.Models.Entity
{
    [Table("mstskill")]
    public class mstSkill
    {
        [Key]
        public int mstSkillId { get; set; }

        #region Skill Details
        public string? skillName { get; set; }
        public string? proficiencyLevel { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
}
