// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ReferenceToObjectEditorDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог редактирования ссылок на объект</summary>
public class ReferenceToObjectEditorDlg : Form
{
  private TextBox tbObjectName;
  private Button btnSelectObject;
  private Label label3;
  private ComboBox cbRefType;
  private Label label1;
  private Button btnCancel;
  private Button btnOK;
  private RadioButton dbObjectReferenceRB;
  private RadioButton emptyReferenceRB;
  private RadioButton docNodeReferenceRB;
  private CheckBox passiveLinkCB;
  private ComboBox cbLinkAttributeName;
  private Label label5;
  private Button btnSelectLinkAttribute;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReferenceToObjectEditorDlg));
    this.tbObjectName = new TextBox();
    this.btnSelectObject = new Button();
    this.label3 = new Label();
    this.cbRefType = new ComboBox();
    this.label1 = new Label();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.dbObjectReferenceRB = new RadioButton();
    this.emptyReferenceRB = new RadioButton();
    this.docNodeReferenceRB = new RadioButton();
    this.passiveLinkCB = new CheckBox();
    this.cbLinkAttributeName = new ComboBox();
    this.label5 = new Label();
    this.btnSelectLinkAttribute = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tbObjectName, "tbObjectName");
    this.tbObjectName.Name = "tbObjectName";
    this.tbObjectName.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.btnSelectObject, "btnSelectObject");
    this.btnSelectObject.Name = "btnSelectObject";
    this.btnSelectObject.Click += new EventHandler(this.btnSelectObject_Click);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
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
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
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
    componentResourceManager.ApplyResources((object) this.docNodeReferenceRB, "docNodeReferenceRB");
    this.docNodeReferenceRB.Name = "docNodeReferenceRB";
    this.docNodeReferenceRB.UseVisualStyleBackColor = true;
    this.docNodeReferenceRB.CheckedChanged += new EventHandler(this.emptyReferenceRB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.passiveLinkCB, "passiveLinkCB");
    this.passiveLinkCB.Name = "passiveLinkCB";
    this.passiveLinkCB.UseVisualStyleBackColor = true;
    this.passiveLinkCB.CheckedChanged += new EventHandler(this.passiveLinkCB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbLinkAttributeName, "cbLinkAttributeName");
    this.cbLinkAttributeName.Name = "cbLinkAttributeName";
    this.cbLinkAttributeName.TextChanged += new EventHandler(this.cbLinkAttributeName_TextChanged);
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.btnSelectLinkAttribute, "btnSelectLinkAttribute");
    this.btnSelectLinkAttribute.Name = "btnSelectLinkAttribute";
    this.btnSelectLinkAttribute.Click += new EventHandler(this.btnSelectLinkAttribute_Click);
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.cbLinkAttributeName);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.btnSelectLinkAttribute);
    this.Controls.Add((Control) this.passiveLinkCB);
    this.Controls.Add((Control) this.dbObjectReferenceRB);
    this.Controls.Add((Control) this.emptyReferenceRB);
    this.Controls.Add((Control) this.docNodeReferenceRB);
    this.Controls.Add((Control) this.tbObjectName);
    this.Controls.Add((Control) this.btnSelectObject);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.cbRefType);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReferenceToObjectEditorDlg);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Конструктор</summary>
  public ReferenceToObjectEditorDlg() => this.InitializeComponent();

  /// <summary>Редактировать ссылку</summary>
  /// <param name="referenceOwner">Владелец ссылки</param>
  /// <param name="reference">Ссылка</param>
  /// <returns>Отредактированная ссылка</returns>
  public ReferenceBase EditReference(DocumentTreeNode referenceOwner, ReferenceBase reference)
  {
    try
    {
      this.suspendEvents = true;
      this.referenceOwner = referenceOwner;
      if (reference != null)
      {
        this.reference = reference.Clone();
        this.reference.AssignOwnerNode(referenceOwner);
        this.reference.UpdateLink(false, false);
        if (reference is ReferenceToDBObjectBase)
        {
          this.dbObjectReferenceRB.Checked = true;
          this.passiveLinkCB.Checked = ((ReferenceToDBObjectBase) reference).PassiveLink;
        }
        else if (reference is ReferenceToNode)
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
        reference = this.reference;
    }
    finally
    {
      if (reference == null && this.reference != null)
        this.reference.DisconnectLink();
    }
    return reference;
  }

  /// <summary>Выполнить диалог</summary>
  /// <param name="referenceOwner">Владелец ссылки</param>
  /// <param name="reference">Ссылка</param>
  /// <returns>Отредактированная ссылка</returns>
  public static ReferenceBase ExecuteDialog(
    DocumentTreeNode referenceOwner,
    ReferenceBase reference)
  {
    return new ReferenceToObjectEditorDlg().EditReference(referenceOwner, reference);
  }

  /// <summary>Обновить свойство Enabled контролов диалога</summary>
  protected virtual void UpdateEnableds()
  {
    if (this.reference is IEditableReferenceToObject reference)
    {
      this.btnOK.Enabled = this.changed;
      this.cbRefType.Enabled = !this.emptyReferenceRB.Checked;
      this.btnSelectObject.Enabled = !this.emptyReferenceRB.Checked && reference.CanCallSelectObjectDialog;
      this.passiveLinkCB.Enabled = reference is ReferenceToDBObjectBase;
      this.cbLinkAttributeName.Enabled = reference.UseLinkAttribute;
      this.btnSelectLinkAttribute.Enabled = reference.UseLinkAttribute;
    }
    else
    {
      this.passiveLinkCB.Enabled = false;
      this.btnOK.Enabled = this.changed;
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
        if (typeof (ReferenceToDBObjectBase).IsAssignableFrom(referenceClass))
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
      num = reference == null || !(reference is ReferenceToNode) ? -1 : this.CalcRefTypeIndex(reference);
    }
    else
    {
      for (int index = 0; index < this.dbObjectRefSubTypeNames.Count; ++index)
      {
        if (this.dbObjectRefSubTypeNames[index].Count > 0)
          this.cbRefType.Items.AddRange((object[]) this.dbObjectRefSubTypeNames[index].ToArray());
      }
      num = reference == null || !(reference is ReferenceToDBObjectBase) ? -1 : this.CalcRefTypeIndex(reference);
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
      int num2 = !(referenceToObject is ReferenceToDBObjectBase) ? this.docRefTypeList.IndexOf(referenceToObject.GetType()) : this.dbObjectRefTypeList.IndexOf(referenceToObject.GetType());
      if (num2 != -1)
      {
        int num3 = 0;
        if (referenceToObject is ReferenceToDBObjectBase)
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
      string text = this.cbLinkAttributeName.Text;
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
          if (this.reference is ReferenceToDBObjectBase)
            ((ReferenceToDBObjectBase) this.reference).PassiveLink = false;
        }
        ((IEditableReferenceToObject) this.reference).SetReferenceSubType(this.referenceOwner, this.cbRefType.Text, typeof (IEditableReferenceToObject));
        this.reference.UpdateLink(true, false, false);
      }
      IEditableReferenceToObject reference = this.reference as IEditableReferenceToObject;
      this.tbObjectName.Text = reference.ObjectCaption;
      this.btnSelectObject.Enabled = reference.CanCallSelectObjectDialog;
      ReferenceToDBObjectBase referenceToDbObjectBase = reference as ReferenceToDBObjectBase;
      this.passiveLinkCB.Checked = referenceToDbObjectBase != null && referenceToDbObjectBase.PassiveLink;
      this.cbLinkAttributeName.Items.Clear();
      this.cbLinkAttributeName.Text = "";
      if (reference.UseLinkAttribute)
      {
        string[] attributeNameList = reference.GetLinkAttributeNameList();
        if (attributeNameList != null && attributeNameList.Length != 0)
        {
          this.cbLinkAttributeName.DropDownStyle = ComboBoxStyle.DropDown;
          this.cbLinkAttributeName.Items.AddRange((object[]) attributeNameList);
        }
        else
          this.cbLinkAttributeName.DropDownStyle = ComboBoxStyle.Simple;
        if (text != "" && text != null)
          this.cbLinkAttributeName.Text = text;
        else
          this.cbLinkAttributeName.Text = reference.LinkAttributeName;
        this.cbLinkAttributeName.SelectionLength = 0;
      }
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
    }
    this.UpdateEnableds();
  }

  private void btnSelectObject_Click(object sender, EventArgs e)
  {
    if (!(this.reference is IEditableReferenceToObject reference))
      return;
    this.changed = true;
    if (reference.CanCallSelectObjectDialog)
      reference.CallSelectObjectDialog();
    this.tbObjectName.Text = reference.ObjectCaption;
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
    if (this.reference is ReferenceToDBObjectBase reference)
      reference.PassiveLink = this.passiveLinkCB.Checked;
    this.UpdateEnableds();
  }

  private void btnSelectLinkAttribute_Click(object sender, EventArgs e)
  {
    if (!(this.reference is IEditableReferenceToObject reference))
      return;
    this.changed = true;
    if (reference.CanCallSelectLinkAttributeDialog)
      reference.CallSelectLinkAttributeDialog();
    this.cbLinkAttributeName.Text = reference.LinkAttributeName;
    this.cbLinkAttributeName.SelectionLength = 0;
    this.UpdateEnableds();
  }

  private void cbLinkAttributeName_TextChanged(object sender, EventArgs e)
  {
    if (!(this.reference is IEditableReferenceToTextSource reference) || !reference.IsReferenceToAttribute)
      return;
    this.changed = true;
    reference.LinkAttributeName = this.cbLinkAttributeName.Text;
    this.reference.UpdateLink(true, false, false);
    this.cbLinkAttributeName.SelectionLength = 0;
    this.UpdateEnableds();
  }
}
