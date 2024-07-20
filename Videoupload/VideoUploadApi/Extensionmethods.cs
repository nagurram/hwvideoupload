using System.Runtime.CompilerServices;

namespace VideoUploadApi
{
    public static class Extensionmethods
    {

        public static long  ConvertToKB(this long value)
        {
            return Convert.ToInt64(value/1024);
        }
    }
}
