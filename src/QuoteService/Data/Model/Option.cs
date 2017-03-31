using QuoteService.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static QuoteService.Constants;

namespace QuoteService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Option: ILoggable
    {        
        public int Id { get; set; }

        [ForeignKey("Question")]
        public int? QuestionId { get; set; }

        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Response> Responses { get; set; } = new HashSet<Response>();

        public int OrderIndex { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Question Question { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
