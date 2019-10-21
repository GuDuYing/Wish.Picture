using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using Wish.Picture.Web.Helpers;
using Wish.Picture.Web.Models;
using Wish.Picture.Web.Repository;

namespace Wish.Picture.Web.Controllers
{
    public class PictureController : Controller
    {
        public IActionResult Index()
        {
            var loginUserName = HttpContext.Session.GetString("LoginUserName");
            if (!string.IsNullOrEmpty(loginUserName))
            {
                ViewData["loginUserName"] = loginUserName;
            }

            var repository = new PictureRepository();
            var pictureList = repository.GetPictures();
            if (pictureList.Count >= 1)
            {
                ViewData["PictureList"] = pictureList;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            var loginUserName = HttpContext.Session.GetString("LoginUserName");
            if (!string.IsNullOrEmpty(loginUserName))
            {
                ViewData["loginUserName"] = loginUserName;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
        }

        public IActionResult UploadImg()
        {

            var loginUserName = HttpContext.Session.GetString("LoginUserName");
            if (!string.IsNullOrEmpty(loginUserName))
            {
                IFormFileCollection files = Request.Form.Files;
                //这里的ak sk 可以写到配置文件中方便修改
                Mac mac = new Mac("", "");
                // 上传策略，参见 
                // https://developer.qiniu.com/kodo/manual/put-policy
                PutPolicy putPolicy = new PutPolicy();
                // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
                // putPolicy.Scope = bucket + ":" + saveKey;  
                //上传储存的空间名称
                putPolicy.Scope = "ling-picture";
                // 上传策略有效期(对应于生成的凭证的有效期)          
                putPolicy.SetExpires(3600);
                // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
                // putPolicy.DeleteAfterDays = 1;
                string jstr = putPolicy.ToJsonString();
                //获取上传凭证
                var uploadToken = Auth.CreateUploadToken(mac, jstr);

                //false 使用https 自动识别存储空间            
                Qiniu.Common.Config.AutoZone("", "ling-picture", false);

                UploadManager um = new UploadManager();
                foreach (IFormFile file in files)//获取多个文件列表集合
                {
                    if (file.Length > 0)
                    {
                        Stream stream = file.OpenReadStream();
                        //var fileName = ContentDispositionHeaderValue
                        //.Parse(file.ContentDisposition)
                        //.FileName
                        //.Trim('"');
                        string fileName = file.FileName.Substring(file.FileName.LastIndexOf('.')); //文件扩展名
                                                                                                   //DateTime.Now.ToString("yyyyMMddHHmmssffffff")
                        var saveKey = "piture/" + Guid.NewGuid().ToString("N") + fileName;//重命名文件加上时间戳 其中上传地址也可以配置s   
                        HttpResult result = um.UploadStream(stream, saveKey, uploadToken);

                        if (result.Code == 200)
                        {
                            var res = result.Text.ToObject<UploadResult>();
                            var repository = new PictureRepository();
                            var addRes = repository.SavePictureInfo(res, loginUserName, file.FileName);
                            if (addRes)
                            {
                                return Content("上传成功");
                            }
                            else
                            {
                                return Content("上传失败");
                            }
                        }
                        else
                        {
                            throw new Exception(result.RefText);//上传失败错误信息
                        }
                    }
                }
                return null;
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


            
        }

    }
}