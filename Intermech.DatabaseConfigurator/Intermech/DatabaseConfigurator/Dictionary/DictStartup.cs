// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Dictionary.DictStartup
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DatabaseConfigurator.Dictionary.Forms;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.Dictionary;

internal class DictStartup
{
  public DictStartup(IServiceProvider serviceProvider)
  {
    DictHolder.ServiceProvider = serviceProvider;
    DictSetup dictSetup = new DictSetup(serviceProvider);
  }
}
