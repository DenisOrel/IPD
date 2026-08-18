// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.IFileStatesView
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.Mvp.Components;
using System;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal interface IFileStatesView : IView
{
  string SelectedDir { get; set; }

  ITreeView FileTree { get; }

  IFileListView FileList { get; }

  void ShowToast(string text);

  void HideToast();

  void EnableSaveButton(bool enabled);

  void EnableSaveAllButton(bool enabled);

  event EventHandler Save;

  event EventHandler SaveAll;
}
