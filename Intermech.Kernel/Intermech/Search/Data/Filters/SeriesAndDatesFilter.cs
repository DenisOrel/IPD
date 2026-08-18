// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Filters.SeriesAndDatesFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Data.Filters;

public sealed class SeriesAndDatesFilter(IUserSession userSession) : 
  FilterBase<SeriesAndDatesFilter.SeriesAndDatesFilterOptions>(userSession)
{
  private IVersionApplicabilitiesService _versionApplicabilitiesService;

  public override bool Apply(Applicability applicability)
  {
    return this.ApplyInternal((RelationObjectBase) applicability);
  }

  public override bool Apply(CompositionPart compositionPart)
  {
    return this.ApplyInternal((RelationObjectBase) compositionPart);
  }

  protected override void CheckOptions(
    SeriesAndDatesFilter.SeriesAndDatesFilterOptions options)
  {
    if (options.SettingsHolder == null)
      throw new InvalidOperationException();
  }

  public override List<ColumnDescriptor> Columns
  {
    get
    {
      return new List<ColumnDescriptor>()
      {
        new ColumnDescriptor()
        {
          AttributeID = (object) Constants.ApplicabilityInSeriesAndDatesAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Object
        }
      };
    }
  }

  public override void Configure(
    SeriesAndDatesFilter.SeriesAndDatesFilterOptions options)
  {
    base.Configure(options);
    this._versionApplicabilitiesService = this.UserSession.GetCustomService(typeof (IVersionApplicabilitiesService)) as IVersionApplicabilitiesService;
  }

  private bool ApplyInternal(RelationObjectBase relationObject)
  {
    if (relationObject == null)
      throw new ArgumentNullException(nameof (relationObject));
    IVersionApplicabilitiesService applicabilitiesService = this._versionApplicabilitiesService;
    IUserSession userSession = this.UserSession;
    if (!(relationObject.Object.Attributes.GetAttributeValue(Constants.ApplicabilityInSeriesAndDatesAttributeTypeID) is string applicabilities))
      applicabilities = string.Empty;
    long versionId = relationObject.Object.VersionID;
    long masterArticle = this.Options.SettingsHolder.MasterArticle;
    DateTime date = this.Options.SettingsHolder.Date;
    int series = this.Options.SettingsHolder.Series;
    ObjectFiltrationState objectFiltrationState = applicabilitiesService.CheckApplicabilities(userSession, applicabilities, versionId, masterArticle, date, series);
    switch (objectFiltrationState)
    {
      case ObjectFiltrationState.fsVersionByDate:
      case ObjectFiltrationState.fsVersionBySeries:
        this.SetStatuses("{14BE37A7-84F7-44CB-97AA-15A713C703E0}", relationObject.Object, (short) objectFiltrationState);
        return true;
      default:
        return false;
    }
  }

  public class SeriesAndDatesFilterOptions : FilterOptions
  {
    public SeriesDateSettingsHolder SettingsHolder { get; set; }
  }
}
