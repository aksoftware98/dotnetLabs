using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNetLabs.Server.Models.Models
{
    public abstract class UserRecord : Record
    {

        public UserRecord()
        {
            CreatationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
        }


        public DateTime CreatationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }
        // Foreign keys 
        [ForeignKey(nameof(CreatedByUser))]
        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser ModifiedByUser { get; set; }

        [ForeignKey(nameof(ModifiedByUser))]
        public string ModifiedByUserId { get; set; }
    }

}
