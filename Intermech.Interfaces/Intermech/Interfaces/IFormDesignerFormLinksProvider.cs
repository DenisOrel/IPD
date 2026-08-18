
// Type: Intermech.Interfaces.IFormDesignerFormLinksProvider
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс провайдера.</summary>
    public interface IFormDesignerFormLinksProvider : ICloneable
    {
      /// <summary>
      /// Результат загрузки информации о типах, которым назначена форма.
      /// </summary>
      bool Loaded { get; }

      /// <summary>
      /// Глобальный идентификатор провайдера для идентификации.
      /// </summary>
      Guid ProviderGuid { get; }

      /// <summary>Наименование провайдера для отображения в TreeView.</summary>
      string ProviderName { get; }

      /// <summary>Корневой узел для дерева связей.</summary>
      object RootNode { get; }

      /// <summary>Список ссылок в провайдере.</summary>
      List<FormLink> FormLinks { get; }

      /// <summary>Построить дерево связей на форму, без root нода.</summary>
      /// <param name="formID">Идентификатор формы</param>
      void Load(long formID);

      /// <summary>Добавить связь.</summary>
      void Add();

      /// <summary>Удалить выделенный узел из списка.</summary>
      /// <param name="node">Узел для удаления</param>
      void Delete(object node);

      /// <summary>Очистить все настройки.</summary>
      void Clear();

      /// <summary>Сохранить список в форму.</summary>
      void Commit();

      /// <summary>Отменить сохранение.</summary>
      void Rollback();
    }
}
