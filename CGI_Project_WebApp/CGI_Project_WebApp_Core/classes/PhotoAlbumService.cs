using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.classes
{
    public class PhotoAlbumService
    {
        IPhotoAlbumRepository _photoAlbumRepository { get; set; }
        public PhotoAlbumService(IPhotoAlbumRepository photoAlbumRepository)
        {
            this._photoAlbumRepository = photoAlbumRepository;
        }
        public async Task TryAddPhoto(Photos photo,string path, IFormFile Upload)
        {
            string Guidstring = Guid.NewGuid().ToString();
            string UploadName = Path.Combine(path, Guidstring + Upload.FileName);


            // If tthe file doesnt exist, add to both the database and folder
            using (var fileStream = new FileStream(UploadName, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                photo.FileName = Guidstring + Upload.FileName;
                await AddPhoto(photo);

        }
        private async Task AddPhoto(Photos photo)
        {
            _photoAlbumRepository.AddPhoto(photo);
        }
        public List<Photos> TryGetPhotos()
        {
            return _photoAlbumRepository.TryGetPhotos();
        }
    }
}
