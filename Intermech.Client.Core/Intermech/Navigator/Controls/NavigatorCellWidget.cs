
// Type: Intermech.Navigator.Controls.NavigatorCellWidget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Ячейка дерева "Навигатора"</summary>
/// <summary>Конструктор</summary>
/// <param name="rowWidget">Строка</param>
/// <param name="column">Колонка</param>
public class NavigatorCellWidget(Infralution.Controls.VirtualTree.RowWidget rowWidget, Column column) : 
  CellWidget(rowWidget, column)
{
  /// <summary>Ширина для чек-боксов</summary>
  protected internal static int CheckBoxWidth = 16 /*0x10*/;
  /// <summary>
  /// Ширина для дополнительного значка, показывающего состояние объекта - взят на изменение или нет
  /// </summary>
  protected internal static int CheckOutWidth = 16 /*0x10*/;
  /// <summary>
  /// Ширина для дополнительного значка, показывающего признак базовой версии объекта
  /// </summary>
  protected internal static int VersionWidth = 16 /*0x10*/;
  /// <summary>Индекс изображения "imgBaseVersion"</summary>
  internal static int _imgBaseVersion = -1;
  /// <summary>Индекс изображения "imgNonBaseVersion"</summary>
  internal static int _imgNonBaseVersion = -1;
  /// <summary>Индекс изображения "BaseVersionEmpty"</summary>
  internal static int _imgBaseVersionEmpty = -1;

  /// <summary>Вернуть подсказку для ячейки</summary>
  /// <returns>Подсказка для ячейки</returns>
  protected override string GetToolTipText()
  {
    NavigatorTreeView tree = this.Tree as NavigatorTreeView;
    NavigatorTreeNode navigatorTreeNode = this.Row != null ? this.Row.Item as NavigatorTreeNode : (NavigatorTreeNode) null;
    if (tree == null || navigatorTreeNode == null || navigatorTreeNode.NodeID == null || this.CellData.Value == null)
      return string.Empty;
    string text = this.CellData.Value.ToString();
    return (double) this.CalculateTextBounds((Control) tree, text).Width > (double) this.Bounds.Width ? text : string.Empty;
  }

  /// <summary>Рассчитать ширину и высоту текста</summary>
  /// <param name="control">Контрол</param>
  /// <param name="text">Текст</param>
  /// <returns>Ширина и высота текста</returns>
  private SizeF CalculateTextBounds(Control control, string text)
  {
    using (Graphics graphics = this.Tree.CreateGraphics())
    {
      int width = Screen.PrimaryScreen.WorkingArea.Width / 100 * 50;
      return graphics.MeasureString(text, control.Font, width, StringFormat.GenericDefault);
    }
  }

  /// <summary>Рассчитать оптимальную ширину</summary>
  /// <param name="graphics">Контекст рисования</param>
  /// <returns>Оптимальная ширина</returns>
  public override int GetOptimalWidth(Graphics graphics)
  {
    int optimalWidth = base.GetOptimalWidth(graphics);
    if (this.Column.MainColumn && (this.Tree as NavigatorTreeView).CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None)
      optimalWidth += NavigatorCellWidget.CheckBoxWidth + 4;
    return optimalWidth;
  }

  /// <summary>Список изображений</summary>
  protected INamedImageList VersionsImageList => Holder.NamedImageList;

  /// <summary>Получить индекс изображения для указанной ячейки</summary>
  /// <param name="nodeID">Идентификатор узла</param>
  /// <param name="cell">Ячейка</param>
  /// <param name="columns">Список доступных колонок</param>
  /// <param name="control">Контрол</param>
  /// <returns>-1 или индекс изображения</returns>
  protected int VersionsImageIndex(INodeID nodeID, NodeColumnCollection columns)
  {
    if (NavigatorCellWidget._imgBaseVersion < 0 && this.VersionsImageList != null)
    {
      NavigatorCellWidget._imgNonBaseVersion = this.VersionsImageList.ImageIndex("imgNonBaseVersion");
      NavigatorCellWidget._imgBaseVersion = this.VersionsImageList.ImageIndex("imgBaseVersion");
      NavigatorCellWidget._imgBaseVersionEmpty = this.VersionsImageList.ImageIndex("imgBaseVersionEmpty");
    }
    if (UISettings.NavigatorWindowBaseVersionsMode == NavigatorWindowBaseVersionsMode.Hidden)
      return -1;
    NavigatorTreeView tree = this.Tree as NavigatorTreeView;
    INodeID rootNodeId = tree.RootNodeID;
    IDBObjectID data = rootNodeId == null || tree.RootHandler == null ? (IDBObjectID) null : tree.RootHandler.GetData(rootNodeId, typeof (IDBObjectID)) as IDBObjectID;
    if (data == null || data.Value == 0L || !(nodeID is NodeID nodeId))
      return -1;
    return (nodeId.BaseVersion & 1L) == 0L ? ((UISettings.NavigatorWindowBaseVersionsMode & NavigatorWindowBaseVersionsMode.ShowOtherVersions) == NavigatorWindowBaseVersionsMode.ShowOtherVersions ? NavigatorCellWidget._imgNonBaseVersion : NavigatorCellWidget._imgBaseVersionEmpty) : ((UISettings.NavigatorWindowBaseVersionsMode & NavigatorWindowBaseVersionsMode.ShowBaseVersions) == NavigatorWindowBaseVersionsMode.ShowBaseVersions ? NavigatorCellWidget._imgBaseVersion : NavigatorCellWidget._imgBaseVersionEmpty);
  }

  /// <summary>Отрисовать текст</summary>
  /// <param name="graphics">Контекст рисования</param>
  /// <param name="style">Стиль</param>
  /// <param name="printing">Идёт ли вывод на печать</param>
  protected override void PaintForeground(Graphics graphics, Style style, bool printing)
  {
    if (this.Row == null || this.Row.Item == null)
      return;
    if (this.ShowErrorIndicator)
      this.PaintErrorIndicator(graphics, style, printing);
    if (this.ShowPreview)
      this.PaintPreview(graphics, style, printing);
    Rectangle textBounds = this.GetTextBounds();
    Rectangle targetRect = new Rectangle(textBounds.X + 2, textBounds.Y + (textBounds.Height - NavigatorCellWidget.CheckBoxWidth) / 2, NavigatorCellWidget.CheckBoxWidth, NavigatorCellWidget.CheckBoxWidth);
    Rectangle rect1 = new Rectangle(textBounds.X - 6 - NavigatorCellWidget.CheckOutWidth - NavigatorCellWidget.VersionWidth, textBounds.Y + (textBounds.Height - NavigatorCellWidget.CheckOutWidth) / 2, NavigatorCellWidget.CheckOutWidth, NavigatorCellWidget.CheckOutWidth);
    Rectangle rect2 = new Rectangle(rect1.X + 4 + rect1.Width, rect1.Y + (textBounds.Height - NavigatorCellWidget.VersionWidth) / 2, NavigatorCellWidget.VersionWidth, NavigatorCellWidget.VersionWidth);
    NavigatorTreeView tree = this.Tree as NavigatorTreeView;
    NavigatorTreeNode node = this.Row != null ? this.Row.Item as NavigatorTreeNode : (NavigatorTreeNode) null;
    if (tree.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None && this.RowWidget.MainColumn == this.Column)
    {
      Icon icon = (Icon) null;
      try
      {
        if (node != null)
        {
          if (node.ShowCheckState)
          {
            switch (node.CheckState)
            {
              case CheckState.Checked:
                using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Checkbox_checked.ico"))
                {
                  icon = new Icon(resourceStream);
                  break;
                }
              case CheckState.Indeterminate:
                using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Checkbox_grayed.ico"))
                {
                  icon = new Icon(resourceStream);
                  break;
                }
              default:
                using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Checkbox_unchecked.ico"))
                {
                  icon = new Icon(resourceStream);
                  break;
                }
            }
          }
        }
        else
        {
          using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Checkbox_unchecked.ico"))
            icon = new Icon(resourceStream);
        }
        if (icon != null)
          graphics.DrawIcon(icon, targetRect);
      }
      finally
      {
        icon?.Dispose();
      }
    }
    int index1 = -1;
    if (tree != null && !tree.DisableCheckedOutColumn && this.RowWidget.MainColumn == this.Column && this.RowWidget.RowData.ImageSize != 32 /*0x20*/ && node != null && node.InTree && node.NodeID != null)
    {
      INodeID nodeId = node.NodeID;
      INode nodeHandler = tree.GetNodeHandler(node);
      nodeHandler?.GetData(nodeId, typeof (IDBObjectID));
      IDBCheckedOutByID data = nodeHandler != null ? nodeHandler.GetData(nodeId, typeof (IDBCheckedOutByID)) as IDBCheckedOutByID : (IDBCheckedOutByID) null;
      if (data != null && tree._currentUserAndRole != null && data.CheckedOutBy != 0L)
        index1 = data.CheckedOutBy != tree._currentUserAndRole.UserID ? Holder.NamedImageList.ImageIndex("imgUserOther") : Holder.NamedImageList.ImageIndex("imgUserCurrent");
      if (index1 >= 0)
        graphics.DrawImage(Holder.NamedImageList.ImageList.Images[index1], rect1);
      int index2 = this.VersionsImageIndex(nodeId, tree.TreeColumns);
      if (this.VersionsImageList != null && index2 >= 0)
        graphics.DrawImage(Holder.NamedImageList.ImageList.Images[index2], rect2);
    }
    if (!this.CellData.ShowText)
      return;
    string text = this.Text;
    if (this.Tree.SelectionMode == Infralution.Controls.VirtualTree.SelectionMode.MainCellText && this.Row.Selected && this.RowWidget.MainColumn == this.Column && !printing)
    {
      style = this.GetSelectedStyle();
      Rectangle actualTextBounds = this.GetActualTextBounds(graphics, textBounds, style, text);
      this.PaintSelectedTextBackground(graphics, actualTextBounds, style);
    }
    if (this.RowWidget.MainColumn == this.Column && tree != null && tree.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None && node != null && node.ShowCheckState)
    {
      textBounds.X += NavigatorCellWidget.CheckBoxWidth + 4;
      textBounds.Width -= NavigatorCellWidget.CheckBoxWidth + 4;
    }
    if (node != null && this.Column is NavigatorTreeColumn)
    {
      NodeColumn navigatorColumn = ((NavigatorTreeColumn) this.Column).NavigatorColumn;
      if (navigatorColumn != null && node.IsCellReadOnly(navigatorColumn) && ((NavigatorTreeView) this.Tree).EditingMode)
        style = new Style(style, new StyleDelta()
        {
          ForeColor = Color.Gray
        });
    }
    if (tree != null && tree.BeforePaintText != null)
      tree.BeforePaintText(node, ref style);
    this.PaintText(graphics, textBounds, style, text);
  }

  /// <summary>Отпущена клавиша мышки в ячейке</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseDown(MouseEventArgs e)
  {
    NavigatorTreeColumn column = this.Column as NavigatorTreeColumn;
    NavigatorTreeView tree = this.Tree as NavigatorTreeView;
    if (column == null || !column.MainColumn || tree.CheckBoxStyle == NavigatorTreeViewCheckBoxStyle.None || e.Button != MouseButtons.Left)
    {
      base.OnMouseDown(e);
    }
    else
    {
      if (!(this.Row.Item is NavigatorTreeNode navigatorTreeNode) || !navigatorTreeNode.ShowCheckState)
        return;
      Rectangle textBounds = this.GetTextBounds();
      if (!new Rectangle(textBounds.X + 2, textBounds.Y + (textBounds.Height - NavigatorCellWidget.CheckBoxWidth) / 2, NavigatorCellWidget.CheckBoxWidth, NavigatorCellWidget.CheckBoxWidth).Contains(e.X, e.Y))
      {
        tree.CancelSelectionChanging = false;
        base.OnMouseDown(e);
      }
      else
      {
        tree.CancelSelectionChanging = true;
        base.OnMouseDown(e);
        CheckState checkState = navigatorTreeNode.CheckState != CheckState.Unchecked ? CheckState.Unchecked : CheckState.Checked;
        navigatorTreeNode.SetCheckState(checkState);
        tree.UpdateCommandManagerItems();
      }
    }
  }

  /// <summary>Raises the double click event</summary>
  /// <param name="e">Event information to send to registered event handlers</param>
  public override void OnDoubleClick(EventArgs e)
  {
    NavigatorTreeColumn column = this.Column as NavigatorTreeColumn;
    NavigatorTreeView tree = this.Tree as NavigatorTreeView;
    if (column != null && column.MainColumn && tree.CheckBoxStyle != NavigatorTreeViewCheckBoxStyle.None && this.Row.Item is NavigatorTreeNode navigatorTreeNode && navigatorTreeNode.ShowCheckState)
    {
      Rectangle textBounds = this.GetTextBounds();
      Rectangle rectangle = new Rectangle(textBounds.X + 2, textBounds.Y + (textBounds.Height - NavigatorCellWidget.CheckBoxWidth) / 2, NavigatorCellWidget.CheckBoxWidth, NavigatorCellWidget.CheckBoxWidth);
      Point position = Cursor.Position;
      Point client = tree.PointToClient(position);
      if (rectangle.Contains(client.X, client.Y))
        return;
    }
    base.OnDoubleClick(e);
  }
}
