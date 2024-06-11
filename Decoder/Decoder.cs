using NLog;
using System;
using System.Text;

namespace Decoder
{
    public static class Decoder
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static string DecodeText(string text)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding cp866 = Encoding.GetEncoding(866);
                Encoding utf8 = Encoding.UTF8;
                byte[] cp866Bytes = cp866.GetBytes(text);
                byte[] utf8Bytes = Encoding.Convert(cp866, utf8, cp866Bytes);
                return utf8.GetString(utf8Bytes);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return text;
            }
        }
    }
}