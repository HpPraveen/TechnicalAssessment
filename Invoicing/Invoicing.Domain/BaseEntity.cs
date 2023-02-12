using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Invoicing.Domain
{
    public class BaseEntity
    {
        [DefaultValue(false)] public bool? IsDeleted { get; set; } = false;

        [StringLength(128)]
        public string? SysCreatedBy { get; set; }

        public DateTime? SysCreatedOn { get; set; }

        [StringLength(128)]
        public string? SysDeletedBy { get; set; }

        public DateTime? SysDeletedOn { get; set; }

        [StringLength(128)]
        public string? SysUpdatedBy { get; set; }

        public DateTime? SysUpdatedOn { get; set; }
    }
}
