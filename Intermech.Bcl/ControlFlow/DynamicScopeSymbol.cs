
// Type: Intermech.ControlFlow.DynamicScopeSymbol
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.ControlFlow
{
    /// <summary>Реализует базовый класс для динамических переменных.</summary>
    public abstract class DynamicScopeSymbol
    {
      private readonly int id;

      /// <summary>Создает новую динамическую переменную.</summary>
      protected DynamicScopeSymbol() => this.id = RuntimeId.Create();

      /// <summary>
      /// Устанавливает идентичность идентификатора переменной и указанного объекта.
      /// </summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты идентичны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is DynamicScopeSymbol dynamicScopeSymbol) ? base.Equals(obj) : dynamicScopeSymbol.id == this.id;
      }

      /// <summary>Вычисляет хэш-код для идентификатора переменной.</summary>
      /// <returns>Хэш-код идентификатора переменной</returns>
      public override int GetHashCode() => this.id;

      /// <summary>Возвращает видимое имя динамической переменной.</summary>
      /// <returns>Имя динамической переменной</returns>
      public override string ToString() => $"Dynamic variable #{this.id}";
    }
}
