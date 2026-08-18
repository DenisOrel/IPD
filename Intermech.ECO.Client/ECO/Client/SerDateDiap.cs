// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.SerDateDiap
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Sets;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class SerDateDiap : Form
{
  private ISet set;
  private bool byDate;
  private bool changed;
  private int selIndex = -1;
  private bool lockChange;
  private IContainer components;
  private Panel panel1;
  private Button btnOK;
  private Button btnCancel;
  private Button btnChange;
  private Button btnAdd;
  private Button btnClear;
  private DateEdit deTo;
  private Label label6;
  private DateEdit deFrom;
  private Label label5;
  private Label label4;
  private Label label3;
  private RadioButton rbDates;
  private RadioButton rbSeries;
  private ListView lv;
  private ColumnHeader colHeaderFrom;
  private ColumnHeader colHeaderTo;
  private Button btnDelete;
  private TextEdit seFrom;
  private TextEdit seTo;

  public SerDateDiap() => this.InitializeComponent();

  public bool Execute(ref ISet set)
  {
    this.set = set;
    this.lockChange = true;
    try
    {
      this.ShowSet();
    }
    finally
    {
      this.lockChange = false;
    }
    if (this.byDate)
    {
      this.seFrom.Enabled = false;
      this.seTo.Enabled = false;
    }
    else
    {
      this.deFrom.Enabled = false;
      this.deTo.Enabled = false;
    }
    this.SetList();
    DateTime dateTime = this.deFrom.DateTime;
    if (dateTime.Year == 1801)
      this.deFrom.DateTime = DateTime.Now;
    dateTime = this.deTo.DateTime;
    if (dateTime.Year == 1801)
      this.deTo.DateTime = DateTime.Now;
    if (this.ShowDialog() != DialogResult.OK)
      return false;
    if (this.changed)
      set = this.set;
    return this.changed;
  }

  private void ShowSet()
  {
    if (this.set != null)
    {
      this.byDate = this.set is Set<DateTime>;
      if (this.byDate)
        this.rbDates.Checked = true;
      else
        this.rbSeries.Checked = true;
    }
    else
    {
      this.set = (ISet) new Set<int>();
      this.changed = true;
      this.rbSeries.Checked = true;
    }
  }

  private void UpdateControls()
  {
    this.seFrom.Enabled = !this.byDate;
    this.seTo.Enabled = !this.byDate;
    this.deFrom.Enabled = this.byDate;
    this.deTo.Enabled = this.byDate;
  }

  private void SetList()
  {
    this.lv.BeginUpdate();
    try
    {
      this.lv.Items.Clear();
      if (this.set is Set<int>)
      {
        foreach (IRange<int> range in ((Set<int>) this.set).Ranges)
          this.lv.Items.Add(new ListViewItem(new string[2]
          {
            this.GetIntMinValue(range.MinValue),
            this.GetIntMaxValue(range.MaxValue)
          }));
      }
      else
      {
        foreach (IRange<DateTime> range in ((Set<DateTime>) this.set).Ranges)
          this.lv.Items.Add(new ListViewItem(new string[2]
          {
            this.GetDateMinValue(range.MinValue),
            this.GetDateMaxValue(range.MaxValue)
          }));
      }
      if (this.lv.Items.Count <= 0)
        return;
      this.lv.Items[0].Selected = true;
      this.selIndex = 0;
    }
    finally
    {
      this.lv.EndUpdate();
    }
  }

  private string GetIntMinValue(int value) => value == int.MinValue ? "" : Convert.ToString(value);

  private string GetIntMaxValue(int value) => value == int.MaxValue ? "" : Convert.ToString(value);

  private string GetDateMinValue(DateTime value)
  {
    return value == Intermech.Interfaces.Sets.Consts.dateMinusInfinity ? "" : value.ToShortDateString();
  }

  private string GetDateMaxValue(DateTime value)
  {
    return value == Intermech.Interfaces.Sets.Consts.datePlusInfinity ? "" : value.ToShortDateString();
  }

  private void rbSeries_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChange || !(sender as RadioButton).Checked)
      return;
    this.byDate = this.rbDates.Checked;
    this.UpdateControls();
    this.lv.Items.Clear();
    this.set = !this.byDate ? (ISet) new Set<int>() : (ISet) new Set<DateTime>();
    this.changed = true;
    this.selIndex = -1;
  }

  private void lv_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lv.SelectedIndices == null || this.lv.SelectedIndices.Count == 0)
      return;
    this.selIndex = this.lv.SelectedIndices[0];
    if (this.set is Set<int>)
    {
      IRange<int> range = ((Set<int>) this.set).Ranges[this.selIndex];
      if (range.MinValue == int.MinValue)
        this.seFrom.Text = "";
      else
        this.seFrom.Text = Convert.ToString(range.MinValue);
      if (range.MaxValue == int.MaxValue)
        this.seTo.Text = "";
      else
        this.seTo.Text = Convert.ToString(range.MaxValue);
    }
    else
    {
      IRange<DateTime> range = ((Set<DateTime>) this.set).Ranges[this.selIndex];
      if (range.MinValue == Intermech.Interfaces.Sets.Consts.dateMinusInfinity)
        this.deFrom.Text = "";
      else
        this.deFrom.EditValue = (object) range.MinValue;
      if (range.MaxValue == Intermech.Interfaces.Sets.Consts.datePlusInfinity)
        this.deTo.Text = "";
      else
        this.deTo.EditValue = (object) range.MaxValue;
    }
  }

  private void btnClear_Click(object sender, EventArgs e)
  {
    this.lv.Items.Clear();
    this.selIndex = -1;
    this.set.Clear();
    this.changed = true;
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    if (this.CheckDoubleEmpty(this.byDate))
      return;
    if (this.byDate)
    {
      DateTime minValue = this.deFrom.Text != "" ? Convert.ToDateTime(this.deFrom.EditValue) : Intermech.Interfaces.Sets.Consts.dateMinusInfinity;
      DateTime maxValue = this.deTo.Text != "" ? Convert.ToDateTime(this.deTo.EditValue) : Intermech.Interfaces.Sets.Consts.datePlusInfinity;
      if (minValue > maxValue)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_337"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
      }
      else
      {
        DateTimeRange dateTimeRange = new DateTimeRange(minValue, maxValue);
        Set<DateTime> set = this.set as Set<DateTime>;
        if (!set.CanAdd((IRange<DateTime>) dateTimeRange))
          return;
        set.Add((IRange<DateTime>) dateTimeRange);
        set.Compact();
        this.SetList();
        this.changed = true;
      }
    }
    else
    {
      int minValue = this.seFrom.Text != "" ? Convert.ToInt32(this.seFrom.Text) : int.MinValue;
      int maxValue = this.seTo.Text != "" ? Convert.ToInt32(this.seTo.Text) : int.MaxValue;
      if (minValue > maxValue)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_337"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
      }
      else
      {
        Int32Range int32Range = new Int32Range(minValue, maxValue);
        Set<int> set = this.set as Set<int>;
        if (!set.CanAdd((IRange<int>) int32Range))
          return;
        set.Add((IRange<int>) int32Range);
        set.Compact();
        this.SetList();
        this.changed = true;
      }
    }
  }

  private void btnChange_Click(object sender, EventArgs e)
  {
    if (this.selIndex < 0 || this.set.IsEmpty || this.CheckDoubleEmpty(this.byDate))
      return;
    if (this.byDate)
    {
      DateTimeRange dateTimeRange = new DateTimeRange(this.deFrom.Text != "" ? Convert.ToDateTime(this.deFrom.EditValue) : Intermech.Interfaces.Sets.Consts.dateMinusInfinity, this.deTo.Text != "" ? Convert.ToDateTime(this.deTo.EditValue) : Intermech.Interfaces.Sets.Consts.datePlusInfinity);
      Set<DateTime> set = this.set as Set<DateTime>;
      if (set.Ranges.Count <= 0)
        return;
      set.Ranges.RemoveAt(this.selIndex);
      if (set.CanAdd((IRange<DateTime>) dateTimeRange))
      {
        set.Add((IRange<DateTime>) dateTimeRange);
        set.Compact();
        this.SetList();
      }
      this.changed = true;
    }
    else
    {
      Int32Range int32Range = new Int32Range(this.seFrom.Text != "" ? Convert.ToInt32(this.seFrom.Text) : int.MinValue, this.seTo.Text != "" ? Convert.ToInt32(this.seTo.Text) : int.MaxValue);
      Set<int> set = this.set as Set<int>;
      if (set.Ranges.Count <= 0)
        return;
      set.Ranges.RemoveAt(this.selIndex);
      if (set.CanAdd((IRange<int>) int32Range))
      {
        set.Add((IRange<int>) int32Range);
        set.Compact();
        this.SetList();
      }
      this.changed = true;
    }
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (this.selIndex < 0 || this.set.IsEmpty)
      return;
    if (this.byDate)
    {
      Set<DateTime> set = this.set as Set<DateTime>;
      set.Ranges.RemoveAt(this.selIndex);
      set.Compact();
      this.SetList();
      this.changed = true;
    }
    else
    {
      Set<int> set = this.set as Set<int>;
      set.Ranges.RemoveAt(this.selIndex);
      set.Compact();
      this.SetList();
      this.changed = true;
    }
  }

  private void textEdit1_EditValueChanging(object sender, ChangingEventArgs e)
  {
    foreach (char c in Convert.ToString(e.NewValue))
    {
      if (!char.IsDigit(c))
      {
        e.Cancel = true;
        break;
      }
    }
  }

  private void lv_SizeChanged(object sender, EventArgs e)
  {
    int num = (this.lv.Width - 25) / 2;
    this.colHeaderFrom.Width = num;
    this.colHeaderTo.Width = num;
  }

  private bool CheckDoubleEmpty(bool byDate)
  {
    if (byDate)
    {
      if (this.deFrom.Text != "" || this.deTo.Text != "")
        return false;
    }
    else if (this.seFrom.Text != "" || this.seTo.Text != "")
      return false;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("ECO.Client_338"), LocalizationHolder.rm.GetString("ECO.Client_176"), MessageBoxButtons.OK);
    return true;
  }

  private void SerDateDiap_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    HelpProvidersClass.ShowHelpTopic(2911);
  }

  private void SerDateDiap_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    HelpProvidersClass.ShowHelpTopic(2911);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ListViewItem listViewItem1 = new ListViewItem(new string[3]
    {
      "363462536",
      "111",
      "222"
    }, 1);
    ListViewItem listViewItem2 = new ListViewItem(new string[3]
    {
      "79679679",
      "573",
      "65685"
    }, 2);
    this.panel1 = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.btnClear = new Button();
    this.btnChange = new Button();
    this.btnAdd = new Button();
    this.deTo = new DateEdit();
    this.label6 = new Label();
    this.deFrom = new DateEdit();
    this.label5 = new Label();
    this.label4 = new Label();
    this.label3 = new Label();
    this.rbDates = new RadioButton();
    this.rbSeries = new RadioButton();
    this.lv = new ListView();
    this.colHeaderFrom = new ColumnHeader();
    this.colHeaderTo = new ColumnHeader();
    this.btnDelete = new Button();
    this.seFrom = new TextEdit();
    this.seTo = new TextEdit();
    this.panel1.SuspendLayout();
    this.deTo.Properties.BeginInit();
    this.deFrom.Properties.BeginInit();
    this.seFrom.Properties.BeginInit();
    this.seTo.Properties.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.btnOK);
    this.panel1.Controls.Add((Control) this.btnCancel);
    this.panel1.Controls.Add((Control) this.btnClear);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 125);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(626, 30);
    this.panel1.TabIndex = 1;
    this.btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(458, 3);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(539, 3);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 0;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnClear.Location = new Point(3, 3);
    this.btnClear.Name = "btnClear";
    this.btnClear.Size = new Size(116, 23);
    this.btnClear.TabIndex = 24;
    this.btnClear.Text = "Очистить всё";
    this.btnClear.UseVisualStyleBackColor = true;
    this.btnClear.Click += new EventHandler(this.btnClear_Click);
    this.btnChange.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnChange.Location = new Point(134, 95);
    this.btnChange.Name = "btnChange";
    this.btnChange.Size = new Size(75, 23);
    this.btnChange.TabIndex = 26;
    this.btnChange.Text = "Изменить";
    this.btnChange.UseVisualStyleBackColor = true;
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    this.btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnAdd.Location = new Point(53, 95);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(75, 23);
    this.btnAdd.TabIndex = 25;
    this.btnAdd.Text = "Добавить";
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.deTo.EditValue = (object) new DateTime(1801, 1, 1, 0, 0, 0, 0);
    this.deTo.Location = new Point(216, 38);
    this.deTo.Name = "deTo";
    this.deTo.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.deTo.Size = new Size(75, 23);
    this.deTo.TabIndex = 23;
    this.label6.AutoSize = true;
    this.label6.Location = new Point(191, 42);
    this.label6.Name = "label6";
    this.label6.Size = new Size(19, 13);
    this.label6.TabIndex = 22;
    this.label6.Text = "по";
    this.deFrom.EditValue = (object) new DateTime(1801, 1, 1, 0, 0, 0, 0);
    this.deFrom.Location = new Point(110, 38);
    this.deFrom.Name = "deFrom";
    this.deFrom.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.deFrom.Size = new Size(75, 23);
    this.deFrom.TabIndex = 21;
    this.label5.AutoSize = true;
    this.label5.Location = new Point(191, 14);
    this.label5.Name = "label5";
    this.label5.Size = new Size(19, 13);
    this.label5.TabIndex = 19;
    this.label5.Text = "по";
    this.label4.AutoSize = true;
    this.label4.Location = new Point(91, 42);
    this.label4.Name = "label4";
    this.label4.Size = new Size(13, 13);
    this.label4.TabIndex = 17;
    this.label4.Text = "c";
    this.label3.AutoSize = true;
    this.label3.Location = new Point(91, 14);
    this.label3.Name = "label3";
    this.label3.Size = new Size(13, 13);
    this.label3.TabIndex = 16 /*0x10*/;
    this.label3.Text = "c";
    this.rbDates.AutoSize = true;
    this.rbDates.Location = new Point(12, 40);
    this.rbDates.Name = "rbDates";
    this.rbDates.Size = new Size(76, 17);
    this.rbDates.TabIndex = 15;
    this.rbDates.Text = "По датам:";
    this.rbDates.UseVisualStyleBackColor = true;
    this.rbDates.CheckedChanged += new EventHandler(this.rbSeries_CheckedChanged);
    this.rbSeries.AutoSize = true;
    this.rbSeries.Checked = true;
    this.rbSeries.Location = new Point(12, 12);
    this.rbSeries.Name = "rbSeries";
    this.rbSeries.Size = new Size(83, 17);
    this.rbSeries.TabIndex = 14;
    this.rbSeries.TabStop = true;
    this.rbSeries.Text = "По сериям:";
    this.rbSeries.UseVisualStyleBackColor = true;
    this.rbSeries.CheckedChanged += new EventHandler(this.rbSeries_CheckedChanged);
    this.lv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lv.Columns.AddRange(new ColumnHeader[2]
    {
      this.colHeaderFrom,
      this.colHeaderTo
    });
    this.lv.FullRowSelect = true;
    this.lv.GridLines = true;
    this.lv.HideSelection = false;
    this.lv.Items.AddRange(new ListViewItem[2]
    {
      listViewItem1,
      listViewItem2
    });
    this.lv.Location = new Point(297, 12);
    this.lv.MultiSelect = false;
    this.lv.Name = "lv";
    this.lv.ShowItemToolTips = true;
    this.lv.Size = new Size(317, 107);
    this.lv.TabIndex = 27;
    this.lv.UseCompatibleStateImageBehavior = false;
    this.lv.View = View.Details;
    this.lv.SelectedIndexChanged += new EventHandler(this.lv_SelectedIndexChanged);
    this.lv.SizeChanged += new EventHandler(this.lv_SizeChanged);
    this.colHeaderFrom.Text = "С";
    this.colHeaderFrom.Width = 150;
    this.colHeaderTo.Text = "По";
    this.colHeaderTo.Width = 143;
    this.btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnDelete.Location = new Point(215, 95);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(75, 23);
    this.btnDelete.TabIndex = 28;
    this.btnDelete.Text = "Удалить";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.seFrom.EditValue = (object) "";
    this.seFrom.Location = new Point(110, 12);
    this.seFrom.Name = "seFrom";
    this.seFrom.Size = new Size(75, 20);
    this.seFrom.TabIndex = 31 /*0x1F*/;
    this.seFrom.EditValueChanging += new ChangingEventHandler(this.textEdit1_EditValueChanging);
    this.seTo.EditValue = (object) "";
    this.seTo.Location = new Point(216, 12);
    this.seTo.Name = "seTo";
    this.seTo.Size = new Size(75, 20);
    this.seTo.TabIndex = 32 /*0x20*/;
    this.seTo.EditValueChanging += new ChangingEventHandler(this.textEdit1_EditValueChanging);
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(626, 155);
    this.Controls.Add((Control) this.seTo);
    this.Controls.Add((Control) this.seFrom);
    this.Controls.Add((Control) this.btnDelete);
    this.Controls.Add((Control) this.lv);
    this.Controls.Add((Control) this.btnChange);
    this.Controls.Add((Control) this.btnAdd);
    this.Controls.Add((Control) this.deTo);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this.deFrom);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.rbDates);
    this.Controls.Add((Control) this.rbSeries);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(640, 193);
    this.Name = nameof (SerDateDiap);
    this.Text = "Диапазон серий или дат";
    this.HelpButtonClicked += new CancelEventHandler(this.SerDateDiap_HelpButtonClicked);
    this.HelpRequested += new HelpEventHandler(this.SerDateDiap_HelpRequested);
    this.panel1.ResumeLayout(false);
    this.deTo.Properties.EndInit();
    this.deFrom.Properties.EndInit();
    this.seFrom.Properties.EndInit();
    this.seTo.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
