
// Type: Intermech.Remoting.RemotingCallContextSavedState
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Remoting
{
    public class RemotingCallContextSavedState
    {
      private static readonly RemotingCallContextSavedState emtpy = new RemotingCallContextSavedState(0);
      private List<KeyValuePair<string, string>> items;

      internal RemotingCallContextSavedState(int capacity)
      {
        this.items = new List<KeyValuePair<string, string>>(capacity);
      }

      internal void Add(string name, string data)
      {
        this.items.Add(new KeyValuePair<string, string>(name, data));
      }

      internal IEnumerable<KeyValuePair<string, string>> Scan()
      {
        return (IEnumerable<KeyValuePair<string, string>>) this.items;
      }

      internal static RemotingCallContextSavedState Empty
      {
        [DebuggerStepThrough] get => RemotingCallContextSavedState.Empty;
      }
    }
}
