// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.DraftDocuments.CommandsProvider
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Client.DraftDocuments;

internal sealed class CommandsProvider : ICommandsProvider
{
  private IDraftDocumentsService draftDocumentsService;
  private IFileVault fileVaultService;
  private IFileImportService fileImportService;

  public CommandsProvider(
    IDraftDocumentsService draftDocumentsService,
    IFileVault fileVaultService,
    IFileImportService fileImportService)
  {
    if (draftDocumentsService == null)
      throw new ArgumentNullException(nameof (draftDocumentsService));
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (fileImportService == null)
      throw new ArgumentNullException(nameof (fileImportService));
    this.draftDocumentsService = draftDocumentsService;
    this.fileVaultService = fileVaultService;
    this.fileImportService = fileImportService;
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    mergedCommands.Add("OpenDocument", new CommandInfo(0, new ClickEventHandler(this.OpenDocumentHandler)));
    mergedCommands.Suppress("EditDocument", 0);
    mergedCommands.Suppress("ViewDocument", 0);
    mergedCommands.Suppress("PrintDocument", 0);
    mergedCommands.Suppress("OpenWith", 0);
    mergedCommands.Add("ConvertToDocument", new CommandInfo(0, new ClickEventHandler(this.ConvertToDocumentHandler)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  private void OpenDocumentHandler(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    for (int index = 0; index < items.Count; ++index)
      this.TryOpenDraftDocumentFile(((IDBObjectID) items.GetItemData(index, typeof (IDBObjectID))).Value);
  }

  private void TryOpenDraftDocumentFile(long draftDocumentId)
  {
    string externalFilePath = this.TryGetExternalFilePath(draftDocumentId);
    if (externalFilePath == null)
      return;
    ProcessStartInfo startInfo = new ProcessStartInfo(externalFilePath);
    startInfo.UseShellExecute = true;
    try
    {
      Process.Start(startInfo)?.Dispose();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void ConvertToDocumentHandler(
    ISelectedItems items,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    List<string> files = new List<string>(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      string externalFilePath = this.TryGetExternalFilePath(((IDBObjectID) items.GetItemData(index, typeof (IDBObjectID))).Value);
      if (externalFilePath != null)
        files.Add(externalFilePath);
    }
    if (files.Count == 0)
      return;
    this.fileImportService.ImportFiles((ICollection<string>) files, new BatchFileImportOptions()
    {
      NotifyOnMasterFileErrors = true,
      NotifyOnDeferredFilesErrors = true
    });
  }

  private string TryGetExternalFilePath(long draftDocumentId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(draftDocumentId, false);
      if (dbObject == null)
        return (string) null;
      IDBAttribute attributeById = dbObject.GetAttributeByID(this.draftDocumentsService.IdCache.ExternalFilePath.Id);
      if (attributeById == null)
        return (string) null;
      string asString = attributeById.AsString;
      if (string.IsNullOrEmpty(asString))
        return (string) null;
      string path = Path.Combine(this.fileVaultService.WorkArea.AreaPath, asString);
      return !File.Exists(path) ? (string) null : path;
    }
  }
}
