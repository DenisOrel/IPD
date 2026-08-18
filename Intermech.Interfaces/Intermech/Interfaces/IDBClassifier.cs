
// Type: Intermech.Interfaces.IDBClassifier
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс на серверный объект-классификатор</summary>
    public interface IDBClassifier
    {
      /// <summary>
      /// Перечитать атрибут ключ папки классификатора у состава
      /// </summary>
      void RebuildKeys();
    }
}
