
// Type: Intermech.Client.Scripting.CSharpScriptsNinjectModule
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Ninject.Modules;


namespace Intermech.Client.Scripting
{
    /// <summary>Ninject-модуль с привязками исполнителя сценариев C#.</summary>
    public sealed class CSharpScriptsNinjectModule : NinjectModule
    {
      public override void Load()
      {
        this.Bind<ICSharpScriptContext, ICSharpScriptClientContext>().To<CSharpScriptClientContext>();
        this.Bind<CSharpScriptExecutorOptionsProvider>().ToSelf().WhenInjectedInto<CSharpScriptExecutor>();
        this.Bind<ICSharpScriptExecutor>().To<CSharpScriptExecutor>().InSingletonScope();
      }
    }
}
