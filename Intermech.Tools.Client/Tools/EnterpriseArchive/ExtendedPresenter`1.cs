// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ExtendedPresenter`1
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal class ExtendedPresenter<TView> : Presenter<TView> where TView : class, IView
{
  protected override void OnAfterAttachView()
  {
    base.OnAfterAttachView();
    this.OnStartBackgroundTask();
  }

  protected virtual void OnStartBackgroundTask()
  {
  }
}
