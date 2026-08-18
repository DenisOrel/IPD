// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.IArchiveParametersView
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using System;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal interface IArchiveParametersView : IView
{
  string ArchiveLocation { get; set; }

  int ImportBatchSize { get; set; }

  void AttachPageChangedHandlers();

  void DetachPageChangesHandlers();

  void EnableArchiveLocation(bool enabled);

  void EnableImportBatchSize(bool enabled);

  event EventHandler SelectLocation;

  event EventHandler EditableStateChanged;
}
