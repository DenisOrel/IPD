// Decompiled with JetBrains decompiler
// Type: Intermech.UI.MessageBoxCentered
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

public static class MessageBoxCentered
{
  public static DialogResult Show(
    [NotNull] Form owner,
    [CanBeNull] string text,
    [CanBeNull] string caption = null,
    MessageBoxButtons buttons = MessageBoxButtons.OK,
    MessageBoxIcon icon = MessageBoxIcon.None,
    MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1,
    MessageBoxOptions options = (MessageBoxOptions) 0)
  {
    using (new CenterWinDialogHelper(owner))
      return MessageBox.Show((IWin32Window) owner, text, caption, buttons, icon, defaultButton, options);
  }

  public static DialogResult Show(
    [NotNull] Form owner,
    [CanBeNull] string text,
    [CanBeNull] string caption,
    MessageBoxButtons buttons,
    MessageBoxIcon icon,
    MessageBoxDefaultButton defaultButton,
    MessageBoxOptions options,
    [CanBeNull] string helpFilePath,
    HelpNavigator navigator = (HelpNavigator) 0,
    [CanBeNull] object param = null)
  {
    using (new CenterWinDialogHelper(owner))
      return MessageBox.Show((IWin32Window) owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param);
  }
}
