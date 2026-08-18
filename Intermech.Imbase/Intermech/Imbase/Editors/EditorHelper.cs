// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.EditorHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Editors;

internal static class EditorHelper
{
  internal static List<TableEditor> _editors = new List<TableEditor>();

  internal static void Initialize(IServiceProvider _container)
  {
    if (!(_container.GetService(typeof (IContentProvider)) is IContentProvider service))
      return;
    service.ContentCallback += new GetContentCallback(EditorHelper.ContentProvider_ContentCallback);
  }

  private static DockControl ContentProvider_ContentCallback(Guid guid, string persistString)
  {
    if (guid.Equals(new Guid("3c867640-5326-4b43-9479-d82a8a02f876")))
    {
      string[] strArray = persistString.Split(',');
      long result1;
      long.TryParse(strArray[0], out result1);
      long result2 = -1;
      if (strArray.Length > 1)
        long.TryParse(strArray[1], out result2);
      int relationTypeId = MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545");
      if (result1 != 0L)
        return (DockControl) EditorHelper.CreateEditor(result1, result2, relationTypeId);
    }
    return (DockControl) null;
  }

  internal static TableEditor CreateEditor(long targetId, long parentID, int relationTypeID)
  {
    long num1 = -1;
    long num2 = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(targetId);
      if (objectInfo.Empty && targetId < 0L)
      {
        targetId = -targetId;
        objectInfo = sessionKeeper.Session.GetObjectInfo(targetId);
      }
      int objectTypeId = objectInfo.ObjectTypeID;
      if (ImbaseHelper.IsTable(objectTypeId))
      {
        num1 = targetId;
      }
      else
      {
        if (!ImbaseHelper.IsTableRef(objectTypeId))
          throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString(sc_7755.ssp_imbase_7756()), (object) objectInfo.Caption, (object) targetId, (object) objectTypeId));
        num1 = sessionKeeper.Session.GetObject(targetId).GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID).AsInteger;
        num2 = targetId;
      }
    }
    TableEditor editor = EditorHelper.FindEditor(num1);
    if (editor == null)
    {
      editor = new TableEditor(parentID, relationTypeID);
      DockManager service = ServicesManager.GetService(typeof (DockManager)) as DockManager;
      editor.Manager = service;
      editor.Initialize(num1, num2);
      if (num2 != -1L)
        RecentObjectsNode.MRUObjects.Add(num2, ObjectAction.Open, DateTime.UtcNow);
      else
        RecentObjectsNode.MRUObjects.Add(num1, ObjectAction.Open, DateTime.UtcNow);
      EditorHelper.AddEditor(editor);
    }
    else
      editor.LinkId = num2;
    return editor;
  }

  private static void AddEditor(TableEditor editor) => EditorHelper._editors.Add(editor);

  private static TableEditor FindEditor(long tableId)
  {
    int count = EditorHelper._editors.Count;
    for (int index = 0; index < count; ++index)
    {
      if (EditorHelper._editors[index]._tableId == tableId)
        return EditorHelper._editors[index];
    }
    return (TableEditor) null;
  }

  internal static IImbaseServer GetServer(IUserSession session)
  {
    return session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
  }
}
