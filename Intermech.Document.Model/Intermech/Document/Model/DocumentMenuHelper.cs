// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.DocumentMenuHelper
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.ComponentModel;
using Intermech.Docking;
using Intermech.Document.Model.UI;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Вспомогательный класс реализующий стандартные пункты меню</summary>
public class DocumentMenuHelper : IDisposable
{
  public const string PropertyCommandName = "DocElementProperty";
  private ImDocumentEditorFormBase form;
  protected ICommandManager commandManager;
  /// <summary>Текст пункта меню CreateDataField</summary>
  public static string miCreateDataField_Name = LocalizationHolder.rm.GetString("Document.Model_170");
  private static MenuButtonItem miInsertPageBefore = (MenuButtonItem) null;
  private static MenuButtonItem miInsertPageAfter = (MenuButtonItem) null;
  /// <summary>Служба именованных значков</summary>
  public static INamedImageList _namedImageList;
  /// <summary>Guid панели инструментов "Навигация"</summary>
  public static Guid NavigatorToolBarGuid = new Guid("CDBD12E3-CADF-4f83-B3D4-62CBF1177DAB");
  /// <summary>Guid панели инструментов "Таблицы"</summary>
  public static Guid TableToolBarGuid = new Guid("0AF3AFDC-9B19-48c4-8DB3-896BCB1E96DF");
  /// <summary>ChooseFontComboBoxToolbarItem</summary>
  public ChooseFontComboBoxToolbarItem ChooseFontComboBoxToolbarItem;
  /// <summary>CbFontSize</summary>
  public ComboBoxItem CbFontSize;
  /// <summary>CbLineStyle</summary>
  public ComboBoxItem CbLineStyle;
  /// <summary>CbPage</summary>
  public ComboBoxItem CbPage;
  /// <summary>CbDocument</summary>
  public ComboBoxItem CbDocument;
  private ButtonItemBase BPrevDocument;
  private ButtonItemBase BNextDocument;
  /// <summary>CbLineStyle</summary>
  public ComboBoxItem CbLineWidth;
  /// <summary>BordersToolButton</summary>
  public ButtonItemBase BordersToolButton;
  public static Image CaLeftTopImage = (Image) null;
  public static Image CaCenterTopImage = (Image) null;
  public static Image CaRightTopImage = (Image) null;
  public static Image CaJustifyTopImage = (Image) null;
  public static Image CaLeftCenterImage = (Image) null;
  public static Image CaCenterCenterImage = (Image) null;
  public static Image CaRightCenterImage = (Image) null;
  public static Image CaJustifyCenterImage = (Image) null;
  public static Image CaLeftBottomImage = (Image) null;
  public static Image CaCenterBottomImage = (Image) null;
  public static Image CaRightBottomImage = (Image) null;
  public static Image CaJustifyBottomImage = (Image) null;
  public IconicMenuItem CaLeftTopIconicMenuItem;
  public IconicMenuItem CaCenterTopIconicMenuItem;
  public IconicMenuItem CaRightTopIconicMenuItem;
  public IconicMenuItem CaJustifyTopIconicMenuItem;
  public IconicMenuItem CaLeftCenterIconicMenuItem;
  public IconicMenuItem CaCenterCenterIconicMenuItem;
  public IconicMenuItem CaRightCenterIconicMenuItem;
  public IconicMenuItem CaJustifyCenterIconicMenuItem;
  public IconicMenuItem CaLeftBottomIconicMenuItem;
  public IconicMenuItem CaCenterBottomIconicMenuItem;
  public IconicMenuItem CaRightBottomIconicMenuItem;
  public IconicMenuItem CaJustifyBottomIconicMenuItem;
  /// <summary>Guid панели инструментов "Форматирование"</summary>
  public static Guid FormatToolBarGuid = new Guid("A5BE6022-B284-4a0e-AB9F-C9AE18BDA383");
  /// <summary>OldFontSizeValue</summary>
  public string OldFontSizeValue = string.Empty;
  private static int _lockTextSizeChangeEventsCounter = 0;
  private static DocumentMenuHelper instance;
  private static Color _bgColor = Color.Aqua;
  private static ColorMenu bgColorMenu = (ColorMenu) null;
  public IconicMenu _iconicMenu;
  private static TextMenuItem noBgColorMenuItem = (TextMenuItem) null;
  private Color _textBkColor = Color.Transparent;
  private ColorMenu textBkColorMenu;
  private TextMenuItem noTextBkColorMenuItem;
  private Color linesColor = Color.Black;
  private ColorMenu linesColorMenu;
  private Color _textColor = Color.Aqua;
  private ColorMenu textColorMenu;
  /// <summary>Guid панели инструментов "Красный карандаш"</summary>
  public static Guid RedlineOnOffToolBarGuid = new Guid("246C770C-03F6-497E-AEDD-4E05D92DD8E9");
  /// <summary>DockManager</summary>
  public static DockManager DockManager = (DockManager) null;
  private static bool _menuItemsCreated = false;
  /// <summary>ActiveBordersCommand</summary>
  public static string ActiveBordersCommand = (string) null;
  /// <summary>ActiveCellAlignCommand</summary>
  public static string ActiveCellAlignCommand = (string) null;
  private static IDictionary menuDictionary = (IDictionary) new HybridDictionary();

  public DocumentMenuHelper(ICommandManager commandManager) => this.commandManager = commandManager;

  public ImDocumentEditorFormBase Form
  {
    get => this.form;
    set => this.form = value;
  }

  private static bool QueryStatus_RemoveNode(DocumentTreeNode context)
  {
    return context.CanRemove() && !DocumentMenuHelper.QueryStatus_RemoveRow(context) && !DocumentMenuHelper.QueryStatus_RemoveColumn(context) && !DocumentMenuHelper.QueryStatus_RemoveCell(context);
  }

  /// <summary>Удалить узлы заданные в массиве. Метод для внутреннего использования.</summary>
  /// <param name="nodeArray">Узлы, которые нужно удалить</param>
  public static void UserCommand_DeleteNodes(
    DocumentTreeNode[] nodeArray,
    bool deletePages,
    DocumentControl docControl)
  {
    if (docControl?.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_568"));
    try
    {
      if (nodeArray == null)
        return;
      if (nodeArray.Length == 1 && nodeArray[0] is TextBoxElement node && node.InPlaceEditorActive && node.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.HilightType != 0)
      {
        placeEditorControl.TerCommand(606);
      }
      else
      {
        DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(nodeArray, true);
        List<ImDocumentData> imDocumentDataList = new List<ImDocumentData>();
        for (int index = 0; index < nodesWithoutChilds.Length; ++index)
        {
          if (nodesWithoutChilds[index] is IDocumentElement documentElement && documentElement.OwnerDocument != null && !imDocumentDataList.Contains(documentElement.OwnerDocument))
            imDocumentDataList.Add(documentElement.OwnerDocument);
          if (!(nodesWithoutChilds[index] is Page) | deletePages)
          {
            if (nodesWithoutChilds[index] is RectangleElement rectangleElement && rectangleElement.IsTableCell && rectangleElement.IsSingleCell)
              DocumentMenuHelper.RemoveCell_Execute(new DocumentTreeNode[1]
              {
                (DocumentTreeNode) rectangleElement
              }, RemoveCellOptions.MergeWithLeft, true);
            else
              nodesWithoutChilds[index].UserCommand_Delete(false);
          }
        }
        for (int index = 0; index < imDocumentDataList.Count; ++index)
          imDocumentDataList[index].UpdateLayout(false, true);
        docControl?.UnselectRemovedNodes();
      }
    }
    finally
    {
      docControl?.UndoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Обработчик пункта меню "Вырезать"</summary>
  private static void Cut_Execute(
    DocumentTreeNode[] context,
    IntPtr hWnd,
    DocumentControl docControl)
  {
    if (context == null)
      return;
    if (context.Length == 1 && context[0] is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.HilightType != 0)
    {
      placeEditorControl.TerCommand(628);
    }
    else
    {
      NodeClipboardHelper.CopyToClipboard(context, hWnd);
      DocumentMenuHelper.UserCommand_DeleteNodes(context, true, docControl);
    }
  }

  /// <summary>Обработчик пункта меню "Копировать"</summary>
  private static void Copy_Execute(DocumentTreeNode[] context, IntPtr hWnd)
  {
    if (context == null)
      return;
    if (context.Length == 1 && context[0] is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl && placeEditorControl.HilightType != 0)
      placeEditorControl.TerCommand(629);
    else
      NodeClipboardHelper.CopyToClipboard(context, hWnd);
  }

  /// <summary>Обработчик пункта меню "Вставить"</summary>
  private static void Paste_Execute(
    DocumentTreeNode[] context,
    IntPtr hWnd,
    DocumentControl docControl)
  {
    if (context == null || context.Length == 0)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_569"));
    try
    {
      bool flag = false;
      if (context[0] is Page && (context[0] as Page).DocumentControl.PageControl != null && NodeClipboardHelper.CanPasteFromClipboard(context[0]))
        flag = (context[0] as Page).DocumentControl.PageControl.InitPasteNodesFromClipboard(context[0]);
      if (flag)
        return;
      NodeClipboardHelper.PasteFromClipboard(context[0], hWnd);
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Обработчик пункта меню "Удалить"</summary>
  private static void CallEditor_Execute(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (context == null || context.Length == 0)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo("");
    try
    {
      if (!context[0].CanCallEditor)
        return;
      context[0].CallEditor();
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  private static void MoveToBegin(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_570"));
    try
    {
      if (context == null || context.Length != 1 || context[0].Parent == null)
        return;
      context[0].MoveDataElementToBegin(true);
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  private static void MoveToEnd(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_570"));
    try
    {
      if (context == null || context.Length != 1 || context[0].Parent == null)
        return;
      context[0].MoveDataElementToEnd(true);
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  private static void MoveUp(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (context == null || context.Length != 1)
      return;
    docControl.UndoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_570"));
    try
    {
      context[0].MoveDataElementUp(true);
    }
    finally
    {
      docControl.UndoManager?.EndCreateMultyUndo();
    }
  }

  private static void MoveDown(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (context == null || context.Length != 1)
      return;
    docControl.UndoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_570"));
    try
    {
      context[0].MoveDataElementDown(true);
    }
    finally
    {
      docControl.UndoManager?.EndCreateMultyUndo();
    }
  }

  private static void BlockGeometryChanging_Execute(
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    if (context == null)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo("");
    try
    {
      for (int index = 0; index < context.Length; ++index)
      {
        if (context[index] is PageElementNode pageElementNode)
          pageElementNode.SetGeometryChangingBlockedRecursive(true);
      }
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  private static void UnblockGeometryChanging_Execute(
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    if (context == null)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo("");
    try
    {
      for (int index = 0; index < context.Length; ++index)
      {
        if (context[index] is PageElementNode pageElementNode)
          pageElementNode.SetGeometryChangingBlockedRecursive(false);
      }
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Преобразовать в Метку"</summary>
  public static void ConvertToLabel_Execute(DocumentTreeNode[] context)
  {
    if (context == null || context.Length == 0)
      return;
    VisualNode visualNode = context[0] as VisualNode;
    IUndoManager undoManager = (IUndoManager) null;
    if (visualNode != null && visualNode.OwnerDocument != null && visualNode.OwnerDocument.UndoManager != null)
      undoManager = visualNode.OwnerDocument.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_571"));
    try
    {
      ImDocumentData imDocumentData = (ImDocumentData) null;
      int fromPage = -1;
      for (int index1 = 0; index1 < context.Length; ++index1)
      {
        RectangleElement node = context[index1] as RectangleElement;
        TextBoxElement textBoxElement = node as TextBoxElement;
        ContainerElement containerElement = node as ContainerElement;
        if (textBoxElement != null || containerElement != null)
        {
          DocumentTreeNode parent1 = node.Parent;
          int index2 = node.Index;
          DocumentControl documentControl = DocumentMenuHelper.GetDocumentControl((DocumentTreeNode) node);
          bool flag = false;
          if (documentControl != null && documentControl.ActiveElement == node)
            flag = true;
          PageData page = node.Page;
          int index3;
          if (page != null && (fromPage > (index3 = page.Index) || fromPage == -1))
          {
            fromPage = index3;
            if (imDocumentData == null)
            {
              imDocumentData = page.OwnerDocument;
              imDocumentData?.SuspendUpdateGeometryRefreshUI();
            }
          }
          if (textBoxElement != null)
            textBoxElement.ConvertToLabel();
          else
            containerElement?.ConvertToLabel();
          DocumentTreeNode parent2 = parent1;
          RectangleElement rectElement = node;
          DocumentControl doc = documentControl;
          int num = flag ? 1 : 0;
          DocumentMenuHelper.ChangeSelection(index2, parent2, (DocumentTreeNode) rectElement, doc, num != 0);
        }
      }
      if (fromPage == -1)
        fromPage = 0;
      if (imDocumentData == null)
        return;
      imDocumentData.ResumeUpdateRefreshUI(false, false);
      imDocumentData.UpdateLayout(fromPage, false, true);
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Преобразовать в Редактор текста"</summary>
  public static void ConvertToTextBox_Execute(DocumentTreeNode[] context)
  {
    if (context == null || context.Length == 0)
      return;
    VisualNode visualNode = context[0] as VisualNode;
    IUndoManager undoManager = (IUndoManager) null;
    if (visualNode != null && visualNode.OwnerDocument != null && visualNode.OwnerDocument.UndoManager != null)
      undoManager = visualNode.OwnerDocument.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_571"));
    try
    {
      ImDocumentData imDocumentData = (ImDocumentData) null;
      int fromPage = -1;
      for (int index1 = 0; index1 < context.Length; ++index1)
      {
        RectangleElement node = context[index1] as RectangleElement;
        DocumentTreeNode parent1 = node.Parent;
        int index2 = node.Index;
        DocumentControl documentControl = DocumentMenuHelper.GetDocumentControl((DocumentTreeNode) node);
        bool flag = false;
        if (documentControl != null && documentControl.ActiveElement == node)
          flag = true;
        LabelElement labelElement = node as LabelElement;
        ContainerElement containerElement = node as ContainerElement;
        TableElement tableElement = node as TableElement;
        if (labelElement != null || containerElement != null || tableElement != null)
        {
          PageData page = node.Page;
          int index3;
          if (page != null && (fromPage > (index3 = page.Index) || fromPage == -1))
          {
            fromPage = index3;
            if (imDocumentData == null)
            {
              imDocumentData = page.OwnerDocument;
              imDocumentData?.SuspendUpdateGeometryRefreshUI();
            }
          }
          if (labelElement != null)
            labelElement.ConvertToTextBox();
          else if (containerElement != null)
            containerElement.ConvertToTextBox();
          else
            tableElement?.ConvertToTextBox();
        }
        DocumentTreeNode parent2 = parent1;
        RectangleElement rectElement = node;
        DocumentControl doc = documentControl;
        int num = flag ? 1 : 0;
        DocumentMenuHelper.ChangeSelection(index2, parent2, (DocumentTreeNode) rectElement, doc, num != 0);
      }
      if (fromPage == -1)
        fromPage = 0;
      if (imDocumentData == null)
        return;
      imDocumentData.ResumeUpdateRefreshUI(false, false);
      imDocumentData.UpdateLayout(fromPage, false, true);
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Поличить DocumentControl элемента</summary>
  /// <param name="node">элемент</param>
  /// <returns></returns>
  protected static DocumentControl GetDocumentControl(DocumentTreeNode node)
  {
    if (node != null && node is PageElementNode)
    {
      PageElementNode pageElementNode = node as PageElementNode;
      if (pageElementNode.Page != null && pageElementNode.Page is Page && (pageElementNode.Page as Page).DocumentControl != null)
        return (pageElementNode.Page as Page).DocumentControl;
    }
    return (DocumentControl) null;
  }

  protected static void ChangeSelection(
    int index,
    DocumentTreeNode parent,
    DocumentTreeNode rectElement,
    DocumentControl doc,
    bool isActive)
  {
    DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
    if (parent != null && parent.NodesCount > index)
      documentTreeNode = parent.Nodes[index];
    if (documentTreeNode == null || !(documentTreeNode is PageElementNode))
      return;
    if (documentTreeNode is IPageElementWithInterface elementWithInterface && elementWithInterface.PageUI == null)
    {
      elementWithInterface.CreateUI();
      elementWithInterface.PageUI.UpdateGeometry();
    }
    List<DocumentTreeNode> selection = new List<DocumentTreeNode>();
    for (int index1 = 0; index1 < doc.SelectedNodes.Count; ++index1)
      selection.Add(doc.SelectedNodes[index1]);
    int index2 = doc.SelectedNodes.IndexOf(rectElement);
    if (index2 != -1)
    {
      selection.Remove(rectElement);
      selection.Insert(index2, documentTreeNode);
    }
    else
      selection.Add(documentTreeNode);
    doc.SetSelection(selection, true, Point.Empty, true, false);
  }

  /// <summary>Конвертация из одного типа в другой</summary>
  /// <param name="context"></param>
  /// <param name="name"></param>
  public static void ConvertToElement(DocumentTreeNode[] context, string name)
  {
    if (context == null || context.Length == 0)
      throw new ArgumentException(nameof (context));
    string elementTypeName1 = TextBoxElement.ElementTypeName;
    string elementTypeName2 = LabelElement.ElementTypeName;
    string elementTypeName3 = ContainerElement.ElementTypeName;
    if (name == elementTypeName1)
      DocumentMenuHelper.ConvertToTextBox_Execute(context);
    if (name == elementTypeName2)
      DocumentMenuHelper.ConvertToLabel_Execute(context);
    if (!(name == elementTypeName3))
      return;
    DocumentMenuHelper.ConvertToContainer_Execute(context);
  }

  /// <summary>Команда контекстного меню "Преобразовать в Область"</summary>
  private static void ConvertToArea_Execute(DocumentTreeNode[] context)
  {
    if (context == null || context.Length != 1 || !(context[0] is RectangleElement rectangleElement))
      return;
    IUndoManager undoManager = (IUndoManager) null;
    if (rectangleElement.OwnerDocument?.UndoManager != null)
      undoManager = rectangleElement.OwnerDocument.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_573"));
    try
    {
      PageData page = rectangleElement.Page;
      ImDocumentData imDocumentData = (ImDocumentData) null;
      if (page != null)
        imDocumentData = page.OwnerDocument;
      if (!(rectangleElement is TableData tableData1))
      {
        tableData1 = rectangleElement.SplitCell(1, 1, true, false, false);
        if (!tableData1.IsTableCell && tableData1.NodesCount > 0 && tableData1.Nodes[0].NodesCount > 0 && !(tableData1.Nodes[0].Nodes[0] is TableData))
        {
          TableData tableData = (tableData1.Nodes[0].Nodes[0] as RectangleElement).SplitCell(0, 0, true, false, false);
          tableData.SetGridColumnsParams(new List<RowColParams>(), true, true);
          tableData1 = tableData;
        }
      }
      tableData1?.SetIsFixedStructureArea(true, false, false);
      if (imDocumentData == null || page == null)
        return;
      imDocumentData.UpdateLayout(page.Index, false, true);
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Преобразовать в Контейнер"</summary>
  public static void ConvertToContainer_Execute(DocumentTreeNode[] context)
  {
    if (context == null || context.Length == 0)
      return;
    VisualNode visualNode = context[0] as VisualNode;
    IUndoManager undoManager = (IUndoManager) null;
    if (visualNode != null && visualNode.OwnerDocument != null && visualNode.OwnerDocument.UndoManager != null)
      undoManager = visualNode.OwnerDocument.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_571"));
    try
    {
      ImDocumentData imDocumentData = (ImDocumentData) null;
      int fromPage = -1;
      for (int index1 = 0; index1 < context.Length; ++index1)
      {
        RectangleElement node = context[index1] as RectangleElement;
        TextBoxElement textBoxElement = node as TextBoxElement;
        LabelElement labelElement = node as LabelElement;
        if (textBoxElement != null || labelElement != null)
        {
          DocumentTreeNode parent1 = node.Parent;
          int index2 = node.Index;
          DocumentControl documentControl = DocumentMenuHelper.GetDocumentControl((DocumentTreeNode) node);
          bool flag = false;
          if (documentControl != null && documentControl.ActiveElement == node)
            flag = true;
          PageData page = node.Page;
          int index3;
          if (page != null && (fromPage > (index3 = page.Index) || fromPage == -1))
          {
            fromPage = index3;
            if (imDocumentData == null)
            {
              imDocumentData = page.OwnerDocument;
              imDocumentData?.SuspendUpdateGeometryRefreshUI();
            }
          }
          if (labelElement != null)
            labelElement.ConvertToContainer();
          else
            textBoxElement?.ConvertToContainer();
          PageElementUI pageUi = (node as IPageElementWithInterface).PageUI;
          DocumentTreeNode parent2 = parent1;
          RectangleElement rectElement = node;
          DocumentControl doc = documentControl;
          int num = flag ? 1 : 0;
          DocumentMenuHelper.ChangeSelection(index2, parent2, (DocumentTreeNode) rectElement, doc, num != 0);
        }
      }
      if (fromPage == -1)
        fromPage = 0;
      if (imDocumentData == null)
        return;
      imDocumentData.ResumeUpdateRefreshUI(false, false);
      imDocumentData.UpdateLayout(fromPage, false, true);
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Обработчик пункта меню "Обновить таблицу"</summary>
  protected static void UpadateTable_Execute(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (context == null || context.Length == 0)
      return;
    docControl?.UndoManager?.LockUndo();
    try
    {
      ImDocumentData imDocumentData = (ImDocumentData) null;
      int fromPage = -1;
      for (int index1 = 0; index1 < context.Length; ++index1)
      {
        PageData pageData = context[index1] is PageElementNode pageElementNode ? pageElementNode.Page : context[index1] as PageData;
        if (pageData != null)
        {
          int index2 = pageData.Index;
          if (index2 < fromPage || fromPage == -1)
            fromPage = index2;
        }
        if (imDocumentData == null && context[index1] is IDocumentElement documentElement)
          imDocumentData = documentElement.OwnerDocument;
        context[index1].SetNeedUpdateLayoutFlag(true, true, false, false, true);
      }
      if (fromPage < 0)
        fromPage = 0;
      imDocumentData?.UpdateLayout(fromPage, true, true);
    }
    finally
    {
      docControl?.UndoManager?.UnlockUndo();
    }
  }

  /// <summary>Обработчик пункта меню "Вставить"</summary>
  private static void TablePaste_Execute(DocumentTreeNode[] context)
  {
    if (context == null || context.Length == 0 || !(context[0] is TableElement tableElement))
      return;
    PageData page = tableElement.Page;
    DocumentNodesClipboardData nodesFromClipboard = NodeClipboardHelper.GetNodesFromClipboard();
    for (int index = 0; index < nodesFromClipboard.Nodes.Length; ++index)
    {
      if (tableElement.CanAddChildElement(nodesFromClipboard.Nodes[index]))
        tableElement.ContextPaste(nodesFromClipboard.Nodes[index], false, false);
    }
    if (page == null || page.OwnerDocument == null)
      return;
    page.OwnerDocument.UpdateLayout(page.Index, false, true);
  }

  /// <summary>Вспомогательный метод. Найти документ который нужно блокировать.</summary>
  /// <returns>Документ который нужно блокировать</returns>
  internal static DocumentTreeNode FindDocumentForSuspend(DocumentTreeNode node)
  {
    while (node != null && node.IsVirtualNode)
      node = node.Parent;
    return node is IDocumentElement documentElement ? (DocumentTreeNode) documentElement.OwnerDocument : (DocumentTreeNode) null;
  }

  /// <summary>Вспомогательный метод поиска строки данных для преставления</summary>
  /// <param name="cell">Ячейка</param>
  /// <param name="viewRow">Строка представление</param>
  private static void FindParentRow(DocumentTreeNode cell, out RectangleElement viewRow)
  {
    viewRow = (RectangleElement) null;
    if (!(cell is RectangleElement rectangleElement))
      return;
    viewRow = rectangleElement.FindParentRow(true);
  }

  private static bool QueryStatus_RemoveRow(DocumentTreeNode context)
  {
    RectangleElement viewRow;
    DocumentMenuHelper.FindParentRow(context, out viewRow);
    return viewRow != null && viewRow.CanRemove() && !viewRow.ReadOnlyStructure;
  }

  private static bool QueryStatus_RemoveRow(DocumentTreeNode[] context)
  {
    bool flag = false;
    if (context != null)
    {
      DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(context, true);
      for (int index = 0; !flag && index < nodesWithoutChilds.Length; ++index)
        flag = DocumentMenuHelper.QueryStatus_RemoveRow(nodesWithoutChilds[index]);
    }
    return flag;
  }

  /// <summary>Обработчик пункта меню "Удалить строку"</summary>
  private static void RemoveRow_Execute(DocumentTreeNode[] context)
  {
    if (context == null || context.Length == 0)
      return;
    DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(context, true);
    ImDocumentData imDocumentData = (ImDocumentData) null;
    int fromPage = -1;
    VisualNode visualNode = context[0] as VisualNode;
    if (context[0] is PageElementNode pageElementNode && pageElementNode.InPlaceEditorActive)
      pageElementNode.DeactivateInPlaceEditor();
    IUndoManager undoManager = (IUndoManager) null;
    if (visualNode != null && visualNode.OwnerDocument != null && visualNode.OwnerDocument.UndoManager != null)
      undoManager = visualNode.OwnerDocument.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_568"));
    try
    {
      try
      {
        for (int index1 = 0; index1 < nodesWithoutChilds.Length; ++index1)
        {
          RectangleElement viewRow;
          DocumentMenuHelper.FindParentRow(nodesWithoutChilds[index1], out viewRow);
          if (viewRow != null)
          {
            PageData page = viewRow.Page;
            int index2;
            if (page != null && (fromPage > (index2 = page.Index) || fromPage == -1))
            {
              fromPage = index2;
              if (imDocumentData == null)
              {
                imDocumentData = page.OwnerDocument;
                if (imDocumentData != null)
                {
                  imDocumentData.SuspendUpdateGeometryRefreshUI();
                  imDocumentData.BeginChangingStructure();
                }
              }
            }
            viewRow.UniteTable();
            viewRow.Remove(false, false);
          }
        }
      }
      finally
      {
        if (imDocumentData != null)
        {
          imDocumentData.EndChangingStructure(false, false, false, false);
          imDocumentData.ResumeUpdateRefreshUI(false, false);
          if (fromPage == -1)
            fromPage = 0;
          imDocumentData.UpdateLayout(fromPage, false, true);
        }
      }
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  private static void FindParentColumn(
    DocumentTreeNode cell,
    out TableElement viewColumnOwner,
    out int viewColIndex)
  {
    viewColumnOwner = (TableElement) null;
    viewColIndex = -1;
    switch (cell)
    {
      case VirtualColumn virtualColumn:
        TableData parentCell1 = virtualColumn.ParentCell;
        if (parentCell1 == null)
          break;
        TableData paramsOwner1;
        parentCell1.GetGridColumnsParams(out paramsOwner1, out bool _, true, false);
        viewColumnOwner = paramsOwner1 as TableElement;
        viewColIndex = virtualColumn.ColumnIndex;
        break;
      case RectangleElement rectangleElement:
        TableData parentCell2 = rectangleElement.ParentCell;
        if (parentCell2 == null)
          break;
        TableData paramsOwner2;
        parentCell2.GetGridColumnsParams(out paramsOwner2, out bool _, true, false);
        viewColumnOwner = paramsOwner2 as TableElement;
        viewColIndex = rectangleElement.GetGridColumnIndex();
        break;
    }
  }

  private static bool QueryStatus_RemoveColumn(DocumentTreeNode context)
  {
    if (context == null)
      return false;
    if (context is TableElement && context.IsVirtualNode)
    {
      bool flag = false;
      for (int index = 0; !flag && index < context.Nodes.Count; ++index)
        flag |= DocumentMenuHelper.QueryStatus_RemoveColumn(context.Nodes[index]);
      return flag;
    }
    TableElement viewColumnOwner;
    int viewColIndex;
    DocumentMenuHelper.FindParentColumn(context, out viewColumnOwner, out viewColIndex);
    return viewColumnOwner != null && viewColIndex != -1 && !viewColumnOwner.ReadOnlyStructure;
  }

  private static bool QueryStatus_RemoveColumn(DocumentTreeNode[] context)
  {
    bool flag = false;
    if (context != null)
    {
      DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(context, true);
      for (int index = 0; !flag && index < nodesWithoutChilds.Length; ++index)
        flag = DocumentMenuHelper.QueryStatus_RemoveColumn(nodesWithoutChilds[index]);
    }
    return flag;
  }

  /// <summary>Обработчик пункта меню "Удалить столбец"</summary>
  private static void RemoveColumn_Execute(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (context == null || context.Length == 0)
      return;
    DocumentTreeNode documentTreeNode = context[0];
    IUndoManager undoManager = (IUndoManager) null;
    if (docControl.UndoManager != null)
      undoManager = docControl.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_568"));
    try
    {
      DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(context, true);
      bool flag = false;
      for (int index = 0; index < nodesWithoutChilds.Length; ++index)
      {
        if (nodesWithoutChilds[index].IsVirtualNode && nodesWithoutChilds[index] is TableElement)
        {
          if (nodesWithoutChilds.Length > 1)
            return;
          flag = true;
        }
      }
      ImDocumentData imDocumentData = (ImDocumentData) null;
      int fromPage = -1;
      if (flag)
      {
        TableElement tableElement = nodesWithoutChilds[0] as TableElement;
        if (tableElement.IsColumn)
        {
          if (tableElement.Nodes.Count > 0 && tableElement.Nodes[0] is TableElement node)
          {
            for (int index1 = 0; index1 < node.Nodes.Count; ++index1)
            {
              TableElement viewColumnOwner;
              int viewColIndex;
              DocumentMenuHelper.FindParentColumn(node.Nodes[index1], out viewColumnOwner, out viewColIndex);
              if (viewColumnOwner != null && viewColIndex != -1)
              {
                PageData page = viewColumnOwner.Page;
                int index2;
                if (page != null && (fromPage > (index2 = page.Index) || fromPage == -1))
                {
                  fromPage = index2;
                  imDocumentData = page.OwnerDocument;
                }
                viewColumnOwner.RemoveGridColumn(viewColIndex, false, false, false);
              }
            }
          }
        }
        else
        {
          for (int index3 = 0; index3 < tableElement.Nodes.Count; ++index3)
          {
            TableElement viewColumnOwner;
            int viewColIndex;
            DocumentMenuHelper.FindParentColumn(tableElement.Nodes[index3], out viewColumnOwner, out viewColIndex);
            if (viewColumnOwner != null && viewColIndex != -1)
            {
              PageData page = viewColumnOwner.Page;
              int index4;
              if (page != null && (fromPage > (index4 = page.Index) || fromPage == -1))
              {
                fromPage = index4;
                imDocumentData = page.OwnerDocument;
              }
              viewColumnOwner.RemoveGridColumn(viewColIndex, false, false, false);
            }
          }
        }
      }
      else
      {
        for (int index5 = 0; index5 < nodesWithoutChilds.Length; ++index5)
        {
          TableElement viewColumnOwner;
          int viewColIndex;
          DocumentMenuHelper.FindParentColumn(nodesWithoutChilds[index5], out viewColumnOwner, out viewColIndex);
          if (viewColumnOwner != null && viewColIndex != -1)
          {
            PageData page = viewColumnOwner.Page;
            int index6;
            if (page != null && (fromPage > (index6 = page.Index) || fromPage == -1))
            {
              fromPage = index6;
              imDocumentData = page.OwnerDocument;
            }
            viewColumnOwner.RemoveGridColumn(viewColIndex, false, false, false);
          }
        }
      }
      if (imDocumentData == null || fromPage == -1)
        return;
      imDocumentData.UpdateLayout(fromPage, false, true);
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  private static bool QueryStatus_RemoveCell(DocumentTreeNode context)
  {
    RectangleElement viewCell;
    DocumentMenuHelper.GetCellsForRemoveCell(context, out viewCell);
    return viewCell != null && viewCell.ParentCell != null && viewCell.OwnerSubTable != null && !viewCell.OwnerSubTable.ReadOnlyStructure;
  }

  private static bool QueryStatus_RemoveCell(IList<DocumentTreeNode> context)
  {
    bool flag = false;
    if (context != null)
    {
      List<DocumentTreeNode> nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(context, true);
      for (int index = 0; !flag && index < nodesWithoutChilds.Count; ++index)
        flag = DocumentMenuHelper.QueryStatus_RemoveCell(nodesWithoutChilds[index]);
    }
    return flag;
  }

  /// <summary>QueryStatus_FormatText</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public virtual bool QueryStatus_FormatText(DocumentTreeNode context)
  {
    if (context is TextData textData1)
      return !textData1.ReadOnlyFormating;
    if (context.NodesCount <= 0)
      return false;
    bool flag = true;
    for (int index1 = 0; flag && index1 < context.Nodes.Count; ++index1)
    {
      if (!(context is RectangleElement rectangleElement))
        return false;
      List<DocumentTreeNode> singleCells = rectangleElement.GetSingleCells();
      for (int index2 = 0; index2 < singleCells.Count; ++index2)
      {
        if (!(singleCells[index1] is TextData textData2))
          return false;
        flag = !textData2.ReadOnlyFormating;
        if (!flag)
          return false;
      }
    }
    return flag;
  }

  /// <summary>QueryStatus_FormatText</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public bool QueryStatus_FormatText(IList<DocumentTreeNode> context)
  {
    if (context == null || context.Count <= 0)
      return false;
    for (int index = 0; index < context.Count; ++index)
    {
      if (!this.QueryStatus_FormatText(context[index]))
        return false;
    }
    return true;
  }

  private static void GetCellsForRemoveCell(DocumentTreeNode context, out RectangleElement viewCell)
  {
    viewCell = context as RectangleElement;
  }

  public static void RemoveCell_Execute(
    DocumentTreeNode[] context,
    RemoveCellOptions options,
    bool removeEmptyParent,
    bool createUndo = true,
    bool updateLayout = true)
  {
    if (context == null || context.Length != 1)
      return;
    VisualNode visualNode = context[0] as VisualNode;
    IUndoManager undoManager = (IUndoManager) null;
    if (visualNode != null && visualNode.OwnerDocument != null && visualNode.OwnerDocument.UndoManager != null)
      undoManager = visualNode.OwnerDocument.UndoManager;
    if (undoManager != null & createUndo)
      undoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_568"));
    try
    {
      DocumentTreeNode[] documentTreeNodeArray = context;
      ImDocumentData imDocumentData = (ImDocumentData) null;
      int fromPage = -1;
      switch (options)
      {
        case RemoveCellOptions.MergeWithLeft:
          RectangleElement viewCell1;
          DocumentMenuHelper.GetCellsForRemoveCell(documentTreeNodeArray[0], out viewCell1);
          if (viewCell1 != null)
          {
            if (viewCell1.ParentCell != null && !viewCell1.ParentCell.IsFixedStructureArea && viewCell1.ParentCell.Nodes.Count > 0)
            {
              if (viewCell1.Index > 0)
              {
                if (viewCell1.ParentCell.Nodes[viewCell1.Index - 1] is RectangleElement node1)
                {
                  undoManager?.CreateUndo((DocumentTreeNode) node1, "GridPos");
                  int num1 = 1;
                  if (!viewCell1.IsDefaultGridPos)
                    num1 = viewCell1.GridPos.SpanCount;
                  int num2;
                  if (node1.IsDefaultGridPos)
                  {
                    num2 = num1 + 1;
                  }
                  else
                  {
                    int num3 = num1;
                    TableGridPosition gridPos = node1.GridPos;
                    int spanCount = gridPos != null ? gridPos.SpanCount : 0;
                    num2 = num3 + spanCount;
                  }
                  TableGridPosition oldValue = (TableGridPosition) null;
                  if (node1.GridPos != null)
                    oldValue = node1.GridPos.Clone();
                  if (num2 > 1 && node1.IsDefaultGridPos)
                    node1.GridPos = new TableGridPosition();
                  if (num2 > 1 || !node1.IsDefaultGridPos)
                    node1.GridPos?.SetCellSpan(num2);
                  undoManager?.CreateUndo((object) node1, "GridPos", (object) oldValue, (object) node1.GridPos?.Clone());
                  RectangleF bounds = node1.Bounds;
                  bounds.Width += viewCell1.Bounds.Width;
                  node1.SetCellSizes(bounds, false, true, false, true);
                  node1.WidthOverrided = true;
                }
              }
              else if (viewCell1.ParentCell.Nodes.Count > 1 && viewCell1.ParentCell.Nodes[viewCell1.Index + 1] is RectangleElement node2)
              {
                undoManager?.CreateUndo((DocumentTreeNode) node2, "GridPos");
                int num4 = 1;
                if (!viewCell1.IsDefaultGridPos)
                  num4 = viewCell1.GridPos.SpanCount;
                int num5;
                if (node2.IsDefaultGridPos)
                {
                  num5 = num4 + 1;
                }
                else
                {
                  int num6 = num4;
                  TableGridPosition gridPos = node2.GridPos;
                  int spanCount = gridPos != null ? gridPos.SpanCount : 0;
                  num5 = num6 + spanCount;
                }
                TableGridPosition oldValue = (TableGridPosition) null;
                if (node2.GridPos != null)
                  oldValue = node2.GridPos.Clone();
                if (num5 > 1 && node2.IsDefaultGridPos)
                  node2.GridPos = new TableGridPosition();
                if (num5 > 1 || !node2.IsDefaultGridPos)
                  node2.GridPos?.SetCellSpan(num5);
                if (undoManager != null && node2.GridPos != null)
                  undoManager.CreateUndo((object) node2, "GridPos", (object) oldValue, (object) node2.GridPos.Clone());
                RectangleF bounds = node2.Bounds;
                bounds.Width += viewCell1.Bounds.Width;
                node2.SetCellSizes(bounds, false, true, false, true);
                node2.WidthOverrided = true;
              }
            }
            PageData page = viewCell1.Page;
            int index;
            if (page != null && fromPage > (index = page.Index))
            {
              fromPage = index;
              imDocumentData = page.OwnerDocument;
            }
            TableData parentCell = viewCell1.ParentCell;
            viewCell1.UniteTable();
            viewCell1.Remove(false, false);
            if (removeEmptyParent && parentCell != null && parentCell.IsRow && parentCell.NodesCount == 0)
            {
              parentCell.UniteTable();
              parentCell.Remove(false, false);
              break;
            }
            break;
          }
          break;
        case RemoveCellOptions.MergeWithRight:
          RectangleElement viewCell2;
          DocumentMenuHelper.GetCellsForRemoveCell(documentTreeNodeArray[0], out viewCell2);
          if (viewCell2 != null)
          {
            if (viewCell2.ParentCell != null && !viewCell2.ParentCell.IsFixedStructureArea && viewCell2.ParentCell.Nodes.Count > 0 && viewCell2.Index < viewCell2.ParentCell.Nodes.Count - 1)
            {
              RectangleElement node = viewCell2.ParentCell.Nodes[viewCell2.Index + 1] as RectangleElement;
              if (undoManager != null & createUndo)
                undoManager.CreateUndo((DocumentTreeNode) node, "GridPos");
              int num7 = 1;
              if (!viewCell2.IsDefaultGridPos)
                num7 = viewCell2.GridPos.SpanCount;
              int num8;
              if (node.IsDefaultGridPos)
              {
                num8 = num7 + 1;
              }
              else
              {
                int num9 = num7;
                TableGridPosition gridPos = node.GridPos;
                int spanCount = gridPos != null ? gridPos.SpanCount : 0;
                num8 = num9 + spanCount;
              }
              TableGridPosition oldValue = (TableGridPosition) null;
              if (node.GridPos != null)
                oldValue = node.GridPos.Clone();
              if (num8 > 1 && node.IsDefaultGridPos)
                node.GridPos = new TableGridPosition();
              if (num8 > 1 || !node.IsDefaultGridPos)
                node.GridPos?.SetCellSpan(num8);
              if (((undoManager == null ? 0 : (node.GridPos != null ? 1 : 0)) & (createUndo ? 1 : 0)) != 0)
                undoManager.CreateUndo((object) node, "GridPos", (object) oldValue, (object) node.GridPos.Clone());
              RectangleF bounds = node.Bounds;
              bounds.Width += viewCell2.Bounds.Width;
              node.SetCellSizes(bounds, false, true, false, true);
              node.WidthOverrided = true;
            }
            PageData page = viewCell2.Page;
            int index;
            if (page != null && fromPage > (index = page.Index))
            {
              fromPage = index;
              imDocumentData = page.OwnerDocument;
            }
            TableData parentCell = viewCell2.ParentCell;
            viewCell2.UniteTable();
            viewCell2.Remove(false, false);
            if (removeEmptyParent && parentCell != null && parentCell.IsRow && parentCell.NodesCount == 0)
            {
              parentCell.UniteTable();
              parentCell.Remove(false, false);
              break;
            }
            break;
          }
          break;
      }
      if (((imDocumentData == null ? 0 : (fromPage != -1 ? 1 : 0)) & (updateLayout ? 1 : 0)) == 0)
        return;
      imDocumentData.UpdateLayout(fromPage, false, true);
    }
    finally
    {
      if (undoManager != null & createUndo)
        undoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Разбить ячейку"</summary>
  private static void SplitCell_Execute(DocumentTreeNode[] context)
  {
    if (context == null || context.Length != 1 || !(context[0] is RectangleElement rectangleElement))
      return;
    IUndoManager undoManager = (IUndoManager) null;
    if (rectangleElement.OwnerDocument?.UndoManager != null)
      undoManager = rectangleElement.OwnerDocument.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_573"));
    try
    {
      TableSize tableSize = SplitCellDlg.Execute(rectangleElement.ParentCell != null || rectangleElement is TableData);
      if (tableSize == null)
        return;
      PageData page = rectangleElement.Page;
      ImDocumentData imDocumentData = (ImDocumentData) null;
      if (page != null)
        imDocumentData = page.OwnerDocument;
      TableData tableData = (TableData) null;
      if (rectangleElement.TableOwner != null)
        tableData = rectangleElement.TableOwner.TopLevelTable;
      rectangleElement.SplitCell(tableSize.Rows, tableSize.Columns, ImDocumentEditorConfig.Instance.ShowDebugInfo, false, false);
      tableData?.SetNeedUpdateLayoutFlag(true, true, false, false, true);
      if (imDocumentData == null || page == null)
        return;
      imDocumentData.UpdateLayout(page.Index, false, true);
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Заблокировать автоматические обновления</summary>
  /// <param name="node">Узел документа</param>
  /// <param name="suspendDV">Блокировать обновление представлений данных</param>
  /// <param name="suspendUI">Блокировать обновление внешнего вида</param>
  /// <param name="suspendedDV">Узел для которого была запущена блокировка обновления представления</param>
  /// <param name="suspendedUI">Узел для которого была запущена блокировка обновления внешнего вида</param>
  public static void SuspendUpdates(
    DocumentTreeNode node,
    bool suspendDV,
    bool suspendUI,
    out DocumentTreeNode suspendedDV,
    out VisualNode suspendedUI)
  {
    suspendedDV = DocumentMenuHelper.FindDocumentForSuspend(node);
    suspendedUI = suspendedDV as VisualNode;
    if (suspendUI && suspendedUI != null && suspendedUI.SuspendedRefreshUIFlag && suspendedUI.SuspendedUpdateUIGeometryFlag)
      suspendedUI = (VisualNode) null;
    if (suspendDV && suspendedDV != null && suspendedDV.SuspendedUpdateLayoutFlag)
      suspendedDV = (DocumentTreeNode) null;
    if (suspendedDV != null)
      suspendedDV.SuspendUpdateLayout();
    if (suspendedUI == null)
      return;
    suspendedUI.SuspendUpdateGeometryRefreshUI();
  }

  /// <summary>Разблокировать обновления</summary>
  /// <param name="suspendedDV">Узел в котром заблокированны обновления представлений данных</param>
  /// <param name="updateDV">Вызвать обновления представлений данных</param>
  /// <param name="suspendedUI">Узел в котором заблокированны обновления внешнего вида</param>
  /// <param name="updateUI">Вызвать обновления внешнего вида</param>
  public static void ResumeUpdates(
    DocumentTreeNode suspendedDV,
    bool updateDV,
    VisualNode suspendedUI,
    bool updateUI)
  {
    suspendedDV?.ResumeUpdateLayout(updateDV, false);
    suspendedUI?.ResumeUpdateUIGeometry(updateUI, updateUI);
  }

  private static void MergeCells(TableElement table)
  {
    if (!table.IsVirtualNode)
      return;
    if (table.IsColumn)
    {
      for (int index = 0; index < table.Nodes.Count; ++index)
      {
        if (table.Nodes[index] is TableElement node)
          DocumentMenuHelper.MergeCells(node);
      }
    }
    else
    {
      RectangleElement rectangleElement = (RectangleElement) null;
      for (int index = 0; index < table.Nodes.Count; ++index)
      {
        RectangleElement node = table.Nodes[index] as RectangleElement;
        if (!node.IsSingleCell && node is TableElement table1)
          DocumentMenuHelper.MergeCells(table1);
        if (rectangleElement == null)
        {
          rectangleElement = node;
          if (rectangleElement.IsDefaultGridPos)
            rectangleElement.GridPos = new TableGridPosition();
        }
        else
        {
          rectangleElement.GridPos.AddCellSpan(node.GridPos.SpanCount);
          node.UniteTable();
          node.Remove(false, false);
        }
      }
    }
  }

  /// <summary>Команда контекстного меню "Объединить ячейки"</summary>
  private static void MergeCells_Execute(DocumentTreeNode[] context, DocumentControl docControl)
  {
  }

  /// <summary>Найти владельца строки которой принадлежит элемент.
  /// Если элемент преставление, то ищет и ее строку данных.</summary>
  /// <param name="context">Элемент в строке или строка</param>
  /// <param name="viewRowParent">Родитель строки представления</param>
  /// <param name="viewRowIndex">Индекс строки преставления в Nodes</param>
  private static void FindRowParent(
    DocumentTreeNode context,
    out TableElement viewRowParent,
    out int viewRowIndex)
  {
    viewRowParent = (TableElement) null;
    viewRowIndex = -1;
    if (!(context is RectangleElement rectangleElement))
      return;
    TableData rowParent;
    viewRowIndex = rectangleElement.GetRowIndex(out rowParent);
    viewRowParent = rowParent as TableElement;
  }

  /// <summary>Команда контекстного меню "Добавить строку"</summary>
  private static void AddRow_Execute(
    DocumentTreeNode[] context,
    bool above,
    DocumentControl docControl)
  {
    if (context == null || context.Length != 1)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_574"));
    try
    {
      TableElement viewRowParent;
      int viewRowIndex;
      DocumentMenuHelper.FindRowParent(context[0], out viewRowParent, out viewRowIndex);
      if (viewRowParent == null)
        return;
      RectangleElement rowModel = (RectangleElement) null;
      if (viewRowIndex == -1)
      {
        if (above)
        {
          viewRowIndex = 0;
          if (viewRowParent.Nodes.Count > 0)
            rowModel = viewRowParent.Nodes[viewRowIndex] as RectangleElement;
        }
        else
        {
          viewRowIndex = viewRowParent.Nodes.Count;
          if (viewRowParent.Nodes.Count > 0)
            rowModel = viewRowParent.Nodes[viewRowIndex - 1] as RectangleElement;
        }
      }
      else
      {
        rowModel = viewRowParent.Nodes[viewRowIndex] as RectangleElement;
        if (!above)
          ++viewRowIndex;
      }
      PageData page = viewRowParent.Page;
      ImDocumentData imDocumentData = (ImDocumentData) null;
      if (page != null)
        imDocumentData = page.OwnerDocument;
      viewRowParent.InsertNewRow(viewRowIndex, rowModel, false, false);
      if (imDocumentData == null || page == null)
        return;
      imDocumentData.UpdateLayout(page.Index, false, true);
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Добавить строку по шаблону..."</summary>
  ///  НАСТОЯЩИЙ
  private static void AddRowFromTemplate_Execute(
    DocumentTreeNode[] context,
    bool above,
    DocumentControl docControl)
  {
    if (context == null || context.Length != 1)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_574"));
    try
    {
      TableElement viewRowParent;
      int viewRowIndex;
      DocumentMenuHelper.FindRowParent(context[0], out viewRowParent, out viewRowIndex);
      AddTableRowFromTemplateDlg rowFromTemplateDlg = new AddTableRowFromTemplateDlg();
      if (viewRowParent == null)
        return;
      TableData structureTemplate = viewRowParent.GetTableStructureTemplate();
      if (structureTemplate == null)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_172"), LocalizationHolder.rm.GetString("Document.Model_173"));
      }
      rowFromTemplateDlg.Execute((DocumentTreeNode) structureTemplate);
      if (rowFromTemplateDlg.DialogResult != DialogResult.OK || rowFromTemplateDlg.RowList.SelectedItems.Count <= 0 || !(rowFromTemplateDlg.SelectedRow is RectangleElement selectedRow))
        return;
      DocumentTreeNode child = selectedRow.CloneFromTemplate(true, true);
      if (viewRowIndex == -1)
        viewRowIndex = !above ? viewRowParent.Nodes.Count : 0;
      else if (!above)
        ++viewRowIndex;
      PageData page = viewRowParent.Page;
      ImDocumentData imDocumentData = (ImDocumentData) null;
      if (page != null)
        imDocumentData = page.OwnerDocument;
      viewRowParent.InsertChildNode(viewRowIndex, child, false, true, false, false, false);
      if (imDocumentData == null || page == null)
        return;
      imDocumentData.UpdateLayout(page.Index, false, true);
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Добавить ОСНОВНУЮ СТРОКУ"</summary>
  private static void AddRowFromTemplate_ExecuteVB(
    DocumentTreeNode[] context,
    bool above,
    DocumentControl docControl)
  {
    if (context == null || context.Length != 1)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_574"));
    try
    {
      TableElement viewRowParent;
      int viewRowIndex;
      DocumentMenuHelper.FindRowParent(context[0], out viewRowParent, out viewRowIndex);
      int index = viewRowIndex + 1;
      if (viewRowParent == null)
        return;
      TableData structureTemplate = viewRowParent.GetTableStructureTemplate();
      if (structureTemplate == null)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_172"), LocalizationHolder.rm.GetString("Document.Model_173"));
      }
      DocumentTreeNode firstNodeByName = structureTemplate?.FindFirstNodeByName("Основная строка");
      if (firstNodeByName == null)
        return;
      DocumentTreeNode child = firstNodeByName.CloneFromTemplate(true, true);
      PageData page = viewRowParent.Page;
      ImDocumentData imDocumentData = (ImDocumentData) null;
      if (page != null)
        imDocumentData = page.OwnerDocument;
      viewRowParent.InsertChildNode(index, child, false, true, false, false, false);
      if (imDocumentData == null || page == null)
        return;
      imDocumentData.UpdateLayout(page.Index, false, true);
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Добавить раздел"</summary>
  private static void AddTableSection_Execute(
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    if (context == null || context.Length != 1)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_574"));
    try
    {
      if (!(context[0] is TableElement parent))
        return;
      TableElement tableElement = new TableElement(parent.IsColumn, (DocumentTreeNode) parent, RectangleElement.EmptyRectangleF, true);
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Добавить колонку"</summary>
  private static void AddColumn_Execute(
    DocumentTreeNode[] context,
    bool left,
    DocumentControl docControl)
  {
    if (context == null || context.Length != 1)
      return;
    docControl?.UndoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_574"));
    try
    {
      if (context[0] is VirtualColumn virtualColumn)
      {
        TableData parentCell = virtualColumn.ParentCell;
        if (parentCell == null || parentCell.ReadOnlyStructure || parentCell.GridColumnsParams == null)
          return;
        int num = virtualColumn.ColumnIndex;
        if (!left)
          ++num;
        if (num < 0)
          num = 0;
        PageData page = parentCell.Page;
        ImDocumentData imDocumentData = (ImDocumentData) null;
        if (page != null)
          imDocumentData = page.OwnerDocument;
        float size = TableData.DefaultCellSize.Width;
        if (parentCell.Page != null && (double) parentCell.Bounds.Right + (double) size > (double) parentCell.Page.Size.Width)
          size = (float) ((double) parentCell.Page.Size.Width - (double) parentCell.Bounds.Right - 1.0);
        RowColParams colParams = new RowColParams(parentCell, num, (string) null, size);
        parentCell.InsertNewGridColumn(num, colParams, true, true);
        if (imDocumentData == null || page == null)
          return;
        imDocumentData.UpdateLayout(page.Index, false, true);
      }
      else
      {
        if (!(context[0] is RectangleElement rectangleElement))
          return;
        TableData tableForAddColumn = rectangleElement.FindTableForAddColumn(true);
        if (tableForAddColumn == null || tableForAddColumn.ReadOnlyStructure)
          return;
        if (tableForAddColumn.IsFixedStructureArea)
        {
          RectangleF properBounds = tableForAddColumn.ProperBounds with
          {
            Location = PointF.Empty
          };
          if ((double) properBounds.Width > 10.0)
            properBounds.Width = 10f;
          TextBoxElement textBoxElement = new TextBoxElement((DocumentTreeNode) null, properBounds, true);
          tableForAddColumn.AddChildNode((DocumentTreeNode) textBoxElement, true, true);
          docControl?.SetSelection((DocumentTreeNode) textBoxElement, true, false);
        }
        else
        {
          int num1 = tableForAddColumn.GridColumnsParams != null ? tableForAddColumn.GridColumnsParams.Count - 1 : tableForAddColumn.Nodes.Count - 1;
          TableData parentCell = rectangleElement.ParentCell;
          int num2;
          if (parentCell != null && parentCell.IsRow)
          {
            num2 = rectangleElement.GetGridColumnIndex();
            if (!left)
              ++num2;
          }
          else
            num2 = !left ? num1 + 1 : 0;
          if (num2 < 0)
            num2 = 0;
          PageData page = tableForAddColumn.Page;
          ImDocumentData imDocumentData = (ImDocumentData) null;
          if (page != null)
            imDocumentData = page.OwnerDocument;
          float size = TableData.DefaultCellSize.Width;
          if (tableForAddColumn.Page != null && (double) tableForAddColumn.Bounds.Right + (double) size > (double) tableForAddColumn.Page.Size.Width)
            size = (float) ((double) tableForAddColumn.Page.Size.Width - (double) tableForAddColumn.Bounds.Right - 1.0);
          RowColParams colParams = new RowColParams(tableForAddColumn, num2, (string) null, size);
          tableForAddColumn.InsertNewGridColumn(num2, colParams, false, false);
          if (imDocumentData != null && page != null)
            imDocumentData.UpdateLayout(page.Index, true, true);
          else
            tableForAddColumn.UpdateLayout(true);
        }
      }
    }
    finally
    {
      docControl?.UndoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда контекстного меню "Преобразовать в заголовок"</summary>
  private static void ConvertToHeader_Execute(DocumentTreeNode[] context)
  {
    if (context == null || context.Length == 0)
      return;
    VisualNode visualNode = context[0] as VisualNode;
    IUndoManager undoManager = (IUndoManager) null;
    if (visualNode != null && visualNode.OwnerDocument != null && visualNode.OwnerDocument.UndoManager != null)
      undoManager = visualNode.OwnerDocument.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_571"));
    try
    {
      foreach (DocumentTreeNode nodesWithoutChild in DocumentTreeNode.GetNodesWithoutChilds(context, true))
      {
        if (nodesWithoutChild is TableElement tableElement)
          tableElement.ConvertToHeader(true);
      }
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  private static void ContainerLoadOle_Execute(ContainerElement context)
  {
    IUndoManager undoManager = (IUndoManager) null;
    if (context != null && context.OwnerDocument != null && context.OwnerDocument.UndoManager != null)
      undoManager = context.OwnerDocument.UndoManager;
    undoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_567"));
    try
    {
      if (context == null)
        return;
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      context.LoadDataObjectFromFile(openFileDialog.FileName);
      context.CheckOriginalSizeAndAskUser();
    }
    finally
    {
      undoManager?.EndCreateMultyUndo();
    }
  }

  private static void ContainerCreateOle_Execute(ContainerElement context)
  {
    context?.CreateOleObject();
  }

  private static void ContainerSaveImageToFile_Execute(ContainerElement context)
  {
    if (context == null || context.Image == null)
      return;
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.RestoreDirectory = true;
    string defaultExtension = ContainerElement.GetDefaultExtension(context.Image);
    if (defaultExtension != null && defaultExtension != "")
    {
      string[] strArray = defaultExtension.Split(';');
      string str = "";
      for (int index = 0; index < strArray.Length; ++index)
        str = $"{str}{(index > 0 ? "|" : "")}{strArray[index].Replace(".*", "")}|{strArray[index]}";
      saveFileDialog.Filter = str;
    }
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
    {
      ContainerData.SaveImageToStream(context.Image, (Stream) fileStream);
      fileStream.Close();
    }
  }

  /// <summary>Команда меню "Предыдущая страница"</summary>
  private static void PrevPage_Execute(DocumentControl docControl) => docControl?.GotoPrevPage();

  /// <summary>Команда меню "Следующая страница"</summary>
  private static void NextPage_Execute(DocumentControl docControl) => docControl?.GotoNextPage();

  /// <summary>Команда меню "Новая страница"</summary>
  /// <param name="docControl">DocumentControl для которого пришёл вызов</param>
  /// <param name="insertBefore">true - вставить перед текущей странице, false - после текущей</param>
  private static void NewPage_Execute(DocumentControl docControl, bool insertBefore)
  {
    if (docControl == null || docControl.Document == null)
      return;
    if (docControl.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_574"));
    try
    {
      if (docControl.Document.IsTemplate || docControl.Document.IsFormulaLib)
      {
        docControl.InsertNewPage(insertBefore);
      }
      else
      {
        Page page = (Page) SelectNodeDlg.Execute(typeof (Page), (DocumentTreeNode) null, docControl.Document.TemplateRoot, LocalizationHolder.rm.GetString("Document.Model_594"), 0);
        if (page == null)
          return;
        docControl.InsertNewPage(page.Id, insertBefore);
      }
    }
    finally
    {
      if (docControl.UndoManager != null)
        docControl.UndoManager.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда меню "Создать новую страницу"</summary>
  private static void CreateNextPageTemplate_Execute(
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    docControl?.UndoManager?.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_574"));
    try
    {
      Page basePage = (Page) null;
      if (NodeContextMenu.ContextMenuCommand && NodeContextMenu.ContextForContextMenu == context && context != null && context.Length == 1)
        basePage = context[0] as Page;
      if (basePage == null)
        basePage = docControl?.ActivePage;
      if (basePage == null)
        return;
      Page page = (Page) null;
      if (basePage.IsTemplate && basePage.OwnerDocument != null)
        page = basePage.OwnerDocument.CreateNextPageTemplate((PageData) basePage) as Page;
      if (page == null || docControl == null)
        return;
      docControl.ActivePage = page;
    }
    finally
    {
      docControl?.UndoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда меню "Удалить страницу"</summary>
  private static void RemovePage_Execute(DocumentControl docControl)
  {
    if (docControl?.UndoManager != null)
      docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_568"));
    try
    {
      if (docControl?.ActivePage == null || MessageBox.Show(LocalizationHolder.rm.GetString("Document.Model_529"), LocalizationHolder.rm.GetString("Document.Model_521"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
        return;
      docControl.ActivePage.UserCommand_Delete(true);
    }
    finally
    {
      docControl?.UndoManager?.EndCreateMultyUndo();
    }
  }

  /// <summary>Команда меню "Масштаб"</summary>
  private static void Zoom_Execute(DocumentControl docControl, DocZoomMode zoomMode, float scale)
  {
    if (docControl == null)
      return;
    double num = (double) docControl.SetZoom(zoomMode, scale);
  }

  /// <summary>Команда меню "Система координат"</summary>
  private static void CoorSystem_Execute(DocumentControl docControl, PageCoorSystem coorSystem)
  {
    if (coorSystem == PageCoorSystem.Custom)
    {
      DocumentControl.IsCoorSystemSelecting = true;
    }
    else
    {
      ImDocumentEditorConfig.Instance.AssignCoorSystem(coorSystem);
      DocumentControl.IsCoorSystemSelecting = false;
    }
    docControl.Refresh();
  }

  /// <summary>Команда меню "Размер сетки"</summary>
  private static void GridSize_Execute(DocumentControl docControl, float gridSize)
  {
    ImDocumentEditorConfig.Instance.AssignGridSize(gridSize);
    docControl.Refresh();
  }

  /// <summary>Команда меню "Добавить дополнительные страницы"</summary>
  /// <param name="docControl">DocumentControl для которого пришёл вызов</param>
  public static void InsertAdditionalPage(DocumentControl docControl)
  {
    string[] pageNumbers;
    if (docControl?.Document == null || docControl.Document.IsFormulaLib || PageNumberingDlg.ExecuteAdd(docControl.ActivePage.HierarchicalPageNumber, out pageNumbers) != DialogResult.OK)
      return;
    int length = pageNumbers != null ? pageNumbers.Length : 0;
    if (length <= 0)
      return;
    Page currentPage = docControl.ActivePage;
    for (int index = 0; index < length; ++index)
      currentPage = docControl.InsertAdditionalPageAfter((PageData) currentPage, pageNumbers[index], index == length - 1);
  }

  /// <summary>Команда меню "Удалить дополнительные страницы"</summary>
  /// <param name="docControl">DocumentControl для которого пришёл вызов</param>
  public static void RemoveAdditionalPage(DocumentControl docControl)
  {
    if (docControl?.Document == null)
      return;
    Page activePage = docControl.ActivePage;
    PageSelectionType pageSelectionType = PageSelectionType.None;
    if (PageNumberingDlg.ExecuteRemove(docControl.ActivePage.HierarchicalPageNumber, out pageSelectionType) != DialogResult.OK)
      return;
    if (pageSelectionType == PageSelectionType.ActivePage)
      docControl.RemovePage((Page) null, true, true);
    if (pageSelectionType == PageSelectionType.CurrentRange)
    {
      PageData pageData = (PageData) docControl.ActivePage;
      if (pageData.IsAdditionalPage)
      {
        for (PageData prevPage = pageData.PrevPage; prevPage != null && prevPage.IsAdditionalPage; prevPage = prevPage.PrevPage)
          pageData = prevPage;
      }
      else
        pageData = pageData.NextPage;
      bool flag;
      for (; pageData != null && pageData.IsAdditionalPage; pageData = (PageData) docControl.RemovePage((Page) pageData, flag, flag))
      {
        PageData nextPage = pageData.NextPage;
        flag = nextPage == null || nextPage.IsNextToAdditionalPage;
      }
    }
    if (pageSelectionType != PageSelectionType.All)
      return;
    page = (Page) null;
    int index = 0;
    while (index < docControl.Document.NodesCount && (!(docControl.Document.Nodes[index] is Page page) || page.IsTitlePage))
      ++index;
    bool flag1 = false;
    bool flag2 = false;
    while (page != null && !page.IsTitlePage && !flag1)
    {
      flag1 = page.IsLastPage;
      page = !page.IsAdditionalPage ? (Page) page.NextPage : docControl.RemovePage(page, flag2, flag2);
      flag2 = ((int) page?.NextPage?.IsNextToAdditionalPage ?? 1) != 0;
    }
  }

  /// <summary>Команда меню "Удалить дополнительные страницы"</summary>
  /// <param name="docControl">DocumentControl для которого пришёл вызов</param>
  public static void ChangeAdditionalPageNumberingStyle(DocumentControl docControl)
  {
    if (docControl?.Document == null)
      return;
    PageNumExtensionStyle numberingStyle = PageNumberingHelper.GetNumberingStyle(docControl.ActivePage.HierarchicalPageNumber);
    if (numberingStyle == PageNumExtensionStyle.None)
      return;
    PageNumExtensionStyle numExtensionStyle = numberingStyle;
    if (PageNumberingDlg.ExecuteChangeStyle(ref numberingStyle) != DialogResult.OK || numExtensionStyle == numberingStyle)
      return;
    PageData pageData = (PageData) docControl.ActivePage;
    if (pageData.IsAdditionalPage)
    {
      for (PageData prevPage = pageData.PrevPage; prevPage != null && prevPage.IsAdditionalPage; prevPage = prevPage.PrevPage)
        pageData = prevPage;
    }
    else
      pageData = pageData.NextPage;
    PageNumBuilder pageNumBuilder = PageNumBuilder.Parse(PageNumberingHelper.ChangeNumberingStyle(pageData.HierarchicalPageNumber));
    while (pageData != null && pageData.IsAdditionalPage)
    {
      pageData.HierarchicalPageNumber = pageNumBuilder.ToString();
      pageData = pageData.NextPage;
      pageNumBuilder.IncrementExtension();
    }
  }

  /// <summary>Добавить новую кнопку на панель инструментов</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="toolBar">Панель инструментов</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns>Кнопка</returns>
  public ButtonItemBase AddNewButton(
    string commandName,
    Intermech.Bars.ToolBar toolBar,
    ICommandManager commandManager)
  {
    if (toolBar == null)
      throw new ArgumentNullException(nameof (toolBar));
    if (commandManager == null)
      throw new ArgumentNullException(nameof (commandManager));
    MenuButtonItem menuItem1 = DocumentMenuHelper.GetMenuItem(commandName);
    if (commandManager.FindCommand(commandName) == null)
      return (ButtonItemBase) null;
    ButtonItemBase buttonItemBase = menuItem1 == null || !menuItem1.HasChildren ? (ButtonItemBase) new ButtonItem() : (ButtonItemBase) new DropDownMenuItem();
    if (menuItem1 != null)
    {
      buttonItemBase.Image = menuItem1.Image;
      if (!(buttonItemBase is DropDownMenuItem))
        buttonItemBase.Text = menuItem1.Text;
      else
        buttonItemBase.Text = string.Empty;
      buttonItemBase.Locked = menuItem1.Locked;
      buttonItemBase.ToolTipText = menuItem1.ToolTipText;
      buttonItemBase.BeginGroup = menuItem1.BeginGroup;
      if (menuItem1.HasChildren)
      {
        DropDownMenuItem dropDownMenuItem = buttonItemBase as DropDownMenuItem;
        foreach (MenuButtonItem menuItem2 in (CollectionBase) menuItem1.Items)
        {
          MenuButtonItem buttonClone = this.CreateButtonClone(menuItem2, commandManager);
          dropDownMenuItem.Items.Add((ToolbarItemBase) buttonClone);
        }
      }
    }
    buttonItemBase.CommandName = commandName;
    toolBar.Items.Add((ToolbarItemBase) buttonItemBase);
    commandManager.Add(buttonItemBase);
    if (buttonItemBase.Image == null && buttonItemBase is ButtonItem)
      (buttonItemBase as ButtonItem).ShowText = true;
    return buttonItemBase;
  }

  private MenuButtonItem CreateButtonClone(MenuButtonItem menuItem, ICommandManager manager)
  {
    MenuButtonItem buttonClone = new MenuButtonItem();
    buttonClone.Shortcut = menuItem.Shortcut;
    foreach (MenuItemBase menuItem1 in (CollectionBase) menuItem.Items)
      buttonClone.Items.Add((ToolbarItemBase) this.CreateButtonClone((MenuButtonItem) menuItem1, manager));
    buttonClone.Checked = menuItem.Checked;
    if (menuItem.Icon != null)
      buttonClone.Icon = (Icon) menuItem.Icon.Clone();
    buttonClone.IconSize = menuItem.IconSize;
    if (menuItem.Image != null)
      buttonClone.Image = (Image) menuItem.Image.Clone();
    buttonClone.ImageIndex = menuItem.ImageIndex;
    buttonClone.BeginGroup = menuItem.BeginGroup;
    buttonClone.Enabled = menuItem.Enabled;
    buttonClone.Importance = menuItem.Importance;
    buttonClone.Padding.Left = menuItem.Padding.Left;
    buttonClone.Padding.Top = menuItem.Padding.Top;
    buttonClone.Padding.Right = menuItem.Padding.Right;
    buttonClone.Padding.Bottom = menuItem.Padding.Bottom;
    buttonClone.Tag = menuItem.Tag;
    buttonClone.Text = menuItem.Text;
    buttonClone.Font = menuItem.Font;
    buttonClone.ForeColor = menuItem.ForeColor;
    buttonClone.CommandName = menuItem.CommandName;
    buttonClone.ToolTipText = menuItem.ToolTipText;
    buttonClone.Visible = menuItem.Visible;
    buttonClone.Stretch = menuItem.Stretch;
    manager.Add((ButtonItemBase) buttonClone);
    return buttonClone;
  }

  /// <summary>Добавить новую кнопку настройки выравнивания текста в ячейке</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="toolBar">Панель инструментов</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <param name="itemsPerLine">Колличество команд в одной строке</param>
  /// <returns>Кнопка</returns>
  public static IconicMenu AddNewIconicDropDownButton(
    string commandName,
    Intermech.Bars.ToolBar toolBar,
    ICommandManager commandManager,
    int itemsPerLine)
  {
    MenuButtonItem menuItem = DocumentMenuHelper.GetMenuItem(commandName);
    IconicMenu iconicMenu = new IconicMenu(itemsPerLine);
    if (menuItem != null)
    {
      iconicMenu.Image = menuItem.Image;
      iconicMenu.Text = string.Empty;
      iconicMenu.ToolTipText = menuItem.ToolTipText;
      iconicMenu.BeginGroup = menuItem.BeginGroup;
      iconicMenu.Locked = menuItem.Locked;
    }
    iconicMenu.CommandName = commandName;
    toolBar.Items.Add((ToolbarItemBase) iconicMenu);
    commandManager?.Add((ButtonItemBase) iconicMenu);
    return iconicMenu;
  }

  /// <summary>Добавить новую кнопку выбора цвета на панель инструментов</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="toolBar">Панель инструментов</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns>Кнопка</returns>
  public static ColorMenu AddNewColorButton(
    string commandName,
    Intermech.Bars.ToolBar toolBar,
    ICommandManager commandManager)
  {
    MenuButtonItem menuItem = DocumentMenuHelper.GetMenuItem(commandName);
    ColorMenu colorMenu = new ColorMenu();
    if (menuItem != null)
    {
      colorMenu.Image = menuItem.Image;
      colorMenu.Text = string.Empty;
      colorMenu.ToolTipText = menuItem.ToolTipText;
      colorMenu.BeginGroup = menuItem.BeginGroup;
      colorMenu.Locked = menuItem.Locked;
    }
    colorMenu.CommandName = commandName;
    toolBar.Items.Add((ToolbarItemBase) colorMenu);
    commandManager?.Add((ButtonItemBase) colorMenu);
    return colorMenu;
  }

  public static Image CreateColorIcon(Color color)
  {
    int num = 16 /*0x10*/;
    Bitmap colorIcon = new Bitmap(num, num);
    Graphics graphics = Graphics.FromImage((Image) colorIcon);
    using (SolidBrush solidBrush = new SolidBrush(color))
      graphics.FillRectangle((Brush) solidBrush, new Rectangle(0, 0, num, num));
    graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 0, num, num));
    return (Image) colorIcon;
  }

  public void CreateColorMenu(ColorMenu colorMenu)
  {
    ColorMenuItem colorMenuItem1 = new ColorMenuItem();
    colorMenuItem1.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_273");
    colorMenuItem1.Text = colorMenuItem1.ToolTipText;
    colorMenuItem1.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(0));
    colorMenuItem1.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem1.Color);
    colorMenuItem1.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem1);
    ColorMenuItem colorMenuItem2 = new ColorMenuItem();
    colorMenuItem2.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_274");
    colorMenuItem2.Text = colorMenuItem2.ToolTipText;
    colorMenuItem2.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10040064));
    colorMenuItem2.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem2.Color);
    colorMenuItem2.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem2);
    ColorMenuItem colorMenuItem3 = new ColorMenuItem();
    colorMenuItem3.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_275");
    colorMenuItem3.Text = colorMenuItem3.ToolTipText;
    colorMenuItem3.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3355392));
    colorMenuItem3.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem3.Color);
    colorMenuItem3.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem3);
    ColorMenuItem colorMenuItem4 = new ColorMenuItem();
    colorMenuItem4.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_276");
    colorMenuItem4.Text = colorMenuItem4.ToolTipText;
    colorMenuItem4.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13056));
    colorMenuItem4.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem4.Color);
    colorMenuItem4.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem4);
    ColorMenuItem colorMenuItem5 = new ColorMenuItem();
    colorMenuItem5.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_277");
    colorMenuItem5.Text = colorMenuItem5.ToolTipText;
    colorMenuItem5.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13158));
    colorMenuItem5.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem5.Color);
    colorMenuItem5.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem5);
    ColorMenuItem colorMenuItem6 = new ColorMenuItem();
    colorMenuItem6.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_278");
    colorMenuItem6.Text = colorMenuItem6.ToolTipText;
    colorMenuItem6.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(128 /*0x80*/));
    colorMenuItem6.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem6.Color);
    colorMenuItem6.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem6);
    ColorMenuItem colorMenuItem7 = new ColorMenuItem();
    colorMenuItem7.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_279");
    colorMenuItem7.Text = colorMenuItem7.ToolTipText;
    colorMenuItem7.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3355545));
    colorMenuItem7.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem7.Color);
    colorMenuItem7.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem7);
    ColorMenuItem colorMenuItem8 = new ColorMenuItem();
    colorMenuItem8.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_280");
    colorMenuItem8.Text = colorMenuItem8.ToolTipText;
    colorMenuItem8.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3355443 /*0x333333*/));
    colorMenuItem8.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem8.Color);
    colorMenuItem8.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem8);
    ColorMenuItem colorMenuItem9 = new ColorMenuItem();
    colorMenuItem9.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_281");
    colorMenuItem9.Text = colorMenuItem9.ToolTipText;
    colorMenuItem9.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8388608 /*0x800000*/));
    colorMenuItem9.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem9.Color);
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem9);
    ColorMenuItem colorMenuItem10 = new ColorMenuItem();
    colorMenuItem10.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_282");
    colorMenuItem10.Text = colorMenuItem10.ToolTipText;
    colorMenuItem10.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16737792));
    colorMenuItem10.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem10.Color);
    colorMenuItem10.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem10);
    ColorMenuItem colorMenuItem11 = new ColorMenuItem();
    colorMenuItem11.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_283");
    colorMenuItem11.Text = colorMenuItem11.ToolTipText;
    colorMenuItem11.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8421376 /*0x808000*/));
    colorMenuItem11.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem11.Color);
    colorMenuItem11.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem11);
    ColorMenuItem colorMenuItem12 = new ColorMenuItem();
    colorMenuItem12.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_284");
    colorMenuItem12.Text = colorMenuItem12.ToolTipText;
    colorMenuItem12.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(32768 /*0x8000*/));
    colorMenuItem12.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem12.Color);
    colorMenuItem12.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem12);
    ColorMenuItem colorMenuItem13 = new ColorMenuItem();
    colorMenuItem13.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_285");
    colorMenuItem13.Text = colorMenuItem13.ToolTipText;
    colorMenuItem13.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(32896));
    colorMenuItem13.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem13.Color);
    colorMenuItem13.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem13);
    ColorMenuItem colorMenuItem14 = new ColorMenuItem();
    colorMenuItem14.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_286");
    colorMenuItem14.Text = colorMenuItem14.ToolTipText;
    colorMenuItem14.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb((int) byte.MaxValue));
    colorMenuItem14.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem14.Color);
    colorMenuItem14.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem14);
    ColorMenuItem colorMenuItem15 = new ColorMenuItem();
    colorMenuItem15.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_287");
    colorMenuItem15.Text = colorMenuItem15.ToolTipText;
    colorMenuItem15.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(6710937));
    colorMenuItem15.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem15.Color);
    colorMenuItem15.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem15);
    ColorMenuItem colorMenuItem16 = new ColorMenuItem();
    colorMenuItem16.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_288");
    colorMenuItem16.Text = colorMenuItem16.ToolTipText;
    colorMenuItem16.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8421504 /*0x808080*/));
    colorMenuItem16.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem16.Color);
    colorMenuItem16.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem16);
    ColorMenuItem colorMenuItem17 = new ColorMenuItem();
    colorMenuItem17.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_289");
    colorMenuItem17.Text = colorMenuItem17.ToolTipText;
    colorMenuItem17.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16711680 /*0xFF0000*/));
    colorMenuItem17.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem17.Color);
    colorMenuItem17.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem17);
    ColorMenuItem colorMenuItem18 = new ColorMenuItem();
    colorMenuItem18.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_290");
    colorMenuItem18.Text = colorMenuItem18.ToolTipText;
    colorMenuItem18.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16750848));
    colorMenuItem18.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem18.Color);
    colorMenuItem18.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem18);
    ColorMenuItem colorMenuItem19 = new ColorMenuItem();
    colorMenuItem19.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_291");
    colorMenuItem19.Text = colorMenuItem19.ToolTipText;
    colorMenuItem19.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10079232));
    colorMenuItem19.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem19.Color);
    colorMenuItem19.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem19);
    ColorMenuItem colorMenuItem20 = new ColorMenuItem();
    colorMenuItem20.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_292");
    colorMenuItem20.Text = colorMenuItem20.ToolTipText;
    colorMenuItem20.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3381606));
    colorMenuItem20.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem20.Color);
    colorMenuItem20.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem20);
    ColorMenuItem colorMenuItem21 = new ColorMenuItem();
    colorMenuItem21.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_293");
    colorMenuItem21.Text = colorMenuItem21.ToolTipText;
    colorMenuItem21.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3394764));
    colorMenuItem21.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem21.Color);
    colorMenuItem21.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem21);
    ColorMenuItem colorMenuItem22 = new ColorMenuItem();
    colorMenuItem22.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_294");
    colorMenuItem22.Text = colorMenuItem22.ToolTipText;
    colorMenuItem22.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3368703));
    colorMenuItem22.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem22.Color);
    colorMenuItem22.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem22);
    ColorMenuItem colorMenuItem23 = new ColorMenuItem();
    colorMenuItem23.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_295");
    colorMenuItem23.Text = colorMenuItem23.ToolTipText;
    colorMenuItem23.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8388736 /*0x800080*/));
    colorMenuItem23.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem23.Color);
    colorMenuItem23.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem23);
    ColorMenuItem colorMenuItem24 = new ColorMenuItem();
    colorMenuItem24.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_296");
    colorMenuItem24.Text = colorMenuItem24.ToolTipText;
    colorMenuItem24.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10066329 /*0x999999*/));
    colorMenuItem24.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem24.Color);
    colorMenuItem24.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem24);
    ColorMenuItem colorMenuItem25 = new ColorMenuItem();
    colorMenuItem25.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_297");
    colorMenuItem25.Text = colorMenuItem25.ToolTipText;
    colorMenuItem25.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16711935));
    colorMenuItem25.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem25.Color);
    colorMenuItem25.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem25);
    ColorMenuItem colorMenuItem26 = new ColorMenuItem();
    colorMenuItem26.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_298");
    colorMenuItem26.Text = colorMenuItem26.ToolTipText;
    colorMenuItem26.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16763904));
    colorMenuItem26.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem26.Color);
    colorMenuItem26.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem26);
    ColorMenuItem colorMenuItem27 = new ColorMenuItem();
    colorMenuItem27.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_299");
    colorMenuItem27.Text = colorMenuItem27.ToolTipText;
    colorMenuItem27.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16776960));
    colorMenuItem27.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem27.Color);
    colorMenuItem27.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem27);
    ColorMenuItem colorMenuItem28 = new ColorMenuItem();
    colorMenuItem28.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_300");
    colorMenuItem28.Text = colorMenuItem28.ToolTipText;
    colorMenuItem28.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(65280));
    colorMenuItem28.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem28.Color);
    colorMenuItem28.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem28);
    ColorMenuItem colorMenuItem29 = new ColorMenuItem();
    colorMenuItem29.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_301");
    colorMenuItem29.Text = colorMenuItem29.ToolTipText;
    colorMenuItem29.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb((int) ushort.MaxValue));
    colorMenuItem29.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem29.Color);
    colorMenuItem29.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem29);
    ColorMenuItem colorMenuItem30 = new ColorMenuItem();
    colorMenuItem30.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_302");
    colorMenuItem30.Text = colorMenuItem30.ToolTipText;
    colorMenuItem30.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(52479));
    colorMenuItem30.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem30.Color);
    colorMenuItem30.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem30);
    ColorMenuItem colorMenuItem31 = new ColorMenuItem();
    colorMenuItem31.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_303");
    colorMenuItem31.Text = colorMenuItem31.ToolTipText;
    colorMenuItem31.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10040166));
    colorMenuItem31.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem31.Color);
    colorMenuItem31.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem31);
    ColorMenuItem colorMenuItem32 = new ColorMenuItem();
    colorMenuItem32.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_304");
    colorMenuItem32.Text = colorMenuItem32.ToolTipText;
    colorMenuItem32.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(12632256 /*0xC0C0C0*/));
    colorMenuItem32.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem32.Color);
    colorMenuItem32.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem32);
    ColorMenuItem colorMenuItem33 = new ColorMenuItem();
    colorMenuItem33.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_305");
    colorMenuItem33.Text = colorMenuItem33.ToolTipText;
    colorMenuItem33.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16751052));
    colorMenuItem33.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem33.Color);
    colorMenuItem33.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem33);
    ColorMenuItem colorMenuItem34 = new ColorMenuItem();
    colorMenuItem34.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_306");
    colorMenuItem34.Text = colorMenuItem34.ToolTipText;
    colorMenuItem34.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16764057));
    colorMenuItem34.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem34.Color);
    colorMenuItem34.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem34);
    ColorMenuItem colorMenuItem35 = new ColorMenuItem();
    colorMenuItem35.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_307");
    colorMenuItem35.Text = colorMenuItem35.ToolTipText;
    colorMenuItem35.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16777113));
    colorMenuItem35.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem35.Color);
    colorMenuItem35.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem35);
    ColorMenuItem colorMenuItem36 = new ColorMenuItem();
    colorMenuItem36.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_308");
    colorMenuItem36.Text = colorMenuItem36.ToolTipText;
    colorMenuItem36.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13434828));
    colorMenuItem36.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem36.Color);
    colorMenuItem36.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem36);
    ColorMenuItem colorMenuItem37 = new ColorMenuItem();
    colorMenuItem37.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_309");
    colorMenuItem37.Text = colorMenuItem37.ToolTipText;
    colorMenuItem37.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13434879));
    colorMenuItem37.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem37.Color);
    colorMenuItem37.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem37);
    ColorMenuItem colorMenuItem38 = new ColorMenuItem();
    colorMenuItem38.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_310");
    colorMenuItem38.Text = colorMenuItem38.ToolTipText;
    colorMenuItem38.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10079487));
    colorMenuItem38.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem38.Color);
    colorMenuItem38.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem38);
    ColorMenuItem colorMenuItem39 = new ColorMenuItem();
    colorMenuItem39.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_311");
    colorMenuItem39.Text = colorMenuItem39.ToolTipText;
    colorMenuItem39.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13408767));
    colorMenuItem39.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem39.Color);
    colorMenuItem39.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem39);
    ColorMenuItem colorMenuItem40 = new ColorMenuItem();
    colorMenuItem40.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_312");
    colorMenuItem40.Text = colorMenuItem40.ToolTipText;
    colorMenuItem40.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16777215 /*0xFFFFFF*/));
    colorMenuItem40.Image = DocumentMenuHelper.CreateColorIcon(colorMenuItem40.Color);
    colorMenuItem40.Tag = (object) colorMenu;
    colorMenu.Items.Add((ToolbarItemBase) colorMenuItem40);
    foreach (ButtonItemBase buttonItemBase in (CollectionBase) colorMenu.Items)
      buttonItemBase.Click += new EventHandler(this.textColorChanged);
    TextMenuItem textMenuItem = new TextMenuItem();
    textMenuItem.Text = LocalizationHolder.rm.GetString("Document.Model_313");
    textMenuItem.Tag = (object) colorMenu;
    textMenuItem.Click += new EventHandler(this.selectTextColorMenuItem_Click);
    colorMenu.Items.Add((ToolbarItemBase) textMenuItem);
  }

  /// <summary>Создать панель инструментов "Навигация"</summary>
  /// <param name="imageList">Список иконок</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns>Панель инструментов "Таблицы"</returns>
  public Intermech.Bars.ToolBar CreateNavigatorToolBar(
    ImageList imageList,
    ICommandManager commandManager)
  {
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Guid = DocumentMenuHelper.NavigatorToolBarGuid;
    toolBar.ImageList = imageList;
    toolBar.Text = LocalizationHolder.rm.GetString("Document.Model_550");
    toolBar.Tearable = false;
    toolBar.DockLine = 1;
    this.AddNewButton("Navigation.FirstPage", toolBar, commandManager);
    this.AddNewButton("Navigation.PrevPage", toolBar, commandManager);
    if (this.CbPage == null)
      this.CbPage = new ComboBoxItem();
    this.CbPage.BeginGroup = false;
    this.CbPage.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_545");
    this.CbPage.CommandName = "Navigation.GoToPage";
    this.CbPage.MinimumControlWidth = 50;
    this.CbPage.ComboBox.DropDownStyle = ComboBoxStyle.DropDown;
    this.CbPage.ComboBox.DropDownHeight = 200;
    this.CbPage.ComboBox.TabStop = false;
    this.CbPage.ComboBox.Enabled = false;
    this.CbPage.ComboBox.SelectedIndexChanged += new EventHandler(this.ComboBoxPage_SelectedIndexChanged);
    this.CbPage.ComboBox.KeyDown += new KeyEventHandler(this.ComboBoxPage_KeyDown);
    this.CbPage.ComboBox.Validated += new EventHandler(this.ComboBoxPage_Validated);
    ButtonItem buttonItem1 = new ButtonItem();
    buttonItem1.CommandName = "Navigation.GoToPage";
    commandManager.Add((ButtonItemBase) buttonItem1);
    toolBar.Items.Add((ToolbarItemBase) this.CbPage);
    this.AddNewButton("Navigation.NextPage", toolBar, commandManager);
    this.AddNewButton("Navigation.LastPage", toolBar, commandManager);
    this.BPrevDocument = this.AddNewButton("Navigation.PrevDocument", toolBar, commandManager);
    this.BPrevDocument.Locked = true;
    if (this.CbDocument == null)
      this.CbDocument = new ComboBoxItem();
    this.CbDocument.BeginGroup = false;
    this.CbDocument.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_555");
    this.CbDocument.CommandName = "Navigation.GoToDocument";
    this.CbDocument.MinimumControlWidth = 250;
    this.CbDocument.ComboBox.DropDownWidth = 300;
    this.CbDocument.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.CbDocument.ComboBox.DropDownHeight = 200;
    this.CbDocument.ComboBox.TabStop = false;
    this.CbDocument.ComboBox.Enabled = false;
    this.CbDocument.ComboBox.SelectedIndexChanged += new EventHandler(this.ComboBoxDocument_SelectedIndexChanged);
    this.CbDocument.ComboBox.KeyDown += new KeyEventHandler(this.ComboBoxDocument_KeyDown);
    this.CbDocument.ComboBox.Validated += new EventHandler(this.ComboBoxDocument_Validated);
    this.CbDocument.ComboBox.DropDown += new EventHandler(this.ComboBoxDocument_DropDown);
    this.CbDocument.Locked = true;
    ButtonItem buttonItem2 = new ButtonItem();
    buttonItem2.CommandName = "Navigation.GoToDocument";
    commandManager.Add((ButtonItemBase) buttonItem2);
    toolBar.Items.Add((ToolbarItemBase) this.CbDocument);
    this.BNextDocument = this.AddNewButton("Navigation.NextDocument", toolBar, commandManager);
    this.BNextDocument.Locked = true;
    return toolBar;
  }

  /// <summary>Установить видимость кнопок навигации между документами комплекта</summary>
  /// <param name="visible"></param>
  public void SetVisibleDocumentButtons(bool visible)
  {
    if (this.CbDocument.Visible == visible)
      return;
    this.CbDocument.Visible = visible;
    this.CbDocument.Locked = !visible;
    this.BNextDocument.Visible = visible;
    this.BNextDocument.Locked = !visible;
    this.BPrevDocument.Visible = visible;
    this.BPrevDocument.Locked = !visible;
  }

  private void ComboBoxDocument_DropDown(object sender, EventArgs e)
  {
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentsComplect == null)
      return;
    System.Windows.Forms.ComboBox.ObjectCollection items = this.CbDocument.ComboBox.Items;
    items.Clear();
    foreach (DocumentTreeNode allDocument in DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentsComplect.GetAllDocuments())
      items.Add((object) allDocument);
    this.CbDocument.ComboBox.SelectedItem = (object) DocumentMenuHelper.ActiveImDocumentEditorFormBase.Document;
  }

  private void ComboBoxDocument_Validated(object sender, EventArgs e)
  {
    if (this.CbDocument.ComboBox.Items.Contains((object) this.CbDocument.ComboBox.Text))
    {
      this.CbDocument.ComboBox.SelectedIndex = this.CbDocument.ComboBox.Items.IndexOf((object) this.CbDocument.ComboBox.Text);
    }
    else
    {
      if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentsComplect == null)
        return;
      if (DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.Document != null)
        this.CbDocument.ComboBox.SelectedItem = (object) DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.Document;
      else
        this.CbDocument.ComboBox.SelectedIndex = -1;
    }
  }

  private void ComboBoxDocument_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return)
      return;
    this.CbDocument.ToolBar.Focus();
  }

  private void ComboBoxDocument_SelectedIndexChanged(object sender, EventArgs e)
  {
    ImDocument selectedItem = this.CbDocument.ComboBox.SelectedItem as ImDocument;
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentsComplect == null || selectedItem == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.Document == selectedItem)
      return;
    DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.SetDocument(DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentsComplect.GetAllDocuments()[this.CbDocument.ComboBox.SelectedIndex] as ImDocument, false, false);
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.Document.NodesCount > 0)
      DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.ActivePage = DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.Document.Nodes[0] as Page;
    else
      DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.ActivePage = (Page) null;
  }

  private void ComboBoxPage_Validated(object sender, EventArgs e)
  {
    if (this.CbPage.ComboBox.Items.Contains((object) this.CbPage.ComboBox.Text))
    {
      this.CbPage.ComboBox.SelectedIndex = this.CbPage.ComboBox.Items.IndexOf((object) this.CbPage.ComboBox.Text);
    }
    else
    {
      if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.Document == null)
        return;
      if (!string.IsNullOrEmpty(this.CbPage.ComboBox.Text) && this.CbPage.ComboBox.Text[0] == ':')
      {
        DocumentTreeNode node = DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.Document.FindNode(this.CbPage.ComboBox.Text.Substring(1, this.CbPage.ComboBox.Text.Length - 1));
        if (node != null)
        {
          DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.SetSelection(node, true, Point.Empty, true, true);
          return;
        }
      }
      int num = -1;
      if (DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.ActivePage != null)
        num = DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.ActivePage.Index;
      if (this.CbPage.ComboBox.Items.Count <= num)
        return;
      this.CbPage.ComboBox.SelectedIndex = num;
    }
  }

  private void ComboBoxPage_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return)
      return;
    this.CbPage.ToolBar.Focus();
  }

  /// <summary>Создать панель инструментов "Таблицы"</summary>
  /// <param name="imageList">Список иконок</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns>Панель инструментов "Таблицы"</returns>
  public Intermech.Bars.ToolBar CreateTableToolBar(
    ImageList imageList,
    ICommandManager commandManager)
  {
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Guid = DocumentMenuHelper.TableToolBarGuid;
    toolBar.ImageList = imageList;
    toolBar.Text = LocalizationHolder.rm.GetString("Document.Model_174");
    toolBar.Tearable = false;
    toolBar.DockLine = 0;
    toolBar.DockOffset = 2;
    this.AddNewButton("AddTableRowAbove", toolBar, commandManager);
    this.AddNewButton("AddTableRowBelow", toolBar, commandManager);
    this.AddNewButton("AddTableColumnLeft", toolBar, commandManager);
    this.AddNewButton("AddTableColumnRight", toolBar, commandManager);
    this.AddNewButton("AddTableCell", toolBar, commandManager);
    this.AddNewButton("SplitCell", toolBar, commandManager);
    this.AddNewButton("RemoveRow", toolBar, commandManager);
    this.AddNewButton("RemoveColumn", toolBar, commandManager);
    this.AddNewButton("RemoveCell", toolBar, commandManager);
    this.CbLineStyle = new ComboBoxItem();
    this.CbLineStyle.BeginGroup = true;
    this.CbLineStyle.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_539");
    this.CbLineStyle.CommandName = "Format.Borders.Style";
    ICollection standardValues = new EnumCustomConverter(typeof (BorderStyles)).GetStandardValues();
    if (standardValues != null)
    {
      ArrayList arrayList = new ArrayList(standardValues);
      arrayList.Remove((object) null);
      object[] items = new object[arrayList.Count];
      arrayList.CopyTo((Array) items);
      this.CbLineStyle.ComboBox.Items.AddRange(items);
    }
    this.CbLineStyle.MinimumControlWidth = 150;
    this.CbLineStyle.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.CbLineStyle.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.CbLineStyle.ComboBox.DrawItem += new DrawItemEventHandler(DocumentMenuHelper.ComboBox_DrawItem);
    this.CbLineStyle.ComboBox.TabStop = false;
    this.CbLineStyle.ComboBox.Enabled = false;
    this.CbLineStyle.ComboBox.SelectedIndex = 1;
    this.CbLineStyle.ComboBox.SelectedIndexChanged += new EventHandler(DocumentMenuHelper.ToolbarBorder_Changed);
    toolBar.Items.Add((ToolbarItemBase) this.CbLineStyle);
    this.CbLineWidth = new ComboBoxItem();
    this.CbLineWidth.CommandName = "Format.Borders.Width";
    this.CbLineWidth.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_540");
    this.CbLineWidth.ComboBox.Items.AddRange(new object[9]
    {
      (object) 0.0f,
      (object) 0.25f,
      (object) 0.5f,
      (object) 0.75f,
      (object) 1f,
      (object) 1.25f,
      (object) 1.5f,
      (object) 1.75f,
      (object) 2f
    });
    this.CbLineWidth.MinimumControlWidth = 65;
    this.CbLineWidth.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
    this.CbLineWidth.ComboBox.DrawItem += new DrawItemEventHandler(DocumentMenuHelper.CbLineStyle_DrawItem);
    this.CbLineWidth.ComboBox.TabStop = false;
    this.CbLineWidth.ComboBox.Enabled = false;
    this.CbLineWidth.ComboBox.SelectedIndex = 0;
    this.CbLineWidth.ComboBox.Width = 300;
    this.CbLineWidth.ComboBox.SelectedIndexChanged += new EventHandler(DocumentMenuHelper.ToolbarBorder_Changed);
    toolBar.Items.Add((ToolbarItemBase) this.CbLineWidth);
    this.linesColorMenu = DocumentMenuHelper.AddNewColorButton("Format.Borders.Color", toolBar, commandManager);
    this.CreateColorMenu(this.linesColorMenu);
    if (this.linesColorMenu.Items != null)
    {
      foreach (ButtonItemBase buttonItemBase in (CollectionBase) this.linesColorMenu.Items)
        buttonItemBase.Click += new EventHandler(DocumentMenuHelper.ToolbarBorder_Changed);
    }
    this.BordersToolButton = this.AddNewButton("Format.Borders", toolBar, commandManager);
    return toolBar;
  }

  private static void CbLineStyle_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (!(sender is System.Windows.Forms.ComboBox comboBox) || !(comboBox.Items[e.Index] is float))
      return;
    Rectangle rectangle = Rectangle.Round((RectangleF) e.Bounds);
    float num = (float) comboBox.Items[e.Index];
    float width1 = num;
    if ((double) width1 == 0.0)
      width1 = PageElementNode.DefaultLineWidth;
    e.DrawBackground();
    Pen pen = new Pen(Color.Black, width1);
    pen.DashStyle = DashStyle.Solid;
    int width2 = 30;
    pen.Color = e.ForeColor;
    GraphicsUnit pageUnit = e.Graphics.PageUnit;
    try
    {
      e.Graphics.PageUnit = GraphicsUnit.Millimeter;
      PointF dpi = new PointF(e.Graphics.DpiX, e.Graphics.DpiY);
      RectangleF mm1 = UnitsConverter.PixelsToMm(new Rectangle(rectangle.X, rectangle.Y, width2, rectangle.Height / 2), dpi);
      e.Graphics.DrawString(num.ToString(), comboBox.Font, Brushes.Black, mm1, StringFormat.GenericTypographic);
      PointF mm2 = UnitsConverter.PixelsToMm(new Point(rectangle.X + width2, rectangle.Y + rectangle.Height / 2), dpi);
      PointF mm3 = UnitsConverter.PixelsToMm(new Point(rectangle.Right - 1, rectangle.Y + rectangle.Height / 2), dpi);
      e.Graphics.DrawLine(pen, mm2, mm3);
    }
    finally
    {
      e.Graphics.PageUnit = pageUnit;
      pen.Dispose();
    }
    if ((e.State & DrawItemState.Selected) == DrawItemState.None)
      return;
    e.DrawFocusRectangle();
  }

  private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (!(sender is System.Windows.Forms.ComboBox comboBox) || !(comboBox.Items[e.Index] is BorderStyles))
      return;
    Rectangle rectangle = Rectangle.Round((RectangleF) e.Bounds);
    BorderStyles style = (BorderStyles) comboBox.Items[e.Index];
    if (comboBox.Enabled)
      e.DrawBackground();
    Pen pen = new BorderLine(style).GetPen();
    if (pen != null)
    {
      pen.Color = e.ForeColor;
      GraphicsUnit pageUnit = e.Graphics.PageUnit;
      try
      {
        pen = (Pen) pen.Clone();
        e.Graphics.PageUnit = GraphicsUnit.Millimeter;
        PointF dpi = new PointF(e.Graphics.DpiX, e.Graphics.DpiY);
        PointF mm = UnitsConverter.PixelsToMm(new Point(rectangle.X, rectangle.Y + rectangle.Height / 2), dpi);
        PointF pt2 = style != BorderStyles.Serif ? UnitsConverter.PixelsToMm(new Point(rectangle.Right - 1, rectangle.Y + rectangle.Height / 2), dpi) : UnitsConverter.PixelsToMm(new Point(7, rectangle.Y + rectangle.Height / 2), dpi);
        e.Graphics.DrawLine(pen, mm, pt2);
      }
      finally
      {
        e.Graphics.PageUnit = pageUnit;
        pen?.Dispose();
      }
    }
    else
    {
      string enumDescription = EnumCustomConverter.GetEnumDescription((Enum) style);
      using (Brush brush = (Brush) new SolidBrush(e.ForeColor))
        e.Graphics.DrawString(enumDescription, e.Font, brush, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    }
    if ((e.State & DrawItemState.Selected) == DrawItemState.None)
      return;
    e.DrawFocusRectangle();
  }

  /// <summary>Создать панель инструментов "Форматирование"</summary>
  /// <param name="imageList">Список иконок</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns>Панель инструментов "Форматирование"</returns>
  public Intermech.Bars.ToolBar CreateFormatToolBar(
    ImageList imageList,
    ICommandManager commandManager)
  {
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Guid = DocumentMenuHelper.FormatToolBarGuid;
    toolBar.ImageList = imageList;
    toolBar.Text = LocalizationHolder.rm.GetString("Document.Model_176");
    toolBar.Tearable = false;
    toolBar.DockLine = 0;
    toolBar.DockOffset = 1;
    toolBar.Flow = ToolBarLayout.Horizontal;
    this.AddNewButton("Format.Font.SetupFont", toolBar, commandManager);
    this.ChooseFontComboBoxToolbarItem = new ChooseFontComboBoxToolbarItem();
    this.ChooseFontComboBoxToolbarItem.CommandName = "Format.Font.FontFamily";
    this.ChooseFontComboBoxToolbarItem.MinimumControlWidth = 150;
    this.ChooseFontComboBoxToolbarItem.ComboBox.Font = new Font(this.ChooseFontComboBoxToolbarItem.ComboBox.Font.FontFamily, 8.25f);
    this.ChooseFontComboBoxToolbarItem.ComboBox.Height = 15;
    this.ChooseFontComboBoxToolbarItem.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_537");
    int num = this.ChooseFontComboBoxToolbarItem.ComboBox.Items.IndexOf((object) "Arial");
    if (num != -1)
      this.ChooseFontComboBoxToolbarItem.ComboBox.SelectedIndex = num;
    else if (this.ChooseFontComboBoxToolbarItem.ComboBox.Items.Count > 1)
      this.ChooseFontComboBoxToolbarItem.ComboBox.SelectedIndex = 1;
    this.ChooseFontComboBoxToolbarItem.Stretch = false;
    toolBar.Items.Add((ToolbarItemBase) this.ChooseFontComboBoxToolbarItem);
    this.ChooseFontComboBoxToolbarItem.ComboBox.SelectedIndexChanged += new EventHandler(DocumentMenuHelper.ComboBoxFontFamily_SelectedIndexChanged);
    this.ChooseFontComboBoxToolbarItem.Enabled = false;
    this.ChooseFontComboBoxToolbarItem.ComboBox.Enabled = false;
    this.ChooseFontComboBoxToolbarItem.ComboBox.BackColor = SystemColors.Control;
    this.CbFontSize = new ComboBoxItem();
    this.CbFontSize.CommandName = "Format.Font.FontSize";
    this.CbFontSize.ComboBox.TabStop = false;
    this.CbFontSize.Enabled = false;
    this.CbFontSize.ComboBox.Enabled = false;
    this.CbFontSize.ComboBox.BackColor = SystemColors.Control;
    this.CbFontSize.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_538");
    this.CbFontSize.Items.AddRange((object[]) new string[16 /*0x10*/]
    {
      "8",
      "9",
      "10",
      "11",
      "12",
      "14",
      "16",
      "18",
      "20",
      "22",
      "24",
      "26",
      "28",
      "36",
      "48",
      "72"
    });
    this.CbFontSize.ComboBox.SelectionLength = 0;
    this.CbFontSize.ComboBox.SelectedIndex = 2;
    this.OldFontSizeValue = (string) this.CbFontSize.Items[this.CbFontSize.ComboBox.SelectedIndex];
    this.CbFontSize.MinimumControlWidth = 65;
    toolBar.Items.Add((ToolbarItemBase) this.CbFontSize);
    this.CbFontSize.ComboBox.KeyDown += new KeyEventHandler(this.ComboBox_KeyDown);
    this.CbFontSize.ComboBox.Leave += new EventHandler(this.ComboBox_Leave);
    this.CbFontSize.ComboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this.AddNewButton("Format.Font.TextBold", toolBar, commandManager);
    this.AddNewButton("Format.Font.TextCursive", toolBar, commandManager);
    this.AddNewButton("Format.Font.TextUnderline", toolBar, commandManager);
    this.AddNewButton("Format.Font.Strikeout", toolBar, commandManager);
    this.AddNewButton("Format.Font.StrikeoutDouble", toolBar, commandManager);
    this.AddNewButton("Format.Font.Subscript", toolBar, commandManager);
    this.AddNewButton("Format.Font.Superscript", toolBar, commandManager);
    this.AddNewButton("Format.Font.Registr", toolBar, commandManager);
    this.AddNewButton("Format.TextAlignLeft", toolBar, commandManager);
    this.AddNewButton("Format.TextAlignCenter", toolBar, commandManager);
    this.AddNewButton("Format.TextAlignRight", toolBar, commandManager);
    this.AddNewButton("Format.TextAlignJustify", toolBar, commandManager);
    string str = "Intermech.Document.Model.Resources.";
    this._iconicMenu = DocumentMenuHelper.AddNewIconicDropDownButton("Format.CellAlign", toolBar, commandManager, 4);
    IconicMenuItem iconicMenuItem1 = new IconicMenuItem();
    DocumentMenuHelper.CaLeftTopImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Left-Top.png");
    iconicMenuItem1.Image = DocumentMenuHelper.CaLeftTopImage;
    iconicMenuItem1.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_179");
    iconicMenuItem1.Text = iconicMenuItem1.ToolTipText;
    iconicMenuItem1.Tag = (object) "Format.CellAlign.LeftTop";
    iconicMenuItem1.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem1);
    this.CaLeftTopIconicMenuItem = iconicMenuItem1;
    IconicMenuItem iconicMenuItem2 = new IconicMenuItem();
    DocumentMenuHelper.CaCenterTopImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Center-Top.png");
    iconicMenuItem2.Image = DocumentMenuHelper.CaCenterTopImage;
    iconicMenuItem2.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_180");
    iconicMenuItem2.Text = iconicMenuItem2.ToolTipText;
    iconicMenuItem2.Tag = (object) "Format.CellAlign.CenterTop";
    iconicMenuItem2.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem2);
    this.CaCenterTopIconicMenuItem = iconicMenuItem2;
    IconicMenuItem iconicMenuItem3 = new IconicMenuItem();
    DocumentMenuHelper.CaRightTopImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Right-Top.png");
    iconicMenuItem3.Image = DocumentMenuHelper.CaRightTopImage;
    iconicMenuItem3.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_181");
    iconicMenuItem3.Text = iconicMenuItem3.ToolTipText;
    iconicMenuItem3.Tag = (object) "Format.CellAlign.RightTop";
    iconicMenuItem3.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem3);
    this.CaRightTopIconicMenuItem = iconicMenuItem3;
    IconicMenuItem iconicMenuItem4 = new IconicMenuItem();
    DocumentMenuHelper.CaJustifyTopImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Justify-Top.png");
    iconicMenuItem4.Image = DocumentMenuHelper.CaJustifyTopImage;
    iconicMenuItem4.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_182");
    iconicMenuItem4.Text = iconicMenuItem4.ToolTipText;
    iconicMenuItem4.Tag = (object) "Format.CellAlign.JustifyTop";
    iconicMenuItem4.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem4);
    this.CaJustifyTopIconicMenuItem = iconicMenuItem4;
    IconicMenuItem iconicMenuItem5 = new IconicMenuItem();
    DocumentMenuHelper.CaLeftCenterImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Left-Center.png");
    iconicMenuItem5.Image = DocumentMenuHelper.CaLeftCenterImage;
    iconicMenuItem5.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_183");
    iconicMenuItem5.Text = iconicMenuItem5.ToolTipText;
    iconicMenuItem5.Tag = (object) "Format.CellAlign.LeftMiddle";
    iconicMenuItem5.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem5);
    this.CaLeftCenterIconicMenuItem = iconicMenuItem5;
    IconicMenuItem iconicMenuItem6 = new IconicMenuItem();
    DocumentMenuHelper.CaCenterCenterImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Center-Center.png");
    iconicMenuItem6.Image = DocumentMenuHelper.CaCenterCenterImage;
    iconicMenuItem6.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_184");
    iconicMenuItem6.Text = iconicMenuItem6.ToolTipText;
    iconicMenuItem6.Tag = (object) "Format.CellAlign.CenterMiddle";
    iconicMenuItem6.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem6);
    this.CaCenterCenterIconicMenuItem = iconicMenuItem6;
    IconicMenuItem iconicMenuItem7 = new IconicMenuItem();
    DocumentMenuHelper.CaRightCenterImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Right-Center.png");
    iconicMenuItem7.Image = DocumentMenuHelper.CaRightCenterImage;
    iconicMenuItem7.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_185");
    iconicMenuItem7.Text = iconicMenuItem7.ToolTipText;
    iconicMenuItem7.Tag = (object) "Format.CellAlign.RightMiddle";
    iconicMenuItem7.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem7);
    this.CaRightCenterIconicMenuItem = iconicMenuItem7;
    IconicMenuItem iconicMenuItem8 = new IconicMenuItem();
    DocumentMenuHelper.CaJustifyCenterImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Justify-Center.png");
    iconicMenuItem8.Image = DocumentMenuHelper.CaJustifyCenterImage;
    iconicMenuItem8.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_186");
    iconicMenuItem8.Text = iconicMenuItem8.ToolTipText;
    iconicMenuItem8.Tag = (object) "Format.CellAlign.JustifyMiddle";
    iconicMenuItem8.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem8);
    this.CaJustifyCenterIconicMenuItem = iconicMenuItem8;
    IconicMenuItem iconicMenuItem9 = new IconicMenuItem();
    DocumentMenuHelper.CaLeftBottomImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Left-Bottom.png");
    iconicMenuItem9.Image = DocumentMenuHelper.CaLeftBottomImage;
    iconicMenuItem9.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_187");
    iconicMenuItem9.Text = iconicMenuItem9.ToolTipText;
    iconicMenuItem9.Tag = (object) "Format.CellAlign.LeftBottom";
    iconicMenuItem9.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem9);
    this.CaLeftBottomIconicMenuItem = iconicMenuItem9;
    IconicMenuItem iconicMenuItem10 = new IconicMenuItem();
    DocumentMenuHelper.CaCenterBottomImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Center-Bottom.png");
    iconicMenuItem10.Image = DocumentMenuHelper.CaCenterBottomImage;
    iconicMenuItem10.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_188");
    iconicMenuItem10.Text = iconicMenuItem10.ToolTipText;
    iconicMenuItem10.Tag = (object) "Format.CellAlign.CenterBottom";
    iconicMenuItem10.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem10);
    this.CaCenterBottomIconicMenuItem = iconicMenuItem10;
    IconicMenuItem iconicMenuItem11 = new IconicMenuItem();
    DocumentMenuHelper.CaRightBottomImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Right-Bottom.png");
    iconicMenuItem11.Image = DocumentMenuHelper.CaRightBottomImage;
    iconicMenuItem11.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_189");
    iconicMenuItem11.Text = iconicMenuItem11.ToolTipText;
    iconicMenuItem11.Tag = (object) "Format.CellAlign.RightBottom";
    iconicMenuItem11.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem11);
    this.CaRightBottomIconicMenuItem = iconicMenuItem11;
    IconicMenuItem iconicMenuItem12 = new IconicMenuItem();
    DocumentMenuHelper.CaJustifyBottomImage = DocumentMenuHelper.LoadImageFromResurces(str + "CA-Justify-Bottom.png");
    iconicMenuItem12.Image = DocumentMenuHelper.CaJustifyBottomImage;
    iconicMenuItem12.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_190");
    iconicMenuItem12.Text = iconicMenuItem12.ToolTipText;
    iconicMenuItem12.Tag = (object) "Format.CellAlign.JustifyBottom";
    iconicMenuItem12.Click += new EventHandler(this.CellAlignButtonClick);
    this._iconicMenu.Items.Add((ToolbarItemBase) iconicMenuItem12);
    this.CaJustifyBottomIconicMenuItem = iconicMenuItem12;
    this.AddNewButton("Format.TextSpaceBetweenLines", toolBar, commandManager);
    this.AddNewButton("Format.SetupParagraph", toolBar, commandManager);
    this.AddNewButton("Format.SetupTextDirrection", toolBar, commandManager);
    this.AddNewButton("Format.NumberingList", toolBar, commandManager);
    this.AddNewButton("Format.BulletsList", toolBar, commandManager);
    this.AddNewButton("Format.DecreaseIdent", toolBar, commandManager);
    this.AddNewButton("Format.IncreaseIdent", toolBar, commandManager);
    DocumentMenuHelper.bgColorMenu = DocumentMenuHelper.AddNewColorButton("Format.BgColor", toolBar, commandManager);
    DocumentMenuHelper.noBgColorMenuItem = new TextMenuItem();
    DocumentMenuHelper.noBgColorMenuItem.Text = LocalizationHolder.rm.GetString("Document.Model_191");
    DocumentMenuHelper.noBgColorMenuItem.Click += new EventHandler(DocumentMenuHelper.clearBgColorColorMenuItem_Click);
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) DocumentMenuHelper.noBgColorMenuItem);
    ColorMenuItem colorMenuItem1 = new ColorMenuItem();
    colorMenuItem1.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_192");
    colorMenuItem1.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16777215 /*0xFFFFFF*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem1);
    ColorMenuItem colorMenuItem2 = new ColorMenuItem();
    colorMenuItem2.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_193");
    colorMenuItem2.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(15987699 /*0xF3F3F3*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem2);
    ColorMenuItem colorMenuItem3 = new ColorMenuItem();
    colorMenuItem3.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_194");
    colorMenuItem3.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(15132390 /*0xE6E6E6*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem3);
    ColorMenuItem colorMenuItem4 = new ColorMenuItem();
    colorMenuItem4.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_195");
    colorMenuItem4.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(14737632 /*0xE0E0E0*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem4);
    ColorMenuItem colorMenuItem5 = new ColorMenuItem();
    colorMenuItem5.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_196");
    colorMenuItem5.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(14277081 /*0xD9D9D9*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem5);
    ColorMenuItem colorMenuItem6 = new ColorMenuItem();
    colorMenuItem6.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_197");
    colorMenuItem6.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13421772 /*0xCCCCCC*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem6);
    ColorMenuItem colorMenuItem7 = new ColorMenuItem();
    colorMenuItem7.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_198");
    colorMenuItem7.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(12632256 /*0xC0C0C0*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem7);
    ColorMenuItem colorMenuItem8 = new ColorMenuItem();
    colorMenuItem8.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_199");
    colorMenuItem8.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(11776947 /*0xB3B3B3*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem8);
    ColorMenuItem colorMenuItem9 = new ColorMenuItem();
    colorMenuItem9.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_200");
    colorMenuItem9.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10921638 /*0xA6A6A6*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem9);
    ColorMenuItem colorMenuItem10 = new ColorMenuItem();
    colorMenuItem10.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_201");
    colorMenuItem10.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10526880 /*0xA0A0A0*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem10);
    ColorMenuItem colorMenuItem11 = new ColorMenuItem();
    colorMenuItem11.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_202");
    colorMenuItem11.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10066329 /*0x999999*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem11);
    ColorMenuItem colorMenuItem12 = new ColorMenuItem();
    colorMenuItem12.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_203");
    colorMenuItem12.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(9211020 /*0x8C8C8C*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem12);
    ColorMenuItem colorMenuItem13 = new ColorMenuItem();
    colorMenuItem13.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_204");
    colorMenuItem13.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8421504 /*0x808080*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem13);
    ColorMenuItem colorMenuItem14 = new ColorMenuItem();
    colorMenuItem14.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_205");
    colorMenuItem14.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(7566195 /*0x737373*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem14);
    ColorMenuItem colorMenuItem15 = new ColorMenuItem();
    colorMenuItem15.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_206");
    colorMenuItem15.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(6710886 /*0x666666*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem15);
    ColorMenuItem colorMenuItem16 = new ColorMenuItem();
    colorMenuItem16.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_207");
    colorMenuItem16.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(6316128 /*0x606060*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem16);
    ColorMenuItem colorMenuItem17 = new ColorMenuItem();
    colorMenuItem17.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_208");
    colorMenuItem17.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(5855577 /*0x595959*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem17);
    ColorMenuItem colorMenuItem18 = new ColorMenuItem();
    colorMenuItem18.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_209");
    colorMenuItem18.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(5000268 /*0x4C4C4C*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem18);
    ColorMenuItem colorMenuItem19 = new ColorMenuItem();
    colorMenuItem19.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_210");
    colorMenuItem19.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(4210752 /*0x404040*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem19);
    ColorMenuItem colorMenuItem20 = new ColorMenuItem();
    colorMenuItem20.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_211");
    colorMenuItem20.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3355443 /*0x333333*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem20);
    ColorMenuItem colorMenuItem21 = new ColorMenuItem();
    colorMenuItem21.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_212");
    colorMenuItem21.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(2500134 /*0x262626*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem21);
    ColorMenuItem colorMenuItem22 = new ColorMenuItem();
    colorMenuItem22.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_213");
    colorMenuItem22.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(2105376 /*0x202020*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem22);
    ColorMenuItem colorMenuItem23 = new ColorMenuItem();
    colorMenuItem23.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_214");
    colorMenuItem23.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(1644825 /*0x191919*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem23);
    ColorMenuItem colorMenuItem24 = new ColorMenuItem();
    colorMenuItem24.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_215");
    colorMenuItem24.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(789516));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem24);
    ColorMenuItem colorMenuItem25 = new ColorMenuItem();
    colorMenuItem25.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_216");
    colorMenuItem25.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(0));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem25);
    ColorMenuItem colorMenuItem26 = new ColorMenuItem();
    colorMenuItem26.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_217");
    colorMenuItem26.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10040064));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem26);
    ColorMenuItem colorMenuItem27 = new ColorMenuItem();
    colorMenuItem27.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_218");
    colorMenuItem27.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3355392));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem27);
    ColorMenuItem colorMenuItem28 = new ColorMenuItem();
    colorMenuItem28.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_219");
    colorMenuItem28.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13056));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem28);
    ColorMenuItem colorMenuItem29 = new ColorMenuItem();
    colorMenuItem29.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_220");
    colorMenuItem29.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13158));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem29);
    ColorMenuItem colorMenuItem30 = new ColorMenuItem();
    colorMenuItem30.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_221");
    colorMenuItem30.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(128 /*0x80*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem30);
    ColorMenuItem colorMenuItem31 = new ColorMenuItem();
    colorMenuItem31.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_222");
    colorMenuItem31.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3355545));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem31);
    ColorMenuItem colorMenuItem32 = new ColorMenuItem();
    colorMenuItem32.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_223");
    colorMenuItem32.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3355443 /*0x333333*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem32);
    ColorMenuItem colorMenuItem33 = new ColorMenuItem();
    colorMenuItem33.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_224");
    colorMenuItem33.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8388608 /*0x800000*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem33);
    ColorMenuItem colorMenuItem34 = new ColorMenuItem();
    colorMenuItem34.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_225");
    colorMenuItem34.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16737792));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem34);
    ColorMenuItem colorMenuItem35 = new ColorMenuItem();
    colorMenuItem35.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_226");
    colorMenuItem35.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8421376 /*0x808000*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem35);
    ColorMenuItem colorMenuItem36 = new ColorMenuItem();
    colorMenuItem36.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_227");
    colorMenuItem36.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(32768 /*0x8000*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem36);
    ColorMenuItem colorMenuItem37 = new ColorMenuItem();
    colorMenuItem37.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_228");
    colorMenuItem37.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(32896));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem37);
    ColorMenuItem colorMenuItem38 = new ColorMenuItem();
    colorMenuItem38.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_229");
    colorMenuItem38.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb((int) byte.MaxValue));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem38);
    ColorMenuItem colorMenuItem39 = new ColorMenuItem();
    colorMenuItem39.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_230");
    colorMenuItem39.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(6710937));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem39);
    ColorMenuItem colorMenuItem40 = new ColorMenuItem();
    colorMenuItem40.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_231");
    colorMenuItem40.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8421504 /*0x808080*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem40);
    ColorMenuItem colorMenuItem41 = new ColorMenuItem();
    colorMenuItem41.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_232");
    colorMenuItem41.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16711680 /*0xFF0000*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem41);
    ColorMenuItem colorMenuItem42 = new ColorMenuItem();
    colorMenuItem42.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_233");
    colorMenuItem42.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16750848));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem42);
    ColorMenuItem colorMenuItem43 = new ColorMenuItem();
    colorMenuItem43.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_234");
    colorMenuItem43.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10079232));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem43);
    ColorMenuItem colorMenuItem44 = new ColorMenuItem();
    colorMenuItem44.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_235");
    colorMenuItem44.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3381606));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem44);
    ColorMenuItem colorMenuItem45 = new ColorMenuItem();
    colorMenuItem45.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_236");
    colorMenuItem45.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3394764));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem45);
    ColorMenuItem colorMenuItem46 = new ColorMenuItem();
    colorMenuItem46.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_237");
    colorMenuItem46.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(3368703));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem46);
    ColorMenuItem colorMenuItem47 = new ColorMenuItem();
    colorMenuItem47.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_238");
    colorMenuItem47.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8388736 /*0x800080*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem47);
    ColorMenuItem colorMenuItem48 = new ColorMenuItem();
    colorMenuItem48.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_239");
    colorMenuItem48.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10066329 /*0x999999*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem48);
    ColorMenuItem colorMenuItem49 = new ColorMenuItem();
    colorMenuItem49.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_240");
    colorMenuItem49.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16711935));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem49);
    ColorMenuItem colorMenuItem50 = new ColorMenuItem();
    colorMenuItem50.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_241");
    colorMenuItem50.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16763904));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem50);
    ColorMenuItem colorMenuItem51 = new ColorMenuItem();
    colorMenuItem51.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_242");
    colorMenuItem51.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16776960));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem51);
    ColorMenuItem colorMenuItem52 = new ColorMenuItem();
    colorMenuItem52.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_243");
    colorMenuItem52.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(65280));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem52);
    ColorMenuItem colorMenuItem53 = new ColorMenuItem();
    colorMenuItem53.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_244");
    colorMenuItem53.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb((int) ushort.MaxValue));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem53);
    ColorMenuItem colorMenuItem54 = new ColorMenuItem();
    colorMenuItem54.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_245");
    colorMenuItem54.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(52479));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem54);
    ColorMenuItem colorMenuItem55 = new ColorMenuItem();
    colorMenuItem55.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_246");
    colorMenuItem55.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10040166));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem55);
    ColorMenuItem colorMenuItem56 = new ColorMenuItem();
    colorMenuItem56.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_247");
    colorMenuItem56.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(12632256 /*0xC0C0C0*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem56);
    ColorMenuItem colorMenuItem57 = new ColorMenuItem();
    colorMenuItem57.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_248");
    colorMenuItem57.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16751052));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem57);
    ColorMenuItem colorMenuItem58 = new ColorMenuItem();
    colorMenuItem58.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_249");
    colorMenuItem58.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16764057));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem58);
    ColorMenuItem colorMenuItem59 = new ColorMenuItem();
    colorMenuItem59.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_250");
    colorMenuItem59.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16777113));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem59);
    ColorMenuItem colorMenuItem60 = new ColorMenuItem();
    colorMenuItem60.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_251");
    colorMenuItem60.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13434828));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem60);
    ColorMenuItem colorMenuItem61 = new ColorMenuItem();
    colorMenuItem61.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_252");
    colorMenuItem61.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13434879));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem61);
    ColorMenuItem colorMenuItem62 = new ColorMenuItem();
    colorMenuItem62.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_253");
    colorMenuItem62.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(10079487));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem62);
    ColorMenuItem colorMenuItem63 = new ColorMenuItem();
    colorMenuItem63.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_254");
    colorMenuItem63.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(13408767));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem63);
    ColorMenuItem colorMenuItem64 = new ColorMenuItem();
    colorMenuItem64.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_255");
    colorMenuItem64.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16777215 /*0xFFFFFF*/));
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) colorMenuItem64);
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) DocumentMenuHelper.bgColorMenu.Items)
    {
      if (menuButtonItem is ColorMenuItem)
      {
        menuButtonItem.Text = menuButtonItem.ToolTipText;
        menuButtonItem.Image = DocumentMenuHelper.CreateColorIcon((menuButtonItem as ColorMenuItem).Color);
        menuButtonItem.Click += new EventHandler(DocumentMenuHelper.bgColorChanged);
      }
    }
    TextMenuItem textMenuItem = new TextMenuItem();
    textMenuItem.Text = LocalizationHolder.rm.GetString("Document.Model_256");
    textMenuItem.Click += new EventHandler(DocumentMenuHelper.selectBgColorColorMenuItem_Click);
    DocumentMenuHelper.bgColorMenu.Items.Add((ToolbarItemBase) textMenuItem);
    DocumentMenuHelper.BgColor = Color.Transparent;
    this.textBkColorMenu = DocumentMenuHelper.AddNewColorButton("Format.TextBkColor", toolBar, commandManager);
    this.textBkColorMenu.ITEMSPERLINE = 5;
    this.noTextBkColorMenuItem = new TextMenuItem();
    this.noTextBkColorMenuItem.Text = LocalizationHolder.rm.GetString("Document.Model_257");
    this.noTextBkColorMenuItem.Click += new EventHandler(this.clearSelectionColorMenuItem_Click);
    this.textBkColorMenu.Items.Add((ToolbarItemBase) this.noTextBkColorMenuItem);
    ColorMenuItem colorMenuItem65 = new ColorMenuItem();
    colorMenuItem65.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_258");
    colorMenuItem65.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16776960));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem65);
    ColorMenuItem colorMenuItem66 = new ColorMenuItem();
    colorMenuItem66.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_259");
    colorMenuItem66.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(65280));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem66);
    ColorMenuItem colorMenuItem67 = new ColorMenuItem();
    colorMenuItem67.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_260");
    colorMenuItem67.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb((int) ushort.MaxValue));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem67);
    ColorMenuItem colorMenuItem68 = new ColorMenuItem();
    colorMenuItem68.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_261");
    colorMenuItem68.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16711935));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem68);
    ColorMenuItem colorMenuItem69 = new ColorMenuItem();
    colorMenuItem69.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_262");
    colorMenuItem69.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb((int) byte.MaxValue));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem69);
    ColorMenuItem colorMenuItem70 = new ColorMenuItem();
    colorMenuItem70.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_263");
    colorMenuItem70.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(16711680 /*0xFF0000*/));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem70);
    ColorMenuItem colorMenuItem71 = new ColorMenuItem();
    colorMenuItem71.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_264");
    colorMenuItem71.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(128 /*0x80*/));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem71);
    ColorMenuItem colorMenuItem72 = new ColorMenuItem();
    colorMenuItem72.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_265");
    colorMenuItem72.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(32896));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem72);
    ColorMenuItem colorMenuItem73 = new ColorMenuItem();
    colorMenuItem73.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_266");
    colorMenuItem73.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(32768 /*0x8000*/));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem73);
    ColorMenuItem colorMenuItem74 = new ColorMenuItem();
    colorMenuItem74.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_267");
    colorMenuItem74.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8388736 /*0x800080*/));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem74);
    ColorMenuItem colorMenuItem75 = new ColorMenuItem();
    colorMenuItem75.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_268");
    colorMenuItem75.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8388608 /*0x800000*/));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem75);
    ColorMenuItem colorMenuItem76 = new ColorMenuItem();
    colorMenuItem76.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_269");
    colorMenuItem76.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8421376 /*0x808000*/));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem76);
    ColorMenuItem colorMenuItem77 = new ColorMenuItem();
    colorMenuItem77.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_270");
    colorMenuItem77.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(8421504 /*0x808080*/));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem77);
    ColorMenuItem colorMenuItem78 = new ColorMenuItem();
    colorMenuItem78.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_271");
    colorMenuItem78.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(12632256 /*0xC0C0C0*/));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem78);
    ColorMenuItem colorMenuItem79 = new ColorMenuItem();
    colorMenuItem79.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_272");
    colorMenuItem79.Color = Color.FromArgb((int) byte.MaxValue, Color.FromArgb(0));
    this.textBkColorMenu.Items.Add((ToolbarItemBase) colorMenuItem79);
    foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.textBkColorMenu.Items)
    {
      if (menuButtonItem is ColorMenuItem)
      {
        menuButtonItem.Text = menuButtonItem.ToolTipText;
        menuButtonItem.Image = DocumentMenuHelper.CreateColorIcon((menuButtonItem as ColorMenuItem).Color);
        menuButtonItem.Click += new EventHandler(this.selectionColorChanged);
      }
    }
    this.TextBkColor = Color.Transparent;
    this.textColorMenu = DocumentMenuHelper.AddNewColorButton("Format.TextColor", toolBar, commandManager);
    this.CreateColorMenu(this.textColorMenu);
    this.TextColor = Color.Black;
    return toolBar;
  }

  private void ComboBoxPage_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.Document == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.ActivePage == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.ActivePage.Index == this.CbPage.ComboBox.SelectedIndex || this.CbPage.ComboBox.SelectedIndex == -1 || DocumentMenuHelper.ActiveImDocumentEditorFormBase.Document.NodesCount <= this.CbPage.ComboBox.SelectedIndex)
      return;
    DocumentMenuHelper.ActiveImDocumentEditorFormBase.DocumentControl.SetActivePage(DocumentMenuHelper.ActiveImDocumentEditorFormBase.Document.Nodes[this.CbPage.ComboBox.SelectedIndex] as Page, true, true, true, false);
  }

  /// <summary>Вызывается когда изменился стиль отрисовываемой линии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void ToolbarBorder_Changed(object sender, EventArgs e)
  {
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null)
      return;
    DocumentMenuHelper.ActiveImDocumentEditorFormBase.UpdateBorberCommands();
  }

  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.RaiseActiveDocumentTextSizeChanged();
  }

  private void ComboBox_Leave(object sender, EventArgs e)
  {
    this.RaiseActiveDocumentTextSizeChanged();
  }

  private void RaiseActiveDocumentTextSizeChanged()
  {
    if (DocumentMenuHelper.IsTextSizeChangeEventsLocked())
      return;
    float newTextSize = 0.0f;
    if (!this.ValidateTextSize(ref newTextSize))
      return;
    DocumentMenuHelper.TextSizeChanged(newTextSize);
  }

  private void ComboBox_KeyDown(object sender, KeyEventArgs e)
  {
    if (DocumentMenuHelper.IsTextSizeChangeEventsLocked() || e.KeyCode != Keys.Return)
      return;
    float newTextSize = 0.0f;
    if (!this.ValidateTextSize(ref newTextSize))
      return;
    DocumentMenuHelper.TextSizeChanged(newTextSize);
  }

  private bool ValidateTextSize(ref float value)
  {
    string text = (string) null;
    if (this.CbFontSize == null || this.CbFontSize.ComboBox == null)
      return false;
    object obj = this.CbFontSize.ComboBox.SelectedItem == null ? (object) this.CbFontSize.ComboBox.Text : this.CbFontSize.ComboBox.SelectedItem;
    if (obj == null || !(obj is string) || obj.Equals((object) this.OldFontSizeValue))
      return false;
    string str = (string) obj;
    if (str == string.Empty || !float.TryParse(FloatConverter.CorrectDecimal(str), out value))
      text = LocalizationHolder.rm.GetString("Document.Model_314");
    else if ((double) value < 1.0 || (double) value > 1638.0)
      text = LocalizationHolder.rm.GetString("Document.Model_315");
    if (text != null)
    {
      DocumentMenuHelper.LockTextSizeChangeEvents();
      try
      {
        int num = (int) MessageBox.Show(text, LocalizationHolder.rm.GetString("Document.Model_316"));
        this.CbFontSize.ComboBox.Text = this.OldFontSizeValue;
        return false;
      }
      finally
      {
        DocumentMenuHelper.UnlockTextSizeChangeEvents();
      }
    }
    else
    {
      this.OldFontSizeValue = str;
      return true;
    }
  }

  private static void TextSizeChanged(float newTextSize)
  {
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null)
      return;
    DocumentMenuHelper.ActiveImDocumentEditorFormBase.TextSizeChanged(newTextSize);
  }

  /// <summary>IsTextSizeChangeEventsLocked</summary>
  /// <returns></returns>
  public static bool IsTextSizeChangeEventsLocked()
  {
    return DocumentMenuHelper._lockTextSizeChangeEventsCounter > 0;
  }

  /// <summary>LockTextSizeChangeEvents</summary>
  public static void LockTextSizeChangeEvents()
  {
    ++DocumentMenuHelper._lockTextSizeChangeEventsCounter;
  }

  /// <summary>UnlockTextSizeChangeEvents</summary>
  public static void UnlockTextSizeChangeEvents()
  {
    if (DocumentMenuHelper._lockTextSizeChangeEventsCounter <= 0)
      return;
    --DocumentMenuHelper._lockTextSizeChangeEventsCounter;
  }

  private static void ComboBoxFontFamily_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null)
      return;
    DocumentMenuHelper.ActiveImDocumentEditorFormBase.FontFamilyCBSelectedIndexChanged();
  }

  public static DocumentMenuHelper Instance
  {
    get => DocumentMenuHelper.instance;
    set => DocumentMenuHelper.instance = value;
  }

  /// <summary> Цвет фона </summary>
  public static Color BgColor
  {
    [DebuggerStepThrough] get => DocumentMenuHelper._bgColor;
    set
    {
      if (!(DocumentMenuHelper._bgColor != value))
        return;
      DocumentMenuHelper._bgColor = value;
      if (DocumentMenuHelper.bgColorMenu != null && DocumentMenuHelper.bgColorMenu.Image != null)
      {
        using (Graphics graphics = Graphics.FromImage(DocumentMenuHelper.bgColorMenu.Image))
        {
          using (Brush brush = (Brush) new SolidBrush(value == Color.Transparent ? Color.White : value))
            graphics.FillRectangle(brush, new Rectangle(0, 12, 16 /*0x10*/, 4));
        }
        ColorMenuItem colorMenuItem = (ColorMenuItem) null;
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) DocumentMenuHelper.bgColorMenu.Items)
        {
          if (menuButtonItem is ColorMenuItem)
          {
            if ((menuButtonItem as ColorMenuItem).Color == value && colorMenuItem == null)
            {
              colorMenuItem = menuButtonItem as ColorMenuItem;
              colorMenuItem.Checked = true;
            }
            else
              (menuButtonItem as ColorMenuItem).Checked = false;
          }
        }
        DocumentMenuHelper.bgColorMenu.ToolTipText = colorMenuItem != null ? $"{LocalizationHolder.rm.GetString("Document.Model_317")}{colorMenuItem.ToolTipText})" : (value == Color.Transparent ? LocalizationHolder.rm.GetString("Document.Model_318") : LocalizationHolder.rm.GetString("Document.Model_319"));
        DocumentMenuHelper.noBgColorMenuItem.Checked = value == Color.Transparent;
        DocumentMenuHelper.bgColorMenu.Invalidate();
      }
      if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null)
        return;
      DocumentMenuHelper.ActiveImDocumentEditorFormBase.BgColorChanged();
    }
  }

  private static void bgColorChanged(object sender, EventArgs e)
  {
    if (!(sender is ColorMenuItem colorMenuItem))
      return;
    DocumentMenuHelper.BgColor = colorMenuItem.Color;
  }

  private static void clearBgColorColorMenuItem_Click(object sender, EventArgs e)
  {
    DocumentMenuHelper.BgColor = Color.Transparent;
  }

  private void CellAlignButtonClick(object sender, EventArgs e)
  {
    if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null || !(sender is IconicMenuItem) || ((ToolbarItemBase) sender).Tag == null || !(((ToolbarItemBase) sender).Tag is string))
      return;
    DocumentMenuHelper.ActiveImDocumentEditorFormBase.SetCellAlign((string) ((ToolbarItemBase) sender).Tag);
    this.CaLeftTopIconicMenuItem.Checked = this.CaLeftTopIconicMenuItem == (IconicMenuItem) sender;
    this.CaCenterTopIconicMenuItem.Checked = this.CaCenterTopIconicMenuItem == (IconicMenuItem) sender;
    this.CaRightTopIconicMenuItem.Checked = this.CaRightTopIconicMenuItem == (IconicMenuItem) sender;
    this.CaJustifyTopIconicMenuItem.Checked = this.CaJustifyTopIconicMenuItem == (IconicMenuItem) sender;
    this.CaLeftCenterIconicMenuItem.Checked = this.CaLeftCenterIconicMenuItem == (IconicMenuItem) sender;
    this.CaCenterCenterIconicMenuItem.Checked = this.CaCenterCenterIconicMenuItem == (IconicMenuItem) sender;
    this.CaRightCenterIconicMenuItem.Checked = this.CaRightCenterIconicMenuItem == (IconicMenuItem) sender;
    this.CaJustifyCenterIconicMenuItem.Checked = this.CaJustifyCenterIconicMenuItem == (IconicMenuItem) sender;
    this.CaLeftBottomIconicMenuItem.Checked = this.CaLeftBottomIconicMenuItem == (IconicMenuItem) sender;
    this.CaCenterBottomIconicMenuItem.Checked = this.CaCenterBottomIconicMenuItem == (IconicMenuItem) sender;
    this.CaRightBottomIconicMenuItem.Checked = this.CaRightBottomIconicMenuItem == (IconicMenuItem) sender;
    this.CaJustifyBottomIconicMenuItem.Checked = this.CaJustifyBottomIconicMenuItem == (IconicMenuItem) sender;
    this._iconicMenu.Image = ((ButtonItemBase) sender).Image;
    this._iconicMenu.ToolTipText = ((ToolbarItemBase) sender).ToolTipText;
  }

  private static void selectBgColorColorMenuItem_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    DocumentMenuHelper.BgColor = colorDialog.Color;
  }

  /// <summary> Цвет выделения </summary>
  public Color TextBkColor
  {
    [DebuggerStepThrough] get => this._textBkColor;
    set
    {
      if (!(this._textBkColor != value))
        return;
      this._textBkColor = value;
      if (this.textBkColorMenu != null && this.textBkColorMenu.Image != null)
      {
        using (Graphics graphics = Graphics.FromImage(this.textBkColorMenu.Image))
        {
          using (Brush brush = (Brush) new SolidBrush(value == Color.Transparent ? Color.White : value))
            graphics.FillRectangle(brush, new Rectangle(0, 12, 16 /*0x10*/, 4));
        }
        ColorMenuItem colorMenuItem = (ColorMenuItem) null;
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.textBkColorMenu.Items)
        {
          if (menuButtonItem is ColorMenuItem)
          {
            if ((menuButtonItem as ColorMenuItem).Color == value)
            {
              colorMenuItem = menuButtonItem as ColorMenuItem;
              colorMenuItem.Checked = true;
            }
            else
              (menuButtonItem as ColorMenuItem).Checked = false;
          }
        }
        this.textBkColorMenu.ToolTipText = colorMenuItem != null ? $"{LocalizationHolder.rm.GetString("Document.Model_320")}{colorMenuItem.ToolTipText})" : (value == Color.Transparent ? LocalizationHolder.rm.GetString("Document.Model_321") : LocalizationHolder.rm.GetString("Document.Model_322"));
        this.noTextBkColorMenuItem.Checked = value == Color.Transparent;
        this.textBkColorMenu.Invalidate();
      }
      if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null)
        return;
      DocumentMenuHelper.ActiveImDocumentEditorFormBase.TextBkColorChanged();
    }
  }

  private void selectionColorChanged(object sender, EventArgs e)
  {
    if (!(sender is ColorMenuItem colorMenuItem))
      return;
    this.TextBkColor = colorMenuItem.Color;
  }

  private void clearSelectionColorMenuItem_Click(object sender, EventArgs e)
  {
    this.TextBkColor = Color.Transparent;
  }

  /// <summary> Цвет текста </summary>
  public Color LinesColor
  {
    [DebuggerStepThrough] get => this.linesColor;
    set
    {
      if (!(this.linesColor != value))
        return;
      this.linesColor = value;
      if (this.linesColorMenu == null || this.linesColorMenu.Image == null)
        return;
      using (Graphics graphics = Graphics.FromImage(this.linesColorMenu.Image))
      {
        using (Brush brush = (Brush) new SolidBrush(value))
          graphics.FillRectangle(brush, new Rectangle(0, 12, 16 /*0x10*/, 4));
      }
      ColorMenuItem colorMenuItem = (ColorMenuItem) null;
      foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.linesColorMenu.Items)
      {
        if (menuButtonItem is ColorMenuItem)
        {
          if ((menuButtonItem as ColorMenuItem).Color == value)
          {
            colorMenuItem = menuButtonItem as ColorMenuItem;
            colorMenuItem.Checked = true;
          }
          else
            (menuButtonItem as ColorMenuItem).Checked = false;
        }
      }
      this.linesColorMenu.ToolTipText = colorMenuItem != null ? $"{LocalizationHolder.rm.GetString("Document.Model_533")}{colorMenuItem.ToolTipText})" : LocalizationHolder.rm.GetString("Document.Model_532");
      this.linesColorMenu.Invalidate();
    }
  }

  /// <summary> Цвет текста </summary>
  public Color TextColor
  {
    [DebuggerStepThrough] get => this._textColor;
    set
    {
      if (!(this._textColor != value))
        return;
      this._textColor = value;
      if (this.textColorMenu != null && this.textColorMenu.Image != null)
      {
        using (Graphics graphics = Graphics.FromImage(this.textColorMenu.Image))
        {
          using (Brush brush = (Brush) new SolidBrush(value))
            graphics.FillRectangle(brush, new Rectangle(0, 12, 16 /*0x10*/, 4));
        }
        ColorMenuItem colorMenuItem = (ColorMenuItem) null;
        foreach (MenuButtonItem menuButtonItem in (CollectionBase) this.textColorMenu.Items)
        {
          if (menuButtonItem is ColorMenuItem)
          {
            if ((menuButtonItem as ColorMenuItem).Color == value)
            {
              colorMenuItem = menuButtonItem as ColorMenuItem;
              colorMenuItem.Checked = true;
            }
            else
              (menuButtonItem as ColorMenuItem).Checked = false;
          }
        }
        this.textColorMenu.ToolTipText = colorMenuItem != null ? $"{LocalizationHolder.rm.GetString("Document.Model_323")}{colorMenuItem.ToolTipText})" : LocalizationHolder.rm.GetString("Document.Model_324");
        this.textColorMenu.Invalidate();
      }
      if (DocumentMenuHelper.ActiveImDocumentEditorFormBase == null || DocumentMenuHelper.ActiveImDocumentEditorFormBase.MenuHelper != this)
        return;
      DocumentMenuHelper.ActiveImDocumentEditorFormBase.TextColorChanged();
    }
  }

  private void textColorChanged(object sender, EventArgs e)
  {
    if (!(sender is ColorMenuItem colorMenuItem))
      return;
    if (colorMenuItem.Tag == this.textColorMenu)
      this.TextColor = colorMenuItem.Color;
    if (colorMenuItem.Tag != this.linesColorMenu)
      return;
    this.LinesColor = colorMenuItem.Color;
  }

  private void selectTextColorMenuItem_Click(object sender, EventArgs e)
  {
    ColorDialog colorDialog = new ColorDialog();
    if (colorDialog.ShowDialog() != DialogResult.OK)
      return;
    if (sender is ToolbarItemBase && (sender as ToolbarItemBase).Tag == this.textColorMenu)
      this.TextColor = colorDialog.Color;
    if (!(sender is ToolbarItemBase) || (sender as ToolbarItemBase).Tag != this.linesColorMenu)
      return;
    this.LinesColor = colorDialog.Color;
  }

  /// <summary>Создать панель инструментов "Красный карандаш"</summary>
  /// <param name="imageList">Список иконок</param>
  /// <param name="commandManager">Менеджер команд</param>
  /// <returns>Панель инструментов "Красный карандаш"</returns>
  public Intermech.Bars.ToolBar CreateRedlineOnOffToolBar(
    ImageList imageList,
    ICommandManager commandManager)
  {
    Intermech.Bars.ToolBar toolBar = new Intermech.Bars.ToolBar();
    toolBar.Guid = DocumentMenuHelper.RedlineOnOffToolBarGuid;
    toolBar.ImageList = imageList;
    toolBar.Text = LocalizationHolder.rm.GetString("Document.Model_663");
    toolBar.Tearable = false;
    toolBar.DockLine = 1;
    toolBar.DockOffset = 0;
    ButtonItemBase buttonItemBase1 = this.AddNewButton("Redline.Edit", toolBar, commandManager);
    ButtonItemBase buttonItemBase2 = this.AddNewButton("Redline.CompleteEdit", toolBar, commandManager);
    buttonItemBase1.Visible = true;
    buttonItemBase2.Visible = false;
    return toolBar;
  }

  /// <summary>Выполнить команду меню</summary>
  /// <param name="commandState">Состояние команды</param>
  /// <param name="context">Контекст команды</param>
  /// <param name="docControl">Документ для которого нужно выполнить команду</param>
  /// <returns>true, если команда найдена</returns>
  public virtual bool Execute(
    ICommandState commandState,
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    IntPtr hWnd = IntPtr.Zero;
    switch (commandState.CommandName)
    {
      case "AddRowFromTemplateAbove":
        DocumentMenuHelper.AddRowFromTemplate_Execute(context, true, docControl);
        return true;
      case "AddRowFromTemplateBelow":
        DocumentMenuHelper.AddRowFromTemplate_Execute(context, false, docControl);
        return true;
      case "AddTableCell":
      case "AddTableColumnLeft":
        DocumentMenuHelper.AddColumn_Execute(context, true, docControl);
        return true;
      case "AddTableColumnRight":
        DocumentMenuHelper.AddColumn_Execute(context, false, docControl);
        return true;
      case "AddTableRowAbove":
        DocumentMenuHelper.AddRow_Execute(context, true, docControl);
        return true;
      case "AddTableRowBelow":
        DocumentMenuHelper.AddRow_Execute(context, false, docControl);
        return true;
      case "AddTableSection":
        DocumentMenuHelper.AddTableSection_Execute(context, docControl);
        return true;
      case "ApplyPreviousTable":
        if (context != null && context.Length == 1 && context[0] is TableData tableData && !tableData.IsVirtualNode && !tableData.ReadOnlyStructure && tableData.UsePreviousTableTemplates && !tableData.HasTemplate())
          tableData.ApplyPreviousTableTemplate(true, true);
        return true;
      case "BlockGeometryChanging":
        DocumentMenuHelper.BlockGeometryChanging_Execute(context, docControl);
        return true;
      case "BringToFront":
        if (context != null && context.Length == 1)
        {
          if (context[0] is RectangleElement rectangleElement1 && rectangleElement1.IsTableCell)
            DocumentMenuHelper.MoveToBegin(context, docControl);
          else
            DocumentMenuHelper.MoveToEnd(context, docControl);
        }
        return true;
      case "CallEditor":
        DocumentMenuHelper.CallEditor_Execute(context, docControl);
        return true;
      case "ChangeVisibility":
        bool flag = !(commandState.Text == LocalizationHolder.rm.GetString("Document.Model_518"));
        if (docControl?.UndoManager != null)
          docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_576"));
        try
        {
          if (context != null && context.Length != 0)
          {
            if (context.Length == 1 && context[0].IsVirtualNode)
            {
              List<DocumentTreeNode> nodesFromVirtualNode = context[0].GetNodesFromVirtualNode();
              Array.Resize<DocumentTreeNode>(ref context, nodesFromVirtualNode.Count);
              nodesFromVirtualNode.CopyTo(context);
            }
            for (int index = 0; index < context.Length; ++index)
            {
              DocumentTreeNode documentTreeNode = context[index];
              if (documentTreeNode is VisualNode)
                (documentTreeNode as VisualNode).SetVisible(flag, false, true, false, false);
            }
          }
          if (docControl?.ActivePage != null)
            docControl.ActivePage.UpdateLayout(true);
        }
        finally
        {
          if (docControl?.UndoManager != null)
            docControl.UndoManager.EndCreateMultyUndo();
        }
        return true;
      case "ClearFormat":
        if (docControl != null)
        {
          if (docControl.UndoManager != null)
            docControl.UndoManager.BeginCreateMultyUndo(LocalizationHolder.rm.GetString("Document.Model_575"));
          try
          {
            if (docControl.SelectedNode is TextBoxElement selectedNode)
            {
              if (selectedNode.InPlaceEditorActive && selectedNode.InPlaceEditorControl is ImRtfEditor)
              {
                selectedNode.SetRtfText((string) null, false, false);
                ImRtfEditor placeEditorControl = selectedNode.InPlaceEditorControl as ImRtfEditor;
                string activeEditorText = selectedNode.GetActiveEditorText();
                SelectionBlock selectionBlock = placeEditorControl.GetSelectionBlock();
                placeEditorControl.TerDeleteAll(false);
                placeEditorControl.TerInsertText(activeEditorText, -1, -1, false);
                placeEditorControl.RestoreSelection(selectionBlock, true);
                placeEditorControl.TerSetModify(true);
                placeEditorControl.FireModified((object) placeEditorControl);
              }
              else
                selectedNode.SetRtfText((string) null, true, true);
            }
            selectedNode?.UpdateLayout(true);
          }
          finally
          {
            if (docControl.UndoManager != null)
              docControl.UndoManager.EndCreateMultyUndo();
          }
        }
        return true;
      case "ConvertToArea":
        DocumentMenuHelper.ConvertToArea_Execute(context);
        return true;
      case "ConvertToContainer":
        DocumentMenuHelper.ConvertToContainer_Execute(context);
        return true;
      case "ConvertToHeader":
        DocumentMenuHelper.ConvertToHeader_Execute(context);
        return true;
      case "ConvertToLabel":
        DocumentMenuHelper.ConvertToLabel_Execute(context);
        return true;
      case "ConvertToTextBox":
        DocumentMenuHelper.ConvertToTextBox_Execute(context);
        return true;
      case "Copy":
        if (docControl != null)
        {
          System.Windows.Forms.Form form = docControl.FindForm();
          if (form != null)
            hWnd = form.Handle;
        }
        DocumentMenuHelper.Copy_Execute(context, hWnd);
        return true;
      case "CopySelectedToExcel":
        DocumentMenuHelper.CopySelectedToExcel(docControl);
        return true;
      case "CopySelectedToImage":
        DocumentMenuHelper.CopySelectedToImage(docControl);
        return true;
      case "CreateNextPageTemplate":
        DocumentMenuHelper.CreateNextPageTemplate_Execute(context, docControl);
        return true;
      case "CreateOleObject":
        if (context != null && context.Length == 1 && context[0] is ContainerElement context1)
          DocumentMenuHelper.ContainerCreateOle_Execute(context1);
        return true;
      case "Cut":
        if (docControl != null)
        {
          System.Windows.Forms.Form form = docControl.FindForm();
          if (form != null)
            hWnd = form.Handle;
        }
        if (context.Length == 1 && context[0].IsVirtualNode)
        {
          List<DocumentTreeNode> nodesFromVirtualNode = context[0].GetNodesFromVirtualNode();
          Array.Resize<DocumentTreeNode>(ref context, nodesFromVirtualNode.Count);
          nodesFromVirtualNode.CopyTo(context);
        }
        DocumentMenuHelper.Cut_Execute(context, hWnd, docControl);
        return true;
      case "Delete":
        if (context.Length == 1 && context[0].IsVirtualNode)
        {
          List<DocumentTreeNode> nodesFromVirtualNode = context[0].GetNodesFromVirtualNode();
          Array.Resize<DocumentTreeNode>(ref context, nodesFromVirtualNode.Count);
          nodesFromVirtualNode.CopyTo(context);
        }
        bool? nullable = new bool?();
        for (int index = 0; index < context.Length; ++index)
        {
          if (context[index] is Page && !nullable.HasValue)
          {
            string text = LocalizationHolder.rm.GetString("Document.Model_522");
            if (context.Length == 1)
              text = LocalizationHolder.rm.GetString("Document.Model_529");
            nullable = new bool?(MessageBox.Show(text, LocalizationHolder.rm.GetString("Document.Model_521"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes);
          }
        }
        DocumentMenuHelper.UserCommand_DeleteNodes(context, ((int) nullable ?? 0) != 0, docControl);
        return true;
      case "Doc.CoorSystem_BottomLeft":
        DocumentMenuHelper.CoorSystem_Execute(docControl, PageCoorSystem.BottomLeft);
        return true;
      case "Doc.CoorSystem_BottomRight":
        DocumentMenuHelper.CoorSystem_Execute(docControl, PageCoorSystem.BottomRight);
        return true;
      case "Doc.CoorSystem_Custom":
        DocumentMenuHelper.CoorSystem_Execute(docControl, PageCoorSystem.Custom);
        return true;
      case "Doc.CoorSystem_TopLeft":
        DocumentMenuHelper.CoorSystem_Execute(docControl, PageCoorSystem.TopLeft);
        return true;
      case "Doc.CoorSystem_TopRight":
        DocumentMenuHelper.CoorSystem_Execute(docControl, PageCoorSystem.TopRight);
        return true;
      case "Doc.GridSize_0.05":
        DocumentMenuHelper.GridSize_Execute(docControl, 0.05f);
        return true;
      case "Doc.GridSize_0.1":
        DocumentMenuHelper.GridSize_Execute(docControl, 0.1f);
        return true;
      case "Doc.GridSize_0.5":
        DocumentMenuHelper.GridSize_Execute(docControl, 0.5f);
        return true;
      case "Doc.GridSize_1":
        DocumentMenuHelper.GridSize_Execute(docControl, 1f);
        return true;
      case "DocEditor.ChangePageNumberingStyle":
        DocumentMenuHelper.ChangeAdditionalPageNumberingStyle(docControl);
        return true;
      case "DocEditor.InsertAdditionalPages":
        DocumentMenuHelper.InsertAdditionalPage(docControl);
        return true;
      case "DocEditor.RemoveAdditionalPages":
        DocumentMenuHelper.RemoveAdditionalPage(docControl);
        return true;
      case "Find":
        FindReplaceForm.Execute(docControl?.FindReplaceManager, false);
        return true;
      case "FindId":
        FindByIdForm.Execute(docControl);
        return true;
      case "FindNext":
        docControl.FindReplaceManager.Find();
        return true;
      case "LoadOleFile":
        if (context != null && context.Length == 1 && context[0] is ContainerElement context2)
          DocumentMenuHelper.ContainerLoadOle_Execute(context2);
        return true;
      case "MergeCells":
        DocumentMenuHelper.MergeCells_Execute(context, docControl);
        return true;
      case "MoveDown":
        DocumentMenuHelper.MoveDown(context, docControl);
        return true;
      case "MoveToBegin":
        DocumentMenuHelper.MoveToBegin(context, docControl);
        return true;
      case "MoveToEnd":
        DocumentMenuHelper.MoveToEnd(context, docControl);
        return true;
      case "MoveUp":
        DocumentMenuHelper.MoveUp(context, docControl);
        return true;
      case "NewPageAfter":
        DocumentMenuHelper.NewPage_Execute(docControl, false);
        return true;
      case "NewPageBefore":
        DocumentMenuHelper.NewPage_Execute(docControl, true);
        return true;
      case "NextPage":
        DocumentMenuHelper.NextPage_Execute(docControl);
        return true;
      case "Paste":
        if (docControl != null)
        {
          System.Windows.Forms.Form form = docControl.FindForm();
          if (form != null)
            hWnd = form.Handle;
        }
        DocumentMenuHelper.Paste_Execute(context, hWnd, docControl);
        return true;
      case "PrevPage":
        DocumentMenuHelper.PrevPage_Execute(docControl);
        return true;
      case "Redo":
        DocumentMenuHelper.Redo(context, docControl);
        return true;
      case "RemoveCell":
        if (context.Length == 1 && context[0].IsVirtualNode)
        {
          List<DocumentTreeNode> nodesFromVirtualNode = context[0].GetNodesFromVirtualNode();
          Array.Resize<DocumentTreeNode>(ref context, nodesFromVirtualNode.Count);
          nodesFromVirtualNode.CopyTo(context);
        }
        DocumentMenuHelper.RemoveCell_Execute(context, RemoveCellOptions.MergeWithLeft, false);
        return true;
      case "RemoveColumn":
        DocumentMenuHelper.RemoveColumn_Execute(context, docControl);
        return true;
      case "RemovePage":
        DocumentMenuHelper.RemovePage_Execute(docControl);
        return true;
      case "RemoveRow":
        DocumentMenuHelper.RemoveRow_Execute(context);
        return true;
      case "Replace":
        FindReplaceForm.Execute(docControl?.FindReplaceManager, true);
        return true;
      case "SaveImageToFile":
        if (context != null && context.Length == 1 && context[0] is ContainerElement context3 && context3.Image != null)
          DocumentMenuHelper.ContainerSaveImageToFile_Execute(context3);
        return true;
      case "SelectAll":
        if (docControl != null && docControl.Document != null)
        {
          List<DocumentTreeNode> selection = new List<DocumentTreeNode>();
          if (docControl.Document.Nodes != null)
          {
            foreach (Page node in docControl.Document.Nodes)
            {
              if (node != null)
              {
                for (int index = 0; index < node.Nodes.Count; ++index)
                  selection.Add(node.Nodes[index]);
              }
            }
            if (selection.Count > 0)
              docControl.SetSelection(selection, true, false);
          }
        }
        return true;
      case "SelectContinuationTable":
        if (context != null && context.Length == 1 && context[0] is TableElement curTable)
        {
          string str = ContinuationTableIdEditor.ChooseContinuationForTable(curTable);
          if (!string.IsNullOrWhiteSpace(str))
            curTable.ContinuationTableIdTE = str;
        }
        return true;
      case "SendToEnd":
        if (context != null && context.Length == 1)
        {
          if (context[0] is RectangleElement rectangleElement2 && rectangleElement2.IsTableCell)
            DocumentMenuHelper.MoveToEnd(context, docControl);
          else
            DocumentMenuHelper.MoveToBegin(context, docControl);
        }
        return true;
      case "SplitCell":
        DocumentMenuHelper.SplitCell_Execute(context);
        return true;
      case "UnblockGeometryChanging":
        DocumentMenuHelper.UnblockGeometryChanging_Execute(context, docControl);
        return true;
      case "Undo":
        DocumentMenuHelper.Undo(context, docControl);
        return true;
      case "UpdateTable":
        DocumentMenuHelper.UpadateTable_Execute(context, docControl);
        return true;
      case "Zoom100":
        DocumentMenuHelper.Zoom_Execute(docControl, DocZoomMode.Custom, 1f);
        return true;
      case "Zoom200":
        DocumentMenuHelper.Zoom_Execute(docControl, DocZoomMode.Custom, 2f);
        return true;
      case "Zoom50":
        DocumentMenuHelper.Zoom_Execute(docControl, DocZoomMode.Custom, 0.5f);
        return true;
      case "Zoom75":
        DocumentMenuHelper.Zoom_Execute(docControl, DocZoomMode.Custom, 0.75f);
        return true;
      case "ZoomFitPage":
        DocumentMenuHelper.Zoom_Execute(docControl, DocZoomMode.FitPage, 1f);
        return true;
      case "ZoomFitWidth":
        DocumentMenuHelper.Zoom_Execute(docControl, DocZoomMode.FitWidth, 1f);
        return true;
      default:
        return false;
    }
  }

  /// <summary>ActiveImDocumentEditorFormBase</summary>
  public static ImDocumentEditorFormBase ActiveImDocumentEditorFormBase
  {
    [DebuggerStepThrough] get
    {
      return DocumentMenuHelper.DockManager == null ? (ImDocumentEditorFormBase) null : DocumentMenuHelper.DockManager.ActiveDocument as ImDocumentEditorFormBase;
    }
  }

  /// <summary>являются ли элементы на основе шаблонов</summary>
  /// <param name="context"></param>
  /// <returns>true - являются, false если среди них есть столбцы, ячейки таблицы</returns>
  private static bool HasTemplate(DocumentTreeNode[] context)
  {
    bool flag = false;
    if (context != null && context.Length != 0)
    {
      for (int index = 0; index < context.Length; ++index)
      {
        DocumentTreeNode documentTreeNode = context[index];
        if (documentTreeNode != null && (documentTreeNode.HasTemplate() || documentTreeNode.ClonedByTemplateWithParent))
          return true;
      }
    }
    else
      flag = true;
    return flag;
  }

  /// <summary>Получение ячеек, если ячейка виртуальная и содержит не виртуальные строки, получить строки</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private static List<DocumentTreeNode> GetNodes(DocumentTreeNode[] context)
  {
    List<DocumentTreeNode> nodes = new List<DocumentTreeNode>();
    if (context != null && context.Length != 0)
    {
      for (int index1 = 0; index1 < context.Length; ++index1)
      {
        if (context[index1] != null)
        {
          if (context[index1].IsVirtualNode && context[index1] is RectangleElement && !(context[index1] as RectangleElement).IsSingleCell)
          {
            RectangleElement rectangleElement = context[index1] as RectangleElement;
            List<DocumentTreeNode> collection = new List<DocumentTreeNode>();
            for (int index2 = 0; index2 < rectangleElement.NodesCount; ++index2)
            {
              if (!rectangleElement.Nodes[index2].IsVirtualNode)
                collection.Add(rectangleElement.Nodes[index2]);
            }
            if (collection.Count == rectangleElement.NodesCount)
              nodes.AddRange((IEnumerable<DocumentTreeNode>) collection);
            else
              nodes.Add((DocumentTreeNode) rectangleElement);
          }
          else
            nodes.Add(context[index1]);
        }
      }
    }
    return nodes;
  }

  /// <summary>Являются ли элементы отдельными элементами, строками, таблицами</summary>
  /// <param name="context"></param>
  /// <returns>true - являются, false если среди них есть столбцы, ячейки таблицы</returns>
  private static bool CanBeChanged(DocumentTreeNode[] context)
  {
    bool flag = true;
    if (context != null && context.Length != 0)
    {
      List<DocumentTreeNode> nodes = DocumentMenuHelper.GetNodes(context);
      for (int index = 0; index < nodes.Count; ++index)
      {
        DocumentTreeNode documentTreeNode = nodes[index];
        if (!(documentTreeNode is VisualNode))
          return false;
        if (documentTreeNode is RectangleElement)
        {
          RectangleElement rectangleElement = documentTreeNode as RectangleElement;
          if (rectangleElement.ParentCell != null && rectangleElement.ParentCell.IsRow || rectangleElement.IsVirtualNode)
            return false;
        }
      }
    }
    else
      flag = false;
    return flag;
  }

  /// <summary>можно ли удалить елементы</summary>
  /// <param name="context"></param>
  /// <returns>true - можно</returns>
  private static bool CanBeDeleted(DocumentTreeNode[] context, DocumentControl docControl)
  {
    bool flag = true;
    if (context == null || context.Length == 0)
      return false;
    List<DocumentTreeNode> nodes = DocumentMenuHelper.GetNodes(context);
    for (int index = 0; index < nodes.Count; ++index)
    {
      DocumentTreeNode documentTreeNode = nodes[index];
      if (documentTreeNode.Parent == null || documentTreeNode is TableData tableData && !tableData.IsRow && tableData.HasTemplate() || documentTreeNode is PageData pageData && pageData.PrevPage != null || documentTreeNode.ClonedByTemplateWithParent)
        return false;
    }
    int num = 0;
    for (int index = 0; index < nodes.Count; ++index)
    {
      if (nodes[index] is Page)
        ++num;
    }
    return num != docControl.Document.NodesCount && flag;
  }

  /// <summary>Являются ли элементы видимыми</summary>
  /// <param name="context"></param>
  /// <returns>true - если среди них есть хоть один видимый элемент, false - все невидимые</returns>
  private static bool CanHideNodes(DocumentTreeNode[] context)
  {
    bool flag1 = false;
    if (context != null)
    {
      for (int index = 0; !flag1 && index < context.Length; ++index)
      {
        bool flag2 = !(context[index] is PageData) && !(context[index] is ImDocumentData) && !(context[index] is DocumentsComplect);
        if (context[index] is PageElementNode && !context[index].IsVirtualNode && context[index].OwnerDocument != null && context[index].OwnerDocument.IsTemplate && !(context[index] as PageElementNode).CloneByTemplateWithParent)
          flag2 = false;
        flag1 |= flag2;
      }
    }
    return flag1;
  }

  /// <summary>Являются ли элементы видимыми</summary>
  /// <param name="context"></param>
  /// <returns>true - если среди них есть хоть один видимый элемент, false - все невидимые</returns>
  private static bool HasVisibleNode(DocumentTreeNode[] context)
  {
    bool flag = false;
    if (context != null && context.Length != 0)
    {
      List<DocumentTreeNode> nodes = DocumentMenuHelper.GetNodes(context);
      for (int index = 0; index < nodes.Count; ++index)
      {
        if (nodes[index] is VisualNode visualNode && visualNode.Visible)
        {
          flag = true;
          break;
        }
      }
    }
    return flag;
  }

  /// <summary>Вожможно ли undo в активном редакторе</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private static bool CanUndo(DocumentTreeNode[] context, DocumentControl docControl)
  {
    return docControl != null && docControl.UndoManager != null && docControl.UndoManager.CanUndo();
  }

  /// <summary>Вожможно ли redo в активном редакторе</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private static bool CanRedo(DocumentTreeNode[] context, DocumentControl docControl)
  {
    return docControl != null && docControl.UndoManager != null && docControl.UndoManager.CanRedo();
  }

  /// <summary>Получить Bounds для SetClip при копировании ячеек как изображения</summary>
  /// <param name="vNode"></param>
  /// <returns></returns>
  private static RectangleF GetClipBounds(VisualNode vNode, Page page)
  {
    RectangleF clipBounds = RectangleF.Empty;
    if (vNode is RectangleElement)
    {
      RectangleElement rectangleElement = vNode as RectangleElement;
      page.ConvertPixelToMm(new Rectangle(0, 0, 1, 1));
      RectangleF bounds = (vNode as RectangleElement).Bounds;
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      float num4 = 0.0f;
      if (rectangleElement.IsVirtualNode && rectangleElement is TableElement)
      {
        TableElement tableElement = rectangleElement as TableElement;
        float? widthTe;
        if (tableElement.LeftBorderLineTE != null)
        {
          widthTe = tableElement.LeftBorderLineTE.WidthTE;
          if (widthTe.HasValue)
          {
            double num5 = (double) num1;
            widthTe = tableElement.LeftBorderLineTE.WidthTE;
            double num6 = (double) widthTe.Value / 2.0;
            num1 = (float) (num5 + num6);
          }
        }
        if (tableElement.RightBorderLineTE != null)
        {
          widthTe = tableElement.RightBorderLineTE.WidthTE;
          if (widthTe.HasValue)
          {
            double num7 = (double) num2;
            widthTe = tableElement.RightBorderLineTE.WidthTE;
            double num8 = (double) widthTe.Value / 2.0;
            num2 = (float) (num7 + num8);
          }
        }
        if (tableElement.TopBorderLineTE != null)
        {
          widthTe = tableElement.TopBorderLineTE.WidthTE;
          if (widthTe.HasValue)
          {
            double num9 = (double) num3;
            widthTe = tableElement.TopBorderLineTE.WidthTE;
            double num10 = (double) widthTe.Value / 2.0;
            num3 = (float) (num9 + num10);
          }
        }
        if (tableElement.BottomBorderLineTE != null)
        {
          widthTe = tableElement.BottomBorderLineTE.WidthTE;
          if (widthTe.HasValue)
          {
            double num11 = (double) num4;
            widthTe = tableElement.BottomBorderLineTE.WidthTE;
            double num12 = (double) widthTe.Value / 2.0;
            num4 = (float) (num11 + num12);
          }
        }
      }
      else
      {
        float num13 = rectangleElement.LeftBorderLine.Width;
        float num14 = 1.2f;
        if ((double) num13 < (double) num14)
          num13 = num14;
        num1 += num13;
        float num15 = rectangleElement.RightBorderLine.Width;
        if ((double) num15 == 0.0)
          num15 = PageElementNode.DefaultLineWidth;
        if ((double) num15 < (double) num14)
          num15 = num14;
        num2 += num15;
        float num16 = rectangleElement.TopBorderLine.Width;
        if ((double) num16 == 0.0)
          num16 = PageElementNode.DefaultLineWidth;
        if ((double) num16 < (double) num14)
          num16 = num14;
        num3 += num16;
        float num17 = rectangleElement.BottomBorderLine.Width;
        if ((double) num17 == 0.0)
          num17 = PageElementNode.DefaultLineWidth;
        if ((double) num17 < (double) num14)
          num17 = num14;
        num4 += num17;
      }
      double left = (double) bounds.Left - (double) num1;
      float num18 = bounds.Top - num3;
      float num19 = bounds.Bottom + num4;
      float num20 = bounds.Right + num2;
      double top = (double) num18;
      double right = (double) num20;
      double bottom = (double) num19;
      clipBounds = RectangleF.FromLTRB((float) left, (float) top, (float) right, (float) bottom);
    }
    if (vNode is Polyline)
    {
      Polyline polyline = vNode as Polyline;
      RectangleF mm = page.ConvertPixelToMm(new Rectangle(0, 0, 1, 1));
      float num21 = mm.Height * 2f + polyline.LineWidth;
      float num22 = mm.Height * 2f + polyline.LineWidth;
      float num23 = mm.Height * 2f + polyline.LineWidth;
      float num24 = mm.Height * 2f + polyline.LineWidth;
      float num25 = float.MaxValue;
      float num26 = float.MaxValue;
      float num27 = float.MinValue;
      float num28 = float.MinValue;
      if (polyline.PathPoints != null)
      {
        for (int index = 0; index < polyline.PathPoints.Length; ++index)
        {
          PointF pathPoint = polyline.PathPoints[index];
          if ((double) pathPoint.X < (double) num25)
            num25 = pathPoint.X;
          if ((double) pathPoint.X > (double) num28)
            num28 = pathPoint.X;
          if ((double) pathPoint.Y < (double) num26)
            num26 = pathPoint.Y;
          if ((double) pathPoint.Y > (double) num27)
            num27 = pathPoint.Y;
        }
      }
      if ((double) num25 != 3.4028234663852886E+38 && (double) num26 != 3.4028234663852886E+38 && (double) num27 != -3.4028234663852886E+38 && (double) num28 != -3.4028234663852886E+38)
      {
        float left = num25 - num21;
        float top = num26 - num23;
        float bottom = num27 + num24;
        float right = num28 + num22;
        clipBounds = RectangleF.FromLTRB(left, top, right, bottom);
      }
    }
    if (vNode is Page)
      clipBounds = new RectangleF(new PointF(0.0f, 0.0f), (vNode as Page).Size);
    return clipBounds;
  }

  /// <summary>Копирование таблиц в excel</summary>
  /// <param name="docControl"></param>
  private static void CopySelectedToExcel(DocumentControl docControl)
  {
    StringBuilder stringBuilder = new StringBuilder();
    List<RectangleElement> rectangleElementList = new List<RectangleElement>();
    foreach (DocumentTreeNode selectedNode in docControl.SelectedNodes)
    {
      if (selectedNode is RectangleElement rectangleElement1)
      {
        if (!rectangleElement1.IsVirtualNode)
        {
          rectangleElementList.Add(rectangleElement1);
        }
        else
        {
          foreach (DocumentTreeNode realCell in rectangleElement1.GetRealCells())
          {
            if (realCell is RectangleElement rectangleElement)
              rectangleElementList.Add(rectangleElement);
          }
        }
      }
    }
    List<TableData> tableDataList1 = new List<TableData>();
    foreach (RectangleElement rectangleElement in rectangleElementList)
    {
      if (!(rectangleElement is TableData tableData) && rectangleElement.ParentCell != null)
        tableData = rectangleElement.ParentCell.TableOwner;
      if (!tableDataList1.Contains(tableData) && tableData != null)
        tableDataList1.Add(tableData);
    }
    List<TableData> tableDataList2 = new List<TableData>();
    foreach (TableData tableData1 in tableDataList1)
    {
      if (tableData1.IsColumn)
      {
        foreach (DocumentTreeNode child in tableData1)
        {
          if (!(child is TableData tableData2))
          {
            tableData2 = (TableData) TableElement.CreateVirtualTable((DocumentTreeNode) tableData1, (DocumentTreeNode) tableData1);
            tableData2.AddChildNode(child, false, false);
          }
          if (!tableDataList2.Contains(tableData2))
            tableDataList2.Add(tableData2);
        }
      }
      else if (!tableDataList2.Contains(tableData1))
        tableDataList2.Add(tableData1);
    }
    tableDataList2.Sort(new Comparison<TableData>(DocumentMenuHelper.CompareRows));
    bool flag = false;
    foreach (DocumentTreeNode documentTreeNode in tableDataList2)
    {
      foreach (object node in documentTreeNode.Nodes)
      {
        if (node is TextData textData)
        {
          string text = textData.Text;
          if (text.All<char>((Func<char, bool>) (x => char.IsDigit(x) || x == '.')) && text.Contains<char>('.'))
            stringBuilder.AppendFormat("=\"{0}\"\t", (object) text);
          else
            stringBuilder.AppendFormat("\"{0}\"\t", (object) text);
        }
        else
          flag = true;
      }
      stringBuilder.AppendFormat("\n");
    }
    if (flag)
    {
      int num = (int) MessageBox.Show("В таблице присутствуют не только текстовые элементы, часть информации может быть утеряна", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    string text1 = stringBuilder.ToString();
    if (string.IsNullOrWhiteSpace(text1))
      return;
    Clipboard.SetText(text1, TextDataFormat.UnicodeText);
  }

  private static int CompareRows(TableData x, TableData y)
  {
    RectangleElement rectangleElement1 = (RectangleElement) x;
    if (rectangleElement1.IsVirtualNode)
      rectangleElement1 = x.Nodes[0] as RectangleElement;
    RectangleElement rectangleElement2 = (RectangleElement) y;
    if (rectangleElement2.IsVirtualNode)
      rectangleElement2 = y.Nodes[0] as RectangleElement;
    if (rectangleElement1.Page != rectangleElement2.Page)
    {
      if (rectangleElement1.Page == null)
        return -1;
      if (rectangleElement2.Page == null)
        return 1;
      if (rectangleElement1.Page != null && rectangleElement2.Page != null)
        return rectangleElement1.Page.Index.CompareTo(rectangleElement2.Page.Index);
    }
    return rectangleElement1.Bounds.Top.CompareTo(rectangleElement2.Bounds.Top);
  }

  private static void CopySelectedToImage(DocumentControl docControl)
  {
    if (docControl == null || docControl.ActivePage == null)
      return;
    Page activePage = docControl.ActivePage;
    Metafile metafile = (Metafile) null;
    IntPtr dc = Page.GetDC(IntPtr.Zero);
    RectangleF rectangleF = new RectangleF(activePage.Location, activePage.Size);
    Rectangle empty = Rectangle.Empty;
    try
    {
      metafile = new Metafile(dc, Rectangle.Empty, MetafileFrameUnit.Millimeter, EmfType.EmfOnly);
      using (Graphics g = Graphics.FromImage((Image) metafile))
      {
        DrawContext context = new DrawContext(new ImGraphics(g), false, new RectangleF(PointF.Empty, activePage.Size), 0, false, false, new MatrixWrapper(g.Transform))
        {
          IsMetafile = true
        };
        for (int index1 = 0; index1 < docControl.SelectedNodes.Count; ++index1)
        {
          if (docControl.SelectedNodes[index1] is VisualNode)
          {
            VisualNode selectedNode = docControl.SelectedNodes[index1] as VisualNode;
            if (selectedNode is RectangleElement rectangleElement && rectangleElement.TopLevelTable != null)
              context.Margins = rectangleElement.TopLevelTable.Margins;
            if (selectedNode.IsVirtualNode && selectedNode is RectangleElement)
            {
              List<DocumentTreeNode> singleCells = (selectedNode as RectangleElement).GetSingleCells();
              for (int index2 = 0; index2 < singleCells.Count; ++index2)
              {
                if (singleCells[index2] is VisualNode)
                  (singleCells[index2] as VisualNode).Draw(context);
              }
            }
            else
              selectedNode.Draw(context);
          }
        }
      }
      if (metafile.Height > 0 && metafile.Width > 0)
      {
        metafile = ContainerElement.SetMetafileHeader(metafile, Rectangle.Empty, RectangleF.Empty);
        ContainerElement.MetafileToClipboard(docControl.Handle, metafile);
      }
      metafile.Dispose();
      metafile = (Metafile) null;
    }
    finally
    {
      metafile?.Dispose();
      Page.ReleaseDC(IntPtr.Zero, dc);
    }
  }

  /// <summary>undo в активном редакторе&gt;</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private static void Undo(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (docControl.UndoManager == null)
      return;
    docControl.UndoManager.DoUndo();
  }

  /// <summary>redo в активном редакторе</summary>
  /// <param name="context"></param>
  /// <returns></returns>
  private static void Redo(DocumentTreeNode[] context, DocumentControl docControl)
  {
    if (docControl.UndoManager == null)
      return;
    docControl.UndoManager.DoRedo();
  }

  /// <summary>Проверить состояние команд</summary>
  /// <param name="commandState">Состояние команды</param>
  /// <param name="context">Контекст команды - список узлов документа</param>
  /// <param name="docControl">Контрол документа</param>
  /// <returns>true, если команда найдена</returns>
  public virtual bool QueryStatus(
    ICommandState commandState,
    DocumentTreeNode[] context,
    DocumentControl docControl)
  {
    if (docControl == null)
      return false;
    bool flag1 = docControl.ReadOnly;
    bool flag2 = false;
    bool cacheHasLockedNodes = docControl.QueryCache_HasLockedNodes;
    switch (commandState.CommandName)
    {
      case "AddRowFromTemplateAbove":
      case "AddRowFromTemplateBelow":
        if (!flag1 && context != null && context.Length == 1 && context[0] != null && !cacheHasLockedNodes && (!context[0].IsVirtualNode || !(context[0] is TableElement)))
        {
          TableElement viewRowParent;
          DocumentMenuHelper.FindRowParent(context[0], out viewRowParent, out int _);
          if (viewRowParent != null)
            flag2 = (!viewRowParent.ReadOnlyStructure || ImDocumentData.ShowDebugInfo) && viewRowParent.Template != null;
        }
        commandState.Enabled = flag2;
        return true;
      case "AddTableCell":
        if (!flag1 && context != null && context.Length == 1 && context[0] != null && !cacheHasLockedNodes && (!context[0].IsVirtualNode || !(context[0] is TableElement)))
        {
          if (context[0] is VirtualColumn virtualColumn1)
          {
            TableData parentCell = virtualColumn1.ParentCell;
            flag2 = parentCell != null && !parentCell.ReadOnlyStructure && parentCell.IsFixedStructureArea;
          }
          else if (context[0] is RectangleElement rectangleElement1)
          {
            TableData tableForAddColumn = rectangleElement1.FindTableForAddColumn(false);
            flag2 = tableForAddColumn != null && !tableForAddColumn.ReadOnlyStructure && tableForAddColumn.IsFixedStructureArea;
          }
        }
        commandState.Enabled = flag2;
        return true;
      case "AddTableColumnLeft":
      case "AddTableColumnRight":
        if (!flag1 && context != null && context.Length == 1 && context[0] != null && !cacheHasLockedNodes && (!context[0].IsVirtualNode || !(context[0] is TableElement)))
        {
          if (context[0] is VirtualColumn virtualColumn2)
          {
            TableData parentCell = virtualColumn2.ParentCell;
            flag2 = parentCell != null && !parentCell.ReadOnlyStructure && !parentCell.IsFixedStructureArea;
          }
          else if (context[0] is RectangleElement rectangleElement2)
          {
            TableData tableForAddColumn = rectangleElement2.FindTableForAddColumn(false);
            flag2 = tableForAddColumn != null && !tableForAddColumn.ReadOnlyStructure && !tableForAddColumn.IsFixedStructureArea;
          }
        }
        commandState.Enabled = flag2;
        return true;
      case "AddTableRowAbove":
      case "AddTableRowBelow":
        if (!flag1 && context != null && context.Length == 1 && !cacheHasLockedNodes && (!context[0].IsVirtualNode || !(context[0] is TableElement)))
        {
          TableElement viewRowParent;
          DocumentMenuHelper.FindRowParent(context[0], out viewRowParent, out int _);
          if (viewRowParent != null)
            flag2 = !viewRowParent.ReadOnlyStructure;
        }
        commandState.Enabled = flag2;
        return true;
      case "AddTableSection":
        commandState.Enabled = flag2;
        return true;
      case "ApplyPreviousTable":
        if (!flag1 && context != null && context.Length == 1 && !cacheHasLockedNodes)
          flag2 = context[0] is TableElement tableElement1 && !tableElement1.IsVirtualNode && !tableElement1.ReadOnlyStructure && tableElement1.UsePreviousTableTemplates && !tableElement1.HasTemplate();
        commandState.Enabled = flag2;
        return true;
      case "BringToFront":
        bool flag3 = false;
        if (context != null && context.Length == 1 && !cacheHasLockedNodes)
          flag3 = !(context[0] is RectangleElement rectangleElement3) || !rectangleElement3.IsTableCell;
        commandState.Enabled = flag3 && !flag1 && context?[0].Parent != null && context[0].Parent.Nodes != null && context[0].Index < context[0].Parent.Nodes.Count - 1;
        return true;
      case "CallEditor":
        commandState.Enabled = !flag1 && context != null && context.Length == 1 && !cacheHasLockedNodes && context[0].CanCallEditor;
        commandState.Visible = true;
        return true;
      case "ChangeVisibility":
        commandState.Enabled = !flag1 && !cacheHasLockedNodes;
        commandState.Text = !DocumentMenuHelper.HasVisibleNode(context) ? LocalizationHolder.rm.GetString("Document.Model_519") : LocalizationHolder.rm.GetString("Document.Model_518");
        return true;
      case "ClearFormat":
        bool flag4 = false;
        if (docControl.SelectedNode is TextBoxElement)
          flag4 = true;
        commandState.Enabled = flag4;
        if (flag4)
          commandState.Visible = true;
        return true;
      case "ConvertToArea":
        if (ImDocumentData.ShowDebugInfo && !flag1 && context != null && context.Length == 1 && !cacheHasLockedNodes)
        {
          for (int index = 0; !flag2 && index < context.Length; ++index)
          {
            RectangleElement rectangleElement4 = context[index] as RectangleElement;
            flag2 = ((flag2 ? 1 : 0) | (rectangleElement4 == null || rectangleElement4.IsVirtualNode ? 0 : (!rectangleElement4.ReadOnlyStructure ? 1 : 0))) != 0;
          }
        }
        if (context != null && DocumentMenuHelper.HasTemplate(context))
          flag2 = false;
        commandState.Enabled = flag2;
        return true;
      case "ConvertToContainer":
        if (!flag1 && context != null && !cacheHasLockedNodes)
        {
          for (int index = 0; !flag2 && index < context.Length; ++index)
          {
            RectangleElement rectangleElement5 = context[index] as RectangleElement;
            int num1 = flag2 ? 1 : 0;
            int num2;
            switch (rectangleElement5)
            {
              case null:
              case ContainerData _:
              case TableData _:
                num2 = 0;
                break;
              default:
                num2 = !rectangleElement5.ReadOnlyStructure ? 1 : 0;
                break;
            }
            flag2 = (num1 | num2) != 0;
          }
        }
        if (context != null && DocumentMenuHelper.HasTemplate(context))
          flag2 = false;
        commandState.Enabled = flag2;
        return true;
      case "ConvertToHeader":
        if (!flag1 && context != null && !cacheHasLockedNodes)
        {
          for (int index = 0; !flag2 && index < context.Length; ++index)
          {
            TableElement tableElement2 = context[index] as TableElement;
            flag2 = ((flag2 ? 1 : 0) | (tableElement2 == null || tableElement2.ParentCell == null || !tableElement2.ParentCell.IsColumn ? 0 : (!tableElement2.ReadOnlyStructure ? 1 : 0))) != 0;
          }
        }
        commandState.Enabled = flag2;
        return true;
      case "ConvertToLabel":
        if (!flag1 && context != null && !cacheHasLockedNodes)
        {
          for (int index = 0; !flag2 && index < context.Length; ++index)
          {
            RectangleElement rectangleElement6 = context[index] as RectangleElement;
            int num3 = flag2 ? 1 : 0;
            int num4;
            switch (rectangleElement6)
            {
              case null:
              case LabelElement _:
              case TableData _:
                num4 = 0;
                break;
              default:
                num4 = !rectangleElement6.ReadOnlyStructure ? 1 : 0;
                break;
            }
            flag2 = (num3 | num4) != 0;
          }
        }
        if (context != null && DocumentMenuHelper.HasTemplate(context))
          flag2 = false;
        commandState.Enabled = flag2;
        return true;
      case "ConvertToTextBox":
        if (!flag1 && context != null && !cacheHasLockedNodes)
        {
          for (int index = 0; !flag2 && index < context.Length; ++index)
          {
            RectangleElement rectangleElement7 = context[index] as RectangleElement;
            int num5 = flag2 ? 1 : 0;
            int num6;
            switch (rectangleElement7)
            {
              case null:
              case TextBoxElement _:
                num6 = 0;
                break;
              case TableData _:
                if (!ImDocumentData.ShowDebugInfo)
                  goto case null;
                goto default;
              default:
                num6 = !rectangleElement7.ReadOnlyStructure ? 1 : 0;
                break;
            }
            flag2 = (num5 | num6) != 0;
          }
        }
        if (context != null && DocumentMenuHelper.HasTemplate(context))
          flag2 = false;
        commandState.Enabled = flag2;
        return true;
      case "Copy":
        commandState.Enabled = context != null && context.Length != 0 && !cacheHasLockedNodes;
        return true;
      case "CopySelectedToExcel":
        commandState.Enabled = docControl.ActivePage != null;
        if (commandState.Enabled)
        {
          commandState.Enabled = false;
          if (docControl.SelectedNodes != null && docControl.SelectedNodes.Count > 0)
          {
            foreach (DocumentTreeNode selectedNode in docControl.SelectedNodes)
            {
              if (selectedNode is RectangleElement rectangleElement8 && (rectangleElement8.IsTableCell || rectangleElement8.IsVirtualNode || rectangleElement8 is TableData))
                commandState.Enabled = true;
            }
          }
        }
        commandState.Visible = true;
        return true;
      case "CopySelectedToImage":
        commandState.Enabled = docControl.ActivePage != null && docControl.SelectedNodes.Count > 0;
        commandState.Visible = true;
        return true;
      case "CreateNextPageTemplate":
        Page page = (Page) null;
        if (NodeContextMenu.ContextMenuCommand && NodeContextMenu.ContextForContextMenu == context && context != null && context.Length == 1)
          page = context[0] as Page;
        if (page == null)
          page = docControl.ActivePage;
        bool flag5 = !docControl.ReadOnly && page != null && page.IsTemplate && page.OwnerDocument != null;
        commandState.Enabled = flag5;
        return true;
      case "CreateOleObject":
      case "LoadOleFile":
        bool flag6 = false;
        if (!flag1 && context != null && context.Length == 1 && !cacheHasLockedNodes && context[0] is ContainerElement containerElement1)
          flag6 = !containerElement1.ReadOnlyNow;
        commandState.Enabled = flag6;
        return true;
      case "Cut":
        commandState.Enabled = context != null && context.Length != 0 && !cacheHasLockedNodes && DocumentMenuHelper.CanBeDeleted(context, docControl) && !flag1;
        return true;
      case "Delete":
        commandState.Enabled = context != null && context.Length != 0 && (ImDocumentEditorConfig.Instance.ShowDebugInfo || !cacheHasLockedNodes && DocumentMenuHelper.CanBeDeleted(context, docControl)) && !flag1;
        return true;
      case "Doc.CoorSystem_BottomLeft":
        commandState.Enabled = true;
        commandState.Checked = ImDocumentEditorConfig.Instance.CoorSystem == PageCoorSystem.BottomLeft;
        return true;
      case "Doc.CoorSystem_BottomRight":
        commandState.Enabled = true;
        commandState.Checked = ImDocumentEditorConfig.Instance.CoorSystem == PageCoorSystem.BottomRight;
        return true;
      case "Doc.CoorSystem_Custom":
        commandState.Enabled = true;
        commandState.Checked = ImDocumentEditorConfig.Instance.CoorSystem == PageCoorSystem.Custom;
        return true;
      case "Doc.CoorSystem_TopLeft":
        commandState.Enabled = true;
        commandState.Checked = ImDocumentEditorConfig.Instance.CoorSystem == PageCoorSystem.TopLeft;
        return true;
      case "Doc.CoorSystem_TopRight":
        commandState.Enabled = true;
        commandState.Checked = ImDocumentEditorConfig.Instance.CoorSystem == PageCoorSystem.TopRight;
        return true;
      case "Doc.GridSize_0.05":
        commandState.Enabled = true;
        commandState.Checked = (double) ImDocumentEditorConfig.Instance.GridSize == 0.05000000074505806;
        return true;
      case "Doc.GridSize_0.1":
        commandState.Enabled = true;
        commandState.Checked = (double) ImDocumentEditorConfig.Instance.GridSize == 0.10000000149011612;
        return true;
      case "Doc.GridSize_0.5":
        commandState.Enabled = true;
        commandState.Checked = (double) ImDocumentEditorConfig.Instance.GridSize == 0.5;
        return true;
      case "Doc.GridSize_1":
        commandState.Enabled = true;
        commandState.Checked = (double) ImDocumentEditorConfig.Instance.GridSize == 1.0;
        return true;
      case "DocEditor.ChangePageNumberingStyle":
        Page activePage1 = docControl.ActivePage;
        commandState.Enabled = commandState.Visible = !docControl.ReadOnly && activePage1 != null && activePage1.IsAdditionalPage;
        return true;
      case "DocEditor.InsertAdditionalPages":
        Page activePage2 = docControl.ActivePage;
        commandState.Enabled = commandState.Visible = !docControl.ReadOnly && activePage2 != null && activePage2.NextPage != null && !activePage2.IsAdditionalPage && !activePage2.NextPage.IsAdditionalPage && !activePage2.IsTitlePage;
        return true;
      case "DocEditor.RemoveAdditionalPages":
        Page activePage3 = docControl.ActivePage;
        commandState.Enabled = commandState.Visible = !docControl.ReadOnly && activePage3 != null && activePage3.NextPage != null && (activePage3.IsAdditionalPage || activePage3.NextPage.IsAdditionalPage);
        return true;
      case "Find":
        bool flag7 = true;
        commandState.Enabled = flag7;
        return true;
      case "FindId":
        bool flag8 = true;
        commandState.Enabled = flag8;
        commandState.Visible = ImDocumentEditorConfig.Instance.ShowDebugInfo || docControl.DocumentEditorForm != null && docControl.DocumentEditorForm.BaseEditCommandsEnabled;
        return true;
      case "FindNext":
        bool flag9 = docControl.FindReplaceManager != null && docControl.FindReplaceManager.Initialized;
        commandState.Enabled = flag9;
        return true;
      case "MergeCells":
        commandState.Enabled = false;
        return true;
      case "MoveDown":
        bool flag10 = false;
        if (context != null && context.Length == 1 && !cacheHasLockedNodes)
          flag10 = !flag1 && !context[0].IsLastCellInParentDataFlow;
        bool flag11 = flag10 && DocumentMenuHelper.CanBeChanged(context);
        commandState.Enabled = flag11;
        return true;
      case "MoveToBegin":
        bool flag12 = false;
        if (context != null && context.Length == 1 && !cacheHasLockedNodes)
          flag12 = context[0] is RectangleElement rectangleElement9 && rectangleElement9.IsTableCell;
        bool flag13 = flag12 && DocumentMenuHelper.CanBeChanged(context);
        commandState.Enabled = flag13 && !flag1 && context != null && context.Length != 0 && !context[0].IsFirstCellInParentDataFlow;
        return true;
      case "MoveToEnd":
        bool flag14 = false;
        if (context != null && context.Length == 1 && !cacheHasLockedNodes)
          flag14 = context[0] is RectangleElement rectangleElement10 && rectangleElement10.IsTableCell;
        bool flag15 = flag14 && DocumentMenuHelper.CanBeChanged(context);
        commandState.Enabled = flag15 && !flag1 && !context[0].IsLastCellInParentDataFlow;
        return true;
      case "MoveUp":
        bool flag16 = false;
        if (context != null && context.Length == 1 && !cacheHasLockedNodes)
          flag16 = !flag1 && !context[0].IsFirstCellInParentDataFlow;
        bool flag17 = flag16 && DocumentMenuHelper.CanBeChanged(context);
        commandState.Enabled = flag17;
        return true;
      case "NewPageAfter":
        commandState.Enabled = !flag1 && !cacheHasLockedNodes;
        if (commandState.Enabled && DocumentMenuHelper.miInsertPageAfter != null && docControl.Document != null)
        {
          if (docControl.Document.IsTemplate)
            DocumentMenuHelper.miInsertPageAfter.Text = LocalizationHolder.rm.GetString("Document.Model_595");
          else
            DocumentMenuHelper.miInsertPageAfter.Text = LocalizationHolder.rm.GetString("Document.Model_595") + "...";
        }
        return true;
      case "NewPageBefore":
        commandState.Enabled = !flag1 && !cacheHasLockedNodes;
        if (commandState.Enabled && DocumentMenuHelper.miInsertPageBefore != null && docControl.Document != null)
        {
          if (docControl.Document.IsTemplate)
            DocumentMenuHelper.miInsertPageBefore.Text = LocalizationHolder.rm.GetString("Document.Model_461");
          else
            DocumentMenuHelper.miInsertPageBefore.Text = LocalizationHolder.rm.GetString("Document.Model_461") + "...";
        }
        return true;
      case "NextPage":
        commandState.Enabled = docControl.Document?.Nodes != null && docControl.Document.Nodes.IndexOf((DocumentTreeNode) docControl.ActivePage) + 1 < docControl.Document.Nodes.Count;
        return true;
      case "Paste":
        bool flag18 = false;
        if (context.Length == 1)
        {
          flag18 = context[0].ReadOnlyStructure;
          if (context[0] is TableData tableData && tableData.IsRow && tableData.ParentCell != null)
            flag18 = tableData.ParentCell.ReadOnlyStructure;
        }
        commandState.Enabled = !flag1 && context.Length == 1 && (!flag18 || context[0] is ContainerElement) && !cacheHasLockedNodes && NodeClipboardHelper.CanPasteFromClipboard(context[0]);
        return true;
      case "PrevPage":
        commandState.Enabled = docControl.Document?.Nodes != null && docControl.Document.Nodes.IndexOf((DocumentTreeNode) docControl.ActivePage) > 0;
        return true;
      case "Redo":
        commandState.Enabled = DocumentMenuHelper.CanRedo(context, docControl);
        return true;
      case "RemoveCell":
        commandState.Enabled = context != null && !flag1 && !cacheHasLockedNodes && DocumentMenuHelper.QueryStatus_RemoveCell((IList<DocumentTreeNode>) context) && !DocumentMenuHelper.HasTemplate(context);
        return true;
      case "RemoveColumn":
        commandState.Enabled = context != null && !flag1 && !cacheHasLockedNodes && DocumentMenuHelper.QueryStatus_RemoveColumn(context) && !DocumentMenuHelper.HasTemplate(context);
        return true;
      case "RemovePage":
        ICommandState commandState1 = commandState;
        int num;
        if (!docControl.ReadOnly && docControl.Document != null && docControl.ActivePage != null)
        {
          if (DocumentMenuHelper.CanBeDeleted(new DocumentTreeNode[1]
          {
            (DocumentTreeNode) docControl.ActivePage
          }, docControl) && docControl.Document.NodesCount > 1)
          {
            num = !cacheHasLockedNodes ? 1 : 0;
            goto label_29;
          }
        }
        num = 0;
label_29:
        commandState1.Enabled = num != 0;
        return true;
      case "RemoveRow":
        commandState.Enabled = !flag1 && !cacheHasLockedNodes && DocumentMenuHelper.QueryStatus_RemoveRow(context);
        return true;
      case "Replace":
        bool flag19 = docControl.DocumentEditorForm != null && !docControl.DocumentEditorForm.ReadOnly;
        commandState.Enabled = flag19;
        commandState.Visible = true;
        return true;
      case "SaveImageToFile":
        bool flag20 = false;
        if (!flag1 && context != null && context.Length == 1 && context[0] is ContainerElement containerElement2 && containerElement2.Image != null)
          flag20 = !containerElement2.ReadOnlyNow;
        commandState.Enabled = flag20;
        return true;
      case "SelectAll":
        commandState.Enabled = docControl.ActivePage != null;
        commandState.Text = LocalizationHolder.rm.GetString("Document.Model_523");
        return true;
      case "SelectContinuationTable":
        if (!flag1 && context != null && context.Length == 1 && !cacheHasLockedNodes)
          flag2 = context[0] is TableElement tableElement3 && tableElement3.IsTemplate && !tableElement3.IsVirtualNode && !tableElement3.ReadOnlyStructure && tableElement3.IsTopLevelTable;
        commandState.Enabled = flag2;
        return true;
      case "SendToEnd":
        bool flag21 = false;
        if (context != null && context.Length == 1 && !cacheHasLockedNodes)
          flag21 = !(context[0] is RectangleElement rectangleElement11) || !rectangleElement11.IsTableCell;
        commandState.Enabled = flag21 && !flag1 && context != null && context.Length != 0 && context[0].Parent != null && context[0].Index < context[0].Parent.NodesCount - 1;
        return true;
      case "SplitCell":
        string str = LocalizationHolder.rm.GetString("Document.Model_345");
        if (!flag1 && context != null && context.Length == 1 && context[0] != null && !cacheHasLockedNodes && !context[0].IsVirtualNode)
        {
          if (context[0] is RectangleElement rectangleElement12)
          {
            flag2 = true;
            if (rectangleElement12 is TableElement)
              flag2 = !(rectangleElement12 as TableElement).ReadOnlyStructure;
          }
          if (rectangleElement12 != null && !rectangleElement12.IsTableCell && !(rectangleElement12 is TableData))
            str = LocalizationHolder.rm.GetString("Document.Model_535");
        }
        commandState.Text = str;
        commandState.Enabled = flag2;
        return true;
      case "Undo":
        commandState.Enabled = DocumentMenuHelper.CanUndo(context, docControl);
        return true;
      case "UpdateTable":
        if (context != null && !cacheHasLockedNodes && ImDocumentData.ShowDebugInfo)
        {
          for (int index = 0; !flag2 && index < context.Length; ++index)
          {
            RectangleElement rectangleElement13 = context[index] as RectangleElement;
            flag2 = ((flag2 ? 1 : 0) | (rectangleElement13 != null ? 1 : (context[index] is PageData ? 1 : 0))) != 0;
          }
        }
        commandState.Enabled = flag2;
        return true;
      case "Zoom100":
        commandState.Enabled = true;
        commandState.Checked = (double) docControl.DocumentScale == 1.0 && docControl.ZoomMode == DocZoomMode.Custom;
        return true;
      case "Zoom200":
        commandState.Enabled = true;
        commandState.Checked = (double) docControl.DocumentScale == 2.0 && docControl.ZoomMode == DocZoomMode.Custom;
        return true;
      case "Zoom50":
        commandState.Enabled = true;
        commandState.Checked = (double) docControl.DocumentScale == 0.5 && docControl.ZoomMode == DocZoomMode.Custom;
        return true;
      case "Zoom75":
        commandState.Enabled = true;
        commandState.Checked = (double) docControl.DocumentScale == 0.75 && docControl.ZoomMode == DocZoomMode.Custom;
        return true;
      case "ZoomFitPage":
        commandState.Enabled = true;
        commandState.Checked = docControl.ZoomMode == DocZoomMode.FitPage;
        return true;
      case "ZoomFitWidth":
        commandState.Enabled = true;
        commandState.Checked = docControl.ZoomMode == DocZoomMode.FitWidth;
        return true;
      default:
        return false;
    }
  }

  /// <summary>Создать пункт меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandCaption">Подпись команды</param>
  /// <param name="commandHint">Подсказка команды</param>
  /// <param name="beginGroup">Пункт меню начинает группу</param>
  /// <param name="createContextMenuItem">Создать и пункт контекстного меню</param>
  /// <param name="commandManager">CommandManager</param>
  /// <param name="lockItem">true - запретить отображение элемента в настройке вида</param>
  /// <returns>Пункт меню</returns>
  public static MenuButtonItem CreateMenuItem(
    string commandName,
    string commandCaption,
    string commandHint,
    bool beginGroup,
    bool createContextMenuItem,
    ICommandManager commandManager,
    bool lockItem)
  {
    return DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, (Image) null, beginGroup, createContextMenuItem, commandManager, lockItem);
  }

  /// <summary>Создать пункт меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandCaption">Подпись команды</param>
  /// <param name="commandHint">Подсказка команды</param>
  /// <param name="beginGroup">Пункт меню начинает группу</param>
  /// <param name="createContextMenuItem">Создать и пункт контекстного меню</param>
  /// <param name="commandManager">CommandManager</param>
  /// <returns>Пункт меню</returns>
  public static MenuButtonItem CreateMenuItem(
    string commandName,
    string commandCaption,
    string commandHint,
    bool beginGroup,
    bool createContextMenuItem,
    ICommandManager commandManager)
  {
    return DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, (Image) null, beginGroup, createContextMenuItem, commandManager);
  }

  /// <summary>Создать пункт меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandCaption">Подпись команды</param>
  /// <param name="img">Иконка для пункта меню</param>
  /// <param name="commandHint">Подсказка команды</param>
  /// <param name="beginGroup">Пункт меню начинает группу</param>
  /// <param name="createContextMenuItem">Создать и пункт контекстного меню</param>
  /// <param name="commandManager">CommandManager</param>
  /// <returns>Пункт меню</returns>
  public static MenuButtonItem CreateMenuItem(
    string commandName,
    string commandCaption,
    string commandHint,
    Image img,
    bool beginGroup,
    bool createContextMenuItem,
    ICommandManager commandManager)
  {
    return DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, img, beginGroup, createContextMenuItem, commandManager, false);
  }

  /// <summary>Создать пункт меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandCaption">Подпись команды</param>
  /// <param name="img">Иконка для пункта меню</param>
  /// <param name="commandHint">Подсказка команды</param>
  /// <param name="beginGroup">Пункт меню начинает группу</param>
  /// <param name="createContextMenuItem">Создать и пункт контекстного меню</param>
  /// <param name="commandManager">CommandManager</param>
  /// <param name="lockItem">true - запретить отображение элемента в настройке вида</param>
  /// <returns>Пункт меню</returns>
  public static MenuButtonItem CreateMenuItem(
    string commandName,
    string commandCaption,
    string commandHint,
    Image img,
    bool beginGroup,
    bool createContextMenuItem,
    ICommandManager commandManager,
    bool lockItem)
  {
    if (commandName.ToUpper().IndexOf("FORMAT.", 0) == 0 && !FormatCommandsList.Commands.Contains(commandName))
      FormatCommandsList.Commands.Add(commandName);
    MenuButtonItem menuItem1 = DocumentMenuHelper.GetMenuItem(commandName);
    if (menuItem1 == null)
    {
      menuItem1 = new MenuButtonItem(commandCaption);
      menuItem1.CommandName = commandName;
      menuItem1.ToolTipText = commandHint;
      menuItem1.Image = img;
      menuItem1.BeginGroup = beginGroup;
      menuItem1.Locked = lockItem;
      menuItem1.ShortcutActive = true;
      DocumentMenuHelper.SetMenuItem(commandName, menuItem1);
    }
    else
      menuItem1.Locked = lockItem;
    if (createContextMenuItem)
    {
      MenuButtonItem contextMenuItem = NodeContextMenu.GetContextMenuItem(commandName);
      if (contextMenuItem == null)
      {
        MenuButtonItem menuItem2 = (MenuButtonItem) menuItem1.CloneItem();
        menuItem2.Locked = lockItem;
        menuItem2.Click += new EventHandler(DocumentMenuHelper.cmi_Click);
        NodeContextMenu.SetContextMenuItem(commandName, menuItem2);
        commandManager?.Add((ButtonItemBase) menuItem1, (ButtonItemBase) menuItem2);
      }
      else
        contextMenuItem.Locked = lockItem;
    }
    else if (commandManager != null && commandManager.FindCommand(commandName) == null)
      commandManager.Add((ButtonItemBase) menuItem1);
    return menuItem1;
  }

  /// <summary>Создать пункт меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandCaption">Подпись команды</param>
  /// <param name="iconName">Имя ресурса с иконкой для пункта меню</param>
  /// <param name="commandHint">Подсказка команды</param>
  /// <param name="beginGroup">Пункт меню начинает группу</param>
  /// <param name="createContextMenuItem">Создать и пункт контекстного меню</param>
  /// <param name="commandManager">CommandManager</param>
  /// <returns>Пункт меню</returns>
  public static MenuButtonItem CreateMenuItem(
    string commandName,
    string commandCaption,
    string commandHint,
    string iconName,
    bool beginGroup,
    bool createContextMenuItem,
    ICommandManager commandManager)
  {
    return DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, iconName, beginGroup, createContextMenuItem, commandManager, false);
  }

  /// <summary>Создать пункт меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandCaption">Подпись команды</param>
  /// <param name="iconName">Имя ресурса с иконкой для пункта меню</param>
  /// <param name="commandHint">Подсказка команды</param>
  /// <param name="beginGroup">Пункт меню начинает группу</param>
  /// <param name="createContextMenuItem">Создать и пункт контекстного меню</param>
  /// <param name="commandManager">CommandManager</param>
  /// <param name="lockItem">true - запретить отображение элемента в настройке вида</param>
  /// <returns>Пункт меню</returns>
  public static MenuButtonItem CreateMenuItem(
    string commandName,
    string commandCaption,
    string commandHint,
    string iconName,
    bool beginGroup,
    bool createContextMenuItem,
    ICommandManager commandManager,
    bool lockItem)
  {
    Image img = DocumentMenuHelper.LoadImageFromResurces(iconName);
    if (img == null)
    {
      DocumentMenuHelper._namedImageList = DocumentMenuHelper._namedImageList == null ? ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList : DocumentMenuHelper._namedImageList;
      int index = DocumentMenuHelper._namedImageList != null ? DocumentMenuHelper._namedImageList.ImageIndex(iconName) : -1;
      img = index >= 0 ? DocumentMenuHelper._namedImageList.ImageList.Images[index] : (Image) null;
    }
    return DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, img, beginGroup, createContextMenuItem, commandManager, lockItem);
  }

  /// <summary>Создать пункт меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandCaption">Подпись команды</param>
  /// <param name="commandHint">Подсказка команды</param>
  /// <param name="assembly">Сборка содержащая ресурс с иконкой</param>
  /// <param name="iconName">Имя ресурса с иконкой для пункта меню</param>
  /// <param name="beginGroup">Пункт меню начинает группу</param>
  /// <param name="createContextMenuItem">Создать и пункт контекстного меню</param>
  /// <param name="commandManager">CommandManager</param>
  /// <returns>Пункт меню</returns>
  public static MenuButtonItem CreateMenuItem(
    string commandName,
    string commandCaption,
    string commandHint,
    Assembly assembly,
    string iconName,
    bool beginGroup,
    bool createContextMenuItem,
    ICommandManager commandManager)
  {
    Image img = DocumentMenuHelper.LoadImageFromResurces(assembly, iconName);
    return DocumentMenuHelper.CreateMenuItem(commandName, commandCaption, commandHint, img, beginGroup, createContextMenuItem, commandManager);
  }

  private static void cmi_Click(object sender, EventArgs e)
  {
    NodeContextMenu.ContextMenuCommand = true;
  }

  /// <summary>Создать команды меню</summary>
  /// <param name="commandManager">CommandManager</param>
  public static void CreateMenuCommands(ICommandManager commandManager)
  {
    if (DocumentMenuHelper._menuItemsCreated)
      return;
    DocumentMenuHelper._menuItemsCreated = true;
    string commandHint = "Intermech.Document.Model.Resources.";
    DocumentMenuHelper.CreateMenuItem("Undo", LocalizationHolder.rm.GetString("Document.Model_325"), "", commandHint + "Undo.bmp", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("Redo", LocalizationHolder.rm.GetString("Document.Model_326"), "", commandHint + "Redo.bmp", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("Cut", LocalizationHolder.rm.GetString("Document.Model_326"), "", commandHint + "Cut_v70.png", false, true, commandManager);
    FormatCommandsList.Commands.Add("Cut");
    DocumentMenuHelper.CreateMenuItem("Copy", LocalizationHolder.rm.GetString("Document.Model_327"), "", commandHint + "Copy_v70.png", false, true, commandManager);
    FormatCommandsList.Commands.Add("Copy");
    DocumentMenuHelper.CreateMenuItem("Paste", LocalizationHolder.rm.GetString("Document.Model_328"), "", commandHint + "Paste_v70.png", false, true, commandManager);
    FormatCommandsList.Commands.Add("Paste");
    DocumentMenuHelper.CreateMenuItem("Delete", LocalizationHolder.rm.GetString("Document.Model_325"), "", commandHint + "Delete_v70.png", false, true, commandManager);
    FormatCommandsList.Commands.Add("Delete");
    DocumentMenuHelper.CreateMenuItem("ClearFormat", LocalizationHolder.rm.GetString("Document.Model_534"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("UpdateFormulas", LocalizationHolder.rm.GetString("Document.Model_669"), "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("AddToUserDictionary", LocalizationHolder.rm.GetString("Document.Model_666"), LocalizationHolder.rm.GetString("Document.Model_667"), true, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("SelectAll", LocalizationHolder.rm.GetString("Document.Model_523"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("CopySelectedToImage", LocalizationHolder.rm.GetString("Document.Model_530"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("CopySelectedToExcel", LocalizationHolder.rm.GetString("Document.Model_653"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("CallEditor", LocalizationHolder.rm.GetString("Document.Model_329"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("BringToFront", LocalizationHolder.rm.GetString("Document.Model_330"), "", true, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("MoveToBegin", LocalizationHolder.rm.GetString("Document.Model_331"), "", true, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("MoveUp", LocalizationHolder.rm.GetString("Document.Model_332"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("MoveDown", LocalizationHolder.rm.GetString("Document.Model_333"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("MoveToEnd", LocalizationHolder.rm.GetString("Document.Model_334"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("SendToEnd", LocalizationHolder.rm.GetString("Document.Model_335"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("Find", LocalizationHolder.rm.GetString("Document.Model_632"), LocalizationHolder.rm.GetString("Document.Model_633"), "Intermech.Document.Model.Resources.FindHS.png", true, false, commandManager).Shortcut = Shortcut.CtrlF;
    DocumentMenuHelper.CreateMenuItem("Replace", LocalizationHolder.rm.GetString("Document.Model_634"), LocalizationHolder.rm.GetString("Document.Model_635"), "Intermech.Document.Model.Resources.FindAndReplace.png", false, false, commandManager).Shortcut = Shortcut.CtrlH;
    DocumentMenuHelper.CreateMenuItem("FindNext", LocalizationHolder.rm.GetString("Document.Model_636"), LocalizationHolder.rm.GetString("Document.Model_635"), "Intermech.Document.Model.Resources.FindNextHS.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("FindId", LocalizationHolder.rm.GetString("Document.Model_648"), LocalizationHolder.rm.GetString("Document.Model_648"), "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("ConvertToLabel", $"{LocalizationHolder.rm.GetString("Document.Model_336")}{LabelElement.ElementTypeName}\"", "", true, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("ConvertToTextBox", $"{LocalizationHolder.rm.GetString("Document.Model_337")}{TextBoxElement.ElementTypeName}\"", "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("ConvertToContainer", $"{LocalizationHolder.rm.GetString("Document.Model_338")}{ContainerElement.ElementTypeName}\"", "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("ConvertToArea", LocalizationHolder.rm.GetString("Document.Model_645"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("AddTableRowAbove", LocalizationHolder.rm.GetString("Document.Model_339"), "", commandHint + "InsertRowAbove.png", true, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("AddTableRowBelow", LocalizationHolder.rm.GetString("Document.Model_340"), "", commandHint + "InsertRowBelow.png", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("AddRowFromTemplateAbove", LocalizationHolder.rm.GetString("Document.Model_341"), commandHint, false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("AddRowFromTemplateBelow", LocalizationHolder.rm.GetString("Document.Model_342"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("AddTableColumnLeft", LocalizationHolder.rm.GetString("Document.Model_343"), "", commandHint + "InsertColumnLeft.png", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("AddTableColumnRight", LocalizationHolder.rm.GetString("Document.Model_344"), "", commandHint + "InsertColumnRight.png", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("AddTableCell", LocalizationHolder.rm.GetString("Document.Model_603"), "", commandHint + "InsertColumnLeft.png", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("SplitCell", LocalizationHolder.rm.GetString("Document.Model_345"), "", commandHint + "TableSplitCells.png", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("RemoveRow", LocalizationHolder.rm.GetString("Document.Model_347"), "", commandHint + "RemoveRows.png", true, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("RemoveColumn", LocalizationHolder.rm.GetString("Document.Model_348"), "", commandHint + "RemoveColumns.png", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("RemoveCell", LocalizationHolder.rm.GetString("Document.Model_349"), "", commandHint + "RemoveCellsProperties.bmp", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("ConvertToHeader", LocalizationHolder.rm.GetString("Document.Model_350"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("UpdateTable", LocalizationHolder.rm.GetString("Document.Model_352"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("ApplyPreviousTable", LocalizationHolder.rm.GetString("Document.Model_646"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("SelectContinuationTable", LocalizationHolder.rm.GetString("Document.Model_657"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("ChangeVisibility", LocalizationHolder.rm.GetString("Document.Model_518"), "", true, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("LoadOleFile", LocalizationHolder.rm.GetString("Document.Model_353"), LocalizationHolder.rm.GetString("Document.Model_354"), false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("CreateOleObject", LocalizationHolder.rm.GetString("Document.Model_355"), LocalizationHolder.rm.GetString("Document.Model_356"), false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("SaveImageToFile", LocalizationHolder.rm.GetString("Document.Model_357"), LocalizationHolder.rm.GetString("Document.Model_358"), false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("Navigation.FirstPage", LocalizationHolder.rm.GetString("Document.Model_541"), LocalizationHolder.rm.GetString("Document.Model_542"), commandHint + "FirstPage.png", false, false, commandManager);
    NavigationCommandsList.Commands.Add("Navigation.FirstPage");
    DocumentMenuHelper.CreateMenuItem("Navigation.PrevPage", LocalizationHolder.rm.GetString("Document.Model_546"), LocalizationHolder.rm.GetString("Document.Model_547"), commandHint + "PrevPage.png", false, false, commandManager);
    NavigationCommandsList.Commands.Add("Navigation.PrevPage");
    DocumentMenuHelper.CreateMenuItem("Navigation.NextPage", LocalizationHolder.rm.GetString("Document.Model_543"), LocalizationHolder.rm.GetString("Document.Model_544"), commandHint + "NextPage.png", false, false, commandManager);
    NavigationCommandsList.Commands.Add("Navigation.NextPage");
    DocumentMenuHelper.CreateMenuItem("Navigation.LastPage", LocalizationHolder.rm.GetString("Document.Model_548"), LocalizationHolder.rm.GetString("Document.Model_549"), commandHint + "LastPage.png", false, false, commandManager);
    NavigationCommandsList.Commands.Add("Navigation.LastPage");
    DocumentMenuHelper.CreateMenuItem("Navigation.PrevDocument", LocalizationHolder.rm.GetString("Document.Model_551"), LocalizationHolder.rm.GetString("Document.Model_552"), commandHint + "DocumentPrev.png", true, false, commandManager);
    NavigationCommandsList.Commands.Add("Navigation.PrevDocument");
    DocumentMenuHelper.CreateMenuItem("Navigation.NextDocument", LocalizationHolder.rm.GetString("Document.Model_553"), LocalizationHolder.rm.GetString("Document.Model_554"), commandHint + "DocumentNext.png", false, false, commandManager);
    NavigationCommandsList.Commands.Add("Navigation.NextDocument");
    NavigationCommandsList.Commands.Add("Navigation.GoToDocument");
    NavigationCommandsList.Commands.Add("Navigation.GoToPage");
    DocumentMenuHelper.CreateMenuItem("Redline.Edit", LocalizationHolder.rm.GetString("Document.Model_661"), LocalizationHolder.rm.GetString("Document.Model_661"), "imgRedEdit", true, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Redline.CompleteEdit", LocalizationHolder.rm.GetString("Document.Model_662"), LocalizationHolder.rm.GetString("Document.Model_662"), "imgFullScreen", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Tree.Update", LocalizationHolder.rm.GetString("Document.Model_664"), LocalizationHolder.rm.GetString("Document.Model_665"), false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.Font.SetupFont", LocalizationHolder.rm.GetString("Document.Model_359"), LocalizationHolder.rm.GetString("Document.Model_360"), commandHint + "font.png", true, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.SetupParagraph", LocalizationHolder.rm.GetString("Document.Model_361"), LocalizationHolder.rm.GetString("Document.Model_362"), commandHint + "Format-Paragraph-3.png", true, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.SetupList", LocalizationHolder.rm.GetString("Document.Model_363"), LocalizationHolder.rm.GetString("Document.Model_364"), commandHint + "Bullets1.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.SetupBordersAndBackground", LocalizationHolder.rm.GetString("Document.Model_365"), LocalizationHolder.rm.GetString("Document.Model_366"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.SetupTextDirrection", LocalizationHolder.rm.GetString("Document.Model_367"), LocalizationHolder.rm.GetString("Document.Model_368"), commandHint + "TextDirrection1.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.TextAlignLeft", LocalizationHolder.rm.GetString("Document.Model_369"), LocalizationHolder.rm.GetString("Document.Model_370"), commandHint + "Align-Left-3.png", true, false, commandManager).Shortcut = Shortcut.CtrlL;
    DocumentMenuHelper.CreateMenuItem("Format.TextAlignCenter", LocalizationHolder.rm.GetString("Document.Model_371"), LocalizationHolder.rm.GetString("Document.Model_372"), commandHint + "Align-Centre-3.png", false, false, commandManager).Shortcut = Shortcut.CtrlE;
    DocumentMenuHelper.CreateMenuItem("Format.TextAlignRight", LocalizationHolder.rm.GetString("Document.Model_373"), LocalizationHolder.rm.GetString("Document.Model_374"), commandHint + "Align-Right-3.png", false, false, commandManager).Shortcut = Shortcut.CtrlR;
    DocumentMenuHelper.CreateMenuItem("Format.TextAlignJustify", LocalizationHolder.rm.GetString("Document.Model_375"), LocalizationHolder.rm.GetString("Document.Model_376"), commandHint + "Align-Full-3.png", false, false, commandManager).Shortcut = Shortcut.CtrlJ;
    MenuButtonItem menuItem1 = DocumentMenuHelper.CreateMenuItem("Format.TextSpaceBetweenLines", LocalizationHolder.rm.GetString("Document.Model_377"), LocalizationHolder.rm.GetString("Document.Model_378"), commandHint + "LineSpacing.png", false, false, commandManager);
    menuItem1.Tag = (object) -1;
    MenuButtonItem menuItem2 = DocumentMenuHelper.CreateMenuItem("Format.TextSpaceBetweenLines.1,0", "1,0", LocalizationHolder.rm.GetString("Document.Model_379"), false, false, commandManager);
    menuItem2.Tag = (object) 0;
    menuItem1.Items.Add((ToolbarItemBase) menuItem2);
    MenuButtonItem menuItem3 = DocumentMenuHelper.CreateMenuItem("Format.TextSpaceBetweenLines.1,5", "1,5", LocalizationHolder.rm.GetString("Document.Model_380"), false, false, commandManager);
    menuItem3.Tag = (object) 50;
    menuItem1.Items.Add((ToolbarItemBase) menuItem3);
    MenuButtonItem menuItem4 = DocumentMenuHelper.CreateMenuItem("Format.TextSpaceBetweenLines.2,0", "2,0", LocalizationHolder.rm.GetString("Document.Model_381"), false, false, commandManager);
    menuItem4.Tag = (object) 100;
    menuItem1.Items.Add((ToolbarItemBase) menuItem4);
    MenuButtonItem menuItem5 = DocumentMenuHelper.CreateMenuItem("Format.TextSpaceBetweenLines.2,5", "2,5", LocalizationHolder.rm.GetString("Document.Model_382"), false, false, commandManager);
    menuItem5.Tag = (object) 150;
    menuItem1.Items.Add((ToolbarItemBase) menuItem5);
    MenuButtonItem menuItem6 = DocumentMenuHelper.CreateMenuItem("Format.TextSpaceBetweenLines.3,0", "3,0", LocalizationHolder.rm.GetString("Document.Model_383"), false, false, commandManager);
    menuItem6.Tag = (object) 300;
    menuItem1.Items.Add((ToolbarItemBase) menuItem6);
    DocumentMenuHelper.CreateMenuItem("Format.NumberingList", LocalizationHolder.rm.GetString("Document.Model_384"), LocalizationHolder.rm.GetString("Document.Model_385"), commandHint + "Numbers1.png", true, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.BulletsList", LocalizationHolder.rm.GetString("Document.Model_386"), LocalizationHolder.rm.GetString("Document.Model_387"), commandHint + "Bullets1.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.Font.TextBold", LocalizationHolder.rm.GetString("Document.Model_388"), LocalizationHolder.rm.GetString("Document.Model_389"), commandHint + "Bold1.png", true, false, commandManager).Shortcut = Shortcut.CtrlB;
    DocumentMenuHelper.CreateMenuItem("Format.Font.TextCursive", LocalizationHolder.rm.GetString("Document.Model_390"), LocalizationHolder.rm.GetString("Document.Model_391"), commandHint + "kursive1.png", false, false, commandManager).Shortcut = Shortcut.CtrlI;
    DocumentMenuHelper.CreateMenuItem("Format.Font.TextUnderline", LocalizationHolder.rm.GetString("Document.Model_392"), LocalizationHolder.rm.GetString("Document.Model_393"), commandHint + "Underline1.png", false, false, commandManager).Shortcut = Shortcut.CtrlU;
    DocumentMenuHelper.CreateMenuItem("Format.Font.TextUnderlineDouble", LocalizationHolder.rm.GetString("Document.Model_392"), LocalizationHolder.rm.GetString("Document.Model_393"), commandHint + "Underline1.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.Font.Strikeout", LocalizationHolder.rm.GetString("Document.Model_649"), LocalizationHolder.rm.GetString("Document.Model_650"), commandHint + "strikeout.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.Font.StrikeoutDouble", LocalizationHolder.rm.GetString("Document.Model_651"), LocalizationHolder.rm.GetString("Document.Model_652"), commandHint + "strikeoutdouble.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.Font.Subscript", LocalizationHolder.rm.GetString("Document.Model_394"), LocalizationHolder.rm.GetString("Document.Model_395"), commandHint + "Subscript.png", true, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.Font.Superscript", LocalizationHolder.rm.GetString("Document.Model_396"), LocalizationHolder.rm.GetString("Document.Model_397"), commandHint + "Superscript.png", false, false, commandManager);
    MenuButtonItem menuItem7 = DocumentMenuHelper.CreateMenuItem("Format.Font.Registr", LocalizationHolder.rm.GetString("Document.Model_597"), LocalizationHolder.rm.GetString("Document.Model_597"), commandHint + "Registr.png", true, false, commandManager);
    menuItem7.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Font.Registr.LowerCase", LocalizationHolder.rm.GetString("Document.Model_598"), LocalizationHolder.rm.GetString("Document.Model_598"), "", true, false, commandManager));
    menuItem7.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Font.Registr.UpperCase", LocalizationHolder.rm.GetString("Document.Model_599"), LocalizationHolder.rm.GetString("Document.Model_599"), "", false, false, commandManager));
    menuItem7.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Font.Registr.BeginFromUpperCase", LocalizationHolder.rm.GetString("Document.Model_600"), LocalizationHolder.rm.GetString("Document.Model_600"), "", false, false, commandManager));
    menuItem7.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Font.Registr.Invert", LocalizationHolder.rm.GetString("Document.Model_601"), LocalizationHolder.rm.GetString("Document.Model_601"), "", false, false, commandManager));
    DocumentMenuHelper.CreateMenuItem("Format.DecreaseIdent", LocalizationHolder.rm.GetString("Document.Model_398"), LocalizationHolder.rm.GetString("Document.Model_399"), commandHint + "Indent-Decrease.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.IncreaseIdent", LocalizationHolder.rm.GetString("Document.Model_400"), LocalizationHolder.rm.GetString("Document.Model_401"), commandHint + "Indent-Increase.png", false, false, commandManager);
    MenuButtonItem menuItem8 = DocumentMenuHelper.CreateMenuItem("Format.Borders", LocalizationHolder.rm.GetString("Document.Model_402"), LocalizationHolder.rm.GetString("Document.Model_403"), commandHint + "BordersOuter.gif", false, false, commandManager);
    BordersCommandsList.Commands.Clear();
    BordersCommandsList.Commands.Add("Format.Borders");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Outer", LocalizationHolder.rm.GetString("Document.Model_404"), LocalizationHolder.rm.GetString("Document.Model_405"), commandHint + "BordersOuter.gif", true, false, commandManager));
    DocumentMenuHelper.ActiveBordersCommand = "Format.Borders.Outer";
    BordersCommandsList.Commands.Add("Format.Borders.Outer");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.All", LocalizationHolder.rm.GetString("Document.Model_406"), LocalizationHolder.rm.GetString("Document.Model_407"), commandHint + "BordersAll.gif", false, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.All");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Inner", LocalizationHolder.rm.GetString("Document.Model_408"), LocalizationHolder.rm.GetString("Document.Model_409"), commandHint + "BordersInner.gif", false, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.Inner");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Delete", LocalizationHolder.rm.GetString("Document.Model_410"), LocalizationHolder.rm.GetString("Document.Model_411"), commandHint + "BordersDelete.gif", false, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.Delete");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Left", LocalizationHolder.rm.GetString("Document.Model_412"), LocalizationHolder.rm.GetString("Document.Model_413"), commandHint + "BorderLeft.gif", true, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.Left");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Top", LocalizationHolder.rm.GetString("Document.Model_414"), LocalizationHolder.rm.GetString("Document.Model_415"), commandHint + "BorderTop.gif", false, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.Top");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Right", LocalizationHolder.rm.GetString("Document.Model_416"), LocalizationHolder.rm.GetString("Document.Model_417"), commandHint + "BorderRight.gif", false, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.Right");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Bottom", LocalizationHolder.rm.GetString("Document.Model_418"), LocalizationHolder.rm.GetString("Document.Model_419"), commandHint + "BorderBottom.gif", false, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.Bottom");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Horisontal", LocalizationHolder.rm.GetString("Document.Model_420"), LocalizationHolder.rm.GetString("Document.Model_421"), commandHint + "BordersHorizontal.gif", true, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.Horisontal");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Vertical", LocalizationHolder.rm.GetString("Document.Model_422"), LocalizationHolder.rm.GetString("Document.Model_423"), commandHint + "BordersVertical.gif", false, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.Vertical");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Diagonal.TopLeft", LocalizationHolder.rm.GetString("Document.Model_424"), LocalizationHolder.rm.GetString("Document.Model_425"), commandHint + "BordersDiagonalTopLeft.gif", true, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.TopLeft");
    menuItem8.Items.Add((ToolbarItemBase) DocumentMenuHelper.CreateMenuItem("Format.Borders.Diagonal.TopRight", LocalizationHolder.rm.GetString("Document.Model_426"), LocalizationHolder.rm.GetString("Document.Model_427"), commandHint + "BordersDiagonalTopRight.gif", false, false, commandManager));
    BordersCommandsList.Commands.Add("Format.Borders.TopRight");
    DocumentMenuHelper.CreateMenuItem("Format.CellAlign", LocalizationHolder.rm.GetString("Document.Model_428"), LocalizationHolder.rm.GetString("Document.Model_429"), commandHint + "CA-Left-Top.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.BgColor", LocalizationHolder.rm.GetString("Document.Model_454"), LocalizationHolder.rm.GetString("Document.Model_455"), commandHint + "FillColor.png", true, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.TextBkColor", LocalizationHolder.rm.GetString("Document.Model_456"), LocalizationHolder.rm.GetString("Document.Model_457"), commandHint + "SelectionColor.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.TextColor", LocalizationHolder.rm.GetString("Document.Model_458"), LocalizationHolder.rm.GetString("Document.Model_459"), commandHint + "FontColor.png", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Format.Borders.Color", LocalizationHolder.rm.GetString("Document.Model_531"), LocalizationHolder.rm.GetString("Document.Model_532"), commandHint + "LineColor.bmp", false, false, commandManager);
    DocumentMenuHelper.miInsertPageBefore = DocumentMenuHelper.CreateMenuItem("NewPageBefore", LocalizationHolder.rm.GetString("Document.Model_461"), "", true, false, commandManager);
    DocumentMenuHelper.miInsertPageAfter = DocumentMenuHelper.CreateMenuItem("NewPageAfter", LocalizationHolder.rm.GetString("Document.Model_495"), "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("CreateNextPageTemplate", LocalizationHolder.rm.GetString("Document.Model_462"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("RemovePage", LocalizationHolder.rm.GetString("Document.Model_463"), "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("PrevPage", LocalizationHolder.rm.GetString("Document.Model_464"), "", true, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("NextPage", LocalizationHolder.rm.GetString("Document.Model_465"), "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("DocEditor.InsertAdditionalPages", LocalizationHolder.rm.GetString("Document.Model_659"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("DocEditor.RemoveAdditionalPages", LocalizationHolder.rm.GetString("Document.Model_660"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("DocEditor.ChangePageNumberingStyle", LocalizationHolder.rm.GetString("Document.Model_658"), "", false, true, commandManager);
    DocumentMenuHelper.CreateMenuItem("Zoom200", "200%", "", true, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Zoom100", "100%", "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Zoom75", "75%", "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Zoom50", "50%", "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("ZoomFitWidth", LocalizationHolder.rm.GetString("Document.Model_466"), "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("ZoomFitPage", LocalizationHolder.rm.GetString("Document.Model_467"), "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.GridSize_1", LocalizationHolder.rm.GetString("Document.Model_468"), LocalizationHolder.rm.GetString("Document.Model_469"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.GridSize_0.5", LocalizationHolder.rm.GetString("Document.Model_470"), LocalizationHolder.rm.GetString("Document.Model_471"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.GridSize_0.1", LocalizationHolder.rm.GetString("Document.Model_472"), LocalizationHolder.rm.GetString("Document.Model_473"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.GridSize_0.05", LocalizationHolder.rm.GetString("Document.Model_474"), LocalizationHolder.rm.GetString("Document.Model_475"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.CoorSystem_BottomLeft", LocalizationHolder.rm.GetString("Document.Model_476"), LocalizationHolder.rm.GetString("Document.Model_477"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.CoorSystem_TopLeft", LocalizationHolder.rm.GetString("Document.Model_478"), LocalizationHolder.rm.GetString("Document.Model_479"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.CoorSystem_TopRight", LocalizationHolder.rm.GetString("Document.Model_480"), LocalizationHolder.rm.GetString("Document.Model_481"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.CoorSystem_BottomRight", LocalizationHolder.rm.GetString("Document.Model_482"), LocalizationHolder.rm.GetString("Document.Model_483"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Doc.CoorSystem_Custom", LocalizationHolder.rm.GetString("Document.Model_484"), LocalizationHolder.rm.GetString("Document.Model_485"), false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Specification.AddNewRecord", LocalizationHolder.rm.GetString("Document.Model_486"), "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Specification.InsertExistRecord", "", "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Specification.InsertImbaseRecord", "", "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Specification.CopyRecord", "", "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Specification.InsertSection", "", "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Specification.RecordProperties", "", "", false, false, commandManager);
    DocumentMenuHelper.CreateMenuItem("Specification.OpenInNewWindow", "", "", false, false, commandManager);
  }

  /// <summary>Получить пункт меню</summary>
  /// <param name="commandName">Имя команды</param>
  /// <returns>Пункт меню</returns>
  public static MenuButtonItem GetMenuItem(string commandName)
  {
    return commandName != null ? (MenuButtonItem) DocumentMenuHelper.menuDictionary[(object) commandName] : throw new ArgumentNullException(nameof (commandName));
  }

  /// <summary>Назначить пункт меню для команды</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="menuItem">Пункт меню</param>
  public static void SetMenuItem(string commandName, MenuButtonItem menuItem)
  {
    if (commandName == null)
      throw new ArgumentNullException(nameof (commandName));
    DocumentMenuHelper.menuDictionary[(object) commandName] = (object) menuItem;
  }

  /// <summary>Загрузить изображение из ресурса</summary>
  /// <param name="resourceName">Имя ресурса</param>
  /// <returns>Изображение</returns>
  public static Image LoadImageFromResurces(string resourceName)
  {
    return !DocumentTreeNode.IsEmptyString(resourceName) ? DocumentMenuHelper.LoadImageFromResurces(typeof (DocumentMenuHelper).Assembly, resourceName) : (Image) null;
  }

  /// <summary>Загрузить изображение из ресурса</summary>
  /// <param name="assembly">Сборка содержащая ресурс с иконкой</param>
  /// <param name="resourceName">Имя ресурса</param>
  /// <returns>Изображение</returns>
  public static Image LoadImageFromResurces(Assembly assembly, string resourceName)
  {
    Image image = (Image) null;
    if (!DocumentTreeNode.IsEmptyString(resourceName))
    {
      Stream manifestResourceStream = assembly.GetManifestResourceStream(resourceName);
      if (manifestResourceStream != null)
      {
        image = Image.FromStream(manifestResourceStream);
        if (image.RawFormat.Equals((object) ImageFormat.Bmp) && image is Bitmap bitmap)
          bitmap.MakeTransparent();
      }
    }
    return image;
  }

  [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
  private static extern void SetThemeAppProperties(int Flags);

  public static void SilentRecoverVisualStyle(VisualStyleState visualStyleState)
  {
    if (visualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled && (!VisualStyleInformation.IsSupportedByOS || VisualStyleRenderer.IsSupported && VisualStyleInformation.IsEnabledByUser))
      return;
    DocumentMenuHelper.SetThemeAppProperties((int) visualStyleState);
  }

  public void Dispose()
  {
    this.CbDocument.Items.Clear();
    this.CbPage.Items.Clear();
  }
}
