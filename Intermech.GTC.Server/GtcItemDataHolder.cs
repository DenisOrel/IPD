// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.GtcItemDataHolder
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces.Entities;
using Intermech.GTC.Server.P21;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.GTC.Server;

public class GtcItemDataHolder
{
  private const string Pattern = "#\\d*\\s?\\=\\s?[^;]*";
  private EntityObjects _entityCache;
  private IItem _item;
  private IItemVersion _itemVersion;
  private IItemDefinition _itemDefinition;
  private int _objectTypeId = -1;
  private string _itemVersionId = string.Empty;
  private string _organization = string.Empty;
  private string[] _alternativeIdentification;
  private Tuple<string, string, string>[] _plibClasses;
  private Tuple<string, string, string>[] _externalLibClasses;
  private Tuple<string, string, string[], object[]>[] _plibProperties;
  private Tuple<string, string, string, string[], object[]>[] _externalLibProperties;
  private string _effectivity;
  private string _coating;
  private Tuple<string, string, string>[] _propRelationShip;
  private string _languaugeCode;

  public GtcItemDataHolder(string filePath, string languaugeCode = "rus")
  {
    this._languaugeCode = languaugeCode;
    this.ReadFileData(filePath);
    this.GetItemData();
  }

  public string Designation => this.GetDesignation();

  public string Name => this.GetName();

  public string ItemVersionId => this._itemVersionId;

  public int ObjectTypeId => this._objectTypeId;

  public string Organization => this._organization;

  public string[] AlternativeIdentification => this._alternativeIdentification;

  public Tuple<string, string>[] Files => this.GetFiles();

  public Tuple<string, string, string>[] PlibClasses => this._plibClasses;

  public Tuple<string, string, string>[] ExternalLibraryClasses => this._externalLibClasses;

  public string Coating => this._coating;

  public Tuple<string, string, string, string[], object[]>[] ExternalLibProperties
  {
    get => this._externalLibProperties;
  }

  public Tuple<string, string, string[], object[]>[] PlibProperties => this._plibProperties;

  public string Effectivity => this._effectivity;

  public List<string> UnusedElements => this.GetUnusedElements();

  public Tuple<string, string, string>[] PropRelationShip => this._propRelationShip;

  private void ReadFileData(string filePath)
  {
    this._entityCache = new EntityObjects();
    MatchCollection source = Regex.Matches(File.ReadAllText(filePath), "#\\d*\\s?\\=\\s?[^;]*", RegexOptions.Multiline);
    if (source.Count <= 0)
      throw new Exception("Data section not found");
    this._entityCache.SetEntitiesData(source.Cast<Match>().Select<Match, string>((Func<Match, string>) (m => m.Value)).ToArray<string>());
  }

  private void GetItemData()
  {
    if (this._entityCache.ObjectCashe.Count == 0)
      throw new Exception("Отсутствуют данные файла!");
    this._item = this.GetItem();
    this._itemVersion = this.GetItemVersion();
    this._itemDefinition = this.GetItemDefinition();
    this._objectTypeId = this.GetObjectTypeId();
    this._itemVersionId = this._itemVersion.Id;
    this._organization = this.GetOrganization();
    this._alternativeIdentification = this.GetAlternativeIdentification();
    this._plibClasses = this.GetPlibClasses();
    this._externalLibClasses = this.GetExternalLibClasses();
    this._plibProperties = this.GetPlibProperties();
    this._externalLibProperties = this.GetExternalLibProperties();
    this._effectivity = this.GetEffectivity();
    this._coating = this.GetCoating();
    this._propRelationShip = this.GetPropertyValueRelationship();
  }

  private IItem GetItem()
  {
    IItem obj = this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (baseObj => baseObj is IItem)).Select<IBaseObject, IItem>((Func<IBaseObject, IItem>) (baseObj => (IItem) baseObj)).FirstOrDefault<IItem>();
    if (obj == null)
      throw new Exception("");
    obj.Used = true;
    return obj;
  }

  private IItemVersion GetItemVersion()
  {
    IItemVersion itemVersion1 = this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (baseObj => baseObj is IItemVersion)).Select<IBaseObject, IItemVersion>((Func<IBaseObject, IItemVersion>) (baseObj => (IItemVersion) baseObj)).FirstOrDefault<IItemVersion>((Func<IItemVersion, bool>) (itemVersion => itemVersion.AssociatedItem == this._item));
    if (itemVersion1 == null)
      throw new Exception("");
    itemVersion1.Used = true;
    return itemVersion1;
  }

  private IItemDefinition GetItemDefinition()
  {
    IItemDefinition itemDefinition1 = this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (baseObj => baseObj is IItemDefinition)).Select<IBaseObject, IItemDefinition>((Func<IBaseObject, IItemDefinition>) (baseObj => (IItemDefinition) baseObj)).FirstOrDefault<IItemDefinition>((Func<IItemDefinition, bool>) (itemDefinition => itemDefinition.AssociatedItemVersion == this._itemVersion));
    if (itemDefinition1 == null)
      throw new Exception("");
    itemDefinition1.Used = true;
    return itemDefinition1;
  }

  private int GetObjectTypeId()
  {
    string[] array = this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (specClass => specClass is ISpecificItemClassification)).Select<IBaseObject, ISpecificItemClassification>((Func<IBaseObject, ISpecificItemClassification>) (specClass => (ISpecificItemClassification) specClass)).Where<ISpecificItemClassification>((Func<ISpecificItemClassification, bool>) (specClass => ((IEnumerable<IBaseObject>) specClass.AssociatedItem).Contains<IBaseObject>((IBaseObject) this._item))).Select<ISpecificItemClassification, string>((Func<ISpecificItemClassification, string>) (specClass => specClass.ClassificationName)).ToArray<string>();
    if (array.Length != 2)
      throw new Exception("Must be 2 SpecificItemClassification Names");
    if (array[0] != "detail")
      throw new Exception("Must be detail");
    int objectTypeId;
    switch (array[1])
    {
      case "adaptive item":
        objectTypeId = Const.AdaptiveItemObjectTypeId;
        break;
      case "cutting item":
        objectTypeId = Const.CuttingItemObjectTypeId;
        break;
      case "tool item":
        objectTypeId = Const.ToolItemObjectTypeId;
        break;
      default:
        objectTypeId = -1;
        break;
    }
    return objectTypeId;
  }

  private string GetDesignation()
  {
    return this.GetMultilanguageStringValue(this._item.Name, this._languaugeCode);
  }

  private string GetName()
  {
    return this.GetMultilanguageStringValue(this._item.Description, this._languaugeCode);
  }

  private string GetOrganization()
  {
    string organization1 = string.Empty;
    IPersonOrganizationAssignment organizationAssignment = this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (it => it is IPersonOrganizationAssignment)).Select<IBaseObject, IPersonOrganizationAssignment>((Func<IBaseObject, IPersonOrganizationAssignment>) (it => (IPersonOrganizationAssignment) it)).First<IPersonOrganizationAssignment>((Func<IPersonOrganizationAssignment, bool>) (z => ((IEnumerable<IBaseObject>) z.IsAppliedTo).Contains<IBaseObject>((IBaseObject) this._item)));
    if (organizationAssignment.AssignedPersonOrganization is IOrganization personOrganization1)
      organization1 = this.GetOrganizationStringValue(personOrganization1);
    else if (organizationAssignment.AssignedPersonOrganization is IPersonInOrganization)
    {
      IPersonInOrganization personOrganization = (IPersonInOrganization) organizationAssignment.AssignedPersonOrganization;
      IPerson person = personOrganization.Person;
      if (person != null)
      {
        string str = person.PersonName != string.Empty ? $"Контактное лицо: '{person.PersonName}'" : organization1;
        organization1 = person.PrefferedBuisnessAdress != string.Empty ? str + $" адрес: '{person.PrefferedBuisnessAdress}'" : str;
      }
      IOrganization organization2 = personOrganization.Organization;
      if (organization2 != null)
      {
        string organizationStringValue = this.GetOrganizationStringValue(organization2);
        organization1 = organizationStringValue != string.Empty ? $"{organization1} {organizationStringValue}" : organization1;
      }
    }
    return organization1;
  }

  private string[] GetAlternativeIdentification()
  {
    List<string> stringList = new List<string>();
    foreach (IAliasIdentification aliasIdentification in this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (it => it is IAliasIdentification)).Select<IBaseObject, IAliasIdentification>((Func<IBaseObject, IAliasIdentification>) (it => (IAliasIdentification) it)).Where<IAliasIdentification>((Func<IAliasIdentification, bool>) (x => x.IsAppliedTo == this._item)))
    {
      string empty = string.Empty;
      string str1 = aliasIdentification.Description != null ? $"{this.GetMultilanguageStringValue(aliasIdentification.Description)}" : empty;
      string str2 = aliasIdentification.AliasVersionId != string.Empty ? str1 + $" '{aliasIdentification.AliasVersionId}'" : str1;
      IOrganization aliasScope = aliasIdentification.AliasScope;
      if (aliasScope != null)
      {
        string organizationStringValue = this.GetOrganizationStringValue(aliasScope);
        str2 = organizationStringValue != string.Empty ? $"{str2} {organizationStringValue}" : str2;
      }
      if (str2 != string.Empty)
        stringList.Add(str2);
    }
    return stringList.ToArray();
  }

  private Tuple<string, string>[] GetFiles()
  {
    List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
    foreach (IDocumentVersion documentVersion1 in this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (it => it is IDocumentAssignment)).Select<IBaseObject, IDocumentAssignment>((Func<IBaseObject, IDocumentAssignment>) (it => (IDocumentAssignment) it)).Where<IDocumentAssignment>((Func<IDocumentAssignment, bool>) (x => x.IsAssignedTo == this._itemDefinition)).Select<IDocumentAssignment, IDocumentVersion>((Func<IDocumentAssignment, IDocumentVersion>) (documentAssigment => documentAssigment.AssignedDocument)).Where<IDocumentVersion>((Func<IDocumentVersion, bool>) (documentVersion => documentVersion != null)).ToArray<IDocumentVersion>())
    {
      IDocumentVersion documentVersion = documentVersion1;
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      IDocument associatedDocument = documentVersion.AssociatedDocument;
      if (associatedDocument != null)
      {
        str1 = associatedDocument.Description != null ? this.GetMultilanguageStringValue(associatedDocument.Description) : string.Empty;
        str2 = associatedDocument.Name != null ? this.GetMultilanguageStringValue(associatedDocument.Name) : string.Empty;
        str3 = associatedDocument.DocumentId;
      }
      string str4 = documentVersion.Description != null ? this.GetMultilanguageStringValue(documentVersion.Description) : string.Empty;
      string id = documentVersion.Id;
      foreach (IDigitalFile digitalFile in this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (it => it is IDigitalDocument && ((IDigitalDocument) it).DocumentVersion == documentVersion)).Select<IBaseObject, IDigitalDocument>((Func<IBaseObject, IDigitalDocument>) (it => (IDigitalDocument) it)).Where<IDigitalDocument>((Func<IDigitalDocument, bool>) (digtalDocument => digtalDocument.File != null && digtalDocument.File.Length != 0)).SelectMany<IDigitalDocument, IDigitalFile>((Func<IDigitalDocument, IEnumerable<IDigitalFile>>) (digtalDocument => (IEnumerable<IDigitalFile>) digtalDocument.File)).Where<IDigitalFile>((Func<IDigitalFile, bool>) (a => a != null)).ToArray<IDigitalFile>())
      {
        string fileId = digitalFile.FileId;
        string versionId = digitalFile.VersionId;
        IDocumentFormatProperty fileFormat = digitalFile.FileFormat;
        string str5 = string.Empty;
        string str6 = string.Empty;
        if (fileFormat != null)
        {
          str5 = fileFormat.DataFormat;
          str6 = fileFormat.CharacterCode;
        }
        foreach (IExternalFileIdAndLocation fileIdAndLocation in digitalFile.ExternalIdAndLocation)
        {
          if (fileIdAndLocation != null)
          {
            string str7 = string.Empty;
            IDocumentLocationProperty location = fileIdAndLocation.Location;
            if (location != null)
              str7 = location.LocationName;
            string externalId = fileIdAndLocation.ExternalId;
            string empty = string.Empty;
            if (externalId != string.Empty)
            {
              string str8 = str1 != string.Empty ? $"description: '{str1}'" : empty;
              string str9 = str2 != string.Empty ? $"{str8} name: '{str2}'" : str8;
              string str10 = str3 != string.Empty ? $"{str9} id: '{str3}'" : str9;
              string str11 = str4 != string.Empty ? $"{str10} version description: '{str4}'" : str10;
              string str12 = id != string.Empty ? $"{str11} version id: '{id}'" : str11;
              string str13 = fileId != string.Empty ? $"{str12} file id: '{fileId}'" : str12;
              string str14 = versionId != string.Empty ? $"{str13} file version id: '{versionId}'" : str13;
              string str15 = str5 != string.Empty ? $"{str14} file format: '{str5}'" : str14;
              string str16 = str6 != string.Empty ? $"{str15} file char code: '{str6}'" : str15;
              string str17 = str7 != string.Empty ? $"{str16} location name: '{str7}'" : str16;
              tupleList.Add(new Tuple<string, string>(externalId, str17));
            }
          }
        }
      }
    }
    return tupleList.ToArray();
  }

  private Tuple<string, string, string>[] GetPlibClasses()
  {
    return this._entityCache.ObjectCashe.Values.OfType<IClassificationAssociation>().Where<IClassificationAssociation>((Func<IClassificationAssociation, bool>) (classifAssociation => classifAssociation.ClassifiedElement == this._itemDefinition)).Where<IClassificationAssociation>((Func<IClassificationAssociation, bool>) (classifAssociation => classifAssociation.AssociatedClassification != null)).Select<IClassificationAssociation, IBaseObject>((Func<IClassificationAssociation, IBaseObject>) (classifAssociation => classifAssociation.AssociatedClassification.ClassificationSource)).OfType<IPlibClassReference>().Select<IPlibClassReference, Tuple<string, string, string>>((Func<IPlibClassReference, Tuple<string, string, string>>) (plibClassReference => new Tuple<string, string, string>(plibClassReference.Code, plibClassReference.SupplierBsu, plibClassReference.Version))).Distinct<Tuple<string, string, string>>().ToArray<Tuple<string, string, string>>();
  }

  private Tuple<string, string, string>[] GetExternalLibClasses()
  {
    return this._entityCache.ObjectCashe.Values.OfType<IClassificationAssociation>().Where<IClassificationAssociation>((Func<IClassificationAssociation, bool>) (classifAssociation => classifAssociation.ClassifiedElement == this._itemDefinition)).Where<IClassificationAssociation>((Func<IClassificationAssociation, bool>) (classifAssociation => classifAssociation.AssociatedClassification != null)).Select<IClassificationAssociation, IBaseObject>((Func<IClassificationAssociation, IBaseObject>) (classifAssociation => classifAssociation.AssociatedClassification.ClassificationSource)).OfType<IExternalLibraryReference>().Select<IExternalLibraryReference, Tuple<string, string, string>>((Func<IExternalLibraryReference, Tuple<string, string, string>>) (extrLibReference => new Tuple<string, string, string>(this.GetMultilanguageStringValue(extrLibReference.Description), extrLibReference.ExternalId, extrLibReference.LibraryType))).Distinct<Tuple<string, string, string>>().ToArray<Tuple<string, string, string>>();
  }

  private Tuple<string, string, string[], object[]>[] GetPlibProperties()
  {
    return this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (baseObj => baseObj is IPropertyValueAssociation)).Select<IBaseObject, IPropertyValueAssociation>((Func<IBaseObject, IPropertyValueAssociation>) (baseObj => (IPropertyValueAssociation) baseObj)).Where<IPropertyValueAssociation>((Func<IPropertyValueAssociation, bool>) (propValAssociation => propValAssociation.DescribedElement == this._itemDefinition && propValAssociation.DescribingPropertyValue != null && propValAssociation.DescribingPropertyValue.Definition != null)).Select(propValAssociation => new
    {
      PropertyDefinition = propValAssociation.DescribingPropertyValue.Definition,
      PropertyValue = propValAssociation.DescribingPropertyValue.SpecifiedValue
    }).Where(anonType => anonType.PropertyDefinition.PropertySource is IPlibPropertyReference).GroupBy(anonType => anonType.PropertyDefinition, anonType => anonType.PropertyValue, (key, vals) => new
    {
      Property = key,
      Values = this.GetPropertiesValues(vals)
    }).Select(x => new Tuple<string, string, string[], object[]>(((IPlibPropertyReference) x.Property.PropertySource).Code, ((IPlibPropertyReference) x.Property.PropertySource).NameScope.Code, ((IEnumerable<IUnit>) x.Property.AllowedUnit).Where<IUnit>((Func<IUnit, bool>) (unit => unit != null && unit.UnitName != string.Empty)).Select<IUnit, string>((Func<IUnit, string>) (unit => unit.UnitName)).ToArray<string>(), x.Values)).ToArray<Tuple<string, string, string[], object[]>>();
  }

  private Tuple<string, string, string, string[], object[]>[] GetExternalLibProperties()
  {
    return this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (baseObj => baseObj is IPropertyValueAssociation)).Select<IBaseObject, IPropertyValueAssociation>((Func<IBaseObject, IPropertyValueAssociation>) (baseObj => (IPropertyValueAssociation) baseObj)).Where<IPropertyValueAssociation>((Func<IPropertyValueAssociation, bool>) (propValAssociation => propValAssociation.DescribedElement == this._itemDefinition && propValAssociation.DescribingPropertyValue != null && propValAssociation.DescribingPropertyValue.Definition != null)).Select(propValAssociation => new
    {
      PropertyDefinition = propValAssociation.DescribingPropertyValue.Definition,
      PropertyValue = propValAssociation.DescribingPropertyValue.SpecifiedValue
    }).Where(anonType => anonType.PropertyDefinition.PropertySource is IExternalLibraryReference).GroupBy(anonType => anonType.PropertyDefinition, anonType => anonType.PropertyValue, (key, vals) =>
    {
      IPropertyValue[] array = vals.ToArray<IPropertyValue>();
      return new
      {
        Property = key,
        Values = this.GetPropertiesValues((IEnumerable<IPropertyValue>) array),
        PropertyName = array.Length != 0 ? array[0].ValueName : ((IExternalLibraryReference) key.PropertySource).ExternalId
      };
    }).Select(x => new Tuple<string, string, string, string[], object[]>(x.PropertyName, ((IExternalLibraryReference) x.Property.PropertySource).LibraryType, ((IExternalLibraryReference) x.Property.PropertySource).Description != null ? this.GetMultilanguageStringValue(((IExternalLibraryReference) x.Property.PropertySource).Description, this._languaugeCode) : string.Empty, ((IEnumerable<IUnit>) x.Property.AllowedUnit).Where<IUnit>((Func<IUnit, bool>) (unit => unit != null && unit.UnitName != string.Empty)).Select<IUnit, string>((Func<IUnit, string>) (unit => unit.UnitName)).ToArray<string>(), x.Values)).ToArray<Tuple<string, string, string, string[], object[]>>();
  }

  private List<string> GetUnusedElements()
  {
    return this._entityCache.ObjectCashe.Where<KeyValuePair<string, IBaseObject>>((Func<KeyValuePair<string, IBaseObject>, bool>) (baseObj => !baseObj.Value.Used)).Select<KeyValuePair<string, IBaseObject>, string>((Func<KeyValuePair<string, IBaseObject>, string>) (baseObj => $"{baseObj.Key} {baseObj.Value.ToString()}")).ToList<string>();
  }

  private string GetEffectivity()
  {
    return string.Join("; ", this._entityCache.ObjectCashe.Values.OfType<IEffectivityAssignment>().Where<IEffectivityAssignment>((Func<IEffectivityAssignment, bool>) (effectAssigment => effectAssigment.AssignedEffectivity != null && effectAssigment.EffectiveElement == this._item)).Select<IEffectivityAssignment, IEffectivity>((Func<IEffectivityAssignment, IEffectivity>) (effectAssigment => effectAssigment.AssignedEffectivity)).Where<IEffectivity>((Func<IEffectivity, bool>) (effectivity => effectivity.StartDefinition != null && effectivity.StartDefinition.Date != string.Empty && effectivity.Period != null && effectivity.Period.Time != string.Empty)).Select<IEffectivity, string>((Func<IEffectivity, string>) (effectivity => $"C {effectivity.StartDefinition.Date} {effectivity.Period.Time}")).ToArray<string>());
  }

  private object[] GetPropertiesValues(IEnumerable<IPropertyValue> vals)
  {
    List<object> objectList = new List<object>();
    if (!(vals is IPropertyValue[] propertyValueArray))
      propertyValueArray = vals.ToArray<IPropertyValue>();
    IEnumerable<IPropertyValue> source = (IEnumerable<IPropertyValue>) propertyValueArray;
    foreach (IPropertyValue propertyValue1 in source)
    {
      if (propertyValue1 is IValueList)
      {
        foreach (IPropertyValue propertyValue2 in source.Where<IPropertyValue>((Func<IPropertyValue, bool>) (propVal => propVal is IValueList)).Select<IPropertyValue, IValueList>((Func<IPropertyValue, IValueList>) (propVal => propVal as IValueList)).Select<IValueList, IPropertyValue[]>((Func<IValueList, IPropertyValue[]>) (valList => valList.Values)).SelectMany<IPropertyValue[], IPropertyValue>((Func<IPropertyValue[], IEnumerable<IPropertyValue>>) (propVal => (IEnumerable<IPropertyValue>) propVal)))
        {
          object propertyValue3 = this.GetPropertyValue(propertyValue2);
          if (propertyValue3 != null)
            objectList.Add(propertyValue3);
        }
      }
      else
      {
        object propertyValue4 = this.GetPropertyValue(propertyValue1);
        if (propertyValue4 != null)
          objectList.Add(propertyValue4);
      }
    }
    return objectList.ToArray();
  }

  private object GetPropertyValue(IPropertyValue value)
  {
    switch (value)
    {
      case IStringValue stringValue:
        return (object) this.GetMultilanguageStringValue(stringValue.ValueSpecification);
      case INumericalValue numericalValue:
        return (object) numericalValue.ValueComponent;
      case IValueLimit valueLimit:
        return (object) $"{valueLimit.Limit} {valueLimit.LimitQualifier}";
      case IValueRange valueRange:
        return valueRange.UnitComponent == null || !(valueRange.UnitComponent.UnitName != string.Empty) ? (object) $"{valueRange.LowerLimit}..{valueRange.UpperLimit}" : (object) $"{valueRange.LowerLimit}..{valueRange.UpperLimit} {valueRange.UnitComponent.UnitName}";
      default:
        return (object) null;
    }
  }

  private string GetCoating()
  {
    return string.Join("; ", this._entityCache.ObjectCashe.Values.Where<IBaseObject>((Func<IBaseObject, bool>) (baseObj => baseObj is IItemCharacteristicAssociation)).Select<IBaseObject, IItemCharacteristicAssociation>((Func<IBaseObject, IItemCharacteristicAssociation>) (baseObj => (IItemCharacteristicAssociation) baseObj)).Where<IItemCharacteristicAssociation>((Func<IItemCharacteristicAssociation, bool>) (itemCharacteristicAssociation => itemCharacteristicAssociation.AssociatedItem == this._itemDefinition && itemCharacteristicAssociation.AssociatedCharacteristic != null)).Select<IItemCharacteristicAssociation, IGrade>((Func<IItemCharacteristicAssociation, IGrade>) (itemCharacteristicAssociation => itemCharacteristicAssociation.AssociatedCharacteristic)).Select(grade => new
    {
      identifier = grade.Identifier,
      standartDesignation = grade.StandartDesignation,
      substrateName = grade.Substrate != null ? grade.Substrate.Name : string.Empty,
      coatingName = grade.Coating != null ? grade.Coating.CoatingName : string.Empty,
      coatingProcess = grade.Coating != null ? grade.Coating.CoatingProcess : string.Empty,
      materialDesignation = grade.WorkpieceMaterial != null ? string.Join(",", ((IEnumerable<IMaterialDesignation>) grade.WorkpieceMaterial).Select<IMaterialDesignation, string>((Func<IMaterialDesignation, string>) (x => x.MaterialName)).ToArray<string>()) : string.Empty,
      conditionName = grade.CuttingCondition != null ? string.Join(",", ((IEnumerable<ICuttingCondition>) grade.CuttingCondition).Select<ICuttingCondition, string>((Func<ICuttingCondition, string>) (x => x.ConditionName)).ToArray<string>()) : string.Empty
    }).Select(anonType => this.ConcatCoatingString(anonType.identifier, anonType.standartDesignation, anonType.substrateName, anonType.coatingName, anonType.coatingProcess, anonType.materialDesignation, anonType.conditionName)).ToArray<string>());
  }

  private string ConcatCoatingString(
    string identifier,
    string standartDesignation,
    string substrateName,
    string coatingName,
    string coatingProcess,
    string materialDesignation,
    string conditionName)
  {
    string empty = string.Empty;
    string str1 = identifier != string.Empty ? string.Format(ServiceHolder.Rm.GetString("GTC_8"), (object) identifier) : empty;
    string str2 = standartDesignation != string.Empty ? string.Format(ServiceHolder.Rm.GetString("GTC_9"), (object) str1, (object) standartDesignation) : str1;
    string str3 = substrateName != string.Empty ? string.Format(ServiceHolder.Rm.GetString("GTC_10"), (object) str2, (object) substrateName) : str2;
    string str4 = coatingName != string.Empty ? string.Format(ServiceHolder.Rm.GetString("GTC_11"), (object) str3, (object) coatingName) : str3;
    string str5 = coatingProcess != string.Empty ? string.Format(ServiceHolder.Rm.GetString("GTC_12"), (object) str4, (object) coatingProcess) : str4;
    string str6 = materialDesignation != string.Empty ? string.Format(ServiceHolder.Rm.GetString("GTC_13"), (object) str5, (object) materialDesignation) : str5;
    return conditionName != string.Empty ? string.Format(ServiceHolder.Rm.GetString("GTC_14"), (object) str6, (object) conditionName) : str6;
  }

  private Tuple<string, string, string>[] GetPropertyValueRelationship()
  {
    return this._entityCache.ObjectCashe.Values.OfType<IPropertyValueRepresentationRelationship>().Select<IPropertyValueRepresentationRelationship, Tuple<string, string, string>>((Func<IPropertyValueRepresentationRelationship, Tuple<string, string, string>>) (propValreprRelationship => new Tuple<string, string, string>(this.GetPropertyIdName(propValreprRelationship.Related), this.GetPropertyIdName(propValreprRelationship.Relating), propValreprRelationship.RelationType))).ToArray<Tuple<string, string, string>>();
  }

  private string GetPropertyIdName(IPropertyValueRepresentation propValRepresentation)
  {
    if (propValRepresentation.Definition.PropertySource is IPlibPropertyReference propertySource1)
      return propertySource1.Code;
    return propValRepresentation.Definition.PropertySource is IExternalLibraryReference propertySource2 ? propertySource2.ExternalId : string.Empty;
  }

  private string GetMultilanguageStringValue(IMultiLanguageString source, string languaugeCode = "eng")
  {
    Dictionary<string, string> source1 = new Dictionary<string, string>();
    if (source.PrimaryLanguageString != null && source.PrimaryLanguageString.LanguageSpecification != null)
      source1.Add(source.PrimaryLanguageString.LanguageSpecification.LanguageCode, source.PrimaryLanguageString.Contents);
    if (source.AdditionalLanguageString != null)
    {
      foreach (KeyValuePair<string, string> keyValuePair in ((IEnumerable<IStringWithLanguage>) source.AdditionalLanguageString).Where<IStringWithLanguage>((Func<IStringWithLanguage, bool>) (stringWithLanguage => stringWithLanguage != null && stringWithLanguage.LanguageSpecification != null)).Select<IStringWithLanguage, KeyValuePair<string, string>>((Func<IStringWithLanguage, KeyValuePair<string, string>>) (stringWithLanguage => new KeyValuePair<string, string>(stringWithLanguage.LanguageSpecification.LanguageCode, stringWithLanguage.Contents))))
      {
        if (!source1.ContainsKey(keyValuePair.Key))
          source1.Add(keyValuePair.Key, keyValuePair.Value);
      }
    }
    if (source1.Values.Count.Equals(0))
      throw new Exception(ServiceHolder.Rm.GetString("GTC_17"));
    string multilanguageStringValue;
    if (!source1.TryGetValue(languaugeCode, out multilanguageStringValue))
      multilanguageStringValue = source1.First<KeyValuePair<string, string>>().Value;
    return multilanguageStringValue;
  }

  private string GetOrganizationStringValue(IOrganization source)
  {
    string empty = string.Empty;
    string str1 = source.OrganizationName != string.Empty ? $"Организация: '{source.OrganizationName}'" : empty;
    string str2 = source.OrganizationType != string.Empty ? str1 + $" тип: '{source.OrganizationType}'" : str1;
    string str3 = source.Id != string.Empty ? str2 + $" id: '{source.Id}'" : str2;
    string str4 = source.DeliveryAdress != string.Empty ? str3 + $" адрес: '{source.DeliveryAdress}'" : str3;
    string str5 = source.PostalAdress != string.Empty ? str4 + $" почтовый адрес: '{source.PostalAdress}'" : str4;
    return source.VisitorAdress != string.Empty ? str5 + $" адрес: '{source.VisitorAdress}'" : str5;
  }
}
