using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace dotNetLabs.Server.Models.Models
{
    public abstract class Record
    {

        public Record()
        {
            Id = Guid.NewGuid().ToString(); 
        }

        [Key] // Primary Key
        public string Id { get; set; }


    }

}
