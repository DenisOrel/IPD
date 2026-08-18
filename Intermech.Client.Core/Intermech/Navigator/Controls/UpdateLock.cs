
// Type: Intermech.Navigator.Controls.UpdateLock
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Реализует механизм, предназначенный для блокирования отрисовки визуальных компонентов.
/// Позволяет предотвратить мерцание экрана при интенсивном изменении внешнего вида приложения.
/// </summary>
public class UpdateLock
{
  /// <summary>
  /// Словарь, описывающий соответствие визуальных компонентов и объектов,
  /// блокирующих их отрисовку.
  /// </summary>
  private static Dictionary<Control, UpdateLock.NativeUpdateLocker> lockers;
  /// <summary>
  /// Признак, что приложение в данный момент работает с модальным диалоговым окном.
  /// </summary>
  private static bool IsInModalState;

  /// <summary>Инициализирует механизм блокировки.</summary>
  internal static void Start()
  {
    UpdateLock.lockers = new Dictionary<Control, UpdateLock.NativeUpdateLocker>();
    UpdateLock.IsInModalState = false;
    Application.EnterThreadModal += new EventHandler(UpdateLock.OnEnterThreadModal);
    Application.LeaveThreadModal += new EventHandler(UpdateLock.OnLeaveThreadModal);
  }

  /// <summary>Завершает работу механизма блокировки.</summary>
  internal static void Stop()
  {
    Application.EnterThreadModal -= new EventHandler(UpdateLock.OnEnterThreadModal);
    Application.LeaveThreadModal -= new EventHandler(UpdateLock.OnLeaveThreadModal);
  }

  /// <summary>
  /// Блокирует прорисовку визуального компонента. Может вызываться несколько
  /// раз для одного и того же компонента, что будет приводить к увеличению
  /// счетчика блокировок компонента.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  public static void Apply(Control control)
  {
    UpdateLock.CheckControl(control);
    if (UpdateLock.lockers == null)
      return;
    lock (UpdateLock.lockers)
      UpdateLock.ExplicitLockControl(control);
  }

  /// <summary>
  /// Отменяет блокировку прорисовки визуального компонента. Фактически,
  /// метод уменьшает счетчик блокировок для компонента, и при достижении нуля
  /// блокировка будет реально снята.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  public static void Release(Control control)
  {
    UpdateLock.CheckControl(control);
    if (UpdateLock.lockers == null)
      return;
    lock (UpdateLock.lockers)
    {
      UpdateLock.UnlockControl(control, false);
      if (UpdateLock.FindLocker(control) != null)
        return;
      control.Invalidate(true);
      control.Update();
    }
  }

  /// <summary>
  /// Возвращает true, если прорисовка указанного компонента заблокирована.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  /// <returns>Признак блокировки прорисовки компонента</returns>
  public static bool IsLocked(Control control)
  {
    UpdateLock.CheckControl(control);
    lock (UpdateLock.lockers)
      return UpdateLock.FindLocker(control) != null;
  }

  /// <summary>
  /// Создает объект, блокирующий отрисовку визуального компонента.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  /// <returns>Объект для блокирования отрисовки</returns>
  private static UpdateLock.NativeUpdateLocker CreateLocker(Control control)
  {
    UpdateLock.NativeUpdateLocker locker = new UpdateLock.NativeUpdateLocker(control);
    UpdateLock.lockers.Add(control, locker);
    control.ControlAdded += new ControlEventHandler(UpdateLock.OnControlAdded);
    control.ControlRemoved += new ControlEventHandler(UpdateLock.OnControlRemoved);
    control.Disposed += new EventHandler(UpdateLock.OnControlDisposed);
    return locker;
  }

  /// <summary>
  /// Разрушает объект, блокирующий отрисовку визуального компонента.
  /// </summary>
  /// <param name="locker">Объект для блокирования отрисовки</param>
  /// <param name="control">Визуальный компонент</param>
  private static void DisposeLocker(UpdateLock.NativeUpdateLocker locker, Control control)
  {
    control.ControlAdded -= new ControlEventHandler(UpdateLock.OnControlAdded);
    control.ControlRemoved -= new ControlEventHandler(UpdateLock.OnControlRemoved);
    control.Disposed -= new EventHandler(UpdateLock.OnControlDisposed);
    locker.Dispose();
    UpdateLock.lockers.Remove(control);
  }

  /// <summary>
  /// Возвращает объект, блокирующий отрисовку визуального компонента, или
  /// null, если такового не существует.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  /// <returns>Объект для блокирования отрисовки</returns>
  private static UpdateLock.NativeUpdateLocker FindLocker(Control control)
  {
    UpdateLock.NativeUpdateLocker locker;
    UpdateLock.lockers.TryGetValue(control, out locker);
    return locker;
  }

  /// <summary>
  /// Возвращает объект, блокирующий отрисовку визуального компонента. Если
  /// такого объекта нет, то он будет создан.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  /// <returns>Объект для блокирования отрисовки</returns>
  private static UpdateLock.NativeUpdateLocker GetLocker(Control control)
  {
    return UpdateLock.FindLocker(control) ?? UpdateLock.CreateLocker(control);
  }

  /// <summary>
  /// Реализует алгоритм включения блокировки прорисовки визуального компонента.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  private static void ExplicitLockControl(Control control)
  {
    UpdateLock.NativeUpdateLocker locker = UpdateLock.GetLocker(control);
    ++locker.LockCount;
    UpdateLock.LockChildren(control, locker);
  }

  /// <summary>
  /// Реализует алгоритм блокировки прорисовки визуального компонента, являющегося
  /// частью другого заблокированного компонента.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  /// <param name="parentLocker">Объет для блокировки родительского компонента</param>
  private static void ImplicitLockControl(
    Control control,
    UpdateLock.NativeUpdateLocker parentLocker)
  {
    UpdateLock.NativeUpdateLocker locker = UpdateLock.GetLocker(control);
    locker.LockCount = locker.LockCount == 0 ? parentLocker.LockCount : locker.LockCount + 1;
    UpdateLock.LockChildren(control, locker);
  }

  /// <summary>Блокирует прорисовку дочерних визуальных компонентов.</summary>
  /// <param name="control">Визуальный компонент</param>
  /// <param name="locker">Объект для блокирования отрисовки</param>
  private static void LockChildren(Control control, UpdateLock.NativeUpdateLocker locker)
  {
    for (int index = 0; index < control.Controls.Count; ++index)
      UpdateLock.ImplicitLockControl(control.Controls[index], locker);
  }

  /// <summary>
  /// Реализует алгоритм снятия блокировки прорисовки визуального компонента.
  /// </summary>
  /// <param name="control">Визуальный компонент</param>
  /// <param name="forceUnlock">
  /// Признак игнорирования счетчика блокировок. Используется при снятии блокировки с
  /// удаляемого или разрушаемого компонента
  /// </param>
  private static void UnlockControl(Control control, bool forceUnlock)
  {
    UpdateLock.NativeUpdateLocker locker1 = UpdateLock.FindLocker(control);
    if (locker1 == null)
      return;
    if (forceUnlock)
    {
      locker1.LockCount = 0;
    }
    else
    {
      if (control.Parent != null)
      {
        UpdateLock.NativeUpdateLocker locker2 = UpdateLock.FindLocker(control.Parent);
        if (locker2 != null && locker2.LockCount >= locker1.LockCount)
          throw new InvalidOperationException(LocalizationHolder.rm.GetString("Client.Core_605"));
      }
      --locker1.LockCount;
    }
    if (locker1.LockCount == 0)
      UpdateLock.DisposeLocker(locker1, control);
    for (int index = 0; index < control.Controls.Count; ++index)
      UpdateLock.UnlockControl(control.Controls[index], forceUnlock);
  }

  private static void CheckControl(Control control)
  {
    if (control == null)
      throw new ArgumentNullException(nameof (control), LocalizationHolder.rm.GetString("Client.Core_606"));
  }

  /// <summary>
  /// Обрабатывает событие вставки дочернего компонента в визуальный компонент,
  /// отрисовка которого заблокирована.
  /// </summary>
  /// <param name="sender">Визуальный компонент в который производится вставка</param>
  /// <param name="e">Содержит данные о вставляемом компоненте</param>
  private static void OnControlAdded(object sender, ControlEventArgs e)
  {
    lock (UpdateLock.lockers)
      UpdateLock.ImplicitLockControl(e.Control, UpdateLock.FindLocker((Control) sender) ?? throw new InvalidOperationException("UpdateLock.OnControlAdded fired on non locked control!"));
  }

  /// <summary>
  /// Обрабатывает событие удаления дочернего компонента из визуального компонента,
  /// отрисовка которого заблокирована.
  /// </summary>
  /// <param name="sender">Визуальный компонент из которого производится удаление</param>
  /// <param name="e">Содержит данные об удаляемом компоненте</param>
  private static void OnControlRemoved(object sender, ControlEventArgs e)
  {
    lock (UpdateLock.lockers)
      UpdateLock.UnlockControl(e.Control, true);
  }

  /// <summary>
  /// Обрабатывает событие разрушения визуального компонента.
  /// </summary>
  /// <param name="sender">Разрушаемый визуальный компонент</param>
  /// <param name="e">Параметры события</param>
  private static void OnControlDisposed(object sender, EventArgs e)
  {
    Control control = (Control) sender;
    lock (UpdateLock.lockers)
      UpdateLock.UnlockControl(control, true);
  }

  /// <summary>
  /// Обрабатывает событие перехода приложения в режим работы с модальным диалоговым окном.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void OnEnterThreadModal(object sender, EventArgs e)
  {
    UpdateLock.IsInModalState = true;
  }

  /// <summary>
  /// Обрабатывает событие выхода приложения из режима работы с модальным диалоговым окном.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void OnLeaveThreadModal(object sender, EventArgs e)
  {
    UpdateLock.IsInModalState = false;
  }

  /// <summary>
  /// Реализует объект для блокировки прорисовки компонента на уровне
  /// оконной процедуры.
  /// </summary>
  private class NativeUpdateLocker : NativeWindow, IDisposable
  {
    private Control control;
    private int lockCount;
    private bool assigned;
    private const int WM_PAINT = 15;
    private const int WM_ERASEBKGND = 20;
    private const int WM_NCPAINT = 133;
    private static readonly IntPtr ZeroResult = IntPtr.Zero;
    private static readonly IntPtr NonZeroResult = new IntPtr(1);

    public NativeUpdateLocker(Control control)
    {
      this.control = control;
      this.lockCount = 0;
      this.assigned = false;
      control.HandleCreated += new EventHandler(this.OnHandleCreated);
      control.HandleDestroyed += new EventHandler(this.OnHandleDestroyed);
      if (!control.IsHandleCreated)
        return;
      this.InternalAssignHandle();
    }

    public void Dispose()
    {
      if (this.assigned)
        this.InternalReleaseHandle();
      this.control.HandleCreated -= new EventHandler(this.OnHandleCreated);
      this.control.HandleDestroyed -= new EventHandler(this.OnHandleDestroyed);
      this.lockCount = 0;
      this.control = (Control) null;
    }

    public int LockCount
    {
      get => this.lockCount;
      set => this.lockCount = value;
    }

    private void OnHandleCreated(object sender, EventArgs e) => this.InternalAssignHandle();

    private void InternalAssignHandle()
    {
      this.AssignHandle(this.control.Handle);
      this.assigned = true;
    }

    private void OnHandleDestroyed(object sender, EventArgs e) => this.InternalReleaseHandle();

    private void InternalReleaseHandle()
    {
      this.ReleaseHandle();
      this.assigned = false;
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    protected override void WndProc(ref Message m)
    {
      if (!UpdateLock.IsInModalState)
      {
        switch (m.Msg)
        {
          case 15:
            m.Result = UpdateLock.NativeUpdateLocker.ZeroResult;
            return;
          case 20:
            m.Result = UpdateLock.NativeUpdateLocker.NonZeroResult;
            return;
          case 133:
            m.Result = UpdateLock.NativeUpdateLocker.ZeroResult;
            return;
        }
      }
      base.WndProc(ref m);
    }
  }
}
