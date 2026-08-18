
// Type: Intermech.PropertyEditors.BlobAttributeValueObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for BlobAttributeValueObject.</summary>
public class BlobAttributeValueObject(
  BlobInformation aBlobInformation,
  long aBoxId,
  bool lIsReadOnly) : CustomAttributeBoxedValueObject(aBlobInformation, aBoxId, lIsReadOnly)
{
  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    base.CreateProperties(pdc);
    this.boxPropDescriptor.SetBrowsable(false);
    this.namePropDescriptor.SetReadOnly(true);
    this.namePropDescriptor.SetBrowsable(false);
  }
}
