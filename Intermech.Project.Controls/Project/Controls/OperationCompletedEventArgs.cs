// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.OperationCompletedEventArgs
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project.Controls;

[Serializable]
public class OperationCompletedEventArgs : OperationEventArgs
{
  public OperationCompletedEventArgs([NotNull] string operationName)
    : this(operationName, true)
  {
  }

  public OperationCompletedEventArgs([NotNull] string operationName, bool success)
    : this(operationName, success, (Exception) null)
  {
    this.Success = success;
  }

  public OperationCompletedEventArgs([NotNull] string operationName, bool success, [CanBeNull] Exception exception)
    : base(operationName)
  {
    this.Success = success;
    this.Exception = exception;
  }

  [CanBeNull]
  public Exception Exception { get; set; }

  public bool Success { get; }
}
