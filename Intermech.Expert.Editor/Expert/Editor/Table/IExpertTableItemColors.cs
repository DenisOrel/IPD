// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.IExpertTableItemColors
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System;
using System.Drawing;

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Интерфес для одного элемента (ячейки)</summary>
public interface IExpertTableItemColors
{
  /// <summary>Цвет для текста</summary>
  Color ForeColor { get; set; }

  /// <summary>Цвет для фона</summary>
  Color BackColor { get; set; }

  /// <summary>Событие на изменение цвета текста</summary>
  event EventHandler ForeColorChanged;

  /// <summary>Событие на изменение цвета фона</summary>
  event EventHandler BackColorChanged;
}
