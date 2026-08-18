// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RowPropsUserControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.AVSViews;
using Intermech.Document.Model;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

[ViewDescriptionProvider(typeof (RowPropsUserControl.RowPropsUserControlViewDescriptionProvider))]
public class RowPropsUserControl : UserControl, IView
{
  private AVSWindow _avsWindow;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PropertyGrid _propertyGrid;

  public RowPropsUserControl() => this.InitializeComponent();

  public AVSWindow AVSWindow => this._avsWindow;

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (this._avsWindow != null || !(ServicesManager.GetService(typeof (IAVSViewsService)) is IAVSViewsService service))
      return;
    this._avsWindow = service.AVSWindow;
  }

  public void Activate(IView previousView)
  {
    object obj = this._propertyGrid.SelectedObject;
    object[] objArray = (object[]) null;
    if (this._avsWindow == null && ServicesManager.GetService(typeof (IAVSViewsService)) is IAVSViewsService service)
      this._avsWindow = service.AVSWindow;
    if (this._avsWindow != null && this._avsWindow.DocumentControl != null)
    {
      List<AVSRow> avsRowList1 = new List<AVSRow>();
      List<AVSRow> avsRowList2 = new List<AVSRow>();
      List<DocumentTreeNode> docRows = new List<DocumentTreeNode>();
      DocumentTreeNode[] selectedNodes = this._avsWindow.DocumentControl.GetSelectedNodes();
      bool flag = true;
      if (selectedNodes != null && selectedNodes.Length != 0)
      {
        foreach (DocumentTreeNode docNode in selectedNodes)
        {
          avsRowList2.Clear();
          docRows.Clear();
          this._avsWindow.AVSDocument.GetAVSRowsAndDocRows(docNode, avsRowList2, docRows);
          if (avsRowList2.Count > 0)
          {
            avsRowList1.AddRange((IEnumerable<AVSRow>) avsRowList2);
            flag = false;
          }
          Chapter chapter = this._avsWindow.AVSDocument.GetChapter(docNode, false);
          if (chapter is AdditionalChapter section)
          {
            obj = (object) new AdditionalChapterPropertiesWrapper(section);
            flag = false;
          }
          else if (chapter != null)
          {
            obj = (object) new ChapterFormatPorpertiesWrapper(chapter);
            flag = false;
          }
          if (this._avsWindow.AVSDocument.IsSpecification && AVSDocument.FindParentSpecSectionDocNode(docNode) is TableData specSectionDocNode && specSectionDocNode.Tag is SpecificationSection tag)
          {
            flag = false;
            obj = (object) new SpecificationSectionFormatPorpertiesWrapper(tag);
          }
          ImDocument ownerDocument = docNode.OwnerDocument as ImDocument;
          if (ownerDocument != null & flag)
          {
            obj = this._avsWindow.BottomPanelType == AVSWindow.enumBottomPanelType.SpecificationProperties ? (object) new DocumentWrapper(ownerDocument) : (object) new DocumentFormatWrapper(ownerDocument);
            flag = false;
            break;
          }
        }
      }
      if (!flag)
      {
        if (avsRowList1.Count > 0)
        {
          if (avsRowList1.Count == 1)
            obj = (object) avsRowList1[0];
          else
            objArray = (object[]) avsRowList1.ToArray();
        }
      }
      else
        obj = (object) null;
    }
    else
      obj = (object) null;
    if (objArray != null)
      this._propertyGrid.SelectedObjects = objArray;
    else
      this._propertyGrid.SelectedObject = obj;
  }

  public void Deactivate(IView nextView) => this._avsWindow = (AVSWindow) null;

  public string Caption => "Форматирование";

  public int ImageIndex => -1;

  public int OrderID => 1;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._propertyGrid = new PropertyGrid();
    this.SuspendLayout();
    this._propertyGrid.Dock = DockStyle.Fill;
    this._propertyGrid.Location = new Point(0, 0);
    this._propertyGrid.Name = "_propertyGrid";
    this._propertyGrid.Size = new Size(556, 621);
    this._propertyGrid.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._propertyGrid);
    this.Name = nameof (RowPropsUserControl);
    this.Size = new Size(556, 621);
    this.ResumeLayout(false);
  }

  private sealed class RowPropsUserControlViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = "Форматирование",
        ImageIndex = -1,
        OrderID = 1
      };
    }
  }
}
