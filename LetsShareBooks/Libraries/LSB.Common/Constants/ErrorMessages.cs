using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSB.Constants
{
    public class ErrorMessages
    {
        /// <summary>
        /// Stores Configuration Read Error - Error Message
        /// </summary>
        public const string ConfigReadErrorMessage = "Configuration value is missing or in incorrect format :";

        /// <summary>
        /// Stores Internal Server Error - Error Message
        /// </summary>
        public const string InternalServerErrorMessage = "Internal Server Error, Please try again later!";

        /// <summary>
        /// Stores Internal Server Error - Error Code
        /// </summary>
        public const string BadRequestMessage = "Bad or Invalid Request Body Error, Please check the request body again!";

        /// <summary>
        /// Stores Null Or Empty - Error Message
        /// </summary>
        public const string NullOrEmptyMessage = "Null or Empty or Whitespace value passed for the Object.";
    }
}
