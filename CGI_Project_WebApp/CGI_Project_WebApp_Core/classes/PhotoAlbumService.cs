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
		FileChecker checker = new FileChecker();
		
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

	//TODO: dit kan eventueel wel een paar unit test gebruiken
	//FileChecker class, kopie uit get individueel project van Bas. kijkt of de ifromfile geupload mag worden. sinds de benodigheden het zelfde zijn dacht ik dat ik het kan herbruiken. 
	public class FileChecker
	{
		int UploadSizeLimitInMb = 10;

		public FileUploadPreCheckValue TestFile(IFormFile Ifile)
		{
			using (var memoryStream = new MemoryStream())
			{
				Ifile.CopyTo(memoryStream);

				// Upload the file if less than 2 MB
				if (memoryStream.Length > UploadSizeLimitInMb * 1048576)
				{
					return FileUploadPreCheckValue.TooLarge;
				}

				byte[] file = memoryStream.ToArray();
				if (!CheckImageAllFileSignatures(file))
				{
					return FileUploadPreCheckValue.NoValidFIleType;
				}

				return FileUploadPreCheckValue.Accepted;

			}

		}
		public bool CheckImageAllFileSignatures(byte[] file)
		{
			List<byte[]> Signatures = new List<byte[]>();
			Signatures.Add(StringToByteArray("89504E470D0A1A0A")); // png
			Signatures.Add(StringToByteArray("0A0D0D0A")); // pcapng 
			Signatures.Add(StringToByteArray("FFD8FFE000104A4649460001"));//jpg-jpeg
			Signatures.Add(StringToByteArray("FFD8FFEE"));//jpg-jpeg
			Signatures.Add(StringToByteArray("FFD8FFE0"));//jpg
			Signatures.Add(StringToByteArray("0000000C6A5020200D0A870A"));//jpg2? wat is jpg2
			Signatures.Add(StringToByteArray("FF4FFF51")); //jpg2? wat is jpg2
			//Signatures.Add(StringToByteArray("474946383761"));//gif
			//Signatures.Add(StringToByteArray("474946383961"));//gif

			//a jpeg file signature dived in 2 parts. only dual part signature i have. if more added in the future program beter solution
			byte[] special1 = StringToByteArray("FFD8FFE1");
			byte[] special2 = StringToByteArray("457869660000");

			if (CheckImageFileSignature(file, special1) == true && CheckImageFileSignature(file, special2, 6) == true) return true;

				foreach (var signature in Signatures)
				{
					if (CheckImageFileSignature(file, signature) == true)
					{
						return true;
					}
				}
			return false;
		}

		public bool CheckImageFileSignature(byte[] file, byte[] Signature, int offset = 0)
		{
			if (Signature.Length > file.Length)
			{
				return false;
			}

			for (int i = 0; i < Signature.Length; i++)
			{
				if (file[i + offset] != Signature[i])
				{
					return false;
				}
			}
			return true;
		}
		//source: https://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array
		public static byte[] StringToByteArray(string hex)
		{
			return Enumerable.Range(0, hex.Length)
							 .Where(x => x % 2 == 0)
							 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
							 .ToArray();
		}

	}

	public enum FileUploadPreCheckValue
	{
		Accepted, TooLarge, NoValidFIleType
	}
}
