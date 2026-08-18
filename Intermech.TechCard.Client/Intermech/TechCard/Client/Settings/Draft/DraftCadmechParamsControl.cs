// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.Draft.DraftCadmechParamsControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.TechAcad;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Settings.Draft;

/// <summary>Контрол редактирования параметров редактора эскизов</summary>
internal class DraftCadmechParamsControl : UserControl
{
  /// <summary>Load / update params mode</summary>
  private bool _updateDataMode;
  /// <summary>Настройки</summary>
  private readonly TechAcadParamsItem _draftParamsItem;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pnlClient;
  private ToolStripMenuItem tsTemplMoveLast;
  private Panel pnlTop;
  private GroupBox grbTop;
  private Label lblCaption;
  private ContextMenuStrip cmTemplates;
  private ToolStripMenuItem tsTemplMoveFirst;
  private ToolStripMenuItem tsTemplMoveUp;
  private ToolStripMenuItem tsTemplMoveDown;
  private Label lblDraftExt;
  private Label lblPrototype;
  private Label lblParams;
  private Label lblAppPath;
  private TextBox tbxAppPath;
  private Button btnAppPath;
  private Button btnPrototype;
  private TextBox tbxPrototype;
  private TextBox tbxParams;
  private TextBox tbxDraftExt;
  private OpenFileDialog ofdlgAcad;
  private OpenFileDialog ofdlgPrototype;
  private ErrorProvider errorProvider;
  private Button btnDefaultSettings;

  /// <summary>Initialize data</summary>
  private void InitializeData()
  {
  }

  /// <summary>Загрузка параметров / обновление контролов</summary>
  private void UpdateControls()
  {
    if (this._draftParamsItem == null)
      return;
    this._updateDataMode = true;
    try
    {
      this.UpdateAppPath(this._draftParamsItem.ApplPath);
      this.UpdateParams(this._draftParamsItem.Params);
      this.UpdateDraftExt(this._draftParamsItem.FileExtention);
      this.UpdatePrototype(this._draftParamsItem.PrototypeDraft);
    }
    finally
    {
      this._updateDataMode = false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  private void UpdateAppPath(string value)
  {
    try
    {
      if (this.tbxAppPath.Text == value)
        return;
      this.tbxAppPath.Text = value;
      this.DoChanged();
    }
    finally
    {
      this.ValidateAppPath();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  private void UpdateParams(string value)
  {
    if (this.tbxParams.Text == value)
      return;
    this.tbxParams.Text = value;
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  private void UpdateDraftExt(string value)
  {
    if (this.tbxDraftExt.Text == value)
      return;
    this.tbxDraftExt.Text = value;
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  private void UpdatePrototype(string value)
  {
    try
    {
      if (this.tbxPrototype.Text == value)
        return;
      this.tbxPrototype.Text = value;
      this.DoChanged();
    }
    finally
    {
      this.ValidatePrototype();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ValidateAppPath()
  {
    this.errorProvider.SetError((Control) this.btnAppPath, string.Empty);
    string str = this.tbxAppPath.Text.Trim();
    if (str == string.Empty)
    {
      this.errorProvider.SetError((Control) this.btnAppPath, LocalizationHolder.rm.GetString("TechCard.Client_414"));
    }
    else
    {
      string path = str.Replace("\"", string.Empty);
      try
      {
        if (File.Exists(path))
          return;
        this.errorProvider.SetError((Control) this.btnAppPath, string.Format(LocalizationHolder.rm.GetString("TechCard.Client_415"), (object) str));
      }
      catch (ArgumentException ex)
      {
        this.errorProvider.SetError((Control) this.btnAppPath, ex.Message);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void ValidatePrototype()
  {
    this.errorProvider.SetError((Control) this.btnPrototype, string.Empty);
    string str = this.tbxPrototype.Text.Trim();
    if (str == string.Empty)
    {
      this.errorProvider.SetError((Control) this.btnPrototype, LocalizationHolder.rm.GetString("TechCard.Client_416"));
    }
    else
    {
      string path = str.Replace("\"", string.Empty);
      try
      {
        if (File.Exists(path))
          return;
        this.errorProvider.SetError((Control) this.btnPrototype, string.Format(LocalizationHolder.rm.GetString("TechCard.Client_415"), (object) str));
      }
      catch (ArgumentException ex)
      {
        this.errorProvider.SetError((Control) this.btnPrototype, ex.Message);
      }
    }
  }

  /// <summary>Fire changed event</summary>
  private void DoChanged()
  {
    if (this._updateDataMode)
      return;
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  /// <summary>Конструктор</summary>
  public DraftCadmechParamsControl()
  {
    this.InitializeComponent();
    this._draftParamsItem = new TechAcadParamsItem();
    this.InitializeData();
  }

  /// <summary>Загрузка параметров:</summary>
  public void LoadParams(bool loadDefault)
  {
    if (this._draftParamsItem == null)
      return;
    bool flag;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ITechAcadParamsService customService = sessionKeeper.Session.GetCustomService(typeof (ITechAcadParamsService)) as ITechAcadParamsService;
      flag = TechAcadParamsHelper.LoadData(this._draftParamsItem, sessionKeeper.Session, customService, false);
    }
    if (!flag & loadDefault && MessageBox.Show($"{LocalizationHolder.rm.GetString("TechCard.Client_421")} {LocalizationHolder.rm.GetString("TechCard.Client_422")}", LocalizationHolder.rm.GetString("TechCard.Client_108"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ITechAcadParamsService service = ServiceUtils.GetService<ITechAcadParamsService>((object) sessionKeeper.Session, false);
        if (service != null)
        {
          TechAcadParamsHelper.LoadData(this._draftParamsItem, sessionKeeper.Session, service, true);
          TechAcadParamsHelper.SaveData(this._draftParamsItem, sessionKeeper.Session, service);
        }
      }
    }
    this.UpdateControls();
  }

  /// <summary>Сохранение параметров</summary>
  public void SaveParams()
  {
    if (this._draftParamsItem == null)
      return;
    this._draftParamsItem.ApplPath = this.tbxAppPath.Text;
    this._draftParamsItem.Params = this.tbxParams.Text;
    this._draftParamsItem.PrototypeDraft = this.tbxPrototype.Text;
    this._draftParamsItem.FileExtention = this.tbxDraftExt.Text;
  }

  /// <summary>Параметры редактора эскизов</summary>
  public TechAcadParamsItem DraftParamsItem => this._draftParamsItem;

  /// <summary>Событие на изменение</summary>
  public event EventHandler Changed;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAppPath_Click(object sender, EventArgs e)
  {
    this.ofdlgAcad.FileName = Path.GetFileName(this.tbxAppPath.Text);
    try
    {
      if (this.tbxAppPath.Text != string.Empty)
        this.ofdlgPrototype.InitialDirectory = Path.GetDirectoryName(this.tbxAppPath.Text);
    }
    catch (ArgumentException ex)
    {
      this.ofdlgPrototype.InitialDirectory = Directory.GetCurrentDirectory();
    }
    if (this.ofdlgAcad.ShowDialog() != DialogResult.OK)
      return;
    this.UpdateAppPath(this.ofdlgAcad.FileName);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnPrototype_Click(object sender, EventArgs e)
  {
    string str = sc_19762.ssp_techcard_19763();
    string text = this.tbxDraftExt.Text;
    if (text != "")
      str = $"{str}|*.{text}|*.{text}";
    this.ofdlgPrototype.Filter = str;
    this.ofdlgPrototype.FileName = Path.GetFileName(this.tbxPrototype.Text);
    try
    {
      if (this.tbxPrototype.Text != string.Empty)
        this.ofdlgPrototype.InitialDirectory = Path.GetDirectoryName(this.tbxPrototype.Text);
    }
    catch (ArgumentException ex)
    {
      this.ofdlgPrototype.InitialDirectory = Directory.GetCurrentDirectory();
    }
    if (this.ofdlgPrototype.ShowDialog() != DialogResult.OK)
      return;
    this.UpdatePrototype(this.ofdlgPrototype.FileName);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxAppPath_TextChanged(object sender, EventArgs e)
  {
    int num = this._updateDataMode ? 1 : 0;
    this.ValidateAppPath();
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxPrototype_TextChanged(object sender, EventArgs e)
  {
    int num = this._updateDataMode ? 1 : 0;
    this.ValidatePrototype();
    this.DoChanged();
  }

  /// <summary>Загрузка настроек по-умолчанию..</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDefaultSettings_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Client_422"), LocalizationHolder.rm.GetString("TechCard.Client_108"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    string errorMessage;
    if (TechAcadParamsHelper.LoadDefData(this._draftParamsItem, out errorMessage))
    {
      this.UpdateControls();
      this.DoChanged();
    }
    else
    {
      if (string.IsNullOrEmpty(errorMessage))
        return;
      int num = (int) MessageBox.Show(errorMessage, LocalizationHolder.rm.GetString("TechCard.Client_108"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxAppPath_Leave(object sender, EventArgs e)
  {
    this.UpdateAppPath(this.tbxAppPath.Text.Trim());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxPrototype_Leave(object sender, EventArgs e)
  {
    this.UpdatePrototype(this.tbxPrototype.Text.Trim());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxDraftExt_Leave(object sender, EventArgs e)
  {
    this.UpdateDraftExt(this.tbxDraftExt.Text.Trim());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxDraftExt_TextChanged(object sender, EventArgs e) => this.DoChanged();

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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DraftCadmechParamsControl));
    this.pnlClient = new Panel();
    this.btnDefaultSettings = new Button();
    this.tbxDraftExt = new TextBox();
    this.btnPrototype = new Button();
    this.tbxPrototype = new TextBox();
    this.tbxParams = new TextBox();
    this.btnAppPath = new Button();
    this.tbxAppPath = new TextBox();
    this.lblDraftExt = new Label();
    this.lblPrototype = new Label();
    this.lblParams = new Label();
    this.lblAppPath = new Label();
    this.tsTemplMoveLast = new ToolStripMenuItem();
    this.pnlTop = new Panel();
    this.grbTop = new GroupBox();
    this.lblCaption = new Label();
    this.cmTemplates = new ContextMenuStrip(this.components);
    this.tsTemplMoveFirst = new ToolStripMenuItem();
    this.tsTemplMoveUp = new ToolStripMenuItem();
    this.tsTemplMoveDown = new ToolStripMenuItem();
    this.ofdlgAcad = new OpenFileDialog();
    this.ofdlgPrototype = new OpenFileDialog();
    this.errorProvider = new ErrorProvider(this.components);
    this.pnlClient.SuspendLayout();
    this.pnlTop.SuspendLayout();
    this.grbTop.SuspendLayout();
    this.cmTemplates.SuspendLayout();
    ((ISupportInitialize) this.errorProvider).BeginInit();
    this.SuspendLayout();
    this.ofdlgPrototype.RestoreDirectory = true;
    this.pnlClient.Controls.Add((Control) this.btnDefaultSettings);
    this.pnlClient.Controls.Add((Control) this.tbxDraftExt);
    this.pnlClient.Controls.Add((Control) this.btnPrototype);
    this.pnlClient.Controls.Add((Control) this.tbxPrototype);
    this.pnlClient.Controls.Add((Control) this.tbxParams);
    this.pnlClient.Controls.Add((Control) this.btnAppPath);
    this.pnlClient.Controls.Add((Control) this.tbxAppPath);
    this.pnlClient.Controls.Add((Control) this.lblDraftExt);
    this.pnlClient.Controls.Add((Control) this.lblPrototype);
    this.pnlClient.Controls.Add((Control) this.lblParams);
    this.pnlClient.Controls.Add((Control) this.lblAppPath);
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    componentResourceManager.ApplyResources((object) this.btnDefaultSettings, "btnDefaultSettings");
    this.btnDefaultSettings.Name = "btnDefaultSettings";
    this.btnDefaultSettings.UseVisualStyleBackColor = true;
    this.btnDefaultSettings.Click += new EventHandler(this.btnDefaultSettings_Click);
    componentResourceManager.ApplyResources((object) this.tbxDraftExt, "tbxDraftExt");
    this.tbxDraftExt.Name = "tbxDraftExt";
    this.tbxDraftExt.TextChanged += new EventHandler(this.tbxDraftExt_TextChanged);
    this.tbxDraftExt.Leave += new EventHandler(this.tbxDraftExt_Leave);
    componentResourceManager.ApplyResources((object) this.btnPrototype, "btnPrototype");
    this.btnPrototype.Name = "btnPrototype";
    this.btnPrototype.UseVisualStyleBackColor = true;
    this.btnPrototype.Click += new EventHandler(this.btnPrototype_Click);
    componentResourceManager.ApplyResources((object) this.tbxPrototype, "tbxPrototype");
    this.tbxPrototype.Name = "tbxPrototype";
    this.tbxPrototype.TextChanged += new EventHandler(this.tbxPrototype_TextChanged);
    this.tbxPrototype.Leave += new EventHandler(this.tbxPrototype_Leave);
    componentResourceManager.ApplyResources((object) this.tbxParams, "tbxParams");
    this.tbxParams.Name = "tbxParams";
    this.tbxParams.TextChanged += new EventHandler(this.tbxAppPath_TextChanged);
    componentResourceManager.ApplyResources((object) this.btnAppPath, "btnAppPath");
    this.btnAppPath.Name = "btnAppPath";
    this.btnAppPath.UseVisualStyleBackColor = true;
    this.btnAppPath.Click += new EventHandler(this.btnAppPath_Click);
    componentResourceManager.ApplyResources((object) this.tbxAppPath, "tbxAppPath");
    this.tbxAppPath.Name = "tbxAppPath";
    this.tbxAppPath.TextChanged += new EventHandler(this.tbxAppPath_TextChanged);
    this.tbxAppPath.Leave += new EventHandler(this.tbxAppPath_Leave);
    componentResourceManager.ApplyResources((object) this.lblDraftExt, "lblDraftExt");
    this.lblDraftExt.Name = "lblDraftExt";
    componentResourceManager.ApplyResources((object) this.lblPrototype, "lblPrototype");
    this.lblPrototype.Name = "lblPrototype";
    componentResourceManager.ApplyResources((object) this.lblParams, "lblParams");
    this.lblParams.Name = "lblParams";
    componentResourceManager.ApplyResources((object) this.lblAppPath, "lblAppPath");
    this.lblAppPath.Name = "lblAppPath";
    this.tsTemplMoveLast.Name = "tsTemplMoveLast";
    componentResourceManager.ApplyResources((object) this.tsTemplMoveLast, "tsTemplMoveLast");
    this.pnlTop.Controls.Add((Control) this.grbTop);
    componentResourceManager.ApplyResources((object) this.pnlTop, "pnlTop");
    this.pnlTop.Name = "pnlTop";
    this.grbTop.Controls.Add((Control) this.lblCaption);
    componentResourceManager.ApplyResources((object) this.grbTop, "grbTop");
    this.grbTop.Name = "grbTop";
    this.grbTop.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lblCaption, "lblCaption");
    this.lblCaption.Name = "lblCaption";
    this.cmTemplates.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsTemplMoveFirst,
      (ToolStripItem) this.tsTemplMoveUp,
      (ToolStripItem) this.tsTemplMoveDown,
      (ToolStripItem) this.tsTemplMoveLast
    });
    this.cmTemplates.Name = "cmTemplates";
    componentResourceManager.ApplyResources((object) this.cmTemplates, "cmTemplates");
    this.tsTemplMoveFirst.Name = "tsTemplMoveFirst";
    componentResourceManager.ApplyResources((object) this.tsTemplMoveFirst, "tsTemplMoveFirst");
    this.tsTemplMoveUp.Name = "tsTemplMoveUp";
    componentResourceManager.ApplyResources((object) this.tsTemplMoveUp, "tsTemplMoveUp");
    this.tsTemplMoveDown.Name = "tsTemplMoveDown";
    componentResourceManager.ApplyResources((object) this.tsTemplMoveDown, "tsTemplMoveDown");
    componentResourceManager.ApplyResources((object) this.ofdlgAcad, "ofdlgAcad");
    this.ofdlgAcad.RestoreDirectory = true;
    this.errorProvider.ContainerControl = (ContainerControl) this;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlClient);
    this.Controls.Add((Control) this.pnlTop);
    this.Name = "DraftCadmechParamsEditor";
    this.Tag = (object) " ";
    this.pnlClient.ResumeLayout(false);
    this.pnlClient.PerformLayout();
    this.pnlTop.ResumeLayout(false);
    this.grbTop.ResumeLayout(false);
    this.grbTop.PerformLayout();
    this.cmTemplates.ResumeLayout(false);
    ((ISupportInitialize) this.errorProvider).EndInit();
    this.ResumeLayout(false);
  }
}
