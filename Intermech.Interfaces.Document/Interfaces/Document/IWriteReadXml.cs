// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IWriteReadXml
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Интерфейс классов, которые можно сохранить в XML и извлечь из XML</summary>
public interface IWriteReadXml
{
  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId);

  /// <summary>Загрузить из XML</summary>
  /// <remarks>
  /// Рекомендуемая реализация метода
  /// public virtual void ReadFromXml(XmlReadArgs readArgs)
  /// {
  /// 	// Код который необходимо выполнить до загрузки объекта
  /// 
  /// 	// Загрузка данных
  /// 	WriteReadXmlHelper.ReadFromXml(this, readArgs);
  /// 
  /// 	// Код который необходимо выполнить после загрузки объекта
  /// }
  /// </remarks>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  void ReadFromXml(XmlReadArgs readArgs);

  /// <summary>Прочитать одно поле из XML. Вызывается при стандартной реализации ReadFromXml</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле прочитано</returns>
  bool ReadFieldFromXml(XmlReadArgs readArgs);
}
