// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CleanupCopyStateOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CleanupCopyStateOperation : ICleanupCopyStateRegistry
{
  private HashSet<string> files;
  private HashSet<DBObjectRecord> dbCopies;
  private string workAreaPath;

  public CleanupCopyStateOperation()
  {
    this.files = new HashSet<string>((IEqualityComparer<string>) PathUtils.CurrentPathComparer);
    this.dbCopies = new HashSet<DBObjectRecord>();
  }

  public void RegisterLocalFiles(DBObjectGraphVertex dbObjectVertex)
  {
    if (dbObjectVertex == null)
      throw new ArgumentNullException(nameof (dbObjectVertex));
    this.files.AddRange<string>(dbObjectVertex.Files.Where<DBObjectFileEntry>((Func<DBObjectFileEntry, bool>) (x => x.IsRenamed)).Select<DBObjectFileEntry, string>((Func<DBObjectFileEntry, string>) (x => x.NewName)));
  }

  public void RegisterDBCopy(DBObjectGraphVertex dbObjectVertex, DBObjectRecord dbCopyInfo)
  {
    if (dbObjectVertex == null)
      throw new ArgumentNullException(nameof (dbObjectVertex));
    if (dbCopyInfo == null)
      throw new ArgumentNullException(nameof (dbCopyInfo));
    this.dbCopies.Add(dbCopyInfo);
  }

  public void Clear()
  {
    this.files.Clear();
    this.dbCopies.Clear();
  }

  public void Invoke(CopyingSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (this.files.Count == 0 && this.dbCopies.Count == 0)
      return;
    try
    {
      this.Initialize(session);
      this.RemoveLocalFiles();
      this.RemoveDBCopies();
    }
    finally
    {
      this.Cleanup();
    }
  }

  private void Initialize(CopyingSession session)
  {
    this.workAreaPath = session.Services.FileVaultService.WorkArea.AreaPath;
  }

  private void Cleanup()
  {
    this.files.Clear();
    this.dbCopies.Clear();
    this.workAreaPath = (string) null;
  }

  private void RemoveLocalFiles()
  {
    if (this.files.Count == 0)
      return;
    string[] array = this.files.Select<string, string>((Func<string, string>) (x => Path.Combine(this.workAreaPath, x))).ToArray<string>();
    FileUtils.DeleteFilesSilently((ICollection<string>) array);
    foreach (string str in ((IEnumerable<string>) array).Select<string, string>((Func<string, string>) (x => Path.GetDirectoryName(x))).Where<string>((Func<string, bool>) (x => !string.IsNullOrEmpty(x))).Distinct<string>(this.files.Comparer))
    {
      if (Directory.Exists(str) && Directory.EnumerateFiles(str, "*", SearchOption.AllDirectories).Take<string>(1).ToArray<string>().Length == 0)
        FileUtils.DeleteDirectorySilently(str, true);
    }
  }

  private void RemoveDBCopies()
  {
    if (this.dbCopies.Count == 0)
      return;
    long[] array = this.dbCopies.Select<DBObjectRecord, long>((Func<DBObjectRecord, long>) (x => Math.Abs(x.ObjectId))).ToArray<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.BeginDeleteObjects((IEnumerable<long>) array);
      try
      {
        sessionKeeper.Session.GetObjectCollection(-1).Delete(array, true, 0L);
      }
      catch (Exception ex)
      {
        string currentMethodName = this.GetCurrentMethodName(nameof (RemoveDBCopies));
        SuppressedExceptions.TraceException(ex, currentMethodName);
      }
      finally
      {
        sessionKeeper.Session.EndDeleteObjects();
      }
    }
  }
}
