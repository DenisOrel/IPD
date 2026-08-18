// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.TableCreator
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Interfaces.Document;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Вспомогательный класс, обеспечивает ИП при создании таблицы</summary>
public class TableCreator : RectanglePageElementCreator
{
  private static Image image;
  private TableElement newTable;

  /// <summary>Иконка для кнопки статическая версия</summary>
  public new static Image Icon
  {
    get
    {
      if (TableCreator.image == null)
        TableCreator.image = PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.TableElement.png");
      return TableCreator.image;
    }
  }

  /// <summary>Иконка для строки статическая версия</summary>
  public static Image RowIcon
  {
    get
    {
      return PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.TableRow.png");
    }
  }

  /// <summary>Иконка для кнопки</summary>
  public override Image Image
  {
    [DebuggerStepThrough] get
    {
      if (TableCreator.image == null)
        TableCreator.image = this.LoadImageFromResurces("Intermech.Document.Model.Resources.TableElement.png");
      return TableCreator.image;
    }
  }

  /// <summary>Имя элемента</summary>
  public override string Name
  {
    [DebuggerStepThrough] get => TableElement.ElementTypeName;
  }

  /// <summary>Создать прямоугольный элемент</summary>
  /// <param name="parent">Родительский узел</param>
  /// <param name="bounds">Границы элемента</param>
  /// <returns>Прямоугольный элемент</returns>
  public override DocumentTreeNode CreateRectangleElement(
    DocumentTreeNode parent,
    RectangleF bounds)
  {
    if ((double) bounds.Width == 0.0)
      bounds.Width = RectangleElement.DefaultSize.Width;
    if ((double) bounds.Height == 0.0)
      bounds.Height = RectangleElement.DefaultSize.Height;
    this.newTable = new TableElement(parent, bounds, true);
    CharFormat charFormat = (CharFormat) null;
    if (this.DocumentControl != null)
    {
      ImDocumentData document = (ImDocumentData) this.DocumentControl.Document;
      charFormat = document == null ? TextData.DefaultCharFormat : document.DefaultCharFormat;
    }
    this.newTable.InsertNewGridColumn(0, false, false);
    this.newTable.InsertNewRow(0, (RectangleElement) null, true, true);
    this.newTable.InitCellsCharFormat(charFormat);
    TableElement node = this.newTable.Nodes[0] as TableElement;
    node.SetVisible(false, false, false, false, false, false);
    node.SetVisible(true, false, false, false, true, false);
    node.SetCellSizes(bounds, false, false, true, true, false);
    this.newTable.UpdateLayout(false);
    this.Reset();
    if (this.HostPage != null && this.HostPage.OwnerDocument != null && this.HostPage.OwnerDocument.UndoManager != null)
      this.HostPage.OwnerDocument.UndoManager.LockUndo();
    TableElement rectangleElement = (TableElement) null;
    try
    {
      if (TableEditorDialog.Execute(this.newTable) != DialogResult.OK && this.newTable != null)
      {
        this.newTable.Remove(true, false);
        this.newTable = (TableElement) null;
      }
      if (this.newTable != null)
        this.newTable.InitCellsCharFormat(charFormat);
      rectangleElement = this.newTable;
      this.newTable = (TableElement) null;
    }
    finally
    {
      if (this.HostPage != null && this.HostPage.OwnerDocument != null && this.HostPage.OwnerDocument.UndoManager != null)
        this.HostPage.OwnerDocument.UndoManager.UnlockUndo();
    }
    return (DocumentTreeNode) rectangleElement;
  }

  public override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    if (this.newTable == null)
      return;
    this.newTable.Draw((DrawContext) new DrawContextWithUI(this.newTable.OwnerDocument as ImDocument, this.PageControl, new ImGraphics(e.Graphics), true, VisualNode.NoClipRectangle, 0, true, true, (MatrixWrapper) null));
  }
}
