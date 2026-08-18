// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.TaskInfo
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.Pdm.Server;

internal class TaskInfo
{
  public bool seekChilds;
  public long Id;
  public Thread thread;
  public RelVisState state;
  public DataTable[] result;
  internal long projId;
  internal long projVId;
  internal string filtOwnerId;
  internal ICompositionsAutosortRule rule;
  internal int objType;
  internal Guid userSession;
  internal HybridDictionary dict;
  internal bool showHiddenObjs;
  internal bool showHiddenSostav;
  internal int levels;

  public TaskInfo(bool childs, long id)
  {
    this.seekChilds = childs;
    this.Id = id;
    this.thread = (Thread) null;
    this.state = RelVisState.Unknown;
    this.result = (DataTable[]) null;
  }

  public void SetParms(
    long projId,
    string fOwnId,
    ICompositionsAutosortRule r,
    int oType,
    Guid uSession,
    HybridDictionary dict,
    long projVId = -1,
    int levs = -1,
    bool hiddObjs = false,
    bool hiddSost = false)
  {
    this.projId = projId;
    this.filtOwnerId = fOwnId;
    this.rule = r;
    this.objType = oType;
    this.userSession = uSession;
    this.dict = dict;
    this.projVId = projVId;
    this.showHiddenObjs = hiddObjs;
    this.showHiddenSostav = hiddSost;
    this.levels = levs;
  }
}
