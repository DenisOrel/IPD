
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

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
