// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadApplication
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using ImSSP;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechAcad;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.TechAcad.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.TechAcad.Connector;

[ComVisible(true)]
[Guid("995DB66E-EF7B-4E8E-B1A9-772B9479B6C5")]
[ProgId("TPDesign.Application")]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IApplication))]
public sealed class TechAcadApplication : SingleThreadedObject, IApplication
{
  internal static long GetTechObject(NavWindow window, TechAcadApplication.TreeNodeMode mode)
  {
    try
    {
      if (window?.TreeView == null)
        return 0;
    }
    catch (Exception ex)
    {
      return 0;
    }
    switch (mode)
    {
      case TechAcadApplication.TreeNodeMode.Root:
        NavigatorTreeNode rootNode = window.TreeView.RootNode;
        if (rootNode?.NodeID == null || rootNode.NodeID.CategoryID != 1)
          return 0;
        INode rootHandler = window.TreeView.RootHandler;
        if (rootHandler.GetData(rootNode.NodeID, typeof (IDBObjectTypeID)) is IDBObjectTypeID data1 && TechCardConsts.Utils.IsTechcardObjectType((object) data1.Value) && rootHandler.GetData(rootNode.NodeID, typeof (IDBObjectID)) is IDBObjectID data2)
          return data2.Value;
        break;
      case TechAcadApplication.TreeNodeMode.Current:
        return window.TreeView.FocusedNode == null || !(window.TreeView.FocusedItem.GetItemData(typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || !TechCardConsts.Utils.IsTechcardObjectType((object) itemData.ObjectType) ? 0L : itemData.ObjectID;
    }
    return 0;
  }

  internal static long GetActiveTechObj(NavWindow window)
  {
    return TechAcadApplication.GetTechObject(window, TechAcadApplication.TreeNodeMode.Current);
  }

  internal static long GetRootTechObj(NavWindow window)
  {
    return TechAcadApplication.GetTechObject(window, TechAcadApplication.TreeNodeMode.Root);
  }

  public ITPObject ActiveTPObject
  {
    get
    {
      try
      {
        DockManager service = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
        if (service == null)
          return (ITPObject) null;
        NavWindow activeDockControl = service.ActiveDockControl as NavWindow;
        long activeTechObj = TechAcadApplication.GetActiveTechObj(activeDockControl);
        return activeTechObj == 0L ? (ITPObject) null : (ITPObject) new TechAcadTPObject(activeTechObj, activeDockControl);
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19143.ssp_techacad_19144() + (object) ex);
        throw;
      }
    }
    set
    {
      try
      {
        if (value == null)
          return;
        value.Active = 1;
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19143.ssp_techacad_19145() + (object) ex);
        throw;
      }
    }
  }

  public string GetSettingParams
  {
    get
    {
      try
      {
        TechAcadParamsItem techAcadParamsItem = new TechAcadParamsItem();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          ITechAcadParamsService service = ServiceUtils.GetService<ITechAcadParamsService>((object) sessionKeeper.Session, false);
          TechAcadParamsHelper.LoadData(techAcadParamsItem, sessionKeeper.Session, service, true);
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Editor_Params=" + techAcadParamsItem.Params);
        stringBuilder.Append(';');
        stringBuilder.Append("Editor_Program=" + techAcadParamsItem.ApplPath);
        stringBuilder.Append(';');
        stringBuilder.Append("File_Prototype=" + techAcadParamsItem.PrototypeDraft);
        stringBuilder.Append(';');
        stringBuilder.Append("Replace_Extent=" + techAcadParamsItem.FileExtention);
        stringBuilder.Append(';');
        stringBuilder.Append("Working_Dir=" + ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, true).GetWorkingDirPath());
        return stringBuilder.ToString();
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19143.ssp_techacad_19146() + (object) ex);
        throw;
      }
    }
  }

  public int Loaded => 1;

  public ITPObjectCollection ObjCollection
  {
    get
    {
      try
      {
        TechAcadTPObjectList objCollection = new TechAcadTPObjectList();
        DockManager service = ServiceUtils.GetService<DockManager>((object) ApplicationServices.Container, false);
        if (service == null)
          return (ITPObjectCollection) objCollection;
        List<long> longList = new List<long>();
        foreach (DockControl dockControl in service.GetDockControls())
        {
          if ((dockControl is NavWindow navWindow ? navWindow.TreeView : (NavigatorTreeView) null) != null)
          {
            long rootTechObj = TechAcadApplication.GetRootTechObj(navWindow);
            if (rootTechObj != 0L && !longList.Contains(rootTechObj))
            {
              objCollection.Items.Add(new TechAcadTPObject(rootTechObj, navWindow));
              longList.Add(rootTechObj);
            }
          }
        }
        return (ITPObjectCollection) objCollection;
      }
      catch (Exception ex)
      {
        Plugin.LogError(sc_19143.ssp_techacad_19147() + (object) ex);
        throw;
      }
    }
  }

  public int Version => 2;

  public void SetInterfaceObject(object io)
  {
    if (io == null)
      return;
    try
    {
      Intermech.TechAcad.Connector.TechAcad.SetInterfaceObject(io);
    }
    catch (Exception ex)
    {
      Plugin.LogError(sc_19143.ssp_techacad_19148() + (object) ex);
      throw;
    }
  }

  public IDraftObject GetDraftByFileName(string fileName)
  {
    long pictureObject = ServiceUtils.GetService<ITechAcadService>((object) ApplicationServices.Container, false).GetPictureObject(fileName);
    if (pictureObject == 0L)
      return (IDraftObject) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(pictureObject);
      if (MetaDataHelper.IsObjectTypeChildOf(TechCardConsts.ObjectTypes.DraftCadmechID, objectInfo.ObjectTypeID))
        return (IDraftObject) new TechAcadDraftObject(new ObjInfoItem(objectInfo.ObjectID, objectInfo.ObjectTypeID), (NavWindow) null);
      return MetaDataHelper.IsObjectTypeChildOf(MetaDataHelper.GetObjectTypeID(TechAcadConsts.ObjTypeAcadDraft), objectInfo.ObjectTypeID) ? (IDraftObject) new TechAcadArtDraftObject(new ObjInfoItem(objectInfo.ObjectID, objectInfo.ObjectTypeID), (NavWindow) null) : (IDraftObject) null;
    }
  }

  public string ApplicationName => "Intermech.TechAcad.Connector";

  public static ITPObject GetTpObject(long objectId, NavWindow navWindow)
  {
    return (ITPObject) new TechAcadTPObject(objectId, navWindow);
  }

  public static ITPObject GetTpObject(ObjInfoItem objectItem, NavWindow navWindow)
  {
    return !((TypedInfoItem) objectItem != (TypedInfoItem) null) ? (ITPObject) null : (ITPObject) new TechAcadTPObject(objectItem.ObjectID, navWindow);
  }

  public static IDraftObject GetDraftObject(ObjInfoItem objectItem, NavWindow navWindow)
  {
    return !((TypedInfoItem) objectItem != (TypedInfoItem) null) ? (IDraftObject) null : (IDraftObject) new TechAcadDraftObject(objectItem, navWindow);
  }

  internal enum TreeNodeMode
  {
    Root,
    Current,
  }
}
