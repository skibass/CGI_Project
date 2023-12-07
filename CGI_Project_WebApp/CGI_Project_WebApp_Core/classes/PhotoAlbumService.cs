﻿using CGI_Project_WebApp_Core.Interfaces;
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
        public void TryAddPhoto(Photos photo, string file, IFormFile Upload)
        {
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                Upload.CopyToAsync(fileStream);
            }
            photo.FileName = Upload.FileName;
            AddPhoto(photo);
        }
        private void AddPhoto(Photos photo)
        {
            _photoAlbumRepository.AddPhoto(photo);
        }
        public List<Photos> TryGetPhotos()
        {
            return _photoAlbumRepository.TryGetPhotos();
        }
    }
}
