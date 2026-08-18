// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeAddins.MSOfficeAddinsConstants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.MSOfficeAddins;

public static class MSOfficeAddinsConstants
{
  public static readonly Guid ObjectsAddedByReferenceRelationTypeGuid = new Guid("cadd99d7-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid DocumentObjectTypeGuid = new Guid("cad00070-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid AddedByReferenceAttributeTypeGuid = new Guid("cadd99d8-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PagesAttributeTypeGuid = new Guid("cad003a7-306c-11d8-b4e9-00304f19f545");

  public static int ObjectsAddedByReferenceRelationTypeID
  {
    get
    {
      return MetaDataHelper.GetRelationTypeID(MSOfficeAddinsConstants.ObjectsAddedByReferenceRelationTypeGuid);
    }
  }

  public static string ObjectsAddedByReferenceRelationTypeName
  {
    get
    {
      return MetaDataHelper.GetRelationTypeName(MSOfficeAddinsConstants.ObjectsAddedByReferenceRelationTypeGuid);
    }
  }

  public static int DocumentObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(MSOfficeAddinsConstants.DocumentObjectTypeGuid);
  }

  public static int AddedByReferenceAttributeTypeID
  {
    get
    {
      return MetaDataHelper.GetAttributeTypeID(MSOfficeAddinsConstants.AddedByReferenceAttributeTypeGuid);
    }
  }

  public static int PagesAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID(MSOfficeAddinsConstants.PagesAttributeTypeGuid);
  }

  public static string PagesAttributeTypeName
  {
    get => MetaDataHelper.GetAttributeTypeName(MSOfficeAddinsConstants.PagesAttributeTypeGuid);
  }
}
