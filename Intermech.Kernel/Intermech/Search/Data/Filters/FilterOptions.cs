// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.FilterOptions
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Search.Data.Filters;

public class FilterOptions
{
  public FilterOptions()
  {
    this.EditingContextVersionID = 0L;
    this.ChildObjectTypeIds = new List<int>(0);
    this.ProjectVersionID = 0L;
    this.ProjectTypeID = -1;
    this.PartVersionID = 0L;
    this.PartTypeID = -1;
  }

  public bool FillStatuses { get; set; }

  public VersionsRule VersionRule { get; set; }

  public long EditingContextVersionID { get; set; }

  public int RelationTypeID { get; set; }

  public List<int> ChildObjectTypeIds { get; set; }

  public long ProjectVersionID { get; set; }

  public int ProjectTypeID { get; set; }

  public long PartVersionID { get; set; }

  public int PartTypeID { get; set; }
}
