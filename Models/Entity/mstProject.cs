using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BalajiPortfolio.Models.Entity
{
    [Table("mstproject")]
    public class mstProject
    {
        [Key]
        public int mstProjectId { get; set; }

        #region Project Details
        public string? projectTitle { get; set; }
        public string? clientName { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
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
