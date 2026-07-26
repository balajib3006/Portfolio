namespace BalajiPortfolio.Models.DTO
{
    #region Skill Category DTO
    public class SkillCategoryDTO
    {
        public int mstSkillCategoryId { get; set; }

        #region Category Details
        public string? categoryName { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
    #endregion

    #region Skill DTO
    public class SkillDTO
    {
        public int mstSkillId { get; set; }

        #region Skill Details
        public int mstSkillCategoryId { get; set; }
        public string? categoryName { get; set; }
        public string? skillName { get; set; }
        public string? proficiencyLevel { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
    #endregion

    #region Skills Page DTO (Combined Page View Model)
    public class SkillsPageDTO
    {
        public List<SkillCategoryDTO> categories { get; set; } = new List<SkillCategoryDTO>();
        public List<SkillDTO> skills { get; set; } = new List<SkillDTO>();
    }
    #endregion
}
