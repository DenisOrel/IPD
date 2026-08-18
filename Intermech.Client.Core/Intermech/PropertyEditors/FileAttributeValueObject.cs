
// Type: Intermech.PropertyEditors.FileAttributeValueObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for FileAttributeValueObject.</summary>
public class FileAttributeValueObject(
  BlobInformation aBlobInformation,
  long aBoxId,
  bool lIsReadOnly) : CustomAttributeBoxedValueObject(aBlobInformation, aBoxId, lIsReadOnly)
{
  private bool replaceFileComment;
  protected PropDescriptor checksumPropDescriptor;

  [Browsable(false)]
  public PropDescriptor ChecksumPropDescriptor => this.checksumPropDescriptor;

  [Browsable(false)]
  public Guid ChecksumTaskGuid
  {
    set
    {
      this.checksumPropDescriptor.SetValue((object) this, (object) new ChecksumPgPropertyClass(value));
    }
    get
    {
      if (!(this.checksumPropDescriptor.GetValue((object) this) is ChecksumPgPropertyClass checksumPgPropertyClass))
        return Guid.Empty;
      checksumPgPropertyClass.RereadService();
      return checksumPgPropertyClass.ChecksumTaskGuid;
    }
  }

  [Browsable(false)]
  public bool ReplaceFileComment
  {
    get => this.replaceFileComment;
    set => this.replaceFileComment = value;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    base.CreateProperties(pdc);
    if (this.replaceFileComment)
    {
      this.notePropDescriptor.SetName(LocalizationHolder.rm.GetString("FilePropertyEditor_FileNameTemplate"));
      this.notePropDescriptor.SetDescription(LocalizationHolder.rm.GetString("FilePropertyEditor_FileNameTemplateDescription"));
    }
    this.checksumPropDescriptor = new PropDescriptor(pdc.Count, (object) this, LocalizationHolder.rm.GetString("CrcHeader"), (object) null, typeof (ChecksumPgPropertyClass), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("CrcHeader"), true, true, false);
    pdc.Add((PropertyDescriptor) this.checksumPropDescriptor);
  }

  public void ClearChecksum()
  {
    if (this.checksumPropDescriptor == null)
      return;
    this.checksumPropDescriptor.SetValue((object) this, (object) null);
  }
}
