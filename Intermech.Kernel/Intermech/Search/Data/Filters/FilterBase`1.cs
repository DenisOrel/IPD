// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.FilterBase`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Search.Data.Filters;

public abstract class FilterBase<T> : FilterBase, IFilter<T>, IFilter where T : FilterOptions
{
  public FilterBase(IUserSession userSession)
    : base(userSession)
  {
  }

  protected abstract void CheckOptions(T options);

  protected T Options { get; set; }

  protected new void SetStatuses(string pluginID, _Object @object, short value)
  {
    if (!this.Options.FillStatuses)
      return;
    this._elementStatusesService.SetElementStatuses16(pluginID, @object.Statuses, value);
  }

  protected new void SetStatuses(string pluginID, _Object @object, int value)
  {
    if (!this.Options.FillStatuses)
      return;
    this._elementStatusesService.SetElementStatuses32(pluginID, @object.Statuses, value);
  }

  protected new void SetStatuses(string pluginID, Relation relation, short value)
  {
    if (!this.Options.FillStatuses)
      return;
    this._elementStatusesService.SetElementStatuses16(pluginID, relation.Statuses, value);
  }

  protected new void SetStatuses(string pluginID, Relation relation, int value)
  {
    if (!this.Options.FillStatuses)
      return;
    this._elementStatusesService.SetElementStatuses32(pluginID, relation.Statuses, value);
  }

  void IFilter.Configure(FilterOptions options)
  {
    if (!(options is T options1))
      throw new ArgumentException();
    this.Configure(options1);
  }

  public virtual void Configure(T options)
  {
    if ((object) options == null)
      throw new ArgumentNullException(nameof (options));
    this.CheckOptions(options);
    this.Options = options;
  }
}
