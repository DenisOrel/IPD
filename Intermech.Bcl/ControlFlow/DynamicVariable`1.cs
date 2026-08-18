
// Type: Intermech.ControlFlow.DynamicVariable`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.ControlFlow
{
    /// <summary>
    /// Реализует переменные, которые предназначены для использования в динамических областях видимости.
    /// </summary>
    /// <typeparam name="T">Тип значений переменной</typeparam>
    public sealed class DynamicVariable<T> : DynamicScopeSymbol
    {
      private readonly string name;
      private T globalValue;

      /// <summary>Создает динамическую переменную.</summary>
      /// <param name="name">Имя переменной</param>
      /// <param name="globalValue">Значение по умолчанию, используемое в случае отсутствия определения переменной в текущей области видимости</param>
      /// <exception cref="T:System.ArgumentNullException">Имя переменной не может быть null</exception>
      public DynamicVariable(string name, T globalValue)
      {
        this.name = name != null ? name : throw new ArgumentNullException(nameof (name));
        this.GlobalValue = globalValue;
      }

      /// <summary>Возвращает видимое имя динамической переменной.</summary>
      /// <returns>Имя динамической переменной</returns>
      public override string ToString() => this.name;

      /// <summary>
      /// Возвращает true, если динамическая переменная определена в одной из доступных областей видимости.
      /// </summary>
      public bool IsDeclared => DynamicScope.IsDeclared((DynamicScopeSymbol) this);

      /// <summary>
      /// Возвращает true, если динамическая переменная определена в текущей области видимости.
      /// </summary>
      public bool IsDeclaredInCurrentScope
      {
        get => DynamicScope.IsDeclaredInCurrentScope((DynamicScopeSymbol) this);
      }

      /// <summary>
      /// Объявляет переменную в текущей области видимости и инициализирует ее указанным значением.
      /// </summary>
      /// <param name="value">Начальное значение переменной</param>
      /// <exception cref="T:Intermech.ControlFlow.DynamicScopeException">Переменная уже объявлена в текущей области видимости</exception>
      public void Declare(T value) => DynamicScope.Declare((DynamicScopeSymbol) this, (object) value);

      /// <summary>
      /// Удаляет объявление переменной из текущей области видимости. Если переменная не объявлена, то метод завершается без ошибок.
      /// </summary>
      /// <param name="dispose">Флаг освобождения ресурсов, связанных со значением переменной</param>
      /// <remarks>
      /// Этот метод необходимо использовать только в случае исключительной ситуации при объявлении группы связанных между собой динамических переменных.
      /// Он предназначен для удаления из области видимости частично сформированных переменных.
      /// </remarks>
      public void RemoveDeclaration(bool dispose = true)
      {
        DynamicScope.RemoveDeclaration((DynamicScopeSymbol) this, dispose);
      }

      /// <summary>
      /// Возвращает или задает значение переменной. Если при чтении значения переменной оказалось, что такая переменная не объявлена ни в одной
      /// доступной области видимости, то будет возвращено глобальное значение по умолчанию. Если при изменении значения переменной оказалось, что
      /// такая переменная не объявлена ни в одной доступной области видимости, то она будет автоматически объявлена в текущей области видимости.
      /// </summary>
      /// <exception cref="T:Intermech.ControlFlow.DynamicScopeException">Область видимости не задана</exception>
      public T Value
      {
        get
        {
          object varValue;
          return !DynamicScope.TryRead((DynamicScopeSymbol) this, out varValue) ? this.GlobalValue : (T) varValue;
        }
        set => DynamicScope.Write((DynamicScopeSymbol) this, (object) value);
      }

      /// <summary>
      /// Возвращает или задает глобальное значение по умолчанию, доступное всем потокам. Оно используется в случае чтение значения для не объявленной
      /// переменной.
      /// </summary>
      /// <returns>Глобальное значение по умолчанию</returns>
      public T GlobalValue
      {
        get
        {
          lock (this)
            return this.globalValue;
        }
        set
        {
          lock (this)
          {
            if (object.Equals((object) this.globalValue, (object) value))
              return;
            this.globalValue = value;
            EventHandler globalValueChanged = this.GlobalValueChanged;
            if (globalValueChanged == null)
              return;
            globalValueChanged((object) this, EventArgs.Empty);
          }
        }
      }

      /// <summary>
      /// Событие, вызываемое при изменении глобального значения по умолчанию для переменной.
      /// </summary>
      public event EventHandler GlobalValueChanged;
    }
}
