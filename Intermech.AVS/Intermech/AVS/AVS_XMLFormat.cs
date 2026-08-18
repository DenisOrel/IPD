// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVS_XMLFormat
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.AVS;

public class AVS_XMLFormat : IWriteReadXml
{
  /// <summary>Записать поля в XML</summary>
  /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId)
  {
  }

  /// <summary> Загрузить из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  public void ReadFromXml(XmlReadArgs readArgs)
  {
    WriteReadXmlHelper.ReadFromXml((IWriteReadXml) this, readArgs);
  }

  /// <summary> Прочитать одно поле из XML </summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns> Возвращает true, если поле прочитано </returns>
  public bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    switch (readArgs.Reader.LocalName)
    {
      case "Passport":
        return true;
      case "PassportData":
        return true;
      case "Record":
        return true;
      case "Records":
        return true;
      case "RecordsData":
        return true;
      case "Table_of_correspondence":
        return true;
      case "secondary":
        return true;
      default:
        return false;
    }
  }
}
