// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Relations.DBRelationCollectionFilter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Kernel.Search;
using Intermech.Search;
using Intermech.Search.Data.Filters;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Kernel.Relations;

internal class DBRelationCollectionFilter : 
  FilterBase<DBRelationCollectionFilter.DBRelationCollectionFilterOptions>
{
  private DBRelationCollectionFilter.FilterAdapter<EditingContextFilter> _editingContextFilter;
  private LifecycleLevelFilter _lifecycleLevelFilter;
  private DBRelationCollectionFilter.FilterAdapter<VisibilityFilter> _visibilityFilter;
  private DBRelationCollectionFilter.FilterAdapter<VersionRuleFilter> _versionRuleFilter;
  private DBRelationCollectionFilter.FilterAdapter<SeriesAndDatesFilter> _seriesAndDatesFilter;
  private DBRelationCollectionFilter.FilterAdapter<ConcretizationFilter> _hardConcretizationFilter;
  private DBRelationCollectionFilter.FilterAdapter<InvalidConcretizationFilter> _invalidHardConcretizationFilter;
  private DBRelationCollectionFilter.FilterAdapter<ConcretizationFilter> _softConcretizationFilter;
  private DBRelationCollectionFilter.FilterAdapter<InvalidConcretizationFilter> _invalidSoftConcretizationFilter;
  private static readonly TimeSpan OneDay = new TimeSpan(0, 23, 59, 59, 999);

  public DBRelationCollectionFilter(Intermech.Kernel.UserSession userSession)
    : base((IUserSession) userSession)
  {
    this.CreateFilters();
  }

  public DBRelationCollectionFilter.DBRelationCollectionFilterMode Mode { get; private set; }

  public bool DisableCoreFiltration
  {
    get
    {
      return this.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.AllVersions || this.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.AllVersionsWithConcretization || this._hardConcretizationFilter.Enabled || this.CheckIsSimpleContext(this.Options.EditingContextVersionID);
    }
  }

  public void SetProjectInfo(long projectVersionID, int projectTypeID)
  {
    this.Options.ProjectVersionID = projectVersionID;
    this.Options.ProjectTypeID = projectTypeID;
    this._hardConcretizationFilter.Configure((FilterOptions) this.CreateConcretizationFilterOptions(this.Options));
    this._invalidHardConcretizationFilter.Configure(this.CreateInvalidConcretizationFilterOptions(this.Options));
    this._softConcretizationFilter.Configure((FilterOptions) this.CreateConcretizationFilterOptions(this.Options));
    this._invalidSoftConcretizationFilter.Configure(this.CreateInvalidConcretizationFilterOptions(this.Options));
  }

  public void SetPartInfo(long partVersionID, int partTypeID)
  {
    this.Options.PartVersionID = partVersionID;
    this.Options.PartTypeID = partTypeID;
    this._hardConcretizationFilter.Configure((FilterOptions) this.CreateConcretizationFilterOptions(this.Options));
    this._invalidHardConcretizationFilter.Configure(this.CreateInvalidConcretizationFilterOptions(this.Options));
    this._softConcretizationFilter.Configure((FilterOptions) this.CreateConcretizationFilterOptions(this.Options));
    this._invalidSoftConcretizationFilter.Configure(this.CreateInvalidConcretizationFilterOptions(this.Options));
  }

  public override bool Apply(Applicability applicability) => throw new InvalidOperationException();

  public override bool Apply(CompositionPart compositionPart)
  {
    throw new InvalidOperationException();
  }

  public override IEnumerable<Applicability> Apply(IEnumerable<Applicability> applicabilities)
  {
    if (this.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.AllVersions)
      return this._lifecycleLevelFilter.Apply(this.ApplyInAllVersionsMode(applicabilities));
    if (this.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.AllVersionsWithConcretization)
      return this._lifecycleLevelFilter.Apply(this.ApplyInAllVersionsWithConcretizationMode(applicabilities));
    if (this.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.Default)
      return this._lifecycleLevelFilter.Apply(this.ApplyInDefaultMode(applicabilities));
    throw new NotSupportedException();
  }

  public override IEnumerable<CompositionPart> Apply(IEnumerable<CompositionPart> composition)
  {
    if (this.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.AllVersions)
      return this._lifecycleLevelFilter.Apply(this.ApplyInAllVersionsMode(composition));
    if (this.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.AllVersionsWithConcretization)
      return this._lifecycleLevelFilter.Apply(this.ApplyInAllVersionsWithConcretizationMode(composition));
    if (this.Mode == DBRelationCollectionFilter.DBRelationCollectionFilterMode.Default)
      return this._lifecycleLevelFilter.Apply(this.ApplyInDefaultMode(composition));
    throw new NotSupportedException();
  }

  protected override void CheckOptions(
    DBRelationCollectionFilter.DBRelationCollectionFilterOptions options)
  {
  }

  public override List<ColumnDescriptor> Columns
  {
    get
    {
      List<ColumnDescriptor> destination = new List<ColumnDescriptor>();
      this.AddUnique(destination, this._invalidHardConcretizationFilter.Columns);
      this.AddUnique(destination, this._editingContextFilter.Columns);
      this.AddUnique(destination, this._lifecycleLevelFilter.Columns);
      this.AddUnique(destination, this._visibilityFilter.Columns);
      this.AddUnique(destination, this._versionRuleFilter.Columns);
      this.AddUnique(destination, this._seriesAndDatesFilter.Columns);
      this.AddUnique(destination, this._hardConcretizationFilter.Columns);
      this.AddUnique(destination, new List<ColumnDescriptor>()
      {
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          AttributeSource = AttributeSourceTypes.Relation
        },
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_ID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnDescriptor()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          AttributeSource = AttributeSourceTypes.Object
        }
      });
      return destination;
    }
  }

  public override void Configure(
    DBRelationCollectionFilter.DBRelationCollectionFilterOptions options)
  {
    base.Configure(options);
    this.SetFiltersEnabled();
    this.SetMode();
    this.ConfigureFilters();
  }

  private IEnumerable<Applicability> ApplyInAllVersionsMode(
    IEnumerable<Applicability> applicabilities)
  {
    return applicabilities;
  }

  private IEnumerable<CompositionPart> ApplyInAllVersionsMode(
    IEnumerable<CompositionPart> composition)
  {
    return composition;
  }

  private IEnumerable<Applicability> ApplyInAllVersionsWithConcretizationMode(
    IEnumerable<Applicability> applicabilities)
  {
    foreach (KeyValuePair<long, List<Applicability>> keyValuePair in this.CreateApplicabilityDictionaryByProjectID(applicabilities))
    {
      List<Applicability> applicabilityList = keyValuePair.Value;
      Applicability[] array = this._hardConcretizationFilter.Apply((IEnumerable<Applicability>) applicabilityList).ToArray<Applicability>();
      if (array.Length != 0)
      {
        if (array.Length == 1)
        {
          yield return ((IEnumerable<Applicability>) array).First<Applicability>();
        }
        else
        {
          Applicability applicability1 = this._editingContextFilter.Apply((IEnumerable<Applicability>) array).FirstOrDefault<Applicability>();
          if (applicability1 != null)
          {
            yield return applicability1;
          }
          else
          {
            foreach (Applicability applicability2 in this._versionRuleFilter.Apply((IEnumerable<Applicability>) array))
              yield return applicability2;
          }
        }
      }
      else
      {
        Applicability applicability3 = this._invalidHardConcretizationFilter.Apply((IEnumerable<Applicability>) applicabilityList).FirstOrDefault<Applicability>();
        if (applicability3 != null && this.Options.ShowInvalidConcreteVersions)
          yield return applicability3;
        else if (applicability3 == null)
        {
          Applicability applicability4 = this._softConcretizationFilter.Apply((IEnumerable<Applicability>) applicabilityList).FirstOrDefault<Applicability>();
          if (applicability4 != null)
          {
            yield return applicability4;
          }
          else
          {
            Applicability applicability5 = this._invalidSoftConcretizationFilter.Apply((IEnumerable<Applicability>) applicabilityList).FirstOrDefault<Applicability>();
            if (applicability5 != null && this.Options.ShowInvalidConcreteVersions)
              yield return applicability5;
            else if (applicability5 == null)
            {
              foreach (Applicability applicability6 in applicabilityList)
                yield return applicability6;
            }
          }
        }
      }
    }
  }

  private IEnumerable<CompositionPart> ApplyInAllVersionsWithConcretizationMode(
    IEnumerable<CompositionPart> composition)
  {
    foreach (KeyValuePair<long, Dictionary<long, List<CompositionPart>>> keyValuePair1 in this.CreateCompositionPartDictionaryByRelationID(composition))
    {
      foreach (KeyValuePair<long, List<CompositionPart>> keyValuePair2 in keyValuePair1.Value)
      {
        List<CompositionPart> composition1 = keyValuePair2.Value;
        CompositionPart compositionPart1 = this._hardConcretizationFilter.Apply((IEnumerable<CompositionPart>) composition1).FirstOrDefault<CompositionPart>();
        if (compositionPart1 != null)
        {
          yield return compositionPart1;
        }
        else
        {
          CompositionPart compositionPart2 = this._invalidHardConcretizationFilter.Apply((IEnumerable<CompositionPart>) composition1).FirstOrDefault<CompositionPart>();
          if (compositionPart2 != null && this.Options.ShowInvalidConcreteVersions)
            yield return compositionPart2;
          else if (compositionPart2 == null)
          {
            CompositionPart compositionPart3 = this._softConcretizationFilter.Apply((IEnumerable<CompositionPart>) composition1).FirstOrDefault<CompositionPart>();
            if (compositionPart3 != null)
            {
              yield return compositionPart3;
            }
            else
            {
              CompositionPart compositionPart4 = this._invalidSoftConcretizationFilter.Apply((IEnumerable<CompositionPart>) composition1).FirstOrDefault<CompositionPart>();
              if (compositionPart4 != null && this.Options.ShowInvalidConcreteVersions)
                yield return compositionPart4;
              else if (compositionPart4 == null)
              {
                foreach (CompositionPart compositionPart5 in composition1)
                  yield return compositionPart5;
              }
            }
          }
        }
      }
    }
  }

  private IEnumerable<Applicability> ApplyInDefaultMode(IEnumerable<Applicability> applicabilities)
  {
    foreach (KeyValuePair<long, List<Applicability>> keyValuePair in this.CreateApplicabilityDictionaryByProjectID(applicabilities))
    {
      IEnumerable<Applicability> applicabilities1 = this._visibilityFilter.Apply((IEnumerable<Applicability>) keyValuePair.Value);
      Applicability[] array = this._hardConcretizationFilter.Apply(applicabilities1).ToArray<Applicability>();
      if (array.Length != 0)
      {
        if (array.Length == 1)
        {
          yield return ((IEnumerable<Applicability>) array).First<Applicability>();
        }
        else
        {
          Applicability applicability1 = this._editingContextFilter.Apply((IEnumerable<Applicability>) array).FirstOrDefault<Applicability>();
          if (applicability1 != null)
          {
            yield return applicability1;
          }
          else
          {
            foreach (Applicability applicability2 in this._versionRuleFilter.Apply((IEnumerable<Applicability>) array))
              yield return applicability2;
          }
        }
      }
      else
      {
        Applicability[] invalidHardConcretizationFilteredArray = this._invalidHardConcretizationFilter.Apply(applicabilities1).ToArray<Applicability>();
        Applicability applicability3 = ((IEnumerable<Applicability>) invalidHardConcretizationFilteredArray).FirstOrDefault<Applicability>();
        IEnumerable<Applicability> applicabilities2 = applicabilities1.Where<Applicability>((Func<Applicability, bool>) (o => !((IEnumerable<Applicability>) invalidHardConcretizationFilteredArray).Contains<Applicability>(o)));
        if (applicability3 != null && this.Options.ShowInvalidConcreteVersions)
        {
          yield return applicability3;
        }
        else
        {
          Applicability applicability4 = this._seriesAndDatesFilter.Apply(applicabilities2).FirstOrDefault<Applicability>();
          if (applicability4 != null)
          {
            yield return applicability4;
          }
          else
          {
            Applicability applicability5 = this._editingContextFilter.Apply(applicabilities2).FirstOrDefault<Applicability>();
            if (applicability5 != null)
            {
              yield return applicability5;
            }
            else
            {
              Applicability applicability6 = this._softConcretizationFilter.Apply(applicabilities2).FirstOrDefault<Applicability>();
              if (applicability6 != null)
              {
                yield return applicability6;
              }
              else
              {
                Applicability[] invalidSoftConcretizationFilteredArray = this._invalidSoftConcretizationFilter.Apply(applicabilities2).ToArray<Applicability>();
                Applicability applicability7 = ((IEnumerable<Applicability>) invalidSoftConcretizationFilteredArray).FirstOrDefault<Applicability>();
                IEnumerable<Applicability> applicabilities3 = applicabilities2.Where<Applicability>((Func<Applicability, bool>) (o => !((IEnumerable<Applicability>) invalidSoftConcretizationFilteredArray).Contains<Applicability>(o)));
                if (applicability7 != null && this.Options.ShowInvalidConcreteVersions)
                {
                  yield return applicability7;
                }
                else
                {
                  foreach (Applicability applicability8 in this._versionRuleFilter.Apply(applicabilities3))
                    yield return applicability8;
                }
              }
            }
          }
        }
      }
    }
  }

  private IEnumerable<CompositionPart> ApplyInDefaultMode(IEnumerable<CompositionPart> composition)
  {
    foreach (KeyValuePair<long, Dictionary<long, List<CompositionPart>>> keyValuePair1 in this.CreateCompositionPartDictionaryByRelationID(composition))
    {
      foreach (KeyValuePair<long, List<CompositionPart>> keyValuePair2 in keyValuePair1.Value)
      {
        List<CompositionPart> list = this._visibilityFilter.Apply((IEnumerable<CompositionPart>) keyValuePair2.Value).ToList<CompositionPart>();
        if (list.Count == 1 && this.FilterByVersionable((RelationObjectBase) list[0]))
        {
          if (this._seriesAndDatesFilter.Enabled && !string.IsNullOrEmpty(list[0].Object.Attributes.GetAttributeValue(Intermech.Search.Data.Filters.Constants.ApplicabilityInSeriesAndDatesAttributeTypeID) as string))
          {
            CompositionPart compositionPart = this._seriesAndDatesFilter.Apply((IEnumerable<CompositionPart>) list).FirstOrDefault<CompositionPart>();
            if (compositionPart != null)
              yield return compositionPart;
          }
          else
            yield return list[0];
        }
        else
        {
          CompositionPart compositionPart1 = this._hardConcretizationFilter.Apply((IEnumerable<CompositionPart>) list).FirstOrDefault<CompositionPart>();
          if (compositionPart1 != null)
          {
            yield return compositionPart1;
          }
          else
          {
            CompositionPart compositionPart2 = this._invalidHardConcretizationFilter.Apply((IEnumerable<CompositionPart>) list).FirstOrDefault<CompositionPart>();
            if (compositionPart2 != null && this.Options.ShowInvalidConcreteVersions)
              yield return compositionPart2;
            else if (compositionPart2 == null)
            {
              CompositionPart compositionPart3 = this._seriesAndDatesFilter.Apply((IEnumerable<CompositionPart>) list).FirstOrDefault<CompositionPart>();
              if (compositionPart3 != null)
              {
                yield return compositionPart3;
              }
              else
              {
                CompositionPart compositionPart4 = this._editingContextFilter.Apply((IEnumerable<CompositionPart>) list).FirstOrDefault<CompositionPart>();
                if (compositionPart4 != null)
                {
                  yield return compositionPart4;
                }
                else
                {
                  CompositionPart compositionPart5 = this._softConcretizationFilter.Apply((IEnumerable<CompositionPart>) list).FirstOrDefault<CompositionPart>();
                  if (compositionPart5 != null)
                  {
                    yield return compositionPart5;
                  }
                  else
                  {
                    CompositionPart compositionPart6 = this._invalidSoftConcretizationFilter.Apply((IEnumerable<CompositionPart>) list).FirstOrDefault<CompositionPart>();
                    if (compositionPart6 != null && this.Options.ShowInvalidConcreteVersions)
                      yield return compositionPart6;
                    else if (compositionPart6 == null)
                    {
                      foreach (CompositionPart compositionPart7 in this._versionRuleFilter.Apply((IEnumerable<CompositionPart>) list))
                        yield return compositionPart7;
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
  }

  private bool FilterByVersionable(RelationObjectBase relationObject)
  {
    if (this.IsVersionable(relationObject.Object))
      return false;
    this.SetStatuses("cad005f2-306c-11d8-b4e9-00304f19f545", relationObject.Object, (short) 6);
    return true;
  }

  private bool IsVersionable(_Object @object)
  {
    return MetaDataHelper.GetObjectType(@object.TypeID).VersionsMode == ObjectVersionModes.MultiVersion;
  }

  private void CreateFilters()
  {
    this._editingContextFilter = new DBRelationCollectionFilter.FilterAdapter<EditingContextFilter>(new EditingContextFilter(this.UserSession));
    this._lifecycleLevelFilter = new LifecycleLevelFilter(this.UserSession);
    this._visibilityFilter = new DBRelationCollectionFilter.FilterAdapter<VisibilityFilter>(new VisibilityFilter((Intermech.Kernel.UserSession) this.UserSession), true);
    this._versionRuleFilter = new DBRelationCollectionFilter.FilterAdapter<VersionRuleFilter>(new VersionRuleFilter(this.UserSession));
    this._seriesAndDatesFilter = new DBRelationCollectionFilter.FilterAdapter<SeriesAndDatesFilter>(new SeriesAndDatesFilter(this.UserSession));
    this._hardConcretizationFilter = new DBRelationCollectionFilter.FilterAdapter<ConcretizationFilter>(new ConcretizationFilter(this.UserSession, ConcretizationType.Hard));
    this._invalidHardConcretizationFilter = new DBRelationCollectionFilter.FilterAdapter<InvalidConcretizationFilter>(new InvalidConcretizationFilter(this.UserSession, ConcretizationType.Hard));
    this._softConcretizationFilter = new DBRelationCollectionFilter.FilterAdapter<ConcretizationFilter>(new ConcretizationFilter(this.UserSession, ConcretizationType.Soft));
    this._invalidSoftConcretizationFilter = new DBRelationCollectionFilter.FilterAdapter<InvalidConcretizationFilter>(new InvalidConcretizationFilter(this.UserSession, ConcretizationType.Soft));
  }

  private ConcretizationFilter.ConcretizationFilterOptions CreateConcretizationFilterOptions(
    DBRelationCollectionFilter.DBRelationCollectionFilterOptions options)
  {
    ConcretizationFilter.ConcretizationFilterOptions concretizationFilterOptions = new ConcretizationFilter.ConcretizationFilterOptions();
    concretizationFilterOptions.ChildObjectTypeIds = options.ChildObjectTypeIds;
    concretizationFilterOptions.EditingContextVersionID = options.EditingContextVersionID;
    concretizationFilterOptions.FillStatuses = options.FillStatuses;
    concretizationFilterOptions.PartVersionID = options.PartVersionID;
    concretizationFilterOptions.PartTypeID = options.PartTypeID;
    concretizationFilterOptions.ProjectVersionID = options.ProjectVersionID;
    concretizationFilterOptions.ProjectTypeID = options.ProjectTypeID;
    concretizationFilterOptions.RelationTypeID = options.RelationTypeID;
    concretizationFilterOptions.VersionRule = options.VersionRule;
    concretizationFilterOptions.UseStoredExplicitPartVersionID = options.UseStoredExplicitPartVersionID;
    return concretizationFilterOptions;
  }

  private SeriesAndDatesFilter.SeriesAndDatesFilterOptions CreateSeriesAndDatesFilterOptions(
    DBRelationCollectionFilter.DBRelationCollectionFilterOptions options)
  {
    SeriesAndDatesFilter.SeriesAndDatesFilterOptions datesFilterOptions = new SeriesAndDatesFilter.SeriesAndDatesFilterOptions();
    datesFilterOptions.ChildObjectTypeIds = options.ChildObjectTypeIds;
    datesFilterOptions.EditingContextVersionID = options.EditingContextVersionID;
    datesFilterOptions.FillStatuses = options.FillStatuses;
    datesFilterOptions.PartVersionID = options.PartVersionID;
    datesFilterOptions.PartTypeID = options.PartTypeID;
    datesFilterOptions.ProjectVersionID = options.ProjectVersionID;
    datesFilterOptions.ProjectTypeID = options.ProjectTypeID;
    datesFilterOptions.RelationTypeID = options.RelationTypeID;
    datesFilterOptions.SettingsHolder = options.SeriesDateSettingsHolder;
    datesFilterOptions.VersionRule = options.VersionRule;
    return datesFilterOptions;
  }

  private FilterOptions CreateInvalidConcretizationFilterOptions(
    DBRelationCollectionFilter.DBRelationCollectionFilterOptions options)
  {
    return new FilterOptions()
    {
      ChildObjectTypeIds = options.ChildObjectTypeIds,
      EditingContextVersionID = options.EditingContextVersionID,
      FillStatuses = options.FillStatuses && options.ShowInvalidConcreteVersions,
      PartVersionID = options.PartVersionID,
      PartTypeID = options.PartTypeID,
      ProjectVersionID = options.ProjectVersionID,
      ProjectTypeID = options.ProjectTypeID,
      RelationTypeID = options.RelationTypeID,
      VersionRule = options.VersionRule
    };
  }

  private void ConfigureFilters()
  {
    this._editingContextFilter.Configure((FilterOptions) this.Options);
    this._lifecycleLevelFilter.Configure((FilterOptions) this.Options);
    this._visibilityFilter.Configure((FilterOptions) this.Options);
    this._versionRuleFilter.Configure((FilterOptions) this.Options);
    this._seriesAndDatesFilter.Configure((FilterOptions) this.CreateSeriesAndDatesFilterOptions(this.Options));
    this._hardConcretizationFilter.Configure((FilterOptions) this.CreateConcretizationFilterOptions(this.Options));
    this._invalidHardConcretizationFilter.Configure(this.CreateInvalidConcretizationFilterOptions(this.Options));
    this._softConcretizationFilter.Configure((FilterOptions) this.CreateConcretizationFilterOptions(this.Options));
    this._invalidSoftConcretizationFilter.Configure(this.CreateInvalidConcretizationFilterOptions(this.Options));
  }

  private Dictionary<long, Dictionary<long, List<CompositionPart>>> CreateCompositionPartDictionaryByRelationID(
    IEnumerable<CompositionPart> composition)
  {
    Dictionary<long, Dictionary<long, List<CompositionPart>>> dictionaryByRelationId = composition is ICollection<CompositionPart> compositionParts ? new Dictionary<long, Dictionary<long, List<CompositionPart>>>(compositionParts.Count) : new Dictionary<long, Dictionary<long, List<CompositionPart>>>();
    foreach (CompositionPart compositionPart in composition)
    {
      Dictionary<long, List<CompositionPart>> dictionary = (Dictionary<long, List<CompositionPart>>) null;
      if (!dictionaryByRelationId.TryGetValue(compositionPart.Relation.ID, out dictionary))
      {
        dictionary = new Dictionary<long, List<CompositionPart>>();
        dictionaryByRelationId.Add(compositionPart.Relation.ID, dictionary);
      }
      List<CompositionPart> compositionPartList = (List<CompositionPart>) null;
      if (!dictionary.TryGetValue(compositionPart.Object.ID, out compositionPartList))
      {
        compositionPartList = new List<CompositionPart>();
        dictionary.Add(compositionPart.Object.ID, compositionPartList);
      }
      compositionPartList.Add(compositionPart);
    }
    return dictionaryByRelationId;
  }

  private Dictionary<long, List<Applicability>> CreateApplicabilityDictionaryByProjectID(
    IEnumerable<Applicability> applicabilities)
  {
    Dictionary<long, List<Applicability>> dictionaryByProjectId = new Dictionary<long, List<Applicability>>();
    foreach (Applicability applicability in applicabilities)
    {
      List<Applicability> applicabilityList = (List<Applicability>) null;
      if (!dictionaryByProjectId.TryGetValue(applicability.Object.ID, out applicabilityList))
      {
        applicabilityList = new List<Applicability>();
        dictionaryByProjectId.Add(applicability.Object.ID, applicabilityList);
      }
      applicabilityList.Add(applicability);
    }
    return dictionaryByProjectId;
  }

  private void SetMode()
  {
    if (this.Options.VersionRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
      this.Mode = DBRelationCollectionFilter.DBRelationCollectionFilterMode.AllVersions;
    else if (this.Options.VersionRule.RuleObjectGuid == "cad005ac-306c-11d8-b4e9-00304f19f5455")
      this.Mode = DBRelationCollectionFilter.DBRelationCollectionFilterMode.AllVersionsWithConcretization;
    else
      this.Mode = DBRelationCollectionFilter.DBRelationCollectionFilterMode.Default;
  }

  private void AddUnique(List<ColumnDescriptor> destination, List<ColumnDescriptor> source)
  {
    foreach (ColumnDescriptor columnDescriptor in source)
    {
      ColumnDescriptor item = columnDescriptor;
      if (!destination.Any<ColumnDescriptor>((Func<ColumnDescriptor, bool>) (o => this.CompareColumns(o, item))))
        destination.Add(item);
    }
  }

  private bool CompareColumns(ColumnDescriptor column, ColumnDescriptor otherColumn)
  {
    return object.Equals(column.AttributeID, otherColumn.AttributeID) && column.AttributeSource == otherColumn.AttributeSource;
  }

  private void SetEditingContextFilterEnabled()
  {
    DateTime dateTime = DateTime.MinValue;
    if (this.Options.VersionRule.ActualDate > DateTime.MinValue)
      dateTime = this.Options.VersionRule.ActualDate.Date + DBRelationCollectionFilter.OneDay;
    this._editingContextFilter.Enabled = this.Options.EditingContextVersionID != 0L && dateTime == DateTime.MinValue;
  }

  private bool CheckIsSimpleContext(long editingContextVersionID)
  {
    QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(editingContextVersionID);
    return !objectInfo.Empty && MetaDataHelper.IsSimpleEditingContext(objectInfo.ObjectTypeID);
  }

  private void SetFiltersEnabled()
  {
    this.SetEditingContextFilterEnabled();
    this.SetVisibilityFilterEnabled();
    this.SetVersionRuleFilterEnabled();
    this.SetSeriesAndDatesEnabled();
    this.SetConcretizationEnabled();
  }

  private void SetVisibilityFilterEnabled()
  {
    this._visibilityFilter.Enabled = false;
    if (this.Options.LocalTypesMode || this.Options.ChildObjectTypeIds == null)
      return;
    foreach (int childObjectTypeId in this.Options.ChildObjectTypeIds)
    {
      IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(childObjectTypeId, Intermech.Search.Data.Filters.Constants.ObjectVisibilityAttributeTypeID);
      if (attribute4ObjectType != null && (attribute4ObjectType.OptimizationMode == OptimizationModes.Read || attribute4ObjectType.OptimizationMode == OptimizationModes.Seek))
        this._visibilityFilter.Enabled = true;
    }
  }

  private void SetVersionRuleFilterEnabled()
  {
    this._versionRuleFilter.Enabled = this.Options.VersionRule.CurrentRuleType != VersionsRuleType.vrtAllVersionsRule && this.Options.VersionRule.RuleObjectGuid != "cad005ac-306c-11d8-b4e9-00304f19f5455" && this.Options.VersionRule.CheckCriterions();
  }

  private void SetSeriesAndDatesEnabled()
  {
    this._seriesAndDatesFilter.Enabled = this.Options.SeriesDateSettingsHolder != null && !this.Options.SeriesDateSettingsHolder.IsEmpty && !this.Options.BlokSeriesAndDatesFilters && this.UserSession.EnabledSeriesDates;
  }

  private void SetConcretizationEnabled()
  {
    bool flag1 = true;
    if (this.Options.RelationTypeID != -1)
      flag1 = this.UserSession.GetRelationType(this.Options.RelationTypeID).Attributes.GetAttributeByID(Intermech.Search.Data.Filters.Constants.VersionIDInCompositionAttributeTypeID) != null && this.Options.SelectFunction != SelectFunction.EntersIn;
    this._hardConcretizationFilter.Enabled = flag1;
    this._invalidHardConcretizationFilter.Enabled = flag1;
    bool flag2 = this.Options.VersionRule != null && this.Options.VersionRule.IgnoreSoftConcretization;
    this._softConcretizationFilter.Enabled = flag1 && !flag2;
    this._invalidSoftConcretizationFilter.Enabled = flag1 && !flag2;
  }

  public sealed class DBRelationCollectionFilterOptions : FilterOptions
  {
    public DBRelationCollectionFilterOptions() => this.SelectFunction = SelectFunction.Default;

    public bool LocalTypesMode { get; set; }

    public SelectFunction SelectFunction { get; set; }

    public SeriesDateSettingsHolder SeriesDateSettingsHolder { get; set; }

    public bool ShowInvalidConcreteVersions { get; set; }

    public bool BlokSeriesAndDatesFilters { get; set; }

    public bool UseStoredExplicitPartVersionID { get; set; }
  }

  public sealed class DBRelationCollectionSetFiltersEnabledParams
  {
  }

  public enum DBRelationCollectionFilterMode
  {
    Default,
    AllVersions,
    AllVersionsWithConcretization,
  }

  public sealed class FilterAdapter<T> : IFilter where T : IFilter
  {
    public FilterAdapter(T filter, bool inversed = false)
    {
      this.Filter = (object) filter != null ? filter : throw new ArgumentNullException(nameof (filter));
      this.Inversed = inversed;
    }

    public bool Enabled { get; set; }

    public T Filter { get; private set; }

    public bool Inversed { get; private set; }

    public bool Apply(Applicability applicability) => throw new NotImplementedException();

    public bool Apply(CompositionPart compositionPart) => throw new NotImplementedException();

    public IEnumerable<Applicability> Apply(IEnumerable<Applicability> applicabilities)
    {
      if (this.Enabled)
        return this.Filter.Apply(applicabilities);
      return !this.Inversed ? (IEnumerable<Applicability>) new List<Applicability>(0) : applicabilities;
    }

    public IEnumerable<CompositionPart> Apply(IEnumerable<CompositionPart> composition)
    {
      if (this.Enabled)
        return this.Filter.Apply(composition);
      return !this.Inversed ? (IEnumerable<CompositionPart>) new List<CompositionPart>(0) : composition;
    }

    public List<ColumnDescriptor> Columns
    {
      get => !this.Enabled ? new List<ColumnDescriptor>(0) : this.Filter.Columns;
    }

    public void Configure(FilterOptions options)
    {
      if (!this.Enabled)
        return;
      this.Filter.Configure(options);
    }
  }
}
