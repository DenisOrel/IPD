
// Type: Intermech.Client.Core.FormDesigner.Controls.IMTabControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class IMTabControl : TabControl, IFormDesignerControl, IIMControlEnabled
{
  private EventHandler _formDeactivate;
  private EventHandler _loadDataCompleted;
  private IFormDesignerControl _parent;
  /// <summary>
  /// Может быть подписка и на LoadDataCompleted и на FormDeactivate,
  /// поэтому, чтобы не подписываться 2 раза на событие изменения родителя и закладки (если нужно), выставляем этот флаг при первом подписании.
  /// </summary>
  private bool _isSubscribeOnTabPageParentChanged;
  private bool _enabled = true;

  /// <summary>
  /// Перекрыто для того, чтобы не сериализовать это свойство.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override bool AllowDrop
  {
    get => base.AllowDrop;
    set => base.AllowDrop = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  /// <summary>Конструктор.</summary>
  public IMTabControl()
  {
    this.Name = string.Empty;
    this.DoubleBuffered = true;
    this.CanContainsChildren = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnFormDeactivate(object sender, EventArgs e)
  {
    if (this._formDeactivate == null)
      return;
    this._formDeactivate((object) this, EventArgs.Empty);
  }

  /// <summary>Закончена загрузка данных в контролы.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnLoadDataCompleted(object sender, EventArgs e)
  {
    if (this._loadDataCompleted == null)
      return;
    this._loadDataCompleted((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTabPage_ParentChanged(object sender, EventArgs e)
  {
    if (!(sender is TabPage))
      return;
    this.Unsubscribe();
    this.SubscribeLoadData(this.Parent);
    this.SubscribeFormDeactivate(this.Parent);
  }

  /// <summary>Событие, возникающее при деактивации вьюшки.</summary>
  /// <remark>Событие исходит от формы.
  /// Но на событие должны давать возможность подписываться только контролы, которые могут быть контейнерами контролов.
  /// Необходимость возникла из-за случая, когда во время деактивации вьюшки нужно провести деактивацию контрола.
  /// Поэтому, если контрол лежит на форме, то он получает сообщение от самой формы, а если контрол лежит на другом контроле,
  /// то он получает сообщение от родителя, а родитель в итоге от формы.</remark>
  public event EventHandler FormDeactivate
  {
    add
    {
      if (!this.CanContainsChildren)
        return;
      int num = this._formDeactivate != null ? 0 : (value != null ? 1 : 0);
      this._formDeactivate += value;
      if (num == 0)
        return;
      this.SubscribeFormDeactivate(this.Parent);
    }
    remove
    {
      if (!this.CanContainsChildren)
        return;
      this._formDeactivate -= value;
      if (this._formDeactivate != null)
        return;
      this.Unsubscribe();
    }
  }

  /// <summary>Событие, возникающее после загрузки контролов.</summary>
  /// <remark>Событие исходит от формы.
  /// Но на событие должны давать возможность подписываться только контролы, которые могут быть контейнерами контролов.
  /// Необходимость возникла из-за случая, когда форма должна сообщить дочерним контролам, что данные все загружены.
  /// Поэтому, если контрол лежит на форме, то он получает сообщение от самой формы, а если контрол лежит на другом контроле,
  /// то он получает сообщение от родителя, а родитель в конечном итоге от формы.</remark>
  public event EventHandler LoadDataCompleted
  {
    add
    {
      if (!this.CanContainsChildren)
        return;
      int num = this._loadDataCompleted != null ? 0 : (value != null ? 1 : 0);
      this._loadDataCompleted += value;
      if (num == 0)
        return;
      this.SubscribeLoadData(this.Parent);
    }
    remove
    {
      if (!this.CanContainsChildren)
        return;
      this._loadDataCompleted -= value;
      if (this._loadDataCompleted != null)
        return;
      this.Unsubscribe();
    }
  }

  /// <summary>Возможность контрола иметь дочерние контролы.</summary>
  [Browsable(false)]
  public bool CanContainsChildren { get; private set; }

  /// <summary>Запретить редактирование данных.</summary>
  [Browsable(false)]
  [DefaultValue(false)]
  public bool DisabledInDesign { get; set; }

  /// <summary>Доступность контрола.</summary>
  [Browsable(false)]
  [DefaultValue(true)]
  public bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      foreach (TabPage tabPage in this.TabPages)
      {
        if (tabPage.Controls != null)
        {
          foreach (Control control in (ArrangedElementCollection) tabPage.Controls)
          {
            if (control is IIMControlEnabled imControlEnabled)
              imControlEnabled.EnabledCtrl = value;
            else
              control.Enabled = value;
          }
        }
      }
      this._enabled = value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnParentChanged(EventArgs e)
  {
    base.OnParentChanged(e);
    this.Unsubscribe();
    this.SubscribeLoadData(this.Parent);
    this.SubscribeFormDeactivate(this.Parent);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.Unsubscribe();
    base.Dispose(disposing);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="parent"></param>
  private void SubscribeFormDeactivate(Control parent)
  {
    switch (parent)
    {
      case IFormDesignerControl formDesignerControl:
        this._parent = this._parent ?? formDesignerControl;
        this._parent.FormDeactivate += new EventHandler(this.OnFormDeactivate);
        break;
      case TabPage tabPage:
        if (tabPage.Parent == null)
        {
          if (this._isSubscribeOnTabPageParentChanged)
            break;
          tabPage.ParentChanged += new EventHandler(this.OnTabPage_ParentChanged);
          this._isSubscribeOnTabPageParentChanged = true;
          break;
        }
        this.SubscribeFormDeactivate(tabPage.Parent);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="parent"></param>
  private void SubscribeLoadData(Control parent)
  {
    switch (parent)
    {
      case IFormDesignerControl formDesignerControl:
        this._parent = this._parent ?? formDesignerControl;
        this._parent.LoadDataCompleted += new EventHandler(this.OnLoadDataCompleted);
        break;
      case TabPage tabPage:
        if (tabPage.Parent == null)
        {
          if (this._isSubscribeOnTabPageParentChanged)
            break;
          tabPage.ParentChanged += new EventHandler(this.OnTabPage_ParentChanged);
          this._isSubscribeOnTabPageParentChanged = true;
          break;
        }
        this.SubscribeLoadData(tabPage.Parent);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void Unsubscribe()
  {
    if (this._parent == null)
      return;
    this._parent.LoadDataCompleted -= new EventHandler(this.OnLoadDataCompleted);
    this._parent.FormDeactivate -= new EventHandler(this.OnFormDeactivate);
    this._parent = (IFormDesignerControl) null;
    this._isSubscribeOnTabPageParentChanged = false;
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont()
  {
    return this.Parent != null && !this.Parent.Font.Equals((object) this.Font);
  }
}
