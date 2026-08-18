// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Editor.FieldContents.ParseEventArgs
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Expressions;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Editor.FieldContents;

public class ParseEventArgs : EventArgs
{
  private readonly ExpressionTree _tree;
  private object _result;

  public ExpressionTree Tree => this._tree;

  public object Result
  {
    get => this._result;
    set => this._result = value;
  }

  public ParseEventArgs(ExpressionTree tree)
  {
    this._tree = tree;
    this._result = (object) null;
  }
}
