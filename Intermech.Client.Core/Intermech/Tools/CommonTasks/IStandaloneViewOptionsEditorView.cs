
// Type: Intermech.Tools.CommonTasks.IStandaloneViewOptionsEditorView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using Intermech.Mvp.Components;


namespace Intermech.Tools.CommonTasks;

/// <summary>
/// Интерфейс вида MVP для редактора опций просмотра по команде "Смотреть...".
/// </summary>
internal interface IStandaloneViewOptionsEditorView : IView, IOperationConfirmationView
{
  /// <summary>Значение переключателя "Разрешить запись подписей"</summary>
  bool EnableInjectSigns { get; set; }

  /// <summary>
  /// Значение переключателя "Записывать только фамилию подписавшего"
  /// </summary>
  bool InjectSignNamesOnly { get; set; }

  /// <summary>
  /// Значение переключателя "Разрешить запись контрольной суммы"
  /// </summary>
  bool EnableInjectFileChecksum { get; set; }

  /// <summary>
  /// Значение переключателя "Разрешить запись атрибутов объекта"
  /// </summary>
  bool EnableInjectAttributes { get; set; }
}
