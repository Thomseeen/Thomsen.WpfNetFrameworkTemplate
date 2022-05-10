using System;
using System.Text;

namespace Thomsen.WpfTools.Util {
    public static class ExceptionExtension {
        public static string GetAllMessages(this Exception ex) {
            StringBuilder sb = new();

            sb.AppendLine($"{ex.Message}");
            ex = ex.InnerException;

            for (int ii = 0; ex is not null; ii++) {
                sb.AppendLine($"{new string('-', ii + 1)}> {ex.Message}");
                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}
