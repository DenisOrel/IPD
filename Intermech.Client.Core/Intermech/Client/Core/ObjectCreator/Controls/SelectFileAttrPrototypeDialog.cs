
// Type: Intermech.Client.Core.ObjectCreator.Controls.SelectFileAttrPrototypeDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator.Controls;

internal class SelectFileAttrPrototypeDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button buttonOk;
  private ListBox listBox1;

  /// <summary>Идентификатор выбранного прототипа</summary>
  public long SelectedPrototypeId
  {
    get => ((SelectFileAttrPrototypeDialog.prototypeItem) this.listBox1.SelectedItem).ID;
  }

  /// <summary>Конструктор</summary>
  /// <param name="prototypes">Массив идентификаторов прототипов из которых надо выбрать один</param>
  public SelectFileAttrPrototypeDialog(long[] prototypes)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1182);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (prototypes.Length == 1)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(prototypes[0]);
        this.listBox1.Items.Add((object) new SelectFileAttrPrototypeDialog.prototypeItem(dbObject.ObjectID, dbObject.Caption));
      }
      else if (MetaDataHelper.GetAttribute4ObjectType(new Guid("cad00342-306c-11d8-b4e9-00304f19f545"), new Guid("cad00202-306c-11d8-b4e9-00304f19f545")) != null)
      {
        ColumnDescriptor[] columns = new ColumnDescriptor[3]
        {
          new ColumnDescriptor((object) -2, SortOrders.NONE, 0),
          new ColumnDescriptor((object) sessionKeeper.Session.IdentHelper.SortIndexID, SortOrders.ASC, 0),
          new ColumnDescriptor((object) -50, SortOrders.ASC, 1)
        };
        DataTable dataTable = sessionKeeper.Session.ObjectsSelect(new Guid("cad00342-306c-11d8-b4e9-00304f19f545"), new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) prototypes, LogicalOperators.NONE, 0, false)
        }, columns));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          this.listBox1.Items.Add((object) new SelectFileAttrPrototypeDialog.prototypeItem(Convert.ToInt64(dataTable.Rows[index][0]), dataTable.Rows[index][2].ToString()));
      }
      else
      {
        for (int index = 0; index < prototypes.Length; ++index)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(prototypes[index]);
          this.listBox1.Items.Add((object) new SelectFileAttrPrototypeDialog.prototypeItem(dbObject.ObjectID, dbObject.Caption));
        }
      }
    }
    if (this.listBox1.Items.Count <= 0)
      return;
    this.listBox1.SelectedIndex = 0;
  }

  private void SelectFileAttrPrototypeDialog_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SelectFileAttrPrototypeDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectFileAttrPrototypeDialog));
    this.buttonOk = new Button();
    this.listBox1 = new ListBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.buttonOk, "buttonOk");
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Name = "buttonOk";
    componentResourceManager.ApplyResources((object) this.listBox1, "listBox1");
    this.listBox1.FormattingEnabled = true;
    this.listBox1.Name = "listBox1";
    this.AcceptButton = (IButtonControl) this.buttonOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.listBox1);
    this.Controls.Add((Control) this.buttonOk);
    this.Name = nameof (SelectFileAttrPrototypeDialog);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.SelectFileAttrPrototypeDialog_FormClosed);
    this.Load += new EventHandler(this.SelectFileAttrPrototypeDialog_Load);
    this.ResumeLayout(false);
  }

  /// <summary>Локальный класс для работы с прототипами в ListBox</summary>
  private class prototypeItem
  {
    /// <summary>идентификатор прототипа для файлового атрибута</summary>
    public long ID;
    /// <summary>
    /// строковое значение для предоставления данного прототипа
    /// </summary>
    public string Name;

    /// <summary>Конструктор</summary>
    /// <param name="id">Идентификатор прототипа для файлового атрибута</param>
    /// <param name="name">Строковое значение для предоставления данного прототипа</param>
    public prototypeItem(long id, string name)
    {
      this.ID = id;
      this.Name = name;
    }

    /// <summary>
    /// Перекрытая функция для представления прототипа в виде строки
    /// </summary>
    /// <returns>Строковое значение для предоставления данного прототипа</returns>
    public override string ToString() => this.Name;
  }
}
