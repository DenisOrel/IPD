
// Type: Intermech.Interfaces.IXMLStorageLoadSave
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс, позволяющий выполнять сохранение и загрузку данных в хранилище XML
    /// </summary>
    public interface IXMLStorageLoadSave
    {
      /// <summary>Загрузить данные из указанного узла настроек</summary>
      /// <param name="xmlStorage">Хранилище настроек</param>
      /// <param name="node">Узел с данными</param>
      void Load(XMLSettingsStorage xmlStorage, XmlNode node);

      /// <summary>
      /// Сохранить данные в состав указанного родительского узла
      /// </summary>
      /// <param name="xmlStorage">Хранилище настроек</param>
      /// <param name="parentNode">Родительский узел или null (тогда сохранение можно выполнять в корневой узел)</param>
      void Save(XMLSettingsStorage xmlStorage, XmlNode parentNode);
    }
}
