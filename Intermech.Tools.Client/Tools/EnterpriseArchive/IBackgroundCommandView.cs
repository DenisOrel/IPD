// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.IBackgroundCommandView
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

public interface IBackgroundCommandView : IView
{
  void SetCaption(string text);

  void SetMessage(string text);

  void EnableProgressBar(bool infinite);

  void DisableProgressBar();

  void SetProgress(double progress);

  void Hide();
}
