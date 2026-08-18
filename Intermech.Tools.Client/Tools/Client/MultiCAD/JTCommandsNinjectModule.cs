// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MultiCAD.JTCommandsNinjectModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Ninject.Modules;

#nullable disable
namespace Intermech.Tools.Client.MultiCAD;

internal sealed class JTCommandsNinjectModule : NinjectModule
{
  public override void Load()
  {
    this.Bind<JTSourceDocumentTypesHelper>().ToSelf().InSingletonScope();
    this.Bind<JTCommandsProvider>().ToSelf();
    this.Bind<JTCommandsModule>().ToSelf();
  }
}
