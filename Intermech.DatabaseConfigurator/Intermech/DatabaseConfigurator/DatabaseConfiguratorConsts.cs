// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DatabaseConfiguratorConsts
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;

#nullable disable
namespace Intermech.DatabaseConfigurator;

internal sealed class DatabaseConfiguratorConsts
{
  public static readonly Guid CategorySecurityGuid = new Guid("{cad00124-306c-11d8-b4e9-00304f19f545}");
  public static readonly Guid CategoryEventLogGuid = new Guid("{A0BEED42-1388-4b24-89A5-19F9C537494D}");
  public static readonly Guid CategoryEventFilterGuid = new Guid("{291A5251-7569-47d8-AFBE-BA9EB21788D2}");
  public static int SecurityCategoryID = -1;
  public static int EventLogCategoryID = -1;
  public static int EventFilterCategoryID = -1;
  public static int ObjectTypesCategoryID = -1;
}
