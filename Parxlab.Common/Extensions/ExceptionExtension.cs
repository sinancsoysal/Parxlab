﻿using System;

namespace Parxlab.Common.Extensions
{
   public static class ExceptionExtension
    {
        public static string DetailedMessage(this Exception exception)
        {
            return exception.InnerException != null ? exception.InnerException.Message : exception.Message;
        }
    }
}
