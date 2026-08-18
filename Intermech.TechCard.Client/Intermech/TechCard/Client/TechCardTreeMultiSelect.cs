// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TechCardTreeMultiSelect
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>
/// 
/// </summary>
public class TechCardTreeMultiSelect : IEnableTreeMultiSelect, IEnableTreeColumnsSorting
{
  /// <summary>Получение типа объекта для дескриптора</summary>
  /// <param name="rootDescriptor"></param>
  /// <param name="rootObjectTypeId"></param>
  /// <returns></returns>
  private bool GetRootObjectTypeId(IDescriptor rootDescriptor, out int rootObjectTypeId)
  {
    rootObjectTypeId = -1;
    INodeID recordNodeId = rootDescriptor?.GetRecordNodeID();
    if (recordNodeId == null)
      return false;
    if (recordNodeId.CategoryID == 1)
    {
      rootObjectTypeId = recordNodeId.TypeID;
      return true;
    }
    if (rootDescriptor.GetData(recordNodeId, typeof (IDBTypedObjectID)) is IDBTypedObjectID data1)
    {
      rootObjectTypeId = data1.ObjectType;
      return true;
    }
    if (rootDescriptor.GetData(recordNodeId, typeof (IDBObjectID)) is IDBObjectID data3)
    {
      if (rootDescriptor.GetData(recordNodeId, typeof (IDBObjectTypeID)) is IDBObjectTypeID data2)
      {
        rootObjectTypeId = data2.Value;
        return true;
      }
      QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(data3.Value);
      if (!objectInfo.Empty)
      {
        rootObjectTypeId = objectInfo.ObjectTypeID;
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// 
  /// </summary>
  public Guid Guid => new Guid("{7B3A3DEC-70C3-4AFD-AEFD-B26C21A1CC8E}");

  /// <summary>
  /// 
  /// </summary>
  /// <param name="rootDescriptor"></param>
  /// <param name="viewServices"></param>
  /// <returns></returns>
  public YesNoUnknownEnum EnableTreeMultiSelect(
    IDescriptor rootDescriptor,
    IServiceProvider viewServices)
  {
    int rootObjectTypeId;
    if (!this.GetRootObjectTypeId(rootDescriptor, out rootObjectTypeId))
      return YesNoUnknownEnum.Unknown;
    return !TechCardConsts.Utils.IsTechcardObjectType((object) rootObjectTypeId) ? YesNoUnknownEnum.No : YesNoUnknownEnum.Yes;
  }

  /// <summary>
  /// Выполнить проверку, можно ли разрешать сортировку в колонках в дереве "Навигатора",
  /// которое построено на основании указанного дескриптора корневого узла
  /// </summary>
  /// <param name="rootDescriptor">Дескриптор корневого узла дерева</param>
  /// <param name="viewServices">Контейнер сервисов для дерева</param>
  /// <returns>Сортировка в колонках разрешена, не разрешена, дескриптор не распознан</returns>
  public YesNoUnknownEnum EnableTreeColumnsSorting(
    IDescriptor rootDescriptor,
    IServiceProvider viewServices)
  {
    int rootObjectTypeId;
    return !this.GetRootObjectTypeId(rootDescriptor, out rootObjectTypeId) || !TechCardConsts.Utils.IsTechcardObjectType((object) rootObjectTypeId) ? YesNoUnknownEnum.Unknown : YesNoUnknownEnum.No;
  }
}
