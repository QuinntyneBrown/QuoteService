using System;
using System.Collections.Generic;
using QuoteService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class QuoteLineItem: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Quote")]
        public int? QuoteId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public string Description { get; set; }
        
        public float Amount { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Quote Quote { get; set; }
    }
}
