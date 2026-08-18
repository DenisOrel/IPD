// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AutoUpdateControlHelper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Объект, помогающий реализовать такие функции, как бллокирование обновления контролов, блокирование редактирование и т.п.
///   для котролов с многократной вложеностью
/// </summary>
public class AutoUpdateControlHelper : IStructualControl, IDisposable
{
  private IStructualControlSupport _OwnerSupportIntf;
  public IStructualControl _ParentStructualControl;
  private int _ControlsUpdateLockCounter;
  private bool _ControlsNeedUpdate;
  private bool _ControlsNeedRecurceUpdate;
  private ArrayList _ChildStructualControls = new ArrayList();
  private bool _ReadOnly;
  private EventHandler _OnChangedEvent;
  private Control _OwnerControl;
  private bool _Changed;
  private bool _ControlsAreUpdating;
  private bool _OverrideReadOnly;

  /// <summary>Создаём объект-помошник</summary>
  /// <param name="owner">Контрол, которому мы "помогаем"</param>
  public AutoUpdateControlHelper(Control owner)
  {
    this._OwnerControl = owner != null ? owner : throw new Exception("AutoUpdateControlHelper must have an Owner");
    this._OwnerSupportIntf = owner as IStructualControlSupport;
    if (this._OwnerSupportIntf == null)
      throw new Exception("Object must support an IStructualControlSupport interface");
    this._ParentStructualControl = this.FindParentStructualControl(owner);
    if (this._ParentStructualControl != null)
      this._ParentStructualControl.GetHelperObj()?.RegisterChild((IStructualControl) this);
    this.GetIntf().LockControls();
    try
    {
      this.GetIntf().RefreshReadOnly();
      this.GetIntf().UpdateControls(false);
    }
    finally
    {
      this.GetIntf().UnlockControls();
    }
  }

  /// <summary>
  /// Просмотров контролов-владельцев в поисках поддеживающих интерфейс
  /// </summary>
  /// <param name="owner">Контрол, которому мы "помогаем"</param>
  /// <returns>IStructualControl если таковой был найден в списке контролов-владельцев переданого</returns>
  private IStructualControl FindParentStructualControl(Control owner)
  {
    Control parentInt = this.GetParentInt(owner);
    for (; parentInt != null; parentInt = this.GetParentInt(parentInt))
    {
      if (parentInt is IStructualControlSupport structualControlSupport)
      {
        structualControlSupport.CheckHelperObjCreated();
        return structualControlSupport.GetStructualControlIntf();
      }
    }
    return (IStructualControl) null;
  }

  /// <summary> "Правильное" получение объекта-владельца для переданого </summary>
  /// <param name="control"> Контрол, объект-владелец которого мы хотим получить </param>
  /// <returns> Владелец переданого объекта  </returns>
  private Control GetParentInt(Control control)
  {
    return !(control is ExtForm) ? control.Parent : ((ExtForm) control).OwnerControl;
  }

  /// <summary>Блокирование обновления визуальных контролов</summary>
  void IStructualControl.LockControls()
  {
    if (this._ControlsUpdateLockCounter == 0)
    {
      this._ControlsNeedUpdate = false;
      this._ControlsNeedRecurceUpdate = false;
    }
    ++this._ControlsUpdateLockCounter;
  }

  /// <summary>Разблокирование обновления визуальных контролов</summary>
  void IStructualControl.UnlockControls() => this.GetIntf().UnlockControls(false);

  /// <summary>Разблокирование обновления визуальных контролов</summary>
  void IStructualControl.UnlockControls(bool notUpdate)
  {
    if (notUpdate)
    {
      this._ControlsNeedUpdate = false;
      this._ControlsNeedRecurceUpdate = false;
    }
    if (this._ControlsUpdateLockCounter <= 0)
      return;
    --this._ControlsUpdateLockCounter;
    if (this._ControlsUpdateLockCounter != 0 || !this._ControlsNeedUpdate)
      return;
    ((IStructualControl) this).UpdateControls(this._ControlsNeedRecurceUpdate);
  }

  /// <summary>
  /// Проверка, заблокировано обновление визуальных контролов
  /// </summary>
  bool IStructualControl.IsControlsLocked()
  {
    if (this._ControlsUpdateLockCounter > 0)
      return true;
    return this._ParentStructualControl != null && this._ParentStructualControl.IsControlsLocked();
  }

  /// <summary>Обновление визуального состояния контролов</summary>
  /// <param name="recurce">Обновлять так же все дочерние контролы с "помошниками"</param>
  void IStructualControl.UpdateControls(bool recurce)
  {
    if (!((IStructualControl) this).IsControlsLocked())
    {
      this.UpdateControlsInternal(recurce);
    }
    else
    {
      this._ControlsNeedUpdate = true;
      this._ControlsNeedRecurceUpdate |= recurce;
    }
  }

  /// <summary>
  /// Признак того, что контролы в данный момент обновляются
  /// </summary>
  bool IStructualControl.ControlsAreUpdating => this._ControlsAreUpdating;

  /// <summary>Получить объект-помошник</summary>
  /// <returns>объект-помошник</returns>
  AutoUpdateControlHelper IStructualControl.GetHelperObj() => this;

  /// <summary>Доступно ли редактирование</summary>
  bool IStructualControl.ReadOnly
  {
    get => this._ReadOnly;
    set
    {
      if (this._ReadOnly == value)
        return;
      bool flag = value || this._OwnerSupportIntf.IsReadOnly();
      if (this._ReadOnly == flag)
        return;
      this._ReadOnly = flag;
      foreach (IStructualControl structualControl in this._ChildStructualControls)
        structualControl.ReadOnly = this._ReadOnly;
      ((IStructualControl) this).UpdateControls(false);
    }
  }

  bool IStructualControl.OverrideReadOnly
  {
    get => this._OverrideReadOnly;
    set
    {
      this._OverrideReadOnly = value;
      this.GetIntf().RefreshReadOnly();
    }
  }

  /// <summary>
  /// Обновить значение параметра "Доступно ли редактирование"
  /// </summary>
  void IStructualControl.RefreshReadOnly()
  {
    if (this._ParentStructualControl != null)
      this._ParentStructualControl.RefreshReadOnly();
    this.GetIntf().ReadOnly = this._OverrideReadOnly || this._OwnerSupportIntf.IsReadOnly() || this._ParentStructualControl != null && this._ParentStructualControl.ReadOnly;
  }

  /// <summary>
  /// Должен вызываться при каждой попытке редактирования.
  /// Проверяет доступно ли редактирование данных и, если требуется,
  /// запрашивает у пользователя разрешение на их редактирование
  /// (например, на взятие на изменение соотв. объекта)
  /// </summary>
  bool IStructualControl.CheckCanEdit(ref bool wasUpdated)
  {
    return this._ParentStructualControl != null ? this._ParentStructualControl.CheckCanEdit(ref wasUpdated) : this._OwnerSupportIntf.CheckCanEdit(ref wasUpdated);
  }

  /// <summary>
  /// Признак того, что данные, связаные с контролом были изменены
  /// </summary>
  bool IStructualControl.Changed
  {
    get => this._Changed;
    set
    {
      if (this._Changed == value || this._ControlsAreUpdating)
        return;
      this._Changed = value;
      if (value)
      {
        this.RaiseOnChangedEvent();
        if ((!(this._OwnerControl is ExtForm) || !((ExtForm) this._OwnerControl).ChangedWaitOk()) && this._ParentStructualControl != null)
          this._ParentStructualControl.Changed = true;
      }
      else
      {
        foreach (IStructualControl structualControl in this._ChildStructualControls)
          structualControl.Changed = false;
      }
      this.GetIntf().UpdateControls(false);
    }
  }

  /// <summary>
  /// Событие, вызываемое при изменении данных, связанных с контролом
  /// </summary>
  event EventHandler IStructualControl.OnChangedEvent
  {
    add => this._OnChangedEvent += value;
    remove => this._OnChangedEvent -= value;
  }

  /// <summary>
  /// Вызов события OnChangedEvent (Изменены параметры схемы нумерации)
  /// </summary>
  private void RaiseOnChangedEvent()
  {
    if (this._OnChangedEvent == null)
      return;
    this._OnChangedEvent((object) this._OwnerControl, new EventArgs());
  }

  /// <summary> Очистка использовавшихся ресурсов </summary>
  public void Dispose()
  {
    foreach (IStructualControl structualControl in this._ChildStructualControls)
    {
      AutoUpdateControlHelper helperObj = structualControl.GetHelperObj();
      if (helperObj._ParentStructualControl == this)
        helperObj._ParentStructualControl = this._ParentStructualControl;
    }
    this._ChildStructualControls.Clear();
  }

  /// <summary>
  /// Информирование владельца о наличии дочернего "помошника"
  /// </summary>
  /// <param name="childStructualControl">Интерфейс дочернего "помошника"</param>
  public void RegisterChild(IStructualControl childStructualControl)
  {
    this._ChildStructualControls.Add((object) childStructualControl);
  }

  /// <summary>
  /// Вызвать обновление контролов на контроле, которому мы "помогаем" не проверяя блокировку обновления
  /// </summary>
  /// <param name="recurce"></param>
  public void UpdateControlsInternal(bool recurce)
  {
    if (!this._ControlsAreUpdating)
    {
      this._ControlsAreUpdating = true;
      try
      {
        this._OwnerSupportIntf.UpdateControls();
      }
      finally
      {
        this._ControlsAreUpdating = false;
      }
    }
    if (!recurce)
      return;
    foreach (IStructualControl structualControl in this._ChildStructualControls)
      structualControl.GetHelperObj().UpdateControlsInternal(recurce);
  }

  /// <summary>Получение интерфейса с основными методами</summary>
  /// <returns></returns>
  public IStructualControl GetIntf() => (IStructualControl) this;
}
