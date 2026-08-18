// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TextPosition
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model;

public struct TextPosition(int line, int column) : IComparable<TextPosition>
{
  public int Line = line;
  public int Column = column;

  public static TextPosition Empty => new TextPosition(-1, -1);

  public bool IsEmpty => this.Line <= -1 || this.Column <= -1;

  /// <summary>Сравнить эту позицию с другой позицией</summary>
  /// <param name="other">Другая позиция</param>
  /// <returns>Возвращает значение меньше нуля, если этот экземпляр меньше, чем аргумент.
  /// Возвращает значение равное нулю, если этот экземпляр равен аргументу.
  /// Возвращает значение больше нуля, если этот экземпляр больше, чем аргумент.</returns>
  public int CompareTo(TextPosition other)
  {
    if (this.Line != other.Line)
      return this.Line.CompareTo(other.Line);
    return this.Column == other.Column ? 0 : this.Column.CompareTo(other.Column);
  }

  public override string ToString() => $"{this.Line}, {this.Column}";
}
