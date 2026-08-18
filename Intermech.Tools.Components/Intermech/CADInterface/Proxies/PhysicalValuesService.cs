// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.PhysicalValuesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Tools.Data;
using Interop.CADInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class PhysicalValuesService
{
  private static readonly PhysicalValuesService.MappingTable mappingTable = new PhysicalValuesService.MappingTable();

  public MeasuredValue ToMeasuredValue(IPhysicalQuantity pQuantity)
  {
    return this.ToMeasuredValue(pQuantity, new int?());
  }

  public MeasuredValue ToMeasuredValue(IPhysicalQuantity pQuantity, int? precision)
  {
    if (pQuantity == null)
      throw new ArgumentNullException(nameof (pQuantity));
    if (pQuantity.BaseUnits == EMainUnits.UNIT_Undefined)
      throw new NotSupportedException(LocalizationHolder.rm.GetString("SR_525"));
    if (precision.HasValue && (precision.Value < 0 || precision.Value > 15))
      throw new ArgumentException("Для округления должно использоваться от 0 до 15 разрядов включительно.", nameof (precision));
    int digits = precision.HasValue ? precision.Value : pQuantity.Precision;
    if (digits < 0)
      digits = 6;
    double aValue = Math.Round(pQuantity.Value, digits);
    bool flag = pQuantity is IPhysicalQuantity2 physicalQuantity2 && physicalQuantity2.Unit != null;
    long measureId = this.ToMeasureDescriptor(pQuantity.BaseUnits, pQuantity.Ratio, flag ? physicalQuantity2.Unit.ShortName : (string) null).MeasureID;
    MeasuredValue measuredValue = new MeasuredValue(aValue, measureId);
    if (flag)
      measuredValue.Caption = physicalQuantity2.get_ValueAsString2(true);
    return measuredValue;
  }

  public PhysicalQuantity ToPhysicalQuantity(MeasuredValue mValue)
  {
    MeasureDescriptor mDescriptor = mValue != null ? MeasureHelper.FindDescriptor(mValue) : throw new ArgumentNullException(nameof (mValue));
    PhysicalUnit physicalUnit = !mDescriptor.Empty ? this.ToPhysicalUnit(mDescriptor) : throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("SR_526"), (object) mValue));
    return (PhysicalQuantity) new PhysicalQuantityClass()
    {
      Unit = physicalUnit,
      Precision = 15,
      Value = mValue.Value
    };
  }

  public PhysicalUnit ToPhysicalUnit(MeasureDescriptor mDescriptor, bool throwIfNotPossible = true)
  {
    PhysicalValuesService.MeasureDescriptorMapping descriptorMapping = mDescriptor != null ? PhysicalValuesService.mappingTable.TryGetMapping(mDescriptor) : throw new ArgumentNullException(nameof (mDescriptor));
    if (descriptorMapping != null)
      return descriptorMapping.PhysicalUnit;
    if (!throwIfNotPossible)
      return (PhysicalUnit) null;
    throw new FaultException($"Не удалось преобразовать единицу измерения '{mDescriptor}' в объект типа '{typeof (PhysicalUnit)}', так как она не поддерживается CAD-системой. При вводе измеряемых величин в IPS вы должны использовать только те единицы измерения, которые поддерживаются вашей CAD-системой.");
  }

  public MeasureDescriptor ToMeasureDescriptor(IPhysicalUnit physicalUnit, bool throwIfNotPossible = true)
  {
    if (physicalUnit == null)
      throw new ArgumentNullException(nameof (physicalUnit));
    PhysicalValuesService.MeasureDescriptorMapping mapping = PhysicalValuesService.mappingTable.TryGetMapping(physicalUnit.BaseUnits, physicalUnit.Ratio, physicalUnit.ShortName);
    if (mapping != null)
      return mapping.MeasureDescriptor;
    if (!throwIfNotPossible)
      return (MeasureDescriptor) null;
    throw new FaultException($"Не удалось преобразовать единицу измерения '{physicalUnit.ShortName} ({physicalUnit.BaseUnits}, {physicalUnit.Ratio})' в объект типа '{typeof (MeasureDescriptor)}', так как она не поддерживается PDM-системой. Обратитесь к администратору IPS, чтобы он добавил в базу данных описание этой единицы измерения.");
  }

  public MeasureDescriptor ToMeasureDescriptor(
    EMainUnits baseUnits,
    double ratio,
    string shortName = null,
    bool throwIfNotPossible = true)
  {
    PhysicalValuesService.MeasureDescriptorMapping mapping = PhysicalValuesService.mappingTable.TryGetMapping(baseUnits, ratio, shortName);
    if (mapping != null)
      return mapping.MeasureDescriptor;
    if (!throwIfNotPossible)
      return (MeasureDescriptor) null;
    throw new FaultException($"Не удалось преобразовать единицу измерения '({baseUnits}, {ratio})' в объект типа '{typeof (MeasureDescriptor)}', так как она не поддерживается PDM-системой. Обратитесь к администратору IPS, чтобы он добавил в базу данных описание этой единицы измерения.");
  }

  public PhysicalUnit[] GetAvsPhysicalUnits()
  {
    List<PhysicalUnit> physicalUnitList = new List<PhysicalUnit>(MeasureHelper.Measures.Length);
    long defaultMeasureId = this.TryGetAvsDefaultMeasureId();
    if (defaultMeasureId != 0L)
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(defaultMeasureId);
      if (!descriptor.Empty)
      {
        PhysicalUnit physicalUnit = this.ToPhysicalUnit(descriptor, false);
        if (physicalUnit != null)
          physicalUnitList.Add(physicalUnit);
      }
    }
    foreach (MeasureDescriptor measure in MeasureHelper.Measures)
    {
      if (defaultMeasureId == 0L || measure.MeasureID != defaultMeasureId)
      {
        PhysicalUnit physicalUnit = this.ToPhysicalUnit(measure, false);
        if (physicalUnit != null)
          physicalUnitList.Add(physicalUnit);
      }
    }
    return physicalUnitList.ToArray();
  }

  private long TryGetAvsDefaultMeasureId()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetRelationType(IDCache.Default.ArticleTree.Id).Attributes.GetAttributeByID(IDCache.Default.Count.Id) is IDBMeasureAttributeType attributeById)
        return attributeById.DefaultMeasureID;
    }
    return 0;
  }

  private sealed class MeasureDescriptorMapping
  {
    public MeasureDescriptorMapping(MeasureDescriptor descriptor, PhysicalUnit physicalUnit)
    {
      this.MeasureDescriptor = descriptor;
      this.PhysicalUnit = physicalUnit;
    }

    public MeasureDescriptor MeasureDescriptor { get; private set; }

    public PhysicalUnit PhysicalUnit { get; private set; }
  }

  private sealed class MeasureDescriptorKeyComparer : IEqualityComparer<MeasureDescriptor>
  {
    public bool Equals(MeasureDescriptor x, MeasureDescriptor y) => x.MeasureID == y.MeasureID;

    public int GetHashCode(MeasureDescriptor obj) => obj.MeasureID.GetHashCode();
  }

  private sealed class PhysicalUnitKey
  {
    public PhysicalUnitKey(EMainUnits baseUnits, Decimal ratio, string shortName)
    {
      this.BaseUnits = baseUnits;
      this.Ratio = ratio;
      this.ShortName = shortName;
    }

    public static PhysicalValuesService.PhysicalUnitKey FromPhysicalUnit(PhysicalUnit physicalUnit)
    {
      if (physicalUnit == null)
        throw new ArgumentNullException(nameof (physicalUnit));
      return new PhysicalValuesService.PhysicalUnitKey(physicalUnit.BaseUnits, (Decimal) physicalUnit.Ratio, physicalUnit.ShortName);
    }

    public EMainUnits BaseUnits { get; private set; }

    public Decimal Ratio { get; private set; }

    public string ShortName { get; private set; }
  }

  private sealed class PhysicalUnitKeyComparer : 
    IEqualityComparer<PhysicalValuesService.PhysicalUnitKey>
  {
    public bool Equals(
      PhysicalValuesService.PhysicalUnitKey x,
      PhysicalValuesService.PhysicalUnitKey y)
    {
      return x.BaseUnits == y.BaseUnits && x.Ratio == y.Ratio && x.ShortName == y.ShortName;
    }

    public int GetHashCode(PhysicalValuesService.PhysicalUnitKey obj)
    {
      return obj.BaseUnits.GetHashCode() << 16 /*0x10*/ ^ obj.Ratio.GetHashCode();
    }
  }

  private sealed class MappingTable
  {
    private List<PhysicalValuesService.MeasureDescriptorMapping> mappingTable;
    private Dictionary<MeasureDescriptor, PhysicalValuesService.MeasureDescriptorMapping> indexByMeasureDescriptor;
    private Dictionary<PhysicalValuesService.PhysicalUnitKey, PhysicalValuesService.MeasureDescriptorMapping> indexByPhysicalUnit;
    private static readonly Decimal[] allowedDecimalRatios = new Decimal[24]
    {
      0.000000000001M,
      0.00000000001M,
      0.0000000001M,
      0.000000001M,
      0.00000001M,
      0.0000001M,
      0.000001M,
      0.00001M,
      0.0001M,
      0.001M,
      0.01M,
      0.1M,
      10M,
      100M,
      1000M,
      10000M,
      100000M,
      1000000M,
      10000000M,
      100000000M,
      1000000000M,
      10000000000M,
      100000000000M,
      1000000000000M
    };
    private static readonly Decimal[] allowedTimeRatios = new Decimal[6]
    {
      60.0M,
      3600M,
      86400M,
      604800M,
      2592000M,
      2678400M
    };
    private static readonly Decimal[] noAllowedRatios = new Decimal[0];
    private static readonly string unnamedPhysicalUnit = string.Empty;

    public MappingTable()
    {
      this.mappingTable = new List<PhysicalValuesService.MeasureDescriptorMapping>(64 /*0x40*/);
      this.indexByMeasureDescriptor = new Dictionary<MeasureDescriptor, PhysicalValuesService.MeasureDescriptorMapping>(64 /*0x40*/, (IEqualityComparer<MeasureDescriptor>) new PhysicalValuesService.MeasureDescriptorKeyComparer());
      this.indexByPhysicalUnit = new Dictionary<PhysicalValuesService.PhysicalUnitKey, PhysicalValuesService.MeasureDescriptorMapping>(64 /*0x40*/, (IEqualityComparer<PhysicalValuesService.PhysicalUnitKey>) new PhysicalValuesService.PhysicalUnitKeyComparer());
      this.InitializeTables();
    }

    private void InitializeTables()
    {
      try
      {
        this.InitializeMappingTable();
        this.InitializeIndexByMeasureDescriptor();
        this.InitializeIndexByPhysicalUnit();
      }
      catch
      {
        this.mappingTable.Clear();
        this.indexByMeasureDescriptor.Clear();
        this.indexByPhysicalUnit.Clear();
        throw;
      }
    }

    private void InitializeMappingTable()
    {
      foreach (EMainUnits mainUnit in Enum.GetValues(typeof (EMainUnits)))
      {
        if (mainUnit != EMainUnits.UNIT_Undefined)
        {
          MeasureDescriptor mainDescriptor = this.GetMainUnitMeasureDescriptor(mainUnit);
          this.mappingTable.Add(new PhysicalValuesService.MeasureDescriptorMapping(mainDescriptor, this.CreatePhysicalUnit(mainUnit, 1.0, mainDescriptor.LongName, mainDescriptor.ShortName)));
          Decimal[] unitAllowedRatios = this.GetMainUnitAllowedRatios(mainUnit);
          if (unitAllowedRatios.Length != 0)
          {
            List<MeasureDescriptor> allAsList = CollectionUtils.FindAllAsList<MeasureDescriptor>((ICollection<MeasureDescriptor>) MeasureHelper.Measures, (Predicate<MeasureDescriptor>) (measure => measure.PhysicalQuantityID == mainDescriptor.PhysicalQuantityID && measure.MeasureID != mainDescriptor.MeasureID));
            if (allAsList.Count != 0)
            {
              foreach (MeasureDescriptor descriptor in allAsList)
              {
                Decimal num = (Decimal) descriptor.K / (Decimal) mainDescriptor.K;
                if (CollectionUtils.Contains<Decimal>((IEnumerable<Decimal>) unitAllowedRatios, num))
                  this.mappingTable.Add(new PhysicalValuesService.MeasureDescriptorMapping(descriptor, this.CreatePhysicalUnit(mainUnit, descriptor.K / mainDescriptor.K, descriptor.LongName, descriptor.ShortName)));
              }
            }
          }
        }
      }
    }

    private void InitializeIndexByMeasureDescriptor()
    {
      foreach (PhysicalValuesService.MeasureDescriptorMapping descriptorMapping in this.mappingTable)
        this.indexByMeasureDescriptor.Add(descriptorMapping.MeasureDescriptor, descriptorMapping);
    }

    private void InitializeIndexByPhysicalUnit()
    {
      foreach (PhysicalValuesService.MeasureDescriptorMapping descriptorMapping in this.mappingTable)
      {
        PhysicalValuesService.PhysicalUnitKey key1 = PhysicalValuesService.PhysicalUnitKey.FromPhysicalUnit(descriptorMapping.PhysicalUnit);
        this.indexByPhysicalUnit.Add(key1, descriptorMapping);
        PhysicalValuesService.PhysicalUnitKey key2 = new PhysicalValuesService.PhysicalUnitKey(key1.BaseUnits, key1.Ratio, PhysicalValuesService.MappingTable.unnamedPhysicalUnit);
        if (!this.indexByPhysicalUnit.ContainsKey(key2))
          this.indexByPhysicalUnit.Add(key2, descriptorMapping);
      }
    }

    private MeasureDescriptor GetMainUnitMeasureDescriptor(EMainUnits mainUnit)
    {
      string measureObjectGuid = this.GetMainUnitMeasureObjectGuid(mainUnit);
      if (!string.IsNullOrEmpty(measureObjectGuid))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(new Guid(measureObjectGuid), false);
          if (dbObject != null)
          {
            MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(dbObject.ObjectID);
            if (!descriptor.Empty)
              return descriptor;
          }
        }
      }
      throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("SR_528"), (object) mainUnit));
    }

    private string GetMainUnitMeasureObjectGuid(EMainUnits mainUnit)
    {
      switch (mainUnit)
      {
        case EMainUnits.UNIT_Undefined:
          return string.Empty;
        case EMainUnits.UNIT_Kilogram:
          return "cad002eb-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_Pound:
          return "cad014aa-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_KilogramPerM3:
          return "cad00300-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_Meter:
          return "cad002e4-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_SquareMeter:
          return "cad002f5-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_CubicMeter:
          return "cad002f0-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_Second:
          return "cad002e1-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_Radian:
          return "cae053ff-4355-4b09-89d0-f377dc406064";
        case EMainUnits.UNIT_Degree:
          return "cad00322-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_Item:
          return "cad002e8-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_WattPerMeterKelvin:
          return "cadd95c5-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_JoulePerKilogramKelvin:
          return "cadd95c7-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_Pascal:
          return "cad0030c-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_Inch:
          return "cadd9a12-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_Foot:
          return "cadd9a19-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_SquareInch:
          return "cadd9a15-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_SquareFoot:
          return "cadd9a1b-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_CubicInch:
          return "cadd9a17-306c-11d8-b4e9-00304f19f545";
        case EMainUnits.UNIT_CubicFoot:
          return "cadd9a1d-306c-11d8-b4e9-00304f19f545";
        default:
          throw new NotSupportedEnumException((Enum) mainUnit);
      }
    }

    private Decimal[] GetMainUnitAllowedRatios(EMainUnits mainUnit)
    {
      switch (mainUnit)
      {
        case EMainUnits.UNIT_Kilogram:
        case EMainUnits.UNIT_KilogramPerM3:
        case EMainUnits.UNIT_Meter:
        case EMainUnits.UNIT_SquareMeter:
        case EMainUnits.UNIT_CubicMeter:
        case EMainUnits.UNIT_Pascal:
          return PhysicalValuesService.MappingTable.allowedDecimalRatios;
        case EMainUnits.UNIT_Second:
          return PhysicalValuesService.MappingTable.allowedTimeRatios;
        default:
          return PhysicalValuesService.MappingTable.noAllowedRatios;
      }
    }

    private PhysicalUnit CreatePhysicalUnit(
      EMainUnits mainUnit,
      double ratio,
      string longName,
      string shortName)
    {
      if (longName == null)
        throw new ArgumentNullException(nameof (longName));
      if (shortName == null)
        throw new ArgumentNullException(nameof (shortName));
      return (PhysicalUnit) new PhysicalUnitClass()
      {
        BaseUnits = mainUnit,
        Ratio = ratio,
        FullName = longName,
        ShortName = shortName
      };
    }

    public PhysicalValuesService.MeasureDescriptorMapping TryGetMapping(
      MeasureDescriptor mDescriptor)
    {
      PhysicalValuesService.MeasureDescriptorMapping descriptorMapping;
      return this.indexByMeasureDescriptor.TryGetValue(mDescriptor, out descriptorMapping) ? descriptorMapping : (PhysicalValuesService.MeasureDescriptorMapping) null;
    }

    public PhysicalValuesService.MeasureDescriptorMapping TryGetMapping(
      EMainUnits baseUnits,
      double ratio,
      string shortName = null)
    {
      PhysicalValuesService.MeasureDescriptorMapping descriptorMapping;
      return this.indexByPhysicalUnit.TryGetValue(new PhysicalValuesService.PhysicalUnitKey(baseUnits, (Decimal) ratio, shortName != null ? shortName : PhysicalValuesService.MappingTable.unnamedPhysicalUnit), out descriptorMapping) ? descriptorMapping : (PhysicalValuesService.MeasureDescriptorMapping) null;
    }
  }
}
