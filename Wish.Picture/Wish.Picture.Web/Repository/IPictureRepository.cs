
using System.Collections.Generic;
using Wish.Picture.Web.Models;

namespace Wish.Picture.Web.Repository
{
    public interface IPictureRepository
    {
        bool SavePictureInfo(UploadResult res, string loginName, string fileName);

        List<Models.Picture> GetPictures();
    }
}