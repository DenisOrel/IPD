// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.Commands.RouteElemObjectsInsertCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.TechCard.Client.Commands.Edit;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.CehRoute.Element.Commands;

internal class RouteElemObjectsInsertCommand : RouteElemObjectsBaseCommand
{
  /// <summary>Режим добавления</summary>
  private readonly CompositionTargetMode _targetMode;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="targetMode"></param>
  public RouteElemObjectsInsertCommand(CompositionTargetMode targetMode)
  {
    this._targetMode = targetMode;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbObject"></param>
  /// <param name="index"></param>
  /// <returns></returns>
  protected override BaseCommandResult DoEditCommand(IDBObject dbObject, int index)
  {
    ICompositionsAutomaticSortingService service = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) dbObject.Session, true);
    IDBObject dbObject1 = dbObject.Session.GetObjectCollection(TechCardConsts.ObjectTypes.ElemRouteID).Create(this._routeElementTemplateObjectId);
    IDBRelation dbRelation = dbObject.Session.GetRelationCollection(TechCardConsts.RelTypes.TechRelationID).Create(new NewRelationProperties(0L, dbObject.ObjectID, dbObject1.ID)
    {
      PartObjectID = dbObject1.ObjectID
    });
    ICompositionsAutomaticSortingSession session = service.CreateSession((object) dbObject.Session.SessionGUID);
    try
    {
      session.ProceedRelation((IEnumerable<long>) new long[1]
      {
        dbRelation.RelationID
      }, this._targetMode, 0L, (object) dbObject.Session.SessionGUID);
    }
    finally
    {
      service.DisposeSession((object) dbObject.Session.SessionGUID);
    }
    dbObject1.CommitCreation(true);
    return BaseCommandResult.OK;
  }
}
