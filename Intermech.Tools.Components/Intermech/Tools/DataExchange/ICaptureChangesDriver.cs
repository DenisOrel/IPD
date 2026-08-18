// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ICaptureChangesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.UI;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Позволяет реализовать драйвер анализа и захвата изменений в файлах объекта.
/// </summary>
public interface ICaptureChangesDriver
{
  /// <summary>
  /// Подготавливает драйвер к обработке нового объекта. Этот метод следует использовать для контроля установки свойств объекта, а также
  /// создания вспомогательных объектов и сервисов.
  /// </summary>
  /// <exception cref="T:Intermech.Tools.DataExchange.DataExchangeConfigurationException">Одно из свойств объекта не инициализировано должным образом</exception>
  void BeginAction();

  /// <summary>Анализирует документы на наличие изменений.</summary>
  /// <param name="ctx">Контекст выполнения. Этот объект содержит все необходимые исходные данные, а также вспомогательные объекты</param>
  /// <param name="progressSink">Индикатор хода выполнения процесса. Параметр может быть не задан</param>
  void Invoke(CaptureChangesContext ctx, IPercentageProgressSink progressSink);

  /// <summary>
  /// Удаляет данные драйвера из базы данных контекста. Это требуется, чтобы базу данных можно было безопасно вернуть в качестве результата выполнения.
  /// Этот метод вызывается даже в случае, когда процесс обработки прерывается по исключительной ситуации.
  /// </summary>
  /// <param name="database">База данных контекста</param>
  void DetachDatabase(CaptureChangesDatabase database);

  /// <summary>
  /// Вызывается в самом конце после успешного завершения процесса. Может использоваться драйвером для извлечения
  /// полезных сведений из рабочего контекста.
  /// </summary>
  void Postprocess();

  /// <summary>
  /// Очищает драйвер в конце обработки объекта. Этот метод следует использовать для освобождения ссылок на вспомогательные объекты и сервисы.
  /// Метод не должен сбрасывать исключений, так как он может являться частью обработчика уже возникшего исключения.
  /// </summary>
  void EndAction();

  /// <summary>
  /// Возвращает признак, что метод BeginAction был выполнен без ошибок.
  /// </summary>
  bool Active { get; }
}
