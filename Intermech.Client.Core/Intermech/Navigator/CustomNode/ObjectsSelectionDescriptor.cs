
// Type: Intermech.Navigator.CustomNode.ObjectsSelectionDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.CustomNode;

/// <summary>Дескриптор списка объектов, удовлетворяющих некоторому условию
/// Позволяет строить "виртуальные выборки", получая для отображения список объектов, удовлетворяющий переданному условию</summary>
public class ObjectsSelectionDescriptor : 
  HiveDescriptor,
  IDescriptor,
  INodeItems,
  IPersistable,
  IConditionsProvider
{
  protected const string PropConditions = "Conditions";
  /// <summary>Условия поиска объектов</summary>
  [NotNull]
  protected ConditionStructure[] _Conditions;

  /// <summary>Конструктор</summary>
  /// <typeparam name="TTAttributeValueType">Type of the attribute value type</typeparam>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="attributeID">Идентификатор атрибута, значения которого будут условием выборки</param>
  /// <param name="objectAttributeValues">Перечисление возможных значений атрибута, которым должен равняться атрибут объекта для того, чтобы объект попал в выборку</param>
  /// <param name="duplicateNegativeValue">Дополнить переданные значения отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения. Работает только в случае, если
  /// TAttributeValueType - это long</param>
  /// <returns>Дескриптор</returns>
  [NotNull]
  public static ObjectsSelectionDescriptor CreateFromValues<TTAttributeValueType>(
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotEmpty] int attributeID,
    [NotNull] IReadOnlyCollection<TTAttributeValueType> objectAttributeValues,
    bool duplicateNegativeValue = false)
  {
    return new ObjectsSelectionDescriptor(objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<TTAttributeValueType>(attributeID, objectAttributeValues, duplicateNegativeValue));
  }

  /// <summary>Конструктор</summary>
  /// <typeparam name="TAttributeValueType">Type of the attribute value type</typeparam>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="attributeID">Идентификатор атрибута, значения которого будут условием выборки</param>
  /// <param name="objectAttributeValue">Значение атрибута, которому должен равняться атрибут объекта для того, чтобы объект попал в
  /// выборку</param>
  /// <param name="duplicateNegativeValue">Дополнить переданное значение отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения. Работает только в случае, если
  /// TAttributeValueType - это long</param>
  /// <returns>Дескриптор</returns>
  [NotNull]
  public static ObjectsSelectionDescriptor CreateFromValue<TAttributeValueType>(
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotEmpty] int attributeID,
    [CanBeNull] TAttributeValueType objectAttributeValue,
    bool duplicateNegativeValue = false)
  {
    if (!duplicateNegativeValue)
      return new ObjectsSelectionDescriptor(objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValue2Conditions<TAttributeValueType>(attributeID, objectAttributeValue));
    return new ObjectsSelectionDescriptor(objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<TAttributeValueType>(attributeID, (IReadOnlyCollection<TAttributeValueType>) new TAttributeValueType[1]
    {
      objectAttributeValue
    }, true));
  }

  /// <summary>Конструктор</summary>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="objectVersionIDs">Перечисление идентификаторов версий объектов</param>
  /// <param name="duplicateNegativeValue">(Optional) Дополнить переданное значение отрицательными. Требуется для запросов по
  /// идентификаторам объектов, позволяет проверять одновременно архивные и рабочие значения</param>
  public ObjectsSelectionDescriptor(
    [NotNull] string caption,
    [NotNull, ItemNotEmpty] IReadOnlyCollection<long> objectVersionIDs,
    bool duplicateNegativeValue = true)
    : this(MetaDataHelperService.Instance.GetCommonParentObjectTypeID((IEnumerable<long>) objectVersionIDs), caption, objectVersionIDs, duplicateNegativeValue)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="objectVersionIDs">Перечисление идентификаторов объектов</param>
  /// <param name="duplicateNegativeValue">(Optional) Дополнить переданное значение отрицательными. Требуется для запросов по
  /// идентификаторам объектов, позволяет проверять одновременно архивные и рабочие значения</param>
  public ObjectsSelectionDescriptor(
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotNull, ItemNotEmpty] IReadOnlyCollection<long> objectVersionIDs,
    bool duplicateNegativeValue = true)
    : this(objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<long>(-2, objectVersionIDs, duplicateNegativeValue))
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="objectVersionIDs">Перечисление идентификаторов версий объектов</param>
  /// <param name="conditions">Условия выбора объектов</param>
  /// <param name="duplicateNegativeValue">Дополнить переданное значение отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения</param>
  public ObjectsSelectionDescriptor(
    [NotNull] string caption,
    [NotNull, ItemNotEmpty] IReadOnlyCollection<long> objectVersionIDs,
    [NotNull] IReadOnlyCollection<ConditionStructure> conditions,
    bool duplicateNegativeValue = true)
    : this(MetaDataHelperService.Instance.GetCommonParentObjectTypeID((IEnumerable<long>) objectVersionIDs), caption, objectVersionIDs, conditions, duplicateNegativeValue)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="objectVersionIDs">Перечисление идентификаторов версий объектов</param>
  /// <param name="conditions">Условия выбора объектов</param>
  /// <param name="duplicateNegativeValue">Дополнить переданное значение отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения</param>
  public ObjectsSelectionDescriptor(
    int objectTypeID,
    [NotNull] string caption,
    [NotNull, ItemNotEmpty] IReadOnlyCollection<long> objectVersionIDs,
    [NotNull] IReadOnlyCollection<ConditionStructure> conditions,
    bool duplicateNegativeValue = true)
    : this(objectTypeID, caption, ObjectsSelectionDescriptor.MergeConditions(ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<long>(-2, objectVersionIDs, duplicateNegativeValue), conditions))
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="conditions">Условия поиска объектов</param>
  public ObjectsSelectionDescriptor(
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotNull] IReadOnlyCollection<ConditionStructure> conditions)
    : base(Intermech.Navigator.Consts.CategoryMultipleObjectsNode, objectTypeID, caption)
  {
    this._Conditions = conditions.AsArray<ConditionStructure>();
  }

  /// <summary>Специальный конструктор, используемый для десериализации дескриптора</summary>
  public ObjectsSelectionDescriptor([NotNull] PersistentState state)
    : base(state)
  {
    this._Conditions = (ConditionStructure[]) state.GetValue(nameof (Conditions));
  }

  /// <summary>Преобразование значений аттрибута в массив условий выбора объектов</summary>
  /// <typeparam name="TAttributeValueType">Type of the attribute value type</typeparam>
  /// <param name="attributeID">Идентификатор атрибута, значения которого будут условием выборки</param>
  /// <param name="objectAttributeValues">Перечисление возможных значений атрибута, которым должен равняться атрибут объекта для того,
  /// чтобы объект попал в выборку</param>
  /// <param name="duplicateNegativeValue">Дополнять переданные значения отрицательными.
  /// Требуется для запросов по идентификаторам объектов, позволяет проверять одновременно архивные и рабочие значения
  /// Работает только в случае, если TAttributeValueType - это long</param>
  /// <returns>Массив условий</returns>
  [NotNull]
  protected static IReadOnlyCollection<ConditionStructure> ObjectAttributeValues2Conditions<TAttributeValueType>(
    [NotEmpty] int attributeID,
    [NotNull] IReadOnlyCollection<TAttributeValueType> objectAttributeValues,
    bool duplicateNegativeValue = false)
  {
    return !objectAttributeValues.Any<TAttributeValueType>() ? (IReadOnlyCollection<ConditionStructure>) new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.Equal, (object) 0, LogicalOperators.AND, 0, false)
    } : (IReadOnlyCollection<ConditionStructure>) new ConditionStructure[1]
    {
      new ConditionStructure(attributeID, RelationalOperators.In, !duplicateNegativeValue ? (object) objectAttributeValues.AsArray<TAttributeValueType>() : (object) ((IEnumerable<long>) objectAttributeValues).Concat<long>(((IEnumerable<long>) objectAttributeValues).Select<long, long>((Func<long, long>) (objectID => -objectID))).AsArray<long>(), LogicalOperators.AND, 0, false)
    };
  }

  /// <summary>Преобразование значения аттрибута в массив условий выбора объектов</summary>
  [NotNull]
  protected static IReadOnlyCollection<ConditionStructure> ObjectAttributeValue2Conditions<TAttributeValueType>(
    [NotEmpty] int attributeID,
    [CanBeNull] TAttributeValueType objectAttributeValue)
  {
    return (IReadOnlyCollection<ConditionStructure>) new ConditionStructure[1]
    {
      new ConditionStructure(attributeID, RelationalOperators.Equal, (object) objectAttributeValue, LogicalOperators.AND, 0, false)
    };
  }

  /// <summary>Объединение двух последовательностей условий в одну</summary>
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static IReadOnlyCollection<ConditionStructure> MergeConditions(
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions1,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions2)
  {
    if (conditions1 == null)
      return conditions2 ?? (IReadOnlyCollection<ConditionStructure>) Array.Empty<ConditionStructure>();
    return conditions2 == null ? conditions1 : (IReadOnlyCollection<ConditionStructure>) ListFactory.Create<ConditionStructure>(conditions1.Concat<ConditionStructure>((IEnumerable<ConditionStructure>) conditions2), conditions1.Count + conditions2.Count);
  }

  /// <summary>Перечисление условий поиска объектов</summary>
  [NotNull]
  public ConditionStructure[] Conditions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Conditions;
  }

  public override void GetObjectData([NotNull] PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("Conditions", (object) this._Conditions);
  }

  /// <summary>Возвращает идентификатор поля источника данных для указанной виртуальной колонки. Если данная колонка не поддерживается, то
  /// метод возвращает null.</summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  public override object MapColumnToField([NotNull] NodeColumn column)
  {
    return column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.NameColumnSchemeGuid ? column.ID : (object) null;
  }

  [NotNull]
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new ObjectsSelectionNodeID(this._typeID, (IConditionsProvider) this);
  }

  [NotNull]
  public ConditionStructure[] GetConditions()
  {
    return ((IEnumerable<ConditionStructure>) this._Conditions).AsArray<ConditionStructure>();
  }

  public bool ConditionsChanged
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => false;
  }

  /// <summary>Сравнить дескриптор с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is ObjectsSelectionDescriptor selectionDescriptor))
      return base.Equals(obj);
    return base.Equals(obj) && this.Conditions.Equals((object) selectionDescriptor.Conditions);
  }

  public override int GetHashCode() => (base.GetHashCode(), this.Conditions).GetHashCode();

  /// <summary>Вернуть данные определённого формата по указанному описанию узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные определённого формата по указанному описанию узла</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return !(dataFormat == typeof (IDescriptor)) ? base.GetData(nodeID, dataFormat) : (object) new ObjectsSelectionDescriptor(this._typeID, this._caption ?? string.Empty, (IReadOnlyCollection<ConditionStructure>) this._Conditions);
  }

  [NotNull]
  public override PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = base.Serialize(nodeID);
    persistentState.AddValue("Conditions", (object) this._Conditions);
    return persistentState;
  }

  [NotNull]
  public override INodeID Deserialize(PersistentState persistNodeID)
  {
    return (INodeID) new ObjectsSelectionNodeID(this._typeID, (IConditionsProvider) this);
  }

  /// <summary>Вернуть дочерний узел согласно его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  [NotNull]
  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new ObjectsSelectionNode(this._typeID, (IConditionsProvider) this);
  }

  /// <summary>Обновить список объектов
  /// Лично мне потребовался метод для реализации удаления из списка объектов с отложенным коммитом в БД.
  /// Дело в том, что у меня есть диалог с childrenView и отложенным сохранением (кнопки OK, Cancel, в БД правки отправляются только по ОК)
  /// но удалять из списка объектов (childrenView) нужно и до нажатия OK. Проблема в том, что например при сортировке содержимое грида
  /// перечитывается из дескриптора, список объектов в котором задаётся лишь в конструкторе. Пробую дать возможность его менять.
  /// Не обкатано, пользовать на страх и риск</summary>
  /// <param name="objectVersionIDs">Перечисление идентификаторов версий объектов</param>
  /// <param name="duplicateNegativeValue">Дополнить переданное значение отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения</param>
  public void Update([NotNull] IReadOnlyCollection<long> objectVersionIDs, bool duplicateNegativeValue = false)
  {
    this._Conditions = ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<long>(-2, objectVersionIDs, duplicateNegativeValue).AsArray<ConditionStructure>();
  }
}
