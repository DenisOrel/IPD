
// Type: Intermech.Client.Core.ObjectCreator.ObjectCreatorNewRelationForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>
/// Локальный класс для реализации диалога выбора типа новой связи
/// </summary>
internal class ObjectCreatorNewRelationForm : Form
{
  private Panel panelBottom;
  private Label labelRelationType;
  private Label labelRelatedObject;
  private ComboBox comboBoxRelationTypes;
  private TextBox textBoxRelatedObject;
  private GroupBox groupBox1;
  private CheckBox checkBoxUseForAll;
  private Button buttonOk;
  private Button buttonCancel;
  private Label labelRelatedObjectType;
  private TextBox textBoxRelatedObjectType;
  private TextBox textBoxMessage;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public bool UseForAll => this.checkBoxUseForAll.Checked;

  public int SelectedRelationType
  {
    get
    {
      return ((ObjectCreatorNewRelationForm.LocalRelationType) this.comboBoxRelationTypes.SelectedItem).RelationTypeID;
    }
  }

  public ObjectCreatorNewRelationForm(string objCapt, string objTypeCapt, int[] relTypes)
  {
    this.InitializeComponent();
    this.textBoxRelatedObject.Text = objCapt;
    this.textBoxRelatedObjectType.Text = objTypeCapt;
    this.comboBoxRelationTypes.Items.Clear();
    foreach (int relType in relTypes)
      this.comboBoxRelationTypes.Items.Add((object) new ObjectCreatorNewRelationForm.LocalRelationType(relType));
    if (this.comboBoxRelationTypes.Items.Count <= 0)
      return;
    this.comboBoxRelationTypes.SelectedIndex = 0;
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectCreatorNewRelationForm));
    this.panelBottom = new Panel();
    this.groupBox1 = new GroupBox();
    this.checkBoxUseForAll = new CheckBox();
    this.buttonOk = new Button();
    this.buttonCancel = new Button();
    this.comboBoxRelationTypes = new ComboBox();
    this.labelRelationType = new Label();
    this.labelRelatedObjectType = new Label();
    this.labelRelatedObject = new Label();
    this.textBoxRelatedObjectType = new TextBox();
    this.textBoxRelatedObject = new TextBox();
    this.textBoxMessage = new TextBox();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.panelBottom.Controls.Add((Control) this.groupBox1);
    this.panelBottom.Controls.Add((Control) this.checkBoxUseForAll);
    this.panelBottom.Controls.Add((Control) this.buttonOk);
    this.panelBottom.Controls.Add((Control) this.buttonCancel);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.checkBoxUseForAll, "checkBoxUseForAll");
    this.checkBoxUseForAll.Name = "checkBoxUseForAll";
    componentResourceManager.ApplyResources((object) this.buttonOk, "buttonOk");
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Name = "buttonOk";
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    componentResourceManager.ApplyResources((object) this.comboBoxRelationTypes, "comboBoxRelationTypes");
    this.comboBoxRelationTypes.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxRelationTypes.Name = "comboBoxRelationTypes";
    componentResourceManager.ApplyResources((object) this.labelRelationType, "labelRelationType");
    this.labelRelationType.Name = "labelRelationType";
    componentResourceManager.ApplyResources((object) this.labelRelatedObjectType, "labelRelatedObjectType");
    this.labelRelatedObjectType.Name = "labelRelatedObjectType";
    componentResourceManager.ApplyResources((object) this.labelRelatedObject, "labelRelatedObject");
    this.labelRelatedObject.Name = "labelRelatedObject";
    componentResourceManager.ApplyResources((object) this.textBoxRelatedObjectType, "textBoxRelatedObjectType");
    this.textBoxRelatedObjectType.BackColor = SystemColors.Control;
    this.textBoxRelatedObjectType.Name = "textBoxRelatedObjectType";
    this.textBoxRelatedObjectType.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.textBoxRelatedObject, "textBoxRelatedObject");
    this.textBoxRelatedObject.BackColor = SystemColors.Control;
    this.textBoxRelatedObject.Name = "textBoxRelatedObject";
    this.textBoxRelatedObject.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.textBoxMessage, "textBoxMessage");
    this.textBoxMessage.BackColor = SystemColors.Control;
    this.textBoxMessage.Name = "textBoxMessage";
    this.AcceptButton = (IButtonControl) this.buttonOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.Controls.Add((Control) this.textBoxMessage);
    this.Controls.Add((Control) this.textBoxRelatedObjectType);
    this.Controls.Add((Control) this.labelRelatedObject);
    this.Controls.Add((Control) this.labelRelatedObjectType);
    this.Controls.Add((Control) this.labelRelationType);
    this.Controls.Add((Control) this.textBoxRelatedObject);
    this.Controls.Add((Control) this.comboBoxRelationTypes);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (ObjectCreatorNewRelationForm);
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Локальный класс для представления типов связей</summary>
  private class LocalRelationType
  {
    public int RelationTypeID { get; private set; }

    public LocalRelationType(int relationTypeID) => this.RelationTypeID = relationTypeID;

    public override string ToString() => MetaDataHelper.GetRelationTypeName(this.RelationTypeID);
  }
}
