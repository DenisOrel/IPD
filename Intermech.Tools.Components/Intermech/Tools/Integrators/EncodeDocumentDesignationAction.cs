// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.EncodeDocumentDesignationAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Text;
using Intermech.Tools.Data;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class EncodeDocumentDesignationAction : TransferValueRecordAction
{
  private readonly int documentType;
  private readonly bool embedSuffix;

  public EncodeDocumentDesignationAction(
    ValueBag source,
    StringKey sourceKey,
    ValueBag target,
    StringKey targetKey,
    int documentType,
    bool embedSuffix)
    : base(source, sourceKey, target, targetKey)
  {
    this.documentType = documentType;
    this.embedSuffix = embedSuffix;
    this.IsOpenMetadataTarget = false;
    this.OptimizeEmptyValues = true;
  }

  public bool IsOpenMetadataTarget { get; set; }

  /// <summary>
  /// Возвращает или задает режим оптимизации записи пустых значений.
  /// Если на принимающей стороне нет одноименного параметра, то пустое значение не записывается,
  /// так как считается, что отсутствующее значение эквивалентно пустому.
  /// </summary>
  public bool OptimizeEmptyValues { get; set; }

  public override void Perform()
  {
    ValueRecord valueRecord = this.Source.Find(this.SourceKey);
    if (valueRecord == null)
      return;
    string designationValue = this.GetDesignationValue(valueRecord);
    if (this.OptimizeEmptyValues && string.IsNullOrEmpty(designationValue) && !this.Target.Exists(this.TargetKey))
      return;
    if (!this.Target.CanUpdate(this.TargetKey, valueRecord.DataType, this.IsOpenMetadataTarget))
      throw new CantUpdateAttributeValueException(valueRecord);
    this.Target.Update(this.TargetKey, (object) designationValue, this.IsOpenMetadataTarget);
    this.Target.CopyFlag(this.TargetKey, valueRecord.Flags, NamedFlags.ThrowSetException);
  }

  private string GetDesignationValue(ValueRecord sourceItem)
  {
    string origDesignation = sourceItem.Read<string>(string.Empty);
    if (!this.embedSuffix && this.documentType != -1)
      origDesignation = DocumentDesignationHelper.RemoveDocCode(origDesignation, this.documentType);
    return TextServices.Trim(origDesignation);
  }
}
