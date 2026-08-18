
// Type: Intermech.Search.Diff.DiffCollectionBase`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Collections.Generic;


namespace Intermech.Search.Diff;

public abstract class DiffCollectionBase<T> : IDiffCollection<T>, IEnumerable<T>, IEnumerable where T : IDiff
{
  public abstract IEnumerator<T> GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
