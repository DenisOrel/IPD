// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.Icons
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Project.Controls.Properties;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project.Controls;

internal abstract class Icons : Intermech.Extensions.Icons
{
  [NotNull]
  public static Icon Minus
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => Resources.IconMinus;
  }

  [NotNull]
  public static Icon Plus
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => Resources.IconPlus;
  }
}
