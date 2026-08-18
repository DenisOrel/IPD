// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ComboBoxExItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;

#nullable disable
namespace Intermech.Workflow.Design;

[Serializable]
public class ComboBoxExItem
{
  private string _text;
  private int _imageIndex;

  public string Text
  {
    get => this._text;
    set => this._text = value;
  }

  public int ImageIndex
  {
    get => this._imageIndex;
    set => this._imageIndex = value;
  }

  public ComboBoxExItem()
    : this("")
  {
  }

  public ComboBoxExItem(string text)
    : this(text, -1)
  {
  }

  public ComboBoxExItem(string text, int imageIndex)
  {
    this._text = text;
    this._imageIndex = imageIndex;
  }

  public override string ToString() => this._text;
}
