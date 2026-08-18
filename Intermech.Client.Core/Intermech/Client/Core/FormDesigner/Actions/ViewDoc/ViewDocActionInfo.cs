
// Type: Intermech.Client.Core.FormDesigner.Actions.ViewDoc.ViewDocActionInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Client.Core.FormDesigner.Actions.ViewDoc;

/// <summary>
/// Вспомогательный класс для создания и регистрации действия "Просмотр документа".
/// </summary>
internal static class ViewDocActionInfo
{
  internal static readonly FormDesignerAction ViewDocAction = new FormDesignerAction(ViewDocActionConsts.ActionGuid, ViewDocActionConsts.ActionCaption, (IFormDesignerActionParams) new ViewDocActionParams());

  /// <summary>Регистрация доп. действия для форм редактирования</summary>
  /// <param name="manager"></param>
  internal static void RegisterAction(IFormDesignerActionManager manager = null)
  {
    manager = manager ?? ApplicationServices.Container.GetService<IFormDesignerActionManager>(false);
    manager?.RegisterAction(ViewDocActionInfo.ViewDocAction, (IFormDesignerActionHandler) new ViewDocActionHandler());
  }
}
