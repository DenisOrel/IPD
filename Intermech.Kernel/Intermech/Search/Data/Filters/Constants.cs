// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.Constants
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Search.Data.Filters;

internal static class Constants
{
  public static readonly Guid RevisionInstantiationModeAttributeTypeGuid = new Guid("cadd9609-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid CompositionFilterResultAttributeTypeGuid = new Guid("cad001f0-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid VersionIDInCompositionAttributeTypeGuid = new Guid("cad001c2-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid StoredExplicitPartVersionIDAttributeTypeGuid = new Guid("cadd955d-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjectVisibilityAttributeTypeGuid = new Guid("cad0062f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ApplicabilityInSeriesAndDatesAttributeTypeGuid = new Guid("cadd940c-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SelectVersionRuleObjectTypeGuid = new Guid("cad001b3-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjectOwnerAttributeTypeGuid = new Guid("cad0002f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ObjectOwnerUserGroupVersionGuid = new Guid("cad00059-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SortingAttributeTypeGuid = new Guid("cad00202-306c-11d8-b4e9-00304f19f545");
  private static bool _isInitialized;
  private static int _compositionFilterResultAttributeTypeID = 0;
  private static string _compositionFilterResultAttributeTypeName = string.Empty;
  private static int _versionIDInCompositionAttributeTypeID = 0;
  private static int _objectVisibilityAttributeTypeID = 0;
  private static int _applicabilityInSeriesAndDatesAttributeTypeID = 0;
  private static int _selectVersionRuleObjectTypeID = -1;
  private static int _objectOwnerAttributeTypeID = 0;
  private static int _sortingAttributeTypeID = 0;

  public static int RevisionInstantiationModeAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID(Constants.RevisionInstantiationModeAttributeTypeGuid);
  }

  public static int CompositionFilterResultAttributeTypeID
  {
    get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._compositionFilterResultAttributeTypeID;
    }
  }

  public static string CompositionFilterResultAttributeTypeName
  {
    get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._compositionFilterResultAttributeTypeName;
    }
  }

  public static int VersionIDInCompositionAttributeTypeID
  {
    get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._versionIDInCompositionAttributeTypeID;
    }
  }

  public static int StoredExplicitPartVersionIDAttributeTypeID
  {
    get
    {
      return Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.StoredExplicitPartVersionIDAttributeTypeGuid);
    }
  }

  public static int ObjectVisibilityAttributeTypeID
  {
    get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._objectVisibilityAttributeTypeID;
    }
  }

  public static int ApplicabilityInSeriesAndDatesAttributeTypeID
  {
    get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._applicabilityInSeriesAndDatesAttributeTypeID;
    }
  }

  public static int SelectVersionRuleObjectTypeID
  {
    get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._selectVersionRuleObjectTypeID;
    }
  }

  public static int ObjectOwnerAttributeTypeID
  {
    get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._objectOwnerAttributeTypeID;
    }
  }

  public static int SortingAttributeTypeID
  {
    get
    {
      Constants.InitializeIfNotInitialized();
      return Constants._sortingAttributeTypeID;
    }
  }

  private static void InitializeIfNotInitialized()
  {
    if (Constants._isInitialized)
      return;
    Constants.Initialize();
  }

  private static void Initialize()
  {
    Constants._compositionFilterResultAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.CompositionFilterResultAttributeTypeGuid);
    Constants._compositionFilterResultAttributeTypeName = Constants.GetAttributeTypeName4AttributeTypeGuid(Constants.CompositionFilterResultAttributeTypeGuid);
    Constants._versionIDInCompositionAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.VersionIDInCompositionAttributeTypeGuid);
    Constants._objectVisibilityAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.ObjectVisibilityAttributeTypeGuid);
    Constants._applicabilityInSeriesAndDatesAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.ApplicabilityInSeriesAndDatesAttributeTypeGuid);
    Constants._objectOwnerAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.ObjectOwnerAttributeTypeGuid);
    Constants._sortingAttributeTypeID = Constants.GetAttributeTypeID4AttributeTypeGuid(Constants.SortingAttributeTypeGuid);
    Constants._isInitialized = true;
  }

  private static int GetAttributeTypeID4AttributeTypeGuid(Guid guid)
  {
    return MetaDataHelper.GetAttributeTypeID(guid);
  }

  private static string GetAttributeTypeName4AttributeTypeGuid(Guid guid)
  {
    return MetaDataHelper.GetAttributeTypeName(guid);
  }

  private static int GetObjectTypeID4ObjectTypeGuid(Guid guid)
  {
    return MetaDataHelper.GetObjectTypeID(guid);
  }
}
