// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.PageDescriptor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Setup;

internal class PageDescriptor
{
  private Type controlType;
  private string pageName;

  public PageDescriptor(string pageName, Type controlType)
  {
    this.controlType = controlType;
    this.pageName = pageName;
  }

  public Type ControlType => this.controlType;

  public string PageName => this.pageName;

  public bool Equals(PageDescriptor descriptor)
  {
    return descriptor != null && descriptor.controlType == this.controlType;
  }

  public override bool Equals(object obj)
  {
    return !(obj is PageDescriptor descriptor) ? base.Equals(obj) : this.Equals(descriptor);
  }

  public override int GetHashCode() => this.controlType.GetHashCode();

  public override string ToString() => this.pageName;
}
