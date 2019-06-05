using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreApi_CodeHubGreece.Models
{
    [Helpers.GeneratedController]
    public class Installation : CommonModel
    {

        public Int64 CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public Int64 VersionId { get; set; }
        [ForeignKey("VersionId")]
        public virtual Version Version { get; set; }
        public DateTime RefDate { get; set; }

    }
}
