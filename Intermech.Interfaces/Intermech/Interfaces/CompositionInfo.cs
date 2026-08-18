
// Type: Intermech.Interfaces.CompositionInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Информация о состоянии серверной задачи, выполняющей запросы по получению состава / применяемости
    /// </summary>
    [Serializable]
    public class CompositionInfo
    {
      /// <summary>Процент выполнения текущей задачи, 0 .. 100</summary>
      public int Percent;
      /// <summary>Возникла ли ошибка во время работы текущей задачи</summary>
      public bool ErrorPresent;
      /// <summary>
      /// Если ErrorPresent = true, в этом поле присутствует информация о возникшем исключении
      /// </summary>
      public Exception ErrorException;
      /// <summary>Результат работы текущей задачи, если Percent = 100</summary>
      public object Result;

      /// <summary>
      /// Создать экземпляр класса, с указанным значением процента
      /// </summary>
      /// <param name="percent">Процент выполнения текущей задачи, 0 .. 100</param>
      public CompositionInfo(int percent) => this.Percent = percent;

      /// <summary>Создать экземпляр класса, с указанным исключением</summary>
      /// <param name="errorException">Информация о возникшем исключении</param>
      public CompositionInfo(Exception errorException)
      {
        this.ErrorException = errorException;
        this.ErrorPresent = true;
      }

      /// <summary>
      /// Создать экземпляр класса, с указанными результатами работы текущей задачи
      /// </summary>
      /// <param name="result">Результат работы текущей задачи</param>
      public CompositionInfo(object result)
      {
        this.Percent = 100;
        this.Result = result;
      }
    }
}
