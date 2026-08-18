// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionsConfigurator.CompositionsConfiguratorConstants
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Search.Pdm.CompositionsConfigurator;

public static class CompositionsConfiguratorConstants
{
  public static readonly Guid ApplicationConditionsAttributeTypeGuid = new Guid("cad015ac-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ApplicationConditionsAsStringAttributeTypeGuid = new Guid("cadd970e-306c-11d8-b4e9-00304f19f545");

  public static int ApplicationConditionsAttributeTypeID
  {
    get
    {
      return CompositionsConfiguratorConstants.GetAttributeTypeID(CompositionsConfiguratorConstants.ApplicationConditionsAttributeTypeGuid);
    }
  }

  public static int ApplicationConditionsAsStringAttributeTypeID
  {
    get
    {
      return CompositionsConfiguratorConstants.GetAttributeTypeID(CompositionsConfiguratorConstants.ApplicationConditionsAsStringAttributeTypeGuid);
    }
  }

  private static int GetAttributeTypeID(Guid attributeTypeGuid)
  {
    return MetaDataHelper.GetAttributeTypeID(attributeTypeGuid);
  }
}
