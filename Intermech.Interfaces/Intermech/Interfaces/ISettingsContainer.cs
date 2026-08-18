
// Type: Intermech.Interfaces.ISettingsContainer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для хранения контейнера настроек
    /// Все настройки должны храниться в сериализуемых классах
    /// </summary>
    public interface ISettingsContainer
    {
      /// <summary>
      /// F_OBJECT_ID объекта, в котором хранятся данные настройки
      /// </summary>
      long ObjectID { get; set; }

      /// <summary>
      /// ID атрибута типа ftShortBlob, в котором хранятся данные настройки
      /// </summary>
      int AttrID { get; set; }

      /// <summary>Уникальный ключ владельца данного контейнера настроек</summary>
      string OwnerID { get; }

      /// <summary>
      /// Ссылка на интерфейс коллекции значений [Ключ]=[Значение],
      /// где [Ключ] - уникальное сериализуемое значение-ключ,
      /// а [Значение] - ссылка на сериализуемый объект, в котором что-то хранится
      /// </summary>
      IDictionary Settings { get; }

      /// <summary>
      /// Получить или установить значение настроечного класса с определённым ключом
      /// </summary>
      /// <param name="Key">Уникальный (в пределах коллекции Settings) сериализуемый ключ настроечного класса</param>
      object this[object Key] { get; set; }

      /// <summary>
      /// Дата и время последнего доступа к настройкам (свойство нужно для сборки мусора)
      /// </summary>
      DateTime LastAccess { get; set; }

      /// <summary>Очистить все настройки</summary>
      void Clear();

      /// <summary>
      /// Скопировать все поля объекта Source в данный экземпляр объекта.
      /// Если Source == null, то данный экземпляр будет очищен.
      /// </summary>
      /// <param name="Source">Из этого объекта будут скопированы все настройки</param>
      void Assign(ISettingsContainer Source);

      /// <summary>Загрузить настройки из объекта базы данных</summary>
      /// <param name="session">Сессия</param>
      bool LoadFromObject(IUserSession session);

      /// <summary>Сохранить настройки в объект базы данных</summary>
      /// <param name="session">Сессия</param>
      bool SaveToObject(IUserSession session);

      /// <summary>
      /// Загрузить настройки из конфигурации пользователя. Имя файла будет равно OwnerID
      /// </summary>
      /// <param name="session">Сессия</param>
      bool LoadFromUserConfig(IUserSession session);

      /// <summary>
      /// Сохранить настройки в конфигурацию пользователя. Имя файла будет равно OwnerID
      /// </summary>
      /// <param name="session">Сессия</param>
      bool SaveToUserConfig(IUserSession session);
    }
}
