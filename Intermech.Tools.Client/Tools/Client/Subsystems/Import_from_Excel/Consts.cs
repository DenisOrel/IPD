// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.Consts
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

internal class Consts
{
  public static readonly string ConfigName = "ExcelObjectLoader";
  public static readonly string ConfigurationData = nameof (ConfigurationData);
  public static readonly string SearchDoublePattern = "-?(\\d+((\\.\\d+)|(\\,\\d+))?).*";
  public static readonly string ColumnPropName = "Settings";
  public static readonly string TableName = "Excel Data";
  public static readonly string CommandName = "ImportFromExcel.ImportObjects";
  public static readonly string ImportSettings = nameof (ImportSettings);
}
