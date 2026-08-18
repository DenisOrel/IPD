
// Type: Intermech.Client.Core.BasePopupControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Базовый компонент для организации "всплывающих" редакторов (например,
/// вызываемых по нажатию кнопок в inplace-редакторах гридов, т.п.)
/// </summary>
public class BasePopupControl : Form
{
  /// <summary>Контейнер сервисов</summary>
  protected System.IServiceProvider services;
  /// <summary>Редактируемое значение</summary>
  protected object value;
  /// <summary>Компоненты</summary>
  private IContainer components;

  /// <summary>Создать экземпляр класса</summary>
  public BasePopupControl()
  {
    this.InitializeComponent();
    this.UpdateControls();
  }

  /// <summary>Контейнер сервисов</summary>
  public virtual System.IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
    set => this.services = value;
  }

  /// <summary>Редактируемое значение</summary>
  public virtual object Value
  {
    [DebuggerStepThrough] get => this.value;
    set => this.value = value;
  }

  /// <summary>Обновить состояние элементов компонента</summary>
  public virtual void UpdateControls()
  {
  }

  /// <summary>Компонент деактивирован</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void LeaveOrDeactivate(object sender, EventArgs e)
  {
    if (this.DialogResult == DialogResult.OK)
      return;
    this.DialogResult = DialogResult.Cancel;
  }

  /// <summary>Нажата клавиша</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void DoKeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Escape)
    {
      this.DialogResult = DialogResult.Cancel;
    }
    else
    {
      if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Return)
        return;
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>Компонент первый раз отображается на экране</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void BeforeShow(object sender, EventArgs e)
  {
    Rectangle workingArea = Screen.GetWorkingArea((Control) this);
    Rectangle bounds = this.Bounds;
    int num1 = 0;
    int num2 = 0;
    if (bounds.Left < workingArea.Left)
      num1 = workingArea.Left - bounds.Left;
    else if (bounds.Right > workingArea.Right)
      num1 = workingArea.Right - bounds.Right;
    if (bounds.Top < workingArea.Top)
      num2 = workingArea.Top - bounds.Top;
    else if (bounds.Bottom > workingArea.Bottom)
      num2 = workingArea.Bottom - bounds.Bottom;
    if (num1 == 0 && num2 == 0)
      return;
    this.Location = new Point(this.Location.X + num1, this.Location.Y + num2);
  }

  /// <summary>
  /// Отобразить элемент управления в указанной точке, с указанными размерами
  /// </summary>
  /// <param name="location">Положение левого верхнего угла компонента</param>
  /// <param name="size">Размеры компонента</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="value">Редактируемое значение</param>
  /// <returns>Результат вызова элемента управления</returns>
  public virtual DialogResult Execute(
    Point location,
    Size size,
    System.IServiceProvider services,
    object value)
  {
    this.Location = location;
    if (!size.IsEmpty)
      this.Size = size;
    this.Services = services;
    this.Value = value;
    try
    {
      this.DialogResult = DialogResult.None;
      this.Show();
      this.SetTopLevel(true);
      this.Focus();
      while (this.DialogResult == DialogResult.None)
      {
        if (!this.IsDisposed)
        {
          Application.DoEvents();
          Thread.Sleep(1);
        }
        else
          break;
      }
    }
    finally
    {
      if (!this.IsDisposed)
        this.Hide();
    }
    return this.DialogResult;
  }

  /// <summary>Оконная процедура</summary>
  /// <param name="m">Событие</param>
  protected override void WndProc(ref Message m)
  {
    base.WndProc(ref m);
    if (m.Msg != 132 || (int) m.Result != 1)
      return;
    m.Result = new IntPtr(2);
  }

  /// <summary>Освободить ресурсы</summary>
  /// <param name="disposing">true - требуется освободить управляемые ресурсы</param>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BasePopupControl));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.ControlBox = false;
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (BasePopupControl);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Deactivate += new EventHandler(this.LeaveOrDeactivate);
    this.Shown += new EventHandler(this.BeforeShow);
    this.KeyDown += new KeyEventHandler(this.DoKeyDown);
    this.Leave += new EventHandler(this.LeaveOrDeactivate);
    this.ResumeLayout(false);
  }
}
