namespace BalajiPortfolio.Models.DTO
{
    public class CertificationDTO
    {
        public int mstCertificationId { get; set; }

        #region Certification Details
        public string? type { get; set; }
        public string? name { get; set; }
        public string? issuer { get; set; }
        public string? issueDate { get; set; }
        public string? credentialLink { get; set; }
        public string? embedCode { get; set; }
        #endregion

        #region Audit Fields
        public bool isActive { get; set; } = true;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        #endregion
    }
}
