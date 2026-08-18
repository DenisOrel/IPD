// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.TextSelection
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Координаты выделенного текста</summary>
/// <summary>Конструктор</summary>
/// <param name="position">Позиция первого выделенного символа</param>
/// <param name="length">Длина выделения</param>
public struct TextSelection(int position, int length)
{
  /// <summary>Позиция первого выделенного символа</summary>
  public int Position = position;
  /// <summary>Длина выделения</summary>
  public int Length = length;

  /// <summary>Позиция конца выделения</summary>
  public int EndPosition
  {
    [DebuggerStepThrough] get => this.Position + this.Length;
    set
    {
      if (this.EndPosition == value)
        return;
      this.Length = value - this.Position;
    }
  }
}
