// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ExtUserControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Расширеный User ControlPages</summary>
public class ExtUserControl : UserControl, IStructualControlSupport
{
  private bool _readOnly;
  private AutoUpdateControlHelper _AutoUpdateControlHelper;
  private IStructualControl structualControlIntf;
  private EventHandler _OnChangedEvent;
  protected InitDataEventHandler _OnInitDataEvent;

  /// <summary> Очистка использовавшихся ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (this.structualControlIntf != null)
      this.structualControlIntf = (IStructualControl) null;
    if (this._AutoUpdateControlHelper != null)
    {
      this._AutoUpdateControlHelper.Dispose();
      this._AutoUpdateControlHelper = (AutoUpdateControlHelper) null;
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent() => this.Name = nameof (ExtUserControl);

  /// <summary> Обновление визуальных контролов </summary>
  protected virtual void UpdateControls()
  {
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public virtual bool GetIsReadOnly() => false;

  /// <summary>
  /// Вызывается, когда требуется проверка перед попыткой модификации данных
  /// Например, когда у пользователя необходимо запросить разрешение на взятие
  /// на редактирование некоторого объекта
  /// </summary>
  /// <returns> true если редактирование разрешено </returns>
  protected virtual bool BeforeObjectEditBegin(ref bool wasUpdated) => true;

  /// <summary>
  /// Расстояние от правого края кнопки Отмена до правой границы контрола
  /// </summary>
  public virtual int CancelButtonRightEdge => -1;

  /// <summary> Блокирование обновления визуальных контролов </summary>
  public void LockControls()
  {
    if (this.structualControlIntf == null)
      return;
    this.structualControlIntf.LockControls();
  }

  /// <summary> Разблокирование обновления визуальных контролов </summary>
  public void UnlockControls()
  {
    if (this.structualControlIntf == null)
      return;
    this.structualControlIntf.UnlockControls();
  }

  /// <summary> Разблокирование обновления визуальных контролов </summary>
  public void UnlockControls(bool notUpdate)
  {
    if (this.structualControlIntf == null)
      return;
    this.structualControlIntf.UnlockControls(notUpdate);
  }

  /// <summary> Проверка, заблокировано обновление визуальных контролов </summary>
  public bool IsControlsLocked()
  {
    return this.structualControlIntf != null && this.structualControlIntf.IsControlsLocked();
  }

  /// <summary> Обновление визуального состояния контролов </summary>
  /// <param name="recurce">Обновлять так же все дочерние контролы с "помошниками"</param>
  public virtual void UpdateControls(bool recurce)
  {
    if (this.structualControlIntf == null)
      return;
    this.structualControlIntf.UpdateControls(recurce);
  }

  /// <summary> Признак того, что контролы в данный момент обновляются </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(false)]
  public bool ControlsAreUpdating
  {
    get => this.structualControlIntf != null && this.structualControlIntf.ControlsAreUpdating;
  }

  /// <summary> Доступно ли редактирование </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(false)]
  public bool ReadOnly
  {
    get
    {
      if (this.structualControlIntf != null)
        return this.structualControlIntf.ReadOnly;
      IStructualControlSupport structualControlSupport = (IStructualControlSupport) this;
      return structualControlSupport != null ? structualControlSupport.IsReadOnly() : this._readOnly;
    }
    set
    {
      if (this.structualControlIntf != null)
        this.structualControlIntf.ReadOnly = value;
      else
        this._readOnly = value;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(false)]
  public bool OverrideReadOnly
  {
    get
    {
      return this.structualControlIntf == null ? ((IStructualControlSupport) this).IsReadOnly() : this.structualControlIntf.OverrideReadOnly;
    }
    set
    {
      if (this.structualControlIntf == null)
        return;
      this.structualControlIntf.OverrideReadOnly = value;
    }
  }

  /// <summary> Обновить значение параметра "Доступно ли редактирование" </summary>
  public void RefreshReadOnly()
  {
    if (this.structualControlIntf == null)
      return;
    this.structualControlIntf.RefreshReadOnly();
  }

  /// <summary>
  /// Должен вызываться при каждой попытке редактирования.
  /// Проверяет доступно ли редактирование данных и, если требуется,
  /// запрашивает у пользователя разрешение на их редактирование
  /// (например, на взятие на изменение соотв. объекта)
  /// </summary>
  public bool CheckCanEdit(ref bool wasUpdated)
  {
    return this.structualControlIntf != null && this.structualControlIntf.CheckCanEdit(ref wasUpdated);
  }

  /// <summary> Признак того, что данные, связаные с контролом были изменены </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(false)]
  public bool Changed
  {
    get => this.structualControlIntf != null && this.structualControlIntf.Changed;
    set
    {
      if (this.structualControlIntf == null)
        return;
      this.structualControlIntf.Changed = value;
    }
  }

  /// <summary> Событие, вызываемое при изменении данных, связанных с контролом </summary>
  public event EventHandler OnChangedEvent
  {
    add
    {
      if (this.structualControlIntf != null)
        this.structualControlIntf.OnChangedEvent += value;
      else
        this._OnChangedEvent += value;
    }
    remove
    {
      if (this.structualControlIntf != null)
        this.structualControlIntf.OnChangedEvent -= value;
      else
        this._OnChangedEvent -= value;
    }
  }

  /// <summary> Событие, вызываемое при изменении данных, связанных с контролом </summary>
  public event InitDataEventHandler OnInitDataEvent
  {
    add => ((IStructualControlSupport) this).OnInitDataEvent += value;
    remove => ((IStructualControlSupport) this).OnInitDataEvent -= value;
  }

  /// <summary>
  /// Вызов события OnChangedEvent (Изменены параметры схемы нумерации)
  /// </summary>
  public void RaiseOnInitDataEvent(object data)
  {
    ((IStructualControlSupport) this).RaiseOnInitDataEvent(data);
  }

  /// <summary> Вызывается после загрузки UserPanel </summary>
  protected override void OnLoad(EventArgs e)
  {
    ((IStructualControlSupport) this).CheckHelperObjCreated();
    base.OnLoad(e);
  }

  /// <summary> Вызывается при необходимости обновления визуального состояния контролов </summary>
  void IStructualControlSupport.UpdateControls()
  {
    if (this.structualControlIntf == null)
      return;
    this.UpdateControls();
  }

  /// <summary> Проверка, что объект-помошник создан </summary>
  void IStructualControlSupport.CheckHelperObjCreated()
  {
    if (this._AutoUpdateControlHelper != null)
      return;
    this._AutoUpdateControlHelper = new AutoUpdateControlHelper((Control) this);
    this.structualControlIntf = this._AutoUpdateControlHelper.GetIntf();
    this.LockControls();
    try
    {
      this.RefreshReadOnly();
      this.UpdateControls(false);
    }
    finally
    {
      this.UnlockControls();
    }
    if (this._OnChangedEvent == null)
      return;
    this.structualControlIntf.OnChangedEvent += this._OnChangedEvent;
    this._OnChangedEvent = (EventHandler) null;
  }

  /// <summary> Получить интерфейс с основной функциональностью </summary>
  IStructualControl IStructualControlSupport.GetStructualControlIntf() => this.structualControlIntf;

  /// <summary> Узнать, мешает ли на данном уровне что-либо редактировать контролы </summary>
  bool IStructualControlSupport.IsReadOnly() => this.GetIsReadOnly();

  /// <summary>
  /// Вызывается, когда требуется проверка перед попыткой модификации данных
  /// Например, когда у пользователя необходимо запросить разрешение на взятие
  /// на редактирование некоторого объекта
  /// </summary>
  bool IStructualControlSupport.CheckCanEdit(ref bool wasUpdated)
  {
    return this.BeforeObjectEditBegin(ref wasUpdated);
  }

  /// <summary>
  /// Событие, вызываемое при перезагрузке данных, связанных с контролом (например, при обновлении их из БД)
  /// </summary>
  event InitDataEventHandler IStructualControlSupport.OnInitDataEvent
  {
    add => this._OnInitDataEvent += value;
    remove => this._OnInitDataEvent -= value;
  }

  /// <summary>
  /// Вызов события OnChangedEvent (Изменены параметры схемы нумерации)
  /// </summary>
  void IStructualControlSupport.RaiseOnInitDataEvent(object data)
  {
    if (this._OnInitDataEvent == null)
      return;
    this._OnInitDataEvent((object) this, new InitDataEventArgs(data));
  }
}
