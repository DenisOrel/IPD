// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneViewServiceResult
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Результат работы сервиса интегратора, отвечающего за поддержку автономного просмотра документов.
/// </summary>
public class StandaloneViewServiceResult
{
  private List<ErrorInfo> errors;

  /// <summary>Создает объект.</summary>
  public StandaloneViewServiceResult() => this.errors = new List<ErrorInfo>();

  /// <summary>
  /// Признак успешного или неуспешного выполнения операции.
  /// </summary>
  public bool IsSuccessful
  {
    [DebuggerStepThrough] get => this.errors.Count == 0;
  }

  /// <summary>Коллекция ошибок, возникших при выполнении операции.</summary>
  public ICollection<ErrorInfo> Errors
  {
    [DebuggerStepThrough] get => (ICollection<ErrorInfo>) this.errors;
  }
}
