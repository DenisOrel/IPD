// Decompiled with JetBrains decompiler
// Type: Intermech.Security.SecurityNodeDescriptor
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DatabaseConfigurator;
using Intermech.Localization;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;

#nullable disable
namespace Intermech.Security;

public class SecurityNodeDescriptor : HiveDescriptor
{
  public SecurityNodeDescriptor()
    : base(DatabaseConfiguratorConsts.SecurityCategoryID, 0, LocalizationHolder.rm.GetString("DatabaseConfigurator_203"))
  {
  }

  protected SecurityNodeDescriptor(PersistentState state)
    : this()
  {
  }

  public override void GetObjectData(PersistentState state)
  {
  }
}
