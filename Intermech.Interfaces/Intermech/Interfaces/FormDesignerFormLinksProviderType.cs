
// Type: Intermech.Interfaces.FormDesignerFormLinksProviderType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Данные о провайдере.</summary>
    public class FormDesignerFormLinksProviderType
    {
      /// <summary>Глобальный идентификатор провайдера.</summary>
      public Guid ProviderGuid;
      /// <summary>Наименование провайдера.</summary>
      public string ProviderName;
      /// <summary>Тип провайдера.</summary>
      public Type ProviderType;

      /// <summary>Конструктор.</summary>
      /// <param name="name">Наименование провайдера</param>
      /// <param name="guid">Глобальный идентификатор провайдера</param>
      /// <param name="type">Тип провайдера</param>
      public FormDesignerFormLinksProviderType(string name, Guid guid, Type type)
      {
        this.ProviderName = name;
        this.ProviderGuid = guid;
        this.ProviderType = type;
      }
    }
}
