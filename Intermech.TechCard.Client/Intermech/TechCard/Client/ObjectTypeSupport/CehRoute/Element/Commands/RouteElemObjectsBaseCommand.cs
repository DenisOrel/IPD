// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.Commands.RouteElemObjectsBaseCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.TechCard;
using Intermech.TechCard.Client.Commands.Edit;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.Commands;

/// <summary>
/// 
/// </summary>
internal class RouteElemObjectsBaseCommand : SimpleEditCommand
{
  /// <summary>Шаблон РЭ для которого необходимо выполнить команду</summary>
  protected long _routeElementTemplateObjectId;

  /// <summary>Диалог выбора РЭ</summary>
  /// <returns></returns>
  protected virtual bool SelectRouteElemObjects()
  {
    this._routeElementTemplateObjectId = TechCardClientConst.SelectObjectDlg(TechCardConsts.ObjectTypes.ElemRouteTemplateGuid, "Выберите элемент маршрута");
    return !Intermech.Consts.IsUndefinedObjectId(this._routeElementTemplateObjectId);
  }

  /// <summary>
  /// 
  /// </summary>
  protected override void DoProceedItems()
  {
    if (!this.SelectRouteElemObjects())
      return;
    base.DoProceedItems();
  }

  public RouteElemObjectsBaseCommand()
    : base()
  {
  }
}
