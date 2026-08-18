// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Setup.BlankSetupObjectCreator
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.DataFormats;
using Intermech.Document.Client;
using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Setup;

public class BlankSetupObjectCreator : IObjectCreatorRiderCustomService, IObjectCreatorCustomService
{
  private long _templateObjectId;
  private Dictionary<ObjectCreatePages, bool> _visiblePages;

  private long SelectDocumentTemplate()
  {
    DescriptorCollection descriptors = new DescriptorCollection()
    {
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(DocIDCache.ObjType_ImDocTemplate)
    };
    IDescriptor rootDescriptor = descriptors.Count == 1 ? descriptors[0] : (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("TechCard.Document_011"), descriptors);
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("TechCard.Document_010"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.Default);
    if (objArray == null || objArray.Length == 0)
      return 0;
    IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) objArray[0];
    ImDocument imDocument = DocumentEditorPlugin.LoadDocumentFromDBObject(dbTypedObjectId.ObjectID);
    if (imDocument != null && !imDocument.IsTemplate)
      throw new Exception(LocalizationHolder.rm.GetString("TechCard.Document_012"));
    return dbTypedObjectId.ObjectID;
  }

  public bool AcceptDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    this._templateObjectId = templateObjectId;
    return false;
  }

  public bool AfterCreate(long newObjectId)
  {
    if (this._templateObjectId != 0L && this._templateObjectId != -1L)
      return true;
    long objectID = this.SelectDocumentTemplate();
    if (objectID == 0L)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(newObjectId, false);
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
      dbObject.SetAttributesValues(new List<AttributeValues>()
      {
        new AttributeValues(MetaDataHelper.GetAttributeID((object) BlankConsts.Template.TemplateGuid), (object) objectInfo.VersionGuid),
        new AttributeValues(MetaDataHelper.GetAttributeID((object) "cad00020-306c-11d8-b4e9-00304f19f545"), (object) objectInfo.Caption)
      }.ToArray());
      return DocumentConfigLoader.Load(dbObject.ObjectID, sessionKeeper.Session).Template != null;
    }
  }

  public IDictionary<ObjectCreatePages, bool> VisiblePages
  {
    get
    {
      Dictionary<ObjectCreatePages, bool> visiblePages1 = this._visiblePages;
      if (visiblePages1 != null)
        return (IDictionary<ObjectCreatePages, bool>) visiblePages1;
      Dictionary<ObjectCreatePages, bool> dictionary = new Dictionary<ObjectCreatePages, bool>();
      dictionary.Add(ObjectCreatePages.FileAttributes, true);
      dictionary.Add(ObjectCreatePages.Properties, true);
      dictionary.Add(ObjectCreatePages.Template, true);
      Dictionary<ObjectCreatePages, bool> visiblePages2 = dictionary;
      this._visiblePages = dictionary;
      return (IDictionary<ObjectCreatePages, bool>) visiblePages2;
    }
  }

  public bool OnCommitAction(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public bool OnBeforeCommitAction(IUserSession session, IDBObject newObject) => true;

  public bool OnCancelAction(
    IUserSession session,
    long newObjectId,
    List<NotificationEventArgs> nea)
  {
    return true;
  }

  public Dictionary<UserControl, int> AddPages(object createdObject, int propPageIndex)
  {
    return (Dictionary<UserControl, int>) null;
  }

  public long CreateObjectDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    return 0;
  }

  internal static void Register(IObjectCreatorService service)
  {
    service.RegisterCreatorCustomService(BlankConsts.ObjectType.BlankSetupId, typeof (BlankSetupObjectCreator));
  }

  internal static void UnRegister(IObjectCreatorService service)
  {
    service.UnregisterCreatorCustomService(BlankConsts.ObjectType.BlankSetupId, typeof (BlankSetupObjectCreator));
  }
}
