// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmCriterionsCollection
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Коллекция критериев IPdmCriterion в конфигураторе составов IPS
/// </summary>
[Serializable]
public class PdmCriterionsCollection : 
  IPdmCriterion,
  IAssignable,
  ICloneable,
  IEvaluator,
  IXMLStorageLoadSave,
  IList<IPdmCriterion>,
  ICollection<IPdmCriterion>,
  IEnumerable<IPdmCriterion>,
  IEnumerable,
  IStoreable
{
  /// <summary>Контейнер, которому принадлежит коллекция</summary>
  [NonSerialized]
  public object Holder;
  /// <summary>Объект для синхронизации</summary>
  protected object syncRoot = new object();
  /// <summary>
  /// Коллекция критериев условия применения объекта в конфигураторе составов IPS
  /// </summary>
  protected List<IPdmCriterion> _items = new List<IPdmCriterion>();
  /// <summary>
  /// Логическая функция для объединения данной коллекции со следующим критерием/коллекцией
  /// </summary>
  protected LogicalFunction _function;
  /// <summary>Результат последнего вычисления</summary>
  protected TraceEntry _evaluateTrace = new TraceEntry();

  /// <summary>Логическая функция по умолчанию</summary>
  public virtual LogicalFunction DefaultFunction
  {
    [DebuggerStepThrough] get => LogicalFunction.And;
  }

  /// <summary>
  /// Идентификатор атрибута, в котором хранится содержимое данной коллекции
  /// </summary>
  public virtual int LoadSaveAttributeID
  {
    [DebuggerStepThrough] get => Consts.attributeObjectApplicabilityCondID;
  }

  /// <summary>Количество критериев</summary>
  public virtual int Count
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._items.Count;
    }
  }

  /// <summary>Управление элементами коллекции значений опции</summary>
  /// <param name="index">Индекс элемента коллекции</param>
  /// <returns>Элемент коллекции с указанным индексом</returns>
  public virtual IPdmCriterion this[int index]
  {
    get
    {
      lock (this.syncRoot)
        return this._items[index];
    }
    set => this.Replace(value, index);
  }

  /// <summary>
  /// Создать пустую коллекцию критериев условия применения объекта в конфигураторе составов IPS
  /// </summary>
  public PdmCriterionsCollection()
  {
  }

  /// <summary>
  /// Создать пустую коллекцию критериев условия применения объекта в конфигураторе составов IPS
  /// </summary>
  /// <param name="function">Логическая функция для объединения данной коллекции со следующим критерием/коллекцией</param>
  public PdmCriterionsCollection(LogicalFunction function) => this._function = function;

  /// <summary>
  /// Создать коллекцию критериев условия применения объекта в конфигураторе составов IPS на основе указанного объекта
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public PdmCriterionsCollection(object source) => this.Assign(source);

  /// <summary>Очистить поля класса</summary>
  public virtual void Clear()
  {
    lock (this.syncRoot)
    {
      this._items.Clear();
      this._function = this.DefaultFunction;
    }
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public virtual void Assign(object source)
  {
    this.Clear();
    switch (source)
    {
      case string _:
        string str = (string) source;
        if (str.Length == 0)
          break;
        XMLSettingsStorage xmlStorage = new XMLSettingsStorage();
        xmlStorage.document.InnerXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><IPS />";
        if (!(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper serviceInstance))
        {
          serviceInstance = new PdmGuidMapper();
          xmlStorage.Services.AddService(typeof (PdmGuidMapper), (object) serviceInstance);
        }
        try
        {
          if (str.IndexOf("<?xml version=\"1.0\" encoding=\"utf-8\"?>") < 0)
            xmlStorage.document.InnerXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + str;
          else
            xmlStorage.document.InnerXml = str;
        }
        catch
        {
          break;
        }
        serviceInstance.Load(xmlStorage, (XmlNode) xmlStorage.document.DocumentElement);
        this.Load(xmlStorage, xmlStorage.FindNode((XmlNode) xmlStorage.document.DocumentElement, "d", false));
        break;
      case PdmCriterionsCollection criterionsCollection:
        lock (this.syncRoot)
        {
          this._function = criterionsCollection.Function;
          this.Not = criterionsCollection.Not;
        }
        for (int index = 0; index < criterionsCollection.Count; ++index)
          this.Add(criterionsCollection[index].Clone() as IPdmCriterion);
        break;
      case IDBAttributable dbAttributable:
        StringBuilder stringBuilder = new StringBuilder();
        IDBAttribute attributeById = dbAttributable.GetAttributeByID(this.LoadSaveAttributeID);
        if (attributeById != null)
        {
          if (attributeById.ValuesCount == 1)
          {
            stringBuilder.Append(DataSetProcessor.GetStringValue(attributeById.Value, string.Empty));
          }
          else
          {
            object[] values = attributeById.Values;
            if (values != null)
            {
              for (int index = 0; index < values.Length; ++index)
                stringBuilder.Append(DataSetProcessor.GetStringValue(values[index], string.Empty));
            }
          }
        }
        this.Assign((object) stringBuilder.ToString());
        break;
    }
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public virtual object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

  /// <summary>Добавить критерий в коллекцию</summary>
  /// <param name="item">Добавляемый критерий</param>
  public virtual void Add(IPdmCriterion item)
  {
    lock (this.syncRoot)
      this._items.Add(item);
  }

  /// <summary>Добавить коллекцию критериев в коллекцию</summary>
  /// <param name="collection">Добавляемая коллекция</param>
  public virtual void AddRange(IList<IPdmCriterion> collection)
  {
    lock (this.syncRoot)
      this._items.AddRange((IEnumerable<IPdmCriterion>) collection);
  }

  /// <summary>Вернуть индекс указанного значения</summary>
  /// <param name="item">Искомое значение</param>
  /// <returns>-1 или индекс найденного значения</returns>
  public virtual int IndexOf(IPdmCriterion item)
  {
    lock (this.syncRoot)
      return this._items.IndexOf(item);
  }

  /// <summary>Вставить элемент в коллекцию</summary>
  /// <param name="index">Порядковый номер, под которым следует разместить добавляемый элемент</param>
  /// <param name="item">Добавляемый элемент</param>
  public virtual void Insert(int index, IPdmCriterion item)
  {
    lock (this.syncRoot)
      this._items.Insert(index, item);
  }

  /// <summary>Заменить элемент с указанным индексом</summary>
  /// <param name="item">Новое значение</param>
  /// <param name="index">Индекс заменяемого значения</param>
  public virtual void Replace(IPdmCriterion item, int index)
  {
    lock (this.syncRoot)
      this._items[index] = item;
  }

  /// <summary>Удалить из коллекции указанный элемент</summary>
  /// <param name="item">Удаляемый элемент</param>
  public virtual void Remove(IPdmCriterion item)
  {
    lock (this.syncRoot)
      this._items.Remove(item);
  }

  /// <summary>Удалить из коллекции элемент с указанным индексом</summary>
  /// <param name="index">Индекс удаляемого элемента</param>
  public virtual void RemoveAt(int index)
  {
    lock (this.syncRoot)
      this._items.RemoveAt(index);
  }

  /// <summary>Выполнить сортировку элементов списка</summary>
  public virtual void Sort()
  {
    lock (this.syncRoot)
      this._items.Sort();
  }

  /// <summary>Выполнить сортировку элементов списка</summary>
  /// <param name="comparison">Способ сортировки</param>
  public virtual void Sort(Comparison<IPdmCriterion> comparison)
  {
    lock (this.syncRoot)
      this._items.Sort(comparison);
  }

  /// <summary>Получить массив значений опции</summary>
  /// <returns>Массив значений опции</returns>
  public virtual IPdmCriterion[] ToArray()
  {
    lock (this.syncRoot)
      return this._items.ToArray();
  }

  /// <summary>
  /// Вернуть тип данных элементов коллекции (классов, которые реализуют узлы-критерии)
  /// </summary>
  /// <returns>Тип данных элементов коллекции (классов, которые реализуют узлы-критерии)</returns>
  public virtual Type GetElementType() => typeof (ObjectsApplicabilitiesCriterion);

  /// <summary>Проверить наличие указанного элемента в коллекции</summary>
  /// <param name="item">Искомый элемент</param>
  /// <returns>true - элемент найден в коллекции</returns>
  public virtual bool Contains(IPdmCriterion item)
  {
    lock (this.syncRoot)
      return this._items.Contains(item);
  }

  /// <summary>Скопировать значения коллекции в указанный массив</summary>
  /// <param name="array">Массив-назначение</param>
  /// <param name="arrayIndex">Стартовый индекс в массиве</param>
  public virtual void CopyTo(IPdmCriterion[] array, int arrayIndex)
  {
    lock (this.syncRoot)
      this._items.CopyTo(array, arrayIndex);
  }

  /// <summary>Является ли коллекция заблокированной от изменений</summary>
  public virtual bool IsReadOnly => false;

  /// <summary>Удалить элемент из коллекции</summary>
  /// <param name="item">Удаляемый элемент</param>
  /// <returns>true - удаление успешно выполнено</returns>
  bool ICollection<IPdmCriterion>.Remove(IPdmCriterion item)
  {
    lock (this.syncRoot)
      return this._items.Remove(item);
  }

  /// <summary>Получить перечислитель элементов коллекции</summary>
  /// <returns>Перечислитель элементов коллекции</returns>
  public virtual IEnumerator<IPdmCriterion> GetEnumerator()
  {
    lock (this.syncRoot)
      return (IEnumerator<IPdmCriterion>) this._items.GetEnumerator();
  }

  /// <summary>Получить перечислитель элементов коллекции</summary>
  /// <returns>Перечислитель элементов коллекции</returns>
  IEnumerator IEnumerable.GetEnumerator()
  {
    lock (this.syncRoot)
      return (IEnumerator) this._items.GetEnumerator();
  }

  /// <summary>
  /// Значение по умолчанию, если вычисление не может быть выполнено или не требуется
  /// </summary>
  public virtual PdmConfiguratorResult DefaultEvaluatorValue
  {
    [DebuggerStepThrough] get => PdmConfiguratorResult.True;
  }

  /// <summary>
  /// Логическая функция для объединения данной коллекции со следующим критерием/коллекцией
  /// </summary>
  public virtual LogicalFunction Function
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this._function;
    }
    set
    {
      lock (this.syncRoot)
        this._function = value;
    }
  }

  public bool Not { get; set; }

  /// <summary>Выполнить вычисление значений критериев из коллекции</summary>
  /// <param name="context">Контекст конфигуратора составов IPS</param>
  /// <returns>Результат вычисления значений критериев,
  /// исключение, если значение опции/критерия не найдено в контексте, либо принадлежат разным опциям</returns>
  public virtual PdmConfiguratorResult Evalute(PdmConfiguratorContext context)
  {
    PdmConfiguratorResult configuratorResult1 = this.DefaultEvaluatorValue;
    lock (this.syncRoot)
    {
      if (this.Count == 0)
        return configuratorResult1;
      LogicalFunction func = this.DefaultFunction;
      for (int index = 0; index < this.Count; ++index)
      {
        IEvaluator evaluator = (IEvaluator) this[index];
        PdmConfiguratorResult configuratorResult2 = evaluator.Evalute(context);
        this.EvaluateTrace.Assign((object) evaluator.EvaluateTrace);
        if (configuratorResult2 > PdmConfiguratorResult.True)
          return configuratorResult2;
        configuratorResult1 = index > 0 ? Helper.Combine(configuratorResult1, configuratorResult2, func) : configuratorResult2;
        func = evaluator.Function;
      }
    }
    if (this.Not)
    {
      switch (configuratorResult1)
      {
        case PdmConfiguratorResult.False:
          configuratorResult1 = PdmConfiguratorResult.True;
          break;
        case PdmConfiguratorResult.True:
          configuratorResult1 = PdmConfiguratorResult.False;
          break;
      }
    }
    return configuratorResult1;
  }

  /// <summary>Результат последнего вычисления</summary>
  public virtual TraceEntry EvaluateTrace
  {
    [DebuggerStepThrough] get => this._evaluateTrace;
  }

  /// <summary>Загрузить информацию из объекта/связи базы данных</summary>
  /// <param name="obj">Источник</param>
  /// <returns>true - информация загружена успешно, false - были ошибки</returns>
  public virtual bool LoadFromObject(IDBAttributable obj)
  {
    this.Assign((object) obj);
    return true;
  }

  /// <summary>Записать информацию в указанный элемент базы данных</summary>
  /// <param name="obj">Элемент-назначение</param>
  /// <returns>true - вся информация записана успешно, false - были ошибки</returns>
  public virtual bool SaveToObject(IDBAttributable obj)
  {
    bool flag = false;
    if (obj == null)
      return flag;
    IDBObject dbObject = obj as IDBObject;
    IDBRelation dbRelation = obj as IDBRelation;
    if (dbObject == null && dbRelation == null)
      return flag;
    IMSAttribute4 imsAttribute4 = dbObject != null ? (IMSAttribute4) MetaDataHelper.GetAttribute4ObjectType(dbObject.ObjectType, this.LoadSaveAttributeID) : (IMSAttribute4) MetaDataHelper.GetAttribute4RelationType(dbRelation.RelationType, this.LoadSaveAttributeID);
    if (imsAttribute4 == null)
      return flag;
    List<string> stringList = StringsHelper.SplitString(this.ToXMLString(), (int) MetaDataHelper.GetAttributeType(this.LoadSaveAttributeID).SizeType);
    IDBAttribute dbAttribute = obj.GetAttributeByID(this.LoadSaveAttributeID);
    if (dbAttribute != null)
    {
      if (this.Empty || stringList.Count == 0)
      {
        if (imsAttribute4.Required == RequiredModes.Manual)
          dbAttribute.Delete(0L);
        return true;
      }
    }
    else
    {
      if (this.Empty || stringList.Count == 0)
        return flag;
      if (imsAttribute4.Required == RequiredModes.Manual)
        dbAttribute = obj.Attributes.AddAttribute(this.LoadSaveAttributeID, false);
    }
    if (dbAttribute == null)
      return flag;
    dbAttribute.Values = (object[]) stringList.ToArray();
    return true;
  }

  /// <summary>
  /// Преобразовать содержимое коллекции в массив, пригодный для записи в поле Values у многозначного атрибута
  /// </summary>
  /// <returns>Массив, пригодный для записи в поле Values у многозначного атрибута, или null, если коллекция пустая</returns>
  public virtual object[] ToAttributeValues(int attributeTypeID)
  {
    if (AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID))
      throw new ArgumentException();
    if (this.Empty)
      return (object[]) null;
    List<string> stringList = StringsHelper.SplitString(this.ToXMLString(), (int) MetaDataHelper.GetAttributeType(attributeTypeID).SizeType);
    object[] attributeValues = stringList.Count > 0 ? new object[stringList.Count] : (object[]) null;
    for (int index = 0; index < stringList.Count; ++index)
      attributeValues[index] = (object) stringList[index];
    return attributeValues;
  }

  public string ToXMLString()
  {
    XMLSettingsStorage xmlStorage = new XMLSettingsStorage();
    xmlStorage.document.InnerXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><IPS />";
    if (this.Holder != null)
      xmlStorage.Services.AddService(typeof (object), this.Holder);
    this.Save(xmlStorage, (XmlNode) xmlStorage.document.DocumentElement);
    if (xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper service)
      service.Save(xmlStorage, (XmlNode) xmlStorage.document.DocumentElement);
    string xmlString = xmlStorage.document.InnerXml;
    if (xmlString == "<?xml version=\"1.0\" encoding=\"utf-8\"?><IPS />")
      xmlString = string.Empty;
    else if (xmlString.IndexOf("<?xml version=\"1.0\" encoding=\"utf-8\"?>") == 0)
      xmlString = xmlString.Substring("<?xml version=\"1.0\" encoding=\"utf-8\"?>".Length);
    return xmlString;
  }

  /// <summary>Преобразовать критерий в строку</summary>
  /// <param name="isLastItem"> является ли критерий последним в родительской коллекции</param>
  /// <param name="isSingleItem"> является ли критерий единственным в родительской коллекции</param>
  /// <returns></returns>
  public virtual string GenerateStringComments(bool isLastItem, bool isSingleItem)
  {
    string empty = string.Empty;
    lock (this.syncRoot)
    {
      string str1 = isLastItem ? string.Empty : (this.Function == LogicalFunction.And ? LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_19") : LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_20"));
      string str2 = string.Empty;
      for (int index = 0; index < this._items.Count; ++index)
      {
        IPdmCriterion pdmCriterion = this._items[index];
        str2 += pdmCriterion.GenerateStringComments(index == this._items.Count - 1, this._items.Count == 1);
      }
      if (this.CriterionType == PdmCriterionType.Stub)
        return this.Not ? $"НЕ({str2})" : str2;
      if (str2 != string.Empty)
      {
        if (this.Not)
          str2 = $"НЕ({str2})";
        else if (!isSingleItem)
          str2 = $"({str2})";
      }
      return str2 + str1;
    }
  }

  /// <summary>Является ли элемент пустым или нет</summary>
  public virtual bool Empty
  {
    get
    {
      lock (this.syncRoot)
      {
        if (this.Count == 0)
          return true;
        bool empty = true;
        for (int index = 0; index < this.Count; ++index)
          empty &= this[index].Empty;
        return empty;
      }
    }
  }

  /// <summary>Вид критерия-коллекции конфигуратора составов IPS</summary>
  public virtual PdmCriterionType CriterionType
  {
    [DebuggerStepThrough] get => PdmCriterionType.Collection;
    set
    {
    }
  }

  /// <summary>Сгенерировать пустой критерий</summary>
  /// <returns>Пустой критерий</returns>
  public virtual IPdmCriterion GenerateEmptyCriterion()
  {
    return Activator.CreateInstance(this.GetElementType()) as IPdmCriterion;
  }

  /// <summary>
  /// Сгенерировать пустой критерий и добавить его в коллекцию
  /// </summary>
  /// <returns>Добавленный пустой критерий</returns>
  public virtual IPdmCriterion AddEmptyCriterion()
  {
    IPdmCriterion emptyCriterion = this.GenerateEmptyCriterion();
    this.Add(emptyCriterion);
    return emptyCriterion;
  }

  /// <summary>
  /// Сгенерировать критерий-"заглушку" и добавить его в коллекцию
  /// </summary>
  /// <returns>Добавленный критерий-"заглушка"</returns>
  public virtual IPdmCriterion AddStubCriterion()
  {
    IPdmCriterion pdmCriterion = this.AddEmptyCriterion();
    lock (this.syncRoot)
      pdmCriterion.CriterionType = PdmCriterionType.Stub;
    return pdmCriterion;
  }

  /// <summary>Удалить критерий из коллекции</summary>
  /// <returns>true - удаление было выполнено успешно</returns>
  public virtual bool RemoveCriterion(IPdmCriterion criterion)
  {
    lock (this.syncRoot)
    {
      if (criterion != null)
      {
        if (this.IndexOf(criterion) >= 0)
        {
          this.Remove(criterion);
          return true;
        }
      }
    }
    return false;
  }

  /// <summary>Проверить наличие критерия в коллекции</summary>
  /// <param name="criterion">Искомый критерий</param>
  /// <returns>true - критерий найден в коллекции</returns>
  public virtual bool ExistsCriterion(IPdmCriterion criterion)
  {
    lock (this.syncRoot)
      return this.IndexOf(criterion) >= 0;
  }

  /// <summary>
  /// Отыскать первый критерий, у которого Guid опции равно указанному. Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Первый критерий, у которого Guid опции равно указанному</returns>
  public virtual IPdmCriterion FindCriterion(Guid option)
  {
    IPdmCriterion criterion = (IPdmCriterion) null;
    lock (this.syncRoot)
    {
      for (int index = 0; index < this._items.Count; ++index)
      {
        criterion = this._items[index].FindCriterion(option);
        if (criterion != null)
          break;
      }
    }
    return criterion;
  }

  /// <summary>
  /// Отыскать все критерии не заглушки, у которых Guid опции равно указанному.
  /// Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <returns>Все критерии, у которых Guid опции равно указанному</returns>
  public virtual List<IPdmCriterion> FindCriterionEx(Guid option)
  {
    List<IPdmCriterion> criterionEx1 = new List<IPdmCriterion>();
    lock (this.syncRoot)
    {
      for (int index = 0; index < this._items.Count; ++index)
      {
        List<IPdmCriterion> criterionEx2 = this._items[index].FindCriterionEx(option);
        if (criterionEx2 != null)
          criterionEx1.AddRange((IEnumerable<IPdmCriterion>) criterionEx2);
      }
    }
    return criterionEx1;
  }

  /// <summary>
  /// Отыскать критерий, у которого Guid и значение опции и равны указанным значениям. Поиск выполняется в самом элементе и всех его вложенных элементах
  /// </summary>
  /// <param name="option">Guid опции</param>
  /// <param name="optionValue">ID значения опции</param>
  /// <returns>Критерий, у которого Guid и значение опции и равны указанным значениям</returns>
  public virtual IPdmCriterion FindCriterion(Guid option, string optionValue)
  {
    IPdmCriterion criterion = (IPdmCriterion) null;
    lock (this.syncRoot)
    {
      for (int index = 0; index < this._items.Count; ++index)
      {
        criterion = this._items[index].FindCriterion(option, optionValue);
        if (criterion != null)
          break;
      }
    }
    return criterion;
  }

  /// <summary>
  /// Метод вызывается перед сохранением коллекции в XML-документ. При возникновении ошибки следует сгенерировать исключение
  /// </summary>
  /// <param name="holder">Контейнер, которому принадлежит данный критерий</param>
  public virtual void BeforeSave(object holder)
  {
    lock (this.syncRoot)
    {
      for (int index = 0; index < this._items.Count; ++index)
        this[index].BeforeSave(holder);
    }
  }

  /// <summary>Выполнена загрузка данных из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public virtual void XMLAfterLoad(XMLSettingsStorage xmlStorage, XmlNode node)
  {
  }

  /// <summary>
  /// Выполнено сохранение данных в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public virtual void XMLAfterSave(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public virtual void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (node == null || node.Name != "d" || !(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper))
      return;
    lock (this.syncRoot)
    {
      string str = xmlStorage.GetAttributeValue(node, "a", string.Empty);
      if (str.StartsWith("!"))
      {
        this.Not = true;
        str = str.Substring(1);
      }
      this.Function = LogicalFunctionHelper.FromString(str);
      for (int i = 0; i < node.ChildNodes.Count; ++i)
      {
        XmlNode childNode = node.ChildNodes[i];
        IPdmCriterion pdmCriterion = (IPdmCriterion) null;
        if (childNode.Name == "e")
          pdmCriterion = Activator.CreateInstance(this.GetElementType()) as IPdmCriterion;
        if (childNode.Name == "d")
          pdmCriterion = Activator.CreateInstance(this.GetType()) as IPdmCriterion;
        if (pdmCriterion != null)
        {
          pdmCriterion.Load(xmlStorage, childNode);
          this.Add(pdmCriterion);
        }
      }
      this.XMLAfterLoad(xmlStorage, node);
    }
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public virtual void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    lock (this.syncRoot)
      this.BeforeSave(xmlStorage.Services.GetService(typeof (object)));
    if (!this.Empty)
    {
      if (!(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper))
      {
        PdmGuidMapper serviceInstance = new PdmGuidMapper();
        xmlStorage.Services.AddService(typeof (PdmGuidMapper), (object) serviceInstance);
      }
      XmlNode xmlNode = xmlStorage.AddNode(parentNode, "d");
      lock (this.syncRoot)
      {
        xmlStorage.SetAttributeValue(xmlNode, "a", (this.Not ? "!" : string.Empty) + LogicalFunctionHelper.ToString(this.Function));
        for (int index = 0; index < this.Count; ++index)
          this[index].Save(xmlStorage, xmlNode);
      }
    }
    lock (this.syncRoot)
      this.XMLAfterSave(xmlStorage, parentNode);
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты идентичны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is PdmCriterionsCollection criterionsCollection) || this.Not != criterionsCollection.Not)
      return false;
    lock (this.syncRoot)
    {
      if (criterionsCollection == null || this.Count != criterionsCollection.Count)
        return false;
      for (int index = 0; index < this.Count; ++index)
      {
        if (!this[index].Equals((object) criterionsCollection[index]))
          return false;
      }
    }
    return true;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    lock (this.syncRoot)
      return this.Count.GetHashCode();
  }

  public long[] GetOptionVersionIds()
  {
    List<long> source = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (IPdmCriterion pdmCriterion1 in this)
      {
        if (pdmCriterion1 is PdmCriterion)
        {
          PdmCriterion pdmCriterion2 = (PdmCriterion) pdmCriterion1;
          if (pdmCriterion2.Option != Guid.Empty)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(pdmCriterion2.Option, false);
            if (dbObject != null)
              source.Add(dbObject.ObjectID);
          }
        }
        else if (pdmCriterion1 is PdmCriterionsCollection)
        {
          PdmCriterionsCollection criterionsCollection = (PdmCriterionsCollection) pdmCriterion1;
          source.AddRange((IEnumerable<long>) criterionsCollection.GetOptionVersionIds());
        }
      }
    }
    return source.Distinct<long>().ToArray<long>();
  }
}
