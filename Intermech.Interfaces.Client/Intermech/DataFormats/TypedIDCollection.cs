// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.TypedIDCollection
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System.Collections;

#nullable disable
namespace Intermech.DataFormats;

public class TypedIDCollection : ITypedIDCollection, IEnumerator
{
  protected ArrayList idCoollection = new ArrayList();
  private IEnumerator _baseEnumerator;

  public TypedIDCollection(ArrayList idList)
  {
    this.idCoollection.AddRange((ICollection) idList);
    this._baseEnumerator = idList.GetEnumerator();
  }

  public object this[int index] => this.idCoollection[index];

  public int Count => this.idCoollection.Count;

  public void Reset() => this._baseEnumerator.Reset();

  public object Current => this._baseEnumerator.Current;

  public bool MoveNext() => this._baseEnumerator.MoveNext();

  public override string ToString()
  {
    if (this.idCoollection.Count <= 0)
      return LocalizationHolder.rm.GetString("Interfaces.Client_58");
    string str = this.idCoollection[0].ToString();
    if (this.idCoollection.Count > 1)
      str += " ...";
    return str;
  }
}
