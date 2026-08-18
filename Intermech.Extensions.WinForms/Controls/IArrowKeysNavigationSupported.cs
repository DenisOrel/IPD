// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.IArrowKeysNavigationSupported
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public interface IArrowKeysNavigationSupported
{
  [CanBeNull]
  Control LeftControl { get; set; }

  [CanBeNull]
  Control RightControl { get; set; }

  [CanBeNull]
  Control UpControl { get; set; }

  [CanBeNull]
  Control DownControl { get; set; }

  void NavigateToLeft();

  void NavigateToRight();

  void NavigateToUp();

  void NavigateToDown();

  event OnNavigateDelegate OnNavigateToLeft;

  event OnNavigateDelegate OnNavigateToRight;

  event OnNavigateDelegate OnNavigateToUp;

  event OnNavigateDelegate OnNavigateToDown;
}
