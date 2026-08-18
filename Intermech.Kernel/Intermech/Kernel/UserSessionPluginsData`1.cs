// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionPluginsData`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Diagnostics;


namespace Intermech.Kernel;

internal struct UserSessionPluginsData<T>(IUserSession session, string valueName)
{
  private IUserSession _session = session;
  private string _valueName = valueName;
  private UserSessionPluginsData<T>.ValueContainer _valueContainer = (UserSessionPluginsData<T>.ValueContainer) null;
  private bool _valueRetrieved = false;

  public T Value
  {
    [DebuggerStepThrough] get
    {
      this.GetOrCreateValueContainer();
      return this._valueContainer.Value;
    }
    [DebuggerStepThrough] set
    {
      this.GetOrCreateValueContainer();
      this._valueContainer.Value = value;
    }
  }

  private void GetOrCreateValueContainer()
  {
    if (this._valueRetrieved)
      return;
    this._valueContainer = (UserSessionPluginsData<T>.ValueContainer) this._session.GetSessionPluginsData((object) this._valueName);
    if (this._valueContainer == null)
    {
      this._valueContainer = new UserSessionPluginsData<T>.ValueContainer(default (T));
      this._session.SetSessionPluginsData((object) this._valueName, (object) this._valueContainer);
    }
    this._valueRetrieved = true;
  }

  private sealed class ValueContainer : IUserSessionLocalData
  {
    public ValueContainer(T value) => this.Value = value;

    public T Value { get; set; }
  }
}
