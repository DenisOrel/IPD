
// Type: Intermech.Navigator.DBObjectTypes.Descriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Класс, предназначенный для описания элементов "Тип объекта базы данных" из
/// пространства навигации, включаемых в коллекцию дескрипторов элементов.
/// Такие коллекции используются для создания всевозможных виртуальных
/// элементов.
/// </summary>
public class Descriptor : ObjectTypesItems, IDescriptor, INodeItems, IPersistable
{
  protected int _objTypeID;
  private const string PropObjectType = "ObjType";

  /// <summary>Создает дескриптор.</summary>
  /// <param name="objTypeID"></param>
  public Descriptor(int objTypeID) => this._objTypeID = objTypeID;

  public int ObjectTypeID => this._objTypeID;

  /// <summary>Создать корневую ноду, для отображения типов объектов с переданными идентификаторами Может вернуть как составную ноду,
  /// содержащую в себе ноды типов объектов, так и просто ноду типа объекта, если идентификатор один
  /// 
  /// Если objectTypes == null то вернёт ноду со всеми типами объектов (AllObjectTypesDescriptor)</summary>
  /// <param name="objectTypes">Перечисление идентификаторов типов объектов</param>
  /// <param name="rootNodeCaption">Заголовок ноды, объединяющей типы объектов. Если null или пустой, то будет использовано стандартное значение "Типы объектов"</param>
  public static IDescriptor CreateComposition(IEnumerable<int> objectTypes, string rootNodeCaption = null)
  {
    if (objectTypes == null)
      return (IDescriptor) new AllObjectTypesDescriptor();
    DescriptorCollection descriptors = new DescriptorCollection(objectTypes.Select<int, IDescriptor>((Func<int, IDescriptor>) (objectTypeID => (IDescriptor) new Descriptor(objectTypeID))));
    return descriptors.Count <= 1 ? descriptors[0] : (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, ((Func<IEnumerable<int>, int>) (childObjectTypes =>
    {
      int parentObjectTypeId = MetaDataHelper.GetCommonParentObjectTypeID(childObjectTypes);
      return parentObjectTypeId == -1 ? 0 : parentObjectTypeId;
    }))(objectTypes), !string.IsNullOrWhiteSpace(rootNodeCaption) ? rootNodeCaption : LocalizationHolder.rm.GetString("Client.Core_1608"), descriptors);
  }

  /// <summary>Создать последовательность дескрипторов типов объектов из последовательности идентификаторов типов объектов</summary>
  public static IEnumerable<IDescriptor> CreateMany(IEnumerable<int> objectTypes)
  {
    if (objectTypes != null)
    {
      foreach (int objectType in objectTypes)
        yield return (IDescriptor) new Descriptor(objectType);
    }
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected Descriptor(PersistentState state)
  {
    object obj = state.GetValue("ObjType");
    if (obj != null && obj is int num)
      this._objTypeID = num;
    else
      this._objTypeID = -1;
  }

  public override bool Equals(object obj)
  {
    return !(obj is Descriptor descriptor) ? base.Equals(obj) : this._objTypeID == descriptor._objTypeID;
  }

  public override int GetHashCode() => this._objTypeID;

  public object MapColumnToField(NodeColumn column) => Helper.MapColumnToField(column);

  public INodeID GetRecordNodeID()
  {
    if (this._objTypeID == -1)
      return (INodeID) new Intermech.Navigator.DBObjectTypes.Implementation.NodeID(this._objTypeID, AccessRights.NotDefined);
    IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(this._objTypeID, false);
    return objectType == null ? (INodeID) new VirtualObjectNodeID(LocalizationHolder.rm.GetString("Client.Core_1366")) : (INodeID) new Intermech.Navigator.DBObjectTypes.Implementation.NodeID(objectType.ObjectType, AccessRights.NotDefined);
  }

  public object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    object[] recordValues = new object[fields.Length];
    for (int index = 0; index < recordValues.Length; ++index)
    {
      if (fields[index].Equals((object) "F_OBJ_TYPE_NAME"))
        recordValues[index] = (object) MetaDataHelper.GetObjectTypeName(nodeID.TypeID);
    }
    return recordValues;
  }

  public void GetObjectData(PersistentState state)
  {
    state.AddValue("ObjType", (object) this._objTypeID);
  }
}
