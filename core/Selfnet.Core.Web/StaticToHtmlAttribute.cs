using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Selfnet.Core.Web
{
    /// <summary>
    /// html static attribute
    /// generated html page every day
    /// </summary>
    public class StaticToHtmlAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行前处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                var httpContext = filterContext.HttpContext;
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
                        //else if (FileAttributes.Offline == File.GetAttributes(filename))
                        //{
                        //    filterContext.Result = new ContentResult { Content = "数据准备中，请稍候……", ContentEncoding = Encoding.UTF8, ContentType = "text/html; charset=utf-8" };
                        //}
                        else
                        {
                            httpContext.Response.Output = CreateStaticFileWriter(httpContext, filename);
                        }
                        break;
                }
                //response异常日志
                if (httpContext.Response.StatusCode >= 400)
                {
                    this.Log(httpContext);
                }
            }
            base.OnActionExecuting(filterContext);
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
                var writer = filterContext.HttpContext.Response.Output as StaticHtmlWriter;
                if (writer != null)
                {
                    writer.Complete($"<!--Created On: {DateTime.Now:yyyy-MM-dd HH:mm:ss}-->");
                }
            }
        }

        /// <summary>
        /// 静态化完成处理函数
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void OnStaticCompleted(TextWriter writer)
        {
            var flag = $"<!--Created On: {DateTime.Now:yyyy-MM-dd HH:mm:ss}-->";
            if (writer is StaticHtmlWriter)
            {
                (writer as StaticHtmlWriter).Complete(flag);
            }
            else
            {
                writer.WriteLine(flag);
            }
        }

        /// <summary>
        /// 解析静态化行为
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks>
        /// 通过querystring获取参数 static-action 的配置信息进行解析
        /// </remarks>
        protected virtual StaticAction ParseStaticAction(HttpContextBase context)
        {
            string param = context.Request.QueryString["static-action"];
            StaticAction action;
            if (!Enum.TryParse(param, true, out action))
            {
                action = StaticAction.Normal;
            }
            return action;
        }

        /// <summary>
        /// 创建静态文件拦截文本输出器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        protected virtual TextWriter CreateStaticFileWriter(HttpContextBase context, string filename)
        {
            return new StaticHtmlWriter(context, filename);
        }

        /// <summary>
        /// 获取静态化目标文件名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks>
        /// 默认：将静态文件保存在站点下的 .html 文件夹中
        ///         静态文件按日期分类存储
        /// </remarks>
        protected virtual string GetStaticFileName(HttpContextBase context)
        {
            string filename = context.Request.Url.AbsolutePath;
            if (filename.EndsWith("/") || filename.EndsWith("\\"))
            {
                filename += DefaultFileName;
            }
            if (filename.StartsWith("/") || filename.StartsWith("\\"))
            {
                filename = filename.Substring(1);
            }
            if (SeparatedByDate)
            {
                filename = Path.Combine(string.Format("{0:" + SeparateDateFormat + "}", DateTime.Now), filename);
            }
            filename = Path.Combine(StaticFolder, filename);
            FileInfo fileinfo = new FileInfo(Path.Combine(context.Server.MapPath($"~"), filename));
            return fileinfo.FullName;
        }
        /// <summary>
        /// 记录异常response文本日志
        /// </summary>
        /// <param name="context"></param>
        protected virtual void Log(HttpContextBase context)
        {
            string filename = $"{DateTime.Today.ToString(SeparateDateFormat)}{LogExtname}";
            filename = Path.Combine(LogFolder, filename);

            Task.Run(() =>
            {
                FileInfo fileinfo = new FileInfo(Path.Combine(context.Server.MapPath($"~"), filename));
                if (!fileinfo.Directory.Exists)
                {
                    fileinfo.Directory.Create();
                }
                string logname = fileinfo.FullName;
                StringBuilder sb = new StringBuilder();
                sb.Append("\r\n");
                sb.Append($"<!--{context.Request.Url} Request Error Begin-->\r\n");
                sb.Append($"Response Status:{context.Response.Status}\r\n");
                sb.Append($"Response StatusCode:{context.Response.StatusCode}\r\n");
                sb.Append($"Response StatusDescription:{context.Response.StatusDescription}\r\n");
                sb.Append($"<!--{context.Request.Url} Request Error End-->\r\n");
                sb.Append("\r\n");
                File.AppendAllText(logname, sb.ToString());
            });
        }

        /// <summary>
        /// 记录异常response文本日志
        /// </summary>
        /// <param name="context"></param>
        protected virtual void Log(Exception ex)
        {
            string filename = $"{DateTime.Today.ToString(SeparateDateFormat)}{LogExtname}";
            filename = Path.Combine(LogFolder, filename);
            string path = Path.Combine(HttpContext.Current.Server.MapPath($"~"), filename);
            Task.Run(() =>
            {
                FileInfo fileinfo = new FileInfo(path);
                if (!fileinfo.Directory.Exists)
                {
                    fileinfo.Directory.Create();
                }
                string logname = fileinfo.FullName;
                StringBuilder sb = new StringBuilder();
                sb.Append("\r\n");
                sb.Append($"<!--Exception message Begin-->\r\n");
                sb.Append($"Exception:{ex.ToString()}\r\n");
                sb.Append($"<!--Exception message End-->\r\n");
                sb.Append("\r\n");
                File.AppendAllText(logname, sb.ToString());
            });
        }

        /// <summary>
        /// 是否将静态文件按日期分区存储。默认使用
        /// </summary>
        public virtual bool SeparatedByDate
        {
            get;
            set;
        } = true;

        /// <summary>
        /// 日期间隔表述格式
        /// </summary>
        /// <remarks>
        /// 字符串表示，如 yyyy-MM-dd，默认为 yy-MM-dd
        /// </remarks>
        public virtual string SeparateDateFormat
        {
            get;
            set;
        } = "yy-MM-dd";

        /// <summary>
        /// 当访问目录时，目标文件名。默认为 index.html
        /// </summary>
        public virtual string DefaultFileName
        {
            get;
            set;
        } = "index.html";

        /// <summary>
        /// 静态文件存储路径
        /// </summary>
        public string StaticFolder
        {
            get;
            set;
        } = ".html";
        /// <summary>
        /// 日志文件路径
        /// </summary>
        public string LogFolder { get; set; } = "Log";
        /// <summary>
        /// 日志文件扩展名
        /// </summary>
        public string LogExtname { get; set; } = ".txt";
    }

    /// <summary>
    /// 静态化处理方式
    /// </summary>
    public enum StaticAction
    {
        /// <summary>
        /// 静态化到文件，优先使用静态文件
        /// </summary>
        Normal,
        /// <summary>
        /// 不使用静态化文件
        /// </summary>
        NoStatic,
        /// <summary>
        /// 不使用静态文件，但将结果生成静态文件
        /// </summary>
        ToStatic,
    }
}
