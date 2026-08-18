
// Type: Intermech.Text.StringBuilderPoolableFactory
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Pools;
using System;
using System.Text;


namespace Intermech.Text
{
    internal sealed class StringBuilderPoolableFactory : PoolableObjectFactory<StringBuilder>
    {
      private int textCapacity;
      private bool limitTextCapacity;

      public StringBuilderPoolableFactory(int textCapacity, bool limitTextCapacity)
      {
        this.textCapacity = textCapacity >= 1 ? textCapacity : throw new ArgumentOutOfRangeException(nameof (textCapacity));
        this.limitTextCapacity = limitTextCapacity;
      }

      /// <summary>
      /// Создает экземпляр объект. Метод используется при недостатке объектов в пуле для пополнения пула.
      /// </summary>
      /// <returns>Экземпляр объекта</returns>
      public sealed override StringBuilder CreateObject() => new StringBuilder(this.textCapacity);

      /// <summary>Деактивирует и очищает объект перед возвратом в пул.</summary>
      /// <param name="item">Экземпляр объекта</param>
      public sealed override void DeactivateObject(StringBuilder item)
      {
        base.DeactivateObject(item);
        if (item.Length != 0)
          item.Clear();
        if (!this.limitTextCapacity || item.Capacity <= this.textCapacity)
          return;
        item.Capacity = this.textCapacity;
      }
    }
}
