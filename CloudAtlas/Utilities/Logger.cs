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
            string logFilePath = ConfigurationManager.AppSettings["LogFile"];

            string message = string.Format("{0} was thrown on the {1}.{4}For: {2}{3}{4}",
                ex.Message, DateTime.Now, ex.Source, ex.StackTrace, Environment.NewLine);

            var directoryPath = HttpContext.Current.Server.MapPath("~/Logs/");


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (StreamWriter writer = new StreamWriter(logFilePath, true, Encoding.Default))
            {
                writer.WriteLine(message);
            }

        }
    }
}