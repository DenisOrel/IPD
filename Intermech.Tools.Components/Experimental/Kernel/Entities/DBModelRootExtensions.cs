// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBModelRootExtensions
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;

#nullable disable
namespace Experimental.Kernel.Entities;

public static class DBModelRootExtensions
{
  public static DBModelConfiguration GetModelConfiguration(this IModelRoot modelRoot)
  {
    return modelRoot != null ? DBModelRootExtensions.GetDBModelRoot((object) modelRoot).Configuration : throw new ArgumentNullException(nameof (modelRoot));
  }

  public static DBMetadataInfoService GetMetadataInfoService(this IModelRoot modelRoot)
  {
    return modelRoot != null ? DBModelRootExtensions.GetDBModelRoot((object) modelRoot).MetadataInfoService : throw new ArgumentNullException(nameof (modelRoot));
  }

  private static DBModelRoot GetDBModelRoot(object modelRoot)
  {
    return modelRoot is DBModelRoot dbModelRoot ? dbModelRoot : throw new ArgumentException($"Неподдерживаемый тип modelRoot. Требуется тип, унаследованный от {"DBModelRoot"}.", nameof (modelRoot));
  }
}
