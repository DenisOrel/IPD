
// Type: Intermech.Data.ContainerValues
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Data
{
    /// <summary>
    /// Описывает контейнер значений, полученных из файла документа или другого персистентного источника. Сам контейнер может содержать параметры документа, изделия, связи и др.
    /// </summary>
    public sealed class ContainerValues : ICloneable
    {
      private readonly ValueBag bag;
      private readonly bool isOpenMetadata;

      /// <summary>Создает объект.</summary>
      /// <param name="bag">Контейнер значений</param>
      /// <param name="isOpenMetadata">Признак, разрешено ли добавление произвольных значений в контейнер</param>
      public ContainerValues(ValueBag bag, bool isOpenMetadata)
      {
        this.bag = bag != null ? bag : throw new ArgumentNullException(nameof (bag));
        this.isOpenMetadata = isOpenMetadata;
      }

      /// <summary>Клонирует текущий объект.</summary>
      /// <returns>Клон объекта</returns>
      public ContainerValues Clone() => new ContainerValues(this.bag.Clone(), this.isOpenMetadata);

      /// <summary>Клонирует текущий объект.</summary>
      /// <returns>Клон объекта</returns>
      object ICloneable.Clone() => (object) this.Clone();

      /// <summary>Возвращает контейнер значений.</summary>
      public ValueBag Bag => this.bag;

      /// <summary>
      /// Возвращает признак, разрешено ли добавление произвольных значений в контейнер.
      /// </summary>
      public bool IsOpenMetadata => this.isOpenMetadata;
    }
}
