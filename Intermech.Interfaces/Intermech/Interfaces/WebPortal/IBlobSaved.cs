
// Type: Intermech.Interfaces.WebPortal.IBlobSaved
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Интерфейс для реализации в классах, поддерживающих сохранение и восстановление себя в БД
    /// </summary>
    public interface IBlobSaved
    {
      /// <summary>
      /// Вызывается у объекта при сохранении данных в блобовый атрибут
      /// </summary>
      /// <returns></returns>
      byte[] Save(IUserSession session, IDBObject backupObject);

      /// <summary>
      /// Вызывается у объекта при чтении данных из блобового атрибута
      /// </summary>
      /// <param name="backupObject">Ссылка на объект, в который сохранена задача</param>
      /// <param name="bytes">Что положили в Save, то и получили :)</param>
      void Load(IUserSession session, IDBObject backupObject, byte[] bytes);
    }
}
