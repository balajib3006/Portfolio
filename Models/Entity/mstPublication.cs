using System.ComponentModel.DataAnnotations;

namespace BalajiPortfolio.Models.Entity
{
    public class mstPublication
    {
        [Key]
        public int mstPublicationId { get; set; }

        #region Publication Details
        public string? title { get; set; }
        public string? publisher { get; set; }
        public string? publicationDate { get; set; }
        public string? publicationLink { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
}
