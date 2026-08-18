
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.ComponentModel;


namespace Intermech.Localization
{
    internal class CustomDescription : DescriptionAttribute
    {
      public CustomDescription(string description)
      {
        if (LocalizationHolder.rma.GetString(description) != null)
          this.DescriptionValue = LocalizationHolder.rma.GetString(description);
        else
          this.DescriptionValue = string.Empty;
      }
    }
}
