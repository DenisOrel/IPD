// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.IFileStatisticsView
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal interface IFileStatisticsView : IView
{
  void SetMessage(string text);

  void ToggleProgressBar(bool toggleVisible);

  void SetTotalFiles(string text);

  void SetImportedFiles(string text);

  void SetInProgressFiles(string text);

  void SetNotImportedFiles(string text);
}
