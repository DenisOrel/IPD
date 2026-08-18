
// Type: Intermech.Client.Core.FormDesigner.External.Navigator.ExternalEditorParamsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using Intermech.Client.Core.FormDesigner.External.Classes;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Client.Core.FormDesigner.External.Navigator;

/// <summary>
/// 
/// </summary>
[ViewDescriptionProvider(typeof (ExternalEditorParamsView.ExternalEditorParamsViewDescriptionProvider))]
public class ExternalEditorParamsView : UserControl, IView
{
  private long _objectID;
  private bool _firstRun = true;
  private bool _modified;
  private bool _loading;
  private ExternalEditorParams _exParams;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private Button _btnApply;
  private Button _btnCancel;
  private Button _btnView;
  private Button _btnTest;
  private Label _lbView;
  private TextBox _txtView;
  private Label _lbAdditionalParams;
  private TextBox _txtAdditionalParams;
  private ComboBox comboBox1;
  private Label label3;
  private Label label4;
  private ComboBox comboBox2;
  private Label _lbFileName;
  private CheckBox _chbCantModified;
  private TextBox _txtCreate;
  private ToolTipController toolTipController1;
  private Button _btnCreate;
  private CheckBox checkBox2;

  /// <summary>Конструктор.</summary>
  public ExternalEditorParamsView()
  {
    this.InitializeComponent();
    this.PopulateComboBox();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_exParams_OnModified(object sender, EventArgs e) => this.Modified = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtView_TextChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    this._exParams.Path = this._txtView.Text;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtAdditionalParams_TextChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    this._exParams.Command = this._txtAdditionalParams.Text;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OncomboBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    this._exParams.Send = (SendMethod) EnumTypeHelper.GetEnumValue(typeof (SendMethod), Convert.ToString(this.comboBox1.SelectedItem));
    this._btnCreate.Enabled = this._txtCreate.Enabled = this._exParams.Send == SendMethod.File || this._exParams.Receive == ReceiveMethod.File;
    this.checkBox2.Enabled = this._exParams.Send == SendMethod.File;
    if (this.checkBox2.Enabled)
      return;
    this.checkBox2.Checked = this._exParams.SendAllAttributes = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OncomboBox2_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    this._exParams.Receive = (ReceiveMethod) EnumTypeHelper.GetEnumValue(typeof (ReceiveMethod), Convert.ToString(this.comboBox2.SelectedItem));
    this._btnCreate.Enabled = this._txtCreate.Enabled = this._exParams.Send == SendMethod.File || this._exParams.Receive == ReceiveMethod.File;
    this.checkBox2.Enabled = this._exParams.Send == SendMethod.File;
    if (this.checkBox2.Enabled)
      return;
    this.checkBox2.Checked = this._exParams.SendAllAttributes = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OntextBox3_TextChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    this._exParams.SwapFile = this._txtCreate.Text;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_chbCantModified_CheckedChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    this._exParams.LockControl = this._chbCantModified.Checked;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox2_CheckedChanged(object sender, EventArgs e)
  {
    if (this._loading)
      return;
    this._exParams.SendAllAttributes = this.checkBox2.Checked;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnApply_Click(object sender, EventArgs e)
  {
    if ((this._exParams.Send == SendMethod.File || this._exParams.Receive == ReceiveMethod.File) && this._exParams.SwapFile.Length == 0)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_180"), LocalizationHolder.rm.GetString("Client.Core_181"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      this._txtCreate.Focus();
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          XmlDocument doc = new XmlDocument();
          this._exParams.Save(doc);
          doc.Save((Stream) memoryStream);
          BlobProcWriter blobProcWriter = new BlobProcWriter(sessionKeeper.Session.GetObject(this._objectID).GetAttributeByGuid(ExternalEditorConsts.ExternalEditorParamsAttributeType), 0, new BlobInformation(memoryStream.Length, 0L, DateTime.Now, "ExternalEditorParams", ArcMethods.ZLibPacked, string.Empty), (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          blobProcWriter.WriteData();
          this.Modified = !blobProcWriter.Result;
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnCancel_Click(object sender, EventArgs e)
  {
    this._firstRun = true;
    this.Activate((IView) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnView_Click(object sender, EventArgs e)
  {
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.Title = LocalizationHolder.rm.GetString("Client.Core_182");
      openFileDialog.Multiselect = false;
      openFileDialog.CheckFileExists = true;
      openFileDialog.CheckPathExists = true;
      openFileDialog.Filter = LocalizationHolder.rm.GetString("Client.Core_183");
      openFileDialog.FilterIndex = 0;
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this._txtView.Text = openFileDialog.FileName;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnTest_Click(object sender, EventArgs e)
  {
    ServicesManager.GetService(typeof (IClipboard));
    DataObject data = new DataObject();
    string str = LocalizationHolder.rm.GetString("Client.Core_184");
    string name = this._exParams.Command;
    switch (this._exParams.Send)
    {
      case SendMethod.CommandString:
        name = name + (name.Length > 0 ? " " : string.Empty) + $"\"{str}\"";
        break;
      case SendMethod.File:
        using (StreamWriter streamWriter = new StreamWriter((Stream) new FileStream(Environment.ExpandEnvironmentVariables(this._exParams.SwapFile), FileMode.Create)))
        {
          streamWriter.Write(str);
          break;
        }
      case SendMethod.Clipboard:
        data.SetText(str, TextDataFormat.UnicodeText);
        Clipboard.SetDataObject((object) data);
        break;
    }
    Process.Start(new ProcessStartInfo(Environment.ExpandEnvironmentVariables(this._exParams.Path), Environment.ExpandEnvironmentVariables(name))
    {
      UseShellExecute = false
    }).WaitForExit();
    switch (this._exParams.Receive)
    {
      case ReceiveMethod.NotReturn:
        int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_185"), LocalizationHolder.rm.GetString("Client.Core_186"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        return;
      case ReceiveMethod.File:
        using (StreamReader streamReader = new StreamReader((Stream) new FileStream(Environment.ExpandEnvironmentVariables(this._exParams.SwapFile), FileMode.OpenOrCreate)))
        {
          str = streamReader.ReadToEnd();
          break;
        }
      case ReceiveMethod.Clipboard:
        str = Clipboard.GetText();
        break;
    }
    File.Delete(Environment.ExpandEnvironmentVariables(this._exParams.SwapFile));
    int num2 = (int) MessageBox.Show(str, LocalizationHolder.rm.GetString("Client.Core_187"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnCreate_Click(object sender, EventArgs e)
  {
    this._txtCreate.Text = $"%temp%\\{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.tmp";
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objectID = (items.GetItemData(0, typeof (IDBObjectID)) as IDBObjectID).Value;
    this._firstRun = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    if (!this._firstRun)
      return;
    this._exParams = new ExternalEditorParams();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(this._objectID).GetAttributeByGuid(ExternalEditorConsts.ExternalEditorParamsAttributeType);
      if (attributeByGuid != null)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          BlobProcReader blobProcReader = new BlobProcReader(attributeByGuid, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
          blobProcReader.ReadData();
          if (blobProcReader.Result)
          {
            if (memoryStream.Length > 0L)
            {
              memoryStream.Position = 0L;
              XmlDocument xmlDocument = new XmlDocument();
              xmlDocument.Load((Stream) memoryStream);
              this._exParams.Load((XmlNode) xmlDocument.DocumentElement);
            }
          }
        }
      }
    }
    this.PopulateParams();
    this._exParams.OnModified += new EventHandler(this.On_exParams_OnModified);
    this.Modified = this._firstRun = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
    if (!this.Modified)
      return;
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_178"), LocalizationHolder.rm.GetString("Client.Core_135"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      this._btnApply.PerformClick();
    else
      this._btnCancel.PerformClick();
  }

  /// <summary>
  /// 
  /// </summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_179");

  /// <summary>
  /// 
  /// </summary>
  public int ImageIndex => -1;

  /// <summary>
  /// 
  /// </summary>
  public int OrderID => 7;

  /// <summary>
  /// 
  /// </summary>
  private bool Modified
  {
    get => this._modified;
    set
    {
      this._modified = value;
      this._btnApply.Enabled = this._btnCancel.Enabled = value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void PopulateComboBox()
  {
    this.comboBox1.Items.Clear();
    string empty = string.Empty;
    foreach (FieldInfo field in typeof (SendMethod).GetFields())
    {
      string caption = EnumTypeHelper.GetCaption((Enum) (SendMethod) field.GetValue((object) SendMethod.File));
      if (!this.comboBox1.Items.Contains((object) caption))
        this.comboBox1.Items.Add((object) caption);
    }
    this.comboBox2.Items.Clear();
    foreach (FieldInfo field in typeof (ReceiveMethod).GetFields())
    {
      string caption = EnumTypeHelper.GetCaption((Enum) (ReceiveMethod) field.GetValue((object) ReceiveMethod.File));
      if (!this.comboBox2.Items.Contains((object) caption))
        this.comboBox2.Items.Add((object) caption);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void PopulateParams()
  {
    this._loading = true;
    this._txtView.Text = this._exParams.Path;
    this._txtAdditionalParams.Text = this._exParams.Command;
    this._txtCreate.Text = this._exParams.SwapFile;
    this.comboBox1.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) this._exParams.Send);
    this.comboBox2.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) this._exParams.Receive);
    this._chbCantModified.Checked = this._exParams.LockControl;
    this.checkBox2.Checked = this._exParams.SendAllAttributes;
    this._btnCreate.Enabled = this._txtCreate.Enabled = this._exParams.Send == SendMethod.File || this._exParams.Receive == ReceiveMethod.File;
    this.checkBox2.Enabled = this._exParams.Send == SendMethod.File;
    this._loading = false;
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExternalEditorParamsView));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this._lbView = new Label();
    this._txtView = new TextBox();
    this._lbAdditionalParams = new Label();
    this._txtAdditionalParams = new TextBox();
    this._txtCreate = new TextBox();
    this._btnCancel = new Button();
    this._btnApply = new Button();
    this._btnView = new Button();
    this.label3 = new Label();
    this.label4 = new Label();
    this.comboBox1 = new ComboBox();
    this._lbFileName = new Label();
    this.comboBox2 = new ComboBox();
    this._chbCantModified = new CheckBox();
    this._btnTest = new Button();
    this._btnCreate = new Button();
    this.checkBox2 = new CheckBox();
    this.toolTipController1 = new ToolTipController(this.components);
    this.tableLayoutPanel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this._lbView, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._txtView, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this._lbAdditionalParams, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this._txtAdditionalParams, 0, 3);
    this.tableLayoutPanel1.Controls.Add((Control) this._txtCreate, 0, 7);
    this.tableLayoutPanel1.Controls.Add((Control) this._btnCancel, 4, 11);
    this.tableLayoutPanel1.Controls.Add((Control) this._btnApply, 3, 11);
    this.tableLayoutPanel1.Controls.Add((Control) this._btnView, 4, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.label3, 0, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.label4, 2, 4);
    this.tableLayoutPanel1.Controls.Add((Control) this.comboBox1, 0, 5);
    this.tableLayoutPanel1.Controls.Add((Control) this._lbFileName, 0, 6);
    this.tableLayoutPanel1.Controls.Add((Control) this.comboBox2, 2, 5);
    this.tableLayoutPanel1.Controls.Add((Control) this._chbCantModified, 0, 8);
    this.tableLayoutPanel1.Controls.Add((Control) this._btnTest, 4, 8);
    this.tableLayoutPanel1.Controls.Add((Control) this._btnCreate, 4, 7);
    this.tableLayoutPanel1.Controls.Add((Control) this.checkBox2, 0, 9);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    componentResourceManager.ApplyResources((object) this._lbView, "_lbView");
    this.tableLayoutPanel1.SetColumnSpan((Control) this._lbView, 5);
    this._lbView.Name = "_lbView";
    this.tableLayoutPanel1.SetColumnSpan((Control) this._txtView, 4);
    componentResourceManager.ApplyResources((object) this._txtView, "_txtView");
    this._txtView.Name = "_txtView";
    this._txtView.TextChanged += new EventHandler(this.On_txtView_TextChanged);
    componentResourceManager.ApplyResources((object) this._lbAdditionalParams, "_lbAdditionalParams");
    this.tableLayoutPanel1.SetColumnSpan((Control) this._lbAdditionalParams, 5);
    this._lbAdditionalParams.Name = "_lbAdditionalParams";
    this.tableLayoutPanel1.SetColumnSpan((Control) this._txtAdditionalParams, 5);
    componentResourceManager.ApplyResources((object) this._txtAdditionalParams, "_txtAdditionalParams");
    this._txtAdditionalParams.Name = "_txtAdditionalParams";
    this._txtAdditionalParams.TextChanged += new EventHandler(this.On_txtAdditionalParams_TextChanged);
    this.tableLayoutPanel1.SetColumnSpan((Control) this._txtCreate, 4);
    componentResourceManager.ApplyResources((object) this._txtCreate, "_txtCreate");
    this._txtCreate.Name = "_txtCreate";
    this._txtCreate.TextChanged += new EventHandler(this.OntextBox3_TextChanged);
    componentResourceManager.ApplyResources((object) this._btnCancel, "_btnCancel");
    this._btnCancel.Name = "_btnCancel";
    this._btnCancel.UseVisualStyleBackColor = true;
    this._btnCancel.Click += new EventHandler(this.On_btnCancel_Click);
    componentResourceManager.ApplyResources((object) this._btnApply, "_btnApply");
    this._btnApply.Name = "_btnApply";
    this._btnApply.UseVisualStyleBackColor = true;
    this._btnApply.Click += new EventHandler(this.On_btnApply_Click);
    componentResourceManager.ApplyResources((object) this._btnView, "_btnView");
    this._btnView.Name = "_btnView";
    this.toolTipController1.SetToolTip((Control) this._btnView, "Выбрать исполняемый файл");
    this._btnView.UseVisualStyleBackColor = true;
    this._btnView.Click += new EventHandler(this.On_btnView_Click);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.label3, 2);
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.label4, 3);
    this.label4.Name = "label4";
    this.tableLayoutPanel1.SetColumnSpan((Control) this.comboBox1, 2);
    componentResourceManager.ApplyResources((object) this.comboBox1, "comboBox1");
    this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBox1.FormattingEnabled = true;
    this.comboBox1.Name = "comboBox1";
    this.comboBox1.SelectedIndexChanged += new EventHandler(this.OncomboBox1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._lbFileName, "_lbFileName");
    this.tableLayoutPanel1.SetColumnSpan((Control) this._lbFileName, 5);
    this._lbFileName.Name = "_lbFileName";
    this.tableLayoutPanel1.SetColumnSpan((Control) this.comboBox2, 3);
    componentResourceManager.ApplyResources((object) this.comboBox2, "comboBox2");
    this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBox2.FormattingEnabled = true;
    this.comboBox2.Name = "comboBox2";
    this.comboBox2.SelectedIndexChanged += new EventHandler(this.OncomboBox2_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this._chbCantModified, "_chbCantModified");
    this.tableLayoutPanel1.SetColumnSpan((Control) this._chbCantModified, 4);
    this._chbCantModified.Name = "_chbCantModified";
    this._chbCantModified.UseVisualStyleBackColor = true;
    this._chbCantModified.CheckedChanged += new EventHandler(this.On_chbCantModified_CheckedChanged);
    componentResourceManager.ApplyResources((object) this._btnTest, "_btnTest");
    this._btnTest.Name = "_btnTest";
    this.toolTipController1.SetToolTip((Control) this._btnTest, "Запустить тестирование внешнего редактора");
    this._btnTest.UseVisualStyleBackColor = true;
    this._btnTest.Click += new EventHandler(this.On_btnTest_Click);
    componentResourceManager.ApplyResources((object) this._btnCreate, "_btnCreate");
    this._btnCreate.Name = "_btnCreate";
    this.toolTipController1.SetToolTip((Control) this._btnCreate, "Создать временный файл");
    this._btnCreate.UseVisualStyleBackColor = true;
    this._btnCreate.Click += new EventHandler(this.On_btnCreate_Click);
    componentResourceManager.ApplyResources((object) this.checkBox2, "checkBox2");
    this.tableLayoutPanel1.SetColumnSpan((Control) this.checkBox2, 5);
    this.checkBox2.Name = "checkBox2";
    this.checkBox2.UseVisualStyleBackColor = true;
    this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
    this.toolTipController1.Style = new ViewStyle("ToolTip style");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.DoubleBuffered = true;
    this.Name = nameof (ExternalEditorParamsView);
    this.Tag = (object) "     ";
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.ResumeLayout(false);
  }

  private sealed class ExternalEditorParamsViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_179"),
        ImageIndex = -1,
        OrderID = 7
      };
    }
  }
}
