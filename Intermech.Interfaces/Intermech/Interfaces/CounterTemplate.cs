
// Type: Intermech.Interfaces.CounterTemplate
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Шаблон формулы вычисляемого значения в классификаторах, регистрационных номерах канцелярии
    /// </summary>
    public class CounterTemplate
    {
      /// <summary>Исходный шаблон</summary>
      public string Template { get; }

      /// <summary>Шаблон генератора числа из шаблона регистрационного номера, вида {999}</summary>
      public string ReplaceValue { get; }

      /// <summary>Стартовое значение счетчика</summary>
      public int StartValue { get; }

      /// <summary>Инкремент счетчика</summary>
      public int Increment { get; }

      /// <summary>Максимальное значение счетчика</summary>
      public long MaxValue { get; }

      /// <summary>Индекс первого символа шаблона счетчика</summary>
      public int StartIndex { get; }

      /// <summary>Индекс последнего символа шаблона счетчика</summary>
      public int EndIndex { get; }

      public CounterTemplate(
        string template,
        string replaceValue,
        int startValue,
        int increment,
        long maxValue,
        int startIndex,
        int endIndex)
      {
        this.Template = template;
        this.ReplaceValue = replaceValue;
        this.StartValue = startValue;
        this.Increment = increment;
        this.MaxValue = maxValue;
        this.StartIndex = startIndex;
        this.EndIndex = endIndex;
      }

      public bool Empty => string.IsNullOrEmpty(this.Template);

      public int DigitsCount => !this.Empty ? this.Template.Length : 0;
    }
}
