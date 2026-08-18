
// Type: Intermech.Interfaces.IFormDesignerActionManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для регистрации дополнительных действий на кнопки.
    /// </summary>
    public interface IFormDesignerActionManager : IEnumerable<FormDesignerAction>, IEnumerable
    {
      /// <summary>Регистрация обработчика на кнопку.</summary>
      /// <param name="actionGuid">Глобальный идентификатор события</param>
      /// <param name="actionName">Наименование события (например "Применить", "Отмена" и т.д.)</param>
      /// <param name="handler">Обработчик нажатия</param>
      void RegisterAction(Guid actionGuid, string actionName, IFormDesignerActionHandler handler);

      /// <summary>Регистрация обработчика на кнопку.</summary>
      /// <param name="action">Действие</param>
      /// <param name="handler">Обработчик нажатия</param>
      void RegisterAction(FormDesignerAction action, IFormDesignerActionHandler handler);

      /// <summary>Разрегистрация обработчика.</summary>
      /// <param name="actionGuid">Глобальный идентификатор события</param>
      void UnregisterAction(Guid actionGuid);

      /// <summary>Получить описание события.</summary>
      /// <param name="actionGuid">Глобальный идентификатор события</param>
      /// <returns>Описание события</returns>
      FormDesignerAction GetInfo(Guid actionGuid);

      /// <summary>Получить обработчик.</summary>
      /// <param name="actionInfo">Либо глобальный идентификатор события, либо FormDesignerAction</param>
      /// <returns>Обработчик события (если такое есть), либо null - если нет</returns>
      IFormDesignerActionHandler GetAction(object actionInfo);
    }
}
