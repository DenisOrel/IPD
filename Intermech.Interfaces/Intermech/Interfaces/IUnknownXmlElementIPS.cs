
// Type: Intermech.Interfaces.IUnknownXmlElementIPS
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для классов поддерживающих загрузку неизвестных типов из XML</summary>
    public interface IUnknownXmlElementIPS
    {
      /// <summary>XML атрибуты, не распознанные при загрузке</summary>
      List<StringKeyValueIPS> UnknownXmlAttributes { get; set; }

      /// <summary>XML элементы, не распознанные при загрузке</summary>
      string UnknownXmlElements { get; set; }

      /// <summary>Добваить неизветсный атрибут</summary>
      /// <param name="key">Имя атрибута</param>
      /// <param name="value">Значение атрибута</param>
      void AddUnknownXmlAttribute(string key, string value);
    }
}
