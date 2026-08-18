
// Type: Intermech.PropertyEditors.ColumnSchemeAttProxy
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class ColumnSchemeAttProxy
{
  private string _value;
  private string _typeName;
  private const char _separator = '|';

  public Guid AttributeGuid
  {
    get
    {
      string[] strArray = this._value.Split('|');
      return strArray.Length != 3 ? Guid.Empty : new Guid(strArray[0]);
    }
  }

  public int ColumnWidth
  {
    get
    {
      string[] strArray = this._value.Split('|');
      return strArray.Length != 3 ? 0 : Convert.ToInt32(strArray[2]);
    }
  }

  public AttributeSourceTypes AttributeSource
  {
    get
    {
      string[] strArray = this._value.Split('|');
      return strArray.Length != 3 ? AttributeSourceTypes.Auto : (AttributeSourceTypes) Convert.ToInt32(strArray[1]);
    }
  }

  public ColumnSchemeAttProxy(string value)
  {
    this._value = value;
    this._typeName = string.Empty;
  }

  public ColumnSchemeAttProxy(
    Guid attributeGuid,
    AttributeSourceTypes attributeSource,
    int columnWidth)
  {
    this._value = $"{attributeGuid}|{Convert.ToInt32((object) attributeSource)}|{columnWidth}";
    this._typeName = string.Empty;
  }

  public override string ToString()
  {
    if (this._value == string.Empty)
      return LocalizationHolder.rm.GetString("Client.Core_929");
    if (this._typeName.Length == 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string[] strArray = this._value.Split('|');
        if (strArray.Length != 3)
          return LocalizationHolder.rm.GetString("Client.Core_929");
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid(strArray[0]));
        string str;
        if (attributeType == null)
          str = "Атрибут в базе данных не найден!";
        else
          str = $"{attributeType.Name} ({EnumDescConverter.GetEnumDescription((Enum) this.AttributeSource)} : {strArray[2]}px.)";
        this._typeName = str;
      }
    }
    return this._typeName;
  }

  public override bool Equals(object obj)
  {
    return obj is ColumnSchemeAttProxy columnSchemeAttProxy && columnSchemeAttProxy.AttributeGuid == this.AttributeGuid && columnSchemeAttProxy.AttributeSource == this.AttributeSource;
  }

  public override int GetHashCode()
  {
    return this.AttributeGuid.GetHashCode() ^ this.AttributeSource.GetHashCode();
  }

  public string Value => this._value;
}
