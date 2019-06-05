using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreApi_CodeHubGreece.Models
{
    [Helpers.GeneratedController]
    public class Version : CommonModel
    {

        public DateTime ReleaseDate { get; set; }
        public Int64 ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public string VersionNumber { get; set; }
        public string Description { get; set; }

    }
}
