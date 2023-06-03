using HologFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Resources
{
    class MovieToItemMapper
    {
        public static Item transform(Movie movie)
        {
            Item item = new Item();
            item.id = movie.id;
            item.name = movie.title;
            if (!string.IsNullOrEmpty(movie.poster))
            {
                item.picture = Constants.baseImageUri + movie.poster;
            }
            else
            {
                item.picture = "no_picture.png";
            }
            
            item.description = movie.overview;
            item.custom = false;
            item.date = movie.releaseDate;
            item.score = movie.vote;
            item.status = "pending";
            return item;
        }
    }
}
