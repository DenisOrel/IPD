// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADHeuristics
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal abstract class CADHeuristics
{
  protected CADHeuristics(
    IIntegrator integrator,
    ICopyingSessionServices services,
    CADCloneDataCapabilities cloneDataCapabilities)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (services == null)
      throw new ArgumentNullException(nameof (services));
    this.Integrator = integrator;
    this.Services = services;
    this.CloneDataCapabilities = cloneDataCapabilities;
  }

  public IIntegrator Integrator { get; }

  public ICopyingSessionServices Services { get; }

  public CADCloneDataCapabilities CloneDataCapabilities { get; }

  public string RenameFile(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    DBObjectFileEntry fileRecord)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (dbObjectVertex == null)
      throw new ArgumentNullException(nameof (dbObjectVertex));
    if (fileRecord == null)
      throw new ArgumentNullException(nameof (fileRecord));
    return this.DoRenameFile(session, dbObjectVertex, fileRecord);
  }

  protected virtual string DoRenameFile(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    DBObjectFileEntry fileRecord)
  {
    if (fileRecord.Content.IsMainFile)
    {
      if (dbObjectVertex.IsCADModelDrawing())
      {
        Tuple<DBObjectGraphVertex, DBObjectFileEntry> baseCadModel = this.FindBaseCADModel(session, dbObjectVertex, fileRecord);
        if (baseCadModel != null)
          return this.RenameDerivedFile(session, dbObjectVertex, fileRecord, baseCadModel.Item1, baseCadModel.Item2);
      }
      return this.RenameFileUsingObjectIdentity(session, dbObjectVertex, CADHeuristics.SplittedPath.FromPath(fileRecord.OriginalName));
    }
    return this.IsDerivedFileName(session, dbObjectVertex, fileRecord, dbObjectVertex, dbObjectVertex.Files[0]) ? this.RenameDerivedFile(session, dbObjectVertex, fileRecord, dbObjectVertex, dbObjectVertex.Files[0]) : this.RenameFileUsingSessionId(session, dbObjectVertex, CADHeuristics.SplittedPath.FromPath(fileRecord.OriginalName));
  }

  private Tuple<DBObjectGraphVertex, DBObjectFileEntry> FindBaseCADModel(
    CopyingSession session,
    DBObjectGraphVertex cadModelDrawingVertex,
    DBObjectFileEntry cadModelDrawingFileRecord)
  {
    foreach (DBObjectGraphVertex verticesByOutEdge in (IEnumerable<DBObjectGraphVertex>) session.Graph.GetVerticesByOutEdges(cadModelDrawingVertex))
    {
      DBObjectFileEntry file = verticesByOutEdge.Files[0];
      if (this.IsDerivedFileName(session, cadModelDrawingVertex, cadModelDrawingFileRecord, verticesByOutEdge, file))
        return Tuple.Create<DBObjectGraphVertex, DBObjectFileEntry>(verticesByOutEdge, file);
    }
    return (Tuple<DBObjectGraphVertex, DBObjectFileEntry>) null;
  }

  private string RenameFileUsingObjectIdentity(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    CADHeuristics.SplittedPath originalName)
  {
    string stringAttributeValue = this.TryGetStringAttributeValue(dbObjectVertex, this.Services.IntegratorsIDCache.Designation.Id);
    if (string.IsNullOrEmpty(stringAttributeValue))
      stringAttributeValue = this.TryGetStringAttributeValue(dbObjectVertex, this.Services.IntegratorsIDCache.Name.Id);
    if (string.IsNullOrEmpty(stringAttributeValue))
      return this.RenameFileUsingSessionId(session, dbObjectVertex, originalName);
    string str = FileNameHelper.ReplaceInvalidFileNameChars(stringAttributeValue).Replace(' ', '_');
    return Path.Combine(originalName.Directory, $"{str}_{session.UniqueId}{originalName.Extension}");
  }

  private string RenameFileUsingSessionId(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    CADHeuristics.SplittedPath originalName)
  {
    return Path.Combine(originalName.Directory, $"{originalName.FileNameOnly}_{session.UniqueId}{originalName.Extension}");
  }

  private bool IsDerivedFileName(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    DBObjectFileEntry dbObjectfileRecord,
    DBObjectGraphVertex baseVertex,
    DBObjectFileEntry baseFileRecord)
  {
    string withoutExtension = Path.GetFileNameWithoutExtension(baseFileRecord.OriginalName);
    return dbObjectfileRecord.OriginalName.Contains(withoutExtension);
  }

  private string RenameDerivedFile(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    DBObjectFileEntry dbObjectFileRecord,
    DBObjectGraphVertex baseVertex,
    DBObjectFileEntry baseFileRecord)
  {
    string path = this.RenameFile(session, baseVertex, baseFileRecord);
    string withoutExtension1 = Path.GetFileNameWithoutExtension(baseFileRecord.OriginalName);
    string withoutExtension2 = Path.GetFileNameWithoutExtension(path);
    return dbObjectFileRecord.OriginalName.Replace(withoutExtension1, withoutExtension2);
  }

  private string TryGetStringAttributeValue(DBObjectGraphVertex dbObjectVertex, int attributeId)
  {
    DBObjectAttributeEntry objectAttributeEntry = CollectionUtils.Find<DBObjectAttributeEntry>((IEnumerable<DBObjectAttributeEntry>) dbObjectVertex.Attributes, (Predicate<DBObjectAttributeEntry>) (x => x.AttributeId == attributeId));
    return objectAttributeEntry != null ? Convert.ToString(objectAttributeEntry.NewValues[0]) : (string) null;
  }

  public void PrepareDocumentParametersToWrite(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (dbObjectVertex == null)
      throw new ArgumentNullException(nameof (dbObjectVertex));
    if (virtualContainerSet == null)
      throw new ArgumentNullException(nameof (virtualContainerSet));
    this.DoPrepareDocumentParametersToWrite(session, dbObjectVertex, virtualContainerSet);
  }

  protected virtual void DoPrepareDocumentParametersToWrite(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
  }

  private sealed class SplittedPath
  {
    public SplittedPath(string relativeDir, string fileNameOnly, string @extension)
    {
      this.Directory = relativeDir;
      this.FileNameOnly = fileNameOnly;
      this.Extension = @extension;
    }

    public static CADHeuristics.SplittedPath FromPath(string path)
    {
      string directoryName = Path.GetDirectoryName(path);
      string withoutExtension = Path.GetFileNameWithoutExtension(path);
      string str = Path.GetExtension(path);
      string fileNameOnly = withoutExtension;
      string @extension = str;
      return new CADHeuristics.SplittedPath(directoryName, fileNameOnly, @extension);
    }

    public string Directory { get; }

    public string FileNameOnly { get; }

    public string Extension { get; }
  }
}
