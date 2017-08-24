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
        DALFacade facade;
        public VideoService(DALFacade facade)
        {
            this.facade = facade;
        }
        public void CreateVideo(Video v)
        {
            using (var uow = facade.UnitOfWork) {
                uow.VideoRepository.CreateVideo(v);
                uow.Complete();
            }

        }

        public bool DeleteVideo(int id)
        {
            using (var uow = facade.UnitOfWork)
            {
               var result =  uow.VideoRepository.DeleteVideo(id);
                uow.Complete();
                return result;
            }
        }

        public List<Video> GetAllVideos()
        {
            using (var uow = facade.UnitOfWork)
            {
                return uow.VideoRepository.GetAllVideos();
                
            }
        }

        public Video GetVideoById(int id)
        {
            using (var uow = facade.UnitOfWork)
            {
                return uow.VideoRepository.GetVideoById(id);

            }
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
            using (var uow = facade.UnitOfWork)
            {
                var custFromDB = uow.VideoRepository.GetVideoById(v.Id);
                if (custFromDB == null)
                {
                    throw new InvalidOperationException("Customer Not Found");
                }
                custFromDB.Name = v.Name;
                custFromDB.Genre = v.Genre;
            }
          
        }
    }
}
