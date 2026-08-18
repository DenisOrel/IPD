
// Type: Intermech.Client.BinaryClientFormatterSinkPatcher
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;


namespace Intermech.Client
{
    /// <summary>
    /// Реализует обертку над BinaryClientFormatterSink для исправления ошибок в реализации remoting.
    /// </summary>
    internal sealed class BinaryClientFormatterSinkPatcher(IClientFormatterSink nativeSink) : 
      ClientFormatterSinkWrapper(nativeSink)
    {
      public override IMessage SyncProcessMessage(IMessage msg)
      {
        IMessage message = base.SyncProcessMessage(msg);
        if (message is IMethodReturnMessage methodReturnMessage && methodReturnMessage.Exception != null && msg is IMethodCallMessage mcm && mcm.LogicalCallContext.HasInfo)
          message = (IMessage) new ReturnMessage(methodReturnMessage.Exception, mcm);
        return message;
      }
    }
}
