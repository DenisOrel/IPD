// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIBaseExternalKeysService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Localization;
using Intermech.Text;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Базовый класс для сервисов внешних ключей изделий для интеграторов на основе CAD-интерфейса.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="driver">Драйвер захвата изменений</param>
/// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
/// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
public abstract class CIBaseExternalKeysService(
  CICaptureChangesDriver driver,
  CaptureChangesDriverContext driverContext) : MechanicalDriverService((MechanicalDriver) driver, driverContext), IArticleExternalKeysService
{
  /// <summary>
  /// Проверяет, поддерживается ли указанное изделие механизмом внешних ключей.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="modelItem">Рабочий элемент конструкторского документа</param>
  /// <returns>true, если механизм внешних ключей поддерживает указанное изделие</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  public bool HasExternalKeySupport(SectionEntity articleItem, SectionEntity modelItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    return modelItem != null ? this.DoHasExternalKeySupport(articleItem, modelItem) : throw new ArgumentNullException(nameof (modelItem));
  }

  /// <summary>
  /// Проверяет, поддерживается ли указанное изделие механизмом внешних ключей.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <param name="modelItem">Рабочий элемент конструкторского документа</param>
  /// <returns>true, если механизм внешних ключей поддерживает указанное изделие</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  protected abstract bool DoHasExternalKeySupport(
    SectionEntity articleItem,
    SectionEntity modelItem);

  /// <summary>Возвращает внешний ключ изделия.</summary>
  /// <param name="articleItem">Рабочий элемент изделия, поддерживаемый механизмом внешних ключей</param>
  /// <param name="modelItem">Рабочий элемент конструкторского документа</param>
  /// <returns>Значение внешнего ключа изделия</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  /// <exception cref="T:System.InvalidOperationException">Указанное изделие не поддерживается механизмом внешних ключей</exception>
  public string GetExternalKey(SectionEntity articleItem, SectionEntity modelItem)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (modelItem == null)
      throw new ArgumentNullException(nameof (modelItem));
    string configurationName = this.DoHasExternalKeySupport(articleItem, modelItem) ? this.DoGetArticleInternalId(articleItem) : throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_346"));
    return CADArticleExternalKeys.GetExternalKey(articleItem.Sections.Get<AttributesSection>().WorkingSet, configurationName);
  }

  /// <summary>
  /// Присваивает всем новым изделиям внешние ключи, а для существующих изделий выполняет проверку валидности ключей. Если ключ не валиден,
  /// то он должен быть перегенерирован.
  /// </summary>
  /// <param name="articleItems">Список рабочих элементов изделий, поддерживаемых механизмом внешних ключей</param>
  /// <param name="modelItem">Рабочий элемент конструкторского документа</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  /// <exception cref="T:System.InvalidOperationException">Одно из указанных изделий не поддерживается механизмом внешних ключей</exception>
  public void CorrectExternalKeys(List<SectionEntity> articleItems, SectionEntity modelItem)
  {
    if (articleItems == null)
      throw new ArgumentNullException(nameof (articleItems));
    if (modelItem == null)
      throw new ArgumentNullException(nameof (modelItem));
    if (articleItems.Count == 0)
      return;
    foreach (SectionEntity articleItem in articleItems)
    {
      if (!this.DoHasExternalKeySupport(articleItem, modelItem))
        throw new InvalidOperationException("Одно из указанных изделий не поддерживается механизмом внешних ключей");
    }
    List<SectionEntity> normalArticles = new List<SectionEntity>(articleItems.Count);
    List<SectionEntity> sectionEntityList = new List<SectionEntity>(articleItems.Count);
    List<SectionEntity> newArticles = new List<SectionEntity>(articleItems.Count);
    this.SplitArticlesByExternalKey(articleItems, modelItem, normalArticles, sectionEntityList, newArticles);
    if (sectionEntityList.Count > 0)
      this.ResignExternalKeys(sectionEntityList);
    if (newArticles.Count <= 0)
      return;
    this.MakeExternalKeys(newArticles);
  }

  private void SplitArticlesByExternalKey(
    List<SectionEntity> articleItems,
    SectionEntity modelItem,
    List<SectionEntity> normalArticles,
    List<SectionEntity> badSignedArticles,
    List<SectionEntity> newArticles)
  {
    Dictionary<string, List<SectionEntity>> dictionary = new Dictionary<string, List<SectionEntity>>(articleItems.Count, (IEqualityComparer<string>) StringComparer.CurrentCultureIgnoreCase);
    foreach (SectionEntity articleItem in articleItems)
    {
      string signedExternalKey = CADArticleExternalKeys.GetSignedExternalKey(articleItem.Sections.Get<AttributesSection>().WorkingSet);
      string key = !string.IsNullOrEmpty(signedExternalKey) ? signedExternalKey : string.Empty;
      List<SectionEntity> sectionEntityList;
      if (!dictionary.TryGetValue(key, out sectionEntityList))
      {
        sectionEntityList = new List<SectionEntity>(articleItems.Count);
        dictionary.Add(key, sectionEntityList);
      }
      sectionEntityList.Add(articleItem);
    }
    if (dictionary.ContainsKey(string.Empty))
    {
      newArticles.AddRange((IEnumerable<SectionEntity>) dictionary[string.Empty]);
      dictionary.Remove(string.Empty);
    }
    foreach (KeyValuePair<string, List<SectionEntity>> keyValuePair in dictionary)
    {
      string key = keyValuePair.Key;
      List<SectionEntity> sectionEntityList = keyValuePair.Value;
      string str;
      ref string local1 = ref str;
      string signature;
      ref string local2 = ref signature;
      CADArticleExternalKeys.ParseExternalKey(key, out local1, out local2);
      int index1 = sectionEntityList.FindIndex((Predicate<SectionEntity>) (articleItem => string.Compare(signature, TextServices.Trim(this.DoGetArticleInternalId(articleItem)), true) == 0));
      if (index1 >= 0)
      {
        normalArticles.Add(sectionEntityList[index1]);
        sectionEntityList.RemoveAt(index1);
      }
      else if (sectionEntityList.Count == 1)
      {
        int index2 = 0;
        badSignedArticles.Add(sectionEntityList[index2]);
        sectionEntityList.RemoveAt(index2);
      }
      else if (!FileVars.SoftMode.Value)
      {
        SelectPrototypeConfigurationForm configurationForm = new SelectPrototypeConfigurationForm();
        configurationForm.Document = Path.GetFileName(FilesSection.GetMasterFile(modelItem));
        configurationForm.ConfigurationName = signature;
        configurationForm.Configurations = this.MakeConfigurationInfos(sectionEntityList);
        int index3 = configurationForm.ShowDialog() == DialogResult.OK ? configurationForm.SelectedConfiguration : throw new AbortException();
        if (index3 >= 0)
        {
          badSignedArticles.Add(sectionEntityList[index3]);
          sectionEntityList.RemoveAt(index3);
        }
      }
      newArticles.AddRange((IEnumerable<SectionEntity>) sectionEntityList);
    }
  }

  private List<SelectPrototypeConfigurationForm.ConfigurationInfo> MakeConfigurationInfos(
    List<SectionEntity> cluster)
  {
    List<SelectPrototypeConfigurationForm.ConfigurationInfo> configurationInfoList = new List<SelectPrototypeConfigurationForm.ConfigurationInfo>(cluster.Count);
    foreach (SectionEntity articleItem in cluster)
    {
      string articleInternalId = this.DoGetArticleInternalId(articleItem);
      AttributesSection attributesSection = articleItem.Sections.Get<AttributesSection>();
      string designation = attributesSection.WorkingSet.Read<string>((StringKey) IDCache.Default.Designation.Text, string.Empty);
      string okpCode = attributesSection.WorkingSet.Read<string>((StringKey) IDCache.Default.OKPCode.Text, string.Empty);
      string name = attributesSection.WorkingSet.Read<string>((StringKey) IDCache.Default.Name.Text, string.Empty);
      configurationInfoList.Add(new SelectPrototypeConfigurationForm.ConfigurationInfo(articleInternalId, designation, okpCode, name));
    }
    return configurationInfoList;
  }

  /// <summary>
  /// Переподписывает внешние ключи изделий, у которых было изменено имя конфигурации.
  /// </summary>
  /// <param name="existingArticles">Коллекция узлов изделий</param>
  private void ResignExternalKeys(List<SectionEntity> existingArticles)
  {
    foreach (SectionEntity existingArticle in existingArticles)
    {
      string articleInternalId = this.DoGetArticleInternalId(existingArticle);
      AttributesSection attributesSection = existingArticle.Sections.Get<AttributesSection>();
      string externalKey;
      CADArticleExternalKeys.ParseExternalKey(CADArticleExternalKeys.GetSignedExternalKey(attributesSection.WorkingSet), out externalKey, out string _);
      CADArticleExternalKeys.UpdateSignedExternalKey(attributesSection.WorkingSet, CADArticleExternalKeys.SignExternalKey(externalKey, articleInternalId), true, false);
    }
  }

  /// <summary>Создает внешние ключи для новых изделий.</summary>
  /// <param name="newArticles">Коллекция узлов изделий</param>
  private void MakeExternalKeys(List<SectionEntity> newArticles)
  {
    foreach (SectionEntity newArticle in newArticles)
    {
      string articleInternalId = this.DoGetArticleInternalId(newArticle);
      CADArticleExternalKeys.UpdateSignedExternalKey(newArticle.Sections.Get<AttributesSection>().WorkingSet, CADArticleExternalKeys.SignExternalKey(Guid.NewGuid().ToString(), articleInternalId), true, false);
    }
  }

  /// <summary>
  /// Возвращает уникальный идентификатор изделия внутри документа. Идентификатор должен быть постоянный, т.е. сохраняться при переоткрытии документа.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <returns>Уникальный идентификатор изделия внутри документа</returns>
  protected abstract string DoGetArticleInternalId(SectionEntity articleItem);
}
