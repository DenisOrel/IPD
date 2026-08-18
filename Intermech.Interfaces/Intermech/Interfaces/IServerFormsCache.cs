
// Type: Intermech.Interfaces.IServerFormsCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Кэш зачитанных форм.</summary>
    public interface IServerFormsCache
    {
      /// <summary>Получение формы.</summary>
      /// <param name="sessionGuid">Глобальный идентификатор сессии пользователя</param>
      /// <param name="formID">Идентификатор формы</param>
      /// <returns>Тело формы</returns>
      byte[] GetForm(Guid sessionGuid, long formID);

      /// <summary>Завершение редактирования формы.</summary>
      /// <param name="formID">Идентификатор формы</param>
      void CheckIn(long formID);

      /// <summary>Взятие формы на изменение.</summary>
      /// <param name="formID">Идентификатор формы</param>
      void CheckOut(long formID);

      /// <summary>Отмена изменений формы.</summary>
      /// <param name="formID">Идентификатор формы</param>
      void UndoCheckOut(long formID);

      /// <summary>Сохранение формы.</summary>
      /// <param name="formID">Идентификатор формы</param>
      /// <param name="bytes">Тело формы</param>
      void Save(long formID, byte[] bytes);

      /// <summary>Удаление формы.</summary>
      /// <param name="formID">Идентификатор формы</param>
      void Remove(long formID);

      /// <summary>Очистка кэша.</summary>
      void Clear();
    }
}
