using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Wish.Picture.Web.Models;

namespace Wish.Picture.Web.Repository
{
    public class UserRepository
    {
        public List<User> GetUser(string userName, string password)
        {
            using (var conn = new MySqlConnection("Host=;Port=3306;Database=wish_picture_db;Uid=;pwd="))
            {
                var sql = @"select * from t_user where UserName = @a and Password = @b and Statu = 1";
                var resExecute = conn.Query<User>(sql,new { a = userName,b=password }).ToList();

                return resExecute;
            }
        }
    }
}