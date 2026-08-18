
// Type: Intermech.Search.ContextMenus.ContextMenuConstants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.ContextMenus
{
    public static class ContextMenuConstants
    {
      public static readonly Guid ContextMenuObjectTypeGuid = new Guid("cadd99c0-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid SettingsAttributeTypeGuid = new Guid("cad001f1-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ContextMenusRelationTypeGuid = new Guid("cadd99c8-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ObjectTypesGuidsAttributeTypeGuid = new Guid("cad00149-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid SettingsBlobAttributeTypeGuid = new Guid("cadd9bac-306c-11d8-b4e9-00304f19f545");

      public static int ContextMenuObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(ContextMenuConstants.ContextMenuObjectTypeGuid);
      }

      public static int SettingsAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(ContextMenuConstants.SettingsAttributeTypeGuid);
      }

      public static int ContextMenusRelationTypeID
      {
        get => MetaDataHelper.GetRelationTypeID(ContextMenuConstants.ContextMenusRelationTypeGuid);
      }

      public static int ObjectTypesGuidsAttributeTypeID
      {
        get
        {
          return MetaDataHelper.GetAttributeTypeID(ContextMenuConstants.ObjectTypesGuidsAttributeTypeGuid);
        }
      }

      public static int SettingsBlobAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(ContextMenuConstants.SettingsBlobAttributeTypeGuid);
      }
    }
}
