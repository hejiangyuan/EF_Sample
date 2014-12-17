using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WindowsFormsApplication9.Model
{
    [Table("WAREHOUSE")]
    public partial class WAREHOUSE
    {
        [Key]
        public Guid WHID { get; set; }

        public Guid WHDOMAINID { get; set; }

        [Required]
        [StringLength(50)]
        public string WHIDENTITYCODE { get; set; }

        [StringLength(50)]
        public string WHCODE { get; set; }

        [Required]
        [StringLength(50)]
        public string WHNAME { get; set; }

    }
}
