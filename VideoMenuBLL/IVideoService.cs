using System;
using System.Collections.Generic;
using System.Text;
using VideoMenuEntity;

namespace VideoMenuBLL
{
   public interface IVideoService
    {
        //C
        void CreateVideo(Video v);
        void CreateMultipleVideos(List<Video> videos);
        //R
        List<Video> GetAllVideos();
        Video GetVideoById(int id);
        List<Video> SearchVideos(string str);
        //U
        void UpdateVideo(Video v);
        //D
        bool DeleteVideo(int id);
    }
}
