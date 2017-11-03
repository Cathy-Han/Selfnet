﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Selfnet.Core.Web
{
    /// <summary>
    /// 静态化HTML输出器
    /// </summary>
    public class StaticHtmlWriter : TextWriter
    {
        private HttpContextBase _context;
        private TextWriter _writer;
        private string _filename;
        private TextWriter _current;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filename"></param>
        public StaticHtmlWriter(HttpContextBase context, string filename)
        {
            _context = context;
            _writer = context.Response.Output;
            _filename = filename;
            FileInfo file = new FileInfo(filename);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            using (var writer = new StreamWriter(filename, false, Encoding.ASCII))
            {
                writer.WriteLine($"<!-- this file is auto generated by system. -->");
                writer.WriteLine($"<!-- DateTime on Utc: [{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] -->");
                writer.WriteLine($"<!-- REMOTE_ADDR: [{context.Request.ServerVariables["REMOTE_ADDR"]}] -->");
                writer.WriteLine($"<!-- LOCAL_ADDR: [{context.Request.ServerVariables["LOCAL_ADDR"]}] -->");
            }
            _current = new StreamWriter(filename, true, _writer.Encoding);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void Complete(string text = null)
        {
            if (text != null)
            {
                _current.WriteLine(text);
            }
            this.Flush();
            _current.Close();
        }

        #region 重载函数处理

        /// <summary>
        /// 
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return _writer.Encoding;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Close()
        {
            _current.Close();
            _writer.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Flush()
        {
            _current.Flush();
            _writer.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _current.Dispose();
                //_writer.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override IFormatProvider FormatProvider
        {
            get
            {
                return _writer.FormatProvider;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(bool value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(char value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public override void Write(char[] buffer)
        {
            _current.Write(buffer);
            _writer.Write(buffer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void Write(char[] buffer, int index, int count)
        {
            TryAction(() => _current.Write(buffer, index, count));
            TryAction(() => _writer.Write(buffer, index, count));
        }

        private static void TryAction(Action action, Action<Exception> exception = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                exception?.Invoke(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(decimal value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(double value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(float value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(int value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(long value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(object value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        public override void Write(string format, object arg0)
        {
            _current.Write(format, arg0);
            _writer.Write(format, arg0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        public override void Write(string format, object arg0, object arg1)
        {
            _current.Write(format, arg0, arg1);
            _writer.Write(format, arg0, arg1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            _current.Write(format, arg0, arg1, arg2);
            _writer.Write(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        public override void Write(string format, params object[] arg)
        {
            _current.Write(format, arg);
            _writer.Write(format, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(string value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(uint value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void Write(ulong value)
        {
            _current.Write(value);
            _writer.Write(value);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void WriteLine()
        {
            _current.WriteLine();
            _writer.WriteLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(bool value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(char value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        public override void WriteLine(char[] buffer)
        {
            _current.WriteLine(buffer);
            _writer.WriteLine(buffer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void WriteLine(char[] buffer, int index, int count)
        {
            _current.WriteLine(buffer, index, count);
            _writer.WriteLine(buffer, index, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(decimal value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(double value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(float value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedType"></param>
        /// <returns></returns>
        public override ObjRef CreateObjRef(Type requestedType)
        {
            return _writer.CreateObjRef(requestedType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object InitializeLifetimeService()
        {
            return _writer.InitializeLifetimeService();
        }

        /// <summary>
        /// 
        /// </summary>
        public override string NewLine
        {
            get
            {
                return _writer.NewLine;
            }
            set
            {
                _writer.NewLine = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._writer.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(int value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(long value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(object value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        public override void WriteLine(string format, object arg0)
        {
            _current.WriteLine(format, arg0);
            _writer.WriteLine(format, arg0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        public override void WriteLine(string format, object arg0, object arg1)
        {
            _current.WriteLine(format, arg0, arg1);
            _writer.WriteLine(format, arg0, arg1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            _current.WriteLine(format, arg0, arg1, arg2);
            _writer.WriteLine(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        public override void WriteLine(string format, params object[] arg)
        {
            _current.WriteLine(format, arg);
            _writer.WriteLine(format, arg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(string value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(uint value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public override void WriteLine(ulong value)
        {
            _current.WriteLine(value);
            _writer.WriteLine(value);
        }

        #endregion

    }
}