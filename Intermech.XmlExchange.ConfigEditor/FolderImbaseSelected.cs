// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.FolderImbaseSelected
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Imbase.Selection;
using Intermech.Interfaces;
using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class FolderImbaseSelected : UITypeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    XmlExchangeImportImbaseItem oldImbaseItem = (XmlExchangeImportImbaseItem) null;
    if (value != null)
      oldImbaseItem = value as XmlExchangeImportImbaseItem;
    if (oldImbaseItem == null)
      return value;
    XmlExchangeImportImbaseItem importImbaseItem = (XmlExchangeImportImbaseItem) null;
    if (context != null && context.Instance is GridViewSettingsImportImbase instance)
    {
      object obj = instance.GetProperties(new Attribute[0]).Find("Catalog", true)?.GetValue((object) null);
      if (obj != null)
        importImbaseItem = obj as XmlExchangeImportImbaseItem;
    }
    if (importImbaseItem == null || importImbaseItem.СommonGuid == Guid.Empty)
      return value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session == null)
        return value;
      IDBObject dbObject1 = session.GetObject(importImbaseItem.СommonGuid, false);
      if (dbObject1 == null)
        return value;
      long prevSelectedID = 0;
      if (oldImbaseItem.СommonGuid != Guid.Empty)
        prevSelectedID = session.GetObjectInfo(oldImbaseItem.СommonGuid).ObjectID;
      using (ImbaseFilterSelectionWindow filterSelectionWindow = new ImbaseFilterSelectionWindow(new List<long>()
      {
        dbObject1.ObjectID
      }, 0L, prevSelectedID))
      {
        if (filterSelectionWindow.ShowDialog() == DialogResult.OK)
        {
          IDBObject dbObject2 = session.GetObject(filterSelectionWindow.SelectedID, false);
          return dbObject2 == null ? value : (object) new XmlExchangeImportImbaseItem(dbObject2.ObjectGUID, oldImbaseItem);
        }
      }
    }
    return value;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }
}
