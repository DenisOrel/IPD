// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.StructureEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class StructureEditor : Form
{
  private StructureEditorCtrl _structEditorCtrl = new StructureEditorCtrl();
  private DataSet _ds;
  private bool _readOnly;
  private TableEditor _editor;
  private IContainer components;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private ImageList _imgList;
  private Button _btnOK;
  private Button _btnCancel;

  internal DataSet ChangedDataSet => this._structEditorCtrl.ChangedDataSet;

  internal bool IsColumnsOrderChanged => this._structEditorCtrl.IsColumnsOrderChanged;

  internal StructureEditor(TableEditor editor, bool readOnly)
  {
    this.InitializeComponent();
    this._ds = editor.OriginalDataSet.Copy();
    this._readOnly = readOnly;
    this._editor = editor;
    this.Controls.Add((Control) this._structEditorCtrl);
    this._structEditorCtrl.SendToBack();
    this._structEditorCtrl.DataChanged += new EventHandler(this.On_structEditorCtrl_DataChanged);
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 885);
  }

  private void On_btnOK_Click(object sender, EventArgs e)
  {
    this._structEditorCtrl.SaveData();
    this.Close();
  }

  private void On_structEditorCtrl_DataChanged(object sender, EventArgs e)
  {
    this._editor.CheckCheckout();
    this._btnOK.Enabled = e != null;
  }

  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
    this._structEditorCtrl.LoadData(this._ds, this._readOnly, true);
  }

  internal static bool EditStructure(
    TableEditor editor,
    bool readOnly,
    out DataSet newData,
    out bool colsOrderChanged)
  {
    newData = (DataSet) null;
    colsOrderChanged = false;
    using (StructureEditor structureEditor = new StructureEditor(editor, readOnly))
    {
      if (structureEditor.ShowDialog() == DialogResult.OK)
      {
        colsOrderChanged = structureEditor.IsColumnsOrderChanged;
        newData = structureEditor.ChangedDataSet;
        return true;
      }
    }
    return false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StructureEditor));
    this._imgList = new ImageList(this.components);
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this._btnOK = new Button();
    this._btnCancel = new Button();
    this.SuspendLayout();
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "Top.ico");
    this._imgList.Images.SetKeyName(1, "Up.ico");
    this._imgList.Images.SetKeyName(2, "Down.ico");
    this._imgList.Images.SetKeyName(3, "Bottom.ico");
    this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    componentResourceManager.ApplyResources((object) this._btnOK, "_btnOK");
    this._btnOK.DialogResult = DialogResult.OK;
    this._btnOK.Name = "_btnOK";
    this._btnOK.UseVisualStyleBackColor = true;
    this._btnOK.Click += new EventHandler(this.On_btnOK_Click);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.DialogResult = DialogResult.Cancel;
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this.CancelButton = (IButtonControl) this._btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._btnCancel);
    this.Controls.Add((Control) this._btnOK);
    this.DoubleBuffered = true;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (StructureEditor);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
  }
}
