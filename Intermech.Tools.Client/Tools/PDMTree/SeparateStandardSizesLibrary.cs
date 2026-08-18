// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.SeparateStandardSizesLibrary
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class SeparateStandardSizesLibrary(
  IPDMSystemContext pdmSystemContext,
  IIntegrator integrator,
  ArticleLocatorBuilder articleLocatorBuilder) : PDMStandardLibrary(pdmSystemContext, integrator, articleLocatorBuilder)
{
  protected override string DoBeginUpdatePart(string partNameOrKey, string modelFileName)
  {
    string absolutePath = this.ConvertModelFileNameToAbsolutePath(modelFileName);
    string ownerId = VersionsRuleSources.GetEditorRule().OwnerId;
    long model = StandardLibraryServices.FindModel((IServiceProvider) this.integrator, absolutePath, ownerId);
    if (model != 0L)
    {
      if (this.Log != null)
        this.Log.Write($"The standard part model '{modelFileName}' is found in the IPS database with ObjectID={model}");
      this.fileVault.WorkArea.Publish((IList<DBObjectState>) this.fileVault.DBObjectsInfo.CreateStateListForSingleObject(model), (IReplaceFilePolicy) new ConfirmAnyRefresh());
    }
    else if (this.Log != null)
      this.Log.Write($"The standard part model '{modelFileName}' is not found in the IPS database. It will be created.");
    return absolutePath;
  }

  protected override void DoEndUpdatePart(string partNameOrKey, string modelFileName)
  {
    string absolutePath = this.ConvertModelFileNameToAbsolutePath(modelFileName);
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    if (!File.Exists(absolutePath))
      return;
    this.CreateStandardSizePart(partNameOrKey, absolutePath, editorRule);
  }

  private void CreateStandardSizePart(
    string partNameOrKey,
    string modelFilePath,
    VersionsRulePackage versionsRule)
  {
    long model1 = StandardLibraryServices.FindModel((IServiceProvider) this.integrator, modelFilePath, versionsRule.OwnerId);
    bool flag = Intermech.Consts.IsUndefinedObjectId(model1);
    ObjectLocatorResult imbaseObject = this.GetImbaseObject(partNameOrKey);
    NotificationQueue notificationQueue = new NotificationQueue();
    try
    {
      if (flag)
      {
        string str = Path.Combine(StandardLibraryServices.GetModelFolderName((IServiceProvider) this.integrator), Path.GetFileName(modelFilePath));
        model1 = StandardLibraryServices.CreateModel((IServiceProvider) this.integrator, StandardLibraryServices.GetModelType((IServiceProvider) this.integrator).Id, this.GetModelDesignation(imbaseObject), this.GetModelName(str, imbaseObject), str, modelFilePath);
        this.fileVault.WorkArea.Attach(model1);
        notificationQueue.QueueEvent((NotificationEventArgs) new CreatedExternallyEventArgs("ObjectsCreated", model1));
        if (this.Log != null)
          this.Log.Write($"The standard part model '{str}' is created with ObjectID={model1}");
      }
      if (imbaseObject != null)
      {
        Tuple<long, bool> model2 = StandardLibraryServices.LinkPartToModel(imbaseObject.ObjectId, model1);
        if (model2.Item2)
          notificationQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", model2.Item1));
        if (this.Log != null)
          this.Log.Write($"The standard part with ObjectID={imbaseObject.ObjectId} is linked to its model with RelationID={model2.Item1}");
      }
      else if (this.Log != null)
        this.Log.Write($"The standard part is not found in the IPS database by '{partNameOrKey}'", EventLogItemType.Warning);
      try
      {
        if (!flag || !this.CanCreateIMViewerObject())
          return;
        this.CreateIMViewerObject(model1, versionsRule);
      }
      catch (Exception ex)
      {
        if (this.Log == null)
          return;
        this.Log.Write(ex.Message);
      }
    }
    finally
    {
      notificationQueue.FlushQueue();
    }
  }

  private string GetModelDesignation(ObjectLocatorResult partInfo = null)
  {
    string modelDesignation = string.Empty;
    if (partInfo != null)
    {
      string partDesignation = this.TryGetPartDesignation(partInfo);
      if (!string.IsNullOrEmpty(partDesignation))
        modelDesignation = partDesignation;
    }
    return modelDesignation;
  }

  private string GetModelName(string modelRelativePath, ObjectLocatorResult partInfo = null)
  {
    string modelName = modelRelativePath;
    if (partInfo != null)
    {
      string partName = this.TryGetPartName(partInfo);
      if (!string.IsNullOrEmpty(partName))
        modelName = $"{partName} ({modelRelativePath})";
    }
    return modelName;
  }

  private string TryGetPartDesignation(ObjectLocatorResult partInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(partInfo.ObjectId, false);
      if (dbObject == null)
        return (string) null;
      IDBAttribute attributeById = dbObject.GetAttributeByID(IDCache.Default.Designation.Id);
      return attributeById == null || attributeById.IsNull ? (string) null : attributeById.AsString;
    }
  }

  private string TryGetPartName(ObjectLocatorResult partInfo)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(partInfo.ObjectId, false);
      if (dbObject == null)
        return (string) null;
      IDBAttribute attributeById = dbObject.GetAttributeByID(IDCache.Default.Name.Id);
      return attributeById == null || attributeById.IsNull ? (string) null : attributeById.AsString;
    }
  }

  private bool CanCreateIMViewerObject()
  {
    return this.pdmSystemContext.IMViewerClientService.Settings.EnableIntegration && ServiceUtils.GetService<ICADSettingsService>((object) this.integrator, true).GetCADSettings().EnableIMViewerFiles;
  }

  private void CreateIMViewerObject(long modelId, VersionsRulePackage versionsRule)
  {
    int id = StandardLibraryServices.GetModelType((IServiceProvider) this.integrator).Id;
    using (CADApiSession cadApiSession = new CADApiSession(this.integrator))
    {
      CADSystemProxy application = cadApiSession.Application;
      IList<ErrorInfo> updateViewerObject = this.pdmSystemContext.IMViewerClientService.CreateOrUpdateViewerObject(modelId, id, versionsRule, application, false);
      if (updateViewerObject.Count == 0 || this.Log == null)
        return;
      foreach (ErrorInfo errorInfo in (IEnumerable<ErrorInfo>) updateViewerObject)
        this.Log.Write(errorInfo.Message);
    }
  }
}
