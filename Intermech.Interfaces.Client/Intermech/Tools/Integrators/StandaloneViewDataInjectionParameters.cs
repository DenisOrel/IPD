// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneViewDataInjectionParameters
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.StandaloneView;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Параметры операции по записи в файл документа сведения для автономного просмотра.
/// </summary>
public class StandaloneViewDataInjectionParameters
{
  /// <summary>Идентификатор версии документа.</summary>
  public long ObjectId { get; set; }

  /// <summary>Имя файл документа в файловом атрибуте.</summary>
  public string FileName { get; set; }

  /// <summary>
  /// Абсолютный путь к файлу документа, который будет передан приложению.
  /// </summary>
  public string FilePath { get; set; }

  /// <summary>
  /// Настройки автономного просмотра для документов данного типа.
  /// </summary>
  public StandaloneViewObjectTypeSettings ObjectTypeSettings { get; set; }

  /// <summary>
  /// Модификатор поведения операции подготовки документа, влияющий на запись подписей документа в файл документа.
  /// Если он установлен, то в файл записывается только фамилия подписавшего, а дата подписания и сама подпись остаются пустыми.
  /// </summary>
  /// <remarks>
  /// Этот модификатор не относится к настройкам просмотра для типа объектов, он используется только в некоторых специальных
  /// режимах просмотра (например, по команде "Смотреть...").
  /// </remarks>
  public bool InjectSignNamesOnly { get; set; }
}
