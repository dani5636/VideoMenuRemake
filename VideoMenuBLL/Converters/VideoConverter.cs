﻿using VideoMenuBLL.BusinessObjects;
using VideoMenuDAL.Entities;

namespace VideoMenuBLL.Converters
{
    class VideoConverter
    {
        public Video Convert(VideoBO vid)
        {
            return new Video
            {
                Id = vid.Id,
                Name = vid.Name,
                Genre = vid.Genre
            };
        }
        public VideoBO Convert(Video vid)
        {
            return new VideoBO
            {
                Id = vid.Id,
                Name = vid.Name,
                Genre = vid.Genre
            };
        }
    }
}
