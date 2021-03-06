﻿using dotNetLabs.Server.Models.Models;
using dotNetLabs.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotNetLabs.Server.Models.Mappers
{
    public static class PlaylistsMapper
    {

        public static PlaylistDetail ToPlaylistDetail(this Playlist playlist, IEnumerable<VideoDetail> playlistVideos = null,bool includeVideos = false)
        {
            return new PlaylistDetail
            {
                Id = playlist.Id,
                Description = playlist.Description,
                Name = playlist.Name,
                Videos = includeVideos ? playlistVideos : null
            };
        }

    }
}
