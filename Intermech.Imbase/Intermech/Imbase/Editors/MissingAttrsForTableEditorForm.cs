// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.MissingAttrsForTableEditorForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

public class MissingAttrsForTableEditorForm : Form
{
  private List<string> _missingAtts;
  private List<string> _addedAttrs = new List<string>(0);
  private List<string> _deletedAttrs = new List<string>(0);
  private List<string> _notAddedAttrs = new List<string>(0);
  private List<string> _notDeletedAttrs = new List<string>(0);
  private AttributeTypeProperties[] _atProps;
  private Dictionary<string, List<string>> _conflictedGuids = new Dictionary<string, List<string>>(0);
  private string _strAttrNotDeleted = LocalizationHolder.rm.GetString("TableEditor_MissingAttrs_NotDelAttr");
  private string _strAttrNotAdded = LocalizationHolder.rm.GetString("TableEditor_MissingAttrs_NotAddAttr");
  private IContainer components;
  private Panel _BottomPnl;
  private Button _btnCalcel;
  private Button _btnOK;
  private ImageList _imgButtons;
  private SplitContainer _splContainer;
  private TableLayoutPanel _tlpAddedAttrs;
  private Button _btnLeftAll;
  private Button _btnLeft;
  private Button _btnRight;
  private Button _btnRightAll;
  private ListView _lvAddedAttrs;
  private ColumnHeader colName_All;
  private Label _lbAddedAttrs;
  private TableLayoutPanel _tlpDeletedAttrs;
  private Label _lbDeletedAttrs;
  private ListView _lvDeletedAttrs;
  private ColumnHeader colName_Selected;
  private Panel panel1;
  private RichTextBox _rtbAddedAttrs;
  private Panel panel2;
  private RichTextBox _rtbDeletedAttrs;

  public List<string> AddedAttrs => this._addedAttrs;

  public List<string> DeletedAttrs => this._deletedAttrs;

  public bool NeedShowForm => this._conflictedGuids.Count != this._missingAtts.Count;

  public MissingAttrsForTableEditorForm(
    List<string> missingAttrs,
    AttributeTypeProperties[] atProps)
  {
    this.InitializeComponent();
    this._missingAtts = missingAttrs;
    this._atProps = atProps;
    ImageList imageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this._lvAddedAttrs.SmallImageList = imageList;
    this._lvDeletedAttrs.SmallImageList = imageList;
    this.FillForm();
    this.ShowMessage();
    if (this._conflictedGuids.Count == this._missingAtts.Count)
      return;
    this.CheckButtonsState();
    this._btnOK.Enabled = this._lvAddedAttrs.Items.Count > 0 || this._lvDeletedAttrs.Items.Count > 0;
    this.On_lvs_SizeChanged((object) this._lvAddedAttrs, (EventArgs) null);
    this.On_lvs_SizeChanged((object) this._lvDeletedAttrs, (EventArgs) null);
  }

  private void On_btnLeftRight_Click(object sender, EventArgs e)
  {
    int int16 = (int) Convert.ToInt16((sender as Button).Tag);
    ListView listView1;
    ListView listView2;
    List<string> stringList;
    if (int16 == 0 || int16 == 1)
    {
      listView1 = this._lvAddedAttrs;
      listView2 = this._lvDeletedAttrs;
      stringList = this._notDeletedAttrs;
    }
    else
    {
      listView1 = this._lvDeletedAttrs;
      listView2 = this._lvAddedAttrs;
      stringList = this._notAddedAttrs;
    }
    switch (int16)
    {
      case 0:
      case 3:
        int index = 0;
        while (listView1.Items.Count > stringList.Count)
        {
          ListViewItem listViewItem = listView1.Items[index];
          listViewItem.Selected = false;
          if (stringList.Contains(listViewItem.Name))
          {
            ++index;
          }
          else
          {
            listView1.Items.Remove(listViewItem);
            listView2.Items.Add(listViewItem);
          }
        }
        if (listView2.SelectedItems.Count == 0)
          listView2.Items[0].Selected = listView2.Items[0].Focused = true;
        listView2.Focus();
        break;
      case 1:
      case 2:
        while (listView2.SelectedItems.Count > 0)
          listView2.SelectedItems[0].Selected = false;
        int num = 0;
        while (num < listView1.SelectedItems.Count)
        {
          ListViewItem selectedItem = listView1.SelectedItems[num++];
          selectedItem.Selected = false;
          if (!stringList.Contains(selectedItem.Name))
          {
            listView1.Items.Remove(selectedItem);
            listView2.Items.Add(selectedItem);
            selectedItem.Focused = true;
          }
        }
        if (listView1.Items.Count > 0)
        {
          listView1.Items[listView1.FocusedItem != null ? listView1.FocusedItem.Index : 0].Selected = true;
          break;
        }
        listView2.Focus();
        break;
    }
    (sender as Button).Focus();
    this.CheckButtonsState();
  }

  private void On_lvAddedAttrs_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._lvAddedAttrs.SelectedItems.Count == 0)
    {
      this._btnRight.Enabled = false;
      this._rtbAddedAttrs.Text = string.Empty;
    }
    else if (this._lvAddedAttrs.SelectedItems.Count == 1)
    {
      if (this._notDeletedAttrs.Contains(this._lvAddedAttrs.SelectedItems[0].Name))
      {
        this._btnRight.Enabled = false;
        this._rtbAddedAttrs.Text = this._strAttrNotDeleted;
      }
      else
      {
        this._btnRight.Enabled = true;
        this._rtbAddedAttrs.Text = string.Empty;
      }
    }
    else
    {
      this._rtbAddedAttrs.Text = string.Empty;
      this.CheckButtonsState();
    }
  }

  private void On_lvDeletedAttrs_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._lvDeletedAttrs.SelectedItems.Count == 0)
    {
      this._btnLeft.Enabled = false;
      this._rtbDeletedAttrs.Text = string.Empty;
    }
    else if (this._lvDeletedAttrs.SelectedItems.Count == 1)
    {
      if (this._notAddedAttrs.Contains(this._lvDeletedAttrs.SelectedItems[0].Name))
      {
        this._btnLeft.Enabled = false;
        this._rtbDeletedAttrs.Text = this._strAttrNotAdded;
      }
      else
      {
        this._btnLeft.Enabled = true;
        this._rtbDeletedAttrs.Text = string.Empty;
      }
    }
    else
    {
      this._rtbDeletedAttrs.Text = string.Empty;
      this.CheckButtonsState();
    }
  }

  private void On_lvs_DoubleClick(object sender, EventArgs e)
  {
    this.On_btnLeftRight_Click(sender as ListView == this._lvAddedAttrs ? (object) this._btnRight : (object) this._btnLeft, e);
  }

  private void On_lvs_SizeChanged(object sender, EventArgs e)
  {
    if (!(sender is ListView listView) || listView.Columns.Count == 0)
      return;
    if (listView.Columns[0] == null)
      return;
    try
    {
      listView.SizeChanged -= new EventHandler(this.On_lvs_SizeChanged);
      listView.Columns[0].Width = -2;
      listView.SizeChanged += new EventHandler(this.On_lvs_SizeChanged);
    }
    catch
    {
    }
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    if (this.DialogResult == DialogResult.OK)
    {
      foreach (ListViewItem listViewItem in this._lvAddedAttrs.Items)
      {
        if (!this._addedAttrs.Contains(listViewItem.Name))
          this._addedAttrs.Add(listViewItem.Name);
      }
      foreach (ListViewItem listViewItem in this._lvDeletedAttrs.Items)
      {
        if (!this._deletedAttrs.Contains(listViewItem.Name))
          this._deletedAttrs.Add(listViewItem.Name);
      }
    }
    base.OnClosing(e);
  }

  private void CheckButtonsState()
  {
    this._btnRightAll.Enabled = this._lvAddedAttrs.Items.Count > this._notDeletedAttrs.Count;
    this._btnLeftAll.Enabled = this._lvDeletedAttrs.Items.Count > this._notAddedAttrs.Count;
    bool flag1 = false;
    if (this._lvAddedAttrs.SelectedItems.Count > 0)
    {
      foreach (ListViewItem selectedItem in this._lvAddedAttrs.SelectedItems)
      {
        if (!this._notDeletedAttrs.Contains(selectedItem.Name))
        {
          flag1 = true;
          break;
        }
      }
    }
    this._btnRight.Enabled = flag1;
    bool flag2 = false;
    if (this._lvDeletedAttrs.SelectedItems.Count > 0)
    {
      foreach (ListViewItem selectedItem in this._lvDeletedAttrs.SelectedItems)
      {
        if (!this._notAddedAttrs.Contains(selectedItem.Name))
        {
          flag2 = true;
          break;
        }
      }
    }
    this._btnLeft.Enabled = flag2;
  }

  private void FillForm()
  {
    if (this._missingAtts == null)
      return;
    this._lvAddedAttrs.BeginUpdate();
    this._lvDeletedAttrs.BeginUpdate();
    foreach (string missingAtt in this._missingAtts)
    {
      bool flag1 = true;
      List<string> attrNames = new List<string>(0);
      bool flag2 = this.ValidatingAttr(missingAtt, attrNames);
      try
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(missingAtt));
        if (attributeType != null)
        {
          int imageIndex = Statics.IconSrv.IndexOf(3, -1, (object) attributeType.FieldType);
          this._lvAddedAttrs.Items.Add($"{attributeType.Name} ({missingAtt})", imageIndex).Name = missingAtt;
          if (!flag2 && !this._notDeletedAttrs.Contains(missingAtt))
            this._notDeletedAttrs.Add(missingAtt);
          flag1 = false;
        }
      }
      catch
      {
      }
      if (flag1)
      {
        if (!flag2 && !this._conflictedGuids.ContainsKey(missingAtt))
        {
          this._conflictedGuids.Add(missingAtt, attrNames);
        }
        else
        {
          this._lvDeletedAttrs.Items.Add(missingAtt).Name = missingAtt;
          if (!this._notAddedAttrs.Contains(missingAtt))
            this._notAddedAttrs.Add(missingAtt);
        }
      }
    }
    this._lvAddedAttrs.EndUpdate();
    this._lvDeletedAttrs.EndUpdate();
  }

  private void ShowMessage()
  {
    if (this._conflictedGuids.Count == 0)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(LocalizationHolder.rm.GetString("TableEditor_MissingAttrs_Conflict_Msg"));
    stringBuilder.Append("\n");
    foreach (KeyValuePair<string, List<string>> conflictedGuid in this._conflictedGuids)
    {
      stringBuilder.Append($"\t\"{conflictedGuid.Key}\" :\n");
      foreach (string str in conflictedGuid.Value)
        stringBuilder.Append($"\t\t\"{str}\"\n");
    }
    string caption = LocalizationHolder.rm.GetString("Imbase.Client_45");
    int num = (int) new MessageForm(stringBuilder.ToString(), caption, MessageBoxIcon.Exclamation).ShowDialog();
  }

  private bool ValidatingAttr(string guid, List<string> attrNames)
  {
    using (Parser parser = new Parser())
    {
      parser.CreateVariable += new CreateVariableEventHandler(TableLoadHelper.Parser_CreateVariable);
      try
      {
        parser.AutoDetectVariables = true;
        parser.Context = (object) this._atProps;
        foreach (AttributeTypeProperties atProp in this._atProps)
        {
          object obj = (object) atProp;
          AttributeTypeProperties attributeTypeProperties = (AttributeTypeProperties) obj;
          if (!(attributeTypeProperties.AttributeGuid == Guid.Empty))
          {
            ExpressionTree expressionTree = parser.Parse(attributeTypeProperties.Formula);
            if (expressionTree != null)
            {
              string name = ((AttributeTypeProperties) obj).Name;
              for (int index = 0; index < expressionTree.Variables.Count; ++index)
              {
                if (string.Compare(guid, expressionTree.Variables[index].Name, true) == 0)
                {
                  if (!attrNames.Contains(name))
                  {
                    attrNames.Add(name);
                    break;
                  }
                  break;
                }
              }
            }
          }
        }
      }
      finally
      {
        parser.CreateVariable -= new CreateVariableEventHandler(TableLoadHelper.Parser_CreateVariable);
      }
    }
    return attrNames.Count == 0;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MissingAttrsForTableEditorForm));
    this._splContainer = new SplitContainer();
    this._tlpAddedAttrs = new TableLayoutPanel();
    this._btnLeftAll = new Button();
    this._imgButtons = new ImageList(this.components);
    this._btnLeft = new Button();
    this._btnRight = new Button();
    this._btnRightAll = new Button();
    this._lvAddedAttrs = new ListView();
    this.colName_All = new ColumnHeader();
    this._lbAddedAttrs = new Label();
    this.panel1 = new Panel();
    this._rtbAddedAttrs = new RichTextBox();
    this._tlpDeletedAttrs = new TableLayoutPanel();
    this._lbDeletedAttrs = new Label();
    this._lvDeletedAttrs = new ListView();
    this.colName_Selected = new ColumnHeader();
    this.panel2 = new Panel();
    this._rtbDeletedAttrs = new RichTextBox();
    this._BottomPnl = new Panel();
    this._btnOK = new Button();
    this._btnCalcel = new Button();
    this._splContainer.BeginInit();
    this._splContainer.Panel1.SuspendLayout();
    this._splContainer.Panel2.SuspendLayout();
    this._splContainer.SuspendLayout();
    this._tlpAddedAttrs.SuspendLayout();
    this.panel1.SuspendLayout();
    this._tlpDeletedAttrs.SuspendLayout();
    this.panel2.SuspendLayout();
    this._BottomPnl.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._splContainer, "_splContainer");
    this._splContainer.Name = "_splContainer";
    this._splContainer.Panel1.Controls.Add((Control) this._tlpAddedAttrs);
    this._splContainer.Panel2.Controls.Add((Control) this._tlpDeletedAttrs);
    componentResourceManager.ApplyResources((object) this._tlpAddedAttrs, "_tlpAddedAttrs");
    this._tlpAddedAttrs.Controls.Add((Control) this._btnLeftAll, 1, 5);
    this._tlpAddedAttrs.Controls.Add((Control) this._btnLeft, 1, 4);
    this._tlpAddedAttrs.Controls.Add((Control) this._btnRight, 1, 3);
    this._tlpAddedAttrs.Controls.Add((Control) this._btnRightAll, 1, 2);
    this._tlpAddedAttrs.Controls.Add((Control) this._lvAddedAttrs, 0, 1);
    this._tlpAddedAttrs.Controls.Add((Control) this._lbAddedAttrs, 0, 0);
    this._tlpAddedAttrs.Controls.Add((Control) this.panel1, 0, 7);
    this._tlpAddedAttrs.Name = "_tlpAddedAttrs";
    componentResourceManager.ApplyResources((object) this._btnLeftAll, "_btnLeftAll");
    this._btnLeftAll.ImageList = this._imgButtons;
    this._btnLeftAll.Name = "_btnLeftAll";
    this._btnLeftAll.Tag = (object) "3";
    this._btnLeftAll.UseVisualStyleBackColor = true;
    this._btnLeftAll.Click += new EventHandler(this.On_btnLeftRight_Click);
    this._imgButtons.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgButtons.ImageStream");
    this._imgButtons.TransparentColor = Color.Transparent;
    this._imgButtons.Images.SetKeyName(0, "RightAll.ico");
    this._imgButtons.Images.SetKeyName(1, "Right.ico");
    this._imgButtons.Images.SetKeyName(2, "Left.ico");
    this._imgButtons.Images.SetKeyName(3, "LeftAll.ico");
    componentResourceManager.ApplyResources((object) this._btnLeft, "_btnLeft");
    this._btnLeft.ImageList = this._imgButtons;
    this._btnLeft.Name = "_btnLeft";
    this._btnLeft.Tag = (object) "2";
    this._btnLeft.UseVisualStyleBackColor = true;
    this._btnLeft.Click += new EventHandler(this.On_btnLeftRight_Click);
    componentResourceManager.ApplyResources((object) this._btnRight, "_btnRight");
    this._btnRight.ImageList = this._imgButtons;
    this._btnRight.Name = "_btnRight";
    this._btnRight.Tag = (object) "1";
    this._btnRight.UseVisualStyleBackColor = true;
    this._btnRight.Click += new EventHandler(this.On_btnLeftRight_Click);
    componentResourceManager.ApplyResources((object) this._btnRightAll, "_btnRightAll");
    this._btnRightAll.ImageList = this._imgButtons;
    this._btnRightAll.Name = "_btnRightAll";
    this._btnRightAll.Tag = (object) "0";
    this._btnRightAll.UseVisualStyleBackColor = true;
    this._btnRightAll.Click += new EventHandler(this.On_btnLeftRight_Click);
    this._lvAddedAttrs.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName_All
    });
    componentResourceManager.ApplyResources((object) this._lvAddedAttrs, "_lvAddedAttrs");
    this._lvAddedAttrs.FullRowSelect = true;
    this._lvAddedAttrs.HeaderStyle = ColumnHeaderStyle.None;
    this._lvAddedAttrs.HideSelection = false;
    this._lvAddedAttrs.Name = "_lvAddedAttrs";
    this._tlpAddedAttrs.SetRowSpan((Control) this._lvAddedAttrs, 6);
    this._lvAddedAttrs.Tag = (object) "0";
    this._lvAddedAttrs.UseCompatibleStateImageBehavior = false;
    this._lvAddedAttrs.View = View.Details;
    this._lvAddedAttrs.SelectedIndexChanged += new EventHandler(this.On_lvAddedAttrs_SelectedIndexChanged);
    this._lvAddedAttrs.SizeChanged += new EventHandler(this.On_lvs_SizeChanged);
    this._lvAddedAttrs.DoubleClick += new EventHandler(this.On_lvs_DoubleClick);
    componentResourceManager.ApplyResources((object) this.colName_All, "colName_All");
    componentResourceManager.ApplyResources((object) this._lbAddedAttrs, "_lbAddedAttrs");
    this._lbAddedAttrs.Name = "_lbAddedAttrs";
    this.panel1.BorderStyle = BorderStyle.FixedSingle;
    this.panel1.Controls.Add((Control) this._rtbAddedAttrs);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this._rtbAddedAttrs.BackColor = SystemColors.Control;
    this._rtbAddedAttrs.BorderStyle = BorderStyle.None;
    componentResourceManager.ApplyResources((object) this._rtbAddedAttrs, "_rtbAddedAttrs");
    this._rtbAddedAttrs.HideSelection = false;
    this._rtbAddedAttrs.Name = "_rtbAddedAttrs";
    this._rtbAddedAttrs.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._tlpDeletedAttrs, "_tlpDeletedAttrs");
    this._tlpDeletedAttrs.Controls.Add((Control) this._lbDeletedAttrs, 0, 0);
    this._tlpDeletedAttrs.Controls.Add((Control) this._lvDeletedAttrs, 0, 1);
    this._tlpDeletedAttrs.Controls.Add((Control) this.panel2, 0, 2);
    this._tlpDeletedAttrs.Name = "_tlpDeletedAttrs";
    componentResourceManager.ApplyResources((object) this._lbDeletedAttrs, "_lbDeletedAttrs");
    this._lbDeletedAttrs.Name = "_lbDeletedAttrs";
    this._lvDeletedAttrs.Columns.AddRange(new ColumnHeader[1]
    {
      this.colName_Selected
    });
    componentResourceManager.ApplyResources((object) this._lvDeletedAttrs, "_lvDeletedAttrs");
    this._lvDeletedAttrs.FullRowSelect = true;
    this._lvDeletedAttrs.HeaderStyle = ColumnHeaderStyle.None;
    this._lvDeletedAttrs.HideSelection = false;
    this._lvDeletedAttrs.Name = "_lvDeletedAttrs";
    this._lvDeletedAttrs.Tag = (object) "1";
    this._lvDeletedAttrs.UseCompatibleStateImageBehavior = false;
    this._lvDeletedAttrs.View = View.Details;
    this._lvDeletedAttrs.SelectedIndexChanged += new EventHandler(this.On_lvDeletedAttrs_SelectedIndexChanged);
    this._lvDeletedAttrs.SizeChanged += new EventHandler(this.On_lvs_SizeChanged);
    this._lvDeletedAttrs.DoubleClick += new EventHandler(this.On_lvs_DoubleClick);
    componentResourceManager.ApplyResources((object) this.colName_Selected, "colName_Selected");
    this.panel2.BorderStyle = BorderStyle.FixedSingle;
    this.panel2.Controls.Add((Control) this._rtbDeletedAttrs);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this._rtbDeletedAttrs.BackColor = SystemColors.Control;
    this._rtbDeletedAttrs.BorderStyle = BorderStyle.None;
    componentResourceManager.ApplyResources((object) this._rtbDeletedAttrs, "_rtbDeletedAttrs");
    this._rtbDeletedAttrs.HideSelection = false;
    this._rtbDeletedAttrs.Name = "_rtbDeletedAttrs";
    this._rtbDeletedAttrs.ReadOnly = true;
    this._BottomPnl.Controls.Add((Control) this._btnOK);
    this._BottomPnl.Controls.Add((Control) this._btnCalcel);
    componentResourceManager.ApplyResources((object) this._BottomPnl, "_BottomPnl");
    this._BottomPnl.Name = "_BottomPnl";
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._btnCalcel, "_btnCalcel");
    this._btnCalcel.DialogResult = DialogResult.Cancel;
    this._btnCalcel.Name = "_btnCalcel";
    this._btnCalcel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnCalcel;
    this.Controls.Add((Control) this._splContainer);
    this.Controls.Add((Control) this._BottomPnl);
    this.DoubleBuffered = true;
    this.Name = nameof (MissingAttrsForTableEditorForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._splContainer.Panel1.ResumeLayout(false);
    this._splContainer.Panel2.ResumeLayout(false);
    this._splContainer.EndInit();
    this._splContainer.ResumeLayout(false);
    this._tlpAddedAttrs.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this._tlpDeletedAttrs.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this._BottomPnl.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
