// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.BorderPositionChanged_EventArgs
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>Аргументы события IdentChanged</summary>
public class BorderPositionChanged_EventArgs : EventArgs
{
  /// <summary>Тип измененния границ</summary>
  public enumTypeDrag Type;
  /// <summary>Индекс перенесенной границы</summary>
  public int Index;
  /// <summary>Старое значение</summary>
  public float OldValue;

  /// <summary>Конструктор</summary>
  /// <param name="newName">Новое имя</param>
  public BorderPositionChanged_EventArgs(enumTypeDrag type, int index, float oldValue)
  {
    this.Type = type;
    this.Index = index;
    this.OldValue = oldValue;
  }
}
