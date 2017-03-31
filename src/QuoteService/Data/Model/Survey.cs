using System.Collections.Generic;
using QuoteService.Data.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static QuoteService.Constants;

namespace QuoteService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Survey: ILoggable
    {
        public int Id { get; set; }

        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }

        [Index("UniqueIdIndex", IsUnique = true)]
        [Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public ICollection<SurveyResult> SurveyResults { get; set; } = new HashSet<SurveyResult>();

        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
