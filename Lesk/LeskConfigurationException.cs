using System;

namespace Lesk
{
    public class LeskConfigurationException : Exception
    {
        public LeskConfigurationException(Exception innerException)
            : base("Failed to configure Lesk. Please look at innerException for more details.", innerException)
        {
        }
    }
}