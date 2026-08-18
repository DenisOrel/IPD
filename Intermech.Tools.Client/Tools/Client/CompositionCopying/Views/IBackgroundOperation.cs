// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.IBackgroundOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.ComponentModel;
using System.Windows.Input;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

public interface IBackgroundOperation : INotifyPropertyChanged
{
  bool IsRunning { get; }

  int Progress { get; }

  ICommand StartCommand { get; }

  ICommand StopCommand { get; }
}
