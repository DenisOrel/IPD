// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.DBObjectAttributesToFileWriter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using Intermech.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal sealed class DBObjectAttributesToFileWriter : ServiceExtender
{
  private INotificationService notificationService;
  private IFileVault fileVaultService;
  private Lazy<IOutputView> outputView;

  public DBObjectAttributesToFileWriter(
    INotificationService notificationService,
    IFileVault fileVaultService,
    Lazy<IOutputView> outputView)
  {
    this.notificationService = notificationService;
    this.fileVaultService = fileVaultService;
    this.outputView = outputView;
  }

  protected override void DoEnable()
  {
    base.DoEnable();
    this.notificationService.Subscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectsAttributesChanged));
    this.notificationService.Subscribe("ObjectsCreated", new NotificationEventHandler(this.OnObjectsCreated));
  }

  protected override void DoDisable()
  {
    base.DoDisable();
    this.notificationService.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.OnObjectsAttributesChanged));
    this.notificationService.Unsubscribe("ObjectsCreated", new NotificationEventHandler(this.OnObjectsCreated));
  }

  private void OnObjectsCreated(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || e is CreatedExternallyEventArgs)
      return;
    for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
    {
      long objectId = objectsEventArgs.ObjectIDs[index];
      if (this.ValidateObject(objectId, (Predicate<IDBObject>) (dbObj => dbObj != null), (Predicate<IDBObject>) (dbObj => this.fileVaultService.DBObjectsInfo.GetObjectState(dbObj).IsEditableState)))
      {
        IEmbedAttributesService attributesService = this.FindEmbedAttributesService(objectsEventArgs.ObjectTypeIDs[index] != -1 ? objectsEventArgs.ObjectTypeIDs[index] : DBHelper.GetObjectType(objectId));
        if (attributesService != null)
        {
          try
          {
            AttributeValues[] objectAttributes = this.GetCreatedObjectAttributes(objectId);
            if (objectAttributes != null)
            {
              if (objectAttributes.Length != 0)
                this.EmbedAttributeValues(attributesService, objectId, (IList<AttributeValues>) objectAttributes);
            }
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
        }
      }
    }
  }

  private AttributeValues[] GetCreatedObjectAttributes(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, true);
      if (dbObject.VersionID > 0 && dbObject.ObjectVerType == -1)
        return new AttributeValues[0];
      AttributeValues[] attributesValues1 = dbObject.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckWriteAccess);
      if (dbObject.ParentVersionID == -1L || dbObject.ParentVersionID == 0L)
        return attributesValues1;
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(dbObject.ParentVersionID, true);
      if (dbObject.ObjectType != objectActualCopy.ObjectType)
        return attributesValues1;
      AttributeValues[] attributesValues2 = objectActualCopy.GetAttributesValues(GetAttributeValuesModes.IncludeName | GetAttributeValuesModes.IncludeObligatoryAttributes | GetAttributeValuesModes.CheckWriteAccess);
      List<AttributeValues> attributeValuesList = new List<AttributeValues>(attributesValues1.Length);
      foreach (AttributeValues attributeValues in attributesValues1)
      {
        AttributeValues objectAttr = attributeValues;
        AttributeValues other = CollectionUtils.Find<AttributeValues>((IEnumerable<AttributeValues>) attributesValues2, (Predicate<AttributeValues>) (item => item.AttributeID == objectAttr.AttributeID));
        if (other != null)
        {
          if (!objectAttr.Equals(other, true))
            attributeValuesList.Add(objectAttr);
        }
        else
          attributeValuesList.Add(objectAttr);
      }
      return attributeValuesList.ToArray();
    }
  }

  private void OnObjectsAttributesChanged(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsExtendedEventArgs extendedEventArgs) || extendedEventArgs.ObjectIDs.Count != 1 || extendedEventArgs.AttributeValuesArray == null || extendedEventArgs.AttributeValuesArray.Length == 0)
      return;
    long objectId = extendedEventArgs.ObjectIDs[0];
    if (!this.ValidateObject(objectId, (Predicate<IDBObject>) (dbObj => dbObj != null), (Predicate<IDBObject>) (dbObj => !dbObj.IsCreationMode), (Predicate<IDBObject>) (dbObj => this.fileVaultService.DBObjectsInfo.GetObjectState(dbObj).IsEditableState)))
      return;
    IEmbedAttributesService attributesService = this.FindEmbedAttributesService(extendedEventArgs.ObjectTypeIDs[0] != -1 ? extendedEventArgs.ObjectTypeIDs[0] : DBHelper.GetObjectType(objectId));
    if (attributesService == null)
      return;
    try
    {
      this.EmbedAttributeValues(attributesService, objectId, (IList<AttributeValues>) extendedEventArgs.AttributeValuesArray);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private bool ValidateObject(long objectId, params Predicate<IDBObject>[] matches)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      foreach (Predicate<IDBObject> match in matches)
      {
        if (!match(dbObject))
          return false;
      }
    }
    return true;
  }

  private IEmbedAttributesService FindEmbedAttributesService(int objectType)
  {
    DBObjectTypeFileHandlingRules fileHandlingRules = IntegratorServices.GetFileHandlingRules(objectType);
    return fileHandlingRules.IntegratorRef == null || !fileHandlingRules.RequireNormalEditMode ? (IEmbedAttributesService) null : IntegratorServices.GetService<IEmbedAttributesService>(fileHandlingRules.IntegratorRef, false);
  }

  private void EmbedAttributeValues(
    IEmbedAttributesService svc,
    long objectId,
    IList<AttributeValues> attributeValues)
  {
    ProgressSinks.DialogService.Invoke($"Запись атрибутов в {DBHelper.GetObjectNameInMessages(objectId)}", ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink => svc.EmbedAttributeValues(objectId, attributeValues, new EmbedAttributesActionOptions()
    {
      ProgressSink = progressSink
    })));
  }

  public IOutputView OutputView => this.outputView.Value;
}
