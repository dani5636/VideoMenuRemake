using System;
using System.Collections.Generic;
using System.Text;
using VideoMenuEntity;

namespace VideoMenuBLL
{
    public interface IGenreService
    {
        //C
        bool CreateGenre(Genre g);
        //R
        List<Genre> GetAllGenre();
        Genre GetGenreById(int id);
        List<Genre> SearchGenres(string str);
        //U
        void UpdateGenre(Genre g);
        //D
        bool DeleteGenre(int id);
    }
}
