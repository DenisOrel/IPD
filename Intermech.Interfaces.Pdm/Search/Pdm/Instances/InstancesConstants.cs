// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Instances.InstancesConstants
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Pdm.Instances;

public static class InstancesConstants
{
  public static int NameAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
  }

  public static int GroupProductIDAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID("cad001f9-306c-11d8-b4e9-00304f19f545");
  }

  public static int DesignationAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
  }

  public static int DocumentationRelationTypeID
  {
    get => MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545");
  }

  public static int CreatedByCadModelAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID("CAD0153E-306C-11D8-B4E9-00304F19F545");
  }

  public static int ProductObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
  }

  public static int StandardProductObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(new Guid("cad00252-306c-11d8-b4e9-00304f19f545"));
  }

  public static int OtherProductObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID(new Guid("cad0038d-306c-11d8-b4e9-00304f19f545"));
  }
}
