
// Type: Intermech.Tools.Integrators.IntegratorSettingsCache`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Memoization;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Этот класс позволяет реализовать вычисление и кэширование значений, производных от настроек указанного интегратора.
/// </summary>
/// <typeparam name="T">Тип вычисляемого значения</typeparam>
public sealed class IntegratorSettingsCache<T>
{
  private readonly Func<T> valueFactory;
  private readonly IIntegratorSettingsService settingsService;
  private IStateMonitor settingsMonitor;
  private object valueWriteSeq;
  private T value;

  /// <summary>Создает объект.</summary>
  /// <param name="settingsService">Сервис настроек интегратора</param>
  /// <param name="valueFactory">Фабрика значения</param>
  /// <exception cref="T:ArgumentNullException">settingsService or valueFactory</exception>
  public IntegratorSettingsCache(IIntegratorSettingsService settingsService, Func<T> valueFactory)
  {
    if (settingsService == null)
      throw new ArgumentNullException(nameof (settingsService));
    this.valueFactory = valueFactory != null ? valueFactory : throw new ArgumentNullException(nameof (valueFactory));
    this.settingsService = settingsService;
    this.valueWriteSeq = (object) null;
    this.value = default (T);
  }

  /// <summary>Возвращает вычисленное значение.</summary>
  public T Value
  {
    [DebuggerStepThrough] get
    {
      if (this.settingsMonitor == null)
        this.settingsMonitor = this.settingsService.GetSettingsStateMonitor();
      if (this.settingsMonitor.AnyWritersSince(this.valueWriteSeq))
      {
        object writerSeqNum = this.settingsMonitor.WriterSeqNum;
        this.value = this.valueFactory();
        this.valueWriteSeq = writerSeqNum;
      }
      return this.value;
    }
  }
}
