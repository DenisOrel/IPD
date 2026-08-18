
// Type: Intermech.Runtime.NameExtensions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.CompilerServices;


namespace Intermech.Runtime
{
    public static class NameExtensions
    {
      public static string GetCurrentMethodName(this object obj, [CallerMemberName] string methodName = null)
      {
        if (obj == null)
          throw new ArgumentNullException(nameof (obj));
        if (methodName == null)
          throw new ArgumentNullException(nameof (methodName));
        return $"{obj.GetType().Name}.{methodName}";
      }
    }
}
