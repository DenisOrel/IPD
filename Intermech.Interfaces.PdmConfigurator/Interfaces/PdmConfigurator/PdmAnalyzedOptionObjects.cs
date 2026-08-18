// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmAnalyzedOptionObjects
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Коллекция описаний объектов с опциями</summary>
[DebuggerDisplay("Count: {Count}; Parent: {Parent}")]
[Serializable]
public sealed class PdmAnalyzedOptionObjects : List<PdmAnalyzedOptionObject>, IAssignable, ICloneable
{
  /// <summary>Родительский объект</summary>
  private PdmAnalyzedOptionObjects parent;
  /// <summary>Словарик для дополнительных свойств</summary>
  public HybridDictionary Tags = new HybridDictionary();

  /// <summary>Создать пустой экземпляр класса</summary>
  public PdmAnalyzedOptionObjects()
    : base(0)
  {
  }

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="parent">Родительский объект</param>
  public PdmAnalyzedOptionObjects(PdmAnalyzedOptionObjects parent)
    : base(0)
  {
    this.parent = parent;
  }

  /// <summary>Родительский объект</summary>
  public PdmAnalyzedOptionObjects Parent
  {
    [DebuggerStepThrough] get => this.parent;
  }

  /// <summary>
  /// Отыскать в коллекции (начиная с её корневой записи) описание объекта с указанным идентификатором.
  /// Поиск также будет проходить в дочерних коллекциях.
  /// </summary>
  /// <param name="objectID">Уникальный в пределах коллекции идентификатор версии объекта</param>
  /// <returns>null, если описание объекта не найдено</returns>
  public PdmAnalyzedOptionObject FindObjectFromRoot(long objectID)
  {
    if (objectID == 0L)
      return (PdmAnalyzedOptionObject) null;
    PdmAnalyzedOptionObjects analyzedOptionObjects = this;
    while (analyzedOptionObjects.Parent != null)
      analyzedOptionObjects = analyzedOptionObjects.Parent;
    return analyzedOptionObjects.FindObject(objectID);
  }

  /// <summary>
  /// Отыскать в коллекции описание объекта с указанным идентификатором.
  /// Поиск также будет проходить в дочерних коллекциях.
  /// </summary>
  /// <param name="objectID">Уникальный в пределах коллекции идентификатор версии объекта</param>
  /// <returns>null, если описание объекта не найдено</returns>
  public PdmAnalyzedOptionObject FindObject(long objectID)
  {
    if (objectID == 0L)
      return (PdmAnalyzedOptionObject) null;
    for (int index = 0; index < this.Count; ++index)
    {
      PdmAnalyzedOptionObject analyzedOptionObject = this[index].FindObject(objectID);
      if (analyzedOptionObject != null)
        return analyzedOptionObject;
    }
    return (PdmAnalyzedOptionObject) null;
  }

  /// <summary>
  /// Добавить описание объекта в коллекцию (самый упрощённый вариант)
  /// </summary>
  /// <param name="id">Идентификатор объекта</param>
  /// <param name="objectID">Идентификатор версии объекта (уникальный в пределах всей коллекции)</param>
  /// <returns>Найденное или новое описание объекта</returns>
  public PdmAnalyzedOptionObject Add(long id, long objectID)
  {
    PdmAnalyzedOptionObject objectFromRoot = this.FindObjectFromRoot(objectID);
    if (objectFromRoot != null)
      return objectFromRoot;
    PdmAnalyzedOptionObject analyzedOptionObject = new PdmAnalyzedOptionObject(this, id, objectID, false, false, -1, string.Empty, 0L, 0L, -1, 0L, false, (List<long>) null);
    this.Add(analyzedOptionObject);
    return analyzedOptionObject;
  }

  /// <summary>
  /// Полное присваивание другого списка описаний удаляемых объектов
  /// </summary>
  /// <param name="obj">Источник</param>
  public void Assign(object obj)
  {
    if (this == obj)
      return;
    this.Clear();
    if (this.Tags == null)
      this.Tags = new HybridDictionary();
    else
      this.Tags.Clear();
    if (!(obj is PdmAnalyzedOptionObjects analyzedOptionObjects))
      return;
    this.parent = analyzedOptionObjects.Parent;
    this.Tags = CloneHelper.Clone((object) analyzedOptionObjects.Tags) as HybridDictionary;
    for (int index = 0; index < analyzedOptionObjects.Count; ++index)
    {
      PdmAnalyzedOptionObject analyzedOptionObject = analyzedOptionObjects[index].Clone() as PdmAnalyzedOptionObject;
      analyzedOptionObject.Parent = this.Parent;
      this.Add(analyzedOptionObject);
    }
  }

  /// <summary>
  /// Получение полного списка описаний объектов, включая объекты дочерних коллекций
  /// </summary>
  /// <param name="list">Полный список описаний объектов, включая объекты дочерних коллекций</param>
  private void InternalExtractObjects(List<PdmAnalyzedOptionObject> list)
  {
    for (int index = 0; index < this.Count; ++index)
    {
      list.Add(this[index]);
      this[index].Items.InternalExtractObjects(list);
    }
  }

  /// <summary>
  /// Получение полного списка описаний объектов, включая объекты дочерних коллекций
  /// </summary>
  /// <returns>Полный список описаний объектов, включая объекты дочерних коллекций</returns>
  public List<PdmAnalyzedOptionObject> ExtractObjects()
  {
    List<PdmAnalyzedOptionObject> list = new List<PdmAnalyzedOptionObject>();
    this.InternalExtractObjects(list);
    return list;
  }

  /// <summary>Подсчитать, сколько описаний объектов обработано</summary>
  /// <returns>Количество обработанных описаний объектов</returns>
  public int ParsedCount()
  {
    List<PdmAnalyzedOptionObject> objects = this.ExtractObjects();
    int num = 0;
    for (int index = 0; index < objects.Count; ++index)
    {
      if (objects[index].ParsedObject)
        ++num;
    }
    return num;
  }

  /// <summary>
  /// Отыскивает первого, не равного null, "родителя". Если такого нет, вернёт parObject
  /// </summary>
  /// <returns>Родительский узел, у которого Parent = null</returns>
  public PdmAnalyzedOptionObjects FindRootParent()
  {
    if (this.Parent == null)
      return this;
    if (this.Parent.Parent == null)
      return this.Parent;
    PdmAnalyzedOptionObjects parent = this.Parent;
    PdmAnalyzedOptionObjects rootParent = this.Parent;
    for (; parent.Parent != null; parent = parent.Parent)
      rootParent = parent;
    return rootParent;
  }

  /// <summary>Создать копию экземпляра класса</summary>
  /// <returns>Копия экземпляра класса</returns>
  public object Clone()
  {
    PdmAnalyzedOptionObjects analyzedOptionObjects = new PdmAnalyzedOptionObjects(this.parent);
    analyzedOptionObjects.Assign((object) this);
    return (object) analyzedOptionObjects;
  }

  /// <summary>
  /// Выполнить сортировку коллекции и её дочерних элементов
  /// </summary>
  /// <param name="comparer">Сравниватель</param>
  public new void Sort(IComparer<PdmAnalyzedOptionObject> comparer)
  {
    base.Sort(comparer);
    for (int index = 0; index < this.Count; ++index)
      this[index].Items.Sort(comparer);
  }

  /// <summary>
  /// Проверить коллекцию объектов на наличие игнорируемых объектов
  /// </summary>
  /// <param name="excludedObjects">Список игнорируемых объектов</param>
  public void CheckObjects(IList<long> excludedObjects)
  {
  }
}
