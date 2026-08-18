
// Type: Intermech.Navigator.SelectionView.SelForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

/// <summary>Form to select attribute or object type</summary>
public class SelForm : Form
{
  private bool checkMultiVal;
  public bool sortShort = true;
  public string begStr = "";
  public string fltr = "";
  public bool attr;
  public DataView dv;
  private bool lockUpdate;
  private Button button1;
  private Button button2;
  private CheckEdit checkShort;
  private CheckEdit checkFull;
  private CheckEdit checkFilter;
  private Panel panel1;
  private Panel panel2;
  private Button btnAttr;
  private TextBox tbFull;
  private ListBox cbFull;
  private System.ComponentModel.Container components;

  public SelForm()
  {
    this.InitializeComponent();
    new SelForm.SelWindow(this.cbFull).AssignHandle(this.tbFull.Handle);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Show window and allow user to select</summary>
  /// <param name="dv">DataView with info about attrs/object types</param>
  /// <param name="attr">true for selecting attrs, false for object types</param>
  /// <param name="sortShort">true if sort by short names, false for long names</param>
  /// <param name="begStr">starting substring</param>
  /// <param name="checkMultiVal"></param>
  /// <returns>info of selected attr/objtype or null if none</returns>
  public SelFormResult Execute(
    DataView dv,
    bool attr,
    ref bool sortShort,
    string begStr,
    bool checkMultiVal)
  {
    this.Text = attr ? LocalizationHolder.rm.GetString("Client.Core_221") : LocalizationHolder.rm.GetString("Client.Core_421");
    this.checkMultiVal = checkMultiVal;
    this.sortShort = sortShort;
    this.begStr = begStr;
    this.dv = dv;
    this.attr = attr;
    this.fltr = "";
    this.btnAttr.Visible = attr;
    this.lockUpdate = true;
    try
    {
      this.checkShort.Checked = sortShort;
      this.checkFull.Checked = !sortShort;
    }
    finally
    {
      this.lockUpdate = false;
    }
    this.checkFilter.Enabled = begStr != "";
    if (this.checkFilter.Enabled)
      this.checkFilter.Checked = false;
    this.SetupDataView();
    this.FillList(dv, sortShort);
    if (this.ShowDialog() != DialogResult.OK)
      return (SelFormResult) null;
    sortShort = this.sortShort;
    SelFormResult selFormResult = new SelFormResult();
    DataRowView dataRowView = this.dv[this.cbFull.SelectedIndex];
    if (attr)
    {
      selFormResult.GUID = dataRowView["F_GUID"].ToString();
      selFormResult.ID = Convert.ToInt32(dataRowView["F_ATTRIBUTE_ID"]);
      selFormResult.shortName = Convert.ToString(dataRowView["F_SHORT_NAME"]);
      selFormResult.longName = Convert.ToString(dataRowView["F_NAME"]);
    }
    else
    {
      selFormResult.GUID = dataRowView["F_GUID"].ToString();
      selFormResult.ID = Convert.ToInt32(dataRowView["F_OBJECT_TYPE"]);
      selFormResult.shortName = dataRowView["F_SHORT_NAME"].ToString();
      selFormResult.longName = dataRowView["F_OBJ_NAME"].ToString();
    }
    return selFormResult;
  }

  public SelFormResult Execute(DataView dv)
  {
    bool sortShort = false;
    return this.Execute(dv, true, ref sortShort, "", false);
  }

  internal void SetupDataView()
  {
    this.dv.RowFilter = !(this.fltr != "") ? "" : (!this.sortShort ? (!this.attr ? $"F_OBJ_NAME LIKE '{this.fltr}*'" : $"F_NAME LIKE '{this.fltr}*'") : $"F_SHORT_NAME LIKE '{this.fltr}*'");
    if (this.attr)
    {
      if (this.sortShort)
        this.dv.Sort = "F_SHORT_NAME, F_NAME";
      else
        this.dv.Sort = "F_NAME";
    }
    else if (this.sortShort)
      this.dv.Sort = "F_SHORT_NAME, F_OBJ_NAME";
    else
      this.dv.Sort = "F_OBJ_NAME";
  }

  internal void FillList(DataView dv, bool shortName)
  {
    this.cbFull.Items.Clear();
    string property = !this.attr ? "F_OBJ_NAME" : "F_NAME";
    for (int recordIndex = 0; recordIndex < dv.Count; ++recordIndex)
    {
      DataRowView dataRowView = dv[recordIndex];
      string empty = string.Empty;
      string str;
      if (shortName)
        str = $"{dataRowView["F_SHORT_NAME"]} [{dataRowView[property]}]";
      else
        str = $"{dataRowView[property]} [{dataRowView["F_SHORT_NAME"]}]";
      this.cbFull.Items.Add((object) str);
    }
    this.tbFull.Text = this.begStr;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelForm));
    this.button1 = new Button();
    this.button2 = new Button();
    this.checkShort = new CheckEdit();
    this.checkFull = new CheckEdit();
    this.checkFilter = new CheckEdit();
    this.panel1 = new Panel();
    this.btnAttr = new Button();
    this.panel2 = new Panel();
    this.tbFull = new TextBox();
    this.cbFull = new ListBox();
    this.checkShort.Properties.BeginInit();
    this.checkFull.Properties.BeginInit();
    this.checkFilter.Properties.BeginInit();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    componentResourceManager.ApplyResources((object) this.checkShort, "checkShort");
    this.checkShort.Name = "checkShort";
    this.checkShort.Properties.Caption = componentResourceManager.GetString("checkShort.Properties.Caption");
    this.checkShort.Properties.CheckStyle = CheckStyles.Radio;
    this.checkShort.Properties.RadioGroupIndex = 1;
    this.checkShort.Properties.CheckedChanged += new EventHandler(this.checkShort_Click);
    componentResourceManager.ApplyResources((object) this.checkFull, "checkFull");
    this.checkFull.Name = "checkFull";
    this.checkFull.Properties.Caption = componentResourceManager.GetString("checkFull.Properties.Caption");
    this.checkFull.Properties.CheckStyle = CheckStyles.Radio;
    this.checkFull.Properties.RadioGroupIndex = 1;
    this.checkFull.TabStop = false;
    componentResourceManager.ApplyResources((object) this.checkFilter, "checkFilter");
    this.checkFilter.Name = "checkFilter";
    this.checkFilter.Properties.Caption = componentResourceManager.GetString("checkFilter.Properties.Caption");
    this.checkFilter.CheckedChanged += new EventHandler(this.checkFilter_CheckedChanged);
    this.panel1.Controls.Add((Control) this.btnAttr);
    this.panel1.Controls.Add((Control) this.checkShort);
    this.panel1.Controls.Add((Control) this.checkFull);
    this.panel1.Controls.Add((Control) this.checkFilter);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnAttr, "btnAttr");
    this.btnAttr.Name = "btnAttr";
    this.btnAttr.Click += new EventHandler(this.button3_Click);
    this.panel2.Controls.Add((Control) this.button1);
    this.panel2.Controls.Add((Control) this.button2);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.tbFull, "tbFull");
    this.tbFull.Name = "tbFull";
    this.tbFull.TextChanged += new EventHandler(this.tbFull_TextChanged);
    this.tbFull.KeyDown += new KeyEventHandler(this.tbFull_KeyDown);
    componentResourceManager.ApplyResources((object) this.cbFull, "cbFull");
    this.cbFull.Name = "cbFull";
    this.cbFull.TabStop = false;
    this.cbFull.SelectedIndexChanged += new EventHandler(this.cbFull_SelectedIndexChanged);
    this.cbFull.DoubleClick += new EventHandler(this.cbFull_DoubleClick);
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.cbFull);
    this.Controls.Add((Control) this.tbFull);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelForm);
    this.ShowInTaskbar = false;
    this.checkShort.Properties.EndInit();
    this.checkFull.Properties.EndInit();
    this.checkFilter.Properties.EndInit();
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void checkShort_Click(object sender, EventArgs e)
  {
    if (this.lockUpdate)
      return;
    bool flag = this.checkShort.Checked;
    if (this.sortShort == flag)
      return;
    this.sortShort = flag;
    this.SetupDataView();
    this.FillList(this.dv, this.sortShort);
    this.UpdateText();
  }

  private void checkFilter_CheckedChanged(object sender, EventArgs e)
  {
    this.fltr = !this.checkFilter.Checked ? "" : this.begStr;
    this.SetupDataView();
    this.FillList(this.dv, this.sortShort);
    this.UpdateText();
  }

  private void UpdateText()
  {
    object selectedItem = this.cbFull.SelectedItem;
    if (selectedItem == null)
      this.cbFull.Text = "";
    else
      this.cbFull.Text = selectedItem.ToString();
  }

  private void button1_Click(object sender, EventArgs e)
  {
    if (this.cbFull.SelectedItem == null || this.cbFull.SelectedIndex < 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_422"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      this.DialogResult = DialogResult.None;
    }
    else
    {
      if (!this.attr || this.CanUseSelectedAttribute())
        return;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_423") + LocalizationHolder.rm.GetString("Client.Core_424"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      this.DialogResult = DialogResult.None;
    }
  }

  private void cbFull_DoubleClick(object sender, EventArgs e)
  {
    if (this.cbFull.SelectedItem == null || this.cbFull.SelectedIndex < 0)
      return;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>
  /// Для проверки допустимости выбора атрибута (если атрибут может
  /// принимать множество значений, то его выбор не допускается)
  /// </summary>
  /// <returns></returns>
  private bool CanUseSelectedAttribute()
  {
    if (this.cbFull.SelectedItem == null || this.cbFull.SelectedIndex < 0)
      return false;
    if (!this.checkMultiVal)
      return true;
    int int32 = Convert.ToInt32(this.dv[this.cbFull.SelectedIndex]["F_ATTRIBUTE_ID"]);
    MultiValueModes multipleValued = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(int32).MultipleValued;
    return multipleValued != MultiValueModes.MultiValues && multipleValued != MultiValueModes.MultiValuesFromList;
  }

  private void button3_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.dv = sessionKeeper.Session.GetAttributeTypeCollection(-1).Select("").DefaultView;
      this.SetupDataView();
      this.FillList(this.dv, this.sortShort);
    }
  }

  private void cbFull_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.tbFull.Text = this.cbFull.SelectedIndex >= 0 ? this.cbFull.SelectedItem.ToString() : string.Empty;
    this.tbFull.Focus();
    this.tbFull.SelectAll();
  }

  private void tbFull_TextChanged(object sender, EventArgs e)
  {
    this.cbFull.SelectedIndexChanged -= new EventHandler(this.cbFull_SelectedIndexChanged);
    string upper = this.tbFull.Text.ToUpper();
    bool flag = false;
    if (!upper.Equals(string.Empty))
    {
      for (int index = 0; index < this.cbFull.Items.Count; ++index)
      {
        if (this.cbFull.Items[index].ToString().ToUpper().IndexOf(upper) == 0)
        {
          this.cbFull.SelectedIndex = index;
          flag = true;
          break;
        }
      }
    }
    if (!flag)
      this.cbFull.SelectedIndex = -1;
    this.cbFull.SelectedIndexChanged += new EventHandler(this.cbFull_SelectedIndexChanged);
  }

  private void tbFull_KeyDown(object sender, KeyEventArgs e)
  {
    e.Handled = false;
    switch (e.KeyCode)
    {
      case Keys.Up:
        if (this.cbFull.SelectedIndex > 0 && this.cbFull.SelectedIndex <= this.cbFull.Items.Count - 1)
          --this.cbFull.SelectedIndex;
        e.Handled = true;
        break;
      case Keys.Down:
        if (this.cbFull.SelectedIndex >= 0 && this.cbFull.SelectedIndex < this.cbFull.Items.Count - 1)
          ++this.cbFull.SelectedIndex;
        e.Handled = true;
        break;
    }
  }

  internal class SelWindow : NativeWindow
  {
    private const int WM_MOUSEWEEL = 522;
    private ListBox _listBox;

    public SelWindow(ListBox listBox) => this._listBox = listBox;

    protected override void WndProc(ref Message m)
    {
      if (m.Msg.Equals(522))
        SelForm.SelWindow.SendMessage(new HandleRef((object) this._listBox, this._listBox.Handle), m.Msg, m.WParam, m.LParam);
      base.WndProc(ref m);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr SendMessage(
      HandleRef hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam);
  }
}
