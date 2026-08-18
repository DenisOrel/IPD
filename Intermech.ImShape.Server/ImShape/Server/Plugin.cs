// Decompiled with JetBrains decompiler
// Type: Intermech.ImShape.Server.Plugin
// Assembly: Intermech.ImShape.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 84375EAE-6601-42D1-857F-8650A0F7FEBA
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ImShape.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using Intermech.Runtime.ComInterop;
using Intermech.Tools.Data;
using Interop.IMShape;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

#nullable disable
namespace Intermech.ImShape.Server;

internal sealed class Plugin : IPackage
{
  private static readonly ComObjectProvider ShapeProvider = (ComObjectProvider) new ClsidProvider(typeof (ShapeComClass).GUID, true);
  private List<int> _allowedTypeIds;
  private int _objPartId = MetaDataHelper.GetObjectTypeID("cad00250-306c-11d8-b4e9-00304f19f545");
  private int _lcDeletedId = MetaDataHelper.GetLCLevelID("cad0000e-306c-11d8-b4e9-00304f19f545");
  private ImShapeBaseObserver _observer = new ImShapeBaseObserver();
  private static readonly object _lockObj = new object();

  public Plugin()
  {
    this._allowedTypeIds = MetaDataHelper.GetObjectTypeChildrenID(new Guid("cad0078f-306c-11d8-b4e9-00304f19f545"));
    this._allowedTypeIds.Add(this._objPartId);
  }

  public void Load(IServiceProvider serviceProvider)
  {
    try
    {
      if (!this.IsImShapeInstalled() || !(ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service))
        return;
      service.BeforeNextLCStepEvent += new NextLCStepHandler(this.BeforeNextLCStepEvent);
      service.CommitEvent += new TransactionHandler(this.EventLogHelper_CommitEvent);
    }
    catch
    {
    }
  }

  public void Unload()
  {
  }

  public string Name => LocalizationHolder.rm.GetString("ImShape.Plugin.Name");

  private bool IsImShapeInstalled()
  {
    try
    {
      return Plugin.ShapeProvider.IsRegistered();
    }
    catch
    {
      return false;
    }
  }

  private void BeforeNextLCStepEvent(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (nextstep.LevelID != this._lcDeletedId)
      return;
    QuickObjectInfo objectInfo = session.GetObjectInfo(sender.ObjectID);
    if (!this._allowedTypeIds.Contains(objectInfo.ObjectTypeID))
      return;
    List<string> removedAtrGuids = new List<string>();
    if (objectInfo.ObjectTypeID == this._objPartId)
    {
      removedAtrGuids.Add(PersistentIds.FromObjectVersion(objectInfo.VersionGuid));
    }
    else
    {
      long[] articles = ServiceUtils.GetService<IArticleService>((object) ServerServices.ServiceContainer, false)?.FindArticles(sender.ObjectID, "cad001df-306c-11d8-b4e9-00304f19f545", (object) session.SessionGUID);
      if (articles != null && articles.Length != 0)
      {
        foreach (long objectID in articles)
        {
          Guid versionGuid = session.GetObjectInfo(objectID).VersionGuid;
          if (!(versionGuid == Guid.Empty))
            removedAtrGuids.Add(PersistentIds.FromObjectVersion(versionGuid));
        }
      }
    }
    if (removedAtrGuids.Count <= 0)
      return;
    this._observer.AddRemovedArticles(session.SessionGUID, (ICollection<string>) removedAtrGuids);
  }

  private void EventLogHelper_CommitEvent(IUserSession session)
  {
    string[] strGuids = this._observer.TakeRemovedArtIds(session.SessionGUID);
    if (strGuids.Length == 0)
      return;
    Thread thread = new Thread((ThreadStart) (() => this.ProcessImShapeArticles((ICollection<string>) strGuids)));
    thread.SetApartmentState(ApartmentState.STA);
    thread.Start();
  }

  private void ProcessImShapeArticles(ICollection<string> strGuids)
  {
    lock (Plugin._lockObj)
    {
      try
      {
        IShapeCom3 o = (IShapeCom3) null;
        try
        {
          // ISSUE: reference to a compiler-generated field
          if (Plugin.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            Plugin.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, IShapeCom3>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (IShapeCom3), typeof (Plugin)));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          o = Plugin.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) Plugin.\u003C\u003Eo__14.\u003C\u003Ep__0, Plugin.ShapeProvider.CreateInstance());
          foreach (string strGuid in (IEnumerable<string>) strGuids)
            o.DeleteModelByPdmArtID(strGuid);
        }
        catch (Exception ex)
        {
          if (!(ServerServices.GetService(typeof (IEventLogHelper)) is IEventLogHelper service))
            return;
          service.AddToTrace($"В время удаления модели из ImShape произошла ошибка: {ex.Message}");
        }
        finally
        {
          if (o != null)
            Marshal.FinalReleaseComObject((object) o);
        }
      }
      catch
      {
      }
    }
  }
}
