
// Type: Intermech.Search.Constants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search
{
    /// <summary>Константы</summary>
    public static class Constants
    {
      public static readonly Guid ButtonBarsSettingsAttributeTypeGuid = new Guid("cadd969e-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid UserConfigurationObjectTypeGuid = new Guid("cad00045-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid RoleObjectTypeGuid = new Guid("cad00007-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid RoleConfigurationObjectTypeGuid = new Guid("cad00690-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid DefaultCommandsSettingsAttributeTypeGuid = new Guid("cadd968d-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid RoleConfigurationAttributeTypeGuid = new Guid("cad00692-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid SecurityLevelAttributeTypeGuid = new Guid("cad00816-306c-11d8-b4e9-00304f19f545");
      /// <summary>
      /// Глобальный идентификатор типа атрибута "Идентификатор версии на связи"
      /// </summary>
      public static readonly Guid ExplicitPartVersionIDAttributeTypeGuid = new Guid("cad001c2-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid DesignationAttributeTypeGuid = new Guid("cad0001f-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid NameAttributeTypeGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid CountAttributeTypeGuid = new Guid("cad00267-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid FileAttributeTypeGuid = new Guid("cad0004b-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid UserObjectTypeGuid = new Guid("cad00002-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid SortingAttributeTypeGuid = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
      public static readonly string ArchiveStateStreamPrefix = "ArchiveColumnsConfig_";
      public static readonly Guid ArchiveObjectTypeGuid = new Guid("cad0011e-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid SelectionObjectTypeGuid = new Guid("cad00156-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid CommonSelectionObjectTypeGuid = new Guid("cad00122-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid PersonalSelectionObjectTypeGuid = new Guid("cad00123-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid SelectionTypeAttributeTypeGuid = new Guid("cad00158-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid UserGroupObjectTypeGuid = new Guid("cad00003-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ConfigurationFilesAttributeTypeGuid = new Guid("cad014d4-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ObjectTypesGuidsAttributeTypeGuid = new Guid("cad00149-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid SimpleRelationRelationTypeGuid = new Guid("cad00022-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid AssemblyUnitObjectTypeGuid = new Guid("cad00132-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid AssemblyUnitComputerModelObjectTypeGuid = new Guid("cad00768-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ProductObjectTypeGuid = new Guid("cad00268-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ObjectContentModificationDateAttributeTypeGuid = new Guid("cad0013a-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ObjectVersionSelectionRuleObjectTypeGuid = new Guid("cad001b3-306c-11d8-b4e9-00304f19f545");
      private static bool _initialized;
      private static int _explicitPartVersionIDAttributeTypeID = 0;

      public static int ButtonBarsSettignsAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.ButtonBarsSettingsAttributeTypeGuid);
      }

      public static int UserConfigurationObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.UserConfigurationObjectTypeGuid);
      }

      public static int RoleObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.RoleObjectTypeGuid);
      }

      public static int RoleConfigurationObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.RoleConfigurationObjectTypeGuid);
      }

      public static int DefaultCommandsSettingsAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.DefaultCommandsSettingsAttributeTypeGuid);
      }

      public static int RoleConfigurationAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.RoleConfigurationAttributeTypeGuid);
      }

      public static int SecurityLevelAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.SecurityLevelAttributeTypeGuid);
      }

      /// <summary>
      /// Идентификатор типа атрибута "Идентификатор версии на связи"
      /// </summary>
      public static int ExplicitPartVersionIDAttributeTypeID
      {
        get
        {
          Constants.Initialize();
          return Constants._explicitPartVersionIDAttributeTypeID;
        }
      }

      public static int DesignationAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.DesignationAttributeTypeGuid);
      }

      public static int NameAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.NameAttributeTypeGuid);
      }

      public static int CountAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.CountAttributeTypeGuid);
      }

      public static int FileAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.FileAttributeTypeGuid);
      }

      public static int UserObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.UserObjectTypeGuid);
      }

      public static int SortingAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.SortingAttributeTypeGuid);
      }

      public static int ArchiveObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.ArchiveObjectTypeGuid);
      }

      public static int SelectionObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.SelectionObjectTypeGuid);
      }

      public static int CommonSelectionObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.CommonSelectionObjectTypeGuid);
      }

      public static int PersonalSelectionObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.PersonalSelectionObjectTypeGuid);
      }

      public static int SelectionTypeAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.SelectionTypeAttributeTypeGuid);
      }

      public static int UserGroupObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.UserGroupObjectTypeGuid);
      }

      public static int ConfigurationFilesAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.ConfigurationFilesAttributeTypeGuid);
      }

      public static int ObjectTypesGuidsAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(Constants.ObjectTypesGuidsAttributeTypeGuid);
      }

      public static int SimpleRelationRelationTypeID
      {
        get => MetaDataHelper.GetRelationTypeID(Constants.SimpleRelationRelationTypeGuid);
      }

      public static int AssemblyUnitObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.AssemblyUnitObjectTypeGuid);
      }

      public static int AssemblyUnitComputerModelObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.AssemblyUnitComputerModelObjectTypeGuid);
      }

      public static int ProductObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.ProductObjectTypeGuid);
      }

      public static int ObjectContentModificationDateAttributeTypeID
      {
        get
        {
          return MetaDataHelper.GetAttributeTypeID(Constants.ObjectContentModificationDateAttributeTypeGuid);
        }
      }

      public static int ObjectVersionSelectionRuleObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(Constants.ObjectVersionSelectionRuleObjectTypeGuid);
      }

      private static void Initialize()
      {
        if (Constants._initialized)
          return;
        Constants._explicitPartVersionIDAttributeTypeID = ServiceLocator.Get<IIDConverter>().ConvertAttributeTypeGuidToAttributeTypeID(Constants.ExplicitPartVersionIDAttributeTypeGuid);
        Constants._initialized = true;
      }
    }
}
