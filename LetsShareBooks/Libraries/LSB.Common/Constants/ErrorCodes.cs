using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSB.Constants
{
    public class ErrorCodes
    {
        /// <summary>
        /// Stores Startup Initialize Error - Error Code
        /// </summary>
        public const string StartupInitializeErrorCode = "StartupInitializeError";

        /// <summary>
        /// Stores Configuration Read Error - Error Message
        /// </summary>
        public const string ConfigReadErrorCode = "ConfigReadError";

        /// <summary>
        /// Stores Internal Server Error - Error Code
        /// </summary>
        public const string InternalServerErrorCode = "InternalServerError";

        /// <summary>
        /// Stores Internal Server Error - Error Code
        /// </summary>
        public const string BadRequestCode = "BadRequestError";

        /// <summary>
        /// Stores Null Or Empty - Error Code
        /// </summary>
        public const string NullOrEmptyCode = "NullOrEmptyorWhitespace";
    }
}
