// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.VirtualColumn
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Виртуальный узел Столбец таблицы</summary>
[Serializable]
public class VirtualColumn : RectangleElement
{
  private RowColParams columnParams;

  /// <summary>Параметры столбца сетки</summary>
  [Category("Debug")]
  [System.ComponentModel.ReadOnly(true)]
  public RowColParams ColumnParams
  {
    [DebuggerStepThrough] get => this.columnParams;
    set => this.columnParams = value;
  }

  /// <summary>Получение ширины дочерних ячеек</summary>
  private float? GetWidthForUser(RectangleElement cell, float? cur_width)
  {
    if (cell.IsSingleCell)
      return cell.WidthForUser;
    if (cell.Nodes.Count == 0)
      return cur_width;
    float? widthForUser1;
    if (!(widthForUser1 = this.GetWidthForUser(cell.Nodes[0] as RectangleElement, cur_width)).HasValue)
      return new float?();
    float? widthForUser2;
    if (cur_width.HasValue)
    {
      float? nullable = widthForUser1;
      widthForUser2 = cur_width;
      if (!((double) nullable.GetValueOrDefault() == (double) widthForUser2.GetValueOrDefault() & nullable.HasValue == widthForUser2.HasValue))
      {
        widthForUser2 = new float?();
        return widthForUser2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      widthForUser2 = widthForUser1;
      float? widthForUser3 = this.GetWidthForUser(node, widthForUser1);
      if (!((double) widthForUser2.GetValueOrDefault() == (double) widthForUser3.GetValueOrDefault() & widthForUser2.HasValue == widthForUser3.HasValue))
        return new float?();
    }
    return widthForUser1;
  }

  /// <summary>Изменение ширины дочерних ячеек</summary>
  private RectangleElement SetWidthForUser(float value, RectangleElement cell)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetWidthForUser(value, cell.Nodes[index] as RectangleElement);
      return rectangleElement;
    }
    SizeF size = new SizeF(value, 0.0f);
    if (this.page != null)
      this.page.ConvertUserToInternal(size);
    RectangleF properBounds = cell.ProperBounds with
    {
      Width = value
    };
    cell.overrideFlags |= OverrideFlags.Width;
    cell.overrideFlags2 |= OverrideFlags2.ColumnWidth;
    cell.AssignProperBounds(properBounds, true, false, false);
    cell.RecalcRelativeSize();
    return cell;
  }

  /// <summary>Получение высоты дочерних ячеек</summary>
  private float? GetHeightForUser(RectangleElement cell, float? cur_height)
  {
    if (cell.IsSingleCell)
      return cell.HeightForUser;
    if (cell.Nodes.Count == 0)
      return cur_height;
    float? heightForUser1;
    if (!(heightForUser1 = this.GetHeightForUser(cell.Nodes[0] as RectangleElement, cur_height)).HasValue)
      return new float?();
    float? heightForUser2;
    if (cur_height.HasValue)
    {
      float? nullable = heightForUser1;
      heightForUser2 = cur_height;
      if (!((double) nullable.GetValueOrDefault() == (double) heightForUser2.GetValueOrDefault() & nullable.HasValue == heightForUser2.HasValue))
      {
        heightForUser2 = new float?();
        return heightForUser2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      heightForUser2 = heightForUser1;
      float? heightForUser3 = this.GetHeightForUser(node, heightForUser1);
      if (!((double) heightForUser2.GetValueOrDefault() == (double) heightForUser3.GetValueOrDefault() & heightForUser2.HasValue == heightForUser3.HasValue))
        return new float?();
    }
    return heightForUser1;
  }

  /// <summary>Изменение высоты дочерних ячеек</summary>
  private RectangleElement SetHeightForUser(float value, RectangleElement cell)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetHeightForUser(value, cell.Nodes[index] as RectangleElement);
      return rectangleElement;
    }
    cell.SetHeightForUser(value, false, false);
    return cell;
  }

  /// <summary>Получение левой коорд. левой из дочерних ячеек в Bounds координатах</summary>
  private float GetLeftForUser(RectangleElement cell, float cur_var, ref PageData pg)
  {
    float leftForUser1 = cur_var;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        float leftForUser2 = this.GetLeftForUser(cell.Nodes[index] as RectangleElement, leftForUser1, ref pg);
        leftForUser1 = Math.Min(leftForUser1, leftForUser2);
      }
      return leftForUser1;
    }
    pg = cell.Page;
    return cell.Bounds.Left;
  }

  /// <summary>Получение правой коорд. правой из дочерних ячеек в Bounds координатах</summary>
  private float GetRightForUser(RectangleElement cell, float cur_var, ref PageData pg)
  {
    float rightForUser1 = cur_var;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        float rightForUser2 = this.GetRightForUser(cell.Nodes[index] as RectangleElement, rightForUser1, ref pg);
        rightForUser1 = Math.Max(rightForUser1, rightForUser2);
      }
      return rightForUser1;
    }
    pg = cell.Page;
    return cell.Bounds.Right;
  }

  /// <summary>Получение нижней коорд. правой из дочерних ячеек в Bounds координатах</summary>
  private float GetBottomForUser(RectangleElement cell, float cur_var, ref PageData pg)
  {
    float bottomForUser1 = cur_var;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        float bottomForUser2 = this.GetBottomForUser(cell.Nodes[index] as RectangleElement, bottomForUser1, ref pg);
        bottomForUser1 = Math.Max(bottomForUser1, bottomForUser2);
      }
      return bottomForUser1;
    }
    pg = cell.Page;
    return cell.Bounds.Bottom;
  }

  /// <summary>Получение верхней коорд. левой из дочерних ячеек в Bounds координатах</summary>
  private float GetTopForUser(RectangleElement cell, float cur_var, ref PageData pg)
  {
    float topForUser1 = cur_var;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        float topForUser2 = this.GetTopForUser(cell.Nodes[index] as RectangleElement, topForUser1, ref pg);
        topForUser1 = Math.Min(topForUser1, topForUser2);
      }
      return topForUser1;
    }
    pg = cell.Page;
    return cell.Bounds.Top;
  }

  /// <summary>Получение имен реальных ячеек виртуальной ячейки</summary>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="cur_name">текущее имя</param>
  /// <returns>Возвращает null, если имена не совпадают</returns>
  private string GetName(RectangleElement cell, string cur_name)
  {
    if (cell == null)
      return "";
    if (cell.IsSingleCell)
      return cell.Name;
    if (cell.NodesCount == 0)
      return cur_name;
    string name;
    if ((name = this.GetName(cell.Nodes[0] as RectangleElement, cur_name)) == null)
      return (string) null;
    if (cur_name != null && name != cur_name)
      return (string) null;
    int index = 1;
    for (int nodesCount = cell.NodesCount; index < nodesCount; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      if (name != this.GetName(node, name))
        return (string) null;
    }
    return name;
  }

  /// <summary>Установка имен реальных ячеек виртуальной ячейки</summary>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="cur_name">устанавливаемое имя</param>
  /// <returns>Возвращает последнюю ячейку</returns>
  private RectangleElement SetName(RectangleElement cell, string cur_name)
  {
    RectangleElement rectangleElement = cell != null ? cell : throw new ArgumentNullException(nameof (cell));
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetName(cell.Nodes[index] as RectangleElement, cur_name);
      return rectangleElement;
    }
    cell.Name = cur_name;
    return cell;
  }

  /// <summary>Получение типов реальных ячеек виртуальной ячейки</summary>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="cur_type">текущее имя</param>
  /// <returns>Возвращает null, если типы не совпадают</returns>
  private string GetNodeTypeCaption(RectangleElement cell, string cur_type)
  {
    if (cell.IsSingleCell)
      return cell.NodeTypeCaption;
    if (cell.Nodes.Count == 0)
      return cur_type;
    string nodeTypeCaption;
    if ((nodeTypeCaption = this.GetNodeTypeCaption(cell.Nodes[0] as RectangleElement, cur_type)) == null)
      return (string) null;
    if (cur_type != null && nodeTypeCaption != cur_type)
      return (string) null;
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      if (nodeTypeCaption != this.GetNodeTypeCaption(node, nodeTypeCaption))
        return (string) null;
    }
    return nodeTypeCaption;
  }

  private bool? GetVisible(RectangleElement cell, bool? cur_vis)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.Visible);
    if (cell.Nodes.Count == 0)
      return cur_vis;
    bool? visible1;
    if (!(visible1 = this.GetVisible(cell.Nodes[0] as RectangleElement, cur_vis)).HasValue)
      return new bool?();
    bool? visible2;
    if (cur_vis.HasValue)
    {
      bool? nullable = visible1;
      visible2 = cur_vis;
      if (!(nullable.GetValueOrDefault() == visible2.GetValueOrDefault() & nullable.HasValue == visible2.HasValue))
      {
        visible2 = new bool?();
        return visible2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      visible2 = visible1;
      bool? visible3 = this.GetVisible(node, visible1);
      if (!(visible2.GetValueOrDefault() == visible3.GetValueOrDefault() & visible2.HasValue == visible3.HasValue))
        return new bool?();
    }
    return visible1;
  }

  private RectangleElement SetVisible(RectangleElement cell, bool? cur_vis)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetVisible(cell.Nodes[index] as RectangleElement, cur_vis);
      return rectangleElement;
    }
    cell.SetVisible(cur_vis.Value, false, true, false, false, false);
    return cell;
  }

  private float? GetDefaultRowSize(RectangleElement cell, float? cur_var)
  {
    if (cell.IsSingleCell)
      return new float?(cell.DefaultRowSize);
    if (cell.Nodes.Count == 0)
      return cur_var;
    float? defaultRowSize1;
    if (!(defaultRowSize1 = this.GetDefaultRowSize(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new float?();
    float? defaultRowSize2;
    if (cur_var.HasValue)
    {
      float? nullable = defaultRowSize1;
      defaultRowSize2 = cur_var;
      if (!((double) nullable.GetValueOrDefault() == (double) defaultRowSize2.GetValueOrDefault() & nullable.HasValue == defaultRowSize2.HasValue))
      {
        defaultRowSize2 = new float?();
        return defaultRowSize2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      defaultRowSize2 = defaultRowSize1;
      float? defaultRowSize3 = this.GetDefaultRowSize(node, defaultRowSize1);
      if (!((double) defaultRowSize2.GetValueOrDefault() == (double) defaultRowSize3.GetValueOrDefault() & defaultRowSize2.HasValue == defaultRowSize3.HasValue))
        return new float?();
    }
    return defaultRowSize1;
  }

  private RectangleElement SetDefaultRowSize(RectangleElement cell, float? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetDefaultRowSize(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.SetDefaultRowSize(cur_var.Value, true, true, false, false);
    return cell;
  }

  private bool? GetIsFixedSizeRows(RectangleElement cell, bool? cur_var)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.IsFixedSizeRows);
    if (cell.Nodes.Count == 0)
      return cur_var;
    bool? isFixedSizeRows1;
    if (!(isFixedSizeRows1 = this.GetIsFixedSizeRows(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new bool?();
    bool? isFixedSizeRows2;
    if (cur_var.HasValue)
    {
      bool? nullable = isFixedSizeRows1;
      isFixedSizeRows2 = cur_var;
      if (!(nullable.GetValueOrDefault() == isFixedSizeRows2.GetValueOrDefault() & nullable.HasValue == isFixedSizeRows2.HasValue))
      {
        isFixedSizeRows2 = new bool?();
        return isFixedSizeRows2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      isFixedSizeRows2 = isFixedSizeRows1;
      bool? isFixedSizeRows3 = this.GetIsFixedSizeRows(node, isFixedSizeRows1);
      if (!(isFixedSizeRows2.GetValueOrDefault() == isFixedSizeRows3.GetValueOrDefault() & isFixedSizeRows2.HasValue == isFixedSizeRows3.HasValue))
        return new bool?();
    }
    return isFixedSizeRows1;
  }

  private bool? GetTransparent(RectangleElement cell, bool? cur_var)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.Transparent);
    if (cell.Nodes.Count == 0)
      return cur_var;
    bool? transparent1;
    if (!(transparent1 = this.GetTransparent(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new bool?();
    bool? transparent2;
    if (cur_var.HasValue)
    {
      bool? nullable = transparent1;
      transparent2 = cur_var;
      if (!(nullable.GetValueOrDefault() == transparent2.GetValueOrDefault() & nullable.HasValue == transparent2.HasValue))
      {
        transparent2 = new bool?();
        return transparent2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      transparent2 = transparent1;
      bool? transparent3 = this.GetTransparent(node, transparent1);
      if (!(transparent2.GetValueOrDefault() == transparent3.GetValueOrDefault() & transparent2.HasValue == transparent3.HasValue))
        return new bool?();
    }
    return transparent1;
  }

  private RectangleElement SetTransparent(RectangleElement cell, bool? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetTransparent(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.Transparent = cur_var.Value;
    return cell;
  }

  private bool? GetReadOnly(RectangleElement cell, bool? cur_var)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.ReadOnly);
    if (cell.Nodes.Count == 0)
      return cur_var;
    bool? cur_var1;
    if (!(cur_var1 = this.GetReadOnly(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new bool?();
    bool? nullable1;
    if (cur_var.HasValue)
    {
      bool? nullable2 = cur_var1;
      nullable1 = cur_var;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
      {
        nullable1 = new bool?();
        return nullable1;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      nullable1 = cur_var1;
      bool? nullable3 = this.GetReadOnly(node, cur_var1);
      if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
        return new bool?();
    }
    return cur_var1;
  }

  private RectangleElement SetReadOnly(RectangleElement cell, bool? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetReadOnly(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.ReadOnly = cur_var.Value;
    return cell;
  }

  private bool? GetGeometryChangingBlocked(RectangleElement cell, bool? cur_var)
  {
    if (cell.IsSingleCell)
      return new bool?(cell.GeometryChangingBlocked);
    if (cell.Nodes.Count == 0)
      return cur_var;
    bool? geometryChangingBlocked1;
    if (!(geometryChangingBlocked1 = this.GetGeometryChangingBlocked(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new bool?();
    bool? geometryChangingBlocked2;
    if (cur_var.HasValue)
    {
      bool? nullable = geometryChangingBlocked1;
      geometryChangingBlocked2 = cur_var;
      if (!(nullable.GetValueOrDefault() == geometryChangingBlocked2.GetValueOrDefault() & nullable.HasValue == geometryChangingBlocked2.HasValue))
      {
        geometryChangingBlocked2 = new bool?();
        return geometryChangingBlocked2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      geometryChangingBlocked2 = geometryChangingBlocked1;
      bool? geometryChangingBlocked3 = this.GetGeometryChangingBlocked(node, geometryChangingBlocked1);
      if (!(geometryChangingBlocked2.GetValueOrDefault() == geometryChangingBlocked3.GetValueOrDefault() & geometryChangingBlocked2.HasValue == geometryChangingBlocked3.HasValue))
        return new bool?();
    }
    return geometryChangingBlocked1;
  }

  private RectangleElement SetGeometryChangingBlocked(RectangleElement cell, bool? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        node.GeometryChangingBlocked = cur_var.Value;
        rectangleElement = this.SetGeometryChangingBlocked(node, cur_var);
      }
      return rectangleElement;
    }
    cell.GeometryChangingBlocked = cur_var.Value;
    return cell;
  }

  private Color? GetForeColor(RectangleElement cell, Color? cur_var)
  {
    if (cell.IsSingleCell)
      return new Color?(cell.ForeColor);
    if (cell.Nodes.Count == 0)
      return cur_var;
    Color? foreColor1;
    if (!(foreColor1 = this.GetForeColor(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new Color?();
    Color? foreColor2;
    if (cur_var.HasValue)
    {
      Color? nullable = foreColor1;
      foreColor2 = cur_var;
      if ((nullable.HasValue == foreColor2.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != foreColor2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        foreColor2 = new Color?();
        return foreColor2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      foreColor2 = foreColor1;
      Color? foreColor3 = this.GetForeColor(node, foreColor1);
      if ((foreColor2.HasValue == foreColor3.HasValue ? (foreColor2.HasValue ? (foreColor2.GetValueOrDefault() != foreColor3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        foreColor3 = new Color?();
        return foreColor3;
      }
    }
    return foreColor1;
  }

  private RectangleElement SetForeColorTE(RectangleElement cell, Color? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetForeColorTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.AssignForeColor(cur_var.Value, false);
    return cell;
  }

  private Color? GetBackColor(RectangleElement cell, Color? cur_var)
  {
    if (cell.IsSingleCell)
      return new Color?(cell.BackColor);
    if (cell.Nodes.Count == 0)
      return cur_var;
    Color? backColor1;
    if (!(backColor1 = this.GetBackColor(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
      return new Color?();
    Color? backColor2;
    if (cur_var.HasValue)
    {
      Color? nullable = backColor1;
      backColor2 = cur_var;
      if ((nullable.HasValue == backColor2.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != backColor2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        backColor2 = new Color?();
        return backColor2;
      }
    }
    int index = 1;
    for (int count = cell.Nodes.Count; index < count; ++index)
    {
      RectangleElement node = cell.Nodes[index] as RectangleElement;
      backColor2 = backColor1;
      Color? backColor3 = this.GetBackColor(node, backColor1);
      if ((backColor2.HasValue == backColor3.HasValue ? (backColor2.HasValue ? (backColor2.GetValueOrDefault() != backColor3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        backColor3 = new Color?();
        return backColor3;
      }
    }
    return backColor1;
  }

  private RectangleElement SetBackColorTE(RectangleElement cell, Color? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetBackColorTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    cell.AssignBackColor(cur_var.Value, false);
    return cell;
  }

  /// <summary>Получение имен реальных ячеек виртуальной ячейки</summary>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="cur_var">текущий абзац</param>
  /// <returns>Возвращает false, если все свойства не совпадают</returns>
  private bool GetParagraphFormat(RectangleElement cell, ref ParagraphFormat cur_var)
  {
    bool paragraphFormat1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      paragraphFormat1 = this.GetParagraphFormat(cell.Nodes[0] as RectangleElement, ref cur_var);
      if (!paragraphFormat1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        paragraphFormat1 = this.GetParagraphFormat(cell.Nodes[index] as RectangleElement, ref cur_var);
        if (!paragraphFormat1)
          return false;
      }
    }
    else if (cell is TextData textData)
    {
      ParagraphFormat paragraphFormat2 = textData.ParagraphFormat;
      if (cur_var == null)
      {
        cur_var = paragraphFormat2.Clone();
        return true;
      }
      HorzAlignment? horzAlignment1 = cur_var.HorzAlignment;
      HorzAlignment? horzAlignment2 = paragraphFormat2.HorzAlignment;
      bool flag;
      if (!(horzAlignment1.GetValueOrDefault() == horzAlignment2.GetValueOrDefault() & horzAlignment1.HasValue == horzAlignment2.HasValue))
      {
        cur_var.HorzAlignment = new HorzAlignment?();
        flag = false;
      }
      else
        flag = true;
      VertAlignment? vertAlignment1 = cur_var.VertAlignment;
      VertAlignment? vertAlignment2 = paragraphFormat2.VertAlignment;
      if (!(vertAlignment1.GetValueOrDefault() == vertAlignment2.GetValueOrDefault() & vertAlignment1.HasValue == vertAlignment2.HasValue))
      {
        cur_var.VertAlignment = new VertAlignment?();
        flag = false;
      }
      else
        flag = true;
      bool? disableFloatLines1 = cur_var.DisableFloatLines;
      bool? disableFloatLines2 = paragraphFormat2.DisableFloatLines;
      if (!(disableFloatLines1.GetValueOrDefault() == disableFloatLines2.GetValueOrDefault() & disableFloatLines1.HasValue == disableFloatLines2.HasValue))
      {
        cur_var.DisableFloatLines = new bool?();
        flag = false;
      }
      else
        flag = true;
      float? spaceBetweenLines1 = cur_var.SpaceBetweenLines;
      float? spaceBetweenLines2 = paragraphFormat2.SpaceBetweenLines;
      if (!((double) spaceBetweenLines1.GetValueOrDefault() == (double) spaceBetweenLines2.GetValueOrDefault() & spaceBetweenLines1.HasValue == spaceBetweenLines2.HasValue))
      {
        cur_var.SpaceBetweenLines = new float?();
        flag = false;
      }
      else
        flag = true;
      bool? keepWithNext1 = cur_var.KeepWithNext;
      bool? keepWithNext2 = paragraphFormat2.KeepWithNext;
      if (!(keepWithNext1.GetValueOrDefault() == keepWithNext2.GetValueOrDefault() & keepWithNext1.HasValue == keepWithNext2.HasValue))
      {
        cur_var.KeepWithNext = new bool?();
        flag = false;
      }
      else
        flag = true;
      bool? keepTogether1 = cur_var.KeepTogether;
      bool? keepTogether2 = paragraphFormat2.KeepTogether;
      if (!(keepTogether1.GetValueOrDefault() == keepTogether2.GetValueOrDefault() & keepTogether1.HasValue == keepTogether2.HasValue))
      {
        cur_var.KeepTogether = new bool?();
        flag = false;
      }
      else
        flag = true;
      bool? disableWordWrap1 = cur_var.DisableWordWrap;
      bool? disableWordWrap2 = paragraphFormat2.DisableWordWrap;
      if (!(disableWordWrap1.GetValueOrDefault() == disableWordWrap2.GetValueOrDefault() & disableWordWrap1.HasValue == disableWordWrap2.HasValue))
      {
        cur_var.DisableWordWrap = new bool?();
        flag = false;
      }
      else
        flag = true;
      float? identFirstLine1 = cur_var.IdentFirstLine;
      float? identFirstLine2 = paragraphFormat2.IdentFirstLine;
      if (!((double) identFirstLine1.GetValueOrDefault() == (double) identFirstLine2.GetValueOrDefault() & identFirstLine1.HasValue == identFirstLine2.HasValue))
      {
        cur_var.IdentFirstLine = new float?();
        flag = false;
      }
      else
        flag = true;
      float? identLeft1 = cur_var.IdentLeft;
      float? identLeft2 = paragraphFormat2.IdentLeft;
      if (!((double) identLeft1.GetValueOrDefault() == (double) identLeft2.GetValueOrDefault() & identLeft1.HasValue == identLeft2.HasValue))
      {
        cur_var.IdentLeft = new float?();
        flag = false;
      }
      else
        flag = true;
      float? identRight1 = cur_var.IdentRight;
      float? identRight2 = paragraphFormat2.IdentRight;
      if (!((double) identRight1.GetValueOrDefault() == (double) identRight2.GetValueOrDefault() & identRight1.HasValue == identRight2.HasValue))
      {
        cur_var.IdentRight = new float?();
        flag = false;
      }
      else
        flag = true;
      float? intervalBefore1 = cur_var.IntervalBefore;
      float? intervalBefore2 = paragraphFormat2.IntervalBefore;
      if (!((double) intervalBefore1.GetValueOrDefault() == (double) intervalBefore2.GetValueOrDefault() & intervalBefore1.HasValue == intervalBefore2.HasValue))
      {
        cur_var.IntervalBefore = new float?();
        flag = false;
      }
      else
        flag = true;
      float? intervalAfter1 = cur_var.IntervalAfter;
      float? intervalAfter2 = paragraphFormat2.IntervalAfter;
      if (!((double) intervalAfter1.GetValueOrDefault() == (double) intervalAfter2.GetValueOrDefault() & intervalAfter1.HasValue == intervalAfter2.HasValue))
      {
        cur_var.IntervalAfter = new float?();
        flag = false;
      }
      else
        flag = true;
      bool? fromNewPage1 = cur_var.FromNewPage;
      bool? fromNewPage2 = paragraphFormat2.FromNewPage;
      if (!(fromNewPage1.GetValueOrDefault() == fromNewPage2.GetValueOrDefault() & fromNewPage1.HasValue == fromNewPage2.HasValue))
      {
        cur_var.FromNewPage = new bool?();
        flag = false;
      }
      else
        flag = true;
      LineSpacingMethod? lineSpacingMethod1 = cur_var.LineSpacingMethod;
      LineSpacingMethod? lineSpacingMethod2 = paragraphFormat2.LineSpacingMethod;
      if (!(lineSpacingMethod1.GetValueOrDefault() == lineSpacingMethod2.GetValueOrDefault() & lineSpacingMethod1.HasValue == lineSpacingMethod2.HasValue))
      {
        cur_var.LineSpacingMethod = new LineSpacingMethod?();
        flag = false;
      }
      else
        flag = true;
      int? textLevel1 = cur_var.TextLevel;
      int? textLevel2 = paragraphFormat2.TextLevel;
      bool paragraphFormat3;
      if (!(textLevel1.GetValueOrDefault() == textLevel2.GetValueOrDefault() & textLevel1.HasValue == textLevel2.HasValue))
      {
        cur_var.TextLevel = new int?();
        paragraphFormat3 = false;
      }
      else
        paragraphFormat3 = true;
      return paragraphFormat3;
    }
    return paragraphFormat1;
  }

  private RectangleElement SetParagraphFormat(RectangleElement cell, ParagraphFormat cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetParagraphFormat(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextData textData)
    {
      ParagraphFormat paragraphFormat = textData.ParagraphFormat.Clone();
      if (cur_var.HorzAlignment.HasValue)
        paragraphFormat.HorzAlignment = cur_var.HorzAlignment;
      if (cur_var.VertAlignment.HasValue)
        paragraphFormat.VertAlignment = cur_var.VertAlignment;
      if (cur_var.DisableFloatLines.HasValue)
        paragraphFormat.DisableFloatLines = cur_var.DisableFloatLines;
      if (cur_var.SpaceBetweenLines.HasValue)
        paragraphFormat.SpaceBetweenLines = cur_var.SpaceBetweenLines;
      if (cur_var.KeepWithNext.HasValue)
        paragraphFormat.KeepWithNext = cur_var.KeepWithNext;
      if (cur_var.KeepTogether.HasValue)
        paragraphFormat.KeepTogether = cur_var.KeepTogether;
      if (cur_var.DisableWordWrap.HasValue)
        paragraphFormat.DisableWordWrap = cur_var.DisableWordWrap;
      if (cur_var.IdentFirstLine.HasValue)
        paragraphFormat.IdentFirstLine = cur_var.IdentFirstLine;
      if (cur_var.IdentLeft.HasValue)
        paragraphFormat.IdentLeft = cur_var.IdentLeft;
      if (cur_var.IdentRight.HasValue)
        paragraphFormat.IdentRight = cur_var.IdentRight;
      if (cur_var.IntervalBefore.HasValue)
        paragraphFormat.IntervalBefore = cur_var.IntervalBefore;
      if (cur_var.IntervalAfter.HasValue)
        paragraphFormat.IntervalAfter = cur_var.IntervalAfter;
      if (cur_var.FromNewPage.HasValue)
        paragraphFormat.FromNewPage = cur_var.FromNewPage;
      if (cur_var.LineSpacingMethod.HasValue)
        paragraphFormat.LineSpacingMethod = cur_var.LineSpacingMethod;
      if (cur_var.TextLevel.HasValue)
        paragraphFormat.TextLevel = cur_var.TextLevel;
      textData.SetParagraphFormat(paragraphFormat.Clone(), false, false);
    }
    return cell;
  }

  private TextOrientation? GetOrientation(RectangleElement cell, TextOrientation? cur_var)
  {
    TextOrientation? cur_var1 = new TextOrientation?();
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if (!(cur_var1 = this.GetOrientation(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
        return new TextOrientation?();
      TextOrientation? orientation1;
      TextOrientation? orientation2;
      if (cur_var.HasValue)
      {
        orientation1 = cur_var1;
        orientation2 = cur_var;
        if (!(orientation1.GetValueOrDefault() == orientation2.GetValueOrDefault() & orientation1.HasValue == orientation2.HasValue))
        {
          orientation2 = new TextOrientation?();
          return orientation2;
        }
      }
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        orientation2 = cur_var1;
        orientation1 = this.GetOrientation(node, cur_var1);
        if (!(orientation2.GetValueOrDefault() == orientation1.GetValueOrDefault() & orientation2.HasValue == orientation1.HasValue))
        {
          orientation1 = new TextOrientation?();
          return orientation1;
        }
      }
    }
    else if (cell is LabelElement labelElement)
      return new TextOrientation?(labelElement.Orientation);
    return cur_var1;
  }

  private RectangleElement SetOrientationTE(RectangleElement cell, TextOrientation? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetOrientationTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is LabelElement labelElement)
      labelElement.SetOrientation(cur_var.Value, false, false);
    return cell;
  }

  /// <summary>смотри ParagraphFormatTE</summary>
  private bool GetCharFormat(RectangleElement cell, ref CharFormat cur_var)
  {
    bool charFormat1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      charFormat1 = this.GetCharFormat(cell.Nodes[0] as RectangleElement, ref cur_var);
      if (!charFormat1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        charFormat1 = this.GetCharFormat(cell.Nodes[index] as RectangleElement, ref cur_var);
        if (!charFormat1)
          return false;
      }
    }
    else if (cell is TextData textData)
    {
      CharFormat charFormat2 = textData.CharFormat;
      if (cur_var == null)
      {
        cur_var = charFormat2.Clone();
        return true;
      }
      StrikeoutLineStyle? strike1 = cur_var.Strike;
      StrikeoutLineStyle? strike2 = charFormat2.Strike;
      bool flag;
      if (!(strike1.GetValueOrDefault() == strike2.GetValueOrDefault() & strike1.HasValue == strike2.HasValue))
      {
        cur_var.Strike = new StrikeoutLineStyle?();
        flag = false;
      }
      else
        flag = true;
      if (cur_var.FontFamily != charFormat2.FontFamily)
      {
        cur_var.FontFamily = (string) null;
        flag = false;
      }
      else
        flag = true;
      UnderlineStyle? underline1 = cur_var.Underline;
      UnderlineStyle? underline2 = charFormat2.Underline;
      if (!(underline1.GetValueOrDefault() == underline2.GetValueOrDefault() & underline1.HasValue == underline2.HasValue))
      {
        cur_var.Underline = new UnderlineStyle?();
        flag = false;
      }
      else
        flag = true;
      float? fontSize1 = cur_var.FontSize;
      float? fontSize2 = charFormat2.FontSize;
      if (!((double) fontSize1.GetValueOrDefault() == (double) fontSize2.GetValueOrDefault() & fontSize1.HasValue == fontSize2.HasValue))
      {
        cur_var.FontSize = new float?();
        flag = false;
      }
      else
        flag = true;
      float? fontSizeMm1 = cur_var.FontSizeMm;
      float? fontSizeMm2 = charFormat2.FontSizeMm;
      if (!((double) fontSizeMm1.GetValueOrDefault() == (double) fontSizeMm2.GetValueOrDefault() & fontSizeMm1.HasValue == fontSizeMm2.HasValue))
      {
        cur_var.FontSizeMm = new float?();
        flag = false;
      }
      else
        flag = true;
      BoldItalicStyle? boldItalic1 = cur_var.BoldItalic;
      BoldItalicStyle? boldItalic2 = charFormat2.BoldItalic;
      if (!(boldItalic1.GetValueOrDefault() == boldItalic2.GetValueOrDefault() & boldItalic1.HasValue == boldItalic2.HasValue))
      {
        cur_var.BoldItalic = new BoldItalicStyle?();
        flag = false;
      }
      else
        flag = true;
      Color? textColorForUser1 = cur_var.TextColorForUser;
      Color? textColorForUser2 = charFormat2.TextColorForUser;
      if ((textColorForUser1.HasValue == textColorForUser2.HasValue ? (textColorForUser1.HasValue ? (textColorForUser1.GetValueOrDefault() != textColorForUser2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        cur_var.TextColorForUser = new Color?();
        flag = false;
      }
      else
        flag = true;
      Color? textBkColorForUser1 = cur_var.TextBkColorForUser;
      Color? textBkColorForUser2 = charFormat2.TextBkColorForUser;
      if ((textBkColorForUser1.HasValue == textBkColorForUser2.HasValue ? (textBkColorForUser1.HasValue ? (textBkColorForUser1.GetValueOrDefault() != textBkColorForUser2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        cur_var.TextBkColorForUser = new Color?();
        flag = false;
      }
      else
        flag = true;
      Color? underlineColor1 = cur_var.UnderlineColor;
      Color? underlineColor2 = charFormat2.UnderlineColor;
      bool charFormat3;
      if ((underlineColor1.HasValue == underlineColor2.HasValue ? (underlineColor1.HasValue ? (underlineColor1.GetValueOrDefault() != underlineColor2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        cur_var.UnderlineColor = new Color?();
        charFormat3 = false;
      }
      else
        charFormat3 = true;
      return charFormat3;
    }
    return charFormat1;
  }

  /// <summary>смотри ParagraphFormatTE</summary>
  private RectangleElement SetCharFormat(RectangleElement cell, CharFormat cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetCharFormat(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextData textData)
    {
      CharFormat charFormat = textData.CharFormat.Clone();
      if (cur_var.Strike.HasValue)
        charFormat.Strike = cur_var.Strike;
      if (cur_var.FontFamily != null)
        charFormat.FontFamily = cur_var.FontFamily;
      if (cur_var.Underline.HasValue)
        charFormat.Underline = cur_var.Underline;
      if (cur_var.FontSize.HasValue)
        charFormat.FontSize = cur_var.FontSize;
      if (cur_var.FontSizeMm.HasValue)
        charFormat.FontSizeMm = cur_var.FontSizeMm;
      if (cur_var.BoldItalic.HasValue)
        charFormat.BoldItalic = cur_var.BoldItalic;
      if (cur_var.TextColorForUser.HasValue)
        charFormat.TextColorForUser = cur_var.TextColorForUser;
      if (cur_var.TextBkColorForUser.HasValue)
        charFormat.TextBkColorForUser = cur_var.TextBkColorForUser;
      if (cur_var.UnderlineColor.HasValue)
        charFormat.UnderlineColor = cur_var.UnderlineColor;
      textData.SetCharFormat(charFormat.Clone(), false, false);
    }
    return cell;
  }

  private SizeF? GetOriginalSize(RectangleElement cell, SizeF? cur_var)
  {
    SizeF? cur_var1 = new SizeF?();
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if (!(cur_var1 = this.GetOriginalSize(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
        return new SizeF?();
      SizeF? originalSize;
      if (cur_var.HasValue)
      {
        originalSize = cur_var1;
        SizeF? nullable = cur_var;
        if ((originalSize.HasValue == nullable.HasValue ? (originalSize.HasValue ? (originalSize.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          return new SizeF?();
      }
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        SizeF? nullable = cur_var1;
        originalSize = this.GetOriginalSize(node, cur_var1);
        if ((nullable.HasValue == originalSize.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != originalSize.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          originalSize = new SizeF?();
          return originalSize;
        }
      }
    }
    else if (cell is ContainerData containerData)
      return new SizeF?(containerData.OriginalSize);
    return cur_var1;
  }

  private ImageScaleMode? GetScaleMode(RectangleElement cell, ImageScaleMode? cur_var)
  {
    ImageScaleMode? cur_var1 = new ImageScaleMode?();
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if (!(cur_var1 = this.GetScaleMode(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
        return new ImageScaleMode?();
      ImageScaleMode? scaleMode1;
      ImageScaleMode? scaleMode2;
      if (cur_var.HasValue)
      {
        scaleMode1 = cur_var1;
        scaleMode2 = cur_var;
        if (!(scaleMode1.GetValueOrDefault() == scaleMode2.GetValueOrDefault() & scaleMode1.HasValue == scaleMode2.HasValue))
        {
          scaleMode2 = new ImageScaleMode?();
          return scaleMode2;
        }
      }
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        scaleMode2 = cur_var1;
        scaleMode1 = this.GetScaleMode(node, cur_var1);
        if (!(scaleMode2.GetValueOrDefault() == scaleMode1.GetValueOrDefault() & scaleMode2.HasValue == scaleMode1.HasValue))
        {
          scaleMode1 = new ImageScaleMode?();
          return scaleMode1;
        }
      }
    }
    else if (cell is ContainerData containerData)
      return new ImageScaleMode?(containerData.ScaleMode);
    return cur_var1;
  }

  private RectangleElement SetScaleMode(RectangleElement cell, ImageScaleMode? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetScaleMode(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is ContainerData containerData)
      containerData.AssignScaleMode(cur_var.Value, false, false, true);
    return cell;
  }

  private bool? GetAutoSizeHeight(RectangleElement cell, bool? cur_var)
  {
    bool? cur_var1 = new bool?();
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if (!(cur_var1 = this.GetAutoSizeHeight(cell.Nodes[0] as RectangleElement, cur_var)).HasValue)
        return new bool?();
      bool? autoSizeHeight1;
      bool? autoSizeHeight2;
      if (cur_var.HasValue)
      {
        autoSizeHeight1 = cur_var1;
        autoSizeHeight2 = cur_var;
        if (!(autoSizeHeight1.GetValueOrDefault() == autoSizeHeight2.GetValueOrDefault() & autoSizeHeight1.HasValue == autoSizeHeight2.HasValue))
        {
          autoSizeHeight2 = new bool?();
          return autoSizeHeight2;
        }
      }
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        autoSizeHeight2 = cur_var1;
        autoSizeHeight1 = this.GetAutoSizeHeight(node, cur_var1);
        if (!(autoSizeHeight2.GetValueOrDefault() == autoSizeHeight1.GetValueOrDefault() & autoSizeHeight2.HasValue == autoSizeHeight1.HasValue))
        {
          autoSizeHeight1 = new bool?();
          return autoSizeHeight1;
        }
      }
    }
    else if (cell is TextBoxElement textBoxElement)
      return new bool?(textBoxElement.AutoSizeHeight);
    return cur_var1;
  }

  private RectangleElement SetAutoSizeHeight(RectangleElement cell, bool? cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetAutoSizeHeight(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextBoxElement textBoxElement)
      textBoxElement.AssignAutoSizeHeight(cur_var.Value, false, false, true);
    return cell;
  }

  private string GetText(RectangleElement cell, string cur_var)
  {
    string cur_var1 = (string) null;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if ((cur_var1 = this.GetText(cell.Nodes[0] as RectangleElement, cur_var)) == null)
        return (string) null;
      if (cur_var != null && cur_var1 != cur_var)
        return (string) null;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        if (cur_var1 != this.GetText(node, cur_var1))
          return (string) null;
      }
    }
    else if (cell is TextData textData)
      return textData.Text;
    return cur_var1;
  }

  private RectangleElement SetText(RectangleElement cell, string cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetText(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextData textData)
      textData.AssignText(cur_var, false, true, true, false, false);
    return cell;
  }

  private Image GetImage(RectangleElement cell, Image cur_var)
  {
    Image cur_var1 = (Image) null;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if ((cur_var1 = this.GetImage(cell.Nodes[0] as RectangleElement, cur_var)) == null)
        return (Image) null;
      if (cur_var != null && cur_var1 != cur_var)
        return (Image) null;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        if (cur_var1 != this.GetImage(node, cur_var1))
          return (Image) null;
      }
    }
    else if (cell is ContainerData containerData)
      return containerData.Image == null ? (Image) null : (Image) containerData.Image.Clone();
    return cur_var1;
  }

  private RectangleElement SetImageTE(RectangleElement cell, Image cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetImageTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is ContainerData containerData)
      containerData.SetImage(cur_var, true, false);
    return cell;
  }

  private string GetTextFormat(RectangleElement cell, string cur_var)
  {
    string cur_var1 = (string) null;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if ((cur_var1 = this.GetTextFormat(cell.Nodes[0] as RectangleElement, cur_var)) == null)
        return (string) null;
      if (cur_var != null && cur_var1 != cur_var)
        return (string) null;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        if (cur_var1 != this.GetTextFormat(node, cur_var1))
          return (string) null;
      }
    }
    else if (cell is TextData textData)
      return textData.TextFormat;
    return cur_var1;
  }

  private RectangleElement SetTextFormatTE(RectangleElement cell, string cur_var)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
        rectangleElement = this.SetTextFormatTE(cell.Nodes[index] as RectangleElement, cur_var);
      return rectangleElement;
    }
    if (cell is TextData textData)
      textData.AssignTextFormat(cur_var, false);
    return cell;
  }

  private string GetFormattedText(RectangleElement cell, string cur_var)
  {
    string cur_var1 = (string) null;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return cur_var;
      if ((cur_var1 = this.GetFormattedText(cell.Nodes[0] as RectangleElement, cur_var)) == null)
        return (string) null;
      if (cur_var != null && cur_var1 != cur_var)
        return (string) null;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        if (cur_var1 != this.GetFormattedText(node, cur_var1))
          return (string) null;
      }
    }
    else if (cell is LabelElement labelElement)
      return labelElement.FormattedText;
    return cur_var1;
  }

  /// <summary>Получить значение TopBorderLine дочерних ячеек</summary>
  /// <param name="cur_var">текущее значение</param>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="hastop">имеет ли текущая ячейка граничащуюю с ней сверху</param>
  /// <returns>Возвращает false если все свойства не совпадают</returns>
  private bool GetTopBorderLineTE(RectangleElement cell, ref BorderLineTE cur_var, bool hastop)
  {
    bool hastop1 = hastop;
    bool topBorderLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      topBorderLineTe1 = this.GetTopBorderLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hastop1);
      if (!topBorderLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hastop2 = hastop;
        if (!flag)
          hastop2 = true;
        topBorderLineTe1 = this.GetTopBorderLineTE(node, ref cur_var, hastop2);
        if (!topBorderLineTe1)
          return false;
      }
    }
    else if (!hastop)
    {
      BorderLine topBorderLine = cell.TopBorderLine;
      if (cur_var == null)
      {
        cur_var = new BorderLineTE(topBorderLine);
        return true;
      }
      Color? nullable1 = cur_var.ColorTE;
      Color color = topBorderLine.Color;
      bool flag;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
      {
        BorderLineTE borderLineTe = cur_var;
        nullable1 = new Color?();
        Color? nullable2 = nullable1;
        borderLineTe.ColorTE = nullable2;
        flag = false;
      }
      else
        flag = true;
      BorderStyles? styleTe = cur_var.StyleTE;
      BorderStyles style = topBorderLine.Style;
      if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
      {
        cur_var.StyleTE = new BorderStyles?();
        flag = false;
      }
      else
        flag = true;
      float? widthTe = cur_var.WidthTE;
      float width = topBorderLine.Width;
      if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
      {
        cur_var.WidthTE = new float?();
        flag = false;
      }
      else
        flag = true;
      float? serifWidthTe = cur_var.SerifWidthTE;
      float serifWidth = topBorderLine.SerifWidth;
      bool topBorderLineTe2;
      if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
      {
        cur_var.SerifWidthTE = new float?();
        topBorderLineTe2 = false;
      }
      else
        topBorderLineTe2 = true;
      return topBorderLineTe2;
    }
    return topBorderLineTe1;
  }

  /// <summary>Получить значение TopBorderLine дочерних ячеек</summary>
  /// <param name="cur_var">текущее значение</param>
  /// <param name="cell">текущая ячейка</param>
  /// <param name="hastop">имеет ли текущая ячейка граничащуюю с ней сверху</param>
  /// <returns>Возвращает последнюю ячейку</returns>
  private RectangleElement SetTopBorderLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hastop)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hastop1 = hastop;
        if (!flag && index != 0)
          hastop1 = true;
        rectangleElement = this.SetTopBorderLineTE(cell.Nodes[index] as RectangleElement, cur_var, hastop1);
      }
    }
    else if (!hastop)
    {
      BorderLine borderLine1 = cell.TopBorderLine.Clone();
      if (cur_var.ColorTE.HasValue)
        borderLine1.Color = cur_var.ColorTE.Value;
      BorderStyles? styleTe = cur_var.StyleTE;
      if (styleTe.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine2.Style = (BorderStyles) num;
      }
      float? nullable = cur_var.WidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine3.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine4.SerifWidth = (float) num;
      }
      cell.AssignTopBorderLine(borderLine1, false);
      return cell;
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetBottomBorderLineTE(
    RectangleElement cell,
    ref BorderLineTE cur_var,
    bool hasbottom)
  {
    bool hasbottom1 = hasbottom;
    bool bottomBorderLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      if (!flag && cell.Nodes.Count > 1)
        hasbottom1 = true;
      bottomBorderLineTe1 = this.GetBottomBorderLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hasbottom1);
      if (!bottomBorderLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hasbottom2 = hasbottom;
        if (!flag && index != count - 1)
          hasbottom2 = true;
        bottomBorderLineTe1 = this.GetBottomBorderLineTE(node, ref cur_var, hasbottom2);
        if (!bottomBorderLineTe1)
          return false;
      }
    }
    else if (!hasbottom)
    {
      BorderLine bottomBorderLine = cell.BottomBorderLine;
      if (cur_var == null)
      {
        cur_var = new BorderLineTE(bottomBorderLine);
        return true;
      }
      Color? nullable1 = cur_var.ColorTE;
      Color color = bottomBorderLine.Color;
      bool flag;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
      {
        BorderLineTE borderLineTe = cur_var;
        nullable1 = new Color?();
        Color? nullable2 = nullable1;
        borderLineTe.ColorTE = nullable2;
        flag = false;
      }
      else
        flag = true;
      BorderStyles? styleTe = cur_var.StyleTE;
      BorderStyles style = bottomBorderLine.Style;
      if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
      {
        cur_var.StyleTE = new BorderStyles?();
        flag = false;
      }
      else
        flag = true;
      float? widthTe = cur_var.WidthTE;
      float width = bottomBorderLine.Width;
      if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
      {
        cur_var.WidthTE = new float?();
        flag = false;
      }
      else
        flag = true;
      float? serifWidthTe = cur_var.SerifWidthTE;
      float serifWidth = bottomBorderLine.SerifWidth;
      bool bottomBorderLineTe2;
      if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
      {
        cur_var.SerifWidthTE = new float?();
        bottomBorderLineTe2 = false;
      }
      else
        bottomBorderLineTe2 = true;
      return bottomBorderLineTe2;
    }
    return bottomBorderLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetBottomBorderLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hasbottom)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hasbottom1 = hasbottom;
        if (!flag && index != count - 1)
          hasbottom1 = true;
        rectangleElement = this.SetBottomBorderLineTE(cell.Nodes[index] as RectangleElement, cur_var, hasbottom1);
      }
    }
    else if (!hasbottom)
    {
      BorderLine borderLine1 = cell.BottomBorderLine.Clone();
      if (cur_var.ColorTE.HasValue)
        borderLine1.Color = cur_var.ColorTE.Value;
      BorderStyles? styleTe = cur_var.StyleTE;
      if (styleTe.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine2.Style = (BorderStyles) num;
      }
      float? nullable = cur_var.WidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine3.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine4.SerifWidth = (float) num;
      }
      cell.AssignBottomBorderLine(borderLine1, false);
      return cell;
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetLeftBorderLineTE(RectangleElement cell, ref BorderLineTE cur_var, bool hasleft)
  {
    bool hasleft1 = hasleft;
    bool leftBorderLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      leftBorderLineTe1 = this.GetLeftBorderLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hasleft1);
      if (!leftBorderLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hasleft2 = hasleft;
        if (flag)
          hasleft2 = true;
        leftBorderLineTe1 = this.GetLeftBorderLineTE(node, ref cur_var, hasleft2);
        if (!leftBorderLineTe1)
          return false;
      }
    }
    else if (!hasleft)
    {
      BorderLine leftBorderLine = cell.LeftBorderLine;
      if (cur_var == null)
      {
        cur_var = new BorderLineTE(leftBorderLine);
        return true;
      }
      Color? nullable1 = cur_var.ColorTE;
      Color color = leftBorderLine.Color;
      bool flag;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
      {
        BorderLineTE borderLineTe = cur_var;
        nullable1 = new Color?();
        Color? nullable2 = nullable1;
        borderLineTe.ColorTE = nullable2;
        flag = false;
      }
      else
        flag = true;
      BorderStyles? styleTe = cur_var.StyleTE;
      BorderStyles style = leftBorderLine.Style;
      if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
      {
        cur_var.StyleTE = new BorderStyles?();
        flag = false;
      }
      else
        flag = true;
      float? widthTe = cur_var.WidthTE;
      float width = leftBorderLine.Width;
      if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
      {
        cur_var.WidthTE = new float?();
        flag = false;
      }
      else
        flag = true;
      float? serifWidthTe = cur_var.SerifWidthTE;
      float serifWidth = leftBorderLine.SerifWidth;
      bool leftBorderLineTe2;
      if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
      {
        cur_var.SerifWidthTE = new float?();
        leftBorderLineTe2 = false;
      }
      else
        leftBorderLineTe2 = true;
      return leftBorderLineTe2;
    }
    return leftBorderLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetLeftBorderLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hasleft)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hasleft1 = hasleft;
        if (flag && index != 0)
          hasleft1 = true;
        rectangleElement = this.SetLeftBorderLineTE(cell.Nodes[index] as RectangleElement, cur_var, hasleft1);
      }
    }
    else if (!hasleft)
    {
      BorderLine borderLine1 = cell.LeftBorderLine.Clone();
      if (cur_var.ColorTE.HasValue)
        borderLine1.Color = cur_var.ColorTE.Value;
      BorderStyles? styleTe = cur_var.StyleTE;
      if (styleTe.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine2.Style = (BorderStyles) num;
      }
      float? nullable = cur_var.WidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine3.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine4.SerifWidth = (float) num;
      }
      cell.AssignLeftBorderLine(borderLine1, false);
      return cell;
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetRightBorderLineTE(RectangleElement cell, ref BorderLineTE cur_var, bool hasright)
  {
    bool hasright1 = hasright;
    bool rightBorderLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      if (flag && cell.Nodes.Count > 1)
        hasright1 = true;
      rightBorderLineTe1 = this.GetRightBorderLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hasright1);
      if (!rightBorderLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hasright2 = hasright;
        if (flag && index != count - 1)
          hasright2 = true;
        rightBorderLineTe1 = this.GetRightBorderLineTE(node, ref cur_var, hasright2);
        if (!rightBorderLineTe1)
          return false;
      }
    }
    else if (!hasright)
    {
      BorderLine rightBorderLine = cell.RightBorderLine;
      if (cur_var == null)
      {
        cur_var = new BorderLineTE(rightBorderLine);
        return true;
      }
      Color? nullable1 = cur_var.ColorTE;
      Color color = rightBorderLine.Color;
      bool flag;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
      {
        BorderLineTE borderLineTe = cur_var;
        nullable1 = new Color?();
        Color? nullable2 = nullable1;
        borderLineTe.ColorTE = nullable2;
        flag = false;
      }
      else
        flag = true;
      BorderStyles? styleTe = cur_var.StyleTE;
      BorderStyles style = rightBorderLine.Style;
      if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
      {
        cur_var.StyleTE = new BorderStyles?();
        flag = false;
      }
      else
        flag = true;
      float? widthTe = cur_var.WidthTE;
      float width = rightBorderLine.Width;
      if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
      {
        cur_var.WidthTE = new float?();
        flag = false;
      }
      else
        flag = true;
      float? serifWidthTe = cur_var.SerifWidthTE;
      float serifWidth = rightBorderLine.SerifWidth;
      bool rightBorderLineTe2;
      if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
      {
        cur_var.SerifWidthTE = new float?();
        rightBorderLineTe2 = false;
      }
      else
        rightBorderLineTe2 = true;
      return rightBorderLineTe2;
    }
    return rightBorderLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetRightBorderLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hasright)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hasright1 = hasright;
        if (flag && index != count - 1)
          hasright1 = true;
        rectangleElement = this.SetRightBorderLineTE(cell.Nodes[index] as RectangleElement, cur_var, hasright1);
      }
    }
    else if (!hasright)
    {
      BorderLine borderLine1 = cell.RightBorderLine.Clone();
      if (cur_var.ColorTE.HasValue)
        borderLine1.Color = cur_var.ColorTE.Value;
      BorderStyles? styleTe = cur_var.StyleTE;
      if (styleTe.HasValue)
      {
        BorderLine borderLine2 = borderLine1;
        styleTe = cur_var.StyleTE;
        int num = (int) styleTe.Value;
        borderLine2.Style = (BorderStyles) num;
      }
      float? nullable = cur_var.WidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine3 = borderLine1;
        nullable = cur_var.WidthTE;
        double num = (double) nullable.Value;
        borderLine3.Width = (float) num;
      }
      nullable = cur_var.SerifWidthTE;
      if (nullable.HasValue)
      {
        BorderLine borderLine4 = borderLine1;
        nullable = cur_var.SerifWidthTE;
        double num = (double) nullable.Value;
        borderLine4.SerifWidth = (float) num;
      }
      cell.AssignRightBorderLine(borderLine1, false);
      return cell;
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetInnerHorizontalLineTE(
    RectangleElement cell,
    ref BorderLineTE cur_var,
    bool hastop,
    bool hasbottom)
  {
    bool hastop1 = hastop;
    bool hasbottom1 = hasbottom;
    bool horizontalLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      if (!flag && cell.Nodes.Count > 1)
        hasbottom1 = true;
      horizontalLineTe1 = this.GetInnerHorizontalLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hastop1, hasbottom1);
      if (!horizontalLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hastop2 = hastop;
        bool hasbottom2 = hasbottom;
        if (!flag)
        {
          hastop2 = true;
          if (index != count - 1)
            hasbottom2 = true;
        }
        horizontalLineTe1 = this.GetInnerHorizontalLineTE(node, ref cur_var, hastop2, hasbottom2);
        if (!horizontalLineTe1)
          return false;
      }
    }
    else
    {
      bool flag;
      if (hastop)
      {
        BorderLine topBorderLine = cell.TopBorderLine;
        if (cur_var == null)
        {
          cur_var = new BorderLineTE(topBorderLine);
          return true;
        }
        Color? colorTe = cur_var.ColorTE;
        Color color = topBorderLine.Color;
        if ((colorTe.HasValue ? (colorTe.HasValue ? (colorTe.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
        {
          cur_var.ColorTE = new Color?();
          flag = false;
        }
        else
          flag = true;
        BorderStyles? styleTe = cur_var.StyleTE;
        BorderStyles style = topBorderLine.Style;
        if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
        {
          cur_var.StyleTE = new BorderStyles?();
          flag = false;
        }
        else
          flag = true;
        float? widthTe = cur_var.WidthTE;
        float width = topBorderLine.Width;
        if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
        {
          cur_var.WidthTE = new float?();
          flag = false;
        }
        else
          flag = true;
        float? serifWidthTe = cur_var.SerifWidthTE;
        float serifWidth = topBorderLine.SerifWidth;
        bool horizontalLineTe2;
        if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
        {
          cur_var.SerifWidthTE = new float?();
          horizontalLineTe2 = false;
        }
        else
          horizontalLineTe2 = true;
        return horizontalLineTe2;
      }
      if (hasbottom)
      {
        BorderLine bottomBorderLine = cell.BottomBorderLine;
        if (cur_var == null)
        {
          cur_var = new BorderLineTE(bottomBorderLine);
          return true;
        }
        Color? colorTe = cur_var.ColorTE;
        Color color = bottomBorderLine.Color;
        if ((colorTe.HasValue ? (colorTe.HasValue ? (colorTe.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
        {
          cur_var.ColorTE = new Color?();
          flag = false;
        }
        else
          flag = true;
        BorderStyles? styleTe = cur_var.StyleTE;
        BorderStyles style = bottomBorderLine.Style;
        if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
        {
          cur_var.StyleTE = new BorderStyles?();
          flag = false;
        }
        else
          flag = true;
        float? widthTe = cur_var.WidthTE;
        float width = bottomBorderLine.Width;
        if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
        {
          cur_var.WidthTE = new float?();
          flag = false;
        }
        else
          flag = true;
        float? serifWidthTe = cur_var.SerifWidthTE;
        float serifWidth = bottomBorderLine.SerifWidth;
        bool horizontalLineTe3;
        if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
        {
          cur_var.SerifWidthTE = new float?();
          horizontalLineTe3 = false;
        }
        else
          horizontalLineTe3 = true;
        return horizontalLineTe3;
      }
    }
    return horizontalLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetInnerHorizontalLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hastop,
    bool hasbottom)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hastop1 = hastop;
        bool hasbottom1 = hasbottom;
        if (!flag)
        {
          if (index != 0)
            hastop1 = true;
          if (index != count - 1)
            hasbottom1 = true;
        }
        rectangleElement = this.SetInnerHorizontalLineTE(cell.Nodes[index] as RectangleElement, cur_var, hastop1, hasbottom1);
      }
    }
    else
    {
      if (hastop)
      {
        BorderLine borderLine1 = cell.TopBorderLine.Clone();
        if (cur_var.ColorTE.HasValue)
          borderLine1.Color = cur_var.ColorTE.Value;
        BorderStyles? styleTe = cur_var.StyleTE;
        if (styleTe.HasValue)
        {
          BorderLine borderLine2 = borderLine1;
          styleTe = cur_var.StyleTE;
          int num = (int) styleTe.Value;
          borderLine2.Style = (BorderStyles) num;
        }
        float? nullable = cur_var.WidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine3 = borderLine1;
          nullable = cur_var.WidthTE;
          double num = (double) nullable.Value;
          borderLine3.Width = (float) num;
        }
        nullable = cur_var.SerifWidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine4 = borderLine1;
          nullable = cur_var.SerifWidthTE;
          double num = (double) nullable.Value;
          borderLine4.SerifWidth = (float) num;
        }
        cell.AssignTopBorderLine(borderLine1, false);
        return cell;
      }
      if (hasbottom)
      {
        BorderLine borderLine5 = cell.BottomBorderLine.Clone();
        if (cur_var.ColorTE.HasValue)
          borderLine5.Color = cur_var.ColorTE.Value;
        BorderStyles? styleTe = cur_var.StyleTE;
        if (styleTe.HasValue)
        {
          BorderLine borderLine6 = borderLine5;
          styleTe = cur_var.StyleTE;
          int num = (int) styleTe.Value;
          borderLine6.Style = (BorderStyles) num;
        }
        float? nullable = cur_var.WidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine7 = borderLine5;
          nullable = cur_var.WidthTE;
          double num = (double) nullable.Value;
          borderLine7.Width = (float) num;
        }
        nullable = cur_var.SerifWidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine8 = borderLine5;
          nullable = cur_var.SerifWidthTE;
          double num = (double) nullable.Value;
          borderLine8.SerifWidth = (float) num;
        }
        cell.AssignBottomBorderLine(borderLine5, false);
        return cell;
      }
    }
    return rectangleElement;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private bool GetInnerVerticalLineTE(
    RectangleElement cell,
    ref BorderLineTE cur_var,
    bool hasleft,
    bool hasright)
  {
    bool hasleft1 = hasleft;
    bool hasright1 = hasright;
    bool innerVerticalLineTe1 = true;
    if (!cell.IsSingleCell)
    {
      if (cell.Nodes.Count == 0)
        return true;
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      if (flag && cell.Nodes.Count > 1)
        hasright1 = true;
      innerVerticalLineTe1 = this.GetInnerVerticalLineTE(cell.Nodes[0] as RectangleElement, ref cur_var, hasleft1, hasright1);
      if (!innerVerticalLineTe1)
        return false;
      int index = 1;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        RectangleElement node = cell.Nodes[index] as RectangleElement;
        bool hasleft2 = hasleft;
        if (flag)
        {
          hasleft2 = true;
          hasright1 = hasright;
          if (index != count - 1)
            hasright1 = true;
        }
        innerVerticalLineTe1 = this.GetInnerVerticalLineTE(node, ref cur_var, hasleft2, hasright1);
        if (!innerVerticalLineTe1)
          return false;
      }
    }
    else
    {
      bool flag;
      if (hasleft)
      {
        BorderLine leftBorderLine = cell.LeftBorderLine;
        if (cur_var == null)
        {
          cur_var = new BorderLineTE(leftBorderLine);
          return true;
        }
        Color? colorTe = cur_var.ColorTE;
        Color color = leftBorderLine.Color;
        if ((colorTe.HasValue ? (colorTe.HasValue ? (colorTe.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
        {
          cur_var.ColorTE = new Color?();
          flag = false;
        }
        else
          flag = true;
        BorderStyles? styleTe = cur_var.StyleTE;
        BorderStyles style = leftBorderLine.Style;
        if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
        {
          cur_var.StyleTE = new BorderStyles?();
          flag = false;
        }
        else
          flag = true;
        float? widthTe = cur_var.WidthTE;
        float width = leftBorderLine.Width;
        if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
        {
          cur_var.WidthTE = new float?();
          flag = false;
        }
        else
          flag = true;
        float? serifWidthTe = cur_var.SerifWidthTE;
        float serifWidth = leftBorderLine.SerifWidth;
        bool innerVerticalLineTe2;
        if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
        {
          cur_var.SerifWidthTE = new float?();
          innerVerticalLineTe2 = false;
        }
        else
          innerVerticalLineTe2 = true;
        return innerVerticalLineTe2;
      }
      if (hasright1)
      {
        BorderLine rightBorderLine = cell.RightBorderLine;
        if (cur_var == null)
        {
          cur_var = new BorderLineTE(rightBorderLine);
          return true;
        }
        Color? colorTe = cur_var.ColorTE;
        Color color = rightBorderLine.Color;
        if ((colorTe.HasValue ? (colorTe.HasValue ? (colorTe.GetValueOrDefault() != color ? 1 : 0) : 0) : 1) != 0)
        {
          cur_var.ColorTE = new Color?();
          flag = false;
        }
        else
          flag = true;
        BorderStyles? styleTe = cur_var.StyleTE;
        BorderStyles style = rightBorderLine.Style;
        if (!(styleTe.GetValueOrDefault() == style & styleTe.HasValue))
        {
          cur_var.StyleTE = new BorderStyles?();
          flag = false;
        }
        else
          flag = true;
        float? widthTe = cur_var.WidthTE;
        float width = rightBorderLine.Width;
        if (!((double) widthTe.GetValueOrDefault() == (double) width & widthTe.HasValue))
        {
          cur_var.WidthTE = new float?();
          flag = false;
        }
        else
          flag = true;
        float? serifWidthTe = cur_var.SerifWidthTE;
        float serifWidth = rightBorderLine.SerifWidth;
        bool innerVerticalLineTe3;
        if (!((double) serifWidthTe.GetValueOrDefault() == (double) serifWidth & serifWidthTe.HasValue))
        {
          cur_var.SerifWidthTE = new float?();
          innerVerticalLineTe3 = false;
        }
        else
          innerVerticalLineTe3 = true;
        return innerVerticalLineTe3;
      }
    }
    return innerVerticalLineTe1;
  }

  /// <summary>смотри TopBorderLineTE</summary>
  private RectangleElement SetInnerVerticalLineTE(
    RectangleElement cell,
    BorderLineTE cur_var,
    bool hasleft,
    bool hasright)
  {
    RectangleElement rectangleElement = cell;
    if (!cell.IsSingleCell)
    {
      bool flag = cell is TableElement && (cell as TableElement).IsRow;
      int index = 0;
      for (int count = cell.Nodes.Count; index < count; ++index)
      {
        bool hasleft1 = hasleft;
        bool hasright1 = hasright;
        if (flag)
        {
          if (index != 0)
            hasleft1 = true;
          if (index != count - 1)
            hasright1 = true;
        }
        rectangleElement = this.SetInnerVerticalLineTE(cell.Nodes[index] as RectangleElement, cur_var, hasleft1, hasright1);
      }
    }
    else
    {
      if (hasleft)
      {
        BorderLine borderLine1 = cell.LeftBorderLine.Clone();
        if (cur_var.ColorTE.HasValue)
          borderLine1.Color = cur_var.ColorTE.Value;
        BorderStyles? styleTe = cur_var.StyleTE;
        if (styleTe.HasValue)
        {
          BorderLine borderLine2 = borderLine1;
          styleTe = cur_var.StyleTE;
          int num = (int) styleTe.Value;
          borderLine2.Style = (BorderStyles) num;
        }
        float? nullable = cur_var.WidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine3 = borderLine1;
          nullable = cur_var.WidthTE;
          double num = (double) nullable.Value;
          borderLine3.Width = (float) num;
        }
        nullable = cur_var.SerifWidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine4 = borderLine1;
          nullable = cur_var.SerifWidthTE;
          double num = (double) nullable.Value;
          borderLine4.SerifWidth = (float) num;
        }
        cell.AssignLeftBorderLine(borderLine1, false);
        return cell;
      }
      if (hasright)
      {
        BorderLine borderLine5 = cell.RightBorderLine.Clone();
        if (cur_var.ColorTE.HasValue)
          borderLine5.Color = cur_var.ColorTE.Value;
        BorderStyles? styleTe = cur_var.StyleTE;
        if (styleTe.HasValue)
        {
          BorderLine borderLine6 = borderLine5;
          styleTe = cur_var.StyleTE;
          int num = (int) styleTe.Value;
          borderLine6.Style = (BorderStyles) num;
        }
        float? nullable = cur_var.WidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine7 = borderLine5;
          nullable = cur_var.WidthTE;
          double num = (double) nullable.Value;
          borderLine7.Width = (float) num;
        }
        nullable = cur_var.SerifWidthTE;
        if (nullable.HasValue)
        {
          BorderLine borderLine8 = borderLine5;
          nullable = cur_var.SerifWidthTE;
          double num = (double) nullable.Value;
          borderLine8.SerifWidth = (float) num;
        }
        cell.AssignRightBorderLine(borderLine5, false);
        return cell;
      }
    }
    return rectangleElement;
  }

  [Browsable(false)]
  public override float? WidthForUser
  {
    get
    {
      return this.IsVirtualNode ? this.GetWidthForUser((RectangleElement) this, new float?()) : base.WidthForUser;
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetWidthForUser(value.Value, (RectangleElement) this);
          if (rectangleElement == null)
            return;
          rectangleElement.UpdateLayout(true);
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
          base.WidthForUser = new float?(value.Value);
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  public override float? HeightForUser
  {
    get
    {
      return this.IsVirtualNode ? this.GetHeightForUser((RectangleElement) this, new float?()) : base.HeightForUser;
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetHeightForUser(value.Value, (RectangleElement) this);
          if (rectangleElement == null)
            return;
          rectangleElement.UpdateLayout(true);
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
          base.HeightForUser = new float?(value.Value);
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  public override float? LeftForUser
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.LeftForUser;
      PointF point = new PointF();
      PageData pg = (PageData) null;
      point.X = this.GetLeftForUser((RectangleElement) this, float.MaxValue, ref pg);
      if (pg != null)
        point = pg.ConvertInternalToUser(point);
      return new float?(point.X);
    }
  }

  public override float? RightForUser
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.RightForUser;
      PointF point = new PointF();
      PageData pg = (PageData) null;
      point.X = this.GetRightForUser((RectangleElement) this, float.MinValue, ref pg);
      if (pg != null)
        point = pg.ConvertInternalToUser(point);
      return new float?(point.X);
    }
  }

  public override RectangleF Bounds
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.Bounds;
      PageData pg = (PageData) null;
      float rightForUser = this.GetRightForUser((RectangleElement) this, float.MinValue, ref pg);
      double leftForUser = (double) this.GetLeftForUser((RectangleElement) this, float.MaxValue, ref pg);
      float topForUser = this.GetTopForUser((RectangleElement) this, float.MaxValue, ref pg);
      float bottomForUser = this.GetBottomForUser((RectangleElement) this, float.MinValue, ref pg);
      double top = (double) topForUser;
      double right = (double) rightForUser;
      double bottom = (double) bottomForUser;
      return RectangleF.FromLTRB((float) leftForUser, (float) top, (float) right, (float) bottom);
    }
  }

  public override float? BottomForUser
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.BottomForUser;
      PointF point = new PointF();
      PageData pg = (PageData) null;
      point.Y = this.GetBottomForUser((RectangleElement) this, float.MinValue, ref pg);
      if (pg != null)
        point = pg.ConvertInternalToUser(point);
      return new float?(point.Y);
    }
  }

  public override float? TopForUser
  {
    get
    {
      if (!this.IsVirtualNode)
        return base.TopForUser;
      PointF point = new PointF();
      PageData pg = (PageData) null;
      point.Y = this.GetTopForUser((RectangleElement) this, float.MaxValue, ref pg);
      if (pg != null)
        point = pg.ConvertInternalToUser(point);
      return new float?(point.Y);
    }
  }

  public override string Name
  {
    get => this.IsVirtualNode ? this.GetName((RectangleElement) this, (string) null) : base.Name;
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (value == null)
          return;
        if (this.IsVirtualNode)
          this.SetName((RectangleElement) this, value);
        else
          base.Name = value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get
    {
      return this.IsVirtualNode ? this.GetNodeTypeCaption((RectangleElement) this, (string) null) : (string) null;
    }
  }

  [CustomDisplayName("Attribute.Document.Model_146")]
  [CustomDescription("Attribute.Document.Model_147")]
  [CustomCategory("Attribute.Document.Model_148")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? VisibleTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetVisible((RectangleElement) this, new bool?()) : new bool?(this.Visible);
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetVisible((RectangleElement) this, value);
          if (rectangleElement == null)
            return;
          rectangleElement.UpdateLayout(true);
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
          this.Visible = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override float DefaultRowSize
  {
    get => base.DefaultRowSize;
    set => base.DefaultRowSize = value;
  }

  /// <summary>Высота строки для отрисовки сетки, новых строк и кратной высоты строки</summary>
  [CustomDisplayName("Attribute.Document.Model_149")]
  [CustomDescription("Attribute.Document.Model_150")]
  [CustomCategory("Attribute.Document.Model_151")]
  [TypeConverter(typeof (FloatConverter))]
  public float? DefaultRowSizeTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetDefaultRowSize((RectangleElement) this, new float?()) : new float?(base.DefaultRowSize);
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
          this.SetDefaultRowSize((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
        else
          base.DefaultRowSize = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override bool Transparent
  {
    get => base.Transparent;
    set => base.Transparent = value;
  }

  /// <summary>Прозрачный фон</summary>
  [CustomDisplayName("Attribute.Document.Model_155")]
  [CustomDescription("Attribute.Document.Model_156")]
  [CustomCategory("Attribute.Document.Model_157")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? TransparentTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetTransparent((RectangleElement) this, new bool?()) : new bool?(base.Transparent);
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
          this.SetTransparent((RectangleElement) this, value);
        else
          base.Transparent = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override bool ReadOnly
  {
    get => base.ReadOnly;
    set => base.ReadOnly = value;
  }

  /// <summary>Пользователь не может редактировать данные элемента</summary>
  [CustomDisplayName("Attribute.Document.Model_158")]
  [CustomDescription("Attribute.Document.Model_159")]
  [CustomCategory("Attribute.Document.Model_160")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? ReadOnlyTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetReadOnly((RectangleElement) this, new bool?()) : new bool?(base.ReadOnly);
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
          this.SetReadOnly((RectangleElement) this, value).OnChanged(new Changed_EventArgs());
        else
          base.ReadOnly = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override bool GeometryChangingBlocked
  {
    get => base.GeometryChangingBlocked;
    set => base.GeometryChangingBlocked = value;
  }

  /// <summary>Заблокировать изменение геометрии через интерфейс пользователя</summary>
  [CustomDisplayName("Attribute.Document.Model_161")]
  [CustomDescription("Attribute.Document.Model_162")]
  [CustomCategory("Attribute.Document.Model_163")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? GeometryChangingBlockedTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetGeometryChangingBlocked((RectangleElement) this, new bool?()) : new bool?(base.GeometryChangingBlocked);
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        this.SetGeometryChangingBlocked((RectangleElement) this, value);
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override Color ForeColor
  {
    get => base.ForeColor;
    set => base.ForeColor = value;
  }

  /// <summary>Цвет переднего плана</summary>
  [CustomDisplayName("Attribute.Document.Model_164")]
  [CustomDescription("Attribute.Document.Model_165")]
  [CustomCategory("Attribute.Document.Model_166")]
  [Editor(typeof (ColorEditor), typeof (UITypeEditor))]
  public Color? ForeColorTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetForeColor((RectangleElement) this, new Color?()) : new Color?(base.ForeColor);
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetForeColorTE((RectangleElement) this, value);
          if (rectangleElement == null)
            return;
          rectangleElement.OnChanged(new Changed_EventArgs());
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
          base.ForeColor = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override Color BackColor
  {
    get => base.BackColor;
    set => base.BackColor = value;
  }

  /// <summary>Цвет фона</summary>
  [CustomDisplayName("Attribute.Document.Model_167")]
  [CustomDescription("Attribute.Document.Model_168")]
  [CustomCategory("Attribute.Document.Model_169")]
  [Editor(typeof (ColorEditor), typeof (UITypeEditor))]
  public Color? BackColorTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetBackColor((RectangleElement) this, new Color?()) : new Color?(base.BackColor);
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (this.IsVirtualNode)
        {
          RectangleElement rectangleElement = this.SetBackColorTE((RectangleElement) this, value);
          if (rectangleElement == null)
            return;
          rectangleElement.OnChanged(new Changed_EventArgs());
          rectangleElement.TopLevelTable.RefreshUI();
        }
        else
          base.BackColor = value.Value;
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Форматирование абзаца</summary>
  [CustomDisplayName("Attribute.Document.Model_170")]
  [CustomDescription("Attribute.Document.Model_171")]
  [CustomCategory("Attribute.Document.Model_172")]
  [RefreshProperties(RefreshProperties.All)]
  public ParagraphFormat ParagraphFormat
  {
    get
    {
      if (!this.IsVirtualNode)
        return (ParagraphFormat) null;
      ParagraphFormat cur_var = (ParagraphFormat) null;
      this.GetParagraphFormat((RectangleElement) this, ref cur_var);
      return cur_var;
    }
    set
    {
      if (value == null)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        this.SetParagraphFormat((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Ориентация текста</summary>
  [CustomDisplayName("Attribute.Document.Model_173")]
  [CustomDescription("Attribute.Document.Model_174")]
  [CustomCategory("Attribute.Document.Model_175")]
  [RefreshProperties(RefreshProperties.All)]
  public TextOrientation? Orientation
  {
    get
    {
      return this.IsVirtualNode ? this.GetOrientation((RectangleElement) this, new TextOrientation?()) : new TextOrientation?();
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        RectangleElement rectangleElement = this.SetOrientationTE((RectangleElement) this, value);
        if (rectangleElement == null)
          return;
        rectangleElement.TopLevelTable.RefreshUI();
        rectangleElement.OnChanged(new Changed_EventArgs());
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [CustomDisplayName("Attribute.Document.Model_176")]
  [CustomDescription("Attribute.Document.Model_177")]
  [CustomCategory("Attribute.Document.Model_178")]
  [RefreshProperties(RefreshProperties.All)]
  public CharFormat CharFormat
  {
    get
    {
      if (!this.IsVirtualNode)
        return (CharFormat) null;
      CharFormat cur_var = (CharFormat) null;
      this.GetCharFormat((RectangleElement) this, ref cur_var);
      return cur_var;
    }
    set
    {
      if (value == null || !this.IsVirtualNode)
        return;
      this.SetCharFormat((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
    }
  }

  [CustomDisplayName("Attribute.Document.Model_179")]
  [CustomDescription("Attribute.Document.Model_180")]
  [CustomCategory("Attribute.Document.Model_181")]
  [TypeConverter(typeof (Intermech.Interfaces.Document.SizeFConverter))]
  public SizeF? OriginalSize
  {
    get
    {
      return this.IsVirtualNode ? this.GetOriginalSize((RectangleElement) this, new SizeF?()) : new SizeF?();
    }
  }

  /// <summary>Режим масштабирования</summary>
  [CustomDisplayName("Attribute.Document.Model_182")]
  [CustomDescription("Attribute.Document.Model_183")]
  [CustomCategory("Attribute.Document.Model_184")]
  public ImageScaleMode? ScaleMode
  {
    get
    {
      return this.IsVirtualNode ? this.GetScaleMode((RectangleElement) this, new ImageScaleMode?()) : new ImageScaleMode?();
    }
    set
    {
      if (!value.HasValue)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        RectangleElement rectangleElement = this.SetScaleMode((RectangleElement) this, value);
        if (rectangleElement == null)
          return;
        rectangleElement.UpdateLayout(true);
        rectangleElement.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Document.Model_185")]
  [CustomDescription("Attribute.Document.Model_186")]
  [CustomCategory("Attribute.Document.Model_187")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool? AutoSizeHeightTE
  {
    get
    {
      return this.IsVirtualNode ? this.GetAutoSizeHeight((RectangleElement) this, new bool?()) : new bool?();
    }
    set
    {
      if (!value.HasValue || !this.IsVirtualNode)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        this.SetAutoSizeHeight((RectangleElement) this, value)?.UpdateLayout(true);
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Текст</summary>
  [CustomDisplayName("Attribute.Document.Model_188")]
  [CustomDescription("Attribute.Document.Model_189")]
  [CustomCategory("Attribute.Document.Model_190")]
  [RefreshProperties(RefreshProperties.All)]
  public string Text
  {
    get
    {
      return this.IsVirtualNode ? this.GetText((RectangleElement) this, (string) null) : (string) null;
    }
    set
    {
      if (value == null)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        this.SetText((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Рисунок</summary>
  [CustomDisplayName("Attribute.Document.Model_191")]
  [CustomDescription("Attribute.Document.Model_192")]
  [CustomCategory("Attribute.Document.Model_193")]
  public Image Image
  {
    get => this.IsVirtualNode ? this.GetImage((RectangleElement) this, (Image) null) : (Image) null;
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        RectangleElement rectangleElement = this.SetImageTE((RectangleElement) this, value);
        if (rectangleElement == null)
          return;
        rectangleElement.UpdateLayout(true);
        rectangleElement.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Строка формата вывода текста</summary>
  [RefreshProperties(RefreshProperties.All)]
  [CustomDisplayName("Attribute.Document.Model_194")]
  [CustomDescription("Attribute.Document.Model_195")]
  [CustomCategory("Attribute.Document.Model_196")]
  public string TextFormat
  {
    get
    {
      return this.IsVirtualNode ? this.GetTextFormat((RectangleElement) this, (string) null) : (string) null;
    }
    set
    {
      if (value == null)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        if (!this.IsVirtualNode)
          return;
        this.SetTextFormatTE((RectangleElement) this, value)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Текст отформатированный согласно TextFormat</summary>
  [CustomDisplayName("Attribute.Document.Model_197")]
  [CustomDescription("Attribute.Document.Model_198")]
  [CustomCategory("Attribute.Document.Model_199")]
  public string FormattedText
  {
    get
    {
      return this.IsVirtualNode ? this.GetFormattedText((RectangleElement) this, (string) null) : (string) null;
    }
  }

  [Browsable(false)]
  public override BorderLine TopBorderLine
  {
    get => base.TopBorderLine;
    set => base.TopBorderLine = value;
  }

  /// <summary>Только для PropertyGrid! Линия верхней границы прямоугольника.</summary>
  [CustomDisplayName("Attribute.Document.Model_200")]
  [CustomDescription("Attribute.Document.Model_201")]
  [CustomCategory("Attribute.Document.Model_202")]
  public BorderLineTE TopBorderLineTE
  {
    get
    {
      if (!this.IsVirtualNode)
        return new BorderLineTE(this.TopBorderLine);
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetTopBorderLineTE((RectangleElement) this, ref cur_var, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        this.SetTopBorderLineTE((RectangleElement) this, value, false)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override BorderLine BottomBorderLine
  {
    get => base.BottomBorderLine;
    set => base.BottomBorderLine = value;
  }

  /// <summary>Линия нижней границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Document.Model_203")]
  [CustomDescription("Attribute.Document.Model_204")]
  [CustomCategory("Attribute.Document.Model_205")]
  public BorderLineTE BottomBorderLineTE
  {
    get
    {
      if (!this.IsVirtualNode)
        return new BorderLineTE(this.BottomBorderLine);
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetBottomBorderLineTE((RectangleElement) this, ref cur_var, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        this.SetBottomBorderLineTE((RectangleElement) this, value, false)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override BorderLine LeftBorderLine
  {
    get => base.LeftBorderLine;
    set => base.LeftBorderLine = value;
  }

  /// <summary>Линия левой границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Document.Model_206")]
  [CustomDescription("Attribute.Document.Model_207")]
  [CustomCategory("Attribute.Document.Model_208")]
  public BorderLineTE LeftBorderLineTE
  {
    get
    {
      if (!this.IsVirtualNode)
        return new BorderLineTE(this.LeftBorderLine);
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetLeftBorderLineTE((RectangleElement) this, ref cur_var, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        this.SetLeftBorderLineTE((RectangleElement) this, value, false)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  [Browsable(false)]
  public override BorderLine RightBorderLine
  {
    get => base.RightBorderLine;
    set => base.RightBorderLine = value;
  }

  /// <summary>Линия правой границы прямоугольника</summary>
  [CustomDisplayName("Attribute.Document.Model_209")]
  [CustomDescription("Attribute.Document.Model_210")]
  [CustomCategory("Attribute.Document.Model_211")]
  public BorderLineTE RightBorderLineTE
  {
    get
    {
      if (!this.IsVirtualNode)
        return new BorderLineTE(this.RightBorderLine);
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetRightBorderLineTE((RectangleElement) this, ref cur_var, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo("");
      try
      {
        this.SetRightBorderLineTE((RectangleElement) this, value, false)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Горизонтальные внутренние линии виртуальной таблицы</summary>
  [CustomDisplayName("Attribute.Document.Model_212")]
  [CustomDescription("Attribute.Document.Model_213")]
  [CustomCategory("Attribute.Document.Model_214")]
  public BorderLineTE InnerHorizontalLineTE
  {
    get
    {
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetInnerHorizontalLineTE((RectangleElement) this, ref cur_var, false, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_577"));
      try
      {
        this.SetInnerHorizontalLineTE((RectangleElement) this, value, false, false)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Вертикальные внутренние линии виртуальной таблицы</summary>
  [CustomDisplayName("Attribute.Document.Model_215")]
  [CustomDescription("Attribute.Document.Model_216")]
  [CustomCategory("Attribute.Document.Model_217")]
  public BorderLineTE InnerVerticalLineTE
  {
    get
    {
      BorderLineTE cur_var = (BorderLineTE) null;
      this.GetInnerVerticalLineTE((RectangleElement) this, ref cur_var, false, false);
      return cur_var;
    }
    set
    {
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_578"));
      try
      {
        this.SetInnerVerticalLineTE((RectangleElement) this, value, false, false)?.TopLevelTable.RefreshUI();
      }
      finally
      {
        if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
          this.OwnerDocument.UndoManager.EndCreateMultyUndo();
      }
    }
  }

  /// <summary>Имя строки/столбца</summary>
  [CustomDisplayName("Attribute.Document.Model_218")]
  [CustomDescription("Attribute.Document.Model_219")]
  [CustomCategory("Attribute.Document.Model_220")]
  public string ColumnName
  {
    [DebuggerStepThrough] get => this.columnParams.ColRowName;
    set => this.columnParams.ColRowName = value;
  }

  /// <summary>Размер (высота/ширина) строки/столбца</summary>
  [CustomDisplayName("Attribute.Document.Model_221")]
  [CustomDescription("Attribute.Document.Model_222")]
  [CustomCategory("Attribute.Document.Model_223")]
  [TypeConverter(typeof (FloatConverter))]
  public float ColumnWidth
  {
    [DebuggerStepThrough] get => this.columnParams.Size;
    set
    {
      float size = this.columnParams.Size;
      if ((double) size == (double) value)
        return;
      this.columnParams.AssignSize(value, false, false);
      List<RowColParams> gridColumnsParams = this.columnParams.OwnerTable.GridColumnsParams;
      int index1 = 0;
      for (int count = this.nodes.Count; index1 < count; ++index1)
      {
        if (this.nodes[index1] is RectangleElement node)
        {
          RectangleF bounds = node.Bounds;
          if (!node.WidthOverrided || (double) bounds.Width == (double) size)
          {
            if (gridColumnsParams == null || node.IsDefaultGridPos || node.GridPos.SpanCount == 1)
            {
              bounds.Width = value;
            }
            else
            {
              float num1 = 0.0f;
              int num2 = node.GridPos.SpanCount;
              int gridColIndex = node.GridColIndex;
              if (gridColIndex + num2 > gridColumnsParams.Count)
                num2 = gridColumnsParams.Count - gridColIndex;
              for (int index2 = gridColIndex; index2 < gridColIndex + num2; ++index2)
              {
                if (this.columnParams.Index != index2)
                  num1 += gridColumnsParams[index2].Size;
                else
                  num1 += value;
              }
              bounds.Width = num1;
            }
            node.SetCellSizes(bounds, false, true, false, true);
            node.AssignMinWidth(value, false, false, false);
            node.WidthOverrided = false;
          }
        }
      }
      this.ParentCell?.SetNeedUpdateLayoutFlag(true, true, true, true);
    }
  }

  /// <summary>Стиль левой линии столбца</summary>
  [CustomDisplayName("Attribute.Document.Model_224")]
  [CustomDescription("Attribute.Document.Model_225")]
  [CustomCategory("Attribute.Document.Model_226")]
  [Browsable(false)]
  public BorderLine LeftColumnBorderLine
  {
    [DebuggerStepThrough] get => this.columnParams.BorderLine1;
    set => this.columnParams.BorderLine1 = value;
  }

  /// <summary>Тип правой линии столбца</summary>
  [CustomDisplayName("Attribute.Document.Model_227")]
  [CustomDescription("Attribute.Document.Model_228")]
  [CustomCategory("Attribute.Document.Model_229")]
  [Browsable(false)]
  public BorderLine RightColumnBorderLine
  {
    [DebuggerStepThrough] get => this.columnParams.BorderLine2;
    set => this.columnParams.BorderLine2 = value;
  }

  /// <summary>Индекс столбца в сетке</summary>
  [CustomDisplayName("Attribute.Document.Model_230")]
  [CustomDescription("Attribute.Document.Model_231")]
  [CustomCategory("Attribute.Document.Model_232")]
  [Browsable(false)]
  public int ColumnIndex
  {
    [DebuggerStepThrough] get => this.columnParams.Index;
  }

  /// <summary>Тип строки/столбца</summary>
  [Category("Debug")]
  [System.ComponentModel.ReadOnly(true)]
  [CustomDisplayName("Attribute.Document.Model_236")]
  [CustomDescription("Attribute.Document.Model_237")]
  public CellType CellType
  {
    [DebuggerStepThrough] get => this.columnParams.CellType;
    set => this.columnParams.CellType = value;
  }

  /// <summary>Столбец/строка представляет столбец/строку таблицы данных</summary>
  [Category("Debug")]
  [System.ComponentModel.ReadOnly(true)]
  [Obsolete("DocumentData")]
  public virtual bool IsDataTableView
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Ссылка на столбец в сетке таблицы данных</summary>
  [Category("Debug")]
  [System.ComponentModel.ReadOnly(true)]
  [Obsolete("DocumentData")]
  public virtual int DataColumnID
  {
    [DebuggerStepThrough] get => -1;
    set
    {
    }
  }

  /// <summary>Идентификатор строки/столбца</summary>
  [System.ComponentModel.ReadOnly(true)]
  [CustomDisplayName("Attribute.Document.Model_233")]
  [CustomDescription("Attribute.Document.Model_234")]
  [CustomCategory("Attribute.Document.Model_235")]
  [Browsable(false)]
  public int ID
  {
    [DebuggerStepThrough] get => this.columnParams.ID;
    set => this.columnParams.ID = value;
  }

  /// <summary>Ссылка на шаблон</summary>
  [Category("Debug")]
  [System.ComponentModel.ReadOnly(true)]
  public int TemplateID
  {
    [DebuggerStepThrough] get => this.columnParams.TemplateID;
    set => this.columnParams.TemplateID = value;
  }

  /// <summary>Элемент имеет шаблон</summary>
  [Category("Debug")]
  [System.ComponentModel.ReadOnly(true)]
  public bool HasColumnTemplate
  {
    [DebuggerStepThrough] get => this.columnParams.TemplateID != RowColParams.EmptyIDValue;
  }

  /// <summary>Инициализировать поля объекта</summary>
  protected override void InitFields()
  {
    this.isVirtualNode = true;
    base.InitFields();
    this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре конструкторов</summary>
  public static object EmptyConstructor() => (object) new VirtualColumn();

  /// <summary>Конструктор</summary>
  protected VirtualColumn()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="columnOwner">Владелец столбца</param>
  /// <param name="columnParams">Столбец сетки</param>
  public VirtualColumn(TableElement columnOwner, RowColParams columnParams)
  {
    this.isVirtualNode = true;
    this.columnParams = columnParams ?? new RowColParams((TableData) columnOwner, true, -1, "", 0.0f);
    this.AssignParent((DocumentTreeNode) columnOwner, false, false, false);
  }

  /// <summary>Конструктор</summary>
  /// <param name="columnOwner">Владелец столбца</param>
  /// <param name="columnParams">Столбец сетки</param>
  /// <param name="columnCells">Ячейки столбца</param>
  public VirtualColumn(
    TableElement columnOwner,
    RowColParams columnParams,
    IList<DocumentTreeNode> columnCells)
  {
    if (columnCells == null)
      throw new ArgumentNullException(nameof (columnCells));
    this.isVirtualNode = true;
    this.columnParams = columnParams;
    if (this.columnParams == null)
    {
      RectangleElement rectangleElement = (RectangleElement) null;
      if (columnCells.Count > 0)
        rectangleElement = columnCells[0] as RectangleElement;
      string name = "";
      float size = 0.0f;
      if (rectangleElement != null)
      {
        name = rectangleElement.Name;
        size = (float) ((double) rectangleElement.WidthForUser ?? 0.0);
      }
      this.columnParams = new RowColParams((TableData) columnOwner, true, -1, name, size);
    }
    this.AssignParent((DocumentTreeNode) columnOwner, false, false, false);
    for (int index = 0; index < columnCells.Count; ++index)
      this.AddChildNode(columnCells[index], false, false);
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected VirtualColumn(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.isVirtualNode = true;
  }

  public override ImDocumentData OwnerDocument
  {
    get => this.Parent != null ? this.Parent.OwnerDocument : (ImDocumentData) null;
  }

  /// <summary>Получить подпись элемента по умолчанию</summary>
  public override string GetDefautCaption()
  {
    string columnName = this.ColumnName;
    if ((columnName == null || columnName == "") && this.columnParams.OwnerTable != null && this.columnParams.OwnerTable.GridColumnsParams != null)
    {
      int num = this.columnParams.OwnerTable.GridColumnsParams.IndexOf(this.columnParams);
      if (num != -1)
        columnName = num.ToString();
    }
    return $"{LocalizationHolder.rm.GetString("Document.Model_517")}{columnName}\"";
  }

  private void RemoveCellsProperties(RectangleElement cell, ArrayList curarray)
  {
    int num = 10;
    if (curarray.Count == num)
      return;
    if (!cell.IsSingleCell)
    {
      this.RemoveCellsProperties(cell.Nodes[0] as RectangleElement, curarray);
      int index = 1;
      for (int count = cell.Nodes.Count; index < count && curarray.Count != num; ++index)
        this.RemoveCellsProperties(cell.Nodes[index] as RectangleElement, curarray);
    }
    else
    {
      if (cell is LabelElement)
      {
        if (curarray.IndexOf((object) "OriginalSize") == -1)
          curarray.Add((object) "OriginalSize");
        if (curarray.IndexOf((object) "ScaleMode") == -1)
          curarray.Add((object) "ScaleMode");
        if (curarray.IndexOf((object) "AutoSizeHeight") == -1)
          curarray.Add((object) "AutoSizeHeight");
        if (curarray.IndexOf((object) "Image") == -1)
          curarray.Add((object) "Image");
      }
      if (cell is TextBoxElement)
      {
        if (curarray.IndexOf((object) "OriginalSize") == -1)
          curarray.Add((object) "OriginalSize");
        if (curarray.IndexOf((object) "ScaleMode") == -1)
          curarray.Add((object) "ScaleMode");
        if (curarray.IndexOf((object) "Orientation") == -1)
          curarray.Add((object) "Orientation");
        if (curarray.IndexOf((object) "FormattedText") == -1)
          curarray.Add((object) "FormattedText");
        if (curarray.IndexOf((object) "Image") == -1)
          curarray.Add((object) "Image");
      }
      if (!(cell is ContainerElement))
        return;
      if (curarray.IndexOf((object) "ParagraphFormat") == -1)
        curarray.Add((object) "ParagraphFormat");
      if (curarray.IndexOf((object) "Orientation") == -1)
        curarray.Add((object) "Orientation");
      if (curarray.IndexOf((object) "CharFormat") == -1)
        curarray.Add((object) "CharFormat");
      if (curarray.IndexOf((object) "AutoSizeHeight") == -1)
        curarray.Add((object) "AutoSizeHeight");
      if (curarray.IndexOf((object) "Text") == -1)
        curarray.Add((object) "Text");
      if (curarray.IndexOf((object) "TextFormat") == -1)
        curarray.Add((object) "TextFormat");
      if (curarray.IndexOf((object) "FormattedText") != -1)
        return;
      curarray.Add((object) "FormattedText");
    }
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    ArrayList curarray = new ArrayList();
    this.RemoveCellsProperties((RectangleElement) this, curarray);
    int index = 0;
    for (int count = curarray.Count; index < count; ++index)
      this.RemoveProperty(properties, (string) curarray[index]);
    base.FilterProperties(properties, attributes);
    this.RemoveProperty(properties, "TableCellType");
    if (!(this.OwnerDocument is ImDocument ownerDocument) || ownerDocument.DocumentControl == null || !ownerDocument.DocumentControl.ReadOnly)
      return;
    CustomPropertyDescriptor.SetReadOnlyProperties(properties);
  }

  /// <summary>Заглушка. Корень дерева в котором должен находиться шаблон этого узла</summary>
  public override DocumentTreeNode TemplateRoot => (DocumentTreeNode) null;

  /// <summary>Заглушка. Узел является шаблоном</summary>
  public override bool IsTemplate => false;

  /// <summary>Заглушка. Найти шаблон этого узла по идентификатору templateId</summary>
  /// <returns>Шаблон узла</returns>
  public override DocumentTreeNode FindTemplate(string templateId) => (DocumentTreeNode) null;

  /// <summary>Заглушка. Преобразовать в ячейку-шапку рекурсивно. Удаляет ячейки данных</summary>
  /// <param name="removeData">Удалить данные</param>
  public override void ConvertToHeader(bool removeData)
  {
  }

  /// <summary>Проверить можно ли вставить объект из буфера в этот узел</summary>
  /// <param name="nodeClipboardInfo">Информация об узле в буфере</param>
  /// <returns>Возвращает true, если объект из буфера можно ли вставить в этот узел</returns>
  public override bool CanPasteFromClipboard(NodeClipboardInfo nodeClipboardInfo) => false;
}
