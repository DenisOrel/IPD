// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.WinForms.FormExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions.WinForms;

public static class FormExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static DialogResult ShowTopDialog([NotNull] this Form form)
  {
    Form owner = Application.OpenForms.Cast<Form>().LastOrDefault<Form>();
    if (owner != null && !owner.Modal)
      owner = Application.OpenForms.Cast<Form>().FirstOrDefault<Form>();
    return owner == null || owner.InvokeRequired ? form.ShowDialog() : form.ShowDialog((IWin32Window) owner);
  }
}
