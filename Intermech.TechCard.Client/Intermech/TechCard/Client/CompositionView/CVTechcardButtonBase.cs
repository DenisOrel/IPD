// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.CompositionView.CVTechcardButtonBase
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.CompositionView;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.TechCard.Imbase;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.CompositionView;

/// <summary>Techcard base button</summary>
public class CVTechcardButtonBase : CVButtonBase
{
  /// <summary>
  /// Кэш с режимами создания для объектов Imbase
  /// Key - ид. версии объекта Imbase
  /// </summary>
  protected Dictionary<long, ImbaseObjCreateInfo> _imObjectInfoList;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ownerObjId"></param>
  /// <param name="dbTypedObjectIds"></param>
  /// <param name="session"></param>
  public override void DoBeforeAllCreation(
    IDBTypedObjectID ownerObjId,
    List<IDBTypedObjectID> dbTypedObjectIds,
    IUserSession session)
  {
    base.DoBeforeAllCreation(ownerObjId, dbTypedObjectIds, session);
    if (dbTypedObjectIds == null || dbTypedObjectIds.Count == 0)
      return;
    IImbaseTechObjInfoService service = ServiceUtils.GetService<IImbaseTechObjInfoService>((object) session, false);
    if (service == null)
      return;
    Dictionary<long, int> objects = new Dictionary<long, int>(dbTypedObjectIds.Count);
    foreach (IDBTypedObjectID dbTypedObjectId in dbTypedObjectIds)
    {
      if (dbTypedObjectId != null)
        objects.Add(dbTypedObjectId.ObjectID, -1);
    }
    if (service.GetCreationMode((IDictionary<long, int>) objects, session.SessionGUID, out this._imObjectInfoList))
      return;
    this._imObjectInfoList = (Dictionary<long, ImbaseObjCreateInfo>) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  public override void DoAfterAllCreation(IUserSession session)
  {
    base.DoAfterAllCreation(session);
    if (this._imObjectInfoList == null)
      return;
    this._imObjectInfoList.Clear();
    this._imObjectInfoList = (Dictionary<long, ImbaseObjCreateInfo>) null;
  }
}
