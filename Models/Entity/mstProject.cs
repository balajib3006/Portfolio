using System.ComponentModel.DataAnnotations;

namespace BalajiPortfolio.Models.Entity
{
    public class mstProject
    {
        [Key]
        public int mstProjectId { get; set; }

        #region Project Details
        public string? projectTitle { get; set; }
        public string? description { get; set; }
        public string? productImageUrl { get; set; }
        public string? cadFilePath { get; set; }
        public string? tags { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
}
