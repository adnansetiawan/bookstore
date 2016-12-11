using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Exceptions
{
    public class ExceptionCodes
    {
        /// <summary>
        ///     Enum ExceptionCodes for user defined error codes
        /// </summary>
        public enum BaseExceptions
        {
            [Description("An unknown error occured")]
            unhandled_exception = 0
        }

        public enum BLLExceptions
        {
            [Description("Title Is Null Or Empty")]
            TitleIsNullOrEmpty = 100,
            [Description("Category Not Found")]
            CategoryNotFound = 200
        }
    }
}
