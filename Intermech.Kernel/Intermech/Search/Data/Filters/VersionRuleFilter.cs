// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.VersionRuleFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Data.Filters;

public sealed class VersionRuleFilter(IUserSession userSession) : FilterBase(userSession)
{
  public override bool Apply(Applicability applicability)
  {
    return this.ApplyInternal((RelationObjectBase) applicability);
  }

  public override bool Apply(CompositionPart compositionPart)
  {
    return this.ApplyInternal((RelationObjectBase) compositionPart);
  }

  public override IEnumerable<CompositionPart> Apply(IEnumerable<CompositionPart> composition)
  {
    if (!composition.Any<CompositionPart>())
      return (IEnumerable<CompositionPart>) new List<CompositionPart>(0);
    List<CompositionPart> list = composition.Where<CompositionPart>((Func<CompositionPart, bool>) (o => this.ApplyInternal((RelationObjectBase) o))).ToList<CompositionPart>();
    if (list.Count == 1)
    {
      this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", list[0].Object, Convert.ToInt16((object) ObjectFiltrationState.fsCorrespondingSingle));
      return (IEnumerable<CompositionPart>) list;
    }
    _Object @object = this.Options.VersionRule.SelectVersionAdv(list.Select<CompositionPart, _Object>((Func<CompositionPart, _Object>) (o => o.Object)));
    if (@object == null)
    {
      @object = this.Options.VersionRule.SelectVersionAdv(composition.Select<CompositionPart, _Object>((Func<CompositionPart, _Object>) (o => o.Object)));
      if (@object != null)
        this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", @object, Convert.ToInt16((object) ObjectFiltrationState.fsVariance));
    }
    if (@object != null)
      return composition.Where<CompositionPart>((Func<CompositionPart, bool>) (o => o.Object.VersionID == @object.VersionID));
    if (this.Options.VersionRule.EditingRule)
      throw new AmbiguousVersionsException($"Не удалось подобрать версию по правилу \"{this.Options.VersionRule.RuleObjectCaption}\", которое является правилом для редактирования");
    return (IEnumerable<CompositionPart>) list;
  }

  public override IEnumerable<Applicability> Apply(IEnumerable<Applicability> applicabilities)
  {
    if (!applicabilities.Any<Applicability>())
      return Enumerable.Empty<Applicability>();
    List<Applicability> list = applicabilities.Where<Applicability>((Func<Applicability, bool>) (o => this.ApplyInternal((RelationObjectBase) o))).ToList<Applicability>();
    if (list.Count == 1)
    {
      this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", list[0].Object, Convert.ToInt16((object) ObjectFiltrationState.fsCorrespondingSingle));
      return (IEnumerable<Applicability>) list;
    }
    _Object @object = this.Options.VersionRule.SelectVersionAdv(list.Select<Applicability, _Object>((Func<Applicability, _Object>) (o => o.Object)));
    if (@object == null)
    {
      @object = this.Options.VersionRule.SelectVersionAdv(applicabilities.Select<Applicability, _Object>((Func<Applicability, _Object>) (o => o.Object)));
      if (@object != null)
        this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", @object, Convert.ToInt16((object) ObjectFiltrationState.fsVariance));
    }
    return @object == null ? Enumerable.Empty<Applicability>() : applicabilities.Where<Applicability>((Func<Applicability, bool>) (o => o.Object.VersionID == @object.VersionID));
  }

  public override List<ColumnDescriptor> Columns
  {
    get => this.Options.VersionRule.GetRuleAttrsColumns(0, new DBRecordSetParams());
  }

  public override void Configure(FilterOptions options)
  {
    base.Configure(options);
    this.InitializeMeasureHelperIfNotInitialized();
  }

  private bool ApplyInternal(RelationObjectBase relationObject)
  {
    if (relationObject == null)
      throw new ArgumentNullException(nameof (relationObject));
    if (this.Options.VersionRule.CheckVersionByCriterions(this.UserSession, relationObject.Object))
    {
      this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", relationObject.Object, Convert.ToInt16((object) ObjectFiltrationState.fsCorresponding));
      return true;
    }
    this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", relationObject.Object, Convert.ToInt16((object) ObjectFiltrationState.fsFiltrationStopped));
    return false;
  }

  private void InitializeMeasureHelperIfNotInitialized()
  {
    lock (MeasureHelper.Instance)
    {
      if (MeasureHelper.Measures != null && MeasureHelper.Measures.Length != 0)
        return;
      MeasureHelper.Init(this.UserSession.GetMeasuresList());
    }
  }

  private List<Applicability> FilterByAdv(IEnumerable<Applicability> applicabilities)
  {
    _Object @object = this.Options.VersionRule.SelectVersionAdv(applicabilities.Select<Applicability, _Object>((Func<Applicability, _Object>) (o => o.Object)));
    return @object == null ? new List<Applicability>(0) : applicabilities.Where<Applicability>((Func<Applicability, bool>) (o => o.Object.VersionID == @object.VersionID)).ToList<Applicability>();
  }
}
