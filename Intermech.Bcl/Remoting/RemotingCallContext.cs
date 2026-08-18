
// Type: Intermech.Remoting.RemotingCallContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Remoting.Optimized;
using System;
using System.Collections.Generic;


namespace Intermech.Remoting
{
    /// <summary>
    /// Более быстрая и избавленная от ошибок замена для стандартного класса <see cref="T:System.Runtime.Remoting.Messaging.CallContext" />.
    /// Кроме того, <see cref="T:Intermech.Remoting.RemotingCallContext" /> совместим с будущей реализацией собственного remoting на современных сетевых технологиях.
    /// </summary>
    public static class RemotingCallContext
    {
      public static string GetData(string name) => RemotingMessageHeaders.Current[name];

      public static void SetData(string name, string data)
      {
        RemotingMessageHeaders.Current[name] = data;
      }

      public static void FreeNamedDataSlot(string name)
      {
        RemotingMessageHeaders.Current[name] = (string) null;
      }

      public static RemotingCallContextSavedState CreateCopy()
      {
        RemotingMessageHeaders current = RemotingMessageHeaders.Current;
        int frameSize = current.GetFrameSize();
        if (frameSize == 0)
          return RemotingCallContextSavedState.Empty;
        RemotingCallContextSavedState copy = new RemotingCallContextSavedState(frameSize);
        foreach (KeyValuePair<string, string> keyValuePair in current.ScanFrame())
          copy.Add(keyValuePair.Key, keyValuePair.Value);
        return copy;
      }

      public static void Run(RemotingCallContextSavedState state, Action action)
      {
        if (state == null)
          throw new ArgumentNullException(nameof (state));
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        RemotingMessageHeaders current = RemotingMessageHeaders.Current;
        current.PushFrame();
        try
        {
          foreach (KeyValuePair<string, string> keyValuePair in state.Scan())
            current[keyValuePair.Key] = keyValuePair.Value;
          action();
        }
        finally
        {
          current.PopFrame();
        }
      }
    }
}
