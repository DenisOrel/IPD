
// Type: Intermech.PropertyEditors.CustomAttributeBoxedValueObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

public class CustomAttributeBoxedValueObject : CustomAttributeValueObject
{
  protected PropDescriptor boxPropDescriptor;
  protected PropDescriptor authorPropDescriptor;
  protected PropDescriptor filetypePropDescriptor;
  private bool authorCaptionAssigned;
  private string authorCaption = string.Empty;
  protected long boxId = -1;

  public PropDescriptor FiletypePropDescriptor => this.filetypePropDescriptor;

  public override BlobInformation BlobInformation
  {
    get
    {
      this.bi = base.BlobInformation;
      if (this.filetypePropDescriptor != null)
        this.bi.FileType = this.filetypePropDescriptor.GetValue((object) this) != null ? ((FileTypePropertyClass) this.filetypePropDescriptor.GetValue((object) this)).FileType : FileTypes.ftNormal;
      return this.bi;
    }
    set
    {
      this.filetypePropDescriptor = (PropDescriptor) null;
      base.BlobInformation = value;
    }
  }

  private string AuthorCaption
  {
    get
    {
      if (!this.authorCaptionAssigned)
      {
        this.authorCaption = (CacheManager.Cache("UserNamesCache") as IUserNamesCache).GetUserName(this.BlobInformation.Author);
        this.authorCaptionAssigned = true;
      }
      return this.authorCaption;
    }
  }

  public long BoxID => this.boxId;

  protected string boxCaption => FileAttributeBoxCache.GetBoxCaption(this.boxId);

  public CustomAttributeBoxedValueObject(
    BlobInformation aBlobInformation,
    long aBoxId,
    bool lIsReadOnly)
    : base(aBlobInformation, lIsReadOnly)
  {
    this.boxId = aBoxId;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    base.CreateProperties(pdc);
    this.boxPropDescriptor = new PropDescriptor(pdc.Count, (object) this, LocalizationHolder.rm.GetString("Client.Core_951"), (object) this.boxCaption, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_951"), true, true, false);
    pdc.Add((PropertyDescriptor) this.boxPropDescriptor);
    this.authorPropDescriptor = new PropDescriptor(pdc.Count, (object) this, LocalizationHolder.rm.GetString("Client.Core_Author"), (object) this.AuthorCaption, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_Author"), true, true, false);
    pdc.Add((PropertyDescriptor) this.authorPropDescriptor);
    this.filetypePropDescriptor = new PropDescriptor(pdc.Count, (object) this, EnumTypeHelper.GetDescription(typeof (FileTypes)), (object) new FileTypePropertyClass(this.bi.FileType), typeof (FileTypePropertyClass), (TypeConverter) new FileTypesConverter(), (object) null, string.Empty, EnumTypeHelper.GetDescription(typeof (FileTypes)), this.bi.FileType == FileTypes.ftNormal && this.isReadOnly, true, false);
    pdc.Add((PropertyDescriptor) this.filetypePropDescriptor);
  }
}
