
// Type: Intermech.Expressions.ParseEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Expressions;

public class ParseEventArgs : EventArgs
{
  private ExpressionTree _tree;
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
