using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoMenuDAL;
using VideoMenuEntity;

namespace VideoMenuBLL.Services
{
    class VideoService : IVideoService
    {
        IVideoRepository repo;
        public VideoService(IVideoRepository repo)
        {
            this.repo = repo;
        }
        public void CreateVideo(Video v)
        {
            repo.CreateVideo(v);

        }

        public bool DeleteVideo(int id)
        {
            return repo.DeleteVideo(id);
            
        }

        public List<Video> GetAllVideos()
        {
            return repo.GetAllVideos();
        }

        public Video GetVideoById(int id)
        {
            return repo.GetVideoById(id);  
        }

        public List<Video> SearchVideos(string str)
        {
            

            var searchTerm = str.ToLower();
            bool intExist = false;
            int searchId = 0;
            if (int.TryParse(searchTerm, out searchId))
            {
                intExist = true;
            }
            var searchedVideos = GetAllVideos().Where(x =>
                        x.Name.ToLower().Contains(searchTerm) 
                        || x.Genre.ToLower().Contains(searchTerm)
                        || (intExist && x.Id == searchId)).ToList();
           
            return searchedVideos;
        }

        public void UpdateVideo(Video v)
        {
            var custFromDB = GetVideoById(v.Id);
            if (custFromDB == null) {
                throw new InvalidOperationException("Customer Not Found");
            }
            custFromDB.Name = v.Name;
            custFromDB.Genre = v.Genre;

        }
    }
}
