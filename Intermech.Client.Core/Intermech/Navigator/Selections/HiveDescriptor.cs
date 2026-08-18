
// Type: Intermech.Navigator.Selections.HiveDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.Selections;

/// <summary>
/// Класс, предназначенный для описания элементов "Выборки" и "Классификаторы" из
/// пространства навигации, включаемых в коллекцию дескрипторов элементов. С
/// помощью такого дескриптора соответсвующие элементы навигации получают
/// информацию о контексте работы.
/// </summary>
public class HiveDescriptor : IDescriptor, INodeItems
{
  private int _selTypeID;
  private ITopBinding _binding;
  private const string PropTypeID = "TypeId";

  /// <summary>
  /// Конструктор дескриптора, позволяющий указать тип дерева выборок (т.е.
  /// "Выборки", "Классификаторы" и т.д.) и  информацию о привязке к
  /// родительскому элементу.
  /// </summary>
  /// <param name="selTypeID">
  /// Идентификатор типа объектов базы данных, на основе которых будет построено
  /// дерево выборок.
  /// </param>
  /// <param name="binding">Информация о привязке к родительскому элементу.</param>
  public HiveDescriptor(int selTypeID, ITopBinding binding)
  {
    this._selTypeID = selTypeID;
    this._binding = binding;
  }

  public object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_CAPTION") || column.SchemeGuid == Intermech.Navigator.Consts.NameColumnSchemeGuid ? column.ID : (object) null;
  }

  public INodeID GetRecordNodeID()
  {
    return (INodeID) new HiveNodeID(Intermech.Navigator.Consts.CategorySelectionsNode, this._selTypeID);
  }

  public object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    object[] recordValues = new object[fields.Length];
    for (int index = 0; index < recordValues.Length; ++index)
    {
      if (fields[index].Equals((object) "F_CAPTION"))
        recordValues[index] = (object) this._binding.GetCaption(nodeID.TypeID);
    }
    return recordValues;
  }

  public ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.HasChildren;

  public virtual INode GetChild(INodeID nodeID)
  {
    return ((INodesFactory) ServicesManager.GetService(typeof (IFactory))).GetNode(nodeID, (object) nodeID.TypeID, (object) this._binding, null);
  }

  public string GetAddress(INodeID nodeID) => this._binding.GetCaption(nodeID.TypeID);

  public INodeID ParseAddress(string address)
  {
    return !(address == this._binding.GetCaption(this._selTypeID)) ? (INodeID) null : this.GetRecordNodeID();
  }

  public PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("TypeId", (object) nodeID.TypeID);
    return persistentState;
  }

  public INodeID Deserialize(PersistentState persistNodeID)
  {
    int typeID = (int) persistNodeID.GetValue("TypeId");
    return (INodeID) new HiveNodeID(Intermech.Navigator.Consts.CategorySelectionsNode, typeID);
  }

  public object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (ITopBinding))
      return (object) this._binding;
    return dataFormat == typeof (IBinding) ? (object) this._binding : this._binding.GetData(dataFormat);
  }

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    for (int index = 0; index < data.Length; ++index)
      data[index] = this.GetData(nodeIDs[index], dataFormat);
    return data;
  }

  public IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return (IUpdateAnalyser) null;
  }

  public object GetService(Type service) => (object) null;
}
