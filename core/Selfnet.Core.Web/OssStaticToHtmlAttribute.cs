using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Selfnet.Core.Web
{
    public class OssStaticToHtmlAttribute : StaticToHtmlAttribute
    {
        private const string OSS_USER_AGENT = "aliyun-oss-mirror";
        /// <summary>
        /// Action执行前处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                var httpContext = filterContext.HttpContext;
                bool hasOssHeader = this.HasOssHeader(httpContext); //是否有oss请求头
                var action = this.ParseStaticAction(httpContext);
                HttpContext.Current.Response.Headers.Add("static-action", action.ToString());
                string filename = this.GetStaticFileName(filterContext.HttpContext);
                HttpContext.Current.Response.Headers.Add("static-filename", filename);
                switch (action)
                {
                    case StaticAction.NoStatic:
                        break;
                    case StaticAction.ToStatic:
                        httpContext.Response.Output = CreateStaticFileWriter(httpContext, filename);
                        break;
                    case StaticAction.Normal:
                    default:
                        if (File.Exists(filename))
                        {
                            filterContext.Result = new FilePathResult(filename, $"text/html;charset={httpContext.Response.Output.Encoding.WebName}");
                        }
                        else if (hasOssHeader)
                        {
                            //oss请求，静态文件不存在时，直接返回loading页面
                            filterContext.Result = new FilePathResult(httpContext.Server.MapPath(LoadingPage), $"text/html;charset={httpContext.Response.Output.Encoding.WebName}");
                        }
                        else
                        {
                            httpContext.Response.Output = CreateStaticFileWriter(httpContext, filename);
                        }
                        break;
                }
                //response异常日志
                if (httpContext.Response.StatusCode >= 400)
                {
                    base.Log(httpContext);
                }
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
        /// <summary>
        /// Result执行后处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            if (!filterContext.IsChildAction)
            {
                var action = this.ParseStaticAction(filterContext.HttpContext);

                string bucket = this.GetOssBucket(filterContext.HttpContext);
                string filename = this.GetStaticFileName(filterContext.HttpContext);
                string filekey = this.GetOSSFileKey(filterContext.HttpContext);

                if (action == StaticAction.ToStatic && !string.IsNullOrEmpty(bucket))
                {
#if !DEBUG
                    this.PushStatic(filename, bucket, filekey); //将静态html推送到oss bucket
#endif
                }
            }
        }
        /// <summary>
        /// 请求头是否带有OSS信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected bool HasOssHeader(HttpContextBase context)
        {
            return context.Request.Headers.AllKeys.Contains<string>("User-Agent")
                && context.Request.Headers.GetValues("User-Agent").Contains<string>(OSS_USER_AGENT);
        }
        /// <summary>
        /// 读取原请求主机名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected string GetOssBucket(HttpContextBase context)
        {
            string bucket = null;
            string referer = context.Request.Headers.Get("Referer");
            if (string.IsNullOrEmpty(referer)) return null;
            var uri = new Uri(referer);
            string bucketHost = $"{uri.Scheme}://{uri.Authority}";
            //reading bucket
            var buckets = "";//ZupoecConfigurationManager.OssDataConfig.Buckets;
            bucket = "";//buckets.SingleOrDefault(x => x.Host == bucketHost)?.Name;
            return bucket;
        }
        /// <summary>
        /// 推送静态页面到OSS
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="filekey"></param>
        protected void PushStatic(string filename, string bucket, string filekey)
        {
            base.Log(new Exception($"filename={filename},bucket={bucket},filekey={filekey}"));
            try
            {
                MemoryStream ms = new MemoryStream(File.ReadAllBytes(filename));
                //EngineContext.Current.Resolve<IOSSService>().PutObject(bucket, filekey, ms, "text/html");
            }
            catch (Exception e)
            {
                base.Log(e);
                throw new Exception("OSS文件保存失败！错误原因：" + e.Message);
            }
        }
        /// <summary>
        /// 获取静态化目标oss文件key
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected string GetOSSFileKey(HttpContextBase context)
        {
            string filekey = context.Request.Url.AbsolutePath;
            if (filekey.EndsWith("/") || filekey.EndsWith("\\"))
            {
                filekey += DefaultFileName;
            }
            if (filekey.StartsWith("/") || filekey.StartsWith("\\"))
            {
                filekey = filekey.Substring(1);
            }
            return filekey;
        }
        /// <summary>
        /// 加载中页面文件
        /// </summary>
        public virtual string LoadingPage { get; set; } = "/html/loading.html";
    }
}
