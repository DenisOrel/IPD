// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportArgs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices;

internal class ImportArgs
{
  public IUserSession Session;
  public ITransferedObject Unit;
  public string Path;
  public Dictionary<Guid, ImportedInfo> Links;
  public long UserID;
  public IEventLogHelper EventHelper;
  public Guid UserGuid;
  public List<long> UpdateFolderKeyObjects;
  public List<Tuple<long, Guid, long>> ChangesGroupNums;
  public List<Tuple<Guid, Guid, long, List<Guid>>> Contexts;
  public List<Tuple<Guid, List<Guid>>> ImportedCompositions;
  public Dictionary<long, Guid> ParentVersions;

  public ImportArgs(
    IUserSession session,
    ITransferedObject unit,
    string path,
    Dictionary<Guid, ImportedInfo> links,
    long userID,
    Guid userGuid,
    IEventLogHelper eventHelper,
    List<long> updateFolderKeyObjects,
    List<Tuple<long, Guid, long>> changesGroupNums,
    List<Tuple<Guid, Guid, long, List<Guid>>> contexts,
    List<Tuple<Guid, List<Guid>>> importedCompositions,
    Dictionary<long, Guid> parentVersions)
  {
    this.Session = session;
    this.Unit = unit;
    this.Path = path;
    this.Links = links;
    this.UserID = userID;
    this.UserGuid = userGuid;
    this.UpdateFolderKeyObjects = updateFolderKeyObjects;
    this.EventHelper = eventHelper;
    this.ChangesGroupNums = changesGroupNums;
    this.Contexts = contexts;
    this.ImportedCompositions = importedCompositions;
    this.ParentVersions = parentVersions;
  }
}
