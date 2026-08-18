// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.ISystemSettingsEditorView
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using System;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal interface ISystemSettingsEditorView : IView
{
  bool AllowEditSettings { get; set; }

  bool EnableIntegration { get; set; }

  bool ShowRestartRequiredWarning { get; set; }

  event EventHandler EditableStateChanged;
}
