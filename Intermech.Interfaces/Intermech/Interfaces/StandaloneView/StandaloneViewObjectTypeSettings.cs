
// Type: Intermech.Interfaces.StandaloneView.StandaloneViewObjectTypeSettings
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces.StandaloneView
{
    /// <summary>
    /// Объект с настройками для типа объектов для режима автономного просмотра.
    /// </summary>
    [Serializable]
    public class StandaloneViewObjectTypeSettings : ICloneable
    {
      private bool? injectSigns;
      private bool? injectFileChecksum;
      private StandaloneViewInjectedAttributesSettings injectedAttributes;

      /// <summary>
      /// Включает или выключает внедрение информации об актуальных подписях объекта в файл объекта перед просмотром или печатью.
      /// Значение свойства может быть не задано. В этом случае будет использоваться настройка от базового типа объектов.
      /// </summary>
      public bool? InjectSigns
      {
        [DebuggerStepThrough] get => this.injectSigns;
        [DebuggerStepThrough] set => this.injectSigns = value;
      }

      /// <summary>
      /// Включает или выключает внедрение контрольной суммы файла в файл объекта перед просмотром или печатью.
      /// Значение свойства может быть не задано. В этом случае будет использоваться настройка от базового типа объектов.
      /// </summary>
      public bool? InjectFileChecksum
      {
        [DebuggerStepThrough] get => this.injectFileChecksum;
        [DebuggerStepThrough] set => this.injectFileChecksum = value;
      }

      /// <summary>
      /// Возвращает или задает список атрибутов объектов, внедряемых в файл объекта перед просмотром или печатью.
      /// Значение свойства может быть не задано. В этом случае будет использоваться настройка от базового типа объектов.
      /// </summary>
      public StandaloneViewInjectedAttributesSettings InjectedAttributes
      {
        [DebuggerStepThrough] get => this.injectedAttributes;
        [DebuggerStepThrough] set => this.injectedAttributes = value;
      }

      /// <summary>
      /// Возвращает true, если объект настроек не содержит ни одного заполненного свойства.
      /// </summary>
      public bool IsEmpty
      {
        [DebuggerStepThrough] get
        {
          return !this.InjectSigns.HasValue && !this.InjectFileChecksum.HasValue && this.InjectedAttributes == null;
        }
      }

      /// <summary>
      /// Возвращает true, если объект настроек полностью определен и не содержит не заполненных свойств.
      /// </summary>
      public bool IsFullyDefined
      {
        [DebuggerStepThrough] get
        {
          return this.InjectSigns.HasValue && this.InjectFileChecksum.HasValue && this.InjectedAttributes != null;
        }
      }

      /// <summary>
      /// Делает текущий объект настроек полностью определенным, заполняя не определенные свойства текущего объекта значениями по умолчанию.
      /// </summary>
      public void MakeFullDefined()
      {
        if (!this.InjectSigns.HasValue)
          this.InjectSigns = new bool?(false);
        if (!this.InjectFileChecksum.HasValue)
          this.InjectFileChecksum = new bool?(false);
        if (this.InjectedAttributes != null)
          return;
        this.InjectedAttributes = new StandaloneViewInjectedAttributesSettings();
      }

      /// <summary>
      /// Объединяет текущий объект настроек с другим объектом, заполняя не определенные свойства текущего объекта значениями из другого объекта.
      /// </summary>
      /// <param name="other">Другой объект настроек</param>
      /// <exception cref="T:ArgumentNullException">other</exception>
      public void MergeWith(StandaloneViewObjectTypeSettings other)
      {
        if (other == null)
          throw new ArgumentNullException(nameof (other));
        if (!this.InjectSigns.HasValue && other.InjectSigns.HasValue)
          this.InjectSigns = other.InjectSigns;
        if (!this.InjectFileChecksum.HasValue && other.InjectFileChecksum.HasValue)
          this.InjectFileChecksum = other.InjectFileChecksum;
        if (this.InjectedAttributes != null || other.InjectedAttributes == null)
          return;
        this.InjectedAttributes = other.InjectedAttributes.Clone();
      }

      /// <summary>Создает и возвращает клон текущего объекта.</summary>
      /// <returns>Клон текущего объекта</returns>
      public StandaloneViewObjectTypeSettings Clone()
      {
        StandaloneViewObjectTypeSettings objectTypeSettings = new StandaloneViewObjectTypeSettings();
        objectTypeSettings.InjectSigns = this.InjectSigns;
        objectTypeSettings.InjectFileChecksum = this.InjectFileChecksum;
        if (this.InjectedAttributes != null)
          objectTypeSettings.InjectedAttributes = this.InjectedAttributes.Clone();
        return objectTypeSettings;
      }

      /// <summary>Создает и возвращает клон текущего объекта.</summary>
      /// <returns>Клон текущего объекта</returns>
      object ICloneable.Clone() => (object) this.Clone();
    }
}
