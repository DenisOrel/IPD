// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.DialogService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Windows;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class DialogService
{
  public void DisplayWarning(string messageText)
  {
    int num = messageText != null ? (int) MessageBox.Show(messageText, DialogConsts.WizardCaption, MessageBoxButton.OK, MessageBoxImage.Exclamation) : throw new ArgumentNullException(nameof (messageText));
  }

  public bool AskYesNo(string messageText)
  {
    if (messageText == null)
      throw new ArgumentNullException(nameof (messageText));
    return MessageBox.Show(messageText, DialogConsts.WizardCaption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
  }
}
