// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.LifecycleLevelFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Filters;

public sealed class LifecycleLevelFilter(IUserSession userSession) : FilterBase(userSession)
{
  private List<IMSLifeCycleLevel> _lifecycleLevels = new List<IMSLifeCycleLevel>(0);

  public override bool Apply(CompositionPart compositionPart)
  {
    return this.ApplyInternal((RelationObjectBase) compositionPart);
  }

  public override bool Apply(Applicability applicability)
  {
    return this.ApplyInternal((RelationObjectBase) applicability);
  }

  public override List<ColumnDescriptor> Columns
  {
    get
    {
      return new List<ColumnDescriptor>()
      {
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_LEVEL_ID,
          AttributeSource = AttributeSourceTypes.Object
        }
      };
    }
  }

  public override void Configure(FilterOptions options)
  {
    base.Configure(options);
    this.GetLifecycleLevels();
  }

  private bool ApplyInternal(RelationObjectBase relationObject)
  {
    long levelID = (long) relationObject.Object.LifecycleLevelID;
    int num = this._lifecycleLevels.IndexOf(this._lifecycleLevels.Find((Predicate<IMSLifeCycleLevel>) (o => (long) o.LevelID == levelID)));
    if (num < 0)
      return false;
    short int16 = Convert.ToInt16(num + 1);
    this.SetStatuses("{7074E0E4-B3AB-4B3E-AD56-050CD256AF10}", relationObject.Object, int16);
    return true;
  }

  private void GetLifecycleLevels()
  {
    List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
    lcLevelsList.Sort();
    this._lifecycleLevels = lcLevelsList;
  }
}
