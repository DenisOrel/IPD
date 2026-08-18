// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.NodeClipboardHelper
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.RtfEditor;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Вспомогательный класс для помещения узлов в Clipboard и извлечения их оттуда</summary>
[Serializable]
public class NodeClipboardHelper
{
  /// <summary>Поместить узлы в буфер обмена Windows</summary>
  /// <param name="nodeArray">Массив узлов</param>
  /// <param name="tag">Доп информация</param>
  public static void CopyToClipboard(DocumentTreeNode[] nodeArray, IntPtr hWnd, object tag = null)
  {
    try
    {
      if (nodeArray == null || nodeArray.Length == 0)
        return;
      DocumentTreeNode[] nodesWithoutChilds = DocumentTreeNode.GetNodesWithoutChilds(nodeArray, true);
      DataObject data1 = new DataObject();
      DocumentNodesClipboardData data2 = new DocumentNodesClipboardData(nodesWithoutChilds);
      data1.SetData(DocumentNodesClipboardData.ClipboardFormat, false, (object) data2);
      NodeClipboardInfo[] nodesInfo = new NodeClipboardInfo[nodesWithoutChilds.Length];
      for (int index = 0; index < nodesWithoutChilds.Length; ++index)
        nodesInfo[index] = new NodeClipboardInfo(nodesWithoutChilds[index]);
      data1.SetData(ClipboardDataAdditionalInfo.ClipboardFormat, false, (object) new ClipboardDataAdditionalInfo(nodesInfo)
      {
        Tag = tag
      });
      Clipboard.SetDataObject((object) data1, true);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
    }
  }

  /// <summary>Получить информацию об узлах хранящихся в буфере</summary>
  /// <returns>Информация об узлах хранящихся в буфере</returns>
  public static ClipboardDataAdditionalInfo GetClipboardInfo()
  {
    ClipboardDataAdditionalInfo clipboardInfo = (ClipboardDataAdditionalInfo) null;
    IDataObject dataObject;
    try
    {
      dataObject = Clipboard.GetDataObject();
    }
    catch
    {
      dataObject = (IDataObject) null;
    }
    if (dataObject != null)
    {
      string[] formats = dataObject.GetFormats();
      for (int index = 0; index < formats.Length; ++index)
      {
        if (formats[index] == ClipboardDataAdditionalInfo.ClipboardFormat)
        {
          clipboardInfo = dataObject.GetData(formats[index]) as ClipboardDataAdditionalInfo;
          break;
        }
      }
    }
    return clipboardInfo;
  }

  /// <summary>Можно вставить узлы из буфера Windows</summary>
  /// <param name="destination">Приемник узлов</param>
  /// <returns>Можно вставить узлы из буфера Windows</returns>
  public static bool CanPasteFromClipboard(DocumentTreeNode destination)
  {
    return NodeClipboardHelper.CanPasteFromClipboard(destination, out PasteType _);
  }

  /// <summary>Можно вставить узлы из буфера Windows</summary>
  /// <param name="destination">Приемник узлов</param>
  /// <param name="from">В какое приложение происходит вставка</param>
  /// <param name="type">тип вставляемых данных</param>
  /// <returns>Можно вставить узлы из буфера Windows</returns>
  public static bool CanPasteFromClipboard(DocumentTreeNode destination, out PasteType type)
  {
    if (destination == null)
      throw new ArgumentNullException(nameof (destination));
    bool flag1 = false;
    ContainerElement containerElement = destination as ContainerElement;
    type = PasteType.OleData;
    if (containerElement != null)
    {
      flag1 = containerElement.CanPasteFromClipboard();
      type = PasteType.OleData;
    }
    else
    {
      bool flag2 = true;
      ClipboardDataAdditionalInfo clipboardInfo = NodeClipboardHelper.GetClipboardInfo();
      if (clipboardInfo != null)
      {
        NodeClipboardInfo[] nodesInfo = clipboardInfo.NodesInfo;
        if (nodesInfo != null && destination is RectangleElement && ((destination as RectangleElement).IsTableCell || destination is TableElement))
        {
          for (int index = 0; index < nodesInfo.Length; ++index)
          {
            if (!(nodesInfo[index].NodeType == typeof (TableElement)) && !(nodesInfo[index].NodeType == typeof (VirtualColumn)))
              flag2 = false;
            if (!(destination is RectangleElement))
              flag2 = false;
            if (destination is TableData && (destination as TableData).IsVirtualNode)
              flag2 = false;
          }
          type = PasteType.Nodes;
          return flag2;
        }
      }
      if (clipboardInfo != null)
      {
        DocumentTreeNode documentTreeNode = (DocumentTreeNode) (destination as ImDocumentData);
        System.Type type1 = typeof (PageData);
        for (int index = 0; !flag1 && index < clipboardInfo.NodesInfo.Length; ++index)
        {
          System.Type nodeType = clipboardInfo.NodesInfo[index].NodeType;
          if (type1.IsAssignableFrom(nodeType))
          {
            if (documentTreeNode == null)
            {
              if (destination is PageElementNode pageElementNode && pageElementNode.Page != null)
                documentTreeNode = pageElementNode.Page.Parent;
              else if (destination is PageData)
                documentTreeNode = destination.Parent;
            }
            if (documentTreeNode != null)
              flag1 |= documentTreeNode.CanAddChildElement(type1);
          }
          else
            flag1 |= destination.CanPasteFromClipboard(clipboardInfo.NodesInfo[index]);
        }
        type = PasteType.Nodes;
      }
      else if (destination is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl)
      {
        int num = placeEditorControl.TerMenuEnable(630);
        type = PasteType.Text;
        return num == 0;
      }
    }
    return flag1;
  }

  /// <summary>Вставить из буфера Windows</summary>
  /// <param name="destination">Приемник</param>
  public static void PasteFromClipboard(DocumentTreeNode destination, IntPtr hWnd)
  {
    NodeClipboardHelper.PasteFromClipboard(destination, hWnd, (DocumentTreeNode[]) null);
  }

  /// <summary>Вставить из буфера Windows</summary>
  /// <param name="destination">Приемник</param>
  public static void PasteFromClipboard(
    DocumentTreeNode destination,
    IntPtr hWnd,
    DocumentTreeNode[] nodes)
  {
    if (destination is ContainerElement containerElement)
    {
      containerElement.PasteFromClipboard(hWnd);
      containerElement.CheckOriginalSizeAndAskUser();
    }
    else
    {
      DocumentNodesClipboardData nodesClipboardData = nodes != null ? new DocumentNodesClipboardData(nodes) : NodeClipboardHelper.GetNodesFromClipboard();
      if (nodesClipboardData == null && destination is TextBoxElement textBoxElement && textBoxElement.InPlaceEditorActive && textBoxElement.InPlaceEditorControl is ImRtfEditor placeEditorControl)
      {
        placeEditorControl.TerCommand(630);
      }
      else
      {
        TableElement tableElement = destination as TableElement;
        DocumentTreeNode documentTreeNode1 = (DocumentTreeNode) null;
        visualNode = (VisualNode) null;
        bool flag1 = false;
        bool flag2 = false;
        try
        {
          documentTreeNode1 = DocumentMenuHelper.FindDocumentForSuspend(destination);
          if (documentTreeNode1 != null)
          {
            if (!documentTreeNode1.SuspendedUpdateLayoutFlag)
            {
              flag1 = true;
              documentTreeNode1.SuspendUpdateLayout();
            }
            if (documentTreeNode1 is VisualNode visualNode && (!visualNode.SuspendedRefreshUIFlag || !visualNode.SuspendedUpdateUIGeometryFlag))
            {
              flag2 = true;
              visualNode.SuspendUpdateGeometryRefreshUI();
            }
          }
          int hashCode = destination.GetHashCode();
          DocumentTreeNode documentTreeNode2 = (DocumentTreeNode) null;
          int num = -1;
          if (nodesClipboardData == null)
            return;
          for (int index = 0; index < nodesClipboardData.Nodes.Length; ++index)
          {
            VirtualColumn node1 = nodesClipboardData.Nodes[index] as VirtualColumn;
            if (tableElement != null && node1 != null)
            {
              int columnIndex = 0;
              List<RowColParams> gridColumnsParams = tableElement.GridColumnsParams;
              if (gridColumnsParams != null)
                columnIndex = gridColumnsParams.Count;
              tableElement.InsertColumn(columnIndex, node1, false, false);
            }
            else
            {
              if (nodesClipboardData.Nodes[index] is PageData node2)
              {
                if (num == -1)
                {
                  if (destination is PageElementNode pageElementNode && pageElementNode.Page != null)
                  {
                    num = pageElementNode.Page.Index;
                    documentTreeNode2 = pageElementNode.Page.Parent;
                  }
                  else if (destination is Page)
                  {
                    num = destination.Index;
                    documentTreeNode2 = destination.Parent;
                  }
                }
                if (documentTreeNode2 != null && documentTreeNode2.CanAddChildElement((DocumentTreeNode) node2))
                  documentTreeNode2.InsertChildNode(destination.Index + 1, (DocumentTreeNode) node2, false, true, false, false);
              }
              if (destination is TableData tableData1 && tableData1.IsFixedStructureArea)
              {
                nodesClipboardData.Nodes[index].AssignClonedByTemplateWithParent(false);
                if (hashCode != nodesClipboardData.ParentHashCodes[index])
                  nodesClipboardData.Nodes[index].ResetInheritance();
                if (nodesClipboardData.Nodes[index] is TableData node3 && destination.OwnerDocument != null && node3.IsPageFlow)
                  node3.IsPageFlow = false;
                destination.AddChildNode(nodesClipboardData.Nodes[index], false, false);
                if (nodesClipboardData.Nodes[index] is RectangleElement node4)
                  node4.setProperBounds(new RectangleF(node4.bounds.X - tableData1.bounds.X, node4.bounds.Y - tableData1.bounds.Y, node4.bounds.Width, node4.bounds.Height));
                nodesClipboardData.Nodes[index].UpdateTemplateLinks(false, true, false, false);
              }
              else if (nodesClipboardData.Nodes[index] is TableData node5 && node5.IsRow && destination is RectangleElement)
              {
                TableData ownerSubTable = (destination as RectangleElement).OwnerSubTable;
                TableData ownerRow = (destination as RectangleElement).OwnerRow;
                nodesClipboardData.Nodes[index].AssignClonedByTemplateWithParent(false);
                if (hashCode != nodesClipboardData.ParentHashCodes[index])
                  nodesClipboardData.Nodes[index].ResetInheritance();
                if (ownerRow == null)
                  ownerSubTable.AddChildNode(nodesClipboardData.Nodes[index], false, false);
                else
                  ownerSubTable.InsertChildNode(ownerRow.Index, nodesClipboardData.Nodes[index], false, true, false, false, false);
                nodesClipboardData.Nodes[index].UpdateTemplateLinks(false, true, false, false);
              }
              else if (destination.CanAddChildElement(nodesClipboardData.Nodes[index]))
              {
                DocumentTreeNode child1 = nodesClipboardData.Nodes[index];
                TableData child2 = child1 as TableData;
                child1.AssignClonedByTemplateWithParent(false);
                if (hashCode != nodesClipboardData.ParentHashCodes[index])
                  nodesClipboardData.Nodes[index].ResetInheritance();
                if (child2 != null && child2.IsRow && destination is PageData)
                {
                  TableData tableData = child2.Clone(false, false) as TableData;
                  tableData.IsColumn = true;
                  tableData.AddChildNode((DocumentTreeNode) child2, false, false);
                  child1 = (DocumentTreeNode) tableData;
                }
                ImDocumentData ownerDocument = destination.OwnerDocument;
                if (ownerDocument != null && child2 != null && child2.IsPageFlow && child2.FlowID != null)
                {
                  FlowID flowIdByName = ownerDocument.FindFlowIDByName(child2.FlowID);
                  if (flowIdByName != null && flowIdByName != child2.FlowID)
                    child2.SetFlowID(flowIdByName, false, false);
                }
                destination.AddChildNode(child1, false, false);
              }
            }
          }
        }
        finally
        {
          if (flag1)
            documentTreeNode1.ResumeUpdateLayout(false, true);
          if (flag2)
            visualNode.ResumeUpdateRefreshUI(true, true);
        }
      }
    }
  }

  /// <summary>Получить узлы из буфера</summary>
  /// <returns>Узлы из буфера</returns>
  public static DocumentNodesClipboardData GetNodesFromClipboard()
  {
    DocumentNodesClipboardData nodesFromClipboard = (DocumentNodesClipboardData) null;
    IDataObject dataObject = Clipboard.GetDataObject();
    if (dataObject != null && ((IEnumerable<string>) dataObject.GetFormats()).Contains<string>(DocumentNodesClipboardData.ClipboardFormat))
    {
      nodesFromClipboard = (DocumentNodesClipboardData) dataObject.GetData(DocumentNodesClipboardData.ClipboardFormat);
      if (nodesFromClipboard != null && nodesFromClipboard.Nodes != null)
      {
        foreach (DocumentTreeNode node in nodesFromClipboard.Nodes)
        {
          node.AssignClonedByTemplateWithParent(false);
          node.ClearExternalLinks((IEnumerable<DocumentTreeNode>) nodesFromClipboard.Nodes);
        }
      }
    }
    return nodesFromClipboard;
  }
}
