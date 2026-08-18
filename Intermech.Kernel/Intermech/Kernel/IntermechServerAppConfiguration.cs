// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.IntermechServerAppConfiguration
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;


namespace Intermech.Kernel;

internal sealed class IntermechServerAppConfiguration : 
  IntermechServerService,
  IMServerAppConfiguration
{
  private HashSet<string> publicConfigurationOptions;
  private Dictionary<string, string> publicTraceSwitches;
  private ConcurrentDictionary<string, TraceSwitch> traceSwitchTable;

  public IntermechServerAppConfiguration(IntermechServer server)
    : base(server)
  {
    this.publicConfigurationOptions = new HashSet<string>();
    this.publicConfigurationOptions.Add("CSharpScripts.LogAllInvocations");
    this.publicConfigurationOptions.Add("Remoting.ClientSponsorMode");
    this.publicConfigurationOptions.Add("Protection.SpareServers");
    this.publicConfigurationOptions.Add("Protection.InformAdmins");
    this.publicTraceSwitches = new Dictionary<string, string>();
    this.publicTraceSwitches.Add("UserSession.CheckForForgottenTransactions", "1");
    this.publicTraceSwitches.Add("Remoting.ClientSponsors", "0");
    this.traceSwitchTable = new ConcurrentDictionary<string, TraceSwitch>();
  }

  public string GetConfigurationOption(string optionName)
  {
    if (optionName == null)
      throw new ArgumentNullException(nameof (optionName));
    return this.publicConfigurationOptions.Contains(optionName) ? ConfigurationManager.AppSettings[optionName] : (string) null;
  }

  public TraceLevel GetTraceSwitch(string switchName)
  {
    if (switchName == null)
      throw new ArgumentNullException(nameof (switchName));
    string defaultSwitchValue;
    return this.publicTraceSwitches.TryGetValue(switchName, out defaultSwitchValue) ? this.traceSwitchTable.GetOrAdd(switchName, (Func<string, TraceSwitch>) (name => new TraceSwitch(name, string.Empty, defaultSwitchValue))).Level : TraceLevel.Off;
  }

  public Tuple<Dictionary<string, string>, Dictionary<string, TraceLevel>> GetAll()
  {
    Dictionary<string, string> dictionary1 = new Dictionary<string, string>(this.publicConfigurationOptions.Count);
    foreach (string configurationOption in this.publicConfigurationOptions)
      dictionary1.Add(configurationOption, this.GetConfigurationOption(configurationOption));
    Dictionary<string, TraceLevel> dictionary2 = new Dictionary<string, TraceLevel>(this.publicTraceSwitches.Count);
    foreach (KeyValuePair<string, string> publicTraceSwitch in this.publicTraceSwitches)
    {
      string key = publicTraceSwitch.Key;
      dictionary2.Add(key, this.GetTraceSwitch(key));
    }
    return Tuple.Create<Dictionary<string, string>, Dictionary<string, TraceLevel>>(dictionary1, dictionary2);
  }
}
