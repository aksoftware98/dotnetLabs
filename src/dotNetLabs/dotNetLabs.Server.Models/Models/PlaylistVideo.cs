using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLabs.Server.Models.Models
{
    public class PlaylistVideo : Record
    {

        public virtual Video Video { get; set; }
        public string VideoId { get; set; }

        public virtual Playlist Playlist { get; set; }
        public string PlaylistId { get; set; }

    }
}
