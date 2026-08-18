// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertScript
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertScript : ExpertScriptable
{
  public ExpertScript(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this.objType = ExpertScriptType.CommonCalc;
  }

  public override void SaveScript(ScriptTreeNode root, ExpertScriptParms parms = null)
  {
    base.SaveScript(root, parms);
  }

  internal override void UpdateScriptInCache()
  {
    ScriptTreeNode val = ExpertServer.LoadScriptTree(this.xDoc);
    ExpertServer.es.SetValueToCache<long, ScriptTreeNode>(this.ObjectID, val, ExpertServer.es.expertScripts);
    ((IExpertServerSynchronizer) ServerServices.GetService(typeof (IExpertServerSynchronizer)))?.AddEvent(ExpServerCache.cacheScripts, this.ObjectID, 0L, this.UserSession.DataManager);
  }
}
