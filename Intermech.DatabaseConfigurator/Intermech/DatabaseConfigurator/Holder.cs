// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Holder
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.DatabaseConfigurator;

internal sealed class Holder
{
  public static IGuidMapper GuidMapper;
  public static IFactory Factory;
  public static ICategoryTypeIconService IconService;
  public static INotificationService NotificationService;
  public static INamedImageList NamedImageList;
}
