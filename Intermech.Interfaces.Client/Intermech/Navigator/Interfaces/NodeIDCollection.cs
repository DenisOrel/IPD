// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.NodeIDCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Коллекция унифицированных идентификаторов элементов пространства навигации.
/// При необходимости позволяет связывать с элементами коллекции ключи
/// </summary>
public class NodeIDCollection : List<INodeID>
{
  /// <summary>Словарь для поиска ключей по INodeID</summary>
  private Dictionary<INodeID, string> _keysByNodes;
  /// <summary>Словарь для поиска INodeID по ключам</summary>
  private Dictionary<string, INodeID> _nodesByKeys;
  /// <summary>Словарь для поиска индексов INodeID по ключам</summary>
  private Dictionary<string, int> _indexesByKeys;
  /// <summary>Словарь для поиска ключей по индексам</summary>
  private Dictionary<int, string> _keysByIndexes;
  /// <summary>Словарь для поиска индексов INodeID по узлам</summary>
  private Dictionary<INodeID, int> _indexesByNodes;

  /// <summary>Создать словари при необходимости</summary>
  protected virtual void CreateDictionaries()
  {
    if (this._keysByNodes == null)
      this._keysByNodes = new Dictionary<INodeID, string>();
    if (this._nodesByKeys == null)
      this._nodesByKeys = new Dictionary<string, INodeID>();
    if (this._indexesByKeys == null)
      this._indexesByKeys = new Dictionary<string, int>();
    if (this._keysByIndexes == null)
      this._keysByIndexes = new Dictionary<int, string>();
    if (this._indexesByNodes != null)
      return;
    this._indexesByNodes = new Dictionary<INodeID, int>();
  }

  /// <summary>Удалить словари при необходимости</summary>
  protected virtual void CheckDictionaries()
  {
    if (this.Count > 0)
      return;
    this._keysByNodes = (Dictionary<INodeID, string>) null;
    this._nodesByKeys = (Dictionary<string, INodeID>) null;
    this._indexesByKeys = (Dictionary<string, int>) null;
    this._keysByIndexes = (Dictionary<int, string>) null;
    this._indexesByNodes = (Dictionary<INodeID, int>) null;
  }

  /// <summary>
  /// Перестроить словари, содержащие индексы узлов и ключей
  /// </summary>
  protected virtual void SyncIdxDictionaries()
  {
    if (this._indexesByNodes == null || this._indexesByKeys == null)
      return;
    this._indexesByNodes.Clear();
    this._indexesByKeys.Clear();
    this._keysByIndexes.Clear();
    for (int index = 0; index < this.Count; ++index)
    {
      INodeID key = this[index];
      string keysByNode = this._keysByNodes[key];
      this._indexesByKeys[keysByNode] = index;
      this._indexesByNodes[key] = index;
      this._keysByIndexes[index] = keysByNode;
    }
  }

  /// <summary>
  /// Добавить пару значений [Элемент пространства навигации] - [его ключ] в коллекцию
  /// </summary>
  /// <param name="nodeID">Элемент пространства навигации</param>
  /// <param name="key">Ключ (String.Empty или null - ключи не используются)</param>
  public virtual void Add(INodeID nodeID, string key)
  {
    this.CheckItem(nodeID);
    this.CheckKey(key);
    base.Add(nodeID);
    if (string.IsNullOrEmpty(key))
      return;
    this.CreateDictionaries();
    this._keysByNodes[nodeID] = key;
    this._nodesByKeys[key] = nodeID;
    this._indexesByKeys[key] = this.Count - 1;
    this._keysByIndexes[this.Count - 1] = key;
    this._indexesByNodes[nodeID] = this.Count - 1;
  }

  /// <summary>
  /// Добавить новый элемент пространства навигации в коллекцию
  /// </summary>
  /// <param name="item">Добавляемый элемент пространства навигации в коллекцию</param>
  public new virtual void Add(INodeID item) => this.Add(item, (string) null);

  /// <summary>Добавить элементы из указанной коллекции в текущую</summary>
  /// <param name="collection">Добавляемые в текущую коллекцию элементы</param>
  public new void AddRange(IEnumerable<INodeID> collection)
  {
    if (collection == null)
      return;
    IEnumerator<INodeID> enumerator = collection.GetEnumerator();
    while (enumerator.MoveNext())
      this.Add(enumerator.Current);
  }

  /// <summary>Добавить элементы из указанной коллекции в текущую</summary>
  /// <param name="collection">Добавляемые в текущую коллекцию элементы</param>
  public void AddRange(IEnumerable<Tuple<INodeID, string>> collection)
  {
    if (collection == null)
      return;
    IEnumerator<Tuple<INodeID, string>> enumerator = collection.GetEnumerator();
    while (enumerator.MoveNext())
      this.Add(enumerator.Current.Item1, enumerator.Current.Item2);
  }

  /// <summary>
  /// Отыскать первое вхождение указанного элемента в коллекции
  /// </summary>
  /// <param name="item">Искомый элемент</param>
  /// <returns>Индекс или -1, если элемент не найден</returns>
  public new virtual int IndexOf(INodeID item)
  {
    int num = -1;
    return this._indexesByNodes != null && this._indexesByNodes.TryGetValue(item, out num) ? num : base.IndexOf(item);
  }

  /// <summary>
  /// Отыскать первое вхождение указанного ключа элемента в коллекции
  /// </summary>
  /// <param name="key">Искомый ключ элемента</param>
  /// <returns>Индекс или -1, если ключ элемента не найден</returns>
  public virtual int IndexOfKey(string key)
  {
    int num = -1;
    return this._indexesByKeys != null && this._indexesByKeys.TryGetValue(key, out num) ? num : DataSetProcessor.GetInt32Value((object) key, -1);
  }

  /// <summary>Найти ключ элемента с указанным индексом</summary>
  /// <param name="index">Индекс</param>
  /// <returns>Ключ или null</returns>
  public virtual string KeyOfIndex(int index)
  {
    string str = (string) null;
    if (this._keysByIndexes == null)
      return str;
    this._keysByIndexes.TryGetValue(index, out str);
    return str;
  }

  /// <summary>Проверить наличие указанного элемента в коллекции</summary>
  /// <param name="item">Искомый элемент</param>
  /// <returns>true - элемент найден в коллекции</returns>
  public new virtual bool Contains(INodeID item) => this.IndexOf(item) >= 0;

  /// <summary>Проверить наличие указанного ключа в коллекции</summary>
  /// <param name="key">искомый ключ</param>
  /// <returns>true - ключ найден в коллекции</returns>
  public virtual bool ContainsKey(string key) => this.IndexOfKey(key) >= 0;

  /// <summary>Удалить первый найденный указанный элемент из списка</summary>
  /// <param name="item">Удаляемый элемент</param>
  /// <returns>true - удаление выполнено успешно</returns>
  public new bool Remove(INodeID item)
  {
    int index = this.IndexOf(item);
    bool flag = index >= 0 && base.Remove(this[index]);
    this.CheckDictionaries();
    if (this._keysByNodes == null)
      return flag;
    string keysByNode = this._keysByNodes[item];
    this._keysByNodes.Remove(item);
    this._indexesByNodes.Remove(item);
    this._indexesByKeys.Remove(keysByNode);
    this._nodesByKeys.Remove(keysByNode);
    this.CheckDictionaries();
    if (this._keysByNodes == null)
      return flag;
    this.SyncIdxDictionaries();
    return flag;
  }

  /// <summary>Удалить все вхождения указанного элемента из списка</summary>
  /// <param name="item">Удаляемый элемент</param>
  /// <returns>Количество удалённых элементов</returns>
  public int RemoveAll(INodeID item)
  {
    int num = this.RemoveAll((Predicate<INodeID>) (collItem => collItem.Equals((object) item)));
    this.CheckDictionaries();
    this.SyncIdxDictionaries();
    return num;
  }

  /// <summary>
  /// Проверить значение указанного элемента на null.
  /// При ошибке будет сгенерировано исключение ArgumentNullException
  /// </summary>
  /// <param name="item">Проверяемый элемент</param>
  public virtual void CheckItem(INodeID item)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item), LocalizationHolder.rm.GetString("Interfaces.Client_144"));
  }

  /// <summary>
  /// Проверить соответствие указанного ключа общей политике экземпляра коллекции по работе с ключами.
  /// При ошибке будет сгенерировано исключение ArgumentException
  /// </summary>
  /// <param name="key">Проверяемый ключ</param>
  public virtual void CheckKey(string key)
  {
    if (this._nodesByKeys != null && string.IsNullOrEmpty(key) && this._nodesByKeys.Count > 0)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Client_146"), nameof (key));
    if (this._nodesByKeys == null && this.Count > 0 && !string.IsNullOrEmpty(key))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Interfaces.Client_145"), nameof (key));
  }
}
