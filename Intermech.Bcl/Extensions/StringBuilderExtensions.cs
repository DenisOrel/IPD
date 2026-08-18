
// Type: Intermech.Extensions.StringBuilderExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;


namespace Intermech.Extensions
{
    public static class StringBuilderExtensions
    {
      [NotNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Truncate([NotNull] this StringBuilder stringBuilder, int maxChars)
      {
        if (stringBuilder.Length <= maxChars)
          return;
        stringBuilder.Remove(maxChars, stringBuilder.Length - maxChars);
        stringBuilder.Append("…");
      }
    }
}
