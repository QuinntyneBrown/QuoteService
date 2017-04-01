using System;
using System.Collections.Generic;
using QuoteService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuoteService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Quote: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public ICollection<QuoteLineItem> QuoteLineItems { get; set; } = new HashSet<QuoteLineItem>();

        [NotMapped]
        public float Total {
            get {
                float total = 0;
                foreach (var quoteLineItem in QuoteLineItems) { total += quoteLineItem.Amount; }
                return total;
            }
        }

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
