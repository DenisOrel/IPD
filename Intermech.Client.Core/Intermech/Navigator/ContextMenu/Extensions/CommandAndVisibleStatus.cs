
// Type: Intermech.Navigator.ContextMenu.Extensions.CommandAndVisibleStatus
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.ContextMenu.Extensions;

/// <summary>команда и статус видимости</summary>
public class CommandAndVisibleStatus
{
  public readonly string Name;
  /// <summary>Если true - команда будет доступна, если false - нет</summary>
  public bool IsVisible;

  public CommandAndVisibleStatus(string name, bool isVisible)
  {
    this.Name = name;
    this.IsVisible = isVisible;
  }
}
