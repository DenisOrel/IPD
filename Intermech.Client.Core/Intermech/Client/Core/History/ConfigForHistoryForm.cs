
// Type: Intermech.Client.Core.History.ConfigForHistoryForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.History;

/// <summary>
/// Статический класс для хранения конфигурации в "HistoryForm".
/// </summary>
internal class ConfigForHistoryForm
{
  public static readonly string Section = "HistoryForm";
  public static readonly string SortOrder = nameof (SortOrder);
  public static readonly string UseUserHistory = nameof (UseUserHistory);
  /// <summary>
  /// История м.б. показана
  /// Для данного объекта
  /// Для всех объектов данного типа
  /// Для всех объектов
  /// </summary>
  public static readonly string HistoryType = nameof (HistoryType);
}
