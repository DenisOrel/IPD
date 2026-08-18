
// Type: IMClient.ProgramComObjectFactory




using Intermech.Runtime.ComInterop.LocalServer;
using Ninject;
using System;


namespace IMClient
{
    internal sealed class ProgramComObjectFactory : ComObjectFactory
    {
      private StandardKernel iocContainer;

      public ProgramComObjectFactory(StandardKernel iocContainer) => this.iocContainer = iocContainer;

      protected override object DoCreateInstance(ComServer comServer, Type comClass)
      {
        return this.iocContainer.Get(comClass);
      }
    }
}
