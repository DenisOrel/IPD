// Decompiled with JetBrains decompiler
// Type: Intermech.Techcard.Client.FormDesigner.External.CAD.Classes.ActionsCadInfo
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.TechCard.Client.FormDesigner.CAD.Classes;

#nullable disable
namespace Intermech.Techcard.Client.FormDesigner.External.CAD.Classes;

/// <summary>CAD action info</summary>
internal static class ActionsCadInfo
{
  /// <summary>
  /// 
  /// </summary>
  internal static readonly FormDesignerAction ExternalCadExecute = new FormDesignerAction(ExternalCADConsts.ExternalCADActionGuid, ExternalCADConsts.ExternalCADActionCaption, (IFormDesignerActionParams) new ExternalCADActionParams());

  /// <summary>Регистрация доп. кнопки для форм редактирования</summary>
  internal static void RegisterAction()
  {
    ServiceUtils.GetService<IFormDesignerActionManager>((object) ApplicationServices.Container, false)?.RegisterAction(ActionsCadInfo.ExternalCadExecute, (IFormDesignerActionHandler) new ExternalCADActionHandler());
  }
}
