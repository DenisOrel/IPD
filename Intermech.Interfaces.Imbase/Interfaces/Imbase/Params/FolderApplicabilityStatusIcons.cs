// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.FolderApplicabilityStatusIcons
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params;

/// <summary>Иконки папок в зависимости от атрибута Применяемость</summary>
/// 
///             При добавлении новых свойств, которые должны сохраняться
///             применять к ним атрибут Optional, чтоб не поломалась десериализация
[Serializable]
public class FolderApplicabilityStatusIcons
{
  [NonSerialized]
  private Icon _noRestrictionImage;
  [NonSerialized]
  private Icon _denyAddRecordImage;
  [NonSerialized]
  private Icon _denyAddObjectImage;
  [NonSerialized]
  private Icon _denyAllImage;

  public byte[] NoRestrictionImageData { get; private set; }

  public byte[] DenyAddRecordImageData { get; private set; }

  public byte[] DenyAddObjectImageData { get; private set; }

  public byte[] DenyAllImageData { get; private set; }

  public byte[] SavedData
  {
    get => this.GetData();
    set => this.SetData(value);
  }

  [DefaultValue(typeof (Icon), null)]
  public Icon NoRestrictionImage
  {
    get
    {
      if (this._noRestrictionImage != null)
        return this._noRestrictionImage;
      this._noRestrictionImage = this.GetIconFromByteArray(this.NoRestrictionImageData);
      return this._noRestrictionImage;
    }
    set
    {
      this.NoRestrictionImageData = this.GetByteArrayFromIcon(value);
      this._noRestrictionImage?.Dispose();
      this._noRestrictionImage = (Icon) null;
    }
  }

  [DefaultValue(typeof (Icon), null)]
  public Icon DenyAddRecordImage
  {
    get
    {
      if (this._denyAddRecordImage != null)
        return this._denyAddRecordImage;
      this._denyAddRecordImage = this.GetIconFromByteArray(this.DenyAddRecordImageData);
      return this._denyAddRecordImage;
    }
    set
    {
      this.DenyAddRecordImageData = this.GetByteArrayFromIcon(value);
      this._denyAddRecordImage?.Dispose();
      this._denyAddRecordImage = (Icon) null;
    }
  }

  [DefaultValue(typeof (Icon), null)]
  public Icon DenyAddObjectImage
  {
    get
    {
      if (this._denyAddObjectImage != null)
        return this._denyAddObjectImage;
      this._denyAddObjectImage = this.GetIconFromByteArray(this.DenyAddObjectImageData);
      return this._denyAddObjectImage;
    }
    set
    {
      this.DenyAddObjectImageData = this.GetByteArrayFromIcon(value);
      this._denyAddObjectImage?.Dispose();
      this._denyAddObjectImage = (Icon) null;
    }
  }

  [DefaultValue(typeof (Icon), null)]
  public Icon DenyAllImage
  {
    get
    {
      if (this._denyAllImage != null)
        return this._denyAllImage;
      this._denyAllImage = this.GetIconFromByteArray(this.DenyAllImageData);
      return this._denyAllImage;
    }
    set
    {
      this.DenyAllImageData = this.GetByteArrayFromIcon(value);
      this._denyAllImage?.Dispose();
      this._denyAllImage = (Icon) null;
    }
  }

  private byte[] GetData()
  {
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this);
      return serializationStream.ToArray();
    }
  }

  private void SetData(byte[] data)
  {
    try
    {
      if (data == null || data.Length == 0)
        return;
      using (MemoryStream serializationStream = new MemoryStream(data))
      {
        if (!(new BinaryFormatter().Deserialize((Stream) serializationStream) is FolderApplicabilityStatusIcons applicabilityStatusIcons))
          return;
        this.NoRestrictionImageData = applicabilityStatusIcons.NoRestrictionImageData;
        this.DenyAddRecordImageData = applicabilityStatusIcons.DenyAddRecordImageData;
        this.DenyAddObjectImageData = applicabilityStatusIcons.DenyAddObjectImageData;
        this.DenyAllImage = applicabilityStatusIcons.DenyAllImage;
      }
    }
    catch (Exception ex)
    {
    }
  }

  private byte[] GetByteArrayFromIcon(Icon image)
  {
    if (image == null)
      return (byte[]) null;
    using (MemoryStream outputStream = new MemoryStream())
    {
      image.Save((Stream) outputStream);
      return outputStream.GetBuffer();
    }
  }

  private Icon GetIconFromByteArray(byte[] data)
  {
    if (data == null || data.Length == 0)
      return (Icon) null;
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
      try
      {
        return new Icon((Stream) memoryStream);
      }
      catch (Exception ex)
      {
      }
      return (Icon) null;
    }
  }

  public override string ToString() => string.Empty;
}
