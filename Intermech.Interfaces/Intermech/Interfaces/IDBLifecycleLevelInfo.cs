
// Type: Intermech.Interfaces.IDBLifecycleLevelInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для получения инфы об уровне продвижения</summary>
    public interface IDBLifecycleLevelInfo
    {
      /// <summary>
      /// Идентификатор уровня продвижения. В некоторых реализациях может быть только для
      /// чтения.
      /// </summary>
      int LevelID { get; }

      /// <summary>Наименование уровня продвижения.</summary>
      string LevelName { get; }

      /// <summary>Литера уровня продвижения (например, А)</summary>
      string Litera { get; }

      /// <summary>Гуид уровня продвижения</summary>
      Guid GUID { get; }

      /// <summary>Иконка, отображающая уровень продвижения.</summary>
      byte[] LevelIcon { get; }
    }
}
