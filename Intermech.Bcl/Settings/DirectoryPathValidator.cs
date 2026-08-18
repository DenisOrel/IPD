
// Type: Intermech.Settings.DirectoryPathValidator
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Localization;
using System.IO;


namespace Intermech.Settings
{
    public sealed class DirectoryPathValidator(SettingsCell<string> cell) : SingleCellValidator<string>(cell)
    {
      protected override void OnValidateCell()
      {
        string rawValue = this.cell.RawValue;
        if (string.IsNullOrEmpty(rawValue))
          this.cell.Error = LocalizationHolder.rm.GetString("SR_810");
        else if (!Path.IsPathRooted(rawValue))
        {
          this.cell.Error = LocalizationHolder.rm.GetString("SR_811");
        }
        else
        {
          if (Directory.Exists(rawValue))
            return;
          this.cell.Error = string.Format(LocalizationHolder.rm.GetString("SR_812"), (object) rawValue);
        }
      }
    }
}
