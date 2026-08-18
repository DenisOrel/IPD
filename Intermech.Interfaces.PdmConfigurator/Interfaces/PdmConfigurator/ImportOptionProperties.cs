// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.ImportOptionProperties
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Описание заимствованной опции</summary>
[Serializable]
public sealed class ImportOptionProperties
{
  /// <summary>Поле для синхронизации</summary>
  private object SyncObject = new object();
  /// <summary>видимые значения импортируемой опции</summary>
  private List<string> visibleValues;
  /// <summary>
  /// id версии объекта, у которого будем заимствовать опцию
  /// </summary>
  private long objectID;
  /// <summary>условия несовместимости / связанные значения</summary>
  private ObjectIncompatibilitiesCollection incompCollection = new ObjectIncompatibilitiesCollection();
  /// <summary>
  /// набор id-ков для зависимых опций
  /// key - id опции,
  /// value - счётчик для использования
  /// (value вообще м.б. либо 1 либо 2,
  /// т.к. опция может быть использована только в связанных либо несовместимости)
  /// </summary>
  private Dictionary<long, int> dependentOptions = new Dictionary<long, int>();

  /// <summary>видимые значения импортируемой опции</summary>
  public List<string> VisibleValues
  {
    [DebuggerStepThrough] get => this.visibleValues;
  }

  /// <summary>условия несовместимости/ связанные значения</summary>
  public ObjectIncompatibilitiesCollection IncompCollection
  {
    [DebuggerStepThrough] get => this.incompCollection;
  }

  /// <summary>
  /// набор id-ков для зависимых опций
  /// key - id опции,
  /// value - счётчик для использования
  /// (value вообще м.б. либо 1 либо 2,
  /// т.к. опция может быть использована только в связанных либо несовместимости)
  /// </summary>
  public Dictionary<long, int> DependentOptions
  {
    [DebuggerStepThrough] get => this.dependentOptions;
  }

  /// <summary>
  /// id версии объекта, у которого будем экспортировать опцию
  /// </summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this.objectID;
  }

  /// <summary>Создать объект</summary>
  /// <param name="objID">id версии объекта, у которого экспортируем опцию</param>
  /// <param name="values">видимые значения опции</param>
  public ImportOptionProperties(long objID, List<string> values)
  {
    this.visibleValues = values;
    this.objectID = objID;
  }

  /// <summary>Создать объект</summary>
  /// <param name="objID">id версии объекта, у которого экспортируем опцию</param>
  /// <param name="values">видимые значения опции</param>
  /// <param name="incomp"> условия несовместимости/ связанные значения</param>
  /// <param name="dependent">набор id-ков для зависимых опций</param>
  public ImportOptionProperties(
    long objID,
    List<string> values,
    ObjectIncompatibilitiesCollection incomp,
    Dictionary<long, int> dependent)
  {
    this.visibleValues = values;
    this.objectID = objID;
    this.incompCollection = incomp;
    this.dependentOptions = dependent;
  }

  /// <summary>проверить, входит ли опция в список зависимых</summary>
  /// <param name="optionID">id проверяемой опции</param>
  /// <returns></returns>
  public bool IsDependentOption(long optionID)
  {
    lock (this.SyncObject)
      return this.dependentOptions.ContainsKey(optionID);
  }

  /// <summary>удалить список зависимых опций</summary>
  /// <param name="dependent">удаляемый список зависимых опций</param>
  private void RemoveDependentOptions(string[] dependent)
  {
    lock (this.SyncObject)
    {
      for (int index = 0; index < dependent.Length; ++index)
      {
        long int64 = Convert.ToInt64(dependent[index]);
        if (this.dependentOptions.ContainsKey(int64))
        {
          this.dependentOptions[int64]--;
          if (this.dependentOptions[int64] == 0)
            this.dependentOptions.Remove(int64);
        }
      }
    }
  }

  /// <summary>добавить список зависимых опций</summary>
  /// <param name="dependent">удаляемый список зависимых опций</param>
  private void AddDependentOptions(string[] dependent)
  {
    lock (this.SyncObject)
    {
      for (int index = 0; index < dependent.Length; ++index)
      {
        long int64 = Convert.ToInt64(dependent[index]);
        if (this.dependentOptions.ContainsKey(int64))
          this.dependentOptions[int64]++;
        else
          this.dependentOptions.Add(int64, 1);
      }
    }
  }

  /// <summary>
  /// изменить набор зависимых опций
  /// (связанные значения)
  /// </summary>
  /// <param name="dependent"> список id-ков связанных опций </param>
  /// <param name="linked"></param>
  /// <param name="isRemove"> что бы не делать enum - true удалить, false - добавить</param>
  public void ChangeLinkedOptions(string[] dependent, LinkedOptions linked, bool isRemove)
  {
    if (isRemove)
    {
      this.RemoveDependentOptions(dependent);
      this.incompCollection.LinkedOptions.Clear();
    }
    else
    {
      this.AddDependentOptions(dependent);
      this.incompCollection.LinkedOptions = linked;
    }
  }

  /// <summary>
  /// изменить набор зависимых опций
  /// (опции несовмеастимости)
  /// </summary>
  /// <param name="dependent"> список id-ков связанных опций </param>
  /// <param name="incomp"></param>
  /// <param name="isRemove"> что бы не делать enum - true удалить, false - добавить</param>
  public void ChangeIncompOptions(string[] dependent, IPdmCriterion incomp, bool isRemove)
  {
    if (isRemove)
    {
      this.RemoveDependentOptions(dependent);
      if (incomp == null)
        return;
      this.IncompCollection.Remove(incomp);
    }
    else
    {
      this.AddDependentOptions(dependent);
      if (incomp == null)
        return;
      this.IncompCollection.Add(incomp);
    }
  }
}
