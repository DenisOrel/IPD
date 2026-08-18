// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ObjectLinkEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase;

internal class ObjectLinkEditor : ModalEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (context == null || !(context.Instance is StructureEditorPropGridDescriptor instance))
      return value;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(instance.AttributeGuid);
    if (attributeType == null)
      return value;
    int usersTypeId = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).UsersTypeID;
    SelectionOptions options = SelectionOptions.Default | SelectionOptions.DisableMultiselect;
    if (Convert.ToInt32(attributeType.SizeType) == usersTypeId)
    {
      if (SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1129"), (IDescriptor) new UsersGroupsDescriptor(), typeof (IDBTypedObjectID), options, new int[1]
      {
        usersTypeId
      }) is IDBTypedObjectID[] dbTypedObjectIdArray && dbTypedObjectIdArray.Length != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(dbTypedObjectIdArray[0].ObjectID);
          if (!objectInfo.Empty)
            return (object) objectInfo.VersionGuid;
        }
      }
    }
    else
    {
      IDescriptor descriptor = (IDescriptor) new ObjectTypesNodeDescriptor();
      IDescriptor rootDescriptor = attributeType.SizeType >= 0L ? (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Convert.ToInt32(attributeType.SizeType)) : descriptor;
      if (SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1130"), rootDescriptor, typeof (IDBObjectID), options) is IDBObjectID[] dbObjectIdArray && dbObjectIdArray.Length != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(dbObjectIdArray[0].Value);
          if (!objectInfo.Empty)
            return (object) objectInfo.VersionGuid;
        }
      }
    }
    return value == null || value == DBNull.Value || string.IsNullOrEmpty(value.ToString()) ? (object) Guid.Empty : value;
  }
}
