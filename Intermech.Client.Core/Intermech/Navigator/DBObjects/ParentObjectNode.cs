
// Type: Intermech.Navigator.DBObjects.ParentObjectNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Элемент навигации.
/// В качестве папок возвращает объекты, в состав которых входит объект,
/// представленный данным элементом.
/// </summary>
[Obsolete]
public class ParentObjectNode : ObjectNode
{
  /// <summary>
  /// типы связей, которыми объект может входить в другие типы объектов
  /// </summary>
  private List<int> relTypes = new List<int>();
  /// <summary>
  /// id объекта, представленного данным элементом навигации
  /// </summary>
  private long id;

  /// <summary>Создать узел</summary>
  /// <param name="objTypeID">Тип</param>
  /// <param name="id">id объекта, представленного данным элементом навигации</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="relTypes">Типы связей, которыми объект может входить в другие типы объектов</param>
  public ParentObjectNode(int objTypeID, long id, long objID, List<int> relTypes)
    : base(objTypeID, objID)
  {
    this.id = id;
    this.relTypes = relTypes;
  }

  /// <summary>Создать список слотов-папок</summary>
  /// <returns>Список слотов-папок</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    if (this.relTypes == null || this.relTypes.Count == 0)
      return (List<PartSlot>) null;
    List<PartSlot> folderSlots = new List<PartSlot>();
    for (int index = 0; index < this.relTypes.Count; ++index)
    {
      INodePart folderPart = this.CreateFolderPart(this.relTypes[index]);
      if (folderPart != null)
        folderSlots.Add(new PartSlot(MetaDataHelper.GetRelationTypeGuid(this.relTypes[index]), folderPart));
    }
    return folderSlots;
  }

  protected override INodePart CreateFolderPart(int relTypeId)
  {
    return (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Applicability, relTypeId, this.Services);
  }
}
