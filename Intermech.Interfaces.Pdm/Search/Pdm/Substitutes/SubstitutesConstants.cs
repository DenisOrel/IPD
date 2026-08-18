// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstitutesConstants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

public static class SubstitutesConstants
{
  public static readonly Guid SubstitutePositionTypeAttributeTypeGuid = new Guid("cadd9676-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SubstituteGroupNumberAttributeTypeGuid = new Guid("cad001c0-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SubstituteGroupNameAttributeTypeGuid = new Guid("cad00817-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SubstituteNumberAttributeTypeGuid = new Guid("cad001c1-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid SubstituteNameAttributeTypeGuid = new Guid("cad00818-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid GroupInstanceMarkAttributeTypeGuid = new Guid("cad001f9-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid DesignActualVariantAttributeTypeGuid = new Guid("cad00654-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PositionDesignationAttributeTypeGuid = new Guid("cad01478-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid PositionNumberAttributeTypeGuid = new Guid("cadd99ac-306c-11d8-b4e9-00304f19f545");
  private static LazyService<IIDConverter> _idConverter = new LazyService<IIDConverter>();

  public static int SubstitutePositionTypeAttributeTypeID
  {
    get
    {
      return SubstitutesConstants.GetAttributeTypeID(SubstitutesConstants.SubstitutePositionTypeAttributeTypeGuid);
    }
  }

  public static int SubstituteGroupNumberAttributeTypeID
  {
    get
    {
      return SubstitutesConstants.GetAttributeTypeID(SubstitutesConstants.SubstituteGroupNumberAttributeTypeGuid);
    }
  }

  public static int SubstituteGroupNameAttributeTypeID
  {
    get
    {
      return SubstitutesConstants.GetAttributeTypeID(SubstitutesConstants.SubstituteGroupNameAttributeTypeGuid);
    }
  }

  public static int SubstituteNumberAttributeTypeID
  {
    get
    {
      return SubstitutesConstants.GetAttributeTypeID(SubstitutesConstants.SubstituteNumberAttributeTypeGuid);
    }
  }

  public static int SubstituteNameAttributeTypeID
  {
    get
    {
      return SubstitutesConstants.GetAttributeTypeID(SubstitutesConstants.SubstituteNameAttributeTypeGuid);
    }
  }

  public static int GroupInstanceMarkAttributeTypeID
  {
    get
    {
      return SubstitutesConstants.GetAttributeTypeID(SubstitutesConstants.GroupInstanceMarkAttributeTypeGuid);
    }
  }

  public static int DesignActualVariantAttributeTypeID
  {
    get
    {
      return SubstitutesConstants.GetAttributeTypeID(SubstitutesConstants.DesignActualVariantAttributeTypeGuid);
    }
  }

  public static int PositionDesignationAttributeTypeID
  {
    get
    {
      return MetaDataHelper.GetAttributeTypeID(SubstitutesConstants.PositionDesignationAttributeTypeGuid);
    }
  }

  public static int PositionNumberAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID(SubstitutesConstants.PositionNumberAttributeTypeGuid);
  }

  private static int GetAttributeTypeID(Guid attributeTypeGuid)
  {
    return SubstitutesConstants._idConverter.Value.ConvertAttributeTypeGuidToAttributeTypeID(attributeTypeGuid);
  }
}
