
// Type: Intermech.Files.SymlinkFolderValidator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Settings;
using System.IO;


namespace Intermech.Files;

internal sealed class SymlinkFolderValidator(SettingsCell<string> cell) : SingleCellValidator<string>(cell)
{
  protected override void OnValidateCell()
  {
    string rawValue = this.cell.RawValue;
    if (string.IsNullOrEmpty(rawValue))
      return;
    if (rawValue.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
    {
      this.cell.Error = "Имя папки содержит недопустимые символы.";
    }
    else
    {
      if (!Path.IsPathRooted(rawValue))
        return;
      this.cell.Error = "Имя папки не должно содержать имени диска или сетевого ресурса";
    }
  }
}
