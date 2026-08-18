// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.StartupHolder
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces.Server;
using System;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class StartupHolder
{
  public static IServiceProvider ServiceProvider;
  public static IEventLogHelper EventLogHelper;
  public static Guid DataEditFormsType = new Guid("cad0011b-306c-11d8-b4e9-00304f19f545");
  public static Guid AttrEditFormsType = new Guid("cad0011c-306c-11d8-b4e9-00304f19f545");
  public static Guid GlobalObjGuidType = new Guid("cad00149-306c-11d8-b4e9-00304f19f545");
  public static Guid GlobalRelGuidType = new Guid("cad0014a-306c-11d8-b4e9-00304f19f545");
  public static Guid FormulaGuidType = new Guid("cad00064-306c-11d8-b4e9-00304f19f545");
}
