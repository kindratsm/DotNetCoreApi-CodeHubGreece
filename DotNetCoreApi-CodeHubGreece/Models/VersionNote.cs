using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreApi_CodeHubGreece.Models
{
    [Helpers.GeneratedController]
    public class VersionNote : CommonModel
    {

        public Int64 VersionId { get; set; }
        [ForeignKey("VersionId")]
        public virtual Version Version { get; set; }
        public Int64 UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public Int64 VersionNoteTypeId { get; set; }
        [ForeignKey("VersionNoteTypeId")]
        public VersionNoteType VersionNoteType { get; set; }
        public string Description { get; set; }

    }
}
