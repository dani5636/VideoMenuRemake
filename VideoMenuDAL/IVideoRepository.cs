﻿using System;
using System.Collections.Generic;
using System.Text;
using VideoMenuDAL.Entities;

namespace VideoMenuDAL
{
    public interface IVideoRepository
    {
        //C
        void CreateVideo(Video v);
        //R
        List<Video> GetAllVideos();
        Video GetVideoById(int id);
        //U
        //No update for the repository. Task of UoW
        //D
        bool DeleteVideo(int id);
    }
}
