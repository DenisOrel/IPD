// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.MenuCommandsFlags
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.ComponentModel;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal sealed class MenuCommandsFlags : INotifyPropertyChanged
{
  private bool preOpenDocumentsMode;

  public MenuCommandsFlags() => this.preOpenDocumentsMode = true;

  public bool PreOpenDocumentsMode
  {
    get => this.preOpenDocumentsMode;
    set
    {
      if (this.preOpenDocumentsMode == value)
        return;
      this.preOpenDocumentsMode = value;
      this.RaisePropertyChanged(nameof (PreOpenDocumentsMode));
    }
  }

  public event PropertyChangedEventHandler PropertyChanged;

  private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
  {
    if (propertyName == null || this.PropertyChanged == null)
      return;
    this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }
}
