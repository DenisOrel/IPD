// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.WebPortal.ValueConverters.StringValueConverter
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Server.WebPortal.ValueConverters;

internal sealed class StringValueConverter : ValueConverter
{
  private FieldTypes fieldType;
  private string iPath;

  public StringValueConverter(
    IDBAttributeType attrType,
    AttributeValue record,
    FieldTypes fieldType,
    string iPath)
    : this(attrType, record, (IEventLogHelper) null, fieldType, iPath)
  {
  }

  public StringValueConverter(
    IDBAttributeType attrType,
    AttributeValue record,
    IEventLogHelper log,
    FieldTypes fieldType,
    string iPath)
    : base(attrType, record, log)
  {
    this.fieldType = fieldType;
    this.iPath = iPath;
  }

  public override object GetValue(IUserSession session, bool throwException)
  {
    if (this.fieldType == FieldTypes.ftMemo)
    {
      FileStream fileStream = this.GetFileStream(this.record.FileName);
      if (fileStream == null)
        return this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_20"), (object) this.record.FileName, (object) this.attrType.Name));
      try
      {
        string end;
        using (StreamReader streamReader = new StreamReader((Stream) fileStream, Encoding.UTF8))
          end = streamReader.ReadToEnd();
        return (long) end.Length > this.attrType.SizeType ? this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_21"), (object) this.attrType.Name)) : (object) end;
      }
      finally
      {
        fileStream.Flush();
        fileStream.Close();
      }
    }
    else
    {
      if (this.fieldType == FieldTypes.ftString)
        return (object) this.record.StringValue;
      if (this.record.StringValue != string.Empty)
        return (object) this.record.StringValue;
      if (this.record.IntegerValue != long.MinValue)
        return (object) Convert.ToString(this.record.IntegerValue);
      if (this.record.DoubleValue != double.MinValue)
        return (object) Convert.ToString(this.record.DoubleValue, (IFormatProvider) CultureInfo.InvariantCulture);
      return this.record.DateTimeValue != string.Empty ? (object) this.record.DateTimeValue : this.OnError(throwException, string.Format(LocalizationHolder.rm.GetString("Interfaces.Server_8"), (object) this.attrType.Name));
    }
  }

  private FileStream GetFileStream(string fileName)
  {
    FileInfo fileInfo = new FileInfo(Path.Combine(this.iPath, fileName));
    return !fileInfo.Exists ? (FileStream) null : new FileStream(fileInfo.FullName, FileMode.Open);
  }
}
