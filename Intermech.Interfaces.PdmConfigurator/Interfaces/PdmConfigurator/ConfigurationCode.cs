// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.ConfigurationCode
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Localization;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Класс для шифра</summary>
[Serializable]
public class ConfigurationCode : IAssignable, ICloneable, IXMLStorageLoadSave
{
  /// <summary>для потокобезопасного доступа</summary>
  private object syncRoot = new object();
  /// <summary>набор частей из которых состоит шифр</summary>
  private List<CodePartProperties> codeParts = new List<CodePartProperties>();

  /// <summary>Является ли элемент пустым</summary>
  public bool Empty
  {
    get
    {
      lock (this.syncRoot)
        return this.codeParts.Count == 0;
    }
  }

  /// <summary>Набор частей шифра</summary>
  public List<CodePartProperties> CodeParts
  {
    get
    {
      lock (this.syncRoot)
        return this.codeParts;
    }
    set
    {
      lock (this.syncRoot)
        this.codeParts = value;
    }
  }

  /// <summary>Создать пустой экземпляр класса</summary>
  public ConfigurationCode()
  {
  }

  /// <summary>Создать шифр на основе указанного объекта</summary>
  /// <param name="source">Объект-источник</param>
  public ConfigurationCode(object source) => this.Assign(source);

  /// <summary>Загрузить данные из указанного узла настроек</summary>
  /// <param name="xmlStorage">Хранилище настроек</param>
  /// <param name="node">Узел с данными</param>
  public void Load(XMLSettingsStorage xmlStorage, XmlNode node)
  {
    this.Clear();
    if (node == null || node.Name != "i")
      return;
    lock (this.syncRoot)
    {
      for (int i = 0; i < node.ChildNodes.Count; ++i)
      {
        XmlNode childNode = node.ChildNodes[i];
        if (!(childNode.Name != "j"))
        {
          CodePartProperties codePartProperties = new CodePartProperties();
          codePartProperties.Load(xmlStorage, childNode);
          if (!codePartProperties.Empty)
            this.codeParts.Add(codePartProperties);
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
      if (this.codeParts.Count == 0)
        return;
      this.BeforeSave(xmlStorage.Services.GetService(typeof (object)));
    }
    XmlNode parentNode1 = xmlStorage.AddNode(parentNode, "i");
    lock (this.syncRoot)
    {
      for (int index = 0; index < this.codeParts.Count; ++index)
        this.codeParts[index].Save(xmlStorage, parentNode1);
    }
  }

  /// <summary>проверки перед сохранением</summary>
  /// <param name="holder">Контейнер, которому принадлежит данный критерий</param>
  private void BeforeSave(object holder)
  {
    ObjectOptionsHolder objectOptionsHolder = holder as ObjectOptionsHolder;
    foreach (CodePartProperties codePart in this.CodeParts)
    {
      if (codePart.codePartType == CodePartType.Undefined || codePart.codePartValue == null)
        throw new PdmConfiguratorExeption(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_50"));
      if (codePart.codePartType == CodePartType.OptionValueCode || codePart.codePartType == CodePartType.OptionCode)
      {
        long int64 = Convert.ToInt64(codePart.codePartValue);
        if (!objectOptionsHolder.Options.Contains(int64))
        {
          OptionHolder option = PdmConfiguratorCache.CacheFindOption(int64);
          string str = option == null ? $"c id={int64}" : $"\"{option.OptionCaption}\"";
          throw new PdmConfiguratorExeption(string.Format(LocalizationHolder.rm.GetString("Interfaces.PdmConfigurator_51"), (object) str));
        }
      }
    }
  }

  /// <summary>Очистить поля класса</summary>
  public void Clear()
  {
    lock (this.syncRoot)
      this.codeParts.Clear();
  }

  /// <summary>
  /// Скопировать информацию из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public void Assign(object source)
  {
    if (this == source)
      return;
    this.Clear();
    if (!(source is ConfigurationCode configurationCode))
      return;
    lock (this.syncRoot)
      this.CodeParts = new List<CodePartProperties>((IEnumerable<CodePartProperties>) configurationCode.CodeParts);
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new ConfigurationCode((object) this);

  /// <summary>добавить описание части шифра</summary>
  /// <param name="type"></param>
  /// <param name="value"></param>
  public void AddCodePart(CodePartType type, object value)
  {
    lock (this.codeParts)
      this.codeParts.Add(new CodePartProperties(type, value));
  }

  /// <summary>Преобразовать шифр объекта в строку</summary>
  /// <param name="relation"> конфигурируемая связь,
  /// которой объект входит в родительский состав</param>
  /// <param name="obj">объект, шифр которого преобразуем</param>
  /// <param name="session">сессия </param>
  /// <returns></returns>
  public static string BuildConfigurationCode(
    IDBRelation relation,
    IDBObject obj,
    IUserSession session)
  {
    ObjectOptionsHolder objectOptionsHolder = new ObjectOptionsHolder((object) obj);
    ConfigurationCode configurationCode = objectOptionsHolder.Incompatibilities.ConfigurationCode;
    List<string> stringList = new List<string>();
    foreach (CodePartProperties codePart in configurationCode.CodeParts)
    {
      if (!codePart.Empty && codePart.codePartType != CodePartType.Undefined && codePart.codePartValue != null)
      {
        if (codePart.codePartType == CodePartType.FixedText)
          stringList.Add(codePart.codePartValue.ToString());
        else if (codePart.codePartType == CodePartType.ObjectAttribute)
        {
          IDBAttribute attributeById = obj.GetAttributeByID(Convert.ToInt32(codePart.codePartValue));
          if (attributeById != null)
            stringList.Add(attributeById.AsString);
        }
        else
        {
          long int64 = Convert.ToInt64(codePart.codePartValue);
          if (objectOptionsHolder.Options.Contains(int64))
          {
            OptionHolder option = PdmConfiguratorCache.CacheFindOption(int64);
            if (option == null)
            {
              PdmConfiguratorCache.CacheAddOption(session, int64);
              option = PdmConfiguratorCache.CacheFindOption(int64);
            }
            if (option != null)
            {
              if (codePart.codePartType == CodePartType.OptionCode)
              {
                if (!string.IsNullOrEmpty(option.OptionCode))
                  stringList.Add(option.OptionCode);
              }
              else
              {
                PdmConfiguratorContext configuratorContext = new PdmConfiguratorContext((object) relation);
                if (configuratorContext.OptionsValues.ContainsKey(option.OptionGuid))
                {
                  string optionsValue = configuratorContext.OptionsValues[option.OptionGuid];
                  OptionValue optionValue = option.OptionValues.FindValue(optionsValue);
                  if (optionValue != null && !string.IsNullOrEmpty(optionValue.Code))
                    stringList.Add(optionValue.Code);
                }
              }
            }
          }
        }
      }
    }
    return string.Join("", stringList.ToArray());
  }

  public static string BuildConfigurationCode(
    long objectVersionID,
    PdmConfiguratorContext pdmConfiguratorContext)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    if (pdmConfiguratorContext == null)
      throw new ArgumentNullException(nameof (pdmConfiguratorContext));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject source = sessionKeeper.Session.GetObject(objectVersionID);
      ObjectOptionsHolder objectOptionsHolder = new ObjectOptionsHolder((object) source);
      ConfigurationCode configurationCode = objectOptionsHolder.Incompatibilities.ConfigurationCode;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (CodePartProperties codePart in configurationCode.CodeParts)
      {
        if (!codePart.Empty && codePart.codePartType != CodePartType.Undefined && codePart.codePartValue != null)
        {
          if (codePart.codePartType == CodePartType.FixedText)
            stringBuilder.Append(codePart.codePartValue);
          else if (codePart.codePartType == CodePartType.ObjectAttribute)
          {
            IDBAttribute attributeById = source.GetAttributeByID(Convert.ToInt32(codePart.codePartValue));
            if (attributeById != null)
              stringBuilder.Append(attributeById.AsString);
          }
          else
          {
            long int64 = Convert.ToInt64(codePart.codePartValue);
            if (objectOptionsHolder.Options.Contains(int64))
            {
              OptionHolder option = PdmConfiguratorCache.CacheFindOption(int64);
              if (option == null)
              {
                PdmConfiguratorCache.CacheAddOption(sessionKeeper.Session, int64);
                option = PdmConfiguratorCache.CacheFindOption(int64);
              }
              if (option != null)
              {
                if (codePart.codePartType == CodePartType.OptionCode)
                {
                  if (!string.IsNullOrEmpty(option.OptionCode))
                    stringBuilder.Append(option.OptionCode);
                }
                else
                {
                  string optionValue1 = pdmConfiguratorContext.GetOptionValue(option.OptionGuid);
                  if (optionValue1 != null)
                  {
                    OptionValue optionValue2 = option.OptionValues.FindValue(optionValue1);
                    if (optionValue2 != null && !string.IsNullOrEmpty(optionValue2.Code))
                      stringBuilder.Append(optionValue2.Code);
                  }
                }
              }
            }
          }
        }
      }
      return stringBuilder.ToString();
    }
  }
}
