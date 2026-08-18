
// Type: Intermech.Navigator.CustomNode.RelatedObjectsSelectionDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.CustomNode;

/// <summary>Дескриптор состава объекта, с фильтрацией по переданным в конструктор условиям
/// Позволяет строить "виртуальные выборки", получая для отображения список объектов, удовлетворяющий переданному условию</summary>
public class RelatedObjectsSelectionDescriptor : 
  ObjectsSelectionDescriptor,
  IDescriptor,
  INodeItems,
  IPersistable,
  IConditionsProvider
{
  protected const string PropObjectVersionID = "ObjectVersionID";
  protected const string PropRole = "Role";
  protected const string PropRelationType = "RelationTypeID";
  /// <summary>Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</summary>
  protected readonly long _ObjectVersionID;
  /// <summary>Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</summary>
  protected readonly RelatedObjectsRole _Role;
  /// <summary>Тип связи, по которой получается состав/входимость</summary>
  protected readonly int _RelationTypeID = -1;

  /// <summary>Конструктор</summary>
  /// <typeparam name="TAttributeValueType">Тип значения атрибута</typeparam>
  /// <param name="objectVersionID">Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</param>
  /// <param name="role">Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</param>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="attributeID">Идентификатор атрибута, значения которого будут условием выборки</param>
  /// <param name="objectAttributeValues">Перечисление значений атрибута, которым должен равняться атрибут объекта для того, чтобы объект
  /// попал в выборку</param>
  /// <param name="duplicateNegativeValue">Дополнить переданные значения отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения. Работает только в случае, если
  /// TAttributeValueType - это long</param>
  /// <returns>Дескриптор</returns>
  [NotNull]
  public static RelatedObjectsSelectionDescriptor CreateFromValues<TAttributeValueType>(
    [NotEmpty] long objectVersionID,
    RelatedObjectsRole role,
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotEmpty] int attributeID,
    [NotNull] IReadOnlyCollection<TAttributeValueType> objectAttributeValues,
    bool duplicateNegativeValue = false)
  {
    return new RelatedObjectsSelectionDescriptor(objectVersionID, role, objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<TAttributeValueType>(attributeID, objectAttributeValues, duplicateNegativeValue));
  }

  /// <summary>Конструктор</summary>
  /// <typeparam name="TAttributeValueType">Тип значения атрибута</typeparam>
  /// <param name="objectVersionID">Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</param>
  /// <param name="relationTypeID">Тип связи, по которой собирается состав/входимость</param>
  /// <param name="role">Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</param>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="attributeID">Идентификатор атрибута, значения которого будут условием выборки</param>
  /// <param name="objectAttributeValues">Перечисление значений атрибута, которым должен равняться атрибут объекта для того, чтобы объект
  /// попал в выборку</param>
  /// <param name="duplicateNegativeValue">Дополнить переданные значения отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения. Работает только в случае, если
  /// TAttributeValueType - это long</param>
  /// <returns>Дескриптор</returns>
  [NotNull]
  public static RelatedObjectsSelectionDescriptor CreateFromValues<TAttributeValueType>(
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotEmpty] int attributeID,
    [NotNull] IReadOnlyCollection<TAttributeValueType> objectAttributeValues,
    bool duplicateNegativeValue = false)
  {
    return new RelatedObjectsSelectionDescriptor(objectVersionID, role, objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<TAttributeValueType>(attributeID, objectAttributeValues, duplicateNegativeValue));
  }

  /// <summary>Конструктор</summary>
  /// <typeparam name="TAttributeValueType">Тип значения атрибута</typeparam>
  /// <param name="objectVersionID">Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</param>
  /// <param name="role">Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</param>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="attributeID">Идентификатор атрибута, значения которого будут условием выборки</param>
  /// <param name="objectAttributeValue">Перечисление значений атрибута, которым должен равняться атрибут объекта для того, чтобы объект
  /// попал в выборку</param>
  /// <param name="duplicateNegativeValue">Дополнить переданное значение отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения. Работает только в случае, если
  /// TAttributeValueType - это long</param>
  /// <returns>Дескриптор</returns>
  [NotNull]
  public static RelatedObjectsSelectionDescriptor CreateFromValues<TAttributeValueType>(
    [NotEmpty] long objectVersionID,
    RelatedObjectsRole role,
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotEmpty] int attributeID,
    [CanBeNull] TAttributeValueType objectAttributeValue,
    bool duplicateNegativeValue = false)
  {
    if (!duplicateNegativeValue)
      return new RelatedObjectsSelectionDescriptor(objectVersionID, role, objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValue2Conditions<TAttributeValueType>(attributeID, objectAttributeValue));
    return new RelatedObjectsSelectionDescriptor(objectVersionID, role, objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<TAttributeValueType>(attributeID, (IReadOnlyCollection<TAttributeValueType>) new TAttributeValueType[1]
    {
      objectAttributeValue
    }, true));
  }

  /// <summary>Конструктор</summary>
  /// <typeparam name="TAttributeValueType">Тип значения атрибута</typeparam>
  /// <param name="objectVersionID">Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</param>
  /// <param name="relationTypeID">Тип связи, по которой собирается состав/входимость</param>
  /// <param name="role">Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</param>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="attributeID">Идентификатор атрибута, значения которого будут условием выборки</param>
  /// <param name="objectAttributeValue">Перечисление значений атрибута, которым должен равняться атрибут объекта для того, чтобы объект
  /// попал в выборку</param>
  /// <param name="duplicateNegativeValue">Дополнить переданное значение отрицательными. Требуется для запросов по идентификаторам объектов,
  /// позволяет проверять одновременно архивные и рабочие значения. Работает только в случае, если
  /// TAttributeValueType - это long</param>
  /// <returns>Дескриптор</returns>
  [NotNull]
  public static RelatedObjectsSelectionDescriptor CreateFromValues<TAttributeValueType>(
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotEmpty] int attributeID,
    [CanBeNull] TAttributeValueType objectAttributeValue,
    bool duplicateNegativeValue = false)
  {
    if (!duplicateNegativeValue)
      return new RelatedObjectsSelectionDescriptor(objectVersionID, role, objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValue2Conditions<TAttributeValueType>(attributeID, objectAttributeValue));
    return new RelatedObjectsSelectionDescriptor(objectVersionID, role, objectTypeID, caption, ObjectsSelectionDescriptor.ObjectAttributeValues2Conditions<TAttributeValueType>(attributeID, (IReadOnlyCollection<TAttributeValueType>) new TAttributeValueType[1]
    {
      objectAttributeValue
    }, true));
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectVersionID">Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</param>
  /// <param name="role">Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</param>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="conditions">Условия поиска объектов</param>
  public RelatedObjectsSelectionDescriptor(
    [NotEmpty] long objectVersionID,
    RelatedObjectsRole role,
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotNull] IReadOnlyCollection<ConditionStructure> conditions)
    : base(objectTypeID, caption, conditions)
  {
    this._ObjectVersionID = objectVersionID;
    this._Role = role;
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectVersionID">Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</param>
  /// <param name="relationTypeID">Тип связи, по которой собирается состав/входимость</param>
  /// <param name="role">Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</param>
  /// <param name="objectTypeID">Тип объектов</param>
  /// <param name="caption">Заголовок ноды</param>
  /// <param name="conditions">Условия поиска объектов</param>
  public RelatedObjectsSelectionDescriptor(
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int objectTypeID,
    [NotNull] string caption,
    [NotNull] IReadOnlyCollection<ConditionStructure> conditions)
    : base(objectTypeID, caption, conditions)
  {
    this._ObjectVersionID = objectVersionID;
    this._Role = role;
    this._RelationTypeID = relationTypeID;
  }

  /// <summary>Специальный конструктор, используемый для десериализации дескриптора</summary>
  public RelatedObjectsSelectionDescriptor([NotNull] PersistentState state)
    : base(state)
  {
    this._ObjectVersionID = (long) state.GetValue(nameof (ObjectVersionID));
    this._Role = (RelatedObjectsRole) state.GetValue(nameof (Role));
    if (!state.Contains(nameof (RelationTypeID)))
      return;
    this._RelationTypeID = (int) state.GetValue(nameof (RelationTypeID));
  }

  /// <summary>Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</summary>
  public long ObjectVersionID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._ObjectVersionID;
    }
  }

  /// <summary>Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</summary>
  public RelatedObjectsRole Role
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Role;
  }

  /// <summary>Тип связи, по которой получается состав/входимость</summary>
  public int RelationTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._RelationTypeID;
    }
  }

  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("ObjectVersionID", (object) this._ObjectVersionID);
    state.AddValue("Role", (object) this._Role);
    if (this._RelationTypeID == -1)
      return;
    state.AddValue("RelationTypeID", (object) this._RelationTypeID);
  }

  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new RelatedObjectsSelectionNodeID(this.ObjectVersionID, this.RelationTypeID, this.Role, this._typeID, (IConditionsProvider) this);
  }

  /// <summary>Сравнить дескриптор с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is RelatedObjectsSelectionDescriptor selectionDescriptor))
      return base.Equals(obj);
    return this.ObjectVersionID == selectionDescriptor._ObjectVersionID && this.Role == selectionDescriptor._Role && this._RelationTypeID == selectionDescriptor._RelationTypeID && base.Equals(obj);
  }

  public override int GetHashCode()
  {
    return (base.GetHashCode(), this._ObjectVersionID, this._Role, this._RelationTypeID).GetHashCode();
  }

  /// <summary>Вернуть данные определённого формата по указанному описанию узла</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные определённого формата по указанному описанию узла</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return !(dataFormat == typeof (IDescriptor)) ? base.GetData(nodeID, dataFormat) : (object) new RelatedObjectsSelectionDescriptor(this.ObjectVersionID, this.RelationTypeID, this.Role, this._typeID, this._caption ?? string.Empty, (IReadOnlyCollection<ConditionStructure>) this._Conditions);
  }

  public override PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = base.Serialize(nodeID);
    persistentState.AddValue("ObjectVersionID", (object) this._ObjectVersionID);
    persistentState.AddValue("Role", (object) this._Role);
    if (this._RelationTypeID != -1)
      persistentState.AddValue("RelationTypeID", (object) this._RelationTypeID);
    return persistentState;
  }

  public override INodeID Deserialize(PersistentState persistNodeID)
  {
    return (INodeID) new RelatedObjectsSelectionNodeID(this.ObjectVersionID, this.RelationTypeID, this.Role, this._typeID, (IConditionsProvider) this);
  }

  /// <summary>Вернуть дочерний узел согласно его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new RelatedObjectsSelectionNode(this.ObjectVersionID, this.RelationTypeID, this.Role, this._typeID, (IConditionsProvider) this);
  }
}
