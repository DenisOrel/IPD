
// Type: Intermech.Client.Core.DateTimePopupControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.NavBars;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Компонент выбора даты</summary>
public class DateTimePopupControl : BasePopupControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected MonthCalendar calendar;
  private HeaderControl headerControl;
  private MenuBar menuBar;
  private ContextMenuBarItem contextMenuBarItem;
  private MenuButtonItem mnpAddCriterion;
  private MenuButtonItem mnpDeleteCriterion;
  private MenuButtonItem mnpAddValue;
  private MenuButtonItem mnpDelValue;
  private MenuButtonItem mnpMoveUp;
  private MenuButtonItem mnpMoveDown;
  private Button btnCancel;

  /// <summary>Создать экземпляр класса</summary>
  public DateTimePopupControl()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  /// <summary>Редактируемое значение (дата)</summary>
  public override object Value
  {
    [DebuggerStepThrough] get => base.Value;
    set
    {
      if (value is DateTime date)
        this.calendar.SetDate(date);
      else
        this.calendar.SetDate(DateTime.Now);
      base.Value = (object) this.calendar.SelectionStart;
    }
  }

  /// <summary>
  /// Отобразить элемент управления в указанной точке, с указанными размерами
  /// </summary>
  /// <param name="location">Положение левого верхнего угла компонента</param>
  /// <param name="size">Размеры компонента</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="value">Редактируемое значение</param>
  /// <returns>Результат вызова элемента управления</returns>
  public override DialogResult Execute(
    Point location,
    Size size,
    System.IServiceProvider services,
    object value)
  {
    try
    {
      if (value is DateTime date)
        this.calendar.SetDate(date);
      else
        this.calendar.SetDate(DateTime.Now);
      int num = (int) base.Execute(location, size, services, (object) this.calendar.SelectionStart);
    }
    finally
    {
      if (this.DialogResult == DialogResult.OK)
        this.Value = (object) this.calendar.SelectionStart;
    }
    return this.DialogResult;
  }

  /// <summary>Выделена дата</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void calendar_DateSelected(object sender, DateRangeEventArgs e)
  {
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Нажата кнопка "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCancel(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DateTimePopupControl));
    this.calendar = new MonthCalendar();
    this.headerControl = new HeaderControl();
    this.btnCancel = new Button();
    this.menuBar = new MenuBar();
    this.contextMenuBarItem = new ContextMenuBarItem();
    this.mnpAddCriterion = new MenuButtonItem();
    this.mnpDeleteCriterion = new MenuButtonItem();
    this.mnpAddValue = new MenuButtonItem();
    this.mnpDelValue = new MenuButtonItem();
    this.mnpMoveUp = new MenuButtonItem();
    this.mnpMoveDown = new MenuButtonItem();
    this.headerControl.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.calendar, "calendar");
    this.calendar.Name = "calendar";
    this.calendar.DateSelected += new DateRangeEventHandler(this.calendar_DateSelected);
    this.headerControl.BackColor = SystemColors.Control;
    this.headerControl.Controls.Add((Control) this.btnCancel);
    this.headerControl.Controls.Add((Control) this.menuBar);
    componentResourceManager.ApplyResources((object) this.headerControl, "headerControl");
    this.headerControl.ForeColor = SystemColors.ControlText;
    this.headerControl.HeaderFont = new Font("Tahoma", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.headerControl.Name = "headerControl";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.TabStop = false;
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.DoCancel);
    componentResourceManager.ApplyResources((object) this.menuBar, "menuBar");
    this.menuBar.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuBar.Hidden = false;
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem
    });
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuBarItem, "contextMenuBarItem");
    this.contextMenuBarItem.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpAddCriterion,
      (ToolbarItemBase) this.mnpDeleteCriterion,
      (ToolbarItemBase) this.mnpAddValue,
      (ToolbarItemBase) this.mnpDelValue,
      (ToolbarItemBase) this.mnpMoveUp,
      (ToolbarItemBase) this.mnpMoveDown
    });
    this.contextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpAddCriterion, "mnpAddCriterion");
    this.mnpAddCriterion.ImageIndex = 0;
    this.mnpAddCriterion.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpDeleteCriterion, "mnpDeleteCriterion");
    this.mnpDeleteCriterion.ImageIndex = 1;
    this.mnpDeleteCriterion.ShowText = true;
    this.mnpAddValue.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpAddValue, "mnpAddValue");
    this.mnpAddValue.ImageIndex = 2;
    this.mnpAddValue.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpDelValue, "mnpDelValue");
    this.mnpDelValue.ImageIndex = 3;
    this.mnpDelValue.ShowText = true;
    this.mnpMoveUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveUp, "mnpMoveUp");
    this.mnpMoveUp.ImageIndex = 4;
    this.mnpMoveUp.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveDown, "mnpMoveDown");
    this.mnpMoveDown.ImageIndex = 5;
    this.mnpMoveDown.ShowText = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.headerControl);
    this.Controls.Add((Control) this.calendar);
    this.Name = nameof (DateTimePopupControl);
    this.headerControl.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
