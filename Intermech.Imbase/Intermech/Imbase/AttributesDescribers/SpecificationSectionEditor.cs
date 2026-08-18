// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.SpecificationSectionEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal class SpecificationSectionEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    int objTypeID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objTypeID = sessionKeeper.Session.GetObjectType(new Guid("cad00254-306c-11d8-b4e9-00304f19f545")).ObjectType;
    if (objTypeID == 0)
      return value;
    IDBTypedObjectID[] dbTypedObjectIdArray = (IDBTypedObjectID[]) SelectionWindow.Select(LocalizationHolder.rm.GetString(sc_7648.ssp_imbase_7649()), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objTypeID), typeof (IDBTypedObjectID), (IServiceProvider) null, SelectionOptions.Default);
    if (dbTypedObjectIdArray == null)
      return value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(dbTypedObjectIdArray[0].ObjectID);
      if (dbObject == null)
        return value;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00279-306c-11d8-b4e9-00304f19f545"));
      return attributeByGuid == null ? value : (object) new SPSectionInfo(dbTypedObjectIdArray[0].Caption, attributeByGuid.AsString);
    }
  }
}
