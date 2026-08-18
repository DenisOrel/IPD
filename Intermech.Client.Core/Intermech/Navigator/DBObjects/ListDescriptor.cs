
// Type: Intermech.Navigator.DBObjects.ListDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Дескриптор, позволяющий создавать описания узлов на основании коллекции идентификаторов объектов
/// </summary>
public class ListDescriptor : HiveDescriptor
{
  /// <summary>Список идентификаторов объектов</summary>
  protected IList _objectIDs;

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public ListDescriptor(PersistentState state)
    : base(state)
  {
    this._objectIDs = (IList) new List<long>(((IEnumerable<string>) ((string) state.GetValue("ObjectVersionIds")).Split('|')).Select<string, long>((Func<string, long>) (o => Convert.ToInt64(o))));
  }

  /// <summary>Создать экземпляр дескриптора</summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип (можно указать общий тип объектов)</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  public ListDescriptor(int categoryID, int typeID, string caption, IList objectIDs)
    : base(categoryID, typeID, caption)
  {
    this._objectIDs = objectIDs;
  }

  public IList ObjectIDs => this._objectIDs;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new ListDescriptorNodeID(this._categoryID, this._typeID, this._caption);
  }

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new ObjectsListNode(this._objectIDs, this._typeID);
  }

  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("ObjectVersionIds", (object) string.Join<long>("|", this._objectIDs.Cast<long>()));
  }

  public override bool Equals(object obj)
  {
    if (!(obj is ListDescriptor listDescriptor))
      return base.Equals(obj);
    if (this._categoryID != listDescriptor._categoryID || this._typeID != listDescriptor._typeID || listDescriptor._objectIDs == null || this._objectIDs == null || listDescriptor._objectIDs.Count != this._objectIDs.Count)
      return false;
    bool flag = true;
    for (int index = 0; index < this._objectIDs.Count; ++index)
    {
      flag = this._objectIDs[index].Equals(listDescriptor._objectIDs[index]) & flag;
      if (!flag)
        return false;
    }
    return flag;
  }

  public override int GetHashCode()
  {
    int hashCode = this._categoryID ^ this._typeID;
    if (this._objectIDs != null)
    {
      for (int index = 0; index < this._objectIDs.Count; ++index)
      {
        if (this._objectIDs[index] != null)
          hashCode ^= this._objectIDs[index].GetHashCode();
      }
    }
    return hashCode;
  }
}
