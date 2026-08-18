// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMDocumentSynchronizationInfo
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMDocumentSynchronizationInfo
{
  public PDMDocumentSynchronizationInfo(
    List<DBObjectState> unpublishedObjects,
    List<DBObjectState> outdatedObjects,
    List<DBObjectState> unsavedWorkObjects,
    List<DBObjectState> savedWorkObjects)
  {
    this.UnpublishedObjects = unpublishedObjects;
    this.OutdatedObjects = outdatedObjects;
    this.UnsavedWorkObjects = unsavedWorkObjects;
    this.SavedWorkObjects = savedWorkObjects;
  }

  public List<DBObjectState> UnpublishedObjects { get; private set; }

  public List<DBObjectState> OutdatedObjects { get; private set; }

  public List<DBObjectState> UnsavedWorkObjects { get; private set; }

  public List<DBObjectState> SavedWorkObjects { get; private set; }
}
