// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterConfigView
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.EventLog;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

public class FilterConfigView : UserControl, IView
{
  public static string FilterConfigViewName = LocalizationHolder.rm.GetString("DatabaseConfigurator_101");
  public static INamedImageList _namedImageList = (INamedImageList) null;
  private Guid _filterGuid;
  private Filter _filter;
  private FilterItemController[] _controllers;
  private Panel panelMain;
  private ConditionComboBox conditionComboBox13;
  private DateTimePicker dateTimePicker2;
  private CheckBox checkBox13;
  private Button btSelectUser;
  private TextBox tbUserName;
  private TextBox textBox7;
  private ConditionComboBox conditionComboBox12;
  private CheckBox checkBox12;
  private TextBox tbFilterName;
  private Label label1;
  private CheckedListBox checkedListBox1;
  private TextBox textBox6;
  private ComboBox comboBox2;
  private TextBox textBox5;
  private TextBox textBox4;
  private TextBox textBox3;
  private TextBox textBox2;
  private TextBox textBox1;
  private DateTimePicker dateTimePicker1;
  private ComboBox comboBox1;
  private ConditionComboBox conditionComboBox11;
  private ConditionComboBox conditionComboBox10;
  private ConditionComboBox conditionComboBox9;
  private ConditionComboBox conditionComboBox8;
  private ConditionComboBox conditionComboBox7;
  private ConditionComboBox conditionComboBox6;
  private ConditionComboBox conditionComboBox5;
  private ConditionComboBox conditionComboBox4;
  private ConditionComboBox conditionComboBox3;
  private ConditionComboBox conditionComboBox2;
  private ConditionComboBox conditionComboBox1;
  private CheckBox checkBox11;
  private CheckBox checkBox10;
  private CheckBox checkBox9;
  private CheckBox checkBox8;
  private CheckBox checkBox7;
  private CheckBox checkBox5;
  private CheckBox checkBox6;
  private CheckBox checkBox4;
  private CheckBox checkBox3;
  private CheckBox checkBox2;
  private CheckBox checkBox1;
  private Panel panelBottom;
  private Button btCancel;
  private Button btApply;
  private Button btSelectComp;
  private System.ComponentModel.Container components;
  public static int ViewIconIndex = -1;

  public FilterConfigView()
  {
    this.InitializeComponent();
    this._filterGuid = Guid.Empty;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      ApplicationServices.Container.GetService<INotificationService>()?.Unsubscribe("FilterChanged", new NotificationEventHandler(this.NotificationEventFired));
      this._filterGuid = Guid.Empty;
      this.DestroyControllers();
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilterConfigView));
    this.panelMain = new Panel();
    this.btSelectComp = new Button();
    this.conditionComboBox13 = new ConditionComboBox();
    this.dateTimePicker2 = new DateTimePicker();
    this.checkBox13 = new CheckBox();
    this.btSelectUser = new Button();
    this.tbUserName = new TextBox();
    this.textBox7 = new TextBox();
    this.conditionComboBox12 = new ConditionComboBox();
    this.checkBox12 = new CheckBox();
    this.tbFilterName = new TextBox();
    this.label1 = new Label();
    this.checkedListBox1 = new CheckedListBox();
    this.textBox6 = new TextBox();
    this.comboBox2 = new ComboBox();
    this.textBox5 = new TextBox();
    this.textBox4 = new TextBox();
    this.textBox3 = new TextBox();
    this.textBox2 = new TextBox();
    this.textBox1 = new TextBox();
    this.dateTimePicker1 = new DateTimePicker();
    this.comboBox1 = new ComboBox();
    this.conditionComboBox11 = new ConditionComboBox();
    this.conditionComboBox10 = new ConditionComboBox();
    this.conditionComboBox9 = new ConditionComboBox();
    this.conditionComboBox8 = new ConditionComboBox();
    this.conditionComboBox7 = new ConditionComboBox();
    this.conditionComboBox6 = new ConditionComboBox();
    this.conditionComboBox5 = new ConditionComboBox();
    this.conditionComboBox4 = new ConditionComboBox();
    this.conditionComboBox3 = new ConditionComboBox();
    this.conditionComboBox2 = new ConditionComboBox();
    this.conditionComboBox1 = new ConditionComboBox();
    this.checkBox11 = new CheckBox();
    this.checkBox10 = new CheckBox();
    this.checkBox9 = new CheckBox();
    this.checkBox8 = new CheckBox();
    this.checkBox7 = new CheckBox();
    this.checkBox5 = new CheckBox();
    this.checkBox6 = new CheckBox();
    this.checkBox4 = new CheckBox();
    this.checkBox3 = new CheckBox();
    this.checkBox2 = new CheckBox();
    this.checkBox1 = new CheckBox();
    this.panelBottom = new Panel();
    this.btCancel = new Button();
    this.btApply = new Button();
    this.panelMain.SuspendLayout();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panelMain, "panelMain");
    this.panelMain.Controls.Add((Control) this.btSelectComp);
    this.panelMain.Controls.Add((Control) this.conditionComboBox13);
    this.panelMain.Controls.Add((Control) this.dateTimePicker2);
    this.panelMain.Controls.Add((Control) this.checkBox13);
    this.panelMain.Controls.Add((Control) this.btSelectUser);
    this.panelMain.Controls.Add((Control) this.tbUserName);
    this.panelMain.Controls.Add((Control) this.textBox7);
    this.panelMain.Controls.Add((Control) this.conditionComboBox12);
    this.panelMain.Controls.Add((Control) this.checkBox12);
    this.panelMain.Controls.Add((Control) this.tbFilterName);
    this.panelMain.Controls.Add((Control) this.label1);
    this.panelMain.Controls.Add((Control) this.checkedListBox1);
    this.panelMain.Controls.Add((Control) this.textBox6);
    this.panelMain.Controls.Add((Control) this.comboBox2);
    this.panelMain.Controls.Add((Control) this.textBox5);
    this.panelMain.Controls.Add((Control) this.textBox4);
    this.panelMain.Controls.Add((Control) this.textBox3);
    this.panelMain.Controls.Add((Control) this.textBox2);
    this.panelMain.Controls.Add((Control) this.textBox1);
    this.panelMain.Controls.Add((Control) this.dateTimePicker1);
    this.panelMain.Controls.Add((Control) this.comboBox1);
    this.panelMain.Controls.Add((Control) this.conditionComboBox11);
    this.panelMain.Controls.Add((Control) this.conditionComboBox10);
    this.panelMain.Controls.Add((Control) this.conditionComboBox9);
    this.panelMain.Controls.Add((Control) this.conditionComboBox8);
    this.panelMain.Controls.Add((Control) this.conditionComboBox7);
    this.panelMain.Controls.Add((Control) this.conditionComboBox6);
    this.panelMain.Controls.Add((Control) this.conditionComboBox5);
    this.panelMain.Controls.Add((Control) this.conditionComboBox4);
    this.panelMain.Controls.Add((Control) this.conditionComboBox3);
    this.panelMain.Controls.Add((Control) this.conditionComboBox2);
    this.panelMain.Controls.Add((Control) this.conditionComboBox1);
    this.panelMain.Controls.Add((Control) this.checkBox11);
    this.panelMain.Controls.Add((Control) this.checkBox10);
    this.panelMain.Controls.Add((Control) this.checkBox9);
    this.panelMain.Controls.Add((Control) this.checkBox8);
    this.panelMain.Controls.Add((Control) this.checkBox7);
    this.panelMain.Controls.Add((Control) this.checkBox5);
    this.panelMain.Controls.Add((Control) this.checkBox6);
    this.panelMain.Controls.Add((Control) this.checkBox4);
    this.panelMain.Controls.Add((Control) this.checkBox3);
    this.panelMain.Controls.Add((Control) this.checkBox2);
    this.panelMain.Controls.Add((Control) this.checkBox1);
    this.panelMain.Name = "panelMain";
    componentResourceManager.ApplyResources((object) this.btSelectComp, "btSelectComp");
    this.btSelectComp.Name = "btSelectComp";
    this.conditionComboBox13.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox13, "conditionComboBox13");
    this.conditionComboBox13.Name = "conditionComboBox13";
    this.conditionComboBox13.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox13.Sorted = true;
    this.conditionComboBox13.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.dateTimePicker2, "dateTimePicker2");
    this.dateTimePicker2.Format = DateTimePickerFormat.Custom;
    this.dateTimePicker2.Name = "dateTimePicker2";
    this.dateTimePicker2.Tag = (object) "1";
    this.dateTimePicker2.Value = new DateTime(2004, 9, 3, 11, 30, 0, 0);
    componentResourceManager.ApplyResources((object) this.checkBox13, "checkBox13");
    this.checkBox13.Name = "checkBox13";
    this.checkBox13.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.btSelectUser, "btSelectUser");
    this.btSelectUser.Name = "btSelectUser";
    componentResourceManager.ApplyResources((object) this.tbUserName, "tbUserName");
    this.tbUserName.Name = "tbUserName";
    this.tbUserName.ReadOnly = true;
    this.tbUserName.Tag = (object) "4";
    componentResourceManager.ApplyResources((object) this.textBox7, "textBox7");
    this.textBox7.Name = "textBox7";
    this.textBox7.Tag = (object) "10";
    this.conditionComboBox12.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox12, "conditionComboBox12");
    this.conditionComboBox12.Name = "conditionComboBox12";
    this.conditionComboBox12.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox12.Sorted = true;
    this.conditionComboBox12.Tag = (object) "11";
    componentResourceManager.ApplyResources((object) this.checkBox12, "checkBox12");
    this.checkBox12.Name = "checkBox12";
    this.checkBox12.Tag = (object) "11";
    componentResourceManager.ApplyResources((object) this.tbFilterName, "tbFilterName");
    this.tbFilterName.Name = "tbFilterName";
    this.tbFilterName.KeyUp += new KeyEventHandler(this.tbFilterName_KeyUp);
    this.tbFilterName.Leave += new EventHandler(this.tbFilterName_Leave);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.checkedListBox1.CheckOnClick = true;
    componentResourceManager.ApplyResources((object) this.checkedListBox1, "checkedListBox1");
    this.checkedListBox1.Name = "checkedListBox1";
    this.checkedListBox1.Sorted = true;
    this.checkedListBox1.Tag = (object) "3";
    componentResourceManager.ApplyResources((object) this.textBox6, "textBox6");
    this.textBox6.Name = "textBox6";
    this.textBox6.Tag = (object) "10";
    this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.comboBox2, "comboBox2");
    this.comboBox2.Name = "comboBox2";
    this.comboBox2.Sorted = true;
    this.comboBox2.Tag = (object) "9";
    componentResourceManager.ApplyResources((object) this.textBox5, "textBox5");
    this.textBox5.Name = "textBox5";
    this.textBox5.Tag = (object) "8";
    componentResourceManager.ApplyResources((object) this.textBox4, "textBox4");
    this.textBox4.Name = "textBox4";
    this.textBox4.Tag = (object) "7";
    componentResourceManager.ApplyResources((object) this.textBox3, "textBox3");
    this.textBox3.Name = "textBox3";
    this.textBox3.Tag = (object) "6";
    componentResourceManager.ApplyResources((object) this.textBox2, "textBox2");
    this.textBox2.Name = "textBox2";
    this.textBox2.Tag = (object) "4";
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.Name = "textBox1";
    this.textBox1.Tag = (object) "2";
    componentResourceManager.ApplyResources((object) this.dateTimePicker1, "dateTimePicker1");
    this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
    this.dateTimePicker1.Name = "dateTimePicker1";
    this.dateTimePicker1.Tag = (object) "1";
    this.dateTimePicker1.Value = new DateTime(2004, 9, 3, 11, 30, 0, 0);
    this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.comboBox1, "comboBox1");
    this.comboBox1.Name = "comboBox1";
    this.comboBox1.Sorted = true;
    this.comboBox1.Tag = (object) "0";
    this.conditionComboBox11.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox11, "conditionComboBox11");
    this.conditionComboBox11.Name = "conditionComboBox11";
    this.conditionComboBox11.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox11.Sorted = true;
    this.conditionComboBox11.Tag = (object) "10";
    this.conditionComboBox10.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox10, "conditionComboBox10");
    this.conditionComboBox10.Name = "conditionComboBox10";
    this.conditionComboBox10.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox10.Sorted = true;
    this.conditionComboBox10.Tag = (object) "9";
    this.conditionComboBox9.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox9, "conditionComboBox9");
    this.conditionComboBox9.Name = "conditionComboBox9";
    this.conditionComboBox9.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox9.Sorted = true;
    this.conditionComboBox9.Tag = (object) "8";
    this.conditionComboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox8, "conditionComboBox8");
    this.conditionComboBox8.Name = "conditionComboBox8";
    this.conditionComboBox8.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox8.Sorted = true;
    this.conditionComboBox8.Tag = (object) "7";
    this.conditionComboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox7, "conditionComboBox7");
    this.conditionComboBox7.Name = "conditionComboBox7";
    this.conditionComboBox7.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox7.Sorted = true;
    this.conditionComboBox7.Tag = (object) "6";
    this.conditionComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox6, "conditionComboBox6");
    this.conditionComboBox6.Name = "conditionComboBox6";
    this.conditionComboBox6.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox6.Sorted = true;
    this.conditionComboBox6.Tag = (object) "5";
    this.conditionComboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox5, "conditionComboBox5");
    this.conditionComboBox5.Name = "conditionComboBox5";
    this.conditionComboBox5.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox5.Sorted = true;
    this.conditionComboBox5.Tag = (object) "4";
    this.conditionComboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox4, "conditionComboBox4");
    this.conditionComboBox4.Name = "conditionComboBox4";
    this.conditionComboBox4.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox4.Sorted = true;
    this.conditionComboBox4.Tag = (object) "3";
    this.conditionComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox3, "conditionComboBox3");
    this.conditionComboBox3.Name = "conditionComboBox3";
    this.conditionComboBox3.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox3.Sorted = true;
    this.conditionComboBox3.Tag = (object) "2";
    this.conditionComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox2, "conditionComboBox2");
    this.conditionComboBox2.Name = "conditionComboBox2";
    this.conditionComboBox2.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox2.Sorted = true;
    this.conditionComboBox2.Tag = (object) "1";
    this.conditionComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
    componentResourceManager.ApplyResources((object) this.conditionComboBox1, "conditionComboBox1");
    this.conditionComboBox1.Name = "conditionComboBox1";
    this.conditionComboBox1.SelectedCondition = FlagsConditions.NONE;
    this.conditionComboBox1.Sorted = true;
    this.conditionComboBox1.Tag = (object) "0";
    componentResourceManager.ApplyResources((object) this.checkBox11, "checkBox11");
    this.checkBox11.Name = "checkBox11";
    this.checkBox11.Tag = (object) "10";
    componentResourceManager.ApplyResources((object) this.checkBox10, "checkBox10");
    this.checkBox10.Name = "checkBox10";
    this.checkBox10.Tag = (object) "9";
    componentResourceManager.ApplyResources((object) this.checkBox9, "checkBox9");
    this.checkBox9.Name = "checkBox9";
    this.checkBox9.Tag = (object) "8";
    componentResourceManager.ApplyResources((object) this.checkBox8, "checkBox8");
    this.checkBox8.Name = "checkBox8";
    this.checkBox8.Tag = (object) "7";
    componentResourceManager.ApplyResources((object) this.checkBox7, "checkBox7");
    this.checkBox7.Name = "checkBox7";
    this.checkBox7.Tag = (object) "6";
    componentResourceManager.ApplyResources((object) this.checkBox5, "checkBox5");
    this.checkBox5.Name = "checkBox5";
    this.checkBox5.Tag = (object) "4";
    componentResourceManager.ApplyResources((object) this.checkBox6, "checkBox6");
    this.checkBox6.Name = "checkBox6";
    this.checkBox6.Tag = (object) "5";
    componentResourceManager.ApplyResources((object) this.checkBox4, "checkBox4");
    this.checkBox4.Name = "checkBox4";
    this.checkBox4.Tag = (object) "3";
    componentResourceManager.ApplyResources((object) this.checkBox3, "checkBox3");
    this.checkBox3.Name = "checkBox3";
    this.checkBox3.Tag = (object) "2";
    componentResourceManager.ApplyResources((object) this.checkBox2, "checkBox2");
    this.checkBox2.Name = "checkBox2";
    this.checkBox2.Tag = (object) "1";
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.Tag = (object) "0";
    this.panelBottom.Controls.Add((Control) this.btCancel);
    this.panelBottom.Controls.Add((Control) this.btApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.Name = "btCancel";
    this.btCancel.Click += new EventHandler(this.btCancel_Click);
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    this.btApply.Name = "btApply";
    this.btApply.Click += new EventHandler(this.btApply_Click);
    this.Controls.Add((Control) this.panelMain);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (FilterConfigView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.panelMain.ResumeLayout(false);
    this.panelMain.PerformLayout();
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    if (FilterConfigView.ViewIconIndex == -1)
      FilterConfigView.ViewIconIndex = Holder.NamedImageList.ImageIndex("imgEventLogFilterIcon");
    this._filterGuid = ((IFilterGuid) items.GetItemData(0, typeof (IFilterGuid))).Value;
    this._filter = (Filter) null;
    this.tbFilterName.Text = "";
    this.tbFilterName.Enabled = false;
    this.tbFilterName.ReadOnly = true;
    INotificationService service = ApplicationServices.Container.GetService<INotificationService>();
    if (service == null)
      return;
    service.Unsubscribe("FilterChanged", new NotificationEventHandler(this.NotificationEventFired));
    service.Subscribe("FilterChanged", new NotificationEventHandler(this.NotificationEventFired));
  }

  public void Activate(IView previousView)
  {
    if (this.comboBox1.Items.Count == 0)
    {
      object[] keys = Services.AuditTypes.GetKeys();
      for (int index = 0; index < keys.Length; ++index)
        this.comboBox1.Items.Add((object) new IdTextItem((int) keys[index], (string) Services.AuditTypes[keys[index]]));
    }
    if (this.comboBox2.Items.Count == 0)
    {
      object[] keys = Services.EventCategories.GetKeys();
      for (int index = 0; index < keys.Length; ++index)
        this.comboBox2.Items.Add((object) new IdTextItem((int) keys[index], (string) Services.EventCategories[keys[index]]));
    }
    if (this.checkedListBox1.Items.Count == 0)
    {
      object[] keys = Services.EventTypes.GetKeys();
      for (int index = 0; index < keys.Length; ++index)
        this.checkedListBox1.Items.Add((object) new IdTextItem((int) keys[index], (string) Services.EventTypes[keys[index]]));
    }
    if (this._filter != null)
      return;
    this.LoadData();
  }

  public void Deactivate(IView nextView) => this.SaveOnModify();

  public string Caption => FilterConfigView.FilterConfigViewName;

  public int OrderID => 10;

  public int ImageIndex => FilterConfigView.ViewIconIndex;

  private void CreateControllers()
  {
    this.SuspendLayout();
    try
    {
      this._controllers = new FilterItemController[13]
      {
        (FilterItemController) new TextBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_EVENT_ID), this.checkBox3, this.conditionComboBox3, this.textBox1),
        (FilterItemController) new TextBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_OBJECT_NAME), this.checkBox5, this.conditionComboBox5, this.textBox2),
        (FilterItemController) new TextBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_OBJECT_ID), this.checkBox7, this.conditionComboBox7, this.textBox3),
        (FilterItemController) new TextBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_RELATION_ID), this.checkBox8, this.conditionComboBox8, this.textBox4),
        (FilterItemController) new TextBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_NOTE), this.checkBox9, this.conditionComboBox9, this.textBox5),
        (FilterItemController) new TextBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_CATEGORY_ID), this.checkBox11, this.conditionComboBox11, this.textBox6),
        (FilterItemController) new ComputerNameItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_COMPUTER_NAME), this.checkBox12, this.conditionComboBox12, this.textBox7, this.btSelectComp),
        (FilterItemController) new UserNameItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_USER_ID), this.checkBox6, this.conditionComboBox6, this.tbUserName, this.btSelectUser),
        (FilterItemController) new DateTimePickerItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_BEGIN_DATE), this.checkBox2, this.conditionComboBox2, this.dateTimePicker1),
        (FilterItemController) new DateTimePickerItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_END_DATE), this.checkBox13, this.conditionComboBox13, this.dateTimePicker2),
        (FilterItemController) new ComboBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_AUDIT_TYPE), this.checkBox1, this.conditionComboBox1, this.comboBox1),
        (FilterItemController) new ComboBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_CATEGORY_TYPE), this.checkBox10, this.conditionComboBox10, this.comboBox2),
        (FilterItemController) new CheckedListBoxItemController(this._filter.FindItem(ObligatoryObjectAttributes.F_EVENT_TYPE), this.checkBox4, this.conditionComboBox4, this.checkedListBox1)
      };
      for (int index = 0; index < this._controllers.Length; ++index)
      {
        this._controllers[index].ItemChanged += new EventHandler(this.FilterItemChanged);
        this._controllers[index].Initialize();
      }
    }
    finally
    {
      this.ResumeLayout();
    }
  }

  private void DestroyControllers()
  {
    if (this._controllers != null)
    {
      for (int index = 0; index < this._controllers.Length; ++index)
      {
        this._controllers[index].ItemChanged -= new EventHandler(this.FilterItemChanged);
        this._controllers[index].Uninitialize();
      }
    }
    this._controllers = (FilterItemController[]) null;
  }

  private void SetFilterName()
  {
    try
    {
      this._filter.Name = this.tbFilterName.Text;
      this.SetModifiedState(true);
    }
    catch (Exception ex)
    {
      this.tbFilterName.Text = this._filter.Name;
      int num = (int) MessageBox.Show(ex.Message);
    }
  }

  private void LoadData()
  {
    if (this._filterGuid == Guid.Empty && this._filter != null)
      this._filterGuid = this._filter.Guid;
    Filter filter = FiltersManager.Filters.FindFilter(this._filterGuid);
    if (filter == null)
    {
      this.DestroyControllers();
      this.SetModifiedState(false);
    }
    else
    {
      this._filter = filter.Clone() as Filter;
      this.tbFilterName.Text = this._filter.Name;
      this.tbFilterName.Enabled = true;
      this.tbFilterName.ReadOnly = false;
      this.DestroyControllers();
      this.CreateControllers();
      this.SetModifiedState(false);
    }
  }

  private void SaveData()
  {
    if (this._filterGuid == Guid.Empty && this._filter != null)
      this._filterGuid = this._filter.Guid;
    if (FiltersManager.Filters.FindFilter(this._filterGuid) == null)
    {
      this.SetModifiedState(false);
    }
    else
    {
      FiltersManager.Filters.FindFilter(this._filterGuid).Assign(this._filter);
      FiltersManager.Flush();
      this.SetModifiedState(false);
      ApplicationServices.Container.GetService<INotificationService>()?.FireEvent((object) this, (NotificationEventArgs) new FilterEventArgs("FilterChanged", false, this._filter.Guid));
    }
  }

  private void SaveOnModify()
  {
    if (this._filterGuid == Guid.Empty && this._filter != null)
      this._filterGuid = this._filter.Guid;
    if (FiltersManager.Filters.FindFilter(this._filterGuid) == null)
    {
      this.SetModifiedState(false);
    }
    else
    {
      if (this._filter == null || !this.btApply.Enabled)
        return;
      if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_102"), LocalizationHolder.rm.GetString("DatabaseConfigurator_103"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.SaveData();
      else
        this.LoadData();
    }
  }

  private void SetModifiedState(bool modified)
  {
    this.tbFilterName.Modified = modified;
    this.btApply.Enabled = modified;
    this.btCancel.Enabled = modified;
    FiltersManager.Filters.Modified = modified;
  }

  private void FilterItemChanged(object sender, EventArgs e) => this.SetModifiedState(true);

  private void tbFilterName_KeyUp(object sender, KeyEventArgs e)
  {
    if (!this.tbFilterName.Modified || e.KeyCode != Keys.Return)
      return;
    this.SetFilterName();
  }

  private void tbFilterName_Leave(object sender, EventArgs e)
  {
    if (!this.tbFilterName.Modified)
      return;
    this.SetFilterName();
  }

  private void btCancel_Click(object sender, EventArgs e) => this.LoadData();

  private void btApply_Click(object sender, EventArgs e) => this.SaveData();

  public void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (sender == this || !(e.EventName == "FilterChanged") || !(e is FilterEventArgs filterEventArgs) || this._filter == null || !(this._filter.Guid == filterEventArgs.FilterGuid))
      return;
    this.LoadData();
  }
}
