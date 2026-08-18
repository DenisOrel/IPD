// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.BriefcaseXmlReader`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Globalization;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal abstract class BriefcaseXmlReader<TResult> where TResult : new()
{
  protected ImportEventLog _eventLog;

  protected abstract string nodeName { get; }

  public BriefcaseXmlReader(ImportEventLog eventLog) => this._eventLog = eventLog;

  protected abstract void ReadNode(TResult record, XmlTextReader reader);

  public TResult Read(XmlTextReader reader)
  {
    TResult record = new TResult();
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
      {
        try
        {
          this.ReadNode(record, reader);
        }
        catch (Exception ex)
        {
          this._eventLog.AddToTrace("Ошибка при чтении xml: " + ex.Message);
          this._eventLog.AddToTrace(ex.StackTrace);
        }
      }
      else if (this.CompleteReader(reader))
        return record;
    }
    return record;
  }

  protected bool CompleteReader(XmlTextReader reader)
  {
    return reader.NodeType == XmlNodeType.EndElement && reader.Name == this.nodeName;
  }

  protected bool ReadInt64(XmlTextReader reader, ref long result)
  {
    string s = reader.ReadString();
    return !string.IsNullOrEmpty(s) && long.TryParse(s, out result);
  }

  protected bool ReadInt64(XmlTextReader reader, ref object result)
  {
    string s = reader.ReadString();
    long result1;
    if (string.IsNullOrEmpty(s) || !long.TryParse(s, out result1))
      return false;
    result = (object) result1;
    return true;
  }

  protected bool ReadInt32(XmlTextReader reader, ref int result)
  {
    string s = reader.ReadString();
    return !string.IsNullOrEmpty(s) && int.TryParse(s, out result);
  }

  protected bool ReadInt32(XmlTextReader reader, ref object result)
  {
    string s = reader.ReadString();
    int result1;
    if (string.IsNullOrEmpty(s) || !int.TryParse(s, out result1))
      return false;
    result = (object) result1;
    return true;
  }

  protected bool ReadDateTime(XmlTextReader reader, ref DateTime result)
  {
    string s = reader.ReadString();
    return !string.IsNullOrEmpty(s) && DateTime.TryParse(s, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
  }

  protected bool ReadDateTime(XmlTextReader reader, ref object result)
  {
    string s = reader.ReadString();
    DateTime result1;
    if (string.IsNullOrEmpty(s) || !DateTime.TryParse(s, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result1))
      return false;
    result = (object) result1;
    return true;
  }

  protected bool ReadDouble(XmlTextReader reader, ref object result)
  {
    string s = reader.ReadString();
    double result1;
    if (string.IsNullOrEmpty(s) || !double.TryParse(s, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
      return false;
    result = (object) result1;
    return true;
  }

  protected bool ReadString(XmlTextReader reader, ref string result)
  {
    string str = reader.ReadString();
    if (string.IsNullOrEmpty(str))
      return false;
    result = str;
    return true;
  }

  protected bool ReadString(XmlTextReader reader, ref object result)
  {
    string str = reader.ReadString();
    if (string.IsNullOrEmpty(str))
      return false;
    result = (object) str;
    return true;
  }

  protected bool ReadGuid(XmlTextReader reader, ref object result)
  {
    string str = reader.ReadString();
    if (string.IsNullOrEmpty(str) || !GuidHelper.IsGuid(str))
      return false;
    result = (object) new Guid(str);
    return true;
  }
}
