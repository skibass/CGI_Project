using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.Interfaces
{
    public interface IPhotoAlbumRepository
    {
        public void AddPhoto(Photos photo);
        public void AddUserPicture(Employee employee);
        public List<Photos> TryGetPhotos();
    }
}
