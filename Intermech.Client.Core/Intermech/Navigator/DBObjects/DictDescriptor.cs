
// Type: Intermech.Navigator.DBObjects.DictDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Дескриптор, позволяющий создавать описания узлов на основании типизированных коллекций идентификаторов версий объектов
/// </summary>
public class DictDescriptor : HiveDescriptor
{
  /// <summary>Флаг раскрытия дочерних узлов</summary>
  protected bool _expandNodes = true;
  /// <summary>Типизированные коллекции версий объектов</summary>
  protected Dictionary<int, List<long>> _objectIDs;

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public DictDescriptor(PersistentState state)
    : base(state)
  {
    string str1 = (string) state.GetValue("ObjectVersionIds");
    Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
    char[] chArray1 = new char[1]{ '|' };
    foreach (string str2 in str1.Split(chArray1))
    {
      char[] chArray2 = new char[1]{ '#' };
      string[] strArray = str2.Split(chArray2);
      int int32 = Convert.ToInt32(strArray[0]);
      List<long> list = ((IEnumerable<string>) strArray[1].Split('@')).Select<string, long>((Func<string, long>) (o => Convert.ToInt64(o))).ToList<long>();
      dictionary[int32] = list;
    }
    this._objectIDs = dictionary;
  }

  /// <summary>Создать экземпляр дескриптора</summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип (можно указать общий тип объектов)</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  public DictDescriptor(
    int categoryID,
    int typeID,
    string caption,
    Dictionary<int, List<long>> objectIDs)
    : base(categoryID, typeID, caption)
  {
    this._objectIDs = objectIDs;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new ListDescriptorNodeID(this._categoryID, this._typeID, this._caption);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new ObjectsDictNode(this._objectIDs, this._expandNodes);
  }

  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("ObjectVersionIds", (object) string.Join("|", this._objectIDs.Select<KeyValuePair<int, List<long>>, string>((Func<KeyValuePair<int, List<long>>, string>) (o => $"{o.Key}#{string.Join<long>("@", (IEnumerable<long>) o.Value)}"))));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (!(obj is DictDescriptor dictDescriptor))
      return base.Equals(obj);
    return this._categoryID == dictDescriptor._categoryID && this._typeID == dictDescriptor._typeID && dictDescriptor._objectIDs != null && this._objectIDs != null && dictDescriptor._objectIDs.Count == this._objectIDs.Count && ObjectsCompareHelper.CompareDictionaries((IDictionary) this._objectIDs, (IDictionary) dictDescriptor._objectIDs);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => this._categoryID ^ this._typeID;

  /// <summary>Флаг раскрытия дочерних узлов</summary>
  public bool ExpandNodes
  {
    get => this._expandNodes;
    set => this._expandNodes = value;
  }
}
