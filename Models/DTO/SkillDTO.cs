namespace BalajiPortfolio.Models.DTO
{
    #region Skill DTO
    public class SkillDTO
    {
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
    #endregion
}
