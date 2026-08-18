
// Type: Intermech.Navigator.DBObjectTypes.AllObjectTypesDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Дескриптор, создающий корень дерева всех типов объектов - узел "Все типы объектов"
/// </summary>
public class AllObjectTypesDescriptor : HiveDescriptor
{
  /// <summary>Заголовок - "Все типы объектов"</summary>
  public static string Caption = LocalizationHolder.rm.GetString("Client.Core_1368");

  /// <summary>Создает дескриптор корня дерева типов объектов</summary>
  public AllObjectTypesDescriptor()
    : base(Intermech.Navigator.Consts.CategoryAllObjectTypes, 0, AllObjectTypesDescriptor.Caption)
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected AllObjectTypesDescriptor(PersistentState state)
    : this()
  {
  }

  /// <summary>Выполняет сериализацию дескриптора.</summary>
  /// <param name="state"></param>
  public override void GetObjectData(PersistentState state)
  {
  }

  /// <summary>Создать идентификатор корневого узла "Все объекты"</summary>
  /// <returns>Идентификатор корневого узла "Все объекты"</returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new AllObjectTypesNodeID(this._categoryID, this._typeID);
  }

  /// <summary>Вернуть обработчик для указанного идентификатора узла</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <returns>Обработчик</returns>
  public override INode GetChild(INodeID nodeID)
  {
    AllObjectTypesNodeID objectTypesNodeId = nodeID as AllObjectTypesNodeID;
    IFactory service = (IFactory) ServicesManager.GetService(typeof (IFactory));
    return objectTypesNodeId != null ? service.GetNode(nodeID) : base.GetChild(nodeID);
  }

  /// <summary>Вернуть данные для узла</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="dataFormat">Запрашиваемый тип данных</param>
  /// <returns>Данные или null</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new AllObjectTypesDescriptor();
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }
}
