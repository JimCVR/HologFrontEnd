using System;
using System.Collections.Generic;
using System.Text;
using Android.Graphics;
using HologFrontEnd.Models;
namespace HologFrontEnd.Resources
{
    class VideogameToItemMapper
    {
        public static Item transform(Videogame game)
        {
            Item item = new Item();
            item.id = game.id;
            item.name = game.name;
            if (!string.IsNullOrEmpty(game.image))
            {
                item.picture = game.image;
            }
            else
            {
                item.picture = "no_picture.png";
            }
            
            item.custom = false;
            item.date = game.date;
            item.score = game.score;
            item.status = "pending";
            return item;
        }
    }


}
