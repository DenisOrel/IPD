
// Type: Intermech.Diagnostics.MethodCallTracer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Diagnostics;


namespace Intermech.Diagnostics
{
    /// <summary>
    /// Реализует вывод в журнал трассировки имен вызываемых методов и их аргументов.
    /// </summary>
    public class MethodCallTracer
    {
      private TraceSwitch traceSwitch;
      private IMethodCallFormatter formatter;

      /// <summary>Создает объект.</summary>
      /// <param name="traceSwitch">Ключ трассировки, управляющий включением и выключением текущего объекта</param>
      /// <exception cref="T:ArgumentNullException">traceSwitch</exception>
      public MethodCallTracer(TraceSwitch traceSwitch)
        : this(traceSwitch, (IMethodCallFormatter) new MethodCallFormatter())
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="traceSwitch">Ключ трассировки, управляющий включением и выключением текущего объекта</param>
      /// <param name="formatter">Форматтер для аргументов вызываемых методов</param>
      /// <exception cref="T:ArgumentNullException">traceSwitch || argumentFormatter</exception>
      public MethodCallTracer(TraceSwitch traceSwitch, IMethodCallFormatter formatter)
      {
        if (traceSwitch == null)
          throw new ArgumentNullException(nameof (traceSwitch));
        if (formatter == null)
          throw new ArgumentNullException(nameof (formatter));
        this.traceSwitch = traceSwitch;
        this.formatter = formatter;
      }

      /// <summary>
      /// Возвращает ключ трассировки, управляющий включением и выключением текущего объекта.
      /// Включение происходит при значении ключа TraceVerbose.
      /// </summary>
      public TraceSwitch Switch
      {
        [DebuggerStepThrough] get => this.traceSwitch;
      }

      /// <summary>Возвращает признак, что текущий объект активен.</summary>
      public bool Enabled
      {
        [DebuggerStepThrough] get => this.traceSwitch.TraceVerbose;
      }

      /// <summary>
      /// Выводит в журнал трассировки информацию о вызванном методе.
      /// </summary>
      /// <param name="methodName">Имя вызванного метода</param>
      /// <exception cref="T:ArgumentNullException">methodName</exception>
      public void AddToTrace(string methodName)
      {
        if (methodName == null)
          throw new ArgumentNullException(nameof (methodName));
        Trace.WriteLine(methodName);
      }

      /// <summary>
      /// Выводит в журнал трассировки информацию о вызванном методе.
      /// </summary>
      /// <param name="methodName">Имя вызванного метода</param>
      /// <param name="arg1">1-й аргумент вызванного метода</param>
      /// <exception cref="T:ArgumentNullException">methodName</exception>
      public void AddToTrace<T1>(string methodName, T1 arg1)
      {
        if (methodName == null)
          throw new ArgumentNullException(nameof (methodName));
        Trace.WriteLine($"{methodName} with args: {this.FormatArgument((object) arg1)}");
      }

      /// <summary>
      /// Выводит в журнал трассировки информацию о вызванном методе.
      /// </summary>
      /// <param name="methodName">Имя вызванного метода</param>
      /// <param name="arg1">1-й аргумент вызванного метода</param>
      /// <param name="arg2">2-й аргумент вызванного метода</param>
      /// <exception cref="T:ArgumentNullException">methodName</exception>
      public void AddToTrace<T1, T2>(string methodName, T1 arg1, T2 arg2)
      {
        if (methodName == null)
          throw new ArgumentNullException(nameof (methodName));
        Trace.WriteLine($"{methodName} with args: {this.FormatArgument((object) arg1)}, {this.FormatArgument((object) arg2)}");
      }

      /// <summary>
      /// Выводит в журнал трассировки информацию о вызванном методе.
      /// </summary>
      /// <param name="methodName">Имя вызванного метода</param>
      /// <param name="arg1">1-й аргумент вызванного метода</param>
      /// <param name="arg2">2-й аргумент вызванного метода</param>
      /// <param name="arg3">3-й аргумент вызванного метода</param>
      /// <exception cref="T:ArgumentNullException">methodName</exception>
      public void AddToTrace<T1, T2, T3>(string methodName, T1 arg1, T2 arg2, T3 arg3)
      {
        if (methodName == null)
          throw new ArgumentNullException(nameof (methodName));
        Trace.WriteLine($"{methodName} with args: {this.FormatArgument((object) arg1)}, {this.FormatArgument((object) arg2)}, {this.FormatArgument((object) arg3)}");
      }

      /// <summary>
      /// Выводит в журнал трассировки информацию о вызванном методе.
      /// </summary>
      /// <param name="methodName">Имя вызванного метода</param>
      /// <param name="arg1">1-й аргумент вызванного метода</param>
      /// <param name="arg2">2-й аргумент вызванного метода</param>
      /// <param name="arg3">3-й аргумент вызванного метода</param>
      /// <param name="arg4">4-й аргумент вызванного метода</param>
      /// <exception cref="T:ArgumentNullException">methodName</exception>
      public void AddToTrace<T1, T2, T3, T4>(string methodName, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
      {
        if (methodName == null)
          throw new ArgumentNullException(nameof (methodName));
        Trace.WriteLine($"{methodName} with args: {this.FormatArgument((object) arg1)}, {this.FormatArgument((object) arg2)}, {this.FormatArgument((object) arg3)}, {this.FormatArgument((object) arg4)}");
      }

      private string FormatArgument(object argument) => this.formatter.FormatArgument(argument);
    }
}
