using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Wish.Picture.Web.Models;

namespace Wish.Picture.Web.Repository
{
    public class PictureRepository:IPictureRepository
    {
        public bool SavePictureInfo(UploadResult res,string loginName,string fileName)
        {
            var pictureUrl = $"http://god2035.cn/{res.Key}";

            using (var conn = new MySqlConnection("Host=;Port=3306;Database=wish_picture_db;Uid=;pwd="))
            {
                var sql = @"insert into t_picture(PictureHash,PictureKey,PictureUrl,PictureName,Creator) values(@a,@b,@c,@d,@e)";
                var resExecute = conn.Execute(sql,new []{ new { a = res.Hash, b = res.Key,c = pictureUrl,d=fileName,e=loginName} });
                if (resExecute >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Models.Picture> GetPictures()
        {
            using (var conn = new MySqlConnection("Host=;Port=3306;Database=wish_picture_db;Uid=;pwd="))
            {
                var sql = @"select * from t_picture";
                var resExecute = conn.Query<Models.Picture>(sql).ToList();

                return resExecute;
            }
        }
    }
}