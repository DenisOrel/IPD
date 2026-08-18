// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SchemaComponent
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data;
using SCH;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class SchemaComponent : IValueBagContainer
{
  private ISch_Component _component;
  private bool _isAssembly;
  private bool _modified;

  public SchemaComponent(ISch_Component component, bool isAssembly)
  {
    this._component = component != null ? component : throw new ArgumentNullException(nameof (component));
    this._isAssembly = isAssembly;
  }

  public ISch_Component Instance => this._component;

  public bool IsAssembly => this._isAssembly;

  public string InternalId => this._component.GetState_UniqueId();

  public ComponentKinds Kind => (ComponentKinds) this.Instance.GetState_ComponentKind();

  public string InternalName => this._component.GetState_Text();

  public bool Modified
  {
    get => this._modified;
    set => this._modified = value;
  }

  public void Close() => Marshal.FinalReleaseComObject((object) this._component);
}
