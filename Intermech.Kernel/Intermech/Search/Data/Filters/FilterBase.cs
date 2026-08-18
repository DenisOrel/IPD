// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.FilterBase
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Filters;

public abstract class FilterBase : IFilter
{
  protected IElementStatusesService _elementStatusesService;

  public FilterBase(IUserSession userSession)
  {
    this.UserSession = userSession != null ? userSession : throw new ArgumentNullException(nameof (userSession));
    this._elementStatusesService = ServerServices.GetService(typeof (IElementStatusesService)) as IElementStatusesService;
  }

  protected virtual void CheckOptions(FilterOptions options)
  {
  }

  protected FilterOptions Options { get; set; }

  protected IUserSession UserSession { get; private set; }

  protected void SetStatuses(string pluginID, _Object @object, short value)
  {
    if (!this.Options.FillStatuses)
      return;
    this._elementStatusesService.SetElementStatuses16(pluginID, @object.Statuses, value);
  }

  protected void SetStatuses(string pluginID, _Object @object, int value)
  {
    if (!this.Options.FillStatuses)
      return;
    this._elementStatusesService.SetElementStatuses32(pluginID, @object.Statuses, value);
  }

  protected void SetStatuses(string pluginID, Relation relation, short value)
  {
    if (!this.Options.FillStatuses)
      return;
    this._elementStatusesService.SetElementStatuses16(pluginID, relation.Statuses, value);
  }

  protected void SetStatuses(string pluginID, Relation relation, int value)
  {
    if (!this.Options.FillStatuses)
      return;
    this._elementStatusesService.SetElementStatuses32(pluginID, relation.Statuses, value);
  }

  public abstract bool Apply(CompositionPart compositionPart);

  public abstract bool Apply(Applicability applicability);

  public virtual IEnumerable<Applicability> Apply(IEnumerable<Applicability> applicabilities)
  {
    foreach (Applicability applicability in applicabilities)
    {
      if (this.Apply(applicability))
        yield return applicability;
    }
  }

  public virtual IEnumerable<CompositionPart> Apply(IEnumerable<CompositionPart> composition)
  {
    foreach (CompositionPart compositionPart in composition)
    {
      if (this.Apply(compositionPart))
        yield return compositionPart;
    }
  }

  public abstract List<ColumnDescriptor> Columns { get; }

  public virtual void Configure(FilterOptions options)
  {
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    this.CheckOptions(options);
    this.Options = options;
  }
}
