
// Type: Intermech.Client.Core.ObjectCreator.ObjectCreatorSelectForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>Выбор типа создаваемого объекта</summary>
public class ObjectCreatorSelectForm : Form
{
  private readonly List<int> _aObjTypeIDs;
  private Button buttonCancel;
  private Button buttonNext;
  private System.ComponentModel.Container components;
  private Label label3;
  private Button buttonFinish;
  private CheckBox cbSortCode;
  private ObjectTypesSelectControl treeView1;

  private int _objectTypeID => this.treeView1.ObjectTypeID;

  public ObjectCreatorSelectForm(int[] aObjTypeIDs, int aSelectedID)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 687);
    this._aObjTypeIDs = aObjTypeIDs != null ? new List<int>((IEnumerable<int>) aObjTypeIDs) : new List<int>(0);
    this.treeView1.OnSelectObjectType += new SelectObjectTypeHandler(this.TreeView1_OnSelectObjectType);
    if (UISettings.ShowListObjectTypes4CreatingObject)
    {
      this.cbSortCode.Visible = true;
      this.treeView1.BuildList((IList<int>) aObjTypeIDs, aSelectedID, this.cbSortCode.Checked, false);
    }
    else
    {
      this.cbSortCode.Visible = false;
      this.treeView1.BuildTree((IList<int>) aObjTypeIDs, aSelectedID, false);
    }
  }

  private void TreeView1_OnSelectObjectType(object sender, SelectObjectTypeEventArgs e)
  {
    this.buttonNext.Enabled = e.Enable;
  }

  /// <summary>Для вызова нового экземпляра формы в новом диалоге</summary>
  /// <param name="aObjTypeIDs">массив идентификаторов типов, которые можно
  /// использовать для создания нового объекта</param>
  /// <param name="aSelectedID">Идентификатор типа, которого следует выделить по-умолчанию</param>
  /// <returns>идентификатор типа объекта, по которому надо создавать новый</returns>
  public static int ShowSelectDialog(int[] aObjTypeIDs, int aSelectedID)
  {
    using (ObjectCreatorSelectForm creatorSelectForm = new ObjectCreatorSelectForm(aObjTypeIDs, aSelectedID))
    {
      int num = creatorSelectForm.ShowDialog() == DialogResult.OK ? 1 : 0;
      Intermech.Client.Core.ObjectCreator.ObjectCreator.SaveSettings((Form) creatorSelectForm);
      return num != 0 ? creatorSelectForm._objectTypeID : -1;
    }
  }

  private void ObjectCreatorSelectForm_Load(object sender, EventArgs e)
  {
    Intermech.Client.Core.ObjectCreator.ObjectCreator.LoadSettings((Form) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectCreatorSelectForm));
    this.buttonCancel = new Button();
    this.buttonNext = new Button();
    this.label3 = new Label();
    this.buttonFinish = new Button();
    this.cbSortCode = new CheckBox();
    this.treeView1 = new ObjectTypesSelectControl();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    componentResourceManager.ApplyResources((object) this.buttonNext, "buttonNext");
    this.buttonNext.DialogResult = DialogResult.OK;
    this.buttonNext.Name = "buttonNext";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.ForeColor = SystemColors.GrayText;
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.buttonFinish, "buttonFinish");
    this.buttonFinish.Name = "buttonFinish";
    componentResourceManager.ApplyResources((object) this.cbSortCode, "cbSortCode");
    this.cbSortCode.Name = "cbSortCode";
    this.cbSortCode.UseVisualStyleBackColor = true;
    this.cbSortCode.CheckedChanged += new EventHandler(this.cbSortCode_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.treeView1, "treeView1");
    this.treeView1.Name = "treeView1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.Controls.Add((Control) this.cbSortCode);
    this.Controls.Add((Control) this.treeView1);
    this.Controls.Add((Control) this.buttonFinish);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.buttonNext);
    this.Name = nameof (ObjectCreatorSelectForm);
    this.Load += new EventHandler(this.ObjectCreatorSelectForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void cbSortCode_CheckedChanged(object sender, EventArgs e)
  {
    this.treeView1.BuildList((IList<int>) this._aObjTypeIDs, -1, this.cbSortCode.Checked, false);
  }
}
