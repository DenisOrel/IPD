
// Type: Intermech.Interfaces.IXMLStoredClass
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс классов, которые могут хранить своё состояние в XML-хранилище
    /// </summary>
    public interface IXMLStoredClass
    {
      /// <summary>
      /// Загрузить состояние экземпляра класса из XML-хранилища
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Узел, из которого загружается информация</param>
      void Load(XMLSettingsStorage storage, XmlNode node);

      /// <summary>
      /// Сохранить состояние экземпляра класса в указанный родительский узел XML-хранилища
      /// </summary>
      /// <param name="storage">Хранилище настроек</param>
      /// <param name="node">Родительский узел или null (тогда узел создаётся прямо в корневом узле документа XML)</param>
      void Save(XMLSettingsStorage storage, XmlNode node);

      /// <summary>Очистить поля экземпляра класса</summary>
      void Clear();

      /// <summary>
      /// Загрузить информацию в текущий объект из указанного объекта
      /// </summary>
      /// <param name="source">Объект-источник</param>
      void Assign(object source);
    }
}
