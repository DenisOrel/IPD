
// Type: Intermech.Client.Core.FormDesigner.Actions.ContextCommand.ContextCommandActionMethod
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;


namespace Intermech.Client.Core.FormDesigner.Actions.ContextCommand;

/// <summary>
/// Класс - контейнер для хранения информации о методе контекстного меню
/// </summary>
[TypeConverter(typeof (ContextCommandActionMethodConverter))]
[Serializable]
public class ContextCommandActionMethod
{
  /// <summary>
  /// 
  /// </summary>
  private readonly string _commandName;
  /// <summary>
  /// Локализованное описание комманды, отображажемое в контролах
  /// </summary>
  private string _commandText;

  /// <summary>Конструктор</summary>
  public ContextCommandActionMethod()
    : this(string.Empty)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="commandName"></param>
  public ContextCommandActionMethod(string commandName) => this._commandName = commandName;

  /// <summary>Внутреннее имя комманды</summary>
  public string CommandName => this._commandName;

  /// <summary>
  /// Локализованное описание комманды, отображажемое в контролах
  /// </summary>
  public string CommandText
  {
    get
    {
      if (this._commandText != null)
        return this._commandText;
      this._commandText = string.Empty;
      if (string.IsNullOrEmpty(this.CommandName))
        return this._commandText;
      IFactory service = ServiceUtils.GetService<IFactory>((object) ServicesManager.ServiceContainer, false);
      if (service != null && service.ContextMenuTemplate != null)
      {
        MenuTemplateNode menuTemplateNode = service.ContextMenuTemplate[this.CommandName];
        if (menuTemplateNode != null)
          this._commandText = menuTemplateNode.Text;
      }
      return this._commandText;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString() => this.CommandText;
}
