// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.SearchSchemeSettingsService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.Pdm.Server;

internal sealed class SearchSchemeSettingsService : LongLifeObject, ISearchSchemeSettingsService
{
  private readonly string _sectionSearchSchemes = "SEARCH_SCHEMES";
  private readonly string _paramVisibilityFilter = "VISIBILITY_FILTER";
  private readonly bool _visibilityFilterDefaultValue;

  public bool VisibilityFilter
  {
    get
    {
      return ServerServices.GetService(typeof (IDBConfigurationService)) is IDBConfigurationService service ? Convert.ToBoolean(service.GetValue("CLIENT", this._sectionSearchSchemes, this._paramVisibilityFilter, (object) this._visibilityFilterDefaultValue)) : this._visibilityFilterDefaultValue;
    }
  }

  public void SetVisibilityFilter(Guid sessionGuid, bool value)
  {
    if (!(ServerServices.GetService(typeof (IDBConfigurationService)) is IDBConfigurationService service))
      return;
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    service.GetDBConfigurations(sessionById).WriteBool("CLIENT", this._sectionSearchSchemes, this._paramVisibilityFilter, value, 0L);
  }
}
