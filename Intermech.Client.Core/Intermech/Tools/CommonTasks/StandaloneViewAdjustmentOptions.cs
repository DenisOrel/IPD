
// Type: Intermech.Tools.CommonTasks.StandaloneViewAdjustmentOptions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Tools.CommonTasks;

/// <summary>
/// Опции регулировки настроек для операции внедрения сведений для автономного просмотра.
/// Используются командой "Смотреть...".
/// </summary>
internal sealed class StandaloneViewAdjustmentOptions
{
  /// <summary>
  /// Возвращает или задает флаг, разрешающий запись подписей в соответствии с настройками типа объекта.
  /// </summary>
  public bool EnableInjectSigns { get; set; }

  /// <summary>
  /// Возвращает или задает флаг, разрешающий запись контрольной суммы в соответствии с настройками типа объекта.
  /// </summary>
  public bool EnableInjectFileChecksum { get; set; }

  /// <summary>
  /// Возвращает или задает флаг, разрешающий запись атрибутов объекта в соответствии с настройками типа объекта.
  /// </summary>
  public bool EnableInjectAttributes { get; set; }

  /// <summary>
  /// Инициализирует значения всех флагов указанным значением.
  /// </summary>
  /// <param name="value">Значение для инициализации всех флагов</param>
  public void SetAll(bool value)
  {
    this.EnableInjectSigns = value;
    this.EnableInjectFileChecksum = value;
    this.EnableInjectAttributes = value;
  }

  /// <summary>
  /// Проверяет, что все флаги установлены в true.
  /// Метод используется для подтверждения, что настройки типа объекта должны быть использованы без каких-либо изменений.
  /// </summary>
  /// <returns>true - если все флаги установлены</returns>
  public bool IsFullyEnabled()
  {
    return this.EnableInjectSigns && this.EnableInjectFileChecksum && this.EnableInjectAttributes;
  }
}
