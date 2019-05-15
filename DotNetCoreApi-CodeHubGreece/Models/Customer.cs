using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreApi_CodeHubGreece.Models
{
    [Helpers.GeneratedController]
    public class Customer : CommonModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public UInt64 CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

    }
}
