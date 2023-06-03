using HologFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HologFrontEnd.Resources
{
    internal class BookToItemMapper
    {
        public static Item transform(Book book)
        {
            Item item = new Item();
            item.id = book.id;
            item.name = book.volumeInfo.title;
            if (!string.IsNullOrEmpty(book.volumeInfo.imageLinks.smallthumbnail)) {
                item.picture = book.volumeInfo.imageLinks.smallthumbnail;
            }
            else
            {
                item.picture = "no_picture.png";
            }
            if (!string.IsNullOrEmpty(book.volumeInfo.date))
            {
                item.date = book.volumeInfo.date;
            }
            if (!string.IsNullOrEmpty(book.volumeInfo.score))
            {
                item.score = book.volumeInfo.score;
            }
            item.status = "pending";
            item.custom = false;
            return item;
        }
    }
}
