
// Type: Intermech.Localization.CustomDisplayName
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.ComponentModel;


namespace Intermech.Localization
{
    internal class CustomDisplayName : DisplayNameAttribute
    {
      public CustomDisplayName(string displayName)
      {
        this.DisplayNameValue = LocalizationHolder.rma.GetString(displayName);
      }
    }
}
