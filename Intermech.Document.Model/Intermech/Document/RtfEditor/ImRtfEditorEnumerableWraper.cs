// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.ImRtfEditorEnumerableWraper
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class ImRtfEditorEnumerableWraper : IEnumerable<char>, IEnumerable
{
  private ImRtfEditor editor;
  private int startLine;
  private int startColumn;

  public ImRtfEditorEnumerableWraper(ImRtfEditor editor, int startLine = 0, int startColumn = 0)
  {
    this.editor = editor;
    this.startLine = startLine;
    this.startColumn = startColumn;
  }

  public IEnumerator<char> GetEnumerator()
  {
    ImRtfEditorTextEnumerator enumerator = new ImRtfEditorTextEnumerator(this.editor);
    enumerator.SetPositionBeforeMoveNext(this.startLine, this.startColumn);
    return (IEnumerator<char>) enumerator;
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
