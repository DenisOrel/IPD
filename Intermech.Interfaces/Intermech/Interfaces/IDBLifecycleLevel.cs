
// Type: Intermech.Interfaces.IDBLifecycleLevel
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс уровня продвижения объектов.</summary>
    public interface IDBLifecycleLevel
    {
      /// <summary>
      /// Идентификатор уровня продвижения. В некоторых реализациях может быть только для
      /// чтения.
      /// </summary>
      int LevelID { get; set; }

      /// <summary>Наименование уровня продвижения.</summary>
      string LevelName { get; }

      /// <summary>Литера уровня продвижения (например, А)</summary>
      string Litera { get; }

      /// <summary>Иконка, отображающая уровень продвижения.</summary>
      byte[] LevelIcon { get; }
    }
}
