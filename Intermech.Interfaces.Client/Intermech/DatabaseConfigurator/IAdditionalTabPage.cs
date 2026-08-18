// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.IAdditionalTabPage
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.PropertyEditors;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public interface IAdditionalTabPage
{
  int Index { get; }

  IBaseTabPage TabPage { get; }
}
