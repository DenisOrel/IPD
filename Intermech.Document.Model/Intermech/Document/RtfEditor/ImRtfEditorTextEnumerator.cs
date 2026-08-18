// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.ImRtfEditorTextEnumerator
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.RtfEditor;

public class ImRtfEditorTextEnumerator : IEnumerator<char>, IDisposable, IEnumerator
{
  private ImRtfEditor editor;
  public int CurLine;
  public int CurColumn;
  private int endLine;
  private int endColumn;

  public ImRtfEditorTextEnumerator(ImRtfEditor editor, int startLine = 0, int startColumn = 0)
  {
    this.editor = editor;
    this.SetPositionBeforeMoveNext(startLine, startColumn);
    this.SetEndPosition(-1, -1);
  }

  public ImRtfEditorTextEnumerator(ImRtfEditor editor, TextPosition start, TextPosition end)
  {
    this.editor = editor;
    this.SetPositionBeforeMoveNext(start.Line, start.Column);
    this.SetEndPosition(end.Line, end.Column);
  }

  public char Current
  {
    get
    {
      if (this.CurColumn == -1)
        throw new InvalidOperationException();
      return this.CurLine >= this.editor.TotalLines || this.CurColumn >= this.editor.text[this.CurLine].len ? char.MinValue : this.editor.text[this.CurLine].txt[this.CurColumn];
    }
  }

  object IEnumerator.Current => (object) this.Current;

  public void Dispose() => this.editor = (ImRtfEditor) null;

  public bool MoveNext()
  {
    ++this.CurColumn;
    int num1 = this.endLine != -1 ? this.endLine : this.editor.TotalLines - 1;
    int num2 = this.editor.text[this.CurLine].len;
    if (this.endLine == this.CurLine && num2 > this.endColumn)
      num2 = this.endColumn + 1;
    if (this.CurColumn >= num2)
    {
      this.CurColumn = 0;
      ++this.CurLine;
      while (this.CurLine <= num1 && this.CurColumn >= this.editor.text[this.CurLine].len)
        ++this.CurLine;
    }
    return this.CurLine <= num1;
  }

  public static TextPosition GetPrevPosition(ImRtfEditor editor, TextPosition position)
  {
    TextPosition prevPosition = position;
    --prevPosition.Column;
    if (prevPosition.Column < 0)
    {
      --prevPosition.Line;
      if (prevPosition.Line >= 0)
        prevPosition.Column = editor.text[position.Line].len - 1;
    }
    return prevPosition;
  }

  public static TextPosition GetNextPosition(ImRtfEditor editor, TextPosition position)
  {
    TextPosition nextPosition = position;
    ++nextPosition.Column;
    if (nextPosition.Column >= editor.text[position.Line].len)
    {
      nextPosition.Column = 0;
      ++nextPosition.Line;
    }
    return nextPosition;
  }

  public void Reset()
  {
    this.CurLine = 0;
    this.CurColumn = -1;
  }

  public void SetEndPosition(int endLine, int endColumn)
  {
    this.endLine = endLine;
    this.endColumn = endColumn;
  }

  public TextPosition CurrentPosition => new TextPosition(this.CurLine, this.CurColumn);

  public void SetPositionBeforeMoveNext(int curLine, int curColumn)
  {
    this.CurLine = curLine;
    this.CurColumn = curColumn - 1;
  }
}
