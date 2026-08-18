// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ReferenceToTextSourceEditorDlg
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
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Диалог редактирования ссылок на источник текста</summary>
public class ReferenceToTextSourceEditorDlg : Form
{
  private Button btnCancel;
  private Button btnOK;
  private Label label2;
  private ComboBox cbAttributeName;
  private Label label4;
  private TextBox tbAttrValue;
  private Button btnSelectAttribute;
  private RadioButton docNodeReferenceRB;
  private RadioButton dbObjectReferenceRB;
  private RadioButton emptyReferenceRB;
  private CheckBox passiveLinkCB;
  private GroupBox groupBox1;
  private TextBox tbObjectName;
  private Label label3;
  private Button btnSelectObject;
  private ComboBox cbLinkAttributeName;
  private Label label5;
  private Button btnSelectLinkAttribute;
  private Label label1;
  private ComboBox cbRefType;
  private GroupBox groupBox2;
  private RadioButton dbSignRB;
  private ComboBox cbSignField;
  private Label label6;
  private CheckBox printOnlyCB;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private bool suspendEvents;
  private ReferenceBase reference;
  private DocumentTreeNode referenceOwner;
  private List<System.Type> docRefTypeList = new List<System.Type>();
  private List<System.Type> dbObjectRefTypeList = new List<System.Type>();
  private List<System.Type> dbSignRefTypeList = new List<System.Type>();
  private List<List<string>> docRefSubTypeNames;
  private List<List<string>> dbObjectRefSubTypeNames;
  private List<List<string>> dbSignsRefSubTypeNames;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReferenceToTextSourceEditorDlg));
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.cbAttributeName = new ComboBox();
    this.label2 = new Label();
    this.tbAttrValue = new TextBox();
    this.label4 = new Label();
    this.btnSelectAttribute = new Button();
    this.docNodeReferenceRB = new RadioButton();
    this.dbObjectReferenceRB = new RadioButton();
    this.emptyReferenceRB = new RadioButton();
    this.passiveLinkCB = new CheckBox();
    this.groupBox1 = new GroupBox();
    this.tbObjectName = new TextBox();
    this.label3 = new Label();
    this.btnSelectObject = new Button();
    this.cbLinkAttributeName = new ComboBox();
    this.label5 = new Label();
    this.btnSelectLinkAttribute = new Button();
    this.label1 = new Label();
    this.cbRefType = new ComboBox();
    this.groupBox2 = new GroupBox();
    this.cbSignField = new ComboBox();
    this.label6 = new Label();
    this.dbSignRB = new RadioButton();
    this.printOnlyCB = new CheckBox();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    componentResourceManager.ApplyResources((object) this.cbAttributeName, "cbAttributeName");
    this.cbAttributeName.Name = "cbAttributeName";
    this.cbAttributeName.SelectedIndexChanged += new EventHandler(this.cbAttributeName_SelectedIndexChanged);
    this.cbAttributeName.TextChanged += new EventHandler(this.cbAttributeName_TextChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.tbAttrValue, "tbAttrValue");
    this.tbAttrValue.Name = "tbAttrValue";
    this.tbAttrValue.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.btnSelectAttribute, "btnSelectAttribute");
    this.btnSelectAttribute.Name = "btnSelectAttribute";
    this.btnSelectAttribute.Click += new EventHandler(this.btnSelectAttribute_Click);
    componentResourceManager.ApplyResources((object) this.docNodeReferenceRB, "docNodeReferenceRB");
    this.docNodeReferenceRB.Name = "docNodeReferenceRB";
    this.docNodeReferenceRB.UseVisualStyleBackColor = true;
    this.docNodeReferenceRB.CheckedChanged += new EventHandler(this.emptyReferenceRB_CheckedChanged);
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
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.tbObjectName);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.btnSelectObject);
    this.groupBox1.Controls.Add((Control) this.cbLinkAttributeName);
    this.groupBox1.Controls.Add((Control) this.label5);
    this.groupBox1.Controls.Add((Control) this.btnSelectLinkAttribute);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.tbObjectName, "tbObjectName");
    this.tbObjectName.Name = "tbObjectName";
    this.tbObjectName.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.btnSelectObject, "btnSelectObject");
    this.btnSelectObject.Name = "btnSelectObject";
    this.btnSelectObject.Click += new EventHandler(this.btnSelectObject_Click);
    componentResourceManager.ApplyResources((object) this.cbLinkAttributeName, "cbLinkAttributeName");
    this.cbLinkAttributeName.Name = "cbLinkAttributeName";
    this.cbLinkAttributeName.TextChanged += new EventHandler(this.cbLinkAttributeName_TextChanged);
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.btnSelectLinkAttribute, "btnSelectLinkAttribute");
    this.btnSelectLinkAttribute.Name = "btnSelectLinkAttribute";
    this.btnSelectLinkAttribute.Click += new EventHandler(this.btnSelectLinkAttribute_Click);
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
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Controls.Add((Control) this.cbSignField);
    this.groupBox2.Controls.Add((Control) this.tbAttrValue);
    this.groupBox2.Controls.Add((Control) this.label6);
    this.groupBox2.Controls.Add((Control) this.label2);
    this.groupBox2.Controls.Add((Control) this.cbAttributeName);
    this.groupBox2.Controls.Add((Control) this.label4);
    this.groupBox2.Controls.Add((Control) this.btnSelectAttribute);
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbSignField, "cbSignField");
    this.cbSignField.Name = "cbSignField";
    this.cbSignField.TextChanged += new EventHandler(this.cbSignField_TextChanged);
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.dbSignRB, "dbSignRB");
    this.dbSignRB.Name = "dbSignRB";
    this.dbSignRB.UseVisualStyleBackColor = true;
    this.dbSignRB.CheckedChanged += new EventHandler(this.emptyReferenceRB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.printOnlyCB, "printOnlyCB");
    this.printOnlyCB.Name = "printOnlyCB";
    this.printOnlyCB.UseVisualStyleBackColor = true;
    this.printOnlyCB.CheckedChanged += new EventHandler(this.printOnlyCB_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.cbRefType);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.printOnlyCB);
    this.Controls.Add((Control) this.passiveLinkCB);
    this.Controls.Add((Control) this.dbSignRB);
    this.Controls.Add((Control) this.dbObjectReferenceRB);
    this.Controls.Add((Control) this.emptyReferenceRB);
    this.Controls.Add((Control) this.docNodeReferenceRB);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ReferenceToTextSourceEditorDlg);
    this.ShowInTaskbar = false;
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public bool IsReadOnly { get; set; }

  /// <summary>Конструктор</summary>
  public ReferenceToTextSourceEditorDlg() => this.InitializeComponent();

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
        if (reference is IEditableReferenceToTextSource referenceToTextSource)
          referenceToTextSource.UpdateAttributeInfo();
        this.reference = reference.Clone();
        this.reference.AssignOwnerNode(referenceOwner);
        this.reference.UpdateLink(false, false, false);
        this.printOnlyCB.Checked = reference.PrintReference;
        if (reference is ReferenceToDBObjectAttributeBase)
        {
          this.dbObjectReferenceRB.Checked = true;
          this.passiveLinkCB.Checked = ((ReferenceToDBObjectBase) reference).PassiveLink;
        }
        else if (reference is ReferenceToNodeAttribute)
          this.docNodeReferenceRB.Checked = true;
        else if (reference is ReferenceToSignBase)
          this.dbSignRB.Checked = true;
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
    ReferenceBase reference,
    bool readOnlyMode = false)
  {
    return new ReferenceToTextSourceEditorDlg()
    {
      IsReadOnly = readOnlyMode
    }.EditReference(referenceOwner, reference);
  }

  /// <summary>Обновить свойство Enabled контролов диалога</summary>
  protected virtual void UpdateEnableds()
  {
    if (this.IsReadOnly)
    {
      this.btnOK.Enabled = false;
      this.cbRefType.Enabled = false;
      this.btnSelectObject.Enabled = false;
      this.cbAttributeName.Enabled = false;
      this.btnSelectAttribute.Enabled = false;
      this.passiveLinkCB.Enabled = false;
      this.cbLinkAttributeName.Enabled = false;
      this.btnSelectLinkAttribute.Enabled = false;
      this.printOnlyCB.Enabled = false;
      this.tbAttrValue.Enabled = false;
      this.tbObjectName.Enabled = false;
      this.emptyReferenceRB.Enabled = false;
      this.docNodeReferenceRB.Enabled = false;
      this.dbObjectReferenceRB.Enabled = false;
      this.dbSignRB.Enabled = false;
    }
    else
    {
      if (this.reference is IEditableReferenceToTextSource reference)
      {
        this.btnOK.Enabled = this.changed && (!reference.IsReferenceToAttribute || reference.AttributeName != null && reference.AttributeName != "");
        this.cbRefType.Enabled = !this.emptyReferenceRB.Checked;
        this.btnSelectObject.Enabled = !this.emptyReferenceRB.Checked && reference.CanCallSelectObjectDialog && !reference.UseLinkAttribute;
        this.cbAttributeName.Enabled = !this.emptyReferenceRB.Checked;
        this.btnSelectAttribute.Enabled = !this.emptyReferenceRB.Checked && reference.CanCallSelectAttributeDialog;
        this.passiveLinkCB.Enabled = !this.emptyReferenceRB.Checked && reference is ReferenceToDBObjectAttributeBase;
        this.cbLinkAttributeName.Enabled = !this.emptyReferenceRB.Checked && reference.UseLinkAttribute;
        this.btnSelectLinkAttribute.Enabled = !this.emptyReferenceRB.Checked && reference.UseLinkAttribute;
        this.printOnlyCB.Enabled = true;
      }
      else
      {
        this.btnOK.Enabled = this.changed;
        this.passiveLinkCB.Enabled = false;
        this.printOnlyCB.Enabled = false;
      }
      int num = this.cbAttributeName.Bottom - this.label2.Top + (this.cbAttributeName.Top - this.label2.Bottom);
      if (this.reference is ReferenceToSignBase)
      {
        if (this.cbSignField.Enabled)
          return;
        this.passiveLinkCB.Visible = false;
        this.groupBox2.Height += num;
        this.label4.Location = new Point(this.label4.Location.X, this.label4.Location.Y + num);
        this.tbAttrValue.Location = new Point(this.tbAttrValue.Location.X, this.tbAttrValue.Location.Y + num);
        this.label6.Visible = true;
        Label label6 = this.label6;
        int x1 = this.label2.Location.X;
        Point location = this.label2.Location;
        int y1 = location.Y + num;
        Point point1 = new Point(x1, y1);
        label6.Location = point1;
        this.cbSignField.Visible = true;
        this.cbSignField.Enabled = true;
        ComboBox cbSignField = this.cbSignField;
        location = this.cbAttributeName.Location;
        int x2 = location.X;
        location = this.cbAttributeName.Location;
        int y2 = location.Y + num;
        Point point2 = new Point(x2, y2);
        cbSignField.Location = point2;
      }
      else
      {
        if (!this.cbSignField.Enabled)
          return;
        this.passiveLinkCB.Visible = true;
        this.groupBox2.Height -= num;
        this.label4.Location = new Point(this.label4.Location.X, this.label4.Location.Y - num);
        this.tbAttrValue.Location = new Point(this.tbAttrValue.Location.X, this.tbAttrValue.Location.Y - num);
        this.label6.Visible = false;
        Label label6 = this.label6;
        int x3 = this.label2.Location.X;
        Point location = this.label2.Location;
        int y3 = location.Y - num;
        Point point3 = new Point(x3, y3);
        label6.Location = point3;
        this.cbSignField.Visible = false;
        this.cbSignField.Enabled = false;
        ComboBox cbSignField = this.cbSignField;
        location = this.cbAttributeName.Location;
        int x4 = location.X;
        location = this.cbAttributeName.Location;
        int y4 = location.Y - num;
        Point point4 = new Point(x4, y4);
        cbSignField.Location = point4;
      }
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
      if (typeof (ITextSource).IsAssignableFrom(referenceClass) && typeof (IEditableReferenceToTextSource).IsAssignableFrom(referenceClass))
      {
        if (typeof (ReferenceToDBObjectAttributeBase).IsAssignableFrom(referenceClass))
          this.dbObjectRefTypeList.Add(referenceClass);
        else if (typeof (ReferenceToSignBase).IsAssignableFrom(referenceClass))
          this.dbSignRefTypeList.Add(referenceClass);
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
    this.dbSignsRefSubTypeNames = new List<List<string>>();
    for (int index = 0; index < this.docRefTypeList.Count; ++index)
    {
      string[] referenceSubTypes = ((IEditableReferenceToObject) Activator.CreateInstance(this.docRefTypeList[index])).GetReferenceSubTypes(this.referenceOwner, typeof (IEditableReferenceToTextSource));
      this.docRefSubTypeNames.Add(new List<string>());
      if (referenceSubTypes != null && referenceSubTypes.Length != 0)
        this.docRefSubTypeNames[index].AddRange((IEnumerable<string>) referenceSubTypes);
    }
    for (int index = 0; index < this.dbObjectRefTypeList.Count; ++index)
    {
      string[] referenceSubTypes = ((IEditableReferenceToObject) Activator.CreateInstance(this.dbObjectRefTypeList[index])).GetReferenceSubTypes(this.referenceOwner, typeof (IEditableReferenceToTextSource));
      this.dbObjectRefSubTypeNames.Add(new List<string>());
      if (referenceSubTypes != null && referenceSubTypes.Length != 0)
        this.dbObjectRefSubTypeNames[index].AddRange((IEnumerable<string>) referenceSubTypes);
    }
    for (int index = 0; index < this.dbSignRefTypeList.Count; ++index)
    {
      string[] referenceSubTypes = ((IEditableReferenceToObject) Activator.CreateInstance(this.dbSignRefTypeList[index])).GetReferenceSubTypes(this.referenceOwner, typeof (IEditableReferenceToTextSource));
      this.dbSignsRefSubTypeNames.Add(new List<string>());
      if (referenceSubTypes != null && referenceSubTypes.Length != 0)
        this.dbSignsRefSubTypeNames[index].AddRange((IEnumerable<string>) referenceSubTypes);
    }
    this.UpdateSubTypesComboBox();
  }

  /// <summary>Перезагрузить все подтипы ссылок у классов ссылок</summary>
  protected void UpdateSubTypesComboBox()
  {
    ReferenceBase reference = this.reference;
    this.cbRefType.Items.Clear();
    int num = -1;
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
    else if (this.dbObjectReferenceRB.Checked)
    {
      for (int index = 0; index < this.dbObjectRefSubTypeNames.Count; ++index)
      {
        if (this.dbObjectRefSubTypeNames[index].Count > 0)
          this.cbRefType.Items.AddRange((object[]) this.dbObjectRefSubTypeNames[index].ToArray());
      }
      num = reference == null || !(reference is ReferenceToDBObjectAttributeBase) ? -1 : this.CalcRefTypeIndex(reference);
    }
    else if (this.dbSignRB.Checked)
    {
      for (int index = 0; index < this.dbSignsRefSubTypeNames.Count; ++index)
      {
        if (this.dbSignsRefSubTypeNames[index].Count > 0)
          this.cbRefType.Items.AddRange((object[]) this.dbSignsRefSubTypeNames[index].ToArray());
      }
      num = reference == null || !(reference is ReferenceToSignBase) ? -1 : this.CalcRefTypeIndex(reference);
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
    if (reference is IEditableReferenceToTextSource referenceToTextSource)
    {
      int num2;
      switch (referenceToTextSource)
      {
        case ReferenceToDBObjectAttributeBase _:
          num2 = this.dbObjectRefTypeList.IndexOf(referenceToTextSource.GetType());
          break;
        case ReferenceToSignBase _:
          num2 = this.dbSignRefTypeList.IndexOf(referenceToTextSource.GetType());
          break;
        default:
          num2 = this.docRefTypeList.IndexOf(referenceToTextSource.GetType());
          break;
      }
      if (num2 != -1)
      {
        int num3 = 0;
        switch (referenceToTextSource)
        {
          case ReferenceToDBObjectAttributeBase _:
            for (int index = 0; index < num2; ++index)
            {
              if (this.dbObjectRefSubTypeNames[index] != null)
                num3 += this.dbObjectRefSubTypeNames[index].Count;
            }
            break;
          case ReferenceToSignBase _:
            for (int index = 0; index < num2; ++index)
            {
              if (this.dbSignsRefSubTypeNames[index] != null)
                num3 += this.dbSignsRefSubTypeNames[index].Count;
            }
            break;
          default:
            for (int index = 0; index < num2; ++index)
            {
              if (this.docRefSubTypeNames[index] != null)
                num3 += this.docRefSubTypeNames[index].Count;
            }
            break;
        }
        int referenceSubTypeIndex = referenceToTextSource.GetReferenceSubTypeIndex(typeof (IEditableReferenceToTextSource));
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
      else if (this.dbSignRB.Checked)
      {
        for (int index4 = 0; index4 < this.dbSignsRefSubTypeNames.Count; ++index4)
        {
          if (this.dbSignsRefSubTypeNames[index4] != null)
            num += this.dbSignsRefSubTypeNames[index4].Count;
          if (selectedIndex < num)
          {
            index1 = index4;
            break;
          }
        }
        if (index1 != -1 && index1 < this.dbSignRefTypeList.Count)
          type = this.dbSignRefTypeList[index1];
      }
      string str1 = this.cbLinkAttributeName.Text;
      string str2 = this.cbAttributeName.Text;
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
          if (this.reference is ReferenceToDBObjectAttributeBase)
            ((ReferenceToDBObjectBase) this.reference).PassiveLink = false;
        }
        ((IEditableReferenceToObject) this.reference).SetReferenceSubType(this.referenceOwner, this.cbRefType.Text, typeof (IEditableReferenceToTextSource));
        this.reference.UpdateLink(true, false, false);
      }
      this.printOnlyCB.Checked = this.reference.PrintReference;
      IEditableReferenceToTextSource reference = this.reference as IEditableReferenceToTextSource;
      this.btnSelectObject.Enabled = reference != null && reference.CanCallSelectObjectDialog;
      if (reference != null)
      {
        if (reference.UseLinkAttribute && string.IsNullOrEmpty(str1))
          str1 = reference.LinkAttributeName;
        if (reference.IsReferenceToAttribute && string.IsNullOrEmpty(str2))
          str2 = reference.AttributeName;
        this.tbObjectName.Text = reference.ObjectCaption;
        this.passiveLinkCB.Checked = reference is ReferenceToDBObjectAttributeBase objectAttributeBase && objectAttributeBase.PassiveLink;
      }
      this.cbLinkAttributeName.Items.Clear();
      this.cbLinkAttributeName.Text = "";
      if (reference != null && reference.UseLinkAttribute)
      {
        string[] attributeNameList = reference.GetLinkAttributeNameList();
        if (attributeNameList != null && attributeNameList.Length != 0)
        {
          this.cbLinkAttributeName.DropDownStyle = ComboBoxStyle.DropDown;
          this.cbLinkAttributeName.Items.AddRange((object[]) attributeNameList);
        }
        else
          this.cbLinkAttributeName.DropDownStyle = ComboBoxStyle.Simple;
        if (!string.IsNullOrEmpty(str1))
          this.cbLinkAttributeName.Text = str1;
        else
          this.cbLinkAttributeName.Text = reference?.LinkAttributeName;
        this.cbLinkAttributeName.SelectionLength = 0;
      }
      if (reference is ReferenceToSignBase referenceToSignBase)
      {
        this.cbSignField.Items.Clear();
        this.cbSignField.Text = referenceToSignBase.SignField;
        this.cbSignField.Items.AddRange((object[]) referenceToSignBase.GetSignFieldsList().ToArray());
      }
      this.cbAttributeName.Items.Clear();
      this.cbAttributeName.Text = "";
      if (reference != null)
      {
        string[] attributeNameList = reference.GetAttributeNameList();
        if (attributeNameList != null && attributeNameList.Length != 0)
        {
          this.cbAttributeName.DropDownStyle = ComboBoxStyle.DropDown;
          this.cbAttributeName.Items.AddRange((object[]) attributeNameList);
        }
        else
          this.cbAttributeName.DropDownStyle = ComboBoxStyle.Simple;
        if (reference.IsReferenceToAttribute && !string.IsNullOrEmpty(str2) && attributeNameList != null && ((IEnumerable<string>) attributeNameList).ToList<string>().Contains(str2))
          this.cbAttributeName.Text = str2;
        else
          this.cbAttributeName.Text = reference.AttributeName;
      }
      this.cbAttributeName.SelectionLength = 0;
      if (reference != null)
      {
        object[] customAttributes = reference.GetType().GetProperty("AttributeName").GetCustomAttributes(typeof (ReadOnlyForDlgAttribute), true);
        if (customAttributes != null && customAttributes.Length != 0)
          this.cbAttributeName.Enabled = reference.IsReferenceToAttribute && !((ReadOnlyForDlgAttribute) customAttributes[0]).IsReadOnly;
        else
          this.cbAttributeName.Enabled = reference.IsReferenceToAttribute;
        this.btnSelectAttribute.Enabled = reference.CanCallSelectAttributeDialog;
        if (reference is ITextSource textSource)
          this.tbAttrValue.Text = textSource.Text;
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
      this.cbAttributeName.Text = "";
      this.cbAttributeName.DropDownStyle = ComboBoxStyle.Simple;
      this.cbAttributeName.Enabled = false;
      this.btnSelectAttribute.Enabled = false;
      this.tbAttrValue.Text = "";
    }
    this.UpdateEnableds();
  }

  private void btnSelectObject_Click(object sender, EventArgs e)
  {
    if (!(this.reference is IEditableReferenceToTextSource reference))
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
    this.cbAttributeName.Enabled = reference.IsReferenceToAttribute;
    this.cbAttributeName.Text = text;
    this.btnSelectAttribute.Enabled = reference.CanCallSelectAttributeDialog;
    this.tbAttrValue.Text = ((ITextSource) this.reference).Text;
    this.UpdateEnableds();
  }

  private void btnSelectAttribute_Click(object sender, EventArgs e)
  {
    if (!(this.reference is IEditableReferenceToTextSource reference) || !reference.IsReferenceToAttribute)
      return;
    this.changed = true;
    if (reference.CanCallSelectAttributeDialog)
      reference.CallSelectAttributeDialog();
    this.cbAttributeName.Text = reference.AttributeName;
    this.cbAttributeName.SelectionLength = 0;
    this.tbAttrValue.Text = ((ITextSource) reference).Text;
    this.UpdateEnableds();
  }

  private void cbAttributeName_TextChanged(object sender, EventArgs e)
  {
    if (!(this.reference is IEditableReferenceToTextSource reference) || !reference.IsReferenceToAttribute)
      return;
    this.changed = true;
    reference.AttributeName = this.cbAttributeName.Text;
    this.reference.UpdateLink(true, false, false);
    this.cbAttributeName.SelectionLength = 0;
    this.tbAttrValue.Text = ((ITextSource) this.reference).Text;
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
    if (this.reference is ReferenceToDBObjectAttributeBase reference)
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
    string text = this.cbAttributeName.Text;
    this.cbAttributeName.Items.Clear();
    this.cbAttributeName.Text = "";
    string[] attributeNameList = reference.GetAttributeNameList();
    if (attributeNameList != null && attributeNameList.Length != 0)
    {
      this.cbAttributeName.DropDownStyle = ComboBoxStyle.DropDown;
      this.cbAttributeName.Items.AddRange((object[]) attributeNameList);
    }
    else
      this.cbAttributeName.DropDownStyle = ComboBoxStyle.Simple;
    if (reference.IsReferenceToAttribute && text != "" && text != null)
      this.cbAttributeName.Text = text;
    else
      this.cbAttributeName.Text = reference.AttributeName;
    this.cbAttributeName.SelectionLength = 0;
    object[] customAttributes = reference.GetType().GetProperty("AttributeName").GetCustomAttributes(typeof (ReadOnlyForDlgAttribute), true);
    if (customAttributes != null && customAttributes.Length != 0)
      this.cbAttributeName.Enabled = reference.IsReferenceToAttribute && !((ReadOnlyForDlgAttribute) customAttributes[0]).IsReadOnly;
    else
      this.cbAttributeName.Enabled = reference.IsReferenceToAttribute;
    this.btnSelectAttribute.Enabled = reference.CanCallSelectAttributeDialog;
    this.tbAttrValue.Text = ((ITextSource) reference).Text;
    this.UpdateEnableds();
  }

  private void cbSignField_TextChanged(object sender, EventArgs e)
  {
    if (!(this.reference is ReferenceToSignBase reference))
      return;
    this.changed = true;
    reference.SignField = this.cbSignField.Text;
    this.reference.UpdateLink(true, false, false);
    this.cbSignField.SelectionLength = 0;
    this.tbAttrValue.Text = ((ITextSource) this.reference).Text;
    this.UpdateEnableds();
  }

  private void cbAttributeName_SelectedIndexChanged(object sender, EventArgs e)
  {
  }

  private void printOnlyCB_CheckedChanged(object sender, EventArgs e)
  {
    this.changed = true;
    this.reference.PrintReference = this.printOnlyCB.Checked;
    this.UpdateEnableds();
  }
}
