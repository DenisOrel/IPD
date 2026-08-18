
// Type: Intermech.Interfaces.IFormDesignerFormLinksManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс менеджера связей с формой.</summary>
    public interface IFormDesignerFormLinksManager : 
      IEnumerable<FormDesignerFormLinksProviderType>,
      IEnumerable
    {
      /// <summary>Регистрация провайдера.</summary>
      /// <param name="providerType">Провайдер</param>
      void RegisterProvider(FormDesignerFormLinksProviderType providerType);

      /// <summary>Удаление провайдера.</summary>
      /// <param name="providerGuid">Глобальный идентификатор провайдера</param>
      void UnregisterProvider(Guid providerGuid);

      /// <summary>Получить данные о провайдере по его идентификатору.</summary>
      /// <param name="providerGuid">Глобальный идентификатор провайдера</param>
      /// <returns>Данные о провайдере</returns>
      FormDesignerFormLinksProviderType GetProvider(Guid providerGuid);
    }
}
