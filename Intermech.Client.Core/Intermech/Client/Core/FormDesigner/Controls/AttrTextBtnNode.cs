
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrTextBtnNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
internal class AttrTextBtnNode : ObjectTypeNode
{
  private int _childObjTypeID = -1;
  private long _objID;
  private ConditionStructure[] _conditions;
  private static readonly Guid PartGuid = new Guid("6D2C9DC6-6A07-4617-8457-ACD1DF558351");

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objTypeID"></param>
  /// <param name="accessRights"></param>
  /// <param name="objID"></param>
  /// <param name="selectionGuid"></param>
  public AttrTextBtnNode(
    int objTypeID,
    AccessRights accessRights,
    long objID,
    ConditionStructure[] conditions)
    : base(objTypeID, accessRights)
  {
    this._childObjTypeID = objTypeID;
    this._objID = objID;
    this._conditions = conditions;
  }

  /// <summary>Вернуть слоты-не-папки</summary>
  /// <returns>Слоты-не-папки</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    List<PartSlot> nonFolderSlots = new List<PartSlot>(1);
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this._objID);
    ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    List<Guid> guidList = (List<Guid>) null;
    if (service != null)
      guidList = service.Rule.GetObjectTypeVisibleRelationsGuids(objectInfo.ObjectTypeID, true);
    if (guidList == null)
      guidList = new List<Guid>(1);
    if (guidList.Count == 0)
    {
      Guid relationTypeGuid = MetaDataHelper.GetDefaultRelationTypeGuid(objectInfo.ObjectTypeID);
      if (relationTypeGuid != Guid.Empty)
        guidList.Add(relationTypeGuid);
    }
    if (guidList.Count > 0)
    {
      foreach (Guid relTypeGuid in guidList)
      {
        int relationTypeId = MetaDataHelper.GetRelationTypeID(relTypeGuid);
        nonFolderSlots.Add(new PartSlot(AttrTextBtnNode.PartGuid, (INodePart) new AttrTextBtnPart(objectInfo.ObjectTypeID, this._objID, this._childObjTypeID, relationTypeId, this.Services, this._conditions)));
      }
    }
    return nonFolderSlots;
  }
}
