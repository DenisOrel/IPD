// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.ImportOptions
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Коллекция заимствованных опций</summary>
[Serializable]
public sealed class ImportOptions
{
  /// <summary>Поле для синхронизации</summary>
  private object SyncObject = new object();
  /// <summary>
  /// набор всех импортируемых опций
  /// ключ - id опции,
  /// значение - описание опции
  /// </summary>
  private Dictionary<long, ImportOptionProperties> options = new Dictionary<long, ImportOptionProperties>();

  /// <summary>Является ли коллекция пустой</summary>
  public bool Empty
  {
    [DebuggerStepThrough] get => this.options.Count == 0;
  }

  /// <summary>
  /// Набор всех импортируемых опций. Ключ - id опции, значение - описание опции
  /// </summary>
  public Dictionary<long, ImportOptionProperties> Options
  {
    [DebuggerStepThrough] get => this.options;
  }

  /// <summary>
  /// добавить описание опции в список импортируемых
  /// (если описание опции уже сущесвтует - заменить)
  /// </summary>
  /// <param name="optionID">id импортируемой опции</param>
  /// <param name="objectID"> id версии объекта, у которого экспортируем опцию</param>
  /// <param name="visibleValues">видимые значение для этой опции</param>
  public void AddImportOption(long optionID, long objectID, List<string> visibleValues)
  {
    lock (this.SyncObject)
    {
      ImportOptionProperties optionProperties = new ImportOptionProperties(objectID, visibleValues);
      if (this.options.ContainsKey(optionID))
        this.options[optionID] = optionProperties;
      else
        this.options.Add(optionID, optionProperties);
    }
  }

  /// <summary>
  /// добавить описание опции в список импортируемых
  /// (если описание опции уже сущесвтует - заменить)
  /// </summary>
  /// <param name="optionID">id импортируемой опции</param>
  /// <param name="objectID"> id версии объекта, у которого экспортируем опцию</param>
  /// <param name="visibleValues">видимые значение для этой опции</param>
  /// <param name="incomp">коллекция опций несовместимости (включает связанные опции)</param>
  /// <param name="dependent">список id-ков зависимых опций</param>
  public void AddImportOption(
    long optionID,
    long objectID,
    List<string> visibleValues,
    ObjectIncompatibilitiesCollection incomp,
    Dictionary<long, int> dependent)
  {
    lock (this.SyncObject)
    {
      ImportOptionProperties optionProperties = new ImportOptionProperties(objectID, visibleValues, incomp, dependent);
      if (this.options.ContainsKey(optionID))
        this.options[optionID] = optionProperties;
      else
        this.options.Add(optionID, optionProperties);
    }
  }

  /// <summary>удалить опцию из списка импортируемых</summary>
  /// <param name="optionID"></param>
  public void RemoveImportOption(long optionID)
  {
    lock (this.SyncObject)
    {
      if (!this.options.ContainsKey(optionID))
        return;
      this.options.Remove(optionID);
    }
  }

  /// <summary>
  /// Изменить набор зависимых опций
  /// (связанные опции)
  /// </summary>
  /// <param name="optionID"></param>
  /// <param name="dependent"> список id-ков связанных опций </param>
  /// <param name="linked"> набор связанных опций</param>
  /// <param name="isRemove"> что бы не делать enum - true удалить, false - добавить</param>
  public void ChangeLinkedOptions(
    long optionID,
    string[] dependent,
    LinkedOptions linked,
    bool isRemove)
  {
    lock (this.SyncObject)
    {
      if (!this.options.ContainsKey(optionID))
        return;
      this.options[optionID].ChangeLinkedOptions(dependent, linked, isRemove);
    }
  }

  /// <summary>
  /// изменить набор зависимых опций
  /// (несовместимости)
  /// </summary>
  /// <param name="optionID"></param>
  /// <param name="dependent"> список id-ков связанных опций </param>
  /// <param name="incomp"> набор опций несовместимости</param>
  /// <param name="isRemove"> что бы не делать enum - true удалить, false - добавить</param>
  public void ChangeIncompOptions(
    long optionID,
    string[] dependent,
    IPdmCriterion incomp,
    bool isRemove)
  {
    lock (this.SyncObject)
    {
      if (!this.options.ContainsKey(optionID))
        return;
      this.options[optionID].ChangeIncompOptions(dependent, incomp, isRemove);
    }
  }

  /// <summary>
  /// вернуть описание опции, если опция содержится в списке импортируемых
  /// </summary>
  /// <param name="optionID">id опциb</param>
  /// <returns></returns>
  public ImportOptionProperties IsOptionExists(long optionID)
  {
    lock (this.SyncObject)
      return this.options.ContainsKey(optionID) ? this.options[optionID] : (ImportOptionProperties) null;
  }

  /// <summary>
  /// проверка на наличие ошибок
  /// в списке импортируемых опций
  /// </summary>
  public void CheckErrorExists()
  {
    lock (this.SyncObject)
    {
      foreach (long key1 in this.options.Keys)
      {
        ImportOptionProperties option1 = this.options[key1];
        OptionHolder option2 = PdmConfiguratorCache.CacheFindOption(key1);
        string str1 = option2 != null ? option2.OptionCaption : key1.ToString();
        foreach (long key2 in option1.DependentOptions.Keys)
        {
          OptionHolder option3 = PdmConfiguratorCache.CacheFindOption(key2);
          string str2 = option3 != null ? option3.OptionCaption : key2.ToString();
          string empty = string.Empty;
          ImportOptionProperties optionProperties = this.options.ContainsKey(key2) ? this.options[key2] : throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_53"), (object) str2, (object) str1));
          if (option1.ObjectID != optionProperties.ObjectID)
            throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_52"), (object) str2, (object) str1));
        }
      }
    }
  }
}
