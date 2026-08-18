
// Type: Intermech.Tools.Integrators.LookupResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Tools.Integrators
{
    /// <summary>
    /// Описывает результат поиска интегратора с помощью xpath-выражения.
    /// </summary>
    [Serializable]
    public sealed class LookupResult
    {
      private readonly IntegratorObject integrator;
      private readonly bool success;
      private readonly string xmlData;

      /// <summary>Создает объект.</summary>
      /// <param name="integrator">Объект интегратора</param>
      /// <param name="success">Признак, что конфигурация интегратора содержит элементы, соответствующие xpath-выражению</param>
      /// <param name="xmlData">Xml-документ, содержащий элементы конфигурации интегратора, отобранные с помощью xpath-выражения</param>
      public LookupResult(IntegratorObject integrator, bool success, string xmlData)
      {
        if (integrator == null)
          throw new ArgumentNullException();
        if (string.IsNullOrEmpty(xmlData))
          throw new ArgumentException();
        this.integrator = integrator;
        this.success = success;
        this.xmlData = xmlData;
      }

      /// <summary>Возвращает объект интегратора.</summary>
      public IntegratorObject Integrator => this.integrator;

      /// <summary>
      /// Возвращает признак, что конфигурация интегратора содержит элементы, соответствующие xpath-выражению
      /// </summary>
      public bool Success => this.success;

      /// <summary>
      /// Возвращает xml-документ, содержащий элементы конфигурации интегратора, отобранные с помощью xpath-выражения
      /// </summary>
      public string XmlData => this.xmlData;
    }
}
