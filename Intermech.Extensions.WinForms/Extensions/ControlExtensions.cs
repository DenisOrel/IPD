// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ControlExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.WindowsDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Extensions;

public static class ControlExtensions
{
  [NotNull]
  private static readonly object _suspendDrawingLockObj = new object();
  [NotNull]
  private static readonly Dictionary<IntPtr, int> _suspendDrawingLockCounters = new Dictionary<IntPtr, int>();

  [NotNull]
  [ItemNotNull]
  public static IEnumerable<Control> GetParentsEnumeration([NotNull] this Control control, bool includeThis = false)
  {
    if (!includeThis)
      control = control.Parent;
    while (true)
    {
      Control control1 = control;
      if ((control1 != null ? (!control1.IsDisposed ? 1 : 0) : 0) != 0)
      {
        yield return control;
        control = control.Parent;
      }
      else
        break;
    }
  }

  [NotNull]
  [ItemNotNull]
  public static Control[] GetParentsArray([NotNull] this Control control, bool includeThis = false)
  {
    GetParentsInternal(control, includeThis ? 0 : -1);
    Control[] result;
    if (includeThis)
      result[0] = control;
    return result;

    void GetParentsInternal(Control current, int index)
    {
      Control parent = current.Parent;
      if (parent != null && !parent.IsDisposed)
      {
        GetParentsInternal(parent, ++index);
        result[index] = parent;
      }
      else
        result = new Control[index + 1];
    }
  }

  [NotNull]
  [ItemNotNull]
  public static IEnumerable<Control> GetChildsRecursive(
    [NotNull] this Control control,
    bool onlyEnabled,
    bool includeThis)
  {
    if (!includeThis)
      return control.GetChildsRecursive(onlyEnabled);
    return !onlyEnabled || control.Enabled ? Enumeration.Create<Control>(control).Concat<Control>(control.GetChildsRecursive(onlyEnabled)) : Enumerable.Empty<Control>();
  }

  [NotNull]
  [ItemNotNull]
  public static IEnumerable<Control> GetChildsRecursive([NotNull] this Control control, bool onlyEnabled)
  {
    return control.Controls.Cast<Control>().SelectMany<Control, Control>((Func<Control, IEnumerable<Control>>) (childControl => childControl.IsDisposed || onlyEnabled && !childControl.Enabled ? Enumerable.Empty<Control>() : Enumeration.Create<Control>(childControl).Concat<Control>(childControl.GetChildsRecursive(onlyEnabled))));
  }

  [NotNull]
  public static Control FocusIfCan([NotNull] this Control control)
  {
    if (control.CanFocus && !control.Focused)
      control.Focus();
    return control;
  }

  [NotNull]
  [ItemNotNull]
  public static IEnumerable<Control> GetAllChilds([NotNull] this Control control, [CanBeNull] Type type = null)
  {
    IReadOnlyList<Control> controlList = control.Controls.CastList<Control>();
    return type != (Type) null ? controlList.SelectMany<Control, Control>((Func<Control, IEnumerable<Control>>) (ctrl => ctrl.GetAllChilds(type))).Concat<Control>((IEnumerable<Control>) controlList).Where<Control>((Func<Control, bool>) (childControl =>
    {
      Type type1 = childControl.GetType();
      if (childControl.IsDisposed)
        return false;
      return type1 == type || type1.IsSubclassOf(type);
    })) : controlList.SelectMany<Control, Control>((Func<Control, IEnumerable<Control>>) (ctrl => ctrl.GetAllChilds())).Concat<Control>((IEnumerable<Control>) controlList);
  }

  [NotNull]
  [ItemNotNull]
  public static IEnumerable<Control> GetAllChilds(
    [NotNull] this Control control,
    [CanBeNull] IReadOnlyCollection<Type> types)
  {
    IReadOnlyList<Control> controlList = control.Controls.CastList<Control>();
    if (types == null || types.Count <= 0)
      return control.GetAllChilds();
    return types.Count == 1 ? control.GetAllChilds(types.First<Type>()) : controlList.SelectMany<Control, Control>((Func<Control, IEnumerable<Control>>) (ctrl => ctrl.GetAllChilds(types))).Concat<Control>((IEnumerable<Control>) controlList).Where<Control>((Func<Control, bool>) (childControl => !childControl.IsDisposed && types.All<Type>((Func<Type, bool>) (type =>
    {
      Type type1 = childControl.GetType();
      return type1 == type || type1.IsSubclassOf(type);
    }))));
  }

  public static bool IsDesignParentControl([NotNull] this Control control)
  {
    IDesignerHost service;
    return control.Site != null && control.Site.TryGetService<IDesignerHost>(out service) && service.GetDesigner((IComponent) control) is ParentControlDesigner;
  }

  public static bool IsDesignerEnabled([NotNull] this Control control)
  {
    IDesignerHost service;
    return control.Site != null && control.Site.TryGetService<IDesignerHost>(out service) && service.GetDesigner((IComponent) control) != null;
  }

  [NotNull]
  [ItemNotNull]
  public static IEnumerable<Control> GetChildControls([NotNull] this Control control)
  {
    return control.Controls.Cast<Control>();
  }

  [NotNull]
  [ItemNotNull]
  public static IEnumerable<Control> GetParentControls([NotNull] this Control control)
  {
    for (Control parent = control.Parent; parent != null && !parent.IsDisposed; parent = control.Parent)
      yield return parent;
  }

  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable SuspendDrawingBlock([NotNull] this Control control)
  {
    control.SuspendDrawing();
    return (IDisposable) new CallOnDispose(new Action(((ControlExtensions) control).ResumeDrawing));
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void SuspendDrawing([NotNull] this Control control)
  {
    lock (ControlExtensions._suspendDrawingLockObj)
    {
      int orDefault = ControlExtensions._suspendDrawingLockCounters.GetOrDefault<IntPtr, int>(control.Handle);
      if (orDefault == 0)
      {
        NativeWindow nativeWindow = NativeWindow.FromHandle(control.Handle);
        if (nativeWindow != null)
        {
          Message m = Message.Create(control.Handle, 11, IntPtr.Zero, IntPtr.Zero);
          nativeWindow.DefWndProc(ref m);
        }
      }
      int num = orDefault + 1;
      ControlExtensions._suspendDrawingLockCounters[control.Handle] = num;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void ResumeDrawing([NotNull] this Control control)
  {
    lock (ControlExtensions._suspendDrawingLockObj)
    {
      int num = ControlExtensions._suspendDrawingLockCounters.GetOrDefault<IntPtr, int>(control.Handle) - 1;
      if (num == 0)
      {
        ControlExtensions._suspendDrawingLockCounters.Remove(control.Handle);
        NativeWindow nativeWindow = NativeWindow.FromHandle(control.Handle);
        if (nativeWindow == null)
          return;
        Message m = Message.Create(control.Handle, 11, new IntPtr(1), IntPtr.Zero);
        nativeWindow.DefWndProc(ref m);
        control.Refresh();
      }
      else
        ControlExtensions._suspendDrawingLockCounters[control.Handle] = num;
    }
  }

  [NotNull]
  public static T Clone<T>([NotNull] this T control) where T : Control
  {
    PropertyInfo[] properties = typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
    T instance = Activator.CreateInstance<T>();
    foreach (PropertyInfo propertyInfo in properties)
    {
      if (propertyInfo.CanWrite && propertyInfo.Name != "WindowTarget" && propertyInfo.Name != "AutoScroll")
        propertyInfo.SetValue((object) instance, propertyInfo.GetValue((object) control, (object[]) null), (object[]) null);
    }
    return instance;
  }

  public static bool TryGetControlStyles([NotNull] this Control control, out User32.WindowStyles result)
  {
    IntPtr handle = control.Handle;
    if (handle == IntPtr.Zero)
    {
      result = User32.WindowStyles.WS_OVERLAPPED;
      return false;
    }
    User32.WINDOWINFO WindowInfo = new User32.WINDOWINFO();
    if (!User32.GetWindowInfo(handle, WindowInfo))
    {
      result = User32.WindowStyles.WS_OVERLAPPED;
      return false;
    }
    result = WindowInfo.Style;
    return true;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Size GetMinimumClientSize([NotNull] this Control control)
  {
    Size result;
    return !control.TryGetMinimumClientSize(out result) ? Size.Empty : result;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Size? GetMinimumClientSizeOrNull([NotNull] this Control control)
  {
    Size result;
    return !control.TryGetMinimumClientSize(out result) ? new Size?() : new Size?(result);
  }

  public static bool TryGetMinimumClientSize([NotNull] this Control control, out Size result)
  {
    result = control.MinimumSize;
    if (result.IsEmpty)
      return false;
    Size size = control.Size - control.ClientSize;
    if (!size.IsEmpty)
      result -= size;
    return true;
  }

  public static void AutoFitToNestedControls(
    [NotNull] this Control containerControl,
    FormAutoFitMode fitMode = FormAutoFitMode.WidthAndHeight,
    bool includeInvisible = true)
  {
    if (containerControl.Controls.Count == 0)
      return;
    DockStyle dock = containerControl.Dock;
    switch (dock)
    {
      case DockStyle.None:
        if (fitMode == FormAutoFitMode.None)
          break;
        Size size = containerControl.CalcMinimumSizeFromNestedControls(includeInvisible, fitMode);
        if (size == Size.Empty)
          break;
        if (size.Width == 0 && (fitMode & FormAutoFitMode.Width) != FormAutoFitMode.None)
          fitMode ^= FormAutoFitMode.Width;
        if (size.Height == 0 && (fitMode & FormAutoFitMode.Height) != FormAutoFitMode.None)
          fitMode ^= FormAutoFitMode.Height;
        if (fitMode == FormAutoFitMode.None)
          break;
        Size clientSize = containerControl.ClientSize;
        switch (fitMode - 1)
        {
          case FormAutoFitMode.None:
            if (clientSize.Width == size.Width)
              return;
            break;
          case FormAutoFitMode.Width:
            if (clientSize.Height == size.Height)
              return;
            break;
          case FormAutoFitMode.Height:
            if (clientSize.Width == size.Width)
              fitMode ^= FormAutoFitMode.Width;
            if (clientSize.Height == size.Height)
            {
              fitMode ^= FormAutoFitMode.Height;
              break;
            }
            break;
        }
        if (fitMode == FormAutoFitMode.None)
          break;
        containerControl.ClientSize = new Size((fitMode & FormAutoFitMode.Width) != FormAutoFitMode.None ? size.Width : clientSize.Width, (fitMode & FormAutoFitMode.Height) != FormAutoFitMode.None ? size.Height : clientSize.Height);
        break;
      case DockStyle.Fill:
        break;
      default:
        if ((fitMode & FormAutoFitMode.Height) != FormAutoFitMode.None && (dock == DockStyle.Left || dock == DockStyle.Right))
          fitMode ^= FormAutoFitMode.Height;
        if ((fitMode & FormAutoFitMode.Width) != FormAutoFitMode.None && (dock == DockStyle.Top || dock == DockStyle.Bottom))
        {
          fitMode ^= FormAutoFitMode.Width;
          goto case DockStyle.None;
        }
        goto case DockStyle.None;
    }
  }

  private static Size CalcMinimumSizeFromNestedControls(
    [NotNull] this Control containerControl,
    bool includeInvisible = false,
    [NotEmpty] FormAutoFitMode calcSizes = FormAutoFitMode.WidthAndHeight)
  {
    bool flag1 = (calcSizes & FormAutoFitMode.Width) != 0;
    bool flag2 = (calcSizes & FormAutoFitMode.Height) != 0;
    int val1_1 = 0;
    int val1_2 = 0;
    int num1 = 0;
    int num2 = 0;
    foreach (Control control1 in (ArrangedElementCollection) containerControl.Controls)
    {
      Control control = control1;
      if (control != null && (includeInvisible || control.Visible))
      {
        Size minCtrlClientSize = control.GetMinimumClientSize();
        switch (control.Dock)
        {
          case DockStyle.None:
            if (flag1)
              val1_1 = Math.Max(val1_1, control.Left + control.Width);
            if (flag2)
            {
              val1_2 = Math.Max(val1_2, control.Top + control.Height);
              continue;
            }
            continue;
          case DockStyle.Top:
            if (flag1)
              val1_1 = Math.Max(val1_1, control.Left + GetMinimumControlWidth());
            if (flag2)
            {
              val1_2 = Math.Max(val1_2, control.Top + control.Height);
              continue;
            }
            continue;
          case DockStyle.Bottom:
            if (flag1)
              val1_1 = Math.Max(val1_1, control.Left + GetMinimumControlWidth());
            if (flag2)
            {
              num2 += control.Height;
              continue;
            }
            continue;
          case DockStyle.Left:
            if (flag1)
              val1_1 = Math.Max(val1_1, control.Left + control.Width);
            if (flag2)
            {
              val1_2 = Math.Max(val1_2, control.Top + GetMinimumControlHeight());
              continue;
            }
            continue;
          case DockStyle.Right:
            if (flag1)
              num1 += control.Width;
            if (flag2)
            {
              val1_2 = Math.Max(val1_2, control.Top + GetMinimumControlHeight());
              continue;
            }
            continue;
          case DockStyle.Fill:
            if (calcSizes == FormAutoFitMode.WidthAndHeight)
            {
              Size minimumControlSize = GetMinimumControlSize();
              val1_1 = Math.Max(val1_1, control.Left + minimumControlSize.Width);
              val1_2 = Math.Max(val1_2, control.Top + minimumControlSize.Height);
              continue;
            }
            if (flag1)
              val1_1 = Math.Max(val1_1, control.Left + GetMinimumControlWidth());
            if (flag2)
            {
              val1_2 = Math.Max(val1_2, control.Top + GetMinimumControlHeight());
              continue;
            }
            continue;
          default:
            throw new NotSupportedEnumException((Enum) control.Dock, $"{"DockStyle"} value {control.Dock} not supported!");
        }

        int GetMinimumControlWidth()
        {
          return control.Controls.Count <= 0 ? minCtrlClientSize.Width : Math.Max(minCtrlClientSize.Width, control.CalcMinimumSizeFromNestedControls(includeInvisible, FormAutoFitMode.Width).Width);
        }

        int GetMinimumControlHeight()
        {
          return control.Controls.Count <= 0 ? minCtrlClientSize.Height : Math.Max(minCtrlClientSize.Height, control.CalcMinimumSizeFromNestedControls(includeInvisible, FormAutoFitMode.Height).Height);
        }

        Size GetMinimumControlSize()
        {
          if (control.Controls.Count <= 0)
            return control.MinimumSize;
          Size size = control.CalcMinimumSizeFromNestedControls(includeInvisible);
          return new Size(Math.Max(minCtrlClientSize.Width, size.Width), Math.Max(minCtrlClientSize.Height, size.Height));
        }
      }
    }
    Size minimumClientSize = containerControl.GetMinimumClientSize();
    int width = 0;
    if (flag1)
      width = Math.Max(val1_1 + num1 + containerControl.Padding.Right, minimumClientSize.Width);
    int height = 0;
    if (flag2)
      height = Math.Max(val1_2 + num2 + containerControl.Padding.Bottom, minimumClientSize.Height);
    return new Size(width, height);
  }

  public static void InvokeIfRequired([NotNull] this Control c, [NotNull] Action<Control> action)
  {
    if (c.IsDisposed || !c.IsHandleCreated && !c.FindForm().IsHandleCreated)
      return;
    if (c.InvokeRequired)
      c.Invoke((Delegate) (() => action(c)));
    else
      action(c);
  }
}
