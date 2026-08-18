// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UINotificationActionBuilder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

public class UINotificationActionBuilder
{
  private string name;
  private Uri data;
  private string anchorText;

  public UINotificationActionBuilder(string name)
  {
    this.name = name != null ? name : throw new ArgumentNullException(nameof (name));
    this.anchorText = string.Empty;
  }

  public UINotificationActionBuilder(string name, Uri data)
    : this(name)
  {
    this.data = !(data == (Uri) null) ? data : throw new ArgumentNullException(nameof (data));
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.name;
    [DebuggerStepThrough] set
    {
      this.name = value ?? throw new ArgumentNullException(nameof (value));
    }
  }

  public Uri Data
  {
    [DebuggerStepThrough] get => this.data;
    [DebuggerStepThrough] set => this.data = value;
  }

  public string AnchorText
  {
    [DebuggerStepThrough] get => this.anchorText;
    [DebuggerStepThrough] set
    {
      this.anchorText = value ?? throw new ArgumentNullException(nameof (value));
    }
  }

  public UINotificationAction Build()
  {
    return new UINotificationAction(this.name, this.data, this.anchorText);
  }
}
