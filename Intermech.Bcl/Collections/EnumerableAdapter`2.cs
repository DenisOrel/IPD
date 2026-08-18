
// Type: Intermech.Collections.EnumerableAdapter`2
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Collections
{
    public class EnumerableAdapter<TInput, TOutput> : IEnumerable<TOutput>, IEnumerable where TOutput : TInput
    {
      private readonly IEnumerable<TInput> collection;

      public EnumerableAdapter(IEnumerable<TInput> collection)
      {
        this.collection = collection != null ? collection : throw new ArgumentNullException(nameof (collection));
      }

      public IEnumerator<TOutput> GetEnumerator()
      {
        return (IEnumerator<TOutput>) new EnumeratorAdapter(this.collection.GetEnumerator());
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      private struct EnumeratorAdapter(IEnumerator<TInput> enumerator) : 
        IEnumerator<TOutput>,
        IDisposable,
        IEnumerator
      {
        private readonly IEnumerator<TInput> enumerator = enumerator;

        public void Dispose() => this.enumerator.Dispose();

        public void Reset() => this.enumerator.Reset();

        public bool MoveNext() => this.enumerator.MoveNext();

        public TOutput Current => (TOutput) (object) this.enumerator.Current;

        object IEnumerator.Current => (object) this.enumerator.Current;
      }
    }
}
