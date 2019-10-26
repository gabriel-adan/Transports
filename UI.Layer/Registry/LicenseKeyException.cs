using System;

namespace UI.Layer.Registry
{
    internal class LicenseKeyException : Exception
    {
        public LicenseKeyException(string message) : base (message)
        {
            
        }

        public LicenseKeyException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
