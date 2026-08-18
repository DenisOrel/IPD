// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs.ArtsCompositionMeasureForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Dialogs;

/// <summary>ArtCompositionMeasureForm</summary>
public class ArtsCompositionMeasureForm : Form
{
  private MeasureDialogResult _measureDialogResult = MeasureDialogResult.Cancel;
  private SplitButton btnAdd;
  private SplitButton btnAddAll;
  private Button btnCancel;
  private Button btnAbort;
  private TextBox txbCountRemain;
  private TextBox txbCountCompos;
  private ComboBox cbMeasures;
  private ComboBox edValue;
  private Label lblValue;
  private Label lblMeasures;
  private Label lblCountRemain;
  private Label lblCountCompos;
  private ErrorProvider infoProvider;
  private ContextMenuStrip cmAddButtonMode;
  private ToolStripMenuItem tsmiForAllObjects;
  private ToolStripMenuItem tsmiForCurrentObject;
  private FlowLayoutPanel flowLayoutPanelButtons;
  private TableLayoutPanel tableLayoutPanelControls;
  private IContainer components;
  /// <summary>
  /// 
  /// </summary>
  private readonly IWin32Window _owner;
  /// <summary>Оставшееся кол-во</summary>
  private MeasuredValue _countRemain;
  /// <summary>Выбранное кол-во</summary>
  private MeasuredValue _measuredVal;
  /// <summary>
  /// 
  /// </summary>
  private readonly MeasureDescriptorComparer _mdPhysComparer = new MeasureDescriptorComparer(true, true);
  /// <summary>
  /// 
  /// </summary>
  private readonly MeasureDescriptorComparer _mdKComparer = new MeasureDescriptorComparer(false, true);
  /// <summary>
  /// 
  /// </summary>
  private bool _showRemainQty = true;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ArtsCompositionMeasureForm));
    this.btnAdd = new SplitButton();
    this.cmAddButtonMode = new ContextMenuStrip(this.components);
    this.tsmiForCurrentObject = new ToolStripMenuItem();
    this.tsmiForAllObjects = new ToolStripMenuItem();
    this.btnCancel = new Button();
    this.cbMeasures = new ComboBox();
    this.lblValue = new Label();
    this.lblMeasures = new Label();
    this.edValue = new ComboBox();
    this.txbCountRemain = new TextBox();
    this.txbCountCompos = new TextBox();
    this.btnAbort = new Button();
    this.btnAddAll = new SplitButton();
    this.lblCountRemain = new Label();
    this.lblCountCompos = new Label();
    this.infoProvider = new ErrorProvider(this.components);
    this.flowLayoutPanelButtons = new FlowLayoutPanel();
    this.tableLayoutPanelControls = new TableLayoutPanel();
    this.cmAddButtonMode.SuspendLayout();
    ((ISupportInitialize) this.infoProvider).BeginInit();
    this.flowLayoutPanelButtons.SuspendLayout();
    this.tableLayoutPanelControls.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.ContextMenuStrip = this.cmAddButtonMode;
    this.btnAdd.DialogResult = DialogResult.OK;
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.SplitHeight = 20;
    this.btnAdd.Click += new EventHandler(this.okBtn_Click);
    this.cmAddButtonMode.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiForCurrentObject,
      (ToolStripItem) this.tsmiForAllObjects
    });
    this.cmAddButtonMode.Name = "cmButtonMode";
    componentResourceManager.ApplyResources((object) this.cmAddButtonMode, "cmAddButtonMode");
    this.cmAddButtonMode.Opening += new CancelEventHandler(this.cmAddButtonMode_Opening);
    this.tsmiForCurrentObject.Name = "tsmiForCurrentObject";
    componentResourceManager.ApplyResources((object) this.tsmiForCurrentObject, "tsmiForCurrentObject");
    this.tsmiForCurrentObject.Click += new EventHandler(this.tsmiForCurrentObject_Click);
    this.tsmiForAllObjects.Name = "tsmiForAllObjects";
    componentResourceManager.ApplyResources((object) this.tsmiForAllObjects, "tsmiForAllObjects");
    this.tsmiForAllObjects.Click += new EventHandler(this.tsmiForAllObjects_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.cbMeasures.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.cbMeasures, "cbMeasures");
    this.cbMeasures.Name = "cbMeasures";
    componentResourceManager.ApplyResources((object) this.lblValue, "lblValue");
    this.lblValue.Name = "lblValue";
    componentResourceManager.ApplyResources((object) this.lblMeasures, "lblMeasures");
    this.lblMeasures.Name = "lblMeasures";
    componentResourceManager.ApplyResources((object) this.edValue, "edValue");
    this.edValue.DrawMode = DrawMode.OwnerDrawFixed;
    this.edValue.FormattingEnabled = true;
    this.edValue.Name = "edValue";
    this.edValue.DrawItem += new DrawItemEventHandler(this.valueEdit_DrawItem);
    this.edValue.DropDown += new EventHandler(this.valueEdit_DropDown);
    this.edValue.SelectionChangeCommitted += new EventHandler(this.valueEdit_SelectionChangeCommitted);
    this.edValue.SelectedValueChanged += new EventHandler(this.edValue_SelectedValueChanged);
    this.edValue.TextChanged += new EventHandler(this.edValue_TextChanged);
    componentResourceManager.ApplyResources((object) this.txbCountRemain, "txbCountRemain");
    this.txbCountRemain.Name = "txbCountRemain";
    this.txbCountRemain.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.txbCountCompos, "txbCountCompos");
    this.txbCountCompos.Name = "txbCountCompos";
    this.txbCountCompos.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.btnAbort, "btnAbort");
    this.btnAbort.DialogResult = DialogResult.Abort;
    this.btnAbort.Name = "btnAbort";
    this.btnAbort.Click += new EventHandler(this.btnAbort_Click);
    componentResourceManager.ApplyResources((object) this.btnAddAll, "btnAddAll");
    this.btnAddAll.ContextMenuStrip = this.cmAddButtonMode;
    this.btnAddAll.DialogResult = DialogResult.OK;
    this.btnAddAll.Name = "btnAddAll";
    this.btnAddAll.Click += new EventHandler(this.btnAddAll_Click);
    componentResourceManager.ApplyResources((object) this.lblCountRemain, "lblCountRemain");
    this.lblCountRemain.Name = "lblCountRemain";
    componentResourceManager.ApplyResources((object) this.lblCountCompos, "lblCountCompos");
    this.lblCountCompos.Name = "lblCountCompos";
    this.infoProvider.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this.infoProvider, "infoProvider");
    this.flowLayoutPanelButtons.Controls.Add((Control) this.btnAbort);
    this.flowLayoutPanelButtons.Controls.Add((Control) this.btnCancel);
    this.flowLayoutPanelButtons.Controls.Add((Control) this.btnAddAll);
    this.flowLayoutPanelButtons.Controls.Add((Control) this.btnAdd);
    componentResourceManager.ApplyResources((object) this.flowLayoutPanelButtons, "flowLayoutPanelButtons");
    this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
    componentResourceManager.ApplyResources((object) this.tableLayoutPanelControls, "tableLayoutPanelControls");
    this.tableLayoutPanelControls.Controls.Add((Control) this.txbCountRemain, 2, 1);
    this.tableLayoutPanelControls.Controls.Add((Control) this.lblValue, 0, 0);
    this.tableLayoutPanelControls.Controls.Add((Control) this.lblCountRemain, 2, 0);
    this.tableLayoutPanelControls.Controls.Add((Control) this.edValue, 0, 1);
    this.tableLayoutPanelControls.Controls.Add((Control) this.lblCountCompos, 3, 0);
    this.tableLayoutPanelControls.Controls.Add((Control) this.cbMeasures, 1, 1);
    this.tableLayoutPanelControls.Controls.Add((Control) this.txbCountCompos, 3, 1);
    this.tableLayoutPanelControls.Controls.Add((Control) this.lblMeasures, 1, 0);
    this.tableLayoutPanelControls.Name = "tableLayoutPanelControls";
    this.AcceptButton = (IButtonControl) this.btnAdd;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.tableLayoutPanelControls);
    this.Controls.Add((Control) this.flowLayoutPanelButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ArtsCompositionMeasureForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.cmAddButtonMode.ResumeLayout(false);
    ((ISupportInitialize) this.infoProvider).EndInit();
    this.flowLayoutPanelButtons.ResumeLayout(false);
    this.tableLayoutPanelControls.ResumeLayout(false);
    this.tableLayoutPanelControls.PerformLayout();
    this.ResumeLayout(false);
  }

  /// <summary>Получение текущего дескриптора</summary>
  /// <returns></returns>
  private MeasureDescriptor GetMeasureCB() => (MeasureDescriptor) this.cbMeasures.SelectedItem;

  /// <summary>Назначение текущего дескриптора</summary>
  /// <param name="measureId"></param>
  private void SetMeasureCB(long measureId)
  {
    if (measureId == -1L)
    {
      this.cbMeasures.SelectedItem = (object) null;
    }
    else
    {
      foreach (object obj in this.cbMeasures.Items)
      {
        if (obj is MeasureDescriptor measureDescriptor && measureDescriptor.MeasureID == measureId)
        {
          this.cbMeasures.SelectedItem = (object) measureDescriptor;
          break;
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateControls()
  {
    this.btnAdd.Enabled = this.TryParse(out double _) && this.GetMeasureCB() != null;
  }

  /// <summary>Приведение тек. значение к double</summary>
  /// <param name="d"></param>
  /// <returns></returns>
  private bool TryParse(out double d)
  {
    if (!double.TryParse(this.edValue.Text, out d))
    {
      if (this.edValue.Text.Contains("."))
        this.edValue.Text = this.edValue.Text.Replace('.', ',');
      else if (this.edValue.Text.Contains(","))
        this.edValue.Text = this.edValue.Text.Replace(',', '.');
    }
    return double.TryParse(this.edValue.Text, out d);
  }

  /// <summary>Конструктор</summary>
  public ArtsCompositionMeasureForm()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner"></param>
  public ArtsCompositionMeasureForm(IWin32Window owner)
    : this()
  {
    this._owner = owner;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Заголовок окна</summary>
  public string Caption
  {
    get => this.Text;
    set => this.Text = value;
  }

  /// <summary>Видимость кнопки "Прервать"</summary>
  public bool ShowAbortButton
  {
    get => this.btnAbort.Visible;
    set => this.btnAbort.Visible = value;
  }

  /// <summary>Видимость контролов для "оставшегося количество"</summary>
  public bool ShowRemainQtyControls
  {
    get => this._showRemainQty;
    set
    {
      if (value)
      {
        this.btnAddAll.Visible = true;
        this.tableLayoutPanelControls.ColumnStyles[3].SizeType = SizeType.Absolute;
        this.tableLayoutPanelControls.ColumnStyles[3].Width = (float) sbyte.MaxValue;
        this.tableLayoutPanelControls.ColumnStyles[2].SizeType = SizeType.Absolute;
        this.tableLayoutPanelControls.ColumnStyles[2].Width = (float) sbyte.MaxValue;
        this.tableLayoutPanelControls.ColumnStyles[1].SizeType = SizeType.Absolute;
        this.tableLayoutPanelControls.ColumnStyles[1].Width = (float) sbyte.MaxValue;
      }
      else
      {
        this.btnAddAll.Visible = false;
        this.tableLayoutPanelControls.ColumnStyles[3].SizeType = SizeType.Absolute;
        this.tableLayoutPanelControls.ColumnStyles[3].Width = 0.0f;
        this.tableLayoutPanelControls.ColumnStyles[2].SizeType = SizeType.Absolute;
        this.tableLayoutPanelControls.ColumnStyles[2].Width = 0.0f;
        this.tableLayoutPanelControls.ColumnStyles[1].SizeType = SizeType.Absolute;
        this.tableLayoutPanelControls.ColumnStyles[1].Width = (float) sbyte.MaxValue;
      }
    }
  }

  /// <summary>Показать диалог</summary>
  /// <param name="aMeasureValue">Количество</param>
  /// <param name="countDesign">Количество в КСЕ</param>
  /// <param name="countTech">Количество в ТП</param>
  /// <param name="aMeasureDescriptorList">Список дескрипторов</param>
  /// <param name="multiSelect"></param>
  /// <param name="getDefaultMeasureId">Делегат для ед. измерения по-умолчанию</param>
  /// <returns></returns>
  public MeasureDialogResult ExecuteDialog(
    ref MeasuredValue aMeasureValue,
    MeasuredValue countDesign,
    MeasuredValue countTech,
    MeasureDescriptor[] aMeasureDescriptorList,
    bool multiSelect = false,
    GetDefaultMeasureIDDelegate getDefaultMeasureId = null)
  {
    this.cbMeasures.BeginUpdate();
    try
    {
      this.cbMeasures.Items.Clear();
      if (aMeasureDescriptorList != null)
      {
        List<MeasureDescriptor> source = new List<MeasureDescriptor>((IEnumerable<MeasureDescriptor>) aMeasureDescriptorList);
        source.Sort((IComparer<MeasureDescriptor>) this._mdPhysComparer);
        this.cbMeasures.Items.AddRange(source.Cast<object>().ToArray<object>());
      }
    }
    finally
    {
      this.cbMeasures.EndUpdate();
      if (countDesign != null)
      {
        foreach (MeasureDescriptor measureDescriptor in this.cbMeasures.Items)
        {
          if (measureDescriptor != null && measureDescriptor.MeasureID == countDesign.MeasureID)
          {
            this.cbMeasures.SelectedItem = (object) measureDescriptor;
            break;
          }
        }
      }
      if (this.cbMeasures.SelectedItem == null && this.cbMeasures.Items.Count != 0)
        this.cbMeasures.SelectedItem = this.cbMeasures.Items[0];
    }
    if (countDesign != null)
      this.txbCountCompos.Text = countDesign.Caption;
    if (countDesign != null)
    {
      this._countRemain = countTech != null ? MeasureHelper.Substract(countDesign, countTech) : countDesign;
      this.btnAddAll.Enabled = true;
      this.txbCountRemain.Text = this._countRemain.Caption;
    }
    if (aMeasureValue != null && aMeasureValue.Caption != string.Empty)
    {
      this.edValue.Text = Convert.ToString(aMeasureValue.Value, (IFormatProvider) CultureInfo.InvariantCulture);
      this.SetMeasureCB(aMeasureValue.MeasureID);
    }
    else
    {
      this.edValue.Text = string.Empty;
      long measureId = -1;
      if (getDefaultMeasureId != null)
        measureId = getDefaultMeasureId((object) this);
      if (measureId != -1L)
        this.SetMeasureCB(measureId);
    }
    this.btnAdd.ShowDropDownMenu = this.btnAddAll.ShowDropDownMenu = this.tsmiForAllObjects.Enabled = multiSelect;
    if (this.ShowDialog(this._owner) == DialogResult.OK)
      aMeasureValue = this._measuredVal;
    return this._measureDialogResult;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void valueEdit_DropDown(object sender, EventArgs e)
  {
    this.edValue.BeginUpdate();
    try
    {
      this.edValue.Items.Clear();
      MeasureDescriptor measureCb = this.GetMeasureCB();
      double d;
      if (measureCb == null || !this.TryParse(out d))
        return;
      List<MeasureDescriptor> measureDescriptorList = new List<MeasureDescriptor>();
      for (int index = 0; index < this.cbMeasures.Items.Count; ++index)
      {
        if (((MeasureDescriptor) this.cbMeasures.Items[index]).PhysicalQuantityID == measureCb.PhysicalQuantityID)
          measureDescriptorList.Add((MeasureDescriptor) this.cbMeasures.Items[index]);
      }
      measureDescriptorList.Sort((IComparer<MeasureDescriptor>) this._mdKComparer);
      for (int index = 0; index < measureDescriptorList.Count; ++index)
      {
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue($"{(object) (d * measureCb.K / measureDescriptorList[index].K)} {measureDescriptorList[index].ShortName}");
        this.edValue.Items.Add((object) new MeasuredValueContainer(measuredValue.Value, measuredValue.MeasureID, measuredValue.Caption));
      }
      this.edValue.SelectedItem = (object) null;
    }
    finally
    {
      this.edValue.EndUpdate();
    }
  }

  /// <summary>Прорисовка элементов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void valueEdit_DrawItem(object sender, DrawItemEventArgs e)
  {
    if (e.Index == -1)
      return;
    string s = string.Empty;
    if (this.edValue.Items[e.Index] is MeasuredValue measuredValue)
      s = measuredValue.Caption;
    e.DrawBackground();
    using (Brush brush = (Brush) new SolidBrush(e.ForeColor))
      e.Graphics.DrawString(s, e.Font, brush, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    if ((e.State & DrawItemState.Selected) == DrawItemState.None)
      return;
    e.DrawFocusRectangle();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void valueEdit_SelectionChangeCommitted(object sender, EventArgs e)
  {
    if (this.edValue.SelectedItem is MeasuredValue selectedItem)
      this.SetMeasureCB(selectedItem.MeasureID);
    this.CheckRemainQty();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void edValue_TextChanged(object sender, EventArgs e)
  {
    this.CheckRemainQty();
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void edValue_SelectedValueChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>
  /// 
  /// </summary>
  private void CheckRemainQty()
  {
    int num = this.TryParse(out double _) ? 1 : 0;
    MeasureDescriptor measureCb = this.GetMeasureCB();
    if (num == 0 || measureCb == null)
    {
      this.infoProvider.SetError((Control) this.edValue, string.Empty);
    }
    else
    {
      MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue($"{this.edValue.Text} {measureCb.ShortName}");
      this.infoProvider.SetIconAlignment((Control) this.edValue, ErrorIconAlignment.MiddleRight);
      this.infoProvider.SetIconPadding((Control) this.edValue, -35);
      this.infoProvider.SetError((Control) this.edValue, this._countRemain == null || MeasureHelper.Compare(measuredValue, this._countRemain) != CompareResult.More ? string.Empty : LocalizationHolder.rm.GetString("TechCard.Client_524"));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void okBtn_Click(object sender, EventArgs e)
  {
    if (!this.TryParse(out double _) || this.GetMeasureCB() == null)
    {
      this.DialogResult = DialogResult.None;
    }
    else
    {
      this._measuredVal = MeasureHelper.ConvertToMeasuredValue($"{this.edValue.Text} {this.GetMeasureCB().ShortName}");
      this._measureDialogResult = MeasureDialogResult.Add;
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>Команда "Добавить все"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAddAll_Click(object sender, EventArgs e)
  {
    if (this._countRemain == null || this._countRemain.Value <= 0.0)
      return;
    this._measuredVal = this._countRemain;
    this._measureDialogResult = MeasureDialogResult.AddAllQuantity;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Команда "Прервать"</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAbort_Click(object sender, EventArgs e)
  {
    this._measureDialogResult = MeasureDialogResult.Terminate;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiForCurrentObject_Click(object sender, EventArgs e)
  {
    if (!(this.cmAddButtonMode.SourceControl is Button sourceControl))
      return;
    sourceControl.PerformClick();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiForAllObjects_Click(object sender, EventArgs e)
  {
    if (this.cmAddButtonMode.SourceControl == this.btnAdd)
    {
      if (!this.TryParse(out double _) || this.GetMeasureCB() == null)
      {
        this.DialogResult = DialogResult.None;
      }
      else
      {
        this._measuredVal = MeasureHelper.ConvertToMeasuredValue($"{this.edValue.Text} {this.GetMeasureCB().ShortName}");
        this._measureDialogResult = MeasureDialogResult.AddForAll;
        this.DialogResult = DialogResult.OK;
      }
    }
    else
    {
      if (this.cmAddButtonMode.SourceControl != this.btnAddAll)
        return;
      this._measureDialogResult = MeasureDialogResult.AddAllQuantityForAll;
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmAddButtonMode_Opening(object sender, CancelEventArgs e)
  {
  }
}
