// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.UI.ViewModel
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.UI;

internal abstract class ViewModel : INotifyPropertyChanged
{
  protected void NotifyPropertyChanged(string propertyName)
  {
    if (this.PropertyChanged == null)
      return;
    this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }

  [Conditional("DEBUG")]
  private void CheckPropertyName(string propertyName)
  {
    if (TypeDescriptor.GetProperties((object) this).Find(propertyName, false) == null)
      throw new InvalidOperationException();
  }

  public event PropertyChangedEventHandler PropertyChanged;
}
