// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Kernel.DBAttributeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Tools.LaunchActions;
using System;
using System.Data;
using System.Xml;


namespace Intermech.Tools.Kernel;

internal static class DBAttributeReader
{
  private const string EmptyXml = "<Empty/>";

  public static string GetDisplayName(IDBObject dbObj)
  {
    return DBAttributeReader.GetDisplayName(DBUtils.ReadAttribute<string>((IDBAttributable) dbObj, Consts.NameAttr), (object) dbObj.ObjectID);
  }

  public static string GetDisplayName(DataRow row, int columnIndex, object objectId)
  {
    return DBAttributeReader.GetDisplayName(row.IsNull(columnIndex) ? (string) null : Convert.ToString(row[columnIndex]), objectId);
  }

  private static string GetDisplayName(string rawValue, object objectId)
  {
    return !string.IsNullOrEmpty(rawValue) ? rawValue : string.Format(LocalizationHolder.rm.GetString("Kernel_1122"), objectId);
  }

  public static ITarget GetTarget(IDBObject dbObj, ToolSecurityService toolSecurity)
  {
    return DBAttributeReader.GetTarget(DBUtils.ReadAttribute<string>((IDBAttributable) dbObj, Consts.TargetAttr), toolSecurity, (object) dbObj.ObjectID);
  }

  private static ITarget GetTarget(
    string targetCode,
    ToolSecurityService toolSecurity,
    object objectId)
  {
    try
    {
      return toolSecurity.DecodeTarget(targetCode);
    }
    catch (Exception ex)
    {
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1123"), objectId, (object) ex.Message));
    }
  }

  public static LaunchType GetLaunchType(IDBObject dbObj)
  {
    return DBAttributeReader.GetLaunchType(DBUtils.ReadAttribute<long>((IDBAttributable) dbObj, Consts.LaunchTypeAttr), (object) dbObj.ObjectID);
  }

  private static LaunchType GetLaunchType(long rawValue, object objectId) => (LaunchType) rawValue;

  public static ToolSecurityGroup GetToolSecurityGroup(IDBObject dbObj)
  {
    return DBAttributeReader.GetToolSecurityGroup(DBUtils.ReadAttribute<long>((IDBAttributable) dbObj, Consts.ToolSecurityGroupAttr), (object) dbObj.ObjectID);
  }

  public static ToolSecurityGroup GetToolSecurityGroup(
    DataRow row,
    int columnIndex,
    object objectId)
  {
    return DBAttributeReader.GetToolSecurityGroup(Convert.ToInt64(row[columnIndex]), objectId);
  }

  private static ToolSecurityGroup GetToolSecurityGroup(long rawValue, object objectId)
  {
    return (ToolSecurityGroup) rawValue;
  }

  public static string GetXmlData(IDBObject dbObj)
  {
    return DBAttributeReader.GetXmlData(DBUtils.ReadAttribute<string>((IDBAttributable) dbObj, Consts.XmlDataAttr), (object) dbObj.ObjectID);
  }

  public static string GetXmlData(DataRow row, int columnIndex, object objectId)
  {
    return DBAttributeReader.GetXmlData(row.IsNull(columnIndex) ? (string) null : Convert.ToString(row[columnIndex]), objectId);
  }

  private static string GetXmlData(string rawValue, object objectId)
  {
    return !string.IsNullOrEmpty(rawValue) ? rawValue : "<Empty/>";
  }

  public static XmlDocument TryReadXml(
    IDBObject dbObj,
    DBAttributeReader.CheckDataFormat formatChecker)
  {
    try
    {
      XmlDocument data = new XmlDocument();
      data.LoadXml(DBAttributeReader.GetXmlData(dbObj));
      formatChecker(data);
      return data;
    }
    catch
    {
      return (XmlDocument) null;
    }
  }

  public static XmlDocument TryReadXml(
    DataRow row,
    int columnIndex,
    object objectId,
    DBAttributeReader.CheckDataFormat formatChecker)
  {
    try
    {
      XmlDocument data = new XmlDocument();
      data.LoadXml(DBAttributeReader.GetXmlData(row, columnIndex, objectId));
      formatChecker(data);
      return data;
    }
    catch
    {
      return (XmlDocument) null;
    }
  }

  public delegate void CheckDataFormat(XmlDocument data);
}
