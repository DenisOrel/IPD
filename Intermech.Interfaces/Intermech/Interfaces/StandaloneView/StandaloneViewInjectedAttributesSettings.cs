
// Type: Intermech.Interfaces.StandaloneView.StandaloneViewInjectedAttributesSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces.StandaloneView
{
    /// <summary>
    /// Настройки списка атрибутов объектов, внедряемых в файлы объектов в режиме автономного просмотра.
    /// </summary>
    [Serializable]
    public class StandaloneViewInjectedAttributesSettings : ICloneable
    {
      private bool enabled;
      private List<Guid> ids;

      /// <summary>Создает объект.</summary>
      public StandaloneViewInjectedAttributesSettings() => this.ids = new List<Guid>();

      /// <summary>
      /// Включает или выключает запись атрибутов объекта в файл объекта перед просмотром или печатью.
      /// </summary>
      public bool Enabled
      {
        [DebuggerStepThrough] get => this.enabled;
        [DebuggerStepThrough] set => this.enabled = value;
      }

      /// <summary>
      /// Коллекция идентификаторов атрибутов, которые должны быть записаны в файл объекта перед просмотром или печатью.
      /// </summary>
      public ICollection<Guid> Identifiers
      {
        [DebuggerStepThrough] get => (ICollection<Guid>) this.ids;
      }

      /// <summary>Создает и возвращает клон текущего объекта.</summary>
      /// <returns>Клон текущего объекта</returns>
      public StandaloneViewInjectedAttributesSettings Clone()
      {
        StandaloneViewInjectedAttributesSettings attributesSettings = new StandaloneViewInjectedAttributesSettings();
        attributesSettings.Enabled = this.Enabled;
        attributesSettings.Identifiers.AddRange<Guid>((IEnumerable<Guid>) this.Identifiers);
        return attributesSettings;
      }

      /// <summary>Создает и возвращает клон текущего объекта.</summary>
      /// <returns>Клон текущего объекта</returns>
      object ICloneable.Clone() => (object) this.Clone();
    }
}
