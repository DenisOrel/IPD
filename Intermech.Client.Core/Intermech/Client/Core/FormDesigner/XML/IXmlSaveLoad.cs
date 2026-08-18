
// Type: Intermech.Client.Core.FormDesigner.XML.IXmlSaveLoad
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Xml;


namespace Intermech.Client.Core.FormDesigner.XML;

/// <summary>
/// Интерфейс для перекрытия сохранения данных о контроле в Xml.
/// </summary>
public interface IXmlSaveLoad
{
  /// <summary>Сохранение данных о контроле в Xml.</summary>
  /// <param name="node">XmlNode с базовыми данными о контроле (параметры добавлять внутрь)</param>
  void Save(XmlNode node);

  /// <summary>Загрузка данных в контрол из Xml.</summary>
  /// <param name="node">XmlNode с данными о контроле</param>
  void Load(XmlNode node);
}
