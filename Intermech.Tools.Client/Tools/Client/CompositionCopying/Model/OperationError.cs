// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.OperationError
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class OperationError
{
  public OperationError(
    string message,
    bool isWarning = false,
    DBObjectGraphVertex vertex = null,
    string solution = "")
  {
    this.Message = message != null ? message : throw new ArgumentNullException(nameof (message));
    this.IsWarning = isWarning;
    this.Vertex = vertex;
    this.Solution = solution;
  }

  public string Message { get; }

  public string Solution { get; }

  public bool IsWarning { get; }

  public DBObjectGraphVertex Vertex { get; }

  public override string ToString() => this.Message;
}
