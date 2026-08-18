// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionCellWidgetItemStatus
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Data;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;
using System;
using System.Drawing;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

/// <summary>
/// 
/// </summary>
internal class ArtsCompositionCellWidgetItemStatus : StatusesCellWidget
{
  /// <summary>Конструктор</summary>
  /// <param name="rowWidget">Строка</param>
  /// <param name="column">Колонка</param>
  internal ArtsCompositionCellWidgetItemStatus(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column)
    : base(rowWidget, column)
  {
  }

  /// <summary>Вернуть подсказку для ячейки</summary>
  /// <param name="x">Позиция x курсора мышки</param>
  /// <param name="y">Позиция y курсора мышки</param>
  /// <returns>Подсказка для ячейки</returns>
  protected override string GetToolTipText(int x, int y)
  {
    if (this.CellData.Value == null || this.CellData.Value == DBNull.Value || !(this.CellData.Value is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue.Value == null || !(this.Tree is NavigatorTreeView) || (this.Row.Item is NavigatorTreeNode navigatorTreeNode ? navigatorTreeNode.NodeID : (INodeID) null) == null)
      return string.Empty;
    Rectangle bounds = this.Bounds;
    int num1 = (bounds.Height - 16 /*0x10*/) / 2;
    int num2 = y;
    bounds = this.Bounds;
    int num3 = bounds.Y + num1;
    if (num2 >= num3)
    {
      int num4 = y;
      bounds = this.Bounds;
      int y1 = bounds.Y;
      bounds = this.Bounds;
      int height = bounds.Height;
      int num5 = y1 + height - num1;
      if (num4 <= num5)
      {
        int num6 = x;
        bounds = this.Bounds;
        int right = bounds.Right;
        if (num6 < right)
        {
          int num7 = x;
          bounds = this.Bounds;
          int x1 = bounds.X;
          int num8 = num7 - x1 - 2;
          object obj;
          if (num8 >= num8 / 16 /*0x10*/ * 16 /*0x10*/ + 2 && (obj = nodeDelayedValue.Value) is ArtsCompositionItemStatus)
            return ((ArtsCompositionItemStatus) obj).GetDescription<ArtsCompositionItemStatus>();
        }
      }
    }
    return string.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="graphics">Контекст рисования</param>
  /// <param name="style">Стиль ячейки</param>
  /// <param name="printing">Идёт ли вывод на печать</param>
  protected override void PaintForeground(Graphics graphics, Style style, bool printing)
  {
    object obj;
    if (!(this.CellData.Value is NodeDelayedValue nodeDelayedValue) || nodeDelayedValue.Value == null || !(this.Tree is NavigatorTreeView tree) || (this.Row.Item is NavigatorTreeNode node ? node.NodeID : (INodeID) null) == null || !((obj = nodeDelayedValue.Value) is ArtsCompositionItemStatus))
      return;
    ArtsCompositionItemStatus status = (ArtsCompositionItemStatus) obj;
    IArtsCompositionImageService service = ServiceUtils.GetService<IArtsCompositionImageService>((object) ((tree.GetNodeHandler(node) is IContextAware nodeHandler ? nodeHandler.Services : (IServiceProvider) null) ?? tree.Services), false);
    if (service == null)
      return;
    Image[] imageArray = (Image[]) null;
    int index = service.ImageIndex(status);
    if (index != -1)
      imageArray = new Image[1]
      {
        service.ImageList.Images[index]
      };
    if (imageArray == null)
      return;
    int num1 = 2;
    int num2 = (this.Bounds.Height - 16 /*0x10*/) / 2;
    foreach (Image image1 in imageArray)
    {
      try
      {
        int num3 = this.Bounds.X + num1 + 16 /*0x10*/;
        Rectangle bounds = this.Bounds;
        int right = bounds.Right;
        if (num3 > right)
          break;
        Graphics graphics1 = graphics;
        Image image2 = image1;
        bounds = this.Bounds;
        int x = bounds.X + num1;
        bounds = this.Bounds;
        int y = bounds.Y + num2;
        graphics1.DrawImage(image2, x, y, 16 /*0x10*/, 16 /*0x10*/);
      }
      finally
      {
        image1?.Dispose();
      }
      num1 += 18;
    }
  }
}
