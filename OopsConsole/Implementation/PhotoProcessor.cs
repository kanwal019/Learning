using System;
using Common.Classes;

namespace Common.Delegates;

public class PhotoProcessor
{
    public void Process(string path, Action<Photo> filterHandler)
    {
        var photo = Photo.Load(path);
        filterHandler(photo); // only the applied filter handlers will be used
        photo.Save();
    }
}
