
// Type: Intermech.PropertyEditors.PrototypeEditObjectForm
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class PrototypeEditObjectForm : Form
{
  private int ownerObjType = -1;
  private bool isNew;
  private bool fileChanged;
  private string prototypeName = string.Empty;
  private int objType = -1;
  private string objTypeName = string.Empty;
  private BlobInformation blobInformation;
  public PrototypeClass Prototype;
  private PrototypeClass prototypeOrig;
  private List<int> objTypesToSelect;
  private bool blockOnChange;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label label1;
  private TextBox nameEdit;
  private TextBox descrEdit;
  private Label label2;
  private Label label3;
  private Label label4;
  private Button btnOk;
  private Button btnCancel;
  private ButtonEdit objTypeEdit;
  private ButtonEdit fileEdit;
  private OpenFileDialog openFileDialog;

  public PrototypeEditObjectForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1182);
  }

  /// <summary>
  /// результат помещается в this.Prototype, как при добавлении, так и при редактировании.
  /// </summary>
  /// <param name="ownerObjType">тип объекта, на который выполняем настройку</param>
  /// <param name="isNew">добавление-редактирование</param>
  /// <param name="pc">недоинициализированный при добавлении - полностью инициализированный при редактированиии</param>
  /// <returns></returns>
  public DialogResult Execute(int ownerObjType, bool isNew, PrototypeClass pc)
  {
    this.ownerObjType = ownerObjType;
    this.isNew = isNew;
    this.prototypeOrig = pc;
    this.prototypeName = pc.Caption;
    this.objType = pc.ObjtypeId;
    this.blobInformation = pc.Files[0];
    this.fileChanged = false;
    return this.ShowDialog();
  }

  private void PrototypeEditObjectForm_Load(object sender, EventArgs e)
  {
    this.ControlsVisibility();
    this.blockOnChange = true;
    try
    {
      this.nameEdit.Text = this.prototypeName;
      this.descrEdit.Text = this.blobInformation.Note;
      this.fileEdit.Text = this.blobInformation.FileName;
      this.objTypeName = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(this.objType).ObjectTypeName;
      this.objTypeEdit.Text = this.objTypeName;
    }
    finally
    {
      this.blockOnChange = false;
    }
  }

  private void ControlsVisibility()
  {
    this.objTypeEdit.Enabled = this.isNew;
    this.objTypeEdit.Properties.ReadOnly = true;
    this.fileEdit.Properties.ReadOnly = !this.isNew;
  }

  private void fileEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    if (this.openFileDialog.ShowDialog() != DialogResult.OK || !File.Exists(this.openFileDialog.FileName))
      return;
    this.fileEdit.Text = this.openFileDialog.FileName;
  }

  private void objTypeEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    this.FindTypes4Select();
    SelectorForm selectorForm = new SelectorForm(LocalizationHolder.rm.GetString("Client.Core_1122"), 4, false);
    selectorForm.SelectorFilter = (ISelectorFilter) new ObjTypeSelectorFilter(this.objTypesToSelect);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    this.objType = (int) selectorForm.IDList[0];
    this.objTypeName = (string) selectorForm.NameList[0];
    this.objTypeEdit.Text = this.objTypeName;
  }

  /// <summary>ищем список типов, предоставляемых для выбора</summary>
  private void FindTypes4Select()
  {
    if (this.objTypesToSelect != null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int objectTypeId = sessionKeeper.Session.IdentHelper.GetObjectTypeID("cad00342-306c-11d8-b4e9-00304f19f545");
      this.objTypesToSelect = MetaDataHelper.GetObjectTypeParentsIDReverse(objectTypeId);
      this.objTypesToSelect.Add(objectTypeId);
      IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(objectTypeId);
      if (objectTypeCollection == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) objectTypeCollection.SelectRecursive("").Rows)
        this.objTypesToSelect.Add(Convert.ToInt32(row["F_OBJECT_TYPE"]));
    }
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.isNew && (this.objType == -1 || this.nameEdit.Text == string.Empty) || !this.isNew && this.nameEdit.Text == string.Empty)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_926"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.OK);
      this.DialogResult = DialogResult.None;
    }
    else
    {
      if (this.fileChanged)
      {
        if (!File.Exists(this.fileEdit.Text))
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_927") + this.fileEdit.Text + LocalizationHolder.rm.GetString("Client.Core_922"), LocalizationHolder.rm.GetString("Client.Core_82"), MessageBoxButtons.OK);
          this.DialogResult = DialogResult.None;
          return;
        }
        this.blobInformation = new BlobInformation(-1L, -1L, File.GetCreationTime(this.fileEdit.Text), Path.GetFileName(this.fileEdit.Text), ArcMethods.ZLibPacked, this.descrEdit.Text);
      }
      else
        this.blobInformation.Note = this.descrEdit.Text;
      this.prototypeName = this.nameEdit.Text;
      this.objTypeName = this.objTypeEdit.Text;
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (this.isNew)
          {
            long num = -1;
            try
            {
              IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this.objType);
              if (objectCollection == null)
                return;
              IDBObject dbObject = objectCollection.Create();
              dbObject.Caption = this.prototypeName;
              IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(sessionKeeper.Session.IdentHelper.GetAttributeID("cad00149-306c-11d8-b4e9-00304f19f545"), false);
              IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.ownerObjType);
              dbAttribute.Index = 0;
              dbAttribute.Value = (object) (objectType as IDBGuid).GUID;
              dbObject.Attributes.AddAttribute(sessionKeeper.Session.IdentHelper.GetAttributeID("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
              num = Math.Abs(dbObject.ObjectID);
              using (FileStream aSourceStream = new FileStream(this.fileEdit.Text, FileMode.Open, FileAccess.Read))
              {
                dbObject.CommitCreation(true);
                new BlobProcWriter(num, AttributableElements.Object, sessionKeeper.Session.IdentHelper.GetAttributeID("cad0004b-306c-11d8-b4e9-00304f19f545"), 0, Consts.DefaultBlobBlockSize, this.blobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
              }
              this.Prototype = new PrototypeClass(num);
            }
            catch (Exception ex)
            {
              ExceptionHelper.ExceptionService.ShowException(ex);
              this.DialogResult = DialogResult.None;
              if (num == -1L)
                return;
              sessionKeeper.Session.GetObject(num, false)?.Delete(0L);
            }
          }
          else
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(this.prototypeOrig.Id);
            if (dbObject != null)
            {
              if (dbObject.Caption != this.prototypeName)
                dbObject.Caption = this.prototypeName;
              if (this.fileChanged)
              {
                FileStream aSourceStream = new FileStream(this.fileEdit.Text, FileMode.Open);
                new BlobProcWriter(this.prototypeOrig.Id, AttributableElements.Object, sessionKeeper.Session.IdentHelper.GetAttributeID("cad0004b-306c-11d8-b4e9-00304f19f545"), 0, Consts.DefaultBlobBlockSize, this.blobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
              }
              else if (this.blobInformation.Note != this.prototypeOrig.Files[0].Note)
              {
                IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
                if (attributeByGuid != null)
                  (attributeByGuid as IBlobWriter).OpenBlob(this.blobInformation, true);
              }
            }
            this.Prototype = new PrototypeClass(this.prototypeOrig.Id);
          }
        }
      }
      catch
      {
        this.DialogResult = DialogResult.None;
      }
    }
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
  }

  private void fileEdit_EditValueChanged(object sender, EventArgs e)
  {
    if (this.blockOnChange)
      return;
    this.fileChanged = true;
    this.fileEdit.Properties.ReadOnly = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrototypeEditObjectForm));
    this.label1 = new Label();
    this.nameEdit = new TextBox();
    this.descrEdit = new TextBox();
    this.label2 = new Label();
    this.label3 = new Label();
    this.label4 = new Label();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.objTypeEdit = new ButtonEdit();
    this.fileEdit = new ButtonEdit();
    this.openFileDialog = new OpenFileDialog();
    this.objTypeEdit.Properties.BeginInit();
    this.fileEdit.Properties.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.nameEdit, "nameEdit");
    this.nameEdit.Name = "nameEdit";
    componentResourceManager.ApplyResources((object) this.descrEdit, "descrEdit");
    this.descrEdit.Name = "descrEdit";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.objTypeEdit, "objTypeEdit");
    this.objTypeEdit.Name = "objTypeEdit";
    this.objTypeEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.objTypeEdit.Properties.ReadOnly = true;
    this.objTypeEdit.ButtonPressed += new ButtonPressedEventHandler(this.objTypeEdit_ButtonPressed);
    componentResourceManager.ApplyResources((object) this.fileEdit, "fileEdit");
    this.fileEdit.Name = "fileEdit";
    this.fileEdit.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.fileEdit.ButtonPressed += new ButtonPressedEventHandler(this.fileEdit_ButtonPressed);
    this.fileEdit.EditValueChanged += new EventHandler(this.fileEdit_EditValueChanged);
    this.openFileDialog.RestoreDirectory = true;
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.fileEdit);
    this.Controls.Add((Control) this.objTypeEdit);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.descrEdit);
    this.Controls.Add((Control) this.nameEdit);
    this.Controls.Add((Control) this.label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MinimizeBox = false;
    this.Name = nameof (PrototypeEditObjectForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.PrototypeEditObjectForm_Load);
    this.objTypeEdit.Properties.EndInit();
    this.fileEdit.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
