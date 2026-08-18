// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ReferenceToGraphicsEditorDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог редактирования ссылок на источник текста</summary>
public class ReferenceToGraphicsEditorDlg : Form
{
  private Button btnCancel;
  private Button btnOK;
  private Label label1;
  private Label label2;
  private ComboBox cbAttributeName;
  private Label label3;
  private ComboBox cbRefType;
  private TextBox tbObjectName;
  private Button btnSelectObject;
  private Button btnSelectAttribute;
  private RadioButton dbObjectReferenceRB;
  private RadioButton emptyReferenceRB;
  private CheckBox passiveLinkCB;
  private RadioButton docNodeReferenceRB;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private bool suspendEvents;
  private ReferenceBase reference;
  private DocumentTreeNode referenceOwner;
  private List<System.Type> docRefTypeList = new List<System.Type>();
  private List<System.Type> dbObjectRefTypeList = new List<System.Type>();
  private List<List<string>> docRefSubTypeNames;
  private List<List<string>> dbObjectRefSubTypeNames;
  private bool changed;

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReferenceToGraphicsEditorDlg));
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.label1 = new Label();
    this.cbRefType = new ComboBox();
    this.cbAttributeName = new ComboBox();
    this.label2 = new Label();
    this.label3 = new Label();
    this.tbObjectName = new TextBox();
    this.btnSelectObject = new Button();
    this.btnSelectAttribute = new Button();
    this.dbObjectReferenceRB = new RadioButton();
    this.emptyReferenceRB = new RadioButton();
    this.passiveLinkCB = new CheckBox();
    this.docNodeReferenceRB = new RadioButton();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.cbRefType, "cbRefType");
    this.cbRefType.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbRefType.Items.AddRange(new object[4]
    {
      (object) componentResourceManager.GetString("cbRefType.Items"),
      (object) componentResourceManager.GetString("cbRefType.Items1"),
      (object) componentResourceManager.GetString("cbRefType.Items2"),
      (object) componentResourceManager.GetString("cbRefType.Items3")
    });
    this.cbRefType.Name = "cbRefType";
    this.cbRefType.SelectedIndexChanged += new EventHandler(this.cbRefType_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.cbAttributeName, "cbAttributeName");
    this.cbAttributeName.Name = "cbAttributeName";
    this.cbAttributeName.TextChanged += new EventHandler(this.cbAttributeName_TextChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.tbObjectName, "tbObjectName");
    this.tbObjectName.Name = "tbObjectName";
    this.tbObjectName.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.btnSelectObject, "btnSelectObject");
    this.btnSelectObject.Name = "btnSelectObject";
    this.btnSelectObject.Click += new EventHandler(this.btnSelectObject_Click);
    componentResourceManager.ApplyResources((object) this.btnSelectAttribute, "btnSelectAttribute");
    this.btnSelectAttribute.Name = "btnSelectAttribute";
    this.btnSelectAttribute.Click += new EventHandler(this.btnSelectAttribute_Click);
    componentResourceManager.ApplyResources((object) this.dbObjectReferenceRB, "dbObjectReferenceRB");
    this.dbObjectReferenceRB.Name = "dbObjectReferenceRB";
    this.dbObjectReferenceRB.UseVisualStyleBackColor = true;
    this.dbObjectReferenceRB.CheckedChanged += new EventHandler(this.emptyReferenceRB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.emptyReferenceRB, "emptyReferenceRB");
    this.emptyReferenceRB.Checked = true;
    this.emptyReferenceRB.Name = "emptyReferenceRB";
    this.emptyReferenceRB.TabStop = true;
    this.emptyReferenceRB.UseVisualStyleBackColor = true;
    this.emptyReferenceRB.CheckedChanged += new EventHandler(this.emptyReferenceRB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.passiveLinkCB, "passiveLinkCB");
    this.passiveLinkCB.Name = "passiveLinkCB";
    this.passiveLinkCB.UseVisualStyleBackColor = true;
    this.passiveLinkCB.CheckedChanged += new EventHandler(this.passiveLinkCB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.docNodeReferenceRB, "docNodeReferenceRB");
    this.docNodeReferenceRB.Name = "docNodeReferenceRB";
    this.docNodeReferenceRB.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.docNodeReferenceRB);
    this.Controls.Add((Control) this.passiveLinkCB);
    this.Controls.Add((Control) this.dbObjectReferenceRB);
    this.Controls.Add((Control) this.emptyReferenceRB);
    this.Controls.Add((Control) this.btnSelectAttribute);
    this.Controls.Add((Control) this.tbObjectName);
    this.Controls.Add((Control) this.btnSelectObject);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.cbAttributeName);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.cbRefType);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReferenceToGraphicsEditorDlg);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Конструктор</summary>
  public ReferenceToGraphicsEditorDlg() => this.InitializeComponent();

  /// <summary>Редактировать ссылку</summary>
  /// <param name="referenceOwner">Владелец ссылки</param>
  /// <param name="refToEdit">Ссылка</param>
  /// <returns>Отредактированная ссылка</returns>
  public ReferenceBase EditReference(DocumentTreeNode referenceOwner, ReferenceBase refToEdit)
  {
    try
    {
      this.suspendEvents = true;
      this.referenceOwner = referenceOwner;
      if (refToEdit != null)
      {
        this.reference = refToEdit.Clone();
        this.reference.AssignOwnerNode(referenceOwner);
        this.reference.UpdateLink(true, false, false);
        if (refToEdit is ReferenceToDBObjectBase)
        {
          this.dbObjectReferenceRB.Checked = true;
          this.passiveLinkCB.Checked = ((ReferenceToDBObjectBase) refToEdit).PassiveLink;
        }
        else if (refToEdit is ReferenceToNodeAttribute)
          this.docNodeReferenceRB.Checked = true;
        else
          this.emptyReferenceRB.Checked = true;
      }
      else
        this.reference = (ReferenceBase) null;
      this.LoadReferenceTypes();
      this.cbRefType.SelectedIndex = this.CalcRefTypeIndex(this.reference);
      this.changed = false;
      this.UpdateEnableds();
      this.suspendEvents = false;
      if (this.ShowDialog() == DialogResult.OK)
        refToEdit = this.reference;
    }
    finally
    {
      if (refToEdit == null && this.reference != null)
        this.reference.DisconnectLink();
    }
    return refToEdit;
  }

  /// <summary>Выполнить диалог</summary>
  /// <param name="referenceOwner">Владелец ссылки</param>
  /// <param name="reference">Ссылка</param>
  /// <returns>Отредактированная ссылка</returns>
  public static ReferenceBase ExecuteDialog(
    DocumentTreeNode referenceOwner,
    ReferenceBase reference)
  {
    return new ReferenceToGraphicsEditorDlg().EditReference(referenceOwner, reference);
  }

  /// <summary>Обновить свойство Enabled контролов диалога</summary>
  protected virtual void UpdateEnableds()
  {
    if (this.reference is ReferenceToGraphicsBase reference)
    {
      this.btnOK.Enabled = this.changed;
      this.cbRefType.Enabled = !this.emptyReferenceRB.Checked;
      this.btnSelectObject.Enabled = !this.emptyReferenceRB.Checked && reference.CanCallSelectObjectDialog;
      this.cbAttributeName.Enabled = !this.emptyReferenceRB.Checked;
      this.btnSelectAttribute.Enabled = !this.emptyReferenceRB.Checked;
      this.passiveLinkCB.Enabled = true;
    }
    else
    {
      this.btnOK.Enabled = this.changed;
      this.passiveLinkCB.Enabled = false;
    }
  }

  /// <summary>Получить все классы ссылок</summary>
  protected void LoadReferenceTypes()
  {
    this.docRefTypeList.Clear();
    this.dbObjectRefTypeList.Clear();
    for (int index = 0; index < ReferenceBase.ReferenceClassList.Count; ++index)
    {
      System.Type referenceClass = ReferenceBase.ReferenceClassList[index];
      if (typeof (IEditableReferenceToObject).IsAssignableFrom(referenceClass))
      {
        if (typeof (ReferenceToGraphicsBase).IsAssignableFrom(referenceClass))
          this.dbObjectRefTypeList.Add(referenceClass);
        else
          this.docRefTypeList.Add(referenceClass);
      }
    }
    this.LoadSubTypes();
  }

  /// <summary>Перезагрузить все подтипы ссылок у классов ссылок</summary>
  protected void LoadSubTypes()
  {
    this.docRefSubTypeNames = new List<List<string>>();
    this.dbObjectRefSubTypeNames = new List<List<string>>();
    for (int index = 0; index < this.docRefTypeList.Count; ++index)
    {
      string[] referenceSubTypes = ((IEditableReferenceToObject) Activator.CreateInstance(this.docRefTypeList[index])).GetReferenceSubTypes(this.referenceOwner, typeof (IEditableReferenceToObject));
      this.docRefSubTypeNames.Add(new List<string>());
      if (referenceSubTypes != null && referenceSubTypes.Length != 0)
        this.docRefSubTypeNames[index].AddRange((IEnumerable<string>) referenceSubTypes);
    }
    for (int index = 0; index < this.dbObjectRefTypeList.Count; ++index)
    {
      string[] referenceSubTypes = ((IEditableReferenceToObject) Activator.CreateInstance(this.dbObjectRefTypeList[index])).GetReferenceSubTypes(this.referenceOwner, typeof (IEditableReferenceToObject));
      this.dbObjectRefSubTypeNames.Add(new List<string>());
      if (referenceSubTypes != null && referenceSubTypes.Length != 0)
        this.dbObjectRefSubTypeNames[index].AddRange((IEnumerable<string>) referenceSubTypes);
    }
    this.UpdateSubTypesComboBox();
  }

  /// <summary>Перезагрузить все подтипы ссылок у классов ссылок</summary>
  protected void UpdateSubTypesComboBox()
  {
    ReferenceBase reference = this.reference;
    this.cbRefType.Items.Clear();
    int num;
    if (this.emptyReferenceRB.Checked)
    {
      this.cbRefType.Items.Add((object) LocalizationHolder.rm.GetString("Document.Model_98"));
      num = -1;
    }
    else if (this.docNodeReferenceRB.Checked)
    {
      for (int index = 0; index < this.docRefSubTypeNames.Count; ++index)
      {
        if (this.docRefSubTypeNames[index].Count > 0)
          this.cbRefType.Items.AddRange((object[]) this.docRefSubTypeNames[index].ToArray());
      }
      num = reference == null || !(reference is ReferenceToNodeAttributeBase) ? -1 : this.CalcRefTypeIndex(reference);
    }
    else
    {
      for (int index = 0; index < this.dbObjectRefSubTypeNames.Count; ++index)
      {
        if (this.dbObjectRefSubTypeNames[index].Count > 0)
          this.cbRefType.Items.AddRange((object[]) this.dbObjectRefSubTypeNames[index].ToArray());
      }
      num = reference == null || !(reference is ReferenceToGraphicsBase) ? -1 : this.CalcRefTypeIndex(reference);
    }
    if (num != -1 && num < this.cbRefType.Items.Count)
      this.cbRefType.SelectedIndex = num;
    else if (this.cbRefType.Items.Count != 0)
      this.cbRefType.SelectedIndex = 0;
    else
      this.cbRefType.SelectedIndex = -1;
  }

  /// <summary>Вычислить индекс класса ссылки</summary>
  /// <param name="reference">Ссылка</param>
  /// <returns>Индекс класса ссылки</returns>
  protected int CalcRefTypeIndex(ReferenceBase reference)
  {
    int num1 = 0;
    if (reference is IEditableReferenceToObject referenceToObject)
    {
      int num2 = !(referenceToObject is ReferenceToGraphicsBase) ? this.docRefTypeList.IndexOf(referenceToObject.GetType()) : this.dbObjectRefTypeList.IndexOf(referenceToObject.GetType());
      if (num2 != -1)
      {
        int num3 = 0;
        if (referenceToObject is ReferenceToGraphicsBase)
        {
          for (int index = 0; index < num2; ++index)
          {
            if (this.dbObjectRefSubTypeNames[index] != null)
              num3 += this.dbObjectRefSubTypeNames[index].Count;
          }
        }
        else
        {
          for (int index = 0; index < num2; ++index)
          {
            if (this.docRefSubTypeNames[index] != null)
              num3 += this.docRefSubTypeNames[index].Count;
          }
        }
        int referenceSubTypeIndex = referenceToObject.GetReferenceSubTypeIndex(typeof (IEditableReferenceToObject));
        if (referenceSubTypeIndex != -1)
          num1 = num3 + referenceSubTypeIndex;
      }
    }
    return num1;
  }

  private void cbRefType_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.changed = true;
    int selectedIndex = this.cbRefType.SelectedIndex;
    if (selectedIndex >= 0 && !this.emptyReferenceRB.Checked)
    {
      int num = 0;
      int index1 = -1;
      System.Type type = (System.Type) null;
      if (this.docNodeReferenceRB.Checked)
      {
        for (int index2 = 0; index2 < this.docRefSubTypeNames.Count; ++index2)
        {
          if (this.docRefSubTypeNames[index2] != null)
            num += this.docRefSubTypeNames[index2].Count;
          if (selectedIndex - 1 < num)
          {
            index1 = index2;
            break;
          }
        }
        if (index1 != -1 && index1 < this.docRefTypeList.Count)
          type = this.docRefTypeList[index1];
      }
      else if (this.dbObjectReferenceRB.Checked)
      {
        for (int index3 = 0; index3 < this.dbObjectRefSubTypeNames.Count; ++index3)
        {
          if (this.dbObjectRefSubTypeNames[index3] != null)
            num += this.dbObjectRefSubTypeNames[index3].Count;
          if (selectedIndex < num)
          {
            index1 = index3;
            break;
          }
        }
        if (index1 != -1 && index1 < this.dbObjectRefTypeList.Count)
          type = this.dbObjectRefTypeList[index1];
      }
      string text = this.cbAttributeName.Text;
      if (type != (System.Type) null)
      {
        if (this.reference == null || this.reference.GetType() != type)
        {
          if (this.reference != null)
          {
            this.reference.DisconnectLink();
            this.reference.AssignOwnerNode((DocumentTreeNode) null);
          }
          this.reference = (ReferenceBase) Activator.CreateInstance(type);
          this.reference.AssignOwnerNode(this.referenceOwner);
          if (this.reference is ReferenceToGraphicsBase)
            ((ReferenceToDBObjectBase) this.reference).PassiveLink = false;
        }
        ((IEditableReferenceToObject) this.reference).SetReferenceSubType(this.referenceOwner, this.cbRefType.Text, typeof (IEditableReferenceToObject));
        this.reference.UpdateLink(true, false, false);
      }
      ReferenceToGraphicsBase reference = this.reference as ReferenceToGraphicsBase;
      this.tbObjectName.Text = reference?.ObjectCaption ?? "";
      this.btnSelectObject.Enabled = reference != null && reference.CanCallSelectObjectDialog;
      this.passiveLinkCB.Checked = reference != null && reference.PassiveLink;
      this.cbAttributeName.Items.Clear();
      this.cbAttributeName.Text = "";
      string[] attributeNameList = reference?.GetAttributeNameList();
      if (attributeNameList != null && attributeNameList.Length != 0)
      {
        this.cbAttributeName.DropDownStyle = ComboBoxStyle.DropDown;
        this.cbAttributeName.Items.AddRange((object[]) attributeNameList);
      }
      else
        this.cbAttributeName.DropDownStyle = ComboBoxStyle.Simple;
      if (!string.IsNullOrEmpty(text))
        this.cbAttributeName.Text = text;
      else
        this.cbAttributeName.Text = reference?.AttributeName ?? "";
      this.cbAttributeName.SelectionLength = 0;
      this.cbAttributeName.Enabled = true;
      this.btnSelectAttribute.Enabled = true;
    }
    else
    {
      if (this.reference != null)
      {
        this.reference.DisconnectLink();
        this.reference.AssignOwnerNode((DocumentTreeNode) null);
      }
      this.reference = (ReferenceBase) null;
      this.tbObjectName.Text = "";
      this.btnSelectObject.Enabled = false;
      this.cbAttributeName.Text = "";
      this.cbAttributeName.DropDownStyle = ComboBoxStyle.Simple;
      this.cbAttributeName.Enabled = false;
      this.btnSelectAttribute.Enabled = false;
    }
    this.UpdateEnableds();
  }

  private void btnSelectObject_Click(object sender, EventArgs e)
  {
    if (!(this.reference is ReferenceToGraphicsBase reference))
      return;
    this.changed = true;
    if (reference.CanCallSelectObjectDialog)
      reference.CallSelectObjectDialog();
    this.tbObjectName.Text = reference.ObjectCaption;
    string text = this.cbAttributeName.Text;
    this.cbAttributeName.Items.Clear();
    string[] attributeNameList = reference.GetAttributeNameList();
    if (attributeNameList != null && attributeNameList.Length != 0)
    {
      this.cbAttributeName.DropDownStyle = ComboBoxStyle.DropDown;
      this.cbAttributeName.Items.AddRange((object[]) attributeNameList);
    }
    else
      this.cbAttributeName.DropDownStyle = ComboBoxStyle.Simple;
    this.cbAttributeName.Enabled = true;
    this.cbAttributeName.Text = text;
    this.btnSelectAttribute.Enabled = true;
    this.UpdateEnableds();
  }

  private void btnSelectAttribute_Click(object sender, EventArgs e)
  {
    if (!(this.reference is ReferenceToGraphicsBase reference))
      return;
    this.changed = true;
    reference.CallSelectAttributeDialog();
    this.cbAttributeName.Text = reference.AttributeName;
    this.cbAttributeName.SelectionLength = 0;
    this.UpdateEnableds();
  }

  private void cbAttributeName_TextChanged(object sender, EventArgs e)
  {
    if (!(this.reference is ReferenceToGraphicsBase reference))
      return;
    this.changed = true;
    if (reference.AttributeName != this.cbAttributeName.Text)
    {
      reference.ImageCache = (Image) null;
      reference.AssignAttributeInfo(Guid.Empty, this.cbAttributeName.Text, (string) null, (List<string>) null);
      this.reference.UpdateLink(true, false, false);
      reference.UpdateAttributeInfo();
    }
    this.cbAttributeName.SelectionLength = 0;
    this.UpdateEnableds();
  }

  private void emptyReferenceRB_CheckedChanged(object sender, EventArgs e)
  {
    if (this.suspendEvents)
      return;
    this.changed = true;
    this.UpdateSubTypesComboBox();
    this.UpdateEnableds();
  }

  private void passiveLinkCB_CheckedChanged(object sender, EventArgs e)
  {
    this.changed = true;
    if (this.reference is ReferenceToGraphicsBase reference)
      reference.PassiveLink = this.passiveLinkCB.Checked;
    this.UpdateEnableds();
  }
}
