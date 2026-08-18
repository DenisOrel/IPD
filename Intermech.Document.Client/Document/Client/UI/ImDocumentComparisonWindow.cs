// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.UI.ImDocumentComparisonWindow
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Document.Client.Comparison;
using Intermech.Document.Model;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.UI;

public class ImDocumentComparisonWindow : DockControl
{
  protected UCFilesComparison ucFilesComparison;
  protected Panel panel1;
  private Panel panelDifferences;
  private SplitContainer splitContainerlDocuments;
  private TextBox txtDifferences;
  private Intermech.VirtualTreeView.VirtualTreeView treeViewDifferences;
  private SplitContainer splitContainerDifferences;
  private DocumentControl docControl1;
  private DocumentControl docControl2;
  private Style nodeAbsentStyle1;
  private Style nodeAbsentStyle2;
  private Style nodeDifferentStyle;
  private Style nodeNormalStyle;
  private static Dictionary<string, Icon> _treeIcons;

  private static Dictionary<string, Icon> TreeIcons
  {
    get
    {
      if (ImDocumentComparisonWindow._treeIcons == null)
        ImDocumentComparisonWindow._treeIcons = new Dictionary<string, object>()
        {
          {
            typeof (ImDocument).Name,
            (ImDocument.Icon as Bitmap).Clone()
          },
          {
            typeof (Page).Name,
            (Page.Icon as Bitmap).Clone()
          },
          {
            typeof (TextBoxElement).Name,
            (TextBoxCreator.Icon as Bitmap).Clone()
          },
          {
            typeof (LabelElement).Name,
            (LabelCreator.Icon as Bitmap).Clone()
          },
          {
            typeof (TableElement).Name,
            (TableCreator.Icon as Bitmap).Clone()
          },
          {
            typeof (Polyline).Name,
            (PolylineCreator.Icon as Bitmap).Clone()
          },
          {
            typeof (ContainerElement).Name,
            (ContainerCreator.Icon as Bitmap).Clone()
          },
          {
            "Строка",
            (TableCreator.RowIcon as Bitmap).Clone()
          }
        }.Select<KeyValuePair<string, object>, KeyValuePair<string, Icon>>((Func<KeyValuePair<string, object>, KeyValuePair<string, Icon>>) (tb => new KeyValuePair<string, Icon>(tb.Key, ImageHelper.BitmapToIcon(new Bitmap((Image) tb.Value))))).ToDictionary<KeyValuePair<string, Icon>, string, Icon>((Func<KeyValuePair<string, Icon>, string>) (t => t.Key), (Func<KeyValuePair<string, Icon>, Icon>) (t => t.Value));
      return ImDocumentComparisonWindow._treeIcons;
    }
  }

  public ImDocument DocumentOne
  {
    get => this.docControl1?.Document;
    set
    {
      if (this.docControl1 == null)
        return;
      this.docControl1.Document = value;
      double num = (double) this.docControl1.SetZoom(DocZoomMode.FitPage, 0.0f);
      this.docControl1.PageControl.OnePage = true;
      this.docControl1.ReadOnly = true;
    }
  }

  public ImDocument DocumentTwo
  {
    get => this.docControl2?.Document;
    set
    {
      if (this.docControl2 == null)
        return;
      this.docControl2.Document = value;
      double num = (double) this.docControl2.SetZoom(DocZoomMode.FitPage, 0.0f);
      this.docControl2.PageControl.OnePage = true;
      this.docControl2.ReadOnly = true;
    }
  }

  internal List<ComparisonTreeNode> ComparisonTreeDataSource
  {
    set
    {
      ComparisonTreeNode comparisonTreeNode1 = new ComparisonTreeNode((DocumentTreeNode) null);
      if (value != null && value.Count > 0)
      {
        if (value.Count > 1)
        {
          foreach (ComparisonTreeNode comparisonTreeNode2 in value)
            comparisonTreeNode2.Parent = comparisonTreeNode1;
        }
        else
          comparisonTreeNode1 = value[0];
      }
      else
      {
        ComparisonTreeNode comparisonTreeNode3 = new ComparisonTreeNode((DocumentTreeNode) null);
        comparisonTreeNode3.Text = "Различий не найдено";
        comparisonTreeNode3.Parent = comparisonTreeNode1;
      }
      this.treeViewDifferences.DataSource = (object) comparisonTreeNode1;
      this.treeViewDifferences.RootRow?.ExpandChildren(true);
      this.CollapseDocRows(this.treeViewDifferences.RootRow);
    }
  }

  public ImDocumentComparisonWindow(ObjectFileInfo fileInfo1, ObjectFileInfo fileInfo2)
  {
    if (fileInfo1 == null)
      throw new ArgumentNullException(nameof (fileInfo1));
    if (fileInfo2 == null)
      throw new ArgumentNullException(nameof (fileInfo2));
    this.InitComponents();
    this.ClientSizeChanged += (EventHandler) ((s, e) => this.splitContainerlDocuments.SplitterDistance = this.splitContainerlDocuments.Width / 2);
    this.ucFilesComparison.Init(fileInfo1, fileInfo2);
    string str;
    if (!(fileInfo1.ObjectCaption == fileInfo2.ObjectCaption))
      str = $"Сравнение документов ({fileInfo1.ObjectCaption} и {fileInfo2.ObjectCaption})";
    else
      str = $"Сравнение версий ({fileInfo1.ObjectCaption})";
    this.Text = str;
  }

  private void InitComponents()
  {
    this.ucFilesComparison = new UCFilesComparison();
    this.panel1 = new Panel();
    this.ucFilesComparison.SuspendLayout();
    this.SuspendLayout();
    this.ucFilesComparison.Location = new Point(0, 0);
    this.ucFilesComparison.Name = "ucFilesComparison";
    this.ucFilesComparison.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.ucFilesComparison.Size = new Size(694, 121);
    this.ucFilesComparison.MinimumSize = new Size(694, 120);
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 123);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(694, 283);
    this.panel1.BorderStyle = BorderStyle.FixedSingle;
    this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.TabIndex = 1;
    this.InitDifferenecesPanel();
    this.panel1.Controls.Add((Control) this.panelDifferences);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.ActiveBorder;
    this.ClientSize = new Size(694, 406);
    this.MinimumSize = new Size(694, 406);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.ucFilesComparison);
    this.Name = nameof (ImDocumentComparisonWindow);
    this.Text = "Сравнение документов";
    this.ucFilesComparison.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private Panel InitDifferenecesPanel()
  {
    this.panelDifferences = new Panel();
    this.panelDifferences.Location = new Point(0, 0);
    this.panelDifferences.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panelDifferences.BorderStyle = BorderStyle.Fixed3D;
    this.panelDifferences.Size = new Size(694, 280);
    this.splitContainerDifferences = new SplitContainer();
    this.splitContainerDifferences.Panel1.AutoScroll = true;
    this.splitContainerDifferences.Panel2.AutoScroll = true;
    this.splitContainerDifferences.Panel1.BackColor = SystemColors.Control;
    this.splitContainerDifferences.Panel1.BackColor = SystemColors.Control;
    this.splitContainerDifferences.Dock = DockStyle.Fill;
    this.splitContainerDifferences.SplitterDistance = 70;
    this.splitContainerlDocuments = new SplitContainer();
    this.splitContainerlDocuments.Panel1.AutoScroll = true;
    this.splitContainerlDocuments.Panel2.AutoScroll = true;
    this.splitContainerlDocuments.Panel1.BackColor = SystemColors.Control;
    this.splitContainerlDocuments.Panel1.BackColor = SystemColors.Control;
    this.splitContainerlDocuments.Dock = DockStyle.Fill;
    this.splitContainerlDocuments.SplitterDistance = 90;
    DocumentControl documentControl1 = new DocumentControl();
    documentControl1.Name = "docControl1";
    this.docControl1 = documentControl1;
    DocumentControl documentControl2 = new DocumentControl();
    documentControl2.Name = "docControl2";
    this.docControl2 = documentControl2;
    this.InitDocControl(this.docControl1);
    this.InitDocControl(this.docControl2);
    this.splitContainerlDocuments.Panel1.Controls.Add((Control) this.docControl1);
    this.splitContainerlDocuments.Panel2.Controls.Add((Control) this.docControl2);
    this.treeViewDifferences = this.CreateDifferencesVisualizationTree();
    this.SetTreeNodeStyles();
    this.splitContainerDifferences.Panel1.Controls.Add((Control) this.treeViewDifferences);
    this.splitContainerDifferences.Panel2.Controls.Add((Control) this.splitContainerlDocuments);
    this.panelDifferences.Controls.Add((Control) this.splitContainerDifferences);
    this.splitContainerDifferences.SplitterDistance = 120;
    return this.panelDifferences;
  }

  private void InitDocControl(DocumentControl docControl)
  {
    docControl.ActivePage = (Page) null;
    docControl.Document = (ImDocument) null;
    docControl.DocumentManager = (IImDocumentManager) null;
    docControl.DocumentsComplect = (DocumentsComplect) null;
    docControl.DocumentViewMode = DocumentViewMode.Normal;
    docControl.IsElementCreating = false;
    docControl.IsElementSelecting = true;
    docControl.QueryCache_HasLockedNodes = false;
    docControl.ReadOnly = false;
    docControl.ReadOnlyGeometry = false;
    docControl.ReadOnlyGeometryForDocument = false;
    docControl.RowSelection = false;
    docControl.SelectedElementCreator = (PageElementCreator) null;
    docControl.Dock = DockStyle.Fill;
    docControl.TabIndex = 1;
    docControl.TernEditorBuffer = (ImRtfEditor) null;
  }

  private Intermech.VirtualTreeView.VirtualTreeView CreateDifferencesVisualizationTree()
  {
    Column column = new Column();
    column.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    column.Caption = "Различия в документах";
    column.CellStyle.BorderStyle = Border3DStyle.Flat;
    column.CellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    column.HeaderStyle.HorzAlignment = StringAlignment.Near;
    column.MinWidth = 50;
    column.Movable = false;
    column.Name = "columnDifferences";
    column.Sortable = false;
    column.Width = 70;
    this.treeViewDifferences = new Intermech.VirtualTreeView.VirtualTreeView();
    this.treeViewDifferences.AllowDrop = false;
    this.treeViewDifferences.AllowMultiSelect = false;
    this.treeViewDifferences.AllowUserPinnedColumns = false;
    this.treeViewDifferences.Columns.Add(column);
    this.treeViewDifferences.DisableHeaderContextMenu = true;
    this.treeViewDifferences.EnableRowCaching = false;
    this.treeViewDifferences.ImageList = (ImageList) null;
    this.treeViewDifferences.LineStyle = LineStyle.Dot;
    this.treeViewDifferences.Dock = DockStyle.Fill;
    this.treeViewDifferences.BorderStyle = BorderStyle.Fixed3D;
    this.treeViewDifferences.MainColumn = column;
    this.treeViewDifferences.Name = "treeViewDifferences";
    this.treeViewDifferences.RowSelectedStyle.WordWrap = false;
    this.treeViewDifferences.RowStyle.BorderColor = SystemColors.Control;
    this.treeViewDifferences.RowStyle.BorderStyle = Border3DStyle.Flat;
    this.treeViewDifferences.RowStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.treeViewDifferences.RowStyle.WordWrap = false;
    this.treeViewDifferences.SelectBeforeEdit = true;
    this.treeViewDifferences.ShowRootRow = false;
    this.treeViewDifferences.TabIndex = 1;
    this.treeViewDifferences.GetChildPolicy += (GetChildPolicyHandler) ((s, e) => e.ChildPolicy = RowChildPolicy.Normal);
    this.treeViewDifferences.GetCellData += new GetCellDataHandler(this.treeViewDifferences_GetCellData);
    this.treeViewDifferences.GetChildren += new GetChildrenHandler(this.treeViewDifferences_GetChildren);
    this.treeViewDifferences.GetRowData += new GetRowDataHandler(this.treeViewDifferences_GetRowData);
    this.treeViewDifferences.MouseClick += new MouseEventHandler(this.HandleTreeViewMouseActions);
    return this.treeViewDifferences;
  }

  private void treeViewDifferences_GetRowData(object sender, GetRowDataEventArgs e)
  {
    e.RowData.Icon = ImDocumentComparisonWindow.TreeIcons.Where<KeyValuePair<string, Icon>>((Func<KeyValuePair<string, Icon>, bool>) (i =>
    {
      if (!(e.Row.Item is ComparisonTreeNode comparisonTreeNode2))
        return false;
      return i.Key == comparisonTreeNode2.TypeCaption || i.Key == comparisonTreeNode2.ClassName;
    })).Select<KeyValuePair<string, Icon>, Icon>((Func<KeyValuePair<string, Icon>, Icon>) (kvp => kvp.Value)).FirstOrDefault<Icon>();
  }

  private void HandleTreeViewMouseActions(object sender, MouseEventArgs args)
  {
    if (args.Button != MouseButtons.Left || !(sender is Control))
      return;
    ComparisonTreeNode comparisonTreeNode = (ComparisonTreeNode) null;
    Row nodeAt = this.treeViewDifferences.GetNodeAt(args.X, args.Y);
    if (nodeAt != null)
    {
      comparisonTreeNode = nodeAt.Item as ComparisonTreeNode;
      this.treeViewDifferences.SelectedRow = nodeAt;
    }
    DocumentTreeNode selection = (DocumentTreeNode) null;
    this.docControl1.SetSelection(selection, false, Point.Empty, true, false);
    this.docControl2.SetSelection(selection, false, Point.Empty, true, false);
    if (comparisonTreeNode == (ComparisonTreeNode) null)
      return;
    DocumentTreeNode docNode = comparisonTreeNode.DocNode;
    if (comparisonTreeNode.Verdict == ComparisonVerdict.AbsentInDoc2 || comparisonTreeNode.Verdict == ComparisonVerdict.HasDifferentContentOrGeometry)
    {
      this.docControl1.SetSelection(docNode, false, Point.Empty, true, false);
      if (comparisonTreeNode.Verdict == ComparisonVerdict.AbsentInDoc2)
        this.SyncCurrentPageIndex(this.docControl1, this.docControl2);
    }
    if (comparisonTreeNode.Verdict == ComparisonVerdict.AbsentInDocOne || comparisonTreeNode.Verdict == ComparisonVerdict.HasDifferentContentOrGeometry)
    {
      this.docControl2.SetSelection(comparisonTreeNode.Verdict == ComparisonVerdict.AbsentInDocOne ? docNode : this.DocumentTwo.FindNode(comparisonTreeNode.Id), false, Point.Empty, true, false);
      if (comparisonTreeNode.Verdict == ComparisonVerdict.AbsentInDocOne)
        this.SyncCurrentPageIndex(this.docControl2, this.docControl1);
    }
    if (comparisonTreeNode.Verdict != ComparisonVerdict.Identical)
      return;
    List<ComparisonVerdict> childNodeVerdicts = comparisonTreeNode.GetChildNodeVerdicts();
    if (childNodeVerdicts.Contains(ComparisonVerdict.AbsentInDoc2) || childNodeVerdicts.Contains(ComparisonVerdict.HasDifferentContentOrGeometry))
    {
      this.docControl1.SetSelection(docNode, false, Point.Empty, true, false);
      this.SyncCurrentPageIndex(this.docControl1, this.docControl2);
    }
    if (!childNodeVerdicts.Contains(ComparisonVerdict.AbsentInDocOne) && !childNodeVerdicts.Contains(ComparisonVerdict.HasDifferentContentOrGeometry))
      return;
    this.docControl2.SetSelection(this.DocumentTwo.FindNode(comparisonTreeNode.Id), false, Point.Empty, true, false);
    this.SyncCurrentPageIndex(this.docControl2, this.docControl1);
  }

  private void SyncCurrentPageIndex(DocumentControl origin, DocumentControl target)
  {
    int index = origin.ActivePage.Index;
    if (target.Document.Nodes.Count >= index + 1)
      target.SetActivePage(target.Document.Nodes[index] as Page);
    else
      target.SetActivePage((Page) null);
  }

  private void treeViewDifferences_GetChildren(object sender, GetChildrenEventArgs e)
  {
    e.Children = (IList) (e.Row.Item as ComparisonTreeNode)?.Nodes;
    foreach (ComparisonTreeNode child in (IEnumerable) e.Children)
    {
      if (child.Verdict == ComparisonVerdict.HasDifferentContentOrGeometry)
      {
        child.DocNode.HighlightColor = Color.Orange;
        DocumentTreeNode node = this.DocumentTwo.FindNode(child.Id);
        if (node != null)
          node.HighlightColor = Color.Orange;
      }
    }
  }

  private void treeViewDifferences_GetCellData(object sender, GetCellDataEventArgs e)
  {
    ComparisonTreeNode comparisonTreeNode = e.Row.Item as ComparisonTreeNode;
    if (comparisonTreeNode != (ComparisonTreeNode) null)
    {
      e.CellData.OddStyle = e.CellData.EvenStyle = comparisonTreeNode.Verdict == ComparisonVerdict.HasDifferentContentOrGeometry ? this.nodeDifferentStyle : (comparisonTreeNode.Verdict == ComparisonVerdict.AbsentInDoc2 ? this.nodeAbsentStyle1 : (comparisonTreeNode.Verdict == ComparisonVerdict.AbsentInDocOne ? this.nodeAbsentStyle2 : this.nodeNormalStyle));
      if (comparisonTreeNode.Verdict == ComparisonVerdict.HasDifferentContentOrGeometry)
      {
        comparisonTreeNode.DocNode.HighlightColor = Color.Orange;
        DocumentTreeNode node = this.DocumentTwo.FindNode(comparisonTreeNode.Id);
        if (node != null)
          node.HighlightColor = Color.Orange;
      }
    }
    e.CellData.Value = (object) comparisonTreeNode.Text;
  }

  private void SetTreeNodeStyles()
  {
    this.nodeAbsentStyle1 = new Style(this.treeViewDifferences.RowOddStyle)
    {
      ForeColor = Color.DarkGreen
    };
    this.nodeAbsentStyle2 = new Style(this.treeViewDifferences.RowOddStyle)
    {
      ForeColor = Color.Red
    };
    this.nodeDifferentStyle = new Style(this.treeViewDifferences.RowOddStyle)
    {
      ForeColor = Color.DarkSlateBlue
    };
    this.nodeNormalStyle = new Style(this.treeViewDifferences.RowOddStyle)
    {
      ForeColor = Color.Black
    };
  }

  private void CollapseDocRows(Row treeRow)
  {
    if (!(treeRow.Item is ComparisonTreeNode comparisonTreeNode))
      return;
    if (comparisonTreeNode.DocNode is TableData docNode && docNode.IsRow && comparisonTreeNode.Nodes.OfType<ComparisonTreeNode>().Any<ComparisonTreeNode>((Func<ComparisonTreeNode, bool>) (c => c.Verdict != 0)))
    {
      treeRow.CollapseChildren(true);
      treeRow.Expanded = false;
    }
    else
    {
      for (int childIndex = 0; childIndex < comparisonTreeNode.Nodes.Count; ++childIndex)
        this.CollapseDocRows(treeRow.ChildRowByIndex(childIndex));
    }
  }
}
