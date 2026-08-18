
// Type: Intermech.Files.DriveLetterValitator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Settings;


namespace Intermech.Files;

internal sealed class DriveLetterValitator(SettingsCell<char> cell) : SingleCellValidator<char>(cell)
{
  protected override void OnValidateCell()
  {
    char rawValue = this.cell.RawValue;
    if (rawValue == char.MinValue || rawValue >= 'a' && rawValue <= 'z' || rawValue >= 'A' && rawValue <= 'Z')
      return;
    this.cell.Error = "Недопустимая буква диска.";
  }
}
