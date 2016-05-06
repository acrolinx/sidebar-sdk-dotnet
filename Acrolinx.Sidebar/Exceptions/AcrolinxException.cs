/* Copyright (c) 2016 Acrolinx GmbH */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrolinx.Sdk.Sidebar.Exceptions
{
    public class AcrolinxException : Exception
    {
        public AcrolinxException(string message)
            : base(message)
        {
        }

        public AcrolinxException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
