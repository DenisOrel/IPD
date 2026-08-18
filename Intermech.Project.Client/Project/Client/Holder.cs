// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.Holder
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Plugins;
using System;

#nullable disable
namespace Intermech.Project.Client;

[Obsolete("Class will be removed in future releases!")]
internal class Holder : Intermech.Project.Controls.Holder
{
  [Obsolete("Call IMProject.Client.Init(IPackage, IServiceProvider)!")]
  public new static void Init([NotNull] IPackage plugin, [NotNull] IServiceProvider serviceProvider)
  {
    Intermech.Workflow.Design.Holder.Init(plugin, serviceProvider);
    Intermech.Project.Controls.Library.Init(plugin, serviceProvider);
  }
}
