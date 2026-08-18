// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.EditorTextBlock
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.Model;

internal struct EditorTextBlock(TextPosition start, TextPosition end)
{
  public TextPosition Start = start;
  public TextPosition End = end;

  public bool IsEmpty => this.Start.IsEmpty || this.End.IsEmpty;

  public static EditorTextBlock Empty
  {
    get => new EditorTextBlock(TextPosition.Empty, TextPosition.Empty);
  }

  public override string ToString() => $"{this.Start} - {this.End}";
}
