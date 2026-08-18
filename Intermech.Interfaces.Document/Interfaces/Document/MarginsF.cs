// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.MarginsF
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Поля документа или ячейки</summary>
[Serializable]
public class MarginsF : ICloneable
{
  public float Left;
  public float Right;
  public float Top;
  public float Bottom;

  /// <summary>Конструктор</summary>
  [DebuggerStepThrough]
  public MarginsF(float left, float right, float top, float bottom)
  {
    this.Left = left;
    this.Right = right;
    this.Top = top;
    this.Bottom = bottom;
  }

  /// <summary>Создать копию объекта</summary>
  public MarginsF Clone() => new MarginsF(this.Left, this.Right, this.Top, this.Bottom);

  /// <summary>Создать копию объекта</summary>
  object ICloneable.Clone() => (object) this.Clone();
}
