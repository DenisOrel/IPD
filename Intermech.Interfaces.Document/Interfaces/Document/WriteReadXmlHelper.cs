// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.WriteReadXmlHelper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс для чтения/записи XML</summary>
public class WriteReadXmlHelper
{
  /// <summary>Загрузить элемент из XML</summary>
  /// <param name="element">Элемент данные которого нужно загрузить</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public static void ReadFromXml(IWriteReadXml element, XmlReadArgs readArgs)
  {
    if (element == null)
      throw new ArgumentNullException(nameof (element));
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    IUnknownXmlElement unknownXmlElement = element as IUnknownXmlElement;
    if (readArgs.Reader.HasAttributes)
    {
      int i = 0;
      for (int attributeCount = readArgs.Reader.AttributeCount; i < attributeCount; ++i)
      {
        readArgs.Reader.MoveToAttribute(i);
        if (!element.ReadFieldFromXml(readArgs) && unknownXmlElement != null)
          unknownXmlElement.AddUnknownXmlAttribute(readArgs.Reader.LocalName, readArgs.Reader.Value);
      }
      readArgs.Reader.MoveToElement();
    }
    bool flag = readArgs.Reader.IsEmptyElement;
    while (!flag && (readArgs.SkipRead || readArgs.Reader.Read()))
    {
      readArgs.SkipRead = false;
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          if (!element.ReadFieldFromXml(readArgs) && unknownXmlElement != null)
          {
            unknownXmlElement.UnknownXmlElements += readArgs.Reader.ReadOuterXml();
            readArgs.SkipRead = true;
            continue;
          }
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    if (flag)
      return;
    LogManager.AddLine("WriteReadXmlHelper.ReadFromXml End Element not found.");
  }

  /// <summary>Прочитать типизированный XML элемент</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Загруженный элемент</returns>
  public static IWriteReadXml ReadTypedElementFromXml(XmlReadArgs readArgs)
  {
    Type type = (Type) null;
    constructorDelegate2 = (EmptyConstructorDelegate) null;
    string str = "";
    if (readArgs.Reader.MoveToAttribute("type"))
    {
      str = readArgs.Reader.Value;
      if (str != null && str != "" && !(DocumentTreeNode.TypeConstructorDictionary[(object) str] is EmptyConstructorDelegate constructorDelegate2))
        type = WriteReadXmlHelper.GetTypeForTypeName(str);
      if (constructorDelegate2 == null && type == (Type) null && readArgs.Reader.MoveToAttribute("baseType"))
      {
        str = readArgs.Reader.Value;
        if (str != null && str != "" && !(DocumentTreeNode.TypeConstructorDictionary[(object) str] is EmptyConstructorDelegate constructorDelegate2))
          type = WriteReadXmlHelper.GetTypeForTypeName(str);
        if (constructorDelegate2 == null && type == (Type) null)
          LogManager.AddLine(string.Format(LocalizationHolder.rm.GetString("Interfaces.Document_133"), (object) str));
      }
    }
    else
      LogManager.AddLine(LocalizationHolder.rm.GetString("Interfaces.Document_134"));
    readArgs.Reader.MoveToElement();
    IWriteReadXml writeReadXml = (IWriteReadXml) null;
    if (constructorDelegate2 != null)
      writeReadXml = constructorDelegate2() as IWriteReadXml;
    else if (type != (Type) null)
      writeReadXml = Activator.CreateInstance(type, true) as IWriteReadXml;
    writeReadXml?.ReadFromXml(readArgs);
    return writeReadXml != null ? writeReadXml : throw new Exception($"{LocalizationHolder.rm.GetString("Interfaces.Document_182")}{str}{LocalizationHolder.rm.GetString("Interfaces.Document_183")}{readArgs.Version.ToString()}]");
  }

  /// <summary>Сохранить IWriteReadXml element как элемент XML</summary>
  /// <param name="name">Имя элемента XML</param>
  /// <param name="element">Сохраняемый объект</param>
  /// <param name="skipNull">Не записывать тэг, если element == null</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public static void WriteXmlElement(
    string name,
    IWriteReadXml element,
    bool skipNull,
    XmlWriter xw,
    ObjectIDGenerator objectRefId)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (xw == null)
      throw new ArgumentNullException(nameof (xw));
    if (element != null)
      element.WriteToXml(name, xw, objectRefId);
    else if (!skipNull)
      throw new ArgumentNullException(nameof (element));
  }

  /// <summary>Записать текстовый словарь в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="dictionary">Словарь</param>
  /// <param name="dicItemName">Имя элемента XML для записи словаря</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public static void WriteStringDictionaryToXml(
    string elementName,
    IDictionary dictionary,
    string dicItemName,
    XmlWriter xw,
    ObjectIDGenerator objectRefId)
  {
    if (elementName == null)
      throw new ArgumentNullException(nameof (elementName));
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    if (dicItemName == null)
      throw new ArgumentNullException(nameof (dicItemName));
    if (xw == null)
      throw new ArgumentNullException(nameof (xw));
    xw.WriteStartElement(elementName);
    foreach (DictionaryEntry dictionaryEntry in dictionary)
    {
      xw.WriteStartElement(dicItemName);
      xw.WriteAttributeString("key", dictionaryEntry.Key.ToString());
      if (dictionaryEntry.Value != null)
      {
        if (dictionaryEntry.Value is AddAttrValue addAttrValue)
        {
          xw.WriteAttributeString("valtype", addAttrValue.Type?.ToString() ?? "");
          xw.WriteAttributeString("converter", addAttrValue.ConverterType ?? "");
          xw.WriteAttributeString("show", addAttrValue.IsShownInPropertyGrid ? "1" : "0");
        }
        xw.WriteValue(dictionaryEntry.Value.ToString());
      }
      xw.WriteEndElement();
    }
    xw.WriteEndElement();
  }

  /// <summary>Загрузить текстовый словарь из XML</summary>
  /// <param name="dictionary">Словарь</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public static void ReadStringDictionaryFromXml(IDictionary dictionary, XmlReadArgs readArgs)
  {
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    bool flag = readArgs.Reader.IsEmptyElement;
    while (!flag && readArgs.Reader.Read())
    {
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          WriteReadXmlHelper.ReadStringDictionaryElementFromXml(dictionary, readArgs);
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }

  /// <summary>Загрузить элемент строкового словаря из XML</summary>
  /// <param name="dictionary">Словарь</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  protected static void ReadStringDictionaryElementFromXml(
    IDictionary dictionary,
    XmlReadArgs readArgs)
  {
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    string typeName = (string) null;
    string str1 = (string) null;
    bool flag1 = false;
    if (!readArgs.Reader.HasAttributes)
      throw new Exception(LocalizationHolder.rm.GetString("Interfaces.Document_135"));
    readArgs.Reader.MoveToAttribute("key");
    string key = readArgs.Reader.Value;
    if (readArgs.Reader.MoveToAttribute("valtype"))
      typeName = readArgs.Reader.Value;
    if (readArgs.Reader.MoveToAttribute("converter"))
      str1 = readArgs.Reader.Value;
    if (readArgs.Reader.MoveToAttribute("show"))
      flag1 = readArgs.Reader.Value == "1";
    readArgs.Reader.MoveToElement();
    bool flag2 = readArgs.Reader.IsEmptyElement;
    bool flag3 = false;
    while (!flag2 && readArgs.Reader.Read())
    {
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Text:
        case XmlNodeType.Whitespace:
          if (readArgs.Version >= 41 || readArgs.Reader.NodeType != XmlNodeType.Whitespace)
          {
            string str2 = readArgs.Reader.Value;
            if (typeName == null)
            {
              dictionary[(object) key] = (object) str2;
            }
            else
            {
              AddAttrValue addAttrValue1 = new AddAttrValue();
              addAttrValue1.Type = Type.GetType(typeName);
              addAttrValue1.ConverterType = str1;
              addAttrValue1.IsShownInPropertyGrid = flag1;
              AddAttrValue addAttrValue2 = addAttrValue1;
              addAttrValue2.Value = (object) str2;
              dictionary[(object) key] = (object) addAttrValue2;
            }
            flag3 = true;
            continue;
          }
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag2 = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    if (flag3)
      return;
    dictionary.Add((object) key, (object) null);
  }

  /// <summary>Записать словарь в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="dictionary">Словарь</param>
  /// <param name="dicItemName">Имя элемента XML для записи словаря</param>
  /// <param name="dicValueName">Имя элемента XML для значений записи словаря</param>
  /// <param name="dicKeyType">Тип ключа словаря</param>
  /// <param name="dicValueType">Тип значения словаря</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public static void WriteDictionaryToXml(
    string elementName,
    IDictionary dictionary,
    string dicItemName,
    string dicValueName,
    Type dicKeyType,
    Type dicValueType,
    XmlWriter xw,
    ObjectIDGenerator objectRefId)
  {
    WriteReadXmlHelper.WriteDictionaryToXml(elementName, dictionary, dicItemName, dicValueName, (string) null, (IList<Type>) new List<Type>(1)
    {
      dicKeyType
    }, (IList<Type>) new List<Type>(1) { dicValueType }, xw, objectRefId);
  }

  /// <summary>Записать словарь в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="dictionary">Словарь</param>
  /// <param name="dicItemName">Имя элемента XML для записи словаря</param>
  /// <param name="dicValueName">Имя элемента XML для значений записи словаря</param>
  /// <param name="parentKey">Ключ в родительском словаре, если он есть</param>
  /// <param name="dicKeyTypes">Список типов ключей для этого и вложенных словарей. В порядке вложенности - 0 самый нижний уровень</param>
  /// <param name="dicValueTypes">Список типов значений для этого и вложенных словарей. В порядке вложенности - 0 самый нижний уровень</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public static void WriteDictionaryToXml(
    string elementName,
    IDictionary dictionary,
    string dicItemName,
    string dicValueName,
    string parentKey,
    IList<Type> dicKeyTypes,
    IList<Type> dicValueTypes,
    XmlWriter xw,
    ObjectIDGenerator objectRefId)
  {
    if (elementName == null)
      throw new ArgumentNullException(nameof (elementName));
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    if (dicItemName == null)
      throw new ArgumentNullException(nameof (dicItemName));
    if (dicValueName == null)
      throw new ArgumentNullException(nameof (dicValueName));
    if (dicKeyTypes == null)
      throw new ArgumentNullException("dicKeyType");
    if (dicValueTypes == null)
      throw new ArgumentNullException("dicValueType");
    if (xw == null)
      throw new ArgumentNullException(nameof (xw));
    xw.WriteStartElement(elementName);
    if (parentKey != null && parentKey != "")
      xw.WriteAttributeString("key", parentKey);
    string str = "";
    Type dicKeyType = dicKeyTypes[dicKeyTypes.Count - 1];
    if (!DocumentTreeNode.TypeAliasDictionary.TryGetValue(dicKeyType, out str) || str == null || str == "")
      str = dicKeyType.Name;
    xw.WriteAttributeString("keyType", str);
    str = "";
    Type dicValueType = dicValueTypes[dicValueTypes.Count - 1];
    if (!DocumentTreeNode.TypeAliasDictionary.TryGetValue(dicValueType, out str) || str == null || str == "")
      str = dicValueType.Name;
    xw.WriteAttributeString("valueType", str);
    foreach (DictionaryEntry dictionaryEntry1 in dictionary)
    {
      if (dictionaryEntry1.Value == null)
      {
        xw.WriteStartElement(dicItemName);
        xw.WriteAttributeString("key", dictionaryEntry1.Key.ToString());
        xw.WriteElementString(dicValueName, (string) null);
        xw.WriteEndElement();
      }
      else if (dictionaryEntry1.Value is string)
      {
        xw.WriteStartElement(dicItemName);
        xw.WriteAttributeString("key", dictionaryEntry1.Key.ToString());
        xw.WriteElementString(dicValueName, (string) dictionaryEntry1.Value);
        xw.WriteEndElement();
      }
      else if (dictionaryEntry1.Value is Guid)
      {
        xw.WriteStartElement(dicItemName);
        xw.WriteAttributeString("key", dictionaryEntry1.Key.ToString());
        xw.WriteElementString(dicValueName, dictionaryEntry1.Value.ToString());
        xw.WriteEndElement();
      }
      else if (dictionaryEntry1.Value is IWriteReadXml writeReadXml)
      {
        xw.WriteStartElement(dicItemName);
        xw.WriteAttributeString("key", dictionaryEntry1.Key.ToString());
        writeReadXml.WriteToXml(dicValueName, xw, objectRefId);
        xw.WriteEndElement();
      }
      else if (dictionaryEntry1.Value is IDictionary dictionary1)
      {
        List<Type> dicKeyTypes1 = new List<Type>((IEnumerable<Type>) dicKeyTypes);
        List<Type> dicValueTypes1 = new List<Type>((IEnumerable<Type>) dicValueTypes);
        if (dicKeyTypes1.Count > 0)
        {
          dicKeyTypes1.RemoveAt(dicKeyTypes1.Count - 1);
          dicValueTypes1.RemoveAt(dicValueTypes1.Count - 1);
        }
        if (dicKeyTypes1.Count == 0)
        {
          foreach (DictionaryEntry dictionaryEntry2 in dictionary1)
          {
            if (dictionaryEntry2.Value != null)
            {
              dicKeyTypes1.Add(dictionaryEntry2.Key.GetType());
              dicValueTypes1.Add(dictionaryEntry2.Value.GetType());
              break;
            }
          }
        }
        WriteReadXmlHelper.WriteDictionaryToXml(dicItemName, dictionary1, "Item", "value", dictionaryEntry1.Key.ToString(), (IList<Type>) dicKeyTypes1, (IList<Type>) dicValueTypes1, xw, objectRefId);
      }
    }
    xw.WriteEndElement();
  }

  /// <summary>Загрузить словарь из XML</summary>
  /// <param name="dictionary">Словарь</param>
  /// <param name="keyType">Тип ключа</param>
  /// <param name="elementType">Тип значения</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public static void ReadDictionaryFromXml(
    IDictionary dictionary,
    Type keyType,
    Type elementType,
    XmlReadArgs readArgs)
  {
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    bool flag = readArgs.Reader.IsEmptyElement;
    if (keyType == (Type) null && readArgs.Reader.MoveToAttribute(nameof (keyType)) && readArgs.Reader.HasValue)
    {
      string typeName = readArgs.Reader.Value;
      if (typeName != "")
        keyType = WriteReadXmlHelper.GetTypeForTypeName(typeName);
    }
    if (elementType == (Type) null && readArgs.Reader.MoveToAttribute("valueType") && readArgs.Reader.HasValue)
    {
      string typeName = readArgs.Reader.Value;
      if (typeName != "")
        elementType = WriteReadXmlHelper.GetTypeForTypeName(typeName);
    }
    readArgs.Reader.MoveToElement();
    while (!flag && readArgs.Reader.Read())
    {
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          WriteReadXmlHelper.ReadDictionaryElementFromXml(dictionary, keyType, elementType, readArgs);
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }

  public static Type GetTypeForTypeName(string typeName)
  {
    Type typeForTypeName = typeName != null && !(typeName == "") ? DocumentTreeNode.TypeNameDictionary[(object) typeName] as Type : throw new ArgumentNullException(nameof (typeName));
    if (typeForTypeName == (Type) null)
      typeForTypeName = Type.GetType($"{typeof (ReferenceBase).Namespace}.{typeName}", false);
    if (typeForTypeName == (Type) null)
      typeForTypeName = Type.GetType("System." + typeName, false);
    return typeForTypeName;
  }

  /// <summary>Загрузить элемент словаря из XML</summary>
  /// <param name="dictionary">Словарь</param>
  /// <param name="keyType">Тип ключа</param>
  /// <param name="elementType">Тип значения</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  protected static void ReadDictionaryElementFromXml(
    IDictionary dictionary,
    Type keyType,
    Type elementType,
    XmlReadArgs readArgs)
  {
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    object key = (object) null;
    Type keyType1 = (Type) null;
    Type elementType1 = (Type) null;
    if (readArgs.Reader.HasAttributes)
    {
      if (readArgs.Reader.MoveToAttribute(nameof (keyType)) && readArgs.Reader.HasValue)
      {
        string typeName = readArgs.Reader.Value;
        if (typeName != "")
          keyType1 = WriteReadXmlHelper.GetTypeForTypeName(typeName);
      }
      if (readArgs.Reader.MoveToAttribute("valueType") && readArgs.Reader.HasValue)
      {
        string typeName = readArgs.Reader.Value;
        if (typeName != "")
          elementType1 = WriteReadXmlHelper.GetTypeForTypeName(typeName);
      }
      if (readArgs.Reader.MoveToAttribute("key"))
        key = !(keyType == typeof (Guid)) ? (!(keyType == typeof (long)) ? (!(keyType == typeof (int)) ? (!typeof (Enum).IsAssignableFrom(keyType) ? Convert.ChangeType((object) readArgs.Reader.Value, keyType) : Enum.Parse(keyType, readArgs.Reader.Value)) : (object) Convert.ToInt32(readArgs.Reader.Value)) : (object) Convert.ToInt64(readArgs.Reader.Value)) : (object) new Guid(readArgs.Reader.Value);
      readArgs.Reader.MoveToElement();
    }
    bool flag1 = readArgs.Reader.IsEmptyElement;
    IDictionary dictionary1 = (IDictionary) null;
    bool itemIsIWriteReadXml = typeof (IWriteReadXml).IsAssignableFrom(elementType);
    bool flag2 = typeof (IDictionary).IsAssignableFrom(elementType);
    while (!flag1 && readArgs.Reader.Read())
    {
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
        case XmlNodeType.Text:
          if (flag2)
          {
            if (dictionary1 == null)
              dictionary1 = (IDictionary) Activator.CreateInstance(elementType, true);
            WriteReadXmlHelper.ReadDictionaryElementFromXml(dictionary1, keyType1, elementType1, readArgs);
            dictionary[key] = (object) dictionary1;
            continue;
          }
          object obj = WriteReadXmlHelper.ReadItemFromXml(elementType, itemIsIWriteReadXml, readArgs);
          dictionary[key] = obj;
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag1 = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }

  /// <summary>Записать массив в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="array">Массив</param>
  /// <param name="arrayItemName">Имя элемента массива</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public static void WriteArrayToXml(
    string elementName,
    IList array,
    string arrayItemName,
    XmlWriter xw,
    ObjectIDGenerator objectRefId)
  {
    if (elementName == null)
      throw new ArgumentNullException(nameof (elementName));
    if (array == null)
      throw new ArgumentNullException(nameof (array));
    if (arrayItemName == null)
      throw new ArgumentNullException(nameof (arrayItemName));
    if (xw == null)
      throw new ArgumentNullException(nameof (xw));
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("length", array.Count.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    for (int index = 0; index < array.Count; ++index)
    {
      if (array[index] is IWriteReadXml writeReadXml)
        writeReadXml.WriteToXml(arrayItemName, xw, objectRefId);
      else if (array[index] != null)
        xw.WriteElementString(arrayItemName, array[index].ToString());
      else
        xw.WriteElementString(arrayItemName, "");
    }
    xw.WriteEndElement();
  }

  /// <summary>Загрузить элемент массива, списка или словаря</summary>
  /// <param name="itemType">Тип элемента</param>
  /// <param name="elementIsIWriteReadXml">Элемент типа IWriteReadXml</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns></returns>
  public static object ReadItemFromXml(
    Type itemType,
    bool itemIsIWriteReadXml,
    XmlReadArgs readArgs)
  {
    object obj = (object) null;
    if (readArgs.Reader.LocalName != "null")
    {
      if (itemIsIWriteReadXml)
      {
        obj = Activator.CreateInstance(itemType, true);
        ((IWriteReadXml) obj).ReadFromXml(readArgs);
      }
      else if (!readArgs.Reader.IsEmptyElement)
      {
        if (!readArgs.Reader.HasValue)
          readArgs.Reader.Read();
        obj = !(itemType == typeof (Guid)) ? (!(itemType == typeof (long)) ? (!(itemType == typeof (int)) ? (!(itemType == typeof (double)) ? (!(itemType == typeof (string)) ? Convert.ChangeType((object) readArgs.Reader.Value, itemType) : (object) readArgs.Reader.Value) : (object) Convert.ToDouble(readArgs.Reader.Value)) : (object) Convert.ToInt32(readArgs.Reader.Value)) : (object) Convert.ToInt64(readArgs.Reader.Value)) : (object) new Guid(readArgs.Reader.Value);
      }
    }
    return obj;
  }

  /// <summary>Загрузить массив из XML</summary>
  /// <param name="itemType">Тип элемента</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Массив</returns>
  public static Array ReadArrayFromXml(Type itemType, XmlReadArgs readArgs)
  {
    if (itemType == (Type) null)
      throw new ArgumentNullException(nameof (itemType));
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    bool flag = readArgs.Reader.IsEmptyElement;
    int num = 0;
    int length = 0;
    if (readArgs.Reader.HasAttributes)
    {
      readArgs.Reader.MoveToAttribute("length");
      length = Convert.ToInt32(readArgs.Reader.Value);
      readArgs.Reader.MoveToElement();
    }
    Array instance = Array.CreateInstance(itemType, length);
    bool itemIsIWriteReadXml = typeof (IWriteReadXml).IsAssignableFrom(itemType);
    while (!flag && readArgs.Reader.Read())
    {
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          object obj = WriteReadXmlHelper.ReadItemFromXml(itemType, itemIsIWriteReadXml, readArgs);
          instance.SetValue(obj, num++);
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    return instance;
  }

  /// <summary>Записать список в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="list">Массив</param>
  /// <param name="listItemName">Имя элемента массива</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public static void WriteListToXml(
    string elementName,
    IList list,
    string listItemName,
    XmlWriter xw,
    ObjectIDGenerator objectRefId)
  {
    if (elementName == null)
      throw new ArgumentNullException(nameof (elementName));
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (listItemName == null)
      throw new ArgumentNullException(nameof (listItemName));
    if (xw == null)
      throw new ArgumentNullException(nameof (xw));
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("count", list.Count.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index] is IWriteReadXml writeReadXml)
        writeReadXml.WriteToXml(listItemName, xw, objectRefId);
      else if (list[index] != null)
        xw.WriteElementString(listItemName, list[index].ToString());
      else
        xw.WriteElementString(listItemName, "");
    }
    xw.WriteEndElement();
  }

  /// <summary>Загрузить типизированый список из XML</summary>
  /// <param name="list">Список</param>
  /// <param name="itemType">Тип значения</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public static void ReadListFromXml(IList list, Type itemType, XmlReadArgs readArgs)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (itemType == (Type) null)
      throw new ArgumentNullException(nameof (itemType));
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    bool flag = readArgs.Reader.IsEmptyElement;
    bool itemIsIWriteReadXml = typeof (IWriteReadXml).IsAssignableFrom(itemType);
    while (!flag && readArgs.Reader.Read())
    {
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          object obj = WriteReadXmlHelper.ReadItemFromXml(itemType, itemIsIWriteReadXml, readArgs);
          list.Add(obj);
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }

  /// <summary>Записать список в XML</summary>
  /// <param name="elementName">Имя элемента XML</param>
  /// <param name="list">Массив</param>
  /// <param name="listItemName">Имя элемента массива</param>
  /// <param name="xw">XmlWriter</param>
  public static void WriteStringListToXml(
    string elementName,
    List<string> list,
    string listItemName,
    XmlWriter xw)
  {
    if (elementName == null)
      throw new ArgumentNullException(nameof (elementName));
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (listItemName == null)
      throw new ArgumentNullException(nameof (listItemName));
    if (xw == null)
      throw new ArgumentNullException(nameof (xw));
    xw.WriteStartElement(elementName);
    xw.WriteAttributeString("count", list.Count.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    for (int index = 0; index < list.Count; ++index)
      xw.WriteElementString(listItemName, list[index]);
    xw.WriteEndElement();
  }

  /// <summary>Загрузить типизированный список из XML</summary>
  /// <param name="list">Список</param>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public static void ReadStringListFromXml(List<string> list, XmlReadArgs readArgs)
  {
    if (list == null)
      throw new ArgumentNullException(nameof (list));
    if (readArgs == null)
      throw new ArgumentNullException(nameof (readArgs));
    string localName = readArgs.Reader.LocalName;
    bool flag = readArgs.Reader.IsEmptyElement;
    while (!flag && readArgs.Reader.Read())
    {
      switch (readArgs.Reader.NodeType)
      {
        case XmlNodeType.Element:
          if (!readArgs.Reader.HasValue)
            readArgs.Reader.Read();
          list.Add(readArgs.Reader.Value);
          continue;
        case XmlNodeType.EndElement:
          if (localName == readArgs.Reader.LocalName)
          {
            flag = true;
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }

  /// <summary>Записать поток как Base64 в текущий XML элемент</summary>
  /// <param name="stream">Поток данных</param>
  /// <param name="xw">XmlWriter</param>
  public static void WriteBase64ToCurrentXmlElement(Stream stream, XmlWriter xw)
  {
    if (stream == null)
      throw new ArgumentNullException(nameof (stream));
    if (xw == null)
      throw new ArgumentNullException(nameof (xw));
    int count1 = 65536 /*0x010000*/;
    byte[] buffer = new byte[count1];
    int count2;
    do
    {
      count2 = stream.Read(buffer, 0, count1);
      xw.WriteBase64(buffer, 0, count2);
    }
    while (count1 <= count2);
  }

  /// <summary>Записать поток как Base64 в текущий XML элемент</summary>
  /// <param name="stream">Поток данных</param>
  /// <param name="xw">XmlWriter</param>
  public static void ReadBase64FromCurrentXmlElement(
    Stream outStream,
    XmlReader xr,
    int bufferSize = 65536 /*0x010000*/)
  {
    if (outStream == null)
      throw new ArgumentNullException(nameof (outStream));
    if (xr == null)
      throw new ArgumentNullException(nameof (xr));
    byte[] buffer = new byte[bufferSize];
    int count;
    do
    {
      count = xr.ReadContentAsBase64(buffer, 0, bufferSize);
      outStream.Write(buffer, 0, count);
    }
    while (bufferSize <= count);
  }

  /// <summary>Записать rootObject в XML документ</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="rootObject">Корневой объект, который сохраняется в XML</param>
  /// <param name="rootElementName">Имя корневого объекта в XML</param>
  public static void WriteXmlDocument(
    string fileName,
    IWriteReadXml rootObject,
    string rootElementName)
  {
    WriteReadXmlHelper.WriteXmlDocument((XmlWriter) new XmlTextWriter(fileName, Encoding.UTF8), rootObject, rootElementName);
  }

  /// <summary>Записать rootObject в XML документ</summary>
  /// <param name="stream">Поток документа</param>
  /// <param name="rootObject">Корневой объект, который сохраняется в XML</param>
  /// <param name="rootElementName">Имя корневого объекта в XML</param>
  public static void WriteXmlDocument(
    Stream stream,
    IWriteReadXml rootObject,
    string rootElementName)
  {
    WriteReadXmlHelper.WriteXmlDocument((XmlWriter) new XmlTextWriter(stream, Encoding.UTF8), rootObject, rootElementName);
  }

  /// <summary> Записать состояние объекта, способного сохраняться в XML в строку base64 </summary>
  /// <param name="rootObject">Корневой объект, который сохраняется в XML</param>
  /// <param name="rootElementName">Имя корневого объекта в XML</param>
  /// <returns> Состояние объекта в виде строки содержащей xml документ преобразованый к Base64 строке </returns>
  public static string WriteXmlObjectStateToBase64String(
    IWriteReadXml rootObject,
    string rootElementName)
  {
    StringBuilder output = new StringBuilder();
    WriteReadXmlHelper.WriteXmlDocument(XmlWriter.Create(output, new XmlWriterSettings()), rootObject, rootElementName);
    return Convert.ToBase64String(Encoding.UTF8.GetBytes(output.ToString()));
  }

  /// <summary>Записать rootObject в XML документ</summary>
  /// <param name="xw">XmlTextWriter</param>
  /// <param name="rootObject">Корневой объект, который сохраняется в XML</param>
  /// <param name="rootElementName">Имя корневого объекта в XML</param>
  public static void WriteXmlDocument(
    XmlWriter xw,
    IWriteReadXml rootObject,
    string rootElementName)
  {
    try
    {
      if (xw is XmlTextWriter)
      {
        ((XmlTextWriter) xw).Formatting = Formatting.Indented;
        ((XmlTextWriter) xw).Indentation = 3;
      }
      xw.WriteStartDocument();
      ObjectIDGenerator objectRefId = new ObjectIDGenerator();
      rootObject.WriteToXml(rootElementName, xw, objectRefId);
      xw.WriteEndDocument();
    }
    finally
    {
      xw.Flush();
    }
  }

  /// <summary>Загрузить объект из XML документа</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="rootObject">Экземпляр объекта</param>
  /// <param name="rootElementName">Имя объекта в XML</param>
  /// <returns>true, если объект был найден в документе</returns>
  public static bool LoadFromXmlDocument(
    IUserSession iUserSession,
    XmlReadArgs readArgs,
    string fileName,
    IWriteReadXml rootObject,
    string rootElementName)
  {
    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
    try
    {
      return WriteReadXmlHelper.LoadFromXmlDocument(iUserSession, (Stream) fileStream, rootObject, rootElementName);
    }
    finally
    {
      fileStream.Close();
    }
  }

  /// <summary>Загрузить объект из XML документа</summary>
  /// <param name="stream">Поток документа</param>
  /// <param name="rootObject">Экземпляр объекта</param>
  /// <param name="rootElementName">Имя объекта в XML</param>
  /// <returns>true, если объект был найден в документе</returns>
  public static bool LoadFromXmlDocument(
    IUserSession iUserSession,
    Stream stream,
    IWriteReadXml rootObject,
    string rootElementName)
  {
    XmlReader xr = XmlReader.Create(stream, new XmlReaderSettings()
    {
      CheckCharacters = false
    });
    return WriteReadXmlHelper.LoadFromXmlDocument(iUserSession, xr, rootObject, rootElementName);
  }

  /// <summary>Загрузить объект из XML документа</summary>
  /// <param name="xr">XmlReader</param>
  /// <param name="iUserSession">пользовательская сессия ддля подгрузки данных</param>
  /// <param name="rootObject">Экземпляр объекта</param>
  /// <param name="rootElementName">Имя объекта в XML</param>
  /// <returns>true, если объект был найден в документе</returns>
  public static bool LoadFromXmlDocument(
    IUserSession iUserSession,
    XmlReader xr,
    IWriteReadXml rootObject,
    string rootElementName)
  {
    bool flag1 = false;
    try
    {
      bool flag2 = false;
      XmlReadArgs readArgs = new XmlReadArgs(xr);
      readArgs.IUserSession = iUserSession;
      while (!flag2)
      {
        if (readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              if (readArgs.Reader.LocalName == rootElementName)
              {
                flag1 = true;
                rootObject.ReadFromXml(readArgs);
                continue;
              }
              continue;
            case XmlNodeType.EndElement:
              if (rootElementName == readArgs.Reader.LocalName)
              {
                flag2 = true;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
        else
          break;
      }
    }
    finally
    {
      xr.Close();
    }
    return flag1;
  }

  /// <summary> Прочитать состояние объекта, способного читать совоё состояние из xml, из строки base64 </summary>
  /// <param name="rootObject">Корневой объект, состояние которого читается из строки </param>
  /// <param name="rootElementName">Имя корневого объекта в XML</param>
  /// <returns> Состояние объекта в виде строки содержащей xml документ преобразованый к Base64 строке </returns>
  public static void LoadXmlObjectStateFromBase64String(
    IUserSession iUserSession,
    IWriteReadXml rootObject,
    string rootElementName,
    string base64string)
  {
    using (StringReader input = new StringReader(Encoding.UTF8.GetString(Convert.FromBase64String(base64string))))
    {
      XmlReader xr = XmlReader.Create((TextReader) input);
      WriteReadXmlHelper.LoadFromXmlDocument(iUserSession, xr, rootObject, rootElementName);
    }
  }
}
