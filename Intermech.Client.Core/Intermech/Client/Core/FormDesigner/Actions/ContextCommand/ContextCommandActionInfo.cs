
// Type: Intermech.Client.Core.FormDesigner.Actions.ContextCommand.ContextCommandActionInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;


namespace Intermech.Client.Core.FormDesigner.Actions.ContextCommand;

/// <summary>ContextCommand's action info</summary>
internal static class ContextCommandActionInfo
{
  /// <summary>
  /// 
  /// </summary>
  internal static readonly FormDesignerAction Action = new FormDesignerAction(ContextCommandActionConsts.ActionGuid, ContextCommandActionConsts.ActionCaption, (IFormDesignerActionParams) new ContextCommandActionParams());

  /// <summary>Регистриция доп. кнопки для форм редактирования</summary>
  internal static void RegisterAction(IFormDesignerActionManager manager = null)
  {
    manager = manager ?? ServiceUtils.GetService<IFormDesignerActionManager>((object) ServicesManager.ServiceContainer, false);
    manager?.RegisterAction(ContextCommandActionInfo.Action, (IFormDesignerActionHandler) new ContextCommandActionHandler());
  }
}
