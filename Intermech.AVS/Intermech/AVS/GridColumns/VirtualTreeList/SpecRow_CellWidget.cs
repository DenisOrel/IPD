// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.SpecRow_CellWidget
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Ячейка записи спецификации</summary>
public class SpecRow_CellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column) : 
  CellWidget(rowWidget, column)
{
  /// <summary>Редактор в ячейке</summary>
  public Control Editor => this.EditorControl;

  protected override void LayoutEditor()
  {
    if (this.CellData.Editor != null)
    {
      int displayMode = (int) this.CellData.Editor.DisplayMode;
    }
    base.LayoutEditor();
  }

  protected override void PaintForeground(Graphics graphics, Style style, bool printing)
  {
    base.PaintForeground(graphics, style, printing);
  }

  public override void CompleteEdit() => base.CompleteEdit();

  public override void AbandonEdit() => base.AbandonEdit();

  public override void StartEdit()
  {
    AVSTreeCheckBox editorControl = this.EditorControl as AVSTreeCheckBox;
    base.StartEdit();
  }

  public override bool Editable => base.Editable;

  protected override void CreateEditorControl()
  {
    base.CreateEditorControl();
    if (!(this.EditorControl is AVSUniversalEditBox))
      return;
    AVSWindow avsWindow = (this.Tree as Intermech.AVS.GridColumns.VirtualTreeList.VirtualTreeList).AVSWindow;
    AVSUniversalEditBox editorControl = this.EditorControl as AVSUniversalEditBox;
    editorControl.AVSWindow = avsWindow;
    editorControl.ValueOwner = this.Row.Item;
    editorControl.Converter = this.CellData.TypeConverter;
    if (this.CellData.TypeEditor == null)
      editorControl.UseDefaultEditor = true;
    else
      editorControl.Editor = this.CellData.TypeEditor;
  }

  protected override void PaintBackground(
    Graphics graphics,
    Style rowStyle,
    Style cellStyle,
    bool printing)
  {
    base.PaintBackground(graphics, rowStyle, cellStyle, printing);
    if (this.Row.Item is AVSRow avsRow && !this.Tree.SelectedRows.Contains(this.Row))
    {
      if (avsRow.IsHiddenRow)
      {
        using (SolidBrush solidBrush = new SolidBrush(Color.Gray))
          graphics.FillRectangle((Brush) solidBrush, this.Bounds);
      }
      else
      {
        INavGraphicsCache navGraphicsCache = (this.Tree as Intermech.AVS.GridColumns.VirtualTreeList.VirtualTreeList).AVSWindow._navGraphicsCache;
        ICurrentUserAndRole currentUserAndRole = (this.Tree as Intermech.AVS.GridColumns.VirtualTreeList.VirtualTreeList).AVSWindow._currentUserAndRole;
        UIColorsScheme currentColorsScheme = navGraphicsCache.CurrentColorsScheme;
        Color startColor = Color.Transparent;
        Color endColor = Color.Transparent;
        bool useGradient = false;
        long? nullable1 = new long?(avsRow.GetFieldInt64Value(new AvsRowAttributeInfo(false, -6), -1, (List<RelationAttributeValuesCache>) null, false));
        if (nullable1.HasValue)
        {
          long? nullable2 = nullable1;
          long num1 = 0;
          LinearGradientMode mode;
          if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
          {
            nullable2 = nullable1;
            long num2 = -1;
            if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
            {
              nullable2 = nullable1;
              long userId = currentUserAndRole.UserID;
              if (nullable2.GetValueOrDefault() == userId & nullable2.HasValue)
              {
                startColor = currentColorsScheme.CheckedOutBkStartColor;
                endColor = currentColorsScheme.CheckedOutBkEndColor;
                mode = currentColorsScheme.CheckedOutGradientMode;
                useGradient = (currentColorsScheme.Gradient & GradientUsing.CheckOut) == GradientUsing.CheckOut;
                goto label_14;
              }
              startColor = currentColorsScheme.CheckedOutOtherBkStartColor;
              endColor = currentColorsScheme.CheckedOutOtherBkEndColor;
              mode = currentColorsScheme.CheckedOutOtherGradientMode;
              useGradient = (currentColorsScheme.Gradient & GradientUsing.CheckedOutOther) == GradientUsing.CheckedOutOther;
              goto label_14;
            }
          }
          mode = LinearGradientMode.Horizontal;
label_14:
          if (startColor != Color.Transparent && endColor != Color.Transparent)
          {
            NavGradientBrush navGradientBrush = navGraphicsCache.GetNavGradientBrush(startColor, endColor, mode, this.Bounds, useGradient);
            Rectangle bounds = this.Bounds;
            graphics.FillRectangle(navGradientBrush.Brush, bounds);
          }
        }
      }
    }
    Rectangle bounds1 = this.Bounds;
    if (this.Row.ChildIndex == this.Row.ParentRow.NumChildren - 1)
      --bounds1.Height;
    graphics.DrawRectangle(Pens.LightGray, bounds1);
  }
}
