
// Type: Intermech.Localization.LocalizationUtils
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text;


namespace Intermech.Localization
{
    public static class LocalizationUtils
    {
      public static string RestoreEscapes(string text)
      {
        StringBuilder stringBuilder = text != null ? new StringBuilder(text) : throw new ArgumentNullException(nameof (text));
        stringBuilder.Replace("\\r\\n", "\r\n");
        stringBuilder.Replace("\\n\\r", "\r\n");
        stringBuilder.Replace("\\r", "\r");
        stringBuilder.Replace("\\n", "\n");
        stringBuilder.Replace("\\t", "\t");
        return stringBuilder.ToString();
      }
    }
}
