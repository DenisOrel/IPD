
// Type: Intermech.Interfaces.IDBAttributesGroupCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public interface IDBAttributesGroupCollection : IDBCollection
    {
      /// <summary>Создает группу атрибутов</summary>
      int Create(string groupName, string groupNote, string languageID, string areaID, Guid guid);
    }
}
