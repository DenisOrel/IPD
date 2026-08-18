// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech.DraftCadmDeleteItemsCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.TechCard.Client.Commands;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.Draft.Cadmech;

/// <summary>Реализация команды "Удалить" для эскизов Cadmech</summary>
internal class DraftCadmDeleteItemsCommand : DeleteCommand
{
  /// <summary>
  /// 
  /// </summary>
  protected override bool GetDeletingObjects()
  {
    if (!base.GetDeletingObjects())
      return false;
    Dictionary<DeletingObject, bool> dictionary = new Dictionary<DeletingObject, bool>();
    for (int index = 0; index < this.Items.Count; ++index)
    {
      if (this.CouldDeleteItemRelation(index))
      {
        IDBRelationID dbRelationId = !(this.Items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData) || itemData.Value == 0L || itemData.Value == -1L ? (IDBRelationID) null : itemData;
        if (dbRelationId != null && dbRelationId.RelationType != TechCardConsts.RelTypes.TechDraftRelationID)
        {
          DeletingObject deletingObjectFromRoot = this._deletingObjects.FindDeletingObjectFromRoot(dbRelationId.PartID);
          if (deletingObjectFromRoot != null)
            dictionary[deletingObjectFromRoot] = true;
        }
      }
    }
    foreach (DeletingObject deletingObject in (List<DeletingObject>) this._deletingObjects)
    {
      int count = deletingObject.PrjLinkIDs.Count;
      if (!dictionary.ContainsKey(deletingObject))
        deletingObject.ObjectID = 0L;
    }
    return true;
  }

  public DraftCadmDeleteItemsCommand()
    : base()
  {
  }
}
