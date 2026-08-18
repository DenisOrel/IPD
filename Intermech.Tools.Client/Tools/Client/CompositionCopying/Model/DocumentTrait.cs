// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DocumentTrait
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class DocumentTrait : DBObjectGraphTrait
{
  public bool IsLocalFilesCopied { get; private set; }

  public void SetLocalFilesCopied()
  {
    this.IsLocalFilesCopied = !this.IsLocalFilesCopied ? true : throw new InvalidOperationException();
  }

  public void ResetLocalFilesCopied(ICleanupCopyStateRegistry batch)
  {
    if (batch == null)
      throw new ArgumentNullException(nameof (batch));
    if (!this.IsLocalFilesCopied)
      return;
    batch.RegisterLocalFiles((DBObjectGraphVertex) this.OwnerObject);
    this.IsLocalFilesCopied = false;
  }

  public bool IsDBCopied
  {
    [DebuggerStepThrough] get => this.DBCopyInfo != null;
  }

  public DBObjectRecord DBCopyInfo { get; private set; }

  public void SetDBCopyInfo(DBObjectRecord dbCopyInfo)
  {
    if (dbCopyInfo == null)
      throw new ArgumentNullException(nameof (dbCopyInfo));
    this.DBCopyInfo = this.DBCopyInfo == null ? dbCopyInfo : throw new InvalidOperationException();
  }

  public void ResetDBCopyInfo(ICleanupCopyStateRegistry batch)
  {
    if (batch == null)
      throw new ArgumentNullException(nameof (batch));
    if (this.DBCopyInfo == null)
      return;
    batch.RegisterDBCopy((DBObjectGraphVertex) this.OwnerObject, this.DBCopyInfo);
    this.DBCopyInfo = (DBObjectRecord) null;
  }
}
