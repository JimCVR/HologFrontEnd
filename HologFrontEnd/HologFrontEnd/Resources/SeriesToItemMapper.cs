using HologFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Resources
{
    internal class SeriesToItemMapper
    {

        public static Item transform(Series series)
        {
            Item item = new Item();
            item.id = series.id;
            item.name = series.title;
            if (!string.IsNullOrEmpty(series.poster))
            {
                item.picture = Constants.baseImageUri + series.poster;
            }
            else
            {
                item.picture = "no_picture.png";
            }

            item.description = series.overview;
            item.custom = false;
            item.date = series.releaseDate;
            item.score = series.vote;
            item.status = "pending";
            return item;
        }
    }
}
