// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.OpenFilesUnloadResult
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Результат операции закрытия документов, открытых в CAD-системе.
/// Реализация является immutable и thread safe.
/// </summary>
public sealed class OpenFilesUnloadResult
{
  /// <summary>Создает объект.</summary>
  /// <param name="isSuccessful">Признак успешного или неуспешного выполнения операции</param>
  /// <param name="reloadState">Объект для восстановления состояния CAD-системы, которое было до закрытия документов</param>
  public OpenFilesUnloadResult(bool isSuccessful, object reloadState)
  {
    this.IsSucceessful = isSuccessful;
    this.ReloadState = reloadState;
  }

  /// <summary>
  /// Признак успешного или неуспешного выполнения операции.
  /// </summary>
  public bool IsSucceessful { get; }

  /// <summary>
  /// Объект для восстановления состояния CAD-системы, которое было до закрытия документов.
  /// Значение свойства может быть равно null, если восстановление не требуется или невозможно.
  /// </summary>
  /// <remarks>
  /// Значение свойства используется в тех случаях, когда требуется переоткрыть закрытые ранее документы.
  /// </remarks>
  public object ReloadState { get; }
}
