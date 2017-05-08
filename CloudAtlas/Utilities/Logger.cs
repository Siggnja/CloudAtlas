using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CloudAtlas.Utilities
{
    public class Logger
    {

        private static Logger theInstance = null;
        public static Logger Instance
        {
            get
            {
                if (theInstance == null)
                {
                    theInstance = new Logger();
                }
                return theInstance;
            }
        }

        public void LogException(Exception ex)
        {

            var directoryPath = HttpContext.Current.Server.MapPath("~/Logs/");
            var logFilePath = "CloudAtlas_log.txt";
            var message = string.Format("{0} Time: {1}.{3}For: {2}{3}", ex.Message, DateTime.Now, ex.Source, Environment.NewLine);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (var writer = new StreamWriter(directoryPath + logFilePath, true, Encoding.Default))
            {
                writer.WriteLine(message);
            }

        }
    }
}