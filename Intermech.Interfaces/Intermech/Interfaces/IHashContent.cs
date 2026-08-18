
// Type: Intermech.Interfaces.IHashContent
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс работы с блобом настройки списка данных, из которых формируется хэш подписи.
    /// </summary>
    public interface IHashContent
    {
      bool Compatible { get; set; }

      List<AttributeHashContentClass> Attributes { get; }

      List<RelationHashContentClass> Relations { get; }

      List<string> Files { get; }

      /// <summary>совместимая подпись</summary>
      /// <param name="compatible"></param>
      void Clear(bool compatible);

      void Load(Stream stream);

      void Load(XmlDocument xmlDocument);

      void Save(Stream stream);

      XmlDocument Save();
    }
}
