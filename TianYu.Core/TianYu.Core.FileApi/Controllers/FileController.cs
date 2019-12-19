using TianYu.Core.Common.BaseViewModel;
using TianYu.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using TianYu.Core.Log;
using System.Drawing;
using System.Drawing.Imaging;
using TianYu.Core.FileApi.Models;
using System.Drawing.Drawing2D;

namespace TianYu.Core.FileApi.Controllers
{
    /// <summary>
    /// 文件上传控制器
    /// </summary>

    public class FileController : ApiController
    {
        private static Random random = new Random(DateTime.Now.Millisecond);
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="extpath"></param>
        /// <returns></returns>
        [HttpPost]
        public BusinessBaseViewModel<FileUploadViewModel> UploadFile(string extpath = "")
        {
            BusinessBaseViewModel<FileUploadViewModel> viewModel = new BusinessBaseViewModel<FileUploadViewModel>() { Status = ResponseStatus.Fail };
            var request = HttpContext.Current.Request;
            if (request.Files == null || request.Files.Count == 0)
            {
                viewModel.ErrorMessage = "未收到任何文件数据";
                return viewModel;
            }
            if (request.Files.Count == 1)
            {
                return SaveFile(request.Files[0], extpath);
            }
            else
            {
                viewModel.BusinessData = new FileUploadViewModel();
                var resultList = new List<FileUploadItemViewModel>();
                for (int i = 0; i < request.Files.Count; i++)
                {
                    var result = SaveFile(request.Files[i], extpath);
                    resultList.Add(result.BusinessData.UploadResult.First());
                }
                viewModel.Status = ResponseStatus.Success;
                viewModel.BusinessData.UploadResult = resultList;
                return viewModel;
            }
        }
        /// <summary>
        /// 上传文件Base64
        /// </summary>
        /// <param name="paramModel"></param>
        /// <returns></returns>
        [HttpPost]
        public BusinessBaseViewModel<FileUploadViewModel> UploadFileBase64(UploadFileBase64ParamModel paramModel)
        {
            BusinessBaseViewModel<FileUploadViewModel> viewModel = new BusinessBaseViewModel<FileUploadViewModel>() { Status = ResponseStatus.Fail };
            if (paramModel.base64str.IsNullOrWhiteSpace())
            {
                viewModel.ErrorMessage = "未收到任何文件数据";
                return viewModel;
            }
            return SaveFileBase64(paramModel.base64str, paramModel.extpath, paramModel.fileExtName);
        }
        /// <summary>
        /// 文件保存
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="extPath">扩展路径</param>
        /// <returns></returns>
        [NonAction]
        private BusinessBaseViewModel<FileUploadViewModel> SaveFile(HttpPostedFile postedFile, string extPath)
        {
            BusinessBaseViewModel<FileUploadViewModel> viewModel = new BusinessBaseViewModel<FileUploadViewModel>() { Status = ResponseStatus.Fail };
            if (extPath.IsNullOrWhiteSpace())
            {
                extPath = DateTime.Now.ToString("yyyy/MM/dd");
            }
            string rootPath = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadRootPath);
            string fileExtName = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadExtName).ToLower();
            string fileExt = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.')).ToLower();

            if (!fileExtName.ToSplitArray('|').Any(t => t == fileExt))
            {
                viewModel.ErrorMessage = "不允许上传的文件类型" + fileExt;
                return viewModel;
            }

            string fileName = string.Format("{0}{1}{2}", DateTime.Now.ToString("yyMMddHHmmssfff"), random.Next(999999).ToString().PadLeft(6, 'a'), fileExt);
            string filepath = Path.Combine(rootPath, extPath);
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //如果是图片,则保存原图,原图路径
            string orgFilepath = null;
            if (fileExt == ".bmp" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png")
            {
                orgFilepath = Path.Combine(rootPath, extPath + "/OrgFile");
                if (!Directory.Exists(orgFilepath))
                {
                    Directory.CreateDirectory(orgFilepath);
                }
            }

            viewModel.BusinessData = new FileUploadViewModel()
            {
                UploadResult = new List<FileUploadItemViewModel>()
                    {
                        new FileUploadItemViewModel()
                        {
                            FileSize = postedFile.ContentLength,
                            SourceFileName = postedFile.FileName,
                            ServerFilePath = Path.Combine(extPath, fileName).Replace("\\", "/"),
                            IsSuccess = false,
                            FileUrl = string.Format("{0}/{1}",ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadRootUrl).TrimEnd('/'),Path.Combine(extPath, fileName).Replace("\\", "/").TrimStart('/'))
                        }
                    }
            };
            try
            {
                string fileSave = Path.Combine(filepath, fileName);
                postedFile.SaveAs(fileSave);
                //如果是图片
                if(orgFilepath != null)
                {
                    string orgFileSave = Path.Combine(orgFilepath, fileName); //原图路径
                    postedFile.SaveAs(orgFileSave);                                           //原图
                    //压缩图片
                    try
                    {
                        Bitmap map = new Bitmap(fileSave);
                        MemoryStream stream = new MemoryStream();
                        map.Save(stream, ImageFormat.Jpeg);
                        CompressFileMap(map, fileSave, 50);
                    }
                    catch(Exception ex)
                    {
                        LogHelper.LogError("文件上传错误",ex.ToJsonString());
                    }
                }
                
                viewModel.Status = ResponseStatus.Success;
                viewModel.BusinessData.UploadResult.First().IsSuccess = true;
            }
            catch (Exception ex)
            {

                LogHelper.LogError(string.Format("请求:{0}", "/FilteUpload/SaveFile"),
                                                string.Format("异常信息:{0}", ex.ToString()));
                viewModel.ErrorMessage = "上传文件写入失败：" + ex.Message;
            }
            return viewModel;
        }
        /// <summary>
        /// 文件保存Base64
        /// </summary>
        /// <param name="base64str"></param>
        /// <param name="base64str">扩展路径</param>
        /// <returns></returns>
        [NonAction]
        private BusinessBaseViewModel<FileUploadViewModel> SaveFileBase64(string base64str, string extPath, string fileExtName = "")
        {
            BusinessBaseViewModel<FileUploadViewModel> viewModel = new BusinessBaseViewModel<FileUploadViewModel>() { Status = ResponseStatus.Fail };
            if (extPath.IsNullOrWhiteSpace())
            {
                extPath = DateTime.Now.ToString("yyyy/MM/dd");
            }
            string rootPath = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadRootPath);
            string fileExtNameConfig = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadExtName).ToLower();
            string fileExt = fileExtName.IsNullOrWhiteSpace() ? ".jpg" : string.Format(".{0}", fileExtName.TrimStart('.'));
            if (!fileExtNameConfig.ToSplitArray('|').Any(t => t == fileExt))
            {
                viewModel.ErrorMessage = "不允许上传的文件类型" + fileExt;
                return viewModel;
            }

            string fileName = string.Format("{0}{1}{2}", DateTime.Now.ToString("yyMMddHHmmssfff"), random.Next(999999).ToString().PadLeft(6, 'a'), fileExt);
            string filepath = Path.Combine(rootPath, extPath);
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            string orgFilepath = Path.Combine(rootPath, extPath +"/OrgFile");
            if (!Directory.Exists(orgFilepath))
            {
                Directory.CreateDirectory(orgFilepath);
            }

            viewModel.BusinessData = new FileUploadViewModel()
            {
                UploadResult = new List<FileUploadItemViewModel>()
                    {
                        new FileUploadItemViewModel()
                        {
                            FileSize = 0,
                            SourceFileName = fileName,
                            ServerFilePath = Path.Combine(extPath, fileName).Replace("\\", "/"),
                            IsSuccess = false,
                            FileUrl = string.Format("{0}/{1}",ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadRootUrl).TrimEnd('/'),Path.Combine(extPath, fileName).Replace("\\", "/").TrimStart('/'))
                        }
                    }
            };
            try
            {
                string fileSave = Path.Combine(filepath, fileName);     //压缩图
                string orgFileSave = Path.Combine(orgFilepath, fileName); //原图
                byte[] bit = Convert.FromBase64String(base64str);
                MemoryStream ms = new MemoryStream(bit);
                Bitmap bmp = new Bitmap(ms);
                bmp.Save(fileSave);
                bmp.Save(orgFileSave);
                try
                {
  //                  Bitmap bitmap = new Bitmap(bmp.Width,bmp.Height);
                    CompressFileMap(bmp, fileSave, 40);
                }
               catch
                {
                }
                viewModel.Status = ResponseStatus.Success;
                viewModel.BusinessData.UploadResult.First().IsSuccess = true;
                viewModel.BusinessData.UploadResult.First().FileSize = bit.Length;
            }
            catch (Exception ex)
            {

                LogHelper.LogError(string.Format("请求:{0}", "/FilteUpload/SaveFile"),
                                                string.Format("异常信息:{0}", ex.ToString()));
                viewModel.ErrorMessage = "上传文件写入失败：" + ex.Message;
            }
            return viewModel;
        }
        /// <summary>
        /// 尺寸不变，压缩图片质量
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [NonAction]
        private void CompressFileMap(Bitmap srcBitMap, string filePath,long level)
        {
            BusinessBaseViewModel<FileUploadViewModel> businessBaseViewModel = new BusinessBaseViewModel<FileUploadViewModel>() { Status = ResponseStatus.Fail };
            Stream s = new FileStream(filePath, FileMode.Create);
            Compress(srcBitMap, s, level);
            s.Close();
            return;
        }
        [NonAction]
        private void Compress(Bitmap srcBitMap, Stream destStream, long level)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);

            myEncoderParameter = new EncoderParameter(myEncoder, level);
            myEncoderParameters.Param[0] = myEncoderParameter;
            srcBitMap.Save(destStream, myImageCodecInfo, myEncoderParameters);
        }
        /// <summary>
        /// 编码信息
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        [NonAction]
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }

            return null;
        }
        public BusinessBaseViewModel<FileUploadViewModel> QrCodeEncoder(QrcodeEncoderRequestModel requestModel)
        {
            var res = new BusinessBaseViewModel<FileUploadViewModel>() { BusinessData = new FileUploadViewModel() };

            if (requestModel.LogoFilePath.IsNullOrWhiteSpace())
            {
                requestModel.LogoFilePath = ConfigHelper.GetAppsettingValue(TianYuConsts.LogoImagePath);
            }

            if (requestModel.FileName.IsNullOrWhiteSpace())
            {
                requestModel.FileName = requestModel.Content.ToMd5();
            }
            if (requestModel.FileName.IndexOf('.') > 0)
            {
                requestModel.FileName = requestModel.FileName.Substring(0, requestModel.FileName.LastIndexOf('.'));
            }
            string rootPath = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadRootPath);
            string subPath = Path.Combine("QrCode", string.Format("{0}.jpg", requestModel.FileName));
            string saveFilePath = Path.Combine(rootPath, subPath);
            if (!Directory.Exists(Path.Combine(rootPath, "QrCode")))
            {
                Directory.CreateDirectory(Path.Combine(rootPath, "QrCode"));
            }
            if (!File.Exists(saveFilePath))
            {
                try
                {
                    if (requestModel.IsLogo)
                    {
                        requestModel.LogoFilePath = Path.Combine(rootPath, requestModel.LogoFilePath);
                    }
                    else
                    {
                        requestModel.LogoFilePath = string.Empty;
                    }

                    //生成二维码
                    QrCodeHelper.Encoder(requestModel.Content, saveFilePath, requestModel.LogoFilePath);
                }
                catch (Exception ex)
                {
                    LogHelper.LogError("FileController", ex.ToString());
                    res.Status = ResponseStatus.BusinessError;
                    res.ErrorMessage = ex.Message;
                    return res;
                }

            }
            //已经存在则不再生成
            res.BusinessData.UploadResult = new List<FileUploadItemViewModel>()
                    {
                        new FileUploadItemViewModel()
                        {
                            IsSuccess=true,
                            FileUrl=subPath.ToImageUrl(),
                            ServerFilePath=subPath
                        }
                    };
            res.Status = ResponseStatus.Success;
            return res;
        }

        /// <summary>
        /// 生成分享图片
        /// </summary>
        /// <returns></returns>
        public BusinessBaseViewModel<FileUploadViewModel> CreateRedPacketShareImg(RedPacketShareImgRequestModel requestModel)
        {
            var res = new BusinessBaseViewModel<FileUploadViewModel>() { BusinessData = new FileUploadViewModel() };
            if (requestModel.QrContent.IsNullOrWhiteSpace())
            {
                res.ErrorMessage = "二维码内容不能为空";
                return res;
            }
            if (requestModel.FileName.IsNullOrWhiteSpace())
            {
                res.ErrorMessage = "FileName不能为空";
                return res;
            }
            if (requestModel.FileName.IndexOf('.') > 0)
            {
                requestModel.FileName = requestModel.FileName.Substring(0, requestModel.FileName.LastIndexOf('.'));
            }
            string rootPath = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadRootPath);
            var tmpPath = requestModel.Type == 1 ? "RedPacketShareImg" : "RedPacketShareImgMini";
            string subPath = Path.Combine(tmpPath, string.Format("{0}.jpg", requestModel.FileName));
            string saveFilePath = Path.Combine(rootPath, subPath);
            if (!Directory.Exists(Path.Combine(rootPath, tmpPath)))
            {
                Directory.CreateDirectory(Path.Combine(rootPath, tmpPath));
            }
            if (!File.Exists(saveFilePath))
            {
                try
                {
                    //生成图片
                    var backImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigHelper.GetAppsettingValue(TianYuConsts.RedShareBackImg));
                    QrCodeHelper.EncoderShareImg(requestModel.Type, requestModel.QrContent, backImg, saveFilePath, requestModel.UserName, requestModel.UserImg);
                }
                catch (Exception ex)
                {
                    LogHelper.LogError("FileController.CreateRedPacketShareImg", ex.ToString());
                    res.Status = ResponseStatus.BusinessError;
                    res.ErrorMessage = ex.Message;
                    return res;
                }
            }
            //已经存在则不再生成
            res.BusinessData.UploadResult = new List<FileUploadItemViewModel>()
                    {
                        new FileUploadItemViewModel()
                        {
                            IsSuccess=true,
                            FileUrl=subPath.ToImageUrl(),
                            ServerFilePath=subPath
                        }
                    };
            res.Status = ResponseStatus.Success;
            return res;
        }


        /// <summary>
        /// 根据网络图片上传文件（下载网络图片，并保存到文件站点）
        /// </summary>
        /// <param name="paramModel"></param>
        /// <returns></returns>
        [HttpPost]
        public BusinessBaseViewModel<FileUploadViewModel> UploadFileByNetworkFile(UploadFileNetworkFileParamModel paramModel)
        {
            BusinessBaseViewModel<FileUploadViewModel> viewModel = new BusinessBaseViewModel<FileUploadViewModel>() { Status = ResponseStatus.Fail };
            if (paramModel.FileUrl.IsNullOrWhiteSpace())
            {
                viewModel.ErrorMessage = "图片URL不能为空";
                return viewModel;
            }
            return SaveUploadFileByNetworkFile(paramModel.FileUrl, paramModel.Extpath, paramModel.FileExt);
        }

        /// <summary>
        /// 保存根据网络图片上传文件（下载网络图片，并保存到文件站点）
        /// </summary>
        /// <param name="fileUrl">网络图片URL</param>
        /// <param name="extpath">扩展参数</param>
        /// <param name="fileExt">扩展名（如：.jpg）</param>
        /// <returns></returns>
        private BusinessBaseViewModel<FileUploadViewModel> SaveUploadFileByNetworkFile(string fileUrl, string extpath, string fileExt)
        {
            BusinessBaseViewModel<FileUploadViewModel> viewModel = new BusinessBaseViewModel<FileUploadViewModel>() { Status = ResponseStatus.Fail };

            if (fileExt.IsNullOrWhiteSpace())
            {
                fileExt = ".jpg";
            }
            if (!fileExt.StartsWith("."))
            {
                fileExt = "." + fileExt;
            }
            
            if (extpath.IsNullOrWhiteSpace())
            {
                extpath = DateTime.Now.ToString("yyyy/MM/dd");
            }

            string rootPath = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadRootPath);
            string fileExtNameConfig = ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadExtName).ToLower();

            string fileName = string.Format("{0}{1}{2}", DateTime.Now.ToString("yyMMddHHmmssfff"), random.Next(999999).ToString().PadLeft(6, 'a'), fileExt);
            string filepath = Path.Combine(rootPath, extpath);
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            
            viewModel.BusinessData = new FileUploadViewModel()
            {
                UploadResult = new List<FileUploadItemViewModel>()
                    {
                        new FileUploadItemViewModel()
                        {
                            FileSize = 0,
                            SourceFileName = fileName,
                            ServerFilePath = Path.Combine(extpath, fileName).Replace("\\", "/"),
                            IsSuccess = false,
                            FileUrl = string.Format("{0}/{1}",ConfigHelper.GetAppsettingValue(TianYuConsts.SystemFileUploadRootUrl).TrimEnd('/'),Path.Combine(extpath, fileName).Replace("\\", "/").TrimStart('/'))
                        }
                    }
            };
            try
            {
                string fileSave = Path.Combine(filepath, fileName);

                WebClient client = new WebClient();
                client.DownloadFile(fileUrl, fileSave);
                viewModel.Status = ResponseStatus.Success;
                viewModel.BusinessData.UploadResult.First().IsSuccess = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format("请求:{0}", "/FilteUpload/SaveUploadFileByNetworkFile"),
                                                string.Format("异常信息:{0}", ex.ToString()));
                viewModel.ErrorMessage = "上传文件写入失败：" + ex.Message;
            }
            return viewModel;
        }
    }
}
