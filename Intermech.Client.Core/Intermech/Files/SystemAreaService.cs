
// Type: Intermech.Files.SystemAreaService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Files;

internal sealed class SystemAreaService(
  FileVaultService vault,
  string areaDirectory,
  string displayName) : AreaBase(vault, areaDirectory, displayName)
{
  /// <summary>Выполняет инициализацию файловой области.</summary>
  internal override void Initialize()
  {
    base.Initialize();
    this.HideAreaDirectory();
  }
}
