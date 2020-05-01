using System;

namespace Yotec.Api.Helpers
{
    public static class Contract
    {
        public static void NotNull(string arg, string argName)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new ArgumentNullException(argName);
            }
        }
    }
}
