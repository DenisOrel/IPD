
// Type: Intermech.Data.EncodeAttributesOptions
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.Data
{
    public class EncodeAttributesOptions : IAttributeCodecOptions
    {
      private bool optimizeEmptyValues = true;
      private bool reportErrorsOnly = true;
      private Dictionary<StringKey, object> properties;

      /// <summary>
      /// Возвращает или задает режим оптимизации записи пустых значений.
      /// Если на принимающей стороне нет одноименного параметра, то пустое значение не записывается,
      /// так как считается, что отсутствующее значение эквивалентно пустому.
      /// </summary>
      public bool OptimizeEmptyValues
      {
        get => this.optimizeEmptyValues;
        set => this.optimizeEmptyValues = value;
      }

      /// <summary>
      /// Задает или возвращает способ обработки ошибок кодирования для некритических атрибутов. Если это свойство
      /// установлено в true, то искючения будут подавлены и выведены в прокотол обработки.
      /// </summary>
      public bool ReportErrorsOnly
      {
        get => this.reportErrorsOnly;
        set => this.reportErrorsOnly = value;
      }

      public IDictionary<StringKey, object> Properties
      {
        get
        {
          if (this.properties == null)
            this.properties = new Dictionary<StringKey, object>();
          return (IDictionary<StringKey, object>) this.properties;
        }
      }
    }
}
