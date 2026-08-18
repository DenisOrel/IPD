// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.EmbeddedStandardSizesLibrary
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Localization;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class EmbeddedStandardSizesLibrary(
  IPDMSystemContext pdmSystemContext,
  IIntegrator integrator,
  ArticleLocatorBuilder articleLocatorBuilder) : PDMStandardLibrary(pdmSystemContext, integrator, articleLocatorBuilder)
{
  protected override string DoBeginUpdatePart(string partNameOrKey, string modelFileName)
  {
    string absolutePath = this.ConvertModelFileNameToAbsolutePath(modelFileName);
    string ownerId = VersionsRuleSources.GetEditorRule().OwnerId;
    long num = StandardLibraryServices.FindModel((IServiceProvider) this.integrator, absolutePath, ownerId);
    if (num == 0L)
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("Tools.Client_129"), (object) Path.Combine(StandardLibraryServices.GetModelFolderName((IServiceProvider) this.integrator), Path.GetFileName(modelFileName))));
    if (num > 0L)
      num = this.CheckoutModel(num);
    if (this.Log != null)
      this.Log.Write($"The standard part model '{modelFileName}' is found in the IPS database with ObjectID={num}");
    this.fileVault.WorkArea.Publish((IList<DBObjectState>) this.fileVault.DBObjectsInfo.CreateStateListForSingleObject(num), (IReplaceFilePolicy) new ConfirmAnyRefresh());
    return absolutePath;
  }

  protected override void DoEndUpdatePart(string partNameOrKey, string modelFileName)
  {
    string absolutePath = this.ConvertModelFileNameToAbsolutePath(modelFileName);
    string ownerId = VersionsRuleSources.GetEditorRule().OwnerId;
    if (!File.Exists(absolutePath))
      return;
    this.EmbedStandardSizePart(partNameOrKey, absolutePath, ownerId);
  }

  private void EmbedStandardSizePart(
    string partNameOrKey,
    string modelFilePath,
    string versionsRule)
  {
    NotificationQueue notificationQueue = new NotificationQueue();
    try
    {
      long num1 = StandardLibraryServices.FindModel((IServiceProvider) this.integrator, modelFilePath, versionsRule);
      if (num1 != 0L && num1 < 0L)
      {
        int num2 = this.fileVault.WorkArea.Save(num1) ? 1 : 0;
        num1 = this.CheckinModel(num1);
        this.fileVault.WorkArea.Publish((IList<DBObjectState>) this.fileVault.DBObjectsInfo.CreateStateListForSingleObject(num1), (IReplaceFilePolicy) new PreserveAnyFile());
        if (num2 != 0)
          notificationQueue.QueueEvent((NotificationEventArgs) new CreatedExternallyEventArgs("ObjectsChanged", num1));
      }
      ObjectLocatorResult imbaseObject = this.GetImbaseObject(partNameOrKey);
      if (imbaseObject != null)
      {
        Tuple<long, bool> model = StandardLibraryServices.LinkPartToModel(imbaseObject.ObjectId, num1);
        if (model.Item2)
          notificationQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", model.Item1));
        if (this.Log == null)
          return;
        this.Log.Write($"The standard part with ObjectID={imbaseObject.ObjectId} is linked to its model with RelationID={model.Item1}");
      }
      else
      {
        if (this.Log == null)
          return;
        this.Log.Write($"The standard part is not found in the IPS database by '{partNameOrKey}'", EventLogItemType.Warning);
      }
    }
    finally
    {
      notificationQueue.FlushQueue();
    }
  }

  private long CheckoutModel(long modelId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(modelId, true).CheckOut().ObjectID;
  }

  private long CheckinModel(long modelId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(modelId, true);
      dbObject.CheckIn();
      return dbObject.ObjectID;
    }
  }

  private long RevertModel(long modelId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(modelId, true);
      dbObject.CancelChanges();
      return dbObject.ObjectID;
    }
  }
}
