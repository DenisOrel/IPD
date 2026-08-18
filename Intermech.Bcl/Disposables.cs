
// Type: Intermech.Disposables
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;


namespace Intermech
{
    public class Disposables : IDisposable
    {
      private readonly ICollection<IDisposable> objects;
      private bool isDisposed;

      public Disposables() => this.objects = (ICollection<IDisposable>) new LinkedList<IDisposable>();

      public void Dispose()
      {
        if (this.isDisposed)
          return;
        foreach (IDisposable disposable in (IEnumerable<IDisposable>) this.objects)
          DisposeUtils.SafelyDispose(disposable);
        this.objects.Clear();
        this.isDisposed = true;
      }

      public void Add(IDisposable obj)
      {
        if (obj == null)
          throw new ArgumentNullException(nameof (obj));
        this.objects.Add(obj);
      }
    }
}
