using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class FileLogger:LoggerServiceBase
    {
        public FileLogger():base("JsonFileLogger")
        {

        }
    }
}
