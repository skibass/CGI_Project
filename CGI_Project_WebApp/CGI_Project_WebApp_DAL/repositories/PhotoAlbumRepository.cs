using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class PhotoAlbumRepository : IPhotoAlbumRepository
    {
        Dbi511119Context dbi511119Context = new();
        public async void AddPhoto(Photos photo)
        {
            try
            {
                await dbi511119Context.Photos.AddAsync(photo);
                await dbi511119Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public List<Photos> TryGetPhotos()
        {
            List<Photos> photos = dbi511119Context.Photos.ToList();

            if (photos == null)
            {
                photos = new List<Photos>();
            }
            return photos;
        }
    }
}
