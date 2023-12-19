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
        public void AddPhoto(Photos photo)
        {
           dbi511119Context.Photos.Add(photo);
            dbi511119Context.SaveChanges();
        }
        public void AddUserPicture(Employee employee)
        {
            Employee employeeSel = dbi511119Context.Employees.Where(e => e.Id == employee.Id).FirstOrDefault();
            employeeSel.ProfileImage = employee.ProfileImage;
            dbi511119Context.SaveChanges();
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
