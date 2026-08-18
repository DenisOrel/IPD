// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.Descriptor
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DatabaseConfigurator;
using Intermech.Localization;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;

#nullable disable
namespace Intermech.Security.EventLog;

public class Descriptor : HiveDescriptor
{
  public Descriptor()
    : base(DatabaseConfiguratorConsts.EventLogCategoryID, 0, LocalizationHolder.rm.GetString("DatabaseConfigurator_202"))
  {
  }

  protected Descriptor(PersistentState state)
    : this()
  {
  }

  public override void GetObjectData(PersistentState state)
  {
  }
}
