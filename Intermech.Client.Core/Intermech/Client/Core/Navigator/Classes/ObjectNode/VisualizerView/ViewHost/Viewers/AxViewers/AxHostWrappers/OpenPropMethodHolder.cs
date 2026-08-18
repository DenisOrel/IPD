
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.OpenPropMethodHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.Reflection;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

/// <summary>Хранитель clsid - свойство/метод для октрытия файла</summary>
internal class OpenPropMethodHolder
{
  private static readonly Lazy<OpenPropMethodHolder> lazy = new Lazy<OpenPropMethodHolder>((Func<OpenPropMethodHolder>) (() => new OpenPropMethodHolder()));

  public Dictionary<string, MemberInfo> Items { get; }

  private OpenPropMethodHolder() => this.Items = new Dictionary<string, MemberInfo>();

  public static OpenPropMethodHolder GetInstance() => OpenPropMethodHolder.lazy.Value;
}
