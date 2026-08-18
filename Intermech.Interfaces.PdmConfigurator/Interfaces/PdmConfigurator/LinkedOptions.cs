// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.LinkedOptions
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Коллекция связанных значений опций</summary>
[Serializable]
public sealed class LinkedOptions : IAssignable, ICloneable, IXMLStorageLoadSave
{
  /// <summary>Объект для синхронизации</summary>
  private object syncRoot = new object();
  /// <summary>
  /// Связанные значения опций
  /// [Guid основной опции x ID значения основной опции] =&gt; Список [Guid связанной опции x ID значения связанной опции])
  /// </summary>
  public Dictionary<OptionValuePair, List<OptionValuePair>> Items = new Dictionary<OptionValuePair, List<OptionValuePair>>();

  /// <summary>Создать пустой экземпляр класса</summary>
  public LinkedOptions()
  {
  }

  /// <summary>Создать значение опции на основе указанного объекта</summary>
  /// <param name="source">Объект-источник</param>
  public LinkedOptions(object source) => this.Assign(source);

  /// <summary>Является ли элемент пустым</summary>
  public bool Empty
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this.Items.Count == 0;
    }
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true - объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (this == obj)
      return true;
    LinkedOptions linkedOptions = obj as LinkedOptions;
    lock (this.syncRoot)
      return linkedOptions != null && Helper.CompareObjects((object) this.Items, (object) linkedOptions.Items);
  }

  /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this.Items.GetHashCode() << 8 ^ this.Items.Count.GetHashCode();
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    lock (this.syncRoot)
      this.Items.Clear();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is LinkedOptions linkedOptions))
      return;
    lock (this.syncRoot)
      this.Items = (linkedOptions.Clone() as LinkedOptions).Items;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone()
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.Serialize((Stream) serializationStream, (object) this);
      serializationStream.Position = 0L;
      return binaryFormatter.Deserialize((Stream) serializationStream);
    }
  }

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (node == null || node.Name != "f" || !(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper service))
      return;
    lock (this.syncRoot)
    {
      for (int i1 = 0; i1 < node.ChildNodes.Count; ++i1)
      {
        XmlNode childNode1 = node.ChildNodes[i1];
        if (!(childNode1.Name != "g") && childNode1.ChildNodes.Count != 0)
        {
          string attributeValue = xmlStorage.GetAttributeValue(childNode1, "e", "");
          if (!string.IsNullOrEmpty(attributeValue) && attributeValue.Length >= 2 && attributeValue.IndexOf(":") > 0)
          {
            long int64 = StringsHelper.HexToInt64(attributeValue.Substring(0, attributeValue.IndexOf(":")));
            Guid option = service[int64];
            string id = attributeValue.Substring(attributeValue.IndexOf(":") + 1);
            if (!(option == Guid.Empty) && !string.IsNullOrEmpty(id))
            {
              OptionValuePair key = new OptionValuePair(option, id);
              if (!this.Items.ContainsKey(key))
              {
                List<OptionValuePair> list = new List<OptionValuePair>();
                OptionValuePair optionValuePair = new OptionValuePair();
                for (int i2 = 0; i2 < childNode1.ChildNodes.Count; ++i2)
                {
                  XmlNode childNode2 = childNode1.ChildNodes[i2];
                  if (!(childNode2.Name != "g"))
                  {
                    optionValuePair.Load(xmlStorage, childNode2);
                    if (!optionValuePair.Empty && list.IndexOf(optionValuePair) < 0 && string.IsNullOrEmpty(OptionValuePair.FindOptionValue((IList<OptionValuePair>) list, optionValuePair.Option)))
                      list.Add(optionValuePair.Clone() as OptionValuePair);
                  }
                }
                if (list.Count > 0)
                  this.Items[key] = list;
              }
            }
          }
        }
      }
    }
  }

  /// <summary>
  /// Сохранить данные в состав указанного родительского узла
  /// </summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
  public void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode)
  {
    lock (this.syncRoot)
    {
      if (this.Items.Count == 0)
        return;
    }
    this.RemoveInvalidOptions(xmlStorage.Services.GetService(typeof (object)));
    if (!(xmlStorage.Services.GetService(typeof (PdmGuidMapper)) is PdmGuidMapper serviceInstance))
    {
      serviceInstance = new PdmGuidMapper();
      xmlStorage.Services.AddService(typeof (PdmGuidMapper), (object) serviceInstance);
    }
    XmlNode parentNode1 = xmlStorage.AddNode(parentNode, "f");
    lock (this.syncRoot)
    {
      foreach (KeyValuePair<OptionValuePair, List<OptionValuePair>> keyValuePair in this.Items)
      {
        this.IsIncompConflictExists(xmlStorage.Services.GetService(typeof (object)), keyValuePair);
        List<OptionValuePair> list = new List<OptionValuePair>();
        for (int index = keyValuePair.Value.Count - 1; index >= 0; --index)
        {
          OptionValuePair optionValuePair = keyValuePair.Value[index];
          if (list.IndexOf(optionValuePair) < 0 && string.IsNullOrEmpty(OptionValuePair.FindOptionValue((IList<OptionValuePair>) list, optionValuePair.Option)))
            list.Add(optionValuePair);
        }
        if (list.Count != 0)
        {
          XmlNode xmlNode = xmlStorage.AddNode(parentNode1, "g");
          long num = serviceInstance[keyValuePair.Key.Option];
          xmlStorage.SetAttributeValue(xmlNode, "e", $"{StringsHelper.IntToHex(num)}:{keyValuePair.Key.ID}");
          LinkedOptions linkedOptions = this.Clone() as LinkedOptions;
          for (int index = 0; index < list.Count; ++index)
          {
            this.BeforeSave(keyValuePair.Key, list[index], xmlStorage.Services.GetService(typeof (object)));
            linkedOptions.CheckLinkedConflictExist(xmlStorage.Services.GetService(typeof (object)), keyValuePair.Key);
            list[index].Save(xmlStorage, xmlNode);
          }
        }
      }
    }
  }

  /// <summary>
  /// Удалим опции, которые больше не назначены объекту,
  /// либо значения которых невидимы для объекта
  /// </summary>
  private void RemoveInvalidOptions(object holder)
  {
    ObjectOptionsHolder objectOptionsHolder = holder as ObjectOptionsHolder;
    foreach (KeyValuePair<OptionValuePair, List<OptionValuePair>> keyValuePair in new Dictionary<OptionValuePair, List<OptionValuePair>>((IDictionary<OptionValuePair, List<OptionValuePair>>) this.Items))
    {
      OptionValuePair key = keyValuePair.Key;
      long optionId = PdmConfiguratorCache.CacheFindOptionID(key.Option);
      if (!objectOptionsHolder.Options.Contains(optionId))
        this.Items.Remove(key);
      else if (!objectOptionsHolder.VisibleOptionValues.GetVisibleOptionValue(key.Option, key.ID))
        this.Items.Remove(key);
    }
  }

  /// <summary>
  /// Перед сохранением связанного значения проверим, верно ли заполнены поля
  /// </summary>
  /// <param name="parent"> Опция, для которой создан список связанных значений </param>
  /// <param name="child"> Сохраняемое связанное значение  </param>
  /// <param name="holder">Контейнер, которому принадлежит данный критерий</param>
  private void BeforeSave(OptionValuePair parent, OptionValuePair child, object holder)
  {
    ObjectOptionsHolder objectOptionsHolder = holder as ObjectOptionsHolder;
    OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(parent.Option);
    Guid option2;
    string optionCaption1;
    if (option1 != null)
    {
      optionCaption1 = option1.OptionCaption;
    }
    else
    {
      option2 = parent.Option;
      optionCaption1 = option2.ToString();
    }
    string str1 = optionCaption1;
    OptionValue optionValue1 = option1 == null ? (OptionValue) null : option1.OptionValues.FindValue(parent.ID);
    string str2 = optionValue1 != null ? optionValue1.Value : string.Empty;
    OptionHolder option3 = PdmConfiguratorCache.CacheFindOption(child.Option);
    string optionCaption2;
    if (option3 != null)
    {
      optionCaption2 = option3.OptionCaption;
    }
    else
    {
      option2 = child.Option;
      optionCaption2 = option2.ToString();
    }
    string str3 = optionCaption2;
    OptionValue optionValue2 = option3 == null ? (OptionValue) null : option3.OptionValues.FindValue(child.ID);
    string str4 = optionValue2 != null ? optionValue2.Value : string.Empty;
    if (child.Option == Guid.Empty)
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_54"), (object) str2, (object) str1));
    if (string.IsNullOrEmpty(child.ID))
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_55"), (object) str2, (object) str1, (object) str3));
    if (objectOptionsHolder == null)
      return;
    if (option3 != null && objectOptionsHolder.Options.IndexOf(option3.OptionObjectID) < 0)
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_56"), (object) str2, (object) str1, (object) str3));
    if (!objectOptionsHolder.VisibleOptionValues.GetVisibleOptionValue(child.Option, child.ID))
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_57"), (object) str2, (object) str1, (object) str3, (object) str4));
  }

  /// <summary>
  /// проверить наличие конфликта назначенных опций с несвоместимыми
  /// </summary>
  /// <param name="holder"></param>
  /// <param name="item"></param>
  private void IsIncompConflictExists(
    object holder,
    KeyValuePair<OptionValuePair, List<OptionValuePair>> item)
  {
    if (LinkedOptions.IsIncompConflictExists(holder as ObjectOptionsHolder, item.Key, item.Value))
    {
      OptionHolder option = PdmConfiguratorCache.CacheFindOption(item.Key.Option);
      string str1 = option == null ? item.Key.Option.ToString() : option.OptionCaption;
      OptionValue optionValue = option == null ? (OptionValue) null : option.OptionValues.FindValue(item.Key.ID);
      string str2 = optionValue != null ? optionValue.Value : string.Empty;
      throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_58"), (object) str2, (object) str1));
    }
  }

  /// <summary>Добавить/заменить связанное значение опции</summary>
  /// <param name="option">Guid основной опции</param>
  /// <param name="id">ID значения основной опции</param>
  /// <param name="linkedOption">Guid связанной опции</param>
  /// <param name="linkedID">ID значения связанной опции</param>
  /// <returns>true - значение было успешно добавлено/заменено, false - одно из полей не заполнено</returns>
  public bool AddOrReplace(Guid option, string id, Guid linkedOption, string linkedID)
  {
    if (option == Guid.Empty || string.IsNullOrEmpty(id) || linkedOption == Guid.Empty || string.IsNullOrEmpty(linkedID))
      return false;
    OptionValuePair key = new OptionValuePair(option, id);
    OptionValuePair optionValuePair = new OptionValuePair(linkedOption, linkedID);
    lock (this.syncRoot)
    {
      List<OptionValuePair> list = this.Items.ContainsKey(key) ? this.Items[key] : (List<OptionValuePair>) null;
      if (list == null)
      {
        list = new List<OptionValuePair>();
        this.Items[key] = list;
      }
      return OptionValuePair.AddOrReplace((IList<OptionValuePair>) list, optionValuePair);
    }
  }

  /// <summary>Добавить/заменить связанное значение опции</summary>
  /// <param name="key">Основная опция и её значение</param>
  /// <param name="value">Связанная опция и её значние</param>
  /// <returns></returns>
  public bool AddOrReplace(OptionValuePair key, OptionValuePair value)
  {
    return this.AddOrReplace(key.Option, key.ID, value.Option, value.ID);
  }

  /// <summary>Удалить значение связанной опции из списка</summary>
  /// <param name="option">Guid основной опции</param>
  /// <param name="id">ID значения основной опции</param>
  /// <param name="linkedOption">Guid связанной опции</param>
  /// <returns>true - значение успешно удалено</returns>
  public bool Remove(Guid option, string id, Guid linkedOption)
  {
    if (option == Guid.Empty || string.IsNullOrEmpty(id) || linkedOption == Guid.Empty)
      return false;
    OptionValuePair key = new OptionValuePair(option, id);
    lock (this.syncRoot)
      return OptionValuePair.Remove(this.Items.ContainsKey(key) ? (IList<OptionValuePair>) this.Items[key] : (IList<OptionValuePair>) null, linkedOption);
  }

  /// <summary>
  /// Получить список значений связанных опций (всегда вернёт список, даже если в словаре нет информации)
  /// </summary>
  /// <param name="option">Guid основной опции</param>
  /// <param name="id">ID значения основной опции</param>
  /// <returns>Список значений связанных опций</returns>
  public List<OptionValuePair> GetLinkedOptions(Guid option, string id)
  {
    OptionValuePair key = new OptionValuePair(option, id);
    lock (this.syncRoot)
      return this.Items.ContainsKey(key) ? new List<OptionValuePair>((IEnumerable<OptionValuePair>) this.Items[key]) : new List<OptionValuePair>();
  }

  /// <summary>
  /// Получить список значений связанных опций (всегда вернёт список, даже если в словаре нет информации)
  /// </summary>
  /// <param name="key">Основная опции и её значение</param>
  /// <returns>Список значений связанных опций</returns>
  public List<OptionValuePair> GetLinkedOptions(OptionValuePair key)
  {
    return this.GetLinkedOptions(key.Option, key.ID);
  }

  /// <summary>Получить значение связанной опции</summary>
  /// <param name="option">Guid основной опции</param>
  /// <param name="id">ID значения основной опции</param>
  /// <param name="linkedOption">Guid связанной опции</param>
  /// <returns>Значение связанной опции или String.Empty, если информации нет в словарике</returns>
  public string GetLinkedOptionValue(Guid option, string id, Guid linkedOption)
  {
    OptionValuePair key = new OptionValuePair(option, id);
    lock (this.syncRoot)
      return OptionValuePair.FindOptionValue(this.Items.ContainsKey(key) ? (IList<OptionValuePair>) this.Items[key] : (IList<OptionValuePair>) null, linkedOption);
  }

  /// <summary>
  /// проверить, существует ли данная опция со значением в списке связанных
  /// </summary>
  /// <param name="key">опция и её значение</param>
  /// <param name="linked">связанная опция и её значение</param>
  /// <returns></returns>
  private bool IsLinkedOptionValueExist(OptionValuePair key, OptionValuePair linked)
  {
    lock (this.syncRoot)
    {
      List<OptionValuePair> optionValuePairList = this.Items.ContainsKey(key) ? this.Items[key] : (List<OptionValuePair>) null;
      return optionValuePairList != null && optionValuePairList.Count != 0 && !(linked.Option == Guid.Empty) && !string.IsNullOrEmpty(linked.ID) && optionValuePairList.Contains(linked);
    }
  }

  /// <summary>Добавить связанное значение опции</summary>
  /// <param name="key">опция </param>
  /// <param name="linkedOptionValue">связанное значение</param>
  /// <returns>true - значение было успешно добавлено/заменено, false - одно из полей не заполнено</returns>
  private void AddOptionValue(OptionValuePair key, OptionValuePair linkedOptionValue)
  {
    if (key.Option == Guid.Empty || string.IsNullOrEmpty(key.ID) || linkedOptionValue.Option == Guid.Empty || string.IsNullOrEmpty(linkedOptionValue.ID))
      return;
    lock (this.syncRoot)
    {
      List<OptionValuePair> list = this.Items.ContainsKey(key) ? this.Items[key] : (List<OptionValuePair>) null;
      if (list == null)
      {
        list = new List<OptionValuePair>();
        this.Items[key] = list;
      }
      OptionValuePair.AddOptionValue((IList<OptionValuePair>) list, linkedOptionValue);
    }
  }

  /// <summary>найти все опции, связанные с данной</summary>
  /// <param name="optionKey">id </param>
  /// <returns></returns>
  public List<Guid> FindLinkedOptions(Guid optionKey)
  {
    List<Guid> linkedOptions = new List<Guid>();
    if (optionKey == Guid.Empty)
      return linkedOptions;
    lock (this.syncRoot)
    {
      foreach (KeyValuePair<OptionValuePair, List<OptionValuePair>> keyValuePair in this.Items)
      {
        if (keyValuePair.Key.Option == optionKey)
        {
          List<OptionValuePair> optionValuePairList = keyValuePair.Value;
          foreach (OptionValuePair optionValuePair in keyValuePair.Value)
          {
            if (!linkedOptions.Contains(optionValuePair.Option))
              linkedOptions.Add(optionValuePair.Option);
          }
        }
      }
    }
    return linkedOptions;
  }

  /// <summary>собрать все связанные значения для опции</summary>
  /// <param name="optionKey"></param>
  /// <returns></returns>
  public Dictionary<OptionValuePair, List<OptionValuePair>> SelectLinkedOptions(Guid optionKey)
  {
    Dictionary<OptionValuePair, List<OptionValuePair>> dictionary = new Dictionary<OptionValuePair, List<OptionValuePair>>();
    if (optionKey == Guid.Empty)
      return dictionary;
    lock (this.syncRoot)
    {
      foreach (KeyValuePair<OptionValuePair, List<OptionValuePair>> keyValuePair in this.Items)
      {
        OptionValuePair key = keyValuePair.Key;
        if (key.Option == optionKey)
        {
          List<OptionValuePair> optionValuePairList = keyValuePair.Value;
          dictionary.Add(key, optionValuePairList);
        }
      }
    }
    return dictionary;
  }

  private void CheckLinkedConflictExist(object holder, OptionValuePair parent)
  {
    if (!(holder is ObjectOptionsHolder))
      return;
    List<OptionValuePair> optionValuePairList1 = new List<OptionValuePair>();
    List<OptionValuePair> optionValuePairList2 = new List<OptionValuePair>();
    if (this.CheckLinkedConflictExists(parent, optionValuePairList1, optionValuePairList2))
      throw new PdmConfiguratorExeption(LinkedOptions.FormingPathString(optionValuePairList1) + Environment.NewLine + LinkedOptions.FormingPathString(optionValuePairList2));
  }

  /// <summary>
  /// Формирование строки для отображения пути,
  /// который пришлось пройти чтобы достичь значения связанной опции,
  /// указанного в конце списка
  /// </summary>
  /// <param name="currentPath">путь для достижения значения опции</param>
  /// <returns></returns>
  public static string FormingPathString(List<OptionValuePair> currentPath)
  {
    return LinkedOptions.FormingPathString((IUserSession) null, currentPath);
  }

  /// <summary>
  /// Формирование строки для отображения пути,
  /// который пришлось пройти чтобы достичь значения связанной опции,
  /// указанного в конце списка
  /// </summary>
  /// <param name="session"></param>
  /// <param name="currentPath">путь для достижения значения опции</param>
  /// <returns></returns>
  public static string FormingPathString(IUserSession session, List<OptionValuePair> currentPath)
  {
    string str1 = string.Empty;
    for (int index = 0; index < currentPath.Count - 1; ++index)
    {
      OptionValuePair optionValuePair1 = currentPath[index];
      OptionValuePair optionValuePair2 = currentPath[index + 1];
      OptionHolder option1 = PdmConfiguratorCache.CacheFindOption(optionValuePair1.Option);
      if (option1 == null && session != null)
      {
        PdmConfiguratorCache.CacheAddOption(session, optionValuePair1.Option);
        option1 = PdmConfiguratorCache.CacheFindOption(optionValuePair1.Option);
      }
      OptionHolder option2 = PdmConfiguratorCache.CacheFindOption(optionValuePair2.Option);
      if (option2 == null && session != null)
      {
        PdmConfiguratorCache.CacheAddOption(session, optionValuePair2.Option);
        option2 = PdmConfiguratorCache.CacheFindOption(optionValuePair2.Option);
      }
      string str2 = option1 == null ? optionValuePair1.ID.ToString() : option1.OptionCaption;
      string str3 = option2 == null ? optionValuePair2.ID.ToString() : option2.OptionCaption;
      OptionValue optionValue1 = option1 == null ? (OptionValue) null : option1.OptionValues.FindValue(optionValuePair1.ID);
      string str4 = optionValue1 != null ? (string.IsNullOrEmpty(optionValue1.Code) ? optionValue1.Value : $"[{optionValue1.Code}] {optionValue1.Value}") : string.Empty;
      OptionValue optionValue2 = option2 == null ? (OptionValue) null : option2.OptionValues.FindValue(optionValuePair2.ID);
      string str5 = optionValue2 != null ? (string.IsNullOrEmpty(optionValue2.Code) ? optionValue2.Value : $"[{optionValue2.Code}] {optionValue2.Value}") : string.Empty;
      string str6 = string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_59"), (object) str2, (object) str4, (object) str3, (object) str5);
      str1 = str1 + str6 + Environment.NewLine;
    }
    return str1;
  }

  /// <summary>
  /// Проверить, нет ли для данной опции конфликта с другими связаннми опциями
  /// </summary>
  /// <param name="selectedValuePair">Опция и её значение, для которых проверка</param>
  /// <param name="ov1"> опция и значение </param>
  /// <param name="ov2"> опция и значение </param>
  /// <returns></returns>
  public bool CheckLinkedConflictExists(
    OptionValuePair selectedValuePair,
    List<OptionValuePair> ov1,
    List<OptionValuePair> ov2)
  {
    Dictionary<OptionValuePair, List<OptionValuePair>> linearList = this.CreateLinearList(selectedValuePair);
    foreach (OptionValuePair key in linearList.Keys)
    {
      OptionValuePair sameOption = this.FindSameOption(linearList, key);
      if (sameOption != null)
      {
        List<OptionValuePair> collection1 = linearList[key];
        List<OptionValuePair> collection2 = linearList[sameOption];
        if (ov1.Count != 0)
        {
          if (collection2.Contains(ov1[0]) || sameOption.Option == ov1[0].Option)
          {
            ov1.Clear();
            ov1.AddRange((IEnumerable<OptionValuePair>) collection1);
            ov1.Add(key);
            ov2.AddRange((IEnumerable<OptionValuePair>) collection2);
            ov2.Add(sameOption);
            return true;
          }
        }
        else
        {
          ov1.AddRange((IEnumerable<OptionValuePair>) collection1);
          ov1.Add(key);
          ov2.AddRange((IEnumerable<OptionValuePair>) collection2);
          ov2.Add(sameOption);
          return true;
        }
      }
    }
    return false;
  }

  /// <summary>
  /// ищем в словаре ту же опцию, но с другим значением
  /// если найдём - значит пользователь неверно заполнил
  /// связанные значения
  /// </summary>
  /// <param name="dict">список связанных опций</param>
  /// <param name="pair">опция+значение,  которые проверяем</param>
  /// <returns>null - если ничего не найдено, </returns>
  private OptionValuePair FindSameOption(
    Dictionary<OptionValuePair, List<OptionValuePair>> dict,
    OptionValuePair pair)
  {
    foreach (OptionValuePair key in dict.Keys)
    {
      if (key.Option == pair.Option && key.ID != pair.ID)
        return key;
    }
    return (OptionValuePair) null;
  }

  /// <summary>
  ///  проверка на наличие конфликта с несовместимыми опциями
  /// (проверка для всех связанных опций, назначенных данному значению опций )
  /// </summary>
  /// <param name="options"> опции, назначенный объекту </param>
  /// <param name="parentOptionValue">значение опции для которой проводим проверку</param>
  /// <param name="linkedOptionValueList">набор связанных опций со значениями</param>
  /// <returns>true - есть конфликт </returns>
  public static bool IsIncompConflictExists(
    ObjectOptionsHolder options,
    OptionValuePair parentOptionValue,
    List<OptionValuePair> linkedOptionValueList)
  {
    PdmConfiguratorContext context = new PdmConfiguratorContext((PdmConfiguratorContextsCache) null);
    IPdmCriterion criterion = options.Incompatibilities.FindCriterion(parentOptionValue.Option, parentOptionValue.ID);
    if (criterion != null)
    {
      context[parentOptionValue.Option] = parentOptionValue.ID;
      foreach (OptionValuePair linkedOptionValue in linkedOptionValueList)
        context[linkedOptionValue.Option] = linkedOptionValue.ID;
      switch (criterion.Evalute(context))
      {
        case PdmConfiguratorResult.True:
        case PdmConfiguratorResult.Incompatibles:
          return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Сфомировать словарик для связанных опций:
  ///  пара опция-значение - путь к получению этой пары
  ///  (если путей несколько добалвяется только первый - наверное, самый короткий)
  /// </summary>
  /// <param name="parentValuePair">пара, для которой формируется словарик</param>
  /// <returns></returns>
  public Dictionary<OptionValuePair, List<OptionValuePair>> CreateLinearList(
    OptionValuePair parentValuePair)
  {
    Dictionary<OptionValuePair, List<OptionValuePair>> linearResult = new Dictionary<OptionValuePair, List<OptionValuePair>>();
    linearResult.Add(parentValuePair, new List<OptionValuePair>());
    this.AddChildrenPairs(linearResult, parentValuePair);
    return linearResult;
  }

  /// <summary>Добавить в словарь дочерние элементы</summary>
  /// <param name="linearResult"></param>
  /// <param name="parentValuePair">родительская пара</param>
  private void AddChildrenPairs(
    Dictionary<OptionValuePair, List<OptionValuePair>> linearResult,
    OptionValuePair parentValuePair)
  {
    List<OptionValuePair> linkedOptions = this.GetLinkedOptions(parentValuePair);
    List<OptionValuePair> optionValuePairList = new List<OptionValuePair>((IEnumerable<OptionValuePair>) linearResult[parentValuePair]);
    optionValuePairList.Add(parentValuePair);
    foreach (OptionValuePair optionValuePair in linkedOptions)
    {
      if (!linearResult.ContainsKey(optionValuePair))
      {
        linearResult.Add(optionValuePair, optionValuePairList);
        this.AddChildrenPairs(linearResult, optionValuePair);
      }
    }
  }
}
