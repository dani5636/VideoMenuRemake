using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoMenuDAL;
using VideoMenuEntity;

namespace VideoMenuBLL.Services
{
    class GenreService : IGenreService
    {
        DALFacade facade;
        public GenreService(DALFacade facade)
        {
            this.facade = facade;
        }

        public bool CreateGenre(Genre g)
        {
            bool success = false;
            using (var uow = facade.UnitOfWork)
            {
                if(uow.GenreRepository.GetAllGenres().FirstOrDefault(x => x.Name.ToLower().Equals(g.Name.ToLower())) == null)
                {
                    uow.GenreRepository.CreateGenre(g);
                    success = true;
                }
                uow.Complete();
                return success;
            }
        }

        public bool DeleteGenre(int id)
        {
            using (var uow = facade.UnitOfWork)
            {
                var result = uow.GenreRepository.DeleteGenre(id);
                uow.Complete();
                return result;
            }
        }

        public List<Genre> GetAllGenre()
        {
            using (var uow = facade.UnitOfWork)
            {
                return uow.GenreRepository.GetAllGenres();
            }
        }

        public Genre GetGenreById(int id)
        {
            using (var uow = facade.UnitOfWork) {
                return uow.GenreRepository.GetGenreById(id);
            }
        }

        public List<Genre> SearchGenres(string str)
        {
            var searchTerm = str.ToLower();
            bool intExist = false;
            int searchId = 0;
            if (int.TryParse(searchTerm, out searchId))
            {
                intExist = true;
            }
            var searchedVideos = GetAllGenre().Where(x =>
                        x.Name.ToLower().Contains(searchTerm)
                        || (intExist && x.Id == searchId)).ToList();


            return searchedVideos;
        }

        public void UpdateGenre(Genre g)
        {
            using (var uow = facade.UnitOfWork)
            {
                var vidFromDB = uow.GenreRepository.GetGenreById(g.Id);
                if (vidFromDB == null)
                {
                    throw new InvalidOperationException("Genre Not Found");
                }
                vidFromDB.Name = g.Name;
                uow.Complete();
            }
        }
    }
}
