
// Type: Intermech.Interfaces.XmlHelperIPS
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>Вспомогательный класс для чтения/записи XML</summary>
    public class XmlHelperIPS
    {
      /// <summary>Загрузить элемент из XML</summary>
      /// <param name="element">Элемент данные которого нужно загрузить</param>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      public static void ReadFromXml(IXmlObjectIPS element, XmlReadArgsIPS readArgs)
      {
        if (element == null)
          throw new ArgumentNullException(nameof (element));
        if (readArgs == null)
          throw new ArgumentNullException(nameof (readArgs));
        string localName = readArgs.Reader.LocalName;
        IUnknownXmlElementIPS unknownXmlElementIps = element as IUnknownXmlElementIPS;
        if (readArgs.Reader.HasAttributes)
        {
          int i = 0;
          for (int attributeCount = readArgs.Reader.AttributeCount; i < attributeCount; ++i)
          {
            readArgs.Reader.MoveToAttribute(i);
            if (!element.ReadFieldFromXml(readArgs) && unknownXmlElementIps != null)
              unknownXmlElementIps.AddUnknownXmlAttribute(readArgs.Reader.LocalName, readArgs.Reader.Value);
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
              if (!element.ReadFieldFromXml(readArgs) && unknownXmlElementIps != null)
              {
                unknownXmlElementIps.UnknownXmlElements += readArgs.Reader.ReadOuterXml();
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
        int num = flag ? 1 : 0;
      }

      /// <summary>Прочитать типизированный XML элемент</summary>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      /// <returns>Загруженный элемент</returns>
      public static IXmlObjectIPS ReadTypedElementFromXml(XmlReadArgsIPS readArgs)
      {
        Type type = (Type) null;
        EmptyConstructorDelegateIPS constructorDelegateIps = (EmptyConstructorDelegateIPS) null;
        if (readArgs.Reader.MoveToAttribute("type"))
        {
          string str1 = readArgs.Reader.Value;
          if (constructorDelegateIps == null && type == (Type) null && readArgs.Reader.MoveToAttribute("baseType"))
          {
            string str2 = readArgs.Reader.Value;
            if (constructorDelegateIps == null)
            {
              int num = type == (Type) null ? 1 : 0;
            }
          }
        }
        readArgs.Reader.MoveToElement();
        IXmlObjectIPS xmlObjectIps = (IXmlObjectIPS) null;
        if (constructorDelegateIps != null)
          xmlObjectIps = constructorDelegateIps() as IXmlObjectIPS;
        else if (type != (Type) null)
          xmlObjectIps = Activator.CreateInstance(type, true) as IXmlObjectIPS;
        xmlObjectIps?.ReadFromXml(readArgs);
        return xmlObjectIps;
      }

      /// <summary>Сохранить IXmlObjectIPS element как элемент XML</summary>
      /// <param name="name">Имя элемента XML</param>
      /// <param name="element">Сохраняемый объект</param>
      /// <param name="skipNull">Не записывать тэг, если element == null</param>
      /// <param name="xw">XmlWriter</param>
      /// <param name="objectRefId">Генератор идентификаторов</param>
      public static void WriteXmlElement(
        string name,
        IXmlObjectIPS element,
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
            xw.WriteValue(dictionaryEntry.Value.ToString());
          xw.WriteEndElement();
        }
        xw.WriteEndElement();
      }

      /// <summary>Загрузить текстовый словарь из XML</summary>
      /// <param name="dictionary">Словарь</param>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      public static void ReadStringDictionaryFromXml(IDictionary dictionary, XmlReadArgsIPS readArgs)
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
              XmlHelperIPS.ReadStringDictionaryElementFromXml(dictionary, readArgs);
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
        XmlReadArgsIPS readArgs)
      {
        if (dictionary == null)
          throw new ArgumentNullException(nameof (dictionary));
        if (readArgs == null)
          throw new ArgumentNullException(nameof (readArgs));
        string localName = readArgs.Reader.LocalName;
        if (!readArgs.Reader.HasAttributes)
          throw new Exception(LocalizationHolder.rm.GetString("Interfaces_744"));
        readArgs.Reader.MoveToAttribute("key");
        string key = readArgs.Reader.Value;
        readArgs.Reader.MoveToElement();
        bool flag = readArgs.Reader.IsEmptyElement;
        while (!flag && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Text:
              dictionary[(object) key] = (object) readArgs.Reader.Value;
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

      /// <summary>Записать словарь в XML</summary>
      /// <param name="elementName">Имя элемента XML</param>
      /// <param name="dictionary">Словарь</param>
      /// <param name="dicItemName">Имя элемента XML для записи словаря</param>
      /// <param name="dicValueName">Имя элемента XML для значений записи словаря</param>
      /// <param name="xw">XmlWriter</param>
      /// <param name="objectRefId">Генератор идентификаторов</param>
      public static void WriteDictionaryToXml(
        string elementName,
        IDictionary dictionary,
        string dicItemName,
        string dicValueName,
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
        if (xw == null)
          throw new ArgumentNullException(nameof (xw));
        xw.WriteStartElement(elementName);
        foreach (DictionaryEntry dictionaryEntry in dictionary)
        {
          IXmlObjectIPS xmlObjectIps = (IXmlObjectIPS) dictionaryEntry.Value;
          if (xmlObjectIps != null)
          {
            xw.WriteStartElement(dicItemName);
            xw.WriteAttributeString("key", dictionaryEntry.Key.ToString());
            xmlObjectIps.WriteToXml(dicValueName, xw, objectRefId);
            xw.WriteEndElement();
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
        XmlReadArgsIPS readArgs)
      {
        if (dictionary == null)
          throw new ArgumentNullException(nameof (dictionary));
        if (keyType == (Type) null)
          throw new ArgumentNullException(nameof (keyType));
        if (elementType == (Type) null)
          throw new ArgumentNullException(nameof (elementType));
        if (readArgs == null)
          throw new ArgumentNullException(nameof (readArgs));
        string localName = readArgs.Reader.LocalName;
        bool flag = readArgs.Reader.IsEmptyElement;
        while (!flag && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              XmlHelperIPS.ReadDictionaryElementFromXml(dictionary, keyType, elementType, readArgs);
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

      /// <summary>Загрузить элемент словаря из XML</summary>
      /// <param name="dictionary">Словарь</param>
      /// <param name="keyType">Тип ключа</param>
      /// <param name="elementType">Тип значения</param>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      protected static void ReadDictionaryElementFromXml(
        IDictionary dictionary,
        Type keyType,
        Type elementType,
        XmlReadArgsIPS readArgs)
      {
        if (dictionary == null)
          throw new ArgumentNullException(nameof (dictionary));
        if (keyType == (Type) null)
          throw new ArgumentNullException(nameof (keyType));
        if (elementType == (Type) null)
          throw new ArgumentNullException(nameof (elementType));
        if (readArgs == null)
          throw new ArgumentNullException(nameof (readArgs));
        string localName = readArgs.Reader.LocalName;
        object key = (object) null;
        if (readArgs.Reader.HasAttributes)
        {
          readArgs.Reader.MoveToAttribute("key");
          key = !(keyType == typeof (Guid)) ? Convert.ChangeType((object) readArgs.Reader.Value, keyType) : (object) new Guid(readArgs.Reader.Value);
          readArgs.Reader.MoveToElement();
        }
        bool flag = readArgs.Reader.IsEmptyElement;
        while (!flag && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              IXmlObjectIPS instance = (IXmlObjectIPS) Activator.CreateInstance(elementType, true);
              instance.ReadFromXml(readArgs);
              dictionary[key] = (object) instance;
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

      /// <summary>Записать массив в XML</summary>
      /// <param name="elementName">Имя элемента XML</param>
      /// <param name="array">Массив</param>
      /// <param name="arrayItemName">Имя элемента массива</param>
      /// <param name="xw">XmlWriter</param>
      /// <param name="objectRefId">Генератор идентификаторов</param>
      public static void WriteArrayToXml(
        string elementName,
        Array array,
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
        xw.WriteAttributeString("length", array.GetLength(0).ToString((IFormatProvider) CultureInfo.InvariantCulture));
        for (int index = 0; index < array.Length; ++index)
        {
          object obj = array.GetValue(index);
          if (obj is IXmlObjectIPS xmlObjectIps)
            xmlObjectIps.WriteToXml(arrayItemName, xw, objectRefId);
          else
            xw.WriteElementString(arrayItemName, obj.ToString());
        }
        xw.WriteEndElement();
      }

      /// <summary>Загрузить массив из XML</summary>
      /// <param name="itemType">Тип значения</param>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      /// <returns>Массив</returns>
      public static Array ReadArrayFromXml(Type itemType, XmlReadArgsIPS readArgs)
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
        while (!flag && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              object obj;
              if (readArgs.Reader.LocalName != "null")
              {
                obj = Activator.CreateInstance(itemType, true);
                if (obj is IXmlObjectIPS xmlObjectIps)
                  xmlObjectIps.ReadFromXml(readArgs);
                else if (itemType == typeof (Guid))
                {
                  if (!readArgs.Reader.HasValue)
                    readArgs.Reader.Read();
                  obj = (object) new Guid(readArgs.Reader.Value);
                }
                else if (itemType == typeof (long))
                {
                  if (!readArgs.Reader.HasValue)
                    readArgs.Reader.Read();
                  obj = (object) Convert.ToInt64(readArgs.Reader.Value);
                }
                else if (itemType == typeof (int))
                {
                  if (!readArgs.Reader.HasValue)
                    readArgs.Reader.Read();
                  obj = (object) Convert.ToInt32(readArgs.Reader.Value);
                }
                else if (itemType == typeof (string))
                {
                  if (!readArgs.Reader.HasValue)
                    readArgs.Reader.Read();
                  obj = (object) readArgs.Reader.Value;
                }
                else
                  obj = (object) null;
              }
              else
                obj = (object) null;
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
          if (list[index] is IXmlObjectIPS xmlObjectIps)
            xmlObjectIps.WriteToXml(listItemName, xw, objectRefId);
        }
        xw.WriteEndElement();
      }

      /// <summary>Загрузить типизированый список из XML</summary>
      /// <param name="list">Список</param>
      /// <param name="itemType">Тип значения</param>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      public static void ReadListFromXml(IList list, Type itemType, XmlReadArgsIPS readArgs)
      {
        if (list == null)
          throw new ArgumentNullException(nameof (list));
        if (itemType == (Type) null)
          throw new ArgumentNullException(nameof (itemType));
        if (readArgs == null)
          throw new ArgumentNullException(nameof (readArgs));
        string localName = readArgs.Reader.LocalName;
        bool flag = readArgs.Reader.IsEmptyElement;
        while (!flag && readArgs.Reader.Read())
        {
          switch (readArgs.Reader.NodeType)
          {
            case XmlNodeType.Element:
              object obj;
              if (readArgs.Reader.LocalName != "null")
              {
                obj = Activator.CreateInstance(itemType, true);
                if (obj is IXmlObjectIPS xmlObjectIps)
                  xmlObjectIps.ReadFromXml(readArgs);
              }
              else
                obj = (object) null;
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

      /// <summary>Загрузить типизированый список из XML</summary>
      /// <param name="list">Список</param>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      public static void ReadStringListFromXml(List<string> list, XmlReadArgsIPS readArgs)
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
              if (!readArgs.Reader.HasValue && !readArgs.Reader.IsEmptyElement)
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

      /// <summary>Записать rootObject в XML документ</summary>
      /// <param name="fileName">Имя файла</param>
      /// <param name="rootObject">Корневой объект, который сохраняется в XML</param>
      /// <param name="rootElementName">Имя корневого объекта в XML</param>
      public static void WriteXmlDocument(
        string fileName,
        IXmlObjectIPS rootObject,
        string rootElementName)
      {
        XmlHelperIPS.WriteXmlDocument((XmlWriter) new XmlTextWriter(fileName, Encoding.UTF8), rootObject, rootElementName);
      }

      /// <summary>Записать rootObject в XML документ</summary>
      /// <param name="stream">Поток документа</param>
      /// <param name="rootObject">Корневой объект, который сохраняется в XML</param>
      /// <param name="rootElementName">Имя корневого объекта в XML</param>
      public static void WriteXmlDocument(
        Stream stream,
        IXmlObjectIPS rootObject,
        string rootElementName)
      {
        XmlHelperIPS.WriteXmlDocument((XmlWriter) new XmlTextWriter(stream, Encoding.UTF8), rootObject, rootElementName);
      }

      /// <summary> Записать состояние объекта, способного сохраняться в XML в строку base64 </summary>
      /// <param name="rootObject">Корневой объект, который сохраняется в XML</param>
      /// <param name="rootElementName">Имя корневого объекта в XML</param>
      /// <returns> Состояние объекта в виде строки содержащей xml документ преобразованый к Base64 строке </returns>
      public static string WriteXmlObjectStateToBase64String(
        IXmlObjectIPS rootObject,
        string rootElementName)
      {
        StringBuilder output = new StringBuilder();
        XmlHelperIPS.WriteXmlDocument(XmlWriter.Create(output, new XmlWriterSettings()), rootObject, rootElementName);
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(output.ToString()));
      }

      /// <summary>Записать rootObject в XML документ</summary>
      /// <param name="xw">XmlTextWriter</param>
      /// <param name="rootObject">Корневой объект, который сохраняется в XML</param>
      /// <param name="rootElementName">Имя корневого объекта в XML</param>
      public static void WriteXmlDocument(
        XmlWriter xw,
        IXmlObjectIPS rootObject,
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
      /// <param name="iUserSession">пользовательская сессия ддля подгрузки данных</param>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      /// <param name="fileName">Имя файла</param>
      /// <param name="rootObject">Экземпляр объекта</param>
      /// <param name="rootElementName">Имя объекта в XML</param>
      /// <returns>true, если объект был найден в документе</returns>
      public static bool LoadFromXmlDocument(
        IUserSession iUserSession,
        XmlReadArgsIPS readArgs,
        string fileName,
        IXmlObjectIPS rootObject,
        string rootElementName)
      {
        FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        try
        {
          return XmlHelperIPS.LoadFromXmlDocument(iUserSession, (Stream) fileStream, rootObject, rootElementName);
        }
        finally
        {
          fileStream.Close();
        }
      }

      /// <summary>Загрузить объект из XML документа</summary>
      /// <param name="iUserSession">пользовательская сессия ддля подгрузки данных</param>
      /// <param name="stream">Поток документа</param>
      /// <param name="rootObject">Экземпляр объекта</param>
      /// <param name="rootElementName">Имя объекта в XML</param>
      /// <returns>true, если объект был найден в документе</returns>
      public static bool LoadFromXmlDocument(
        IUserSession iUserSession,
        Stream stream,
        IXmlObjectIPS rootObject,
        string rootElementName)
      {
        XmlReader xr = XmlReader.Create(stream, new XmlReaderSettings()
        {
          CheckCharacters = false
        });
        return XmlHelperIPS.LoadFromXmlDocument(iUserSession, xr, rootObject, rootElementName);
      }

      /// <summary>Загрузить объект из XML документа</summary>
      /// <param name="iUserSession">пользовательская сессия ддля подгрузки данных</param>
      /// <param name="xr">XmlReader</param>
      /// <param name="rootObject">Экземпляр объекта</param>
      /// <param name="rootElementName">Имя объекта в XML</param>
      /// <returns>true, если объект был найден в документе</returns>
      public static bool LoadFromXmlDocument(
        IUserSession iUserSession,
        XmlReader xr,
        IXmlObjectIPS rootObject,
        string rootElementName)
      {
        bool flag1 = false;
        try
        {
          bool flag2 = false;
          XmlReadArgsIPS readArgs = new XmlReadArgsIPS(xr);
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
      /// <param name="iUserSession">пользовательская сессия ддля подгрузки данных</param>
      /// <param name="rootObject">Корневой объект, состояние которого читается из строки </param>
      /// <param name="rootElementName">Имя корневого объекта в XML</param>
      /// <param name="base64string">Строка данных в формате Base64</param>
      /// <returns> Состояние объекта в виде строки содержащей xml документ преобразованый к Base64 строке </returns>
      public static void LoadXmlObjectStateFromBase64String(
        IUserSession iUserSession,
        IXmlObjectIPS rootObject,
        string rootElementName,
        string base64string)
      {
        using (StringReader input = new StringReader(Encoding.UTF8.GetString(Convert.FromBase64String(base64string))))
        {
          XmlReader xr = XmlReader.Create((TextReader) input);
          XmlHelperIPS.LoadFromXmlDocument(iUserSession, xr, rootObject, rootElementName);
        }
      }
    }
}
