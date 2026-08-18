
// Type: Intermech.Client.Core.FormBaseFind
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Configuration;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Client.Core;

/// <summary> База диалога для поиска чего-либо </summary>
public class FormBaseFind : Form, IFindController, IFindDialog
{
  private static readonly string locationTag = "location";
  private IWindowWithFind _iWindowWithFind;
  private bool _loaded;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public FormBaseFind()
  {
    this.InitializeComponent();
    this.TopMost = true;
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
  }

  /// <summary> Ссылка на окно, в содержимом которого должен производиться поиск </summary>
  private IWindowWithFind IWindowWithFind
  {
    get => this._iWindowWithFind;
    set => this._iWindowWithFind = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public new Size ClientSize
  {
    get => base.ClientSize;
    set => base.ClientSize = value;
  }

  /// <summary>
  /// Получение ссылки на объект, который реализует всю функциональность по настройке поиска
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public virtual object InterfaceObject => (object) this;

  /// <summary> Вызывается когда форма настройки поиска "присоединяется" к окну, в содержимом которого должен производиться поиск </summary>
  /// <param name="iWindowWithFind"> Окно, в содержимом которого должен производиться поиск </param>
  public void AttachToWindow(IWindowWithFind iWindowWithFind)
  {
    this.IWindowWithFind = iWindowWithFind;
    this.AfterConnectedToView(iWindowWithFind);
  }

  /// <summary> Показать пользователю форму настройки поиска </summary>
  public new void Show()
  {
    if (!this.Visible)
    {
      this.Visible = true;
      this.Size = new Size(this.Size.Width + 25, this.Size.Height);
    }
    else
    {
      this.Activate();
      this.BringToFront();
    }
    base.Show();
    this.AfterShow();
  }

  /// <summary> Скрыть форму настройки поиска </summary>
  public new void Hide() => this.Close();

  /// <summary> Сохранить выбранные пользователем настройки поиска для последующего востановления </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  public virtual void SaveConfiguration(IConfiguration iConfiguration)
  {
    iConfiguration.SetProperty(FormBaseFind.locationTag, (string) TypeDescriptor.GetConverter(typeof (Point)).ConvertTo((object) this.Location, typeof (string)));
  }

  /// <summary> Востановление настроек поиска из ранее сохнанённых </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  public virtual void LoadConfiguration(IConfiguration iConfiguration)
  {
    string empty = string.Empty;
    if (!iConfiguration.HasProperty(FormBaseFind.locationTag))
      return;
    string property = iConfiguration.GetProperty(FormBaseFind.locationTag);
    if (!(property != string.Empty))
      return;
    this.Location = (Point) TypeDescriptor.GetConverter(typeof (Point)).ConvertFrom((object) property);
  }

  /// <summary> Признак того, что форма настройки поиска видна пользователю </summary>
  public bool IsVisible => this.Visible;

  /// <summary> </summary>
  protected virtual void AfterConnectedToView(IWindowWithFind iWindowWithFind)
  {
  }

  /// <summary> </summary>
  protected virtual void AfterShow()
  {
  }

  /// <summary> Форма была закрыта </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FormBaseFind_Closed(object sender, EventArgs e)
  {
    FindOrReplaceService.SaveFindWindowConfig();
    FindOrReplaceService.IsFindWindowVisible = false;
  }

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

  /// <summary> Была нажата кнопка </summary>
  private void FormBaseFind_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\u001B')
      return;
    this.Hide();
  }

  private void FormBaseFind_Load(object sender, EventArgs e)
  {
    if (this._loaded)
      return;
    this.RestoreLoadParams();
    this._loaded = true;
  }

  protected void RestoreLoadParams()
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control is IPlaceable)
        ((IPlaceable) control).PlaceControls();
    }
  }

  public Point GetScreenCoords() => this.DesktopLocation;

  public void SetScreenCoords(Point point) => this.DesktopLocation = point;

  public Size GetSize() => this.Size;

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormBaseFind));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AccessibleRole = AccessibleRole.None;
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (FormBaseFind);
    this.Closed += new EventHandler(this.FormBaseFind_Closed);
    this.Load += new EventHandler(this.FormBaseFind_Load);
    this.KeyPress += new KeyPressEventHandler(this.FormBaseFind_KeyPress);
    this.ResumeLayout(false);
  }
}
