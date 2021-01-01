using System;
using System.Collections.Generic;
using System.Text;
using Common.Classes;

namespace Common.Delegates
{
    public class PhotoProcessor
    {
        public delegate void PhotoFilterHandler(Photo photo);

        public void Process(string path, PhotoFilterHandler filterHandler)
        {
            var photo = Photo.Load(path);
            filterHandler(photo); // only the applied filter handlers will be used
            photo.Save();
        }
    }
}
