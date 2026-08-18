// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ControlUtilities
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

internal static class ControlUtilities
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ContainsFocus([NotNull] Control control)
  {
    return control.Focused || control.Controls.Cast<Control>().Any<Control>(new Func<Control, bool>(ControlUtilities.ContainsFocus));
  }
}
