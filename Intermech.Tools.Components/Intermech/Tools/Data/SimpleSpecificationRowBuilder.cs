// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.SimpleSpecificationRowBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Data.Queries;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.Tools.Data;

internal sealed class SimpleSpecificationRowBuilder : DBQueryRecordBuilder<SimpleSpecificationRow>
{
  private readonly DBQueryAttribute objectIdAttr;
  private readonly DBQueryAttribute designationAttr;
  private readonly DBQueryAttribute okpCodeAttr;
  private readonly DBQueryAttribute nameAttr;
  private readonly DBQueryAttribute spSectionAttr;
  private readonly DBQueryAttribute occurenceKeyAttr;
  private readonly DBQueryAttribute positionAttr;
  private readonly DBQueryAttribute noteAttr;
  private readonly DBQueryAttribute zoneAttr;
  private readonly DBQueryAttribute objGuidAttr;
  private readonly DBQueryAttribute imbaseRefAttr;
  private readonly DBQueryAttribute countAttr;
  private readonly DBQueryAttribute massAttr;
  private readonly DBQueryAttribute materialAttr;
  private string specialSpSection;

  public SimpleSpecificationRowBuilder()
  {
    this.objectIdAttr = new DBQueryAttribute(-2, AttributeSourceTypes.Object, ColumnContents.Text);
    this.designationAttr = new DBQueryAttribute(IDCache.Default.Designation.Id, AttributeSourceTypes.Object, ColumnContents.Text);
    this.okpCodeAttr = new DBQueryAttribute(IDCache.Default.OKPCode.Id, AttributeSourceTypes.Object, ColumnContents.Text);
    this.nameAttr = new DBQueryAttribute(IDCache.Default.Name.Id, AttributeSourceTypes.Object, ColumnContents.Text);
    this.spSectionAttr = new DBQueryAttribute(-7, AttributeSourceTypes.Object, ColumnContents.Text);
    this.occurenceKeyAttr = new DBQueryAttribute(IDCache.Default.OccurenceKey.Id, AttributeSourceTypes.Relation, ColumnContents.Text);
    this.positionAttr = new DBQueryAttribute(IDCache.Default.Position.Id, AttributeSourceTypes.Relation, ColumnContents.Text);
    this.noteAttr = new DBQueryAttribute(IDCache.Default.Note.Id, AttributeSourceTypes.Relation, ColumnContents.Text);
    this.zoneAttr = new DBQueryAttribute(IDCache.Default.Zone.Id, AttributeSourceTypes.Relation, ColumnContents.Text);
    this.objGuidAttr = new DBQueryAttribute(-18, AttributeSourceTypes.Object, ColumnContents.Text);
    this.imbaseRefAttr = new DBQueryAttribute(IDCache.Default.ImbaseRef.Id, AttributeSourceTypes.Object, ColumnContents.ID);
    this.countAttr = new DBQueryAttribute(IDCache.Default.Count.Id, AttributeSourceTypes.Relation, ColumnContents.Text);
    this.massAttr = new DBQueryAttribute(IDCache.Default.Mass.Id, AttributeSourceTypes.Object, ColumnContents.Text);
    this.materialAttr = new DBQueryAttribute(IDCache.Default.Material.Id, AttributeSourceTypes.Object, ColumnContents.Text);
  }

  public SimpleSpecificationRowBuilder(string specialSpSection)
    : this()
  {
    this.specialSpSection = !string.IsNullOrEmpty(specialSpSection) ? specialSpSection : throw new ArgumentException();
  }

  protected override void DoAttachQuery()
  {
    base.DoAttachQuery();
    this.Query.Attributes.Add(this.objectIdAttr);
    this.Query.Attributes.Add(this.designationAttr);
    this.Query.Attributes.Add(this.okpCodeAttr);
    this.Query.Attributes.Add(this.nameAttr);
    if (string.IsNullOrEmpty(this.specialSpSection))
      this.Query.Attributes.Add(this.spSectionAttr);
    this.Query.Attributes.Add(this.occurenceKeyAttr);
    this.Query.Attributes.Add(this.positionAttr);
    this.Query.Attributes.Add(this.noteAttr);
    this.Query.Attributes.Add(this.zoneAttr);
    this.Query.Attributes.Add(this.objGuidAttr);
    this.Query.Attributes.Add(this.imbaseRefAttr);
    this.Query.Attributes.Add(this.countAttr);
    this.Query.Attributes.Add(this.massAttr);
    this.Query.Attributes.Add(this.materialAttr);
  }

  protected override SimpleSpecificationRow DoBuild(DataRow row)
  {
    long int64 = Convert.ToInt64(this.Read(row, this.objectIdAttr));
    string str1 = Convert.ToString(this.Read(row, this.designationAttr));
    string str2 = Convert.ToString(this.Read(row, this.okpCodeAttr));
    string str3 = Convert.ToString(this.Read(row, this.nameAttr));
    string str4 = !string.IsNullOrEmpty(this.specialSpSection) ? this.specialSpSection : DBHelper.CreateObjectTypeGID(Convert.ToInt32(this.Read(row, this.spSectionAttr))).Name;
    object obj1 = this.Read(row, this.occurenceKeyAttr);
    Guid guid = Convert.IsDBNull(obj1) ? Guid.Empty : new Guid(Convert.ToString(obj1));
    string str5 = Convert.ToString(this.Read(row, this.positionAttr));
    string str6 = Convert.ToString(this.Read(row, this.noteAttr));
    string str7 = Convert.ToString(this.Read(row, this.zoneAttr));
    object obj2 = this.Read(row, this.imbaseRefAttr);
    string str8 = Convert.ToString(this.Read(row, this.objGuidAttr));
    string str9 = Convert.IsDBNull(obj2) ? string.Empty : $"IG{str8}";
    MeasuredValue count1 = SimpleSpecificationRowBuilder.TryConvertToCount(this.Read(row, this.countAttr));
    MeasuredValue mass1 = SimpleSpecificationRowBuilder.TryConvertToMass(this.Read(row, this.massAttr));
    string str10 = Convert.ToString(this.Read(row, this.materialAttr));
    string designation = str1;
    string okpCode = str2;
    string name = str3;
    string imbaseKey = str9;
    string sectionName = str4;
    Guid occurenceGuid = guid;
    string position = str5;
    string note = str6;
    string zone = str7;
    MeasuredValue count2 = count1;
    MeasuredValue mass2 = mass1;
    string material = str10;
    return new SimpleSpecificationRow(int64, designation, okpCode, name, imbaseKey, sectionName, occurenceGuid, position, note, zone, count2, mass2, material);
  }

  private static MeasuredValue TryConvertToCount(object rawValue)
  {
    if (rawValue != null && !Convert.IsDBNull(rawValue))
    {
      string mValue = Convert.ToString(rawValue);
      if (!string.IsNullOrEmpty(mValue))
      {
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, false);
        if (measuredValue != null)
          return measuredValue;
      }
    }
    return new MeasuredValue(0.0, IDCache.Default.ItemsMeasure.Id);
  }

  private static MeasuredValue TryConvertToMass(object rawValue)
  {
    if (rawValue != null && !Convert.IsDBNull(rawValue))
    {
      string mValue = Convert.ToString(rawValue);
      if (!string.IsNullOrEmpty(mValue))
      {
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue, false);
        if (measuredValue != null)
          return measuredValue;
      }
    }
    return (MeasuredValue) null;
  }
}
