
// Type: Intermech.Navigator.Controls.CustomCellBackgroundEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Аргументы для обработчика событий, связанного с показом пользовательского фона в ячейках
/// </summary>
public class CustomCellBackgroundEventArgs : EventArgs
{
  /// <summary>Аргументы события от грида</summary>
  private iGCustomDrawCellEventArgs _drawArgs;
  /// <summary>Ячейка</summary>
  private iGCell _cell;
  /// <summary>Описание узла</summary>
  private INodeID _nodeID;
  /// <summary>Грид</summary>
  private iGrid _grid;

  /// <summary>Аргументы события от грида</summary>
  public iGCustomDrawCellEventArgs DrawArgs => this._drawArgs;

  /// <summary>Ячейка</summary>
  public iGCell Cell => this._cell;

  /// <summary>Описание узла</summary>
  public INodeID NodeID => this._nodeID;

  /// <summary>Грид</summary>
  public iGrid Grid => this._grid;

  /// <summary>Создать аргументы</summary>
  /// <param name="drawArgs">Аргументы события от грида</param>
  /// <param name="grid">Грид</param>
  /// <param name="cell">Ячейка</param>
  /// <param name="nodeID">Описание узла</param>
  public CustomCellBackgroundEventArgs(
    iGCustomDrawCellEventArgs drawArgs,
    iGrid grid,
    iGCell cell,
    INodeID nodeID)
  {
    this._drawArgs = drawArgs;
    this._grid = grid;
    this._cell = cell;
    this._nodeID = nodeID;
  }
}
