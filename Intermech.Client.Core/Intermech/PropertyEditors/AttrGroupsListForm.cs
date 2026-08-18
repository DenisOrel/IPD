
// Type: Intermech.PropertyEditors.AttrGroupsListForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.PropertyEditors;

public class AttrGroupsListForm : TabPageForm
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private iGrid iGrid1;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private iGCellStyle iGrid1Col0CellStyle;
  private iGColHdrStyle iGrid1Col0ColHdrStyle;
  private iGCellStyle iGrid1Col1CellStyle;
  private iGColHdrStyle iGrid1Col1ColHdrStyle;
  private iGCellStyle iGrid1Col2CellStyle;
  private iGColHdrStyle iGrid1Col2ColHdrStyle;

  public AttrGroupsListForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.instGuid = aInstGuid;
  }

  public override void FillForm(IFolder folder)
  {
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    this._folder = folder as CustomFolder;
    int id = (int) (this._folder as AttributeFolder).Id;
    this.iGrid1.Rows.Clear();
    try
    {
      int[] groupsList = service.GetAttributeType(id).GetGroupsList();
      for (int rowIndex = 0; rowIndex < groupsList.Length; ++rowIndex)
      {
        IDBAttributesGroupInfo attributesGroup = service.GetAttributesGroup(groupsList[rowIndex]);
        string groupName = attributesGroup.GroupName;
        string note = attributesGroup.Note;
        this.iGrid1.Rows.Add();
        this.iGrid1.Cells[rowIndex, 0].Value = (object) groupsList[rowIndex];
        this.iGrid1.Cells[rowIndex, 1].Value = (object) groupName;
        this.iGrid1.Cells[rowIndex, 2].Value = (object) note;
      }
    }
    catch
    {
    }
  }

  private void DblClick(object sender)
  {
    CustomFolder tag = (CustomFolder) ((CustomFolder) this._folder.NodeParent.Tag).NodeParent.Tag;
    if (this.iGrid1.CurRow == null)
      return;
    object aId = this.iGrid1.Cells[this.iGrid1.CurRow.Index, 0].Value;
    EventsHolder.FireFolderDClick(sender, this.instGuid, new EventsHolder.FolderArgs(tag.ListCategoryValue, aId, (IFolder) tag));
  }

  private void iGrid1_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.DblClick(sender);
  }

  private void iGrid1_CellDoubleClick(object sender, EventArgs e) => this.DblClick(sender);

  /// <summary>id раздела справки</summary>
  public override string HelpTopicID => this._folder == null ? base.HelpTopicID : "1011";

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
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    this.iGrid1Col0CellStyle = new iGCellStyle(true);
    this.iGrid1Col0ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col1CellStyle = new iGCellStyle(true);
    this.iGrid1Col1ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1Col2CellStyle = new iGCellStyle(true);
    this.iGrid1Col2ColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1 = new iGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    ((ISupportInitialize) this.iGrid1).BeginInit();
    this.SuspendLayout();
    this.iGrid1.AutoResizeCols = true;
    iGcolPattern1.CellStyle = this.iGrid1Col0CellStyle;
    iGcolPattern1.ColHdrStyle = this.iGrid1Col0ColHdrStyle;
    iGcolPattern1.Text = (object) "Ид. группы";
    iGcolPattern1.Width = 103;
    iGcolPattern2.CellStyle = this.iGrid1Col1CellStyle;
    iGcolPattern2.ColHdrStyle = this.iGrid1Col1ColHdrStyle;
    iGcolPattern2.Text = (object) "Наименование";
    iGcolPattern2.Width = 382;
    iGcolPattern3.CellStyle = this.iGrid1Col2CellStyle;
    iGcolPattern3.ColHdrStyle = this.iGrid1Col2ColHdrStyle;
    iGcolPattern3.Text = (object) "Комментарии";
    iGcolPattern3.Width = 542;
    this.iGrid1.Cols.AddRange(new iGColPattern[3]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3
    });
    this.iGrid1.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this.iGrid1.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this.iGrid1.Dock = DockStyle.Fill;
    this.iGrid1.Header.Height = 19;
    this.iGrid1.Location = new Point(0, 0);
    this.iGrid1.Name = "iGrid1";
    this.iGrid1.ReadOnly = true;
    this.iGrid1.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this.iGrid1.Size = new Size(1031, 687);
    this.iGrid1.TabIndex = 0;
    this.iGrid1.Tag = (object) "  ";
    this.iGrid1.CellDoubleClick += new iGCellDoubleClickEventHandler(this.iGrid1_CellDoubleClick);
    this.iGrid1.KeyPress += new KeyPressEventHandler(this.iGrid1_KeyPress);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.iGrid1);
    this.Name = nameof (AttrGroupsListForm);
    this.Size = new Size(1031, 687);
    this.Tag = (object) "   ";
    ((ISupportInitialize) this.iGrid1).EndInit();
    this.ResumeLayout(false);
  }
}
