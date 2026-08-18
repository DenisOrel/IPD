
// Type: Intermech.Remoting.Optimized.RemotingMessageHeaders
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Intermech.Remoting.Optimized
{
    internal sealed class RemotingMessageHeaders
    {
      [ThreadStatic]
      private static RemotingMessageHeaders currentThreadHeaders;
      private Stack<InternalFrame> frameStack;
      private InternalFrame topLevelFrame;

      private RemotingMessageHeaders()
      {
        this.frameStack = new Stack<InternalFrame>();
        this.topLevelFrame = (InternalFrame) null;
        this.PushFrame();
      }

      [Conditional("DEBUG")]
      private void DebugCheckClassInvariant()
      {
      }

      public string this[string name]
      {
        [DebuggerStepThrough] get
        {
          if (name == null)
            throw new ArgumentNullException(nameof (name));
          string str;
          return this.topLevelFrame.ItemsCreated && this.topLevelFrame.Items.TryGetValue(name, out str) ? str : (string) null;
        }
        [DebuggerStepThrough] set
        {
          if (name == null)
            throw new ArgumentNullException(nameof (name));
          if (value != null)
          {
            this.topLevelFrame.Items[name] = value;
          }
          else
          {
            if (!this.topLevelFrame.ItemsCreated)
              return;
            this.topLevelFrame.Items.Remove(name);
          }
        }
      }

      internal void PushFrame()
      {
            InternalFrame internalFrame = new InternalFrame();
        this.frameStack.Push(internalFrame);
        this.topLevelFrame = internalFrame;
      }

      internal void PopFrame()
      {
        if (this.frameStack.Count <= 1)
          throw new InvalidOperationException();
        this.frameStack.Pop();
        this.topLevelFrame = this.frameStack.Peek();
      }

      internal int GetFrameSize()
      {
        return !this.topLevelFrame.ItemsCreated ? 0 : this.topLevelFrame.Items.Count;
      }

      internal IEnumerable<KeyValuePair<string, string>> ScanFrame()
      {
        return !this.topLevelFrame.ItemsCreated ? Enumerable.Empty<KeyValuePair<string, string>>() : (IEnumerable<KeyValuePair<string, string>>) this.topLevelFrame.Items;
      }

      public static RemotingMessageHeaders Current
      {
        [DebuggerStepThrough] get
        {
          if (RemotingMessageHeaders.currentThreadHeaders == null)
            RemotingMessageHeaders.currentThreadHeaders = new RemotingMessageHeaders();
          return RemotingMessageHeaders.currentThreadHeaders;
        }
      }

      private sealed class InternalFrame
      {
        private bool itemsCreated;
        private Dictionary<string, string> items;

        public bool ItemsCreated => this.itemsCreated;

        public Dictionary<string, string> Items
        {
          get
          {
            if (this.items == null)
            {
              this.items = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
              this.itemsCreated = true;
            }
            return this.items;
          }
        }
      }
    }
}
