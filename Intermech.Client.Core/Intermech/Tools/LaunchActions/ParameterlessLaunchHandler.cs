
// Type: Intermech.Tools.LaunchActions.ParameterlessLaunchHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.Xml;


namespace Intermech.Tools.LaunchActions;

public abstract class ParameterlessLaunchHandler : ILaunchHandler
{
  private readonly Guid id;
  private readonly string applicationName;

  protected ParameterlessLaunchHandler(Guid id, string applicationName)
  {
    this.id = id;
    this.applicationName = applicationName;
  }

  public Guid Id
  {
    [DebuggerStepThrough] get => this.id;
  }

  public string DisplayName
  {
    [DebuggerStepThrough] get => this.applicationName;
  }

  public string GetServerObjectTemplate()
  {
    lock (this)
      return $"<Config><LookupData displayName=\"{this.applicationName}\"/></Config>";
  }

  public DataEditorControl CreateSettingsEditor() => (DataEditorControl) new DumbDataEditor();

  public virtual void BeforeLaunch(LaunchParams launchParams, XmlDocument handlerData)
  {
  }

  public abstract void Launch(LaunchParams launchParams, XmlDocument handlerData);
}
