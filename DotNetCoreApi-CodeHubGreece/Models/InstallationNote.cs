using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreApi_CodeHubGreece.Models
{
    [Helpers.GeneratedController]
    public class InstallationNote : CommonModel
    {

        public Int64 InstallationId { get; set; }
        [ForeignKey("InstallationId")]
        public Installation Installation { get; set; }
        public Int64 UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
