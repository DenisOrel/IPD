
// Type: Intermech.Client.Core.Configurator.AddSubscriberForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Mask;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.Configurator;

public class AddSubscriberForm : Form
{
  /// <summary>тип  объекта</summary>
  public int objType = -1;
  /// <summary>ид абонента</summary>
  public long subscriberID;
  /// <summary>тип  объекта</summary>
  public int subscriberTypeID = -1;
  /// <summary>
  /// 
  /// </summary>
  public string subscriberName = string.Empty;
  /// <summary>количество высылаемых копий</summary>
  private int amount = 1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private Label label2;
  private ButtonEdit teSubscriber;
  private NumericUpDown numericUpDown1;
  private Button btnOK;
  private Button btnCancel;

  public int Amount => this.amount;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objType">тип документа, для котрого добавляем абонента</param>
  public AddSubscriberForm(int objType)
  {
    this.InitializeComponent();
    this.objType = objType;
    this.Icon = Statics.IconSrv.GetIcon(4, objType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objType">тип документа, для котрого добавляем абонента</param>
  /// <param name="subscriberName"></param>
  /// <param name="amount"></param>
  public AddSubscriberForm(int objType, string subscriberName, int amount)
  {
    this.InitializeComponent();
    this.objType = objType;
    this.Icon = Statics.IconSrv.GetIcon(4, objType);
    this.teSubscriber.Text = subscriberName;
    this.numericUpDown1.Value = (Decimal) amount;
    this.teSubscriber.Enabled = false;
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.teSubscriber.Text != string.Empty)
    {
      this.amount = (int) this.numericUpDown1.Value;
      this.DialogResult = DialogResult.OK;
    }
    else
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1257"), LocalizationHolder.rm.GetString("Client.Core_1258"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  /// <summary>выбор абонента</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void teSubscriber_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")));
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545")));
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cadd9235-306c-11d8-b4e9-00304f19f545")));
    int objectTypeId = MetaDataHelper.GetObjectTypeID(PortalConsts.objtypeSites);
    if (objectTypeId != -1)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objectTypeId));
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_1259"), descriptors);
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1257"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length != 1)
      return;
    IDBTypedObjectID dbTypedObjectId = objArray[0] as IDBTypedObjectID;
    this.subscriberTypeID = dbTypedObjectId.ObjectType;
    this.subscriberID = dbTypedObjectId.ObjectID;
    this.subscriberName = string.IsNullOrEmpty(dbTypedObjectId.Caption) ? $"{MetaDataHelper.GetObjectName(this.subscriberTypeID)} c ID=\"{dbTypedObjectId.ObjectID}\"" : dbTypedObjectId.Caption;
    this.teSubscriber.Text = this.subscriberName;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddSubscriberForm));
    this.label1 = new Label();
    this.label2 = new Label();
    this.teSubscriber = new ButtonEdit();
    this.numericUpDown1 = new NumericUpDown();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.teSubscriber.Properties.BeginInit();
    this.numericUpDown1.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.teSubscriber, "teSubscriber");
    this.teSubscriber.Name = "teSubscriber";
    this.teSubscriber.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.teSubscriber.Properties.MaskData.BeepOnError = (bool) componentResourceManager.GetObject("teSubscriber.Properties.MaskData.BeepOnError");
    this.teSubscriber.Properties.MaskData.Blank = componentResourceManager.GetString("teSubscriber.Properties.MaskData.Blank");
    this.teSubscriber.Properties.MaskData.EditMask = componentResourceManager.GetString("teSubscriber.Properties.MaskData.EditMask");
    this.teSubscriber.Properties.MaskData.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("teSubscriber.Properties.MaskData.IgnoreMaskBlank");
    this.teSubscriber.Properties.MaskData.MaskType = (MaskType) componentResourceManager.GetObject("teSubscriber.Properties.MaskData.MaskType");
    this.teSubscriber.Properties.MaskData.SaveLiteral = (bool) componentResourceManager.GetObject("teSubscriber.Properties.MaskData.SaveLiteral");
    this.teSubscriber.Properties.ReadOnly = true;
    this.teSubscriber.ButtonClick += new ButtonPressedEventHandler(this.teSubscriber_ButtonClick);
    componentResourceManager.ApplyResources((object) this.numericUpDown1, "numericUpDown1");
    this.numericUpDown1.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDown1.Name = "numericUpDown1";
    this.numericUpDown1.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.numericUpDown1);
    this.Controls.Add((Control) this.teSubscriber);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AddSubscriberForm);
    this.ShowInTaskbar = false;
    this.teSubscriber.Properties.EndInit();
    this.numericUpDown1.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
