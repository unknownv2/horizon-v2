using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    internal class SvodException : Exception
    {
        internal SvodException(string message)
            : base("SVOD: " + message)
        {

        }
    }

    internal class OdfxException : Exception
    {
        internal OdfxException(string message)
            : base("ODFX: " + message)
        {

        }
    }

    internal class StfsException : Exception
    {
        internal StfsException(string message)
            : base("STFS: " + message)
        {

        }
    }

    internal class FatxException : Exception
    {
        internal FatxException(string message)
            : base("FATX: " + message)
        {

        }
    }

    internal class XmiException : Exception
    {
        internal XmiException(string message)
            : base("XMI: " + message)
        {

        }
    }

    internal class DataFileException : Exception
    {
        internal DataFileException(string message)
            : base("GPD: " + message)
        {

        }
    }

    internal class ConfigException : Exception
    {
        internal ConfigException(string message)
            : base("CONFIG: " + message)
        {

        }
    }

    internal class IgnoreException : Exception
    {
        internal IgnoreException()
            : base()
        {

        }
    }
}
