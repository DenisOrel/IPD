// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.NewSpecificationFormService
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Заменитель стандартного мастера создания объектов</summary>
public class NewSpecificationFormService : 
  IObjectCreatorRiderCustomService,
  IObjectCreatorCustomService
{
  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    if (AVSPlugin.PDMSpecificationsService == null)
      throw new Exception("Не загружен модуль PDM, необходимый для работы AVS");
    long objectDialog = -1;
    if (MetaDataHelper.IsObjectTypeChildOf(ObjectTypeID, AvsIDCache.ObjType_Specification))
    {
      SpecificationCreationMode mode = SpecificationCreationMode.CreateNew;
      if (isVersion)
        mode = SpecificationCreationMode.CreateVersion;
      else if (RelationTypeIDs != null && RelationTypeIDs.Length != 0 && RelatedObjectIDs != null && RelationTypeIDs.Length == RelatedObjectIDs.Length)
        mode = SpecificationCreationMode.CreateInclude;
      else if (TemplateObjectID != -1L)
        mode = SpecificationCreationMode.CreateBySpcTemplate;
      SpecificationCreationParams formParams = new SpecificationCreationParams(TemplateObjectID, mode)
      {
        ObjectTypeId = ObjectTypeID
      };
      formParams.RelationTypeIDs = RelationTypeIDs;
      formParams.RelatedObjectIDs = RelatedObjectIDs;
      switch (NewSpecificationFormAdv.Execute(formParams))
      {
        case DialogResult.OK:
          objectDialog = formParams.NewSpecID;
          if (objectDialog.IsDefinedId() && AVSPlugin.Instance != null && formParams.openInEditor)
          {
            AVSPlugin.Instance.OpenAVSWindow(new OpenAVSDocArgs(objectDialog, formParams.NewSpecObjectType, createUndo: new bool?(false))
            {
              ObjectGuid = formParams.NewSpecObjectGuid
            });
            break;
          }
          break;
        case DialogResult.Cancel:
          if (formParams.NewSpecID.IsDefinedId() && ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service)
          {
            service.FireOnObjectCreatorCanceledEvent(formParams.NewSpecID, isVersion);
            break;
          }
          break;
      }
    }
    return objectDialog;
  }

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public bool AcceptDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return MetaDataHelper.IsObjectTypeChildOf(ObjectTypeID, AvsIDCache.ObjType_Specification);
  }

  public bool AfterCreate(long newObjectID) => true;

  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get => (IDictionary<ObjectCreatePages, bool>) null;
  }

  public bool OnCommitAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public bool OnCancelAction(
    IUserSession session,
    long newObjectID,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public Dictionary<UserControl, int> AddPages(object CreatedObject, int propPageIndex)
  {
    return (Dictionary<UserControl, int>) null;
  }
}
