
// Type: Intermech.Navigator.VirtualNodes.HiveDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Diagnostics;


namespace Intermech.Navigator.VirtualNodes;

public class HiveDescriptor : IDescriptor, INodeItems, IPersistable
{
  protected int _categoryID;
  protected int _typeID;
  protected string _caption;
  protected const string PropCategoryGuid = "Category";
  protected const string PropTypeID = "Type";
  protected const string PropCaption = "Caption";

  /// <summary>Создает дескриптор виртуального элемента навигации.</summary>
  /// <param name="categoryID"></param>
  /// <param name="typeID"></param>
  /// <param name="caption"></param>
  public HiveDescriptor(int categoryID, int typeID, string caption)
  {
    this._categoryID = categoryID;
    this._typeID = typeID;
    this._caption = caption;
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public HiveDescriptor(PersistentState state)
  {
    this._categoryID = ((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[(Guid) state.GetValue("Category")];
    this._typeID = (int) state.GetValue("Type");
    this._caption = (string) state.GetValue(nameof (Caption));
  }

  /// <summary>Идентификатор категории сущности, представляемой дескриптором</summary>
  public virtual int CategoryID
  {
    [DebuggerStepThrough] get => this._categoryID;
  }

  /// <summary>Идентификатор типа сущности представляемой дескриптором, в рамках категории</summary>
  public virtual int TypeID => this._typeID;

  /// <summary>Заголовок сущности</summary>
  public virtual string Caption => this._caption;

  /// <summary>Отразить указанную колонку в идентификатор атрибута</summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Идентификатор атрибута</returns>
  public virtual object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.NameColumnSchemeGuid || object.Equals(column.ID, (object) ObligatoryObjectAttributes.CAPTION) || object.Equals(column.ID, (object) -50) ? column.ID : (object) null;
  }

  /// <summary>
  /// Вернуть описание корневого узла для текущего дескриптора
  /// </summary>
  /// <returns></returns>
  public virtual INodeID GetRecordNodeID()
  {
    return (INodeID) new HiveNodeID(this._categoryID, this._typeID);
  }

  /// <summary>Вернуть массив данных для указанного описания узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="fields">Поля, загруженные из базы данных</param>
  /// <returns>массив данных для указанного описания узла</returns>
  public virtual object[] GetRecordValues(INodeID nodeID, object[] fields)
  {
    object[] recordValues = new object[fields.Length];
    for (int index = 0; index < recordValues.Length; ++index)
    {
      if (fields[index].Equals((object) "F_CAPTION") || fields[index].Equals((object) ObligatoryObjectAttributes.CAPTION) || fields[index].Equals((object) -50))
        recordValues[index] = (object) this._caption;
    }
    return recordValues;
  }

  /// <summary>Вернуть атрибуты указанного описания узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <returns>Атрибуты указанного описания узла</returns>
  public virtual ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.HasChildren;

  /// <summary>Вернуть дочерний узел согласно его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  public virtual INode GetChild(INodeID nodeID)
  {
    return ((INodesFactory) ServicesManager.GetService(typeof (IFactory))).GetNode(this._categoryID, this._typeID);
  }

  /// <summary>Вернуть адрес по описанию узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <returns>Адрес узла</returns>
  public virtual string GetAddress(INodeID nodeID) => this._caption;

  public virtual INodeID ParseAddress(string address)
  {
    return !(address == this._caption) ? (INodeID) null : this.GetRecordNodeID();
  }

  public virtual PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("Category", (object) ((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[this._categoryID]);
    persistentState.AddValue("Type", (object) this._typeID);
    return persistentState;
  }

  public virtual INodeID Deserialize(PersistentState persistNodeID)
  {
    return (INodeID) new HiveNodeID(((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[(Guid) persistNodeID.GetValue("Category")], (int) persistNodeID.GetValue("Type"));
  }

  /// <summary>
  /// Вернуть данные определённого формата по указанному описанию узла
  /// </summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные определённого формата по указанному описанию узла</returns>
  public virtual object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (IDescriptor) ? (object) new HiveDescriptor(this._categoryID, this._typeID, this._caption) : (object) null;
  }

  /// <summary>
  /// Вернуть данные определённого формата из коллекции описаний узлов
  /// </summary>
  /// <param name="nodeIDs">Коллекция описаний узлов</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные определённого формата из коллекции описаний узлов</returns>
  public virtual object[] GetData(NodeIDCollection nodeIDs, Type dataFormat) => (object[]) null;

  /// <summary>Вернуть анализатор для обработки события</summary>
  /// <param name="capabilities">Возможности узла</param>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы события</param>
  /// <returns>Анализатор для обработки события</returns>
  public virtual IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return (IUpdateAnalyser) null;
  }

  public object GetService(Type service) => (object) null;

  public virtual void GetObjectData(PersistentState state)
  {
    IGuidMapper service = (IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper));
    state.AddValue("Category", (object) service[this._categoryID]);
    state.AddValue("Type", (object) this._typeID);
    state.AddValue("Caption", (object) this._caption);
  }

  /// <summary>Сравнить дескриптор с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is HiveDescriptor hiveDescriptor))
      return base.Equals(obj);
    return this._categoryID == hiveDescriptor._categoryID && this._typeID == hiveDescriptor._typeID;
  }

  public override int GetHashCode() => this._categoryID ^ this._typeID;
}
