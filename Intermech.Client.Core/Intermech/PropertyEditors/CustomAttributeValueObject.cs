
// Type: Intermech.PropertyEditors.CustomAttributeValueObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

public class CustomAttributeValueObject : PropDescriptorHolder
{
  protected bool isReadOnly;
  protected PropDescriptor namePropDescriptor;
  protected PropDescriptor notePropDescriptor;
  protected PropDescriptor blobIdPropDescriptor;
  protected PropDescriptor dataSizePropDescriptor;
  protected PropDescriptor packedDataSizePropDescriptor;
  protected PropDescriptor modifiedDatePropDescriptor;
  protected PropDescriptor packMethodPropDescriptor;
  protected PropDescriptor ratioPropDescriptor;
  protected BlobInformation bi;

  public bool IsReadOnly => this.isReadOnly;

  public virtual BlobInformation BlobInformation
  {
    get
    {
      if (this.namePropDescriptor != null)
        this.bi.FileName = this.namePropDescriptor.GetValue((object) this) != null ? this.namePropDescriptor.GetValue((object) this).ToString() : (string) null;
      if (this.notePropDescriptor != null)
        this.bi.Note = this.notePropDescriptor.GetValue((object) this) != null ? this.notePropDescriptor.GetValue((object) this).ToString() : (string) null;
      return this.bi;
    }
    set
    {
      this.bi = value;
      this.namePropDescriptor = (PropDescriptor) null;
      this.notePropDescriptor = (PropDescriptor) null;
      this.DropPropertyDescriptorCollection();
    }
  }

  protected long blobID => this.bi.BlobID;

  protected string realFileSize
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Client.Core_949"), (object) Win32Subst.StrFormatByteSize(this.bi.RealFileSize), (object) this.bi.RealFileSize);
    }
  }

  protected string packetFileSize
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Client.Core_949"), (object) Win32Subst.StrFormatByteSize(this.bi.PackedFileSize), (object) this.bi.PackedFileSize);
    }
  }

  protected DateTime modifyDate => this.bi.ModifyDate;

  protected string arcMethod => ArcMethodsHelper.GetCaption(this.bi.ArcMethod);

  protected string FileName
  {
    get
    {
      return this.namePropDescriptor == null ? this.bi.FileName : this.namePropDescriptor.GetValue((object) this).ToString();
    }
  }

  protected string Note
  {
    get
    {
      return this.notePropDescriptor == null ? this.bi.Note : this.notePropDescriptor.GetValue((object) this).ToString();
    }
  }

  protected string compressRatio
  {
    get
    {
      return this.bi.RealFileSize == 0L ? "0 %" : (Convert.ToDouble(this.bi.PackedFileSize) * 100.0 / (double) this.bi.RealFileSize).ToString("G3") + " %";
    }
  }

  public CustomAttributeValueObject(BlobInformation aBlobInformation, bool lIsReadOnly)
  {
    this.bi = aBlobInformation;
    this.isReadOnly = lIsReadOnly;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    this.namePropDescriptor = new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_950"), (object) this.bi.FileName, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_950"), this.isReadOnly, true, false);
    pdc.Add((PropertyDescriptor) this.namePropDescriptor);
    this.notePropDescriptor = new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_35"), (object) this.bi.Note, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_35"), this.isReadOnly, true, false);
    pdc.Add((PropertyDescriptor) this.notePropDescriptor);
    this.blobIdPropDescriptor = new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_952"), (object) this.blobID.ToString(), typeof (long), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_952"), true, true, false);
    pdc.Add((PropertyDescriptor) this.blobIdPropDescriptor);
    this.dataSizePropDescriptor = new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_953"), (object) this.realFileSize, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_953"), true, true, false);
    pdc.Add((PropertyDescriptor) this.dataSizePropDescriptor);
    this.packedDataSizePropDescriptor = new PropDescriptor(4, (object) this, LocalizationHolder.rm.GetString("Client.Core_954"), (object) this.packetFileSize, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_954"), true, true, false);
    pdc.Add((PropertyDescriptor) this.packedDataSizePropDescriptor);
    this.modifiedDatePropDescriptor = new PropDescriptor(5, (object) this, LocalizationHolder.rm.GetString("Client.Core_955"), (object) this.modifyDate, typeof (DateTime), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_955"), true, true, false);
    pdc.Add((PropertyDescriptor) this.modifiedDatePropDescriptor);
    this.packMethodPropDescriptor = new PropDescriptor(6, (object) this, LocalizationHolder.rm.GetString("Client.Core_956"), (object) this.arcMethod, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_956"), true, true, false);
    pdc.Add((PropertyDescriptor) this.packMethodPropDescriptor);
    this.ratioPropDescriptor = new PropDescriptor(7, (object) this, LocalizationHolder.rm.GetString("Client.Core_957"), (object) this.compressRatio, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_957"), true, true, false);
    pdc.Add((PropertyDescriptor) this.ratioPropDescriptor);
  }
}
