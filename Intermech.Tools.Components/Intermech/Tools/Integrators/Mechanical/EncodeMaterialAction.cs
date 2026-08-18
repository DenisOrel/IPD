// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.EncodeMaterialAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class EncodeMaterialAction : TransferValueRecordAction
{
  private StringKey materialIDKey;

  public EncodeMaterialAction(
    ValueBag source,
    StringKey sourceKey,
    ValueBag target,
    StringKey targetKey,
    StringKey materialIDKey = null)
    : base(source, sourceKey, target, targetKey)
  {
    this.materialIDKey = materialIDKey;
  }

  public bool IsOpenMetadataTarget { get; set; }

  public override void Perform()
  {
    ValueRecord valueRecord1 = this.Source.Find(this.SourceKey);
    if (valueRecord1 == null || !(valueRecord1.DataType == typeof (long)))
      return;
    ValueRecord valueRecord2 = this.Target.Find(this.TargetKey);
    if (valueRecord2 != null)
    {
      if (valueRecord2.DataType == typeof (long) || valueRecord2.DataType == typeof (int) || valueRecord2.DataType == typeof (short))
      {
        this.EncodeAsObjectLink(valueRecord1, valueRecord2.DataType);
      }
      else
      {
        if (!(valueRecord2.DataType == typeof (string)))
          throw new CantUpdateAttributeValueException(valueRecord1, (Exception) new InvalidCastException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_225"), (object) valueRecord2.Key, (object) valueRecord2.DataType)));
        this.EncodeAsText(valueRecord1);
      }
    }
    else
      this.EncodeAsText(valueRecord1);
  }

  private void EncodeAsObjectLink(ValueRecord materialItem, Type dataType)
  {
    if (!this.Target.CanUpdate(this.TargetKey, dataType, this.IsOpenMetadataTarget))
      throw new CantUpdateAttributeValueException(materialItem);
    try
    {
      this.Target.Update(this.TargetKey, materialItem.IsNull ? (object) 0 : Convert.ChangeType(materialItem.Value, dataType), this.IsOpenMetadataTarget);
      this.Target.CopyFlag(this.TargetKey, materialItem.Flags, NamedFlags.ThrowSetException);
    }
    catch (InvalidCastException ex)
    {
      throw new CantUpdateAttributeValueException(materialItem, (Exception) ex);
    }
    catch (FormatException ex)
    {
      throw new CantUpdateAttributeValueException(materialItem, (Exception) ex);
    }
  }

  private void EncodeAsText(ValueRecord materialItem)
  {
    if (!this.Target.CanUpdate(this.TargetKey, typeof (string), this.IsOpenMetadataTarget))
      throw new CantUpdateAttributeValueException(materialItem);
    string newValue1 = string.Empty;
    string newValue2 = string.Empty;
    if (!materialItem.IsNull)
    {
      long objectID = materialItem.Read<long>(0L);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, false);
        if (dbObject != null)
        {
          int objectType = dbObject.ObjectType;
          if (PDMHelper.IsArticle(objectType) || TechcardHelper.IsTechBlank(objectType))
          {
            IDBAttribute attributeByName = dbObject.GetAttributeByName(IDCache.Default.Designation.Text);
            if (attributeByName != null && !attributeByName.IsNull)
              newValue1 = attributeByName.AsString;
          }
          if (string.IsNullOrEmpty(newValue1))
          {
            IDBAttribute attributeByName = dbObject.GetAttributeByName(IDCache.Default.Name.Text);
            if (attributeByName != null && !attributeByName.IsNull)
              newValue1 = attributeByName.AsString;
          }
          newValue2 = "IG" + dbObject.GUID.ToString();
        }
      }
    }
    this.Target.Update(this.TargetKey, (object) newValue1, this.IsOpenMetadataTarget);
    this.Target.CopyFlag(this.TargetKey, materialItem.Flags, NamedFlags.ThrowSetException);
    if (!(this.materialIDKey != (StringKey) null))
      return;
    ValueRecord valueRecord = this.Target.Find(this.materialIDKey);
    if (valueRecord == null || valueRecord.IsNull || !(valueRecord.DataType == typeof (string)))
      return;
    this.Target.Update(this.materialIDKey, (object) newValue2);
    this.Target.CopyFlag(this.materialIDKey, materialItem.Flags, NamedFlags.ThrowSetException);
  }
}
