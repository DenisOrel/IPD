// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ObjectColumnsScheme
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

public class ObjectColumnsScheme : AVSColumnScheme
{
  public ObjectColumnsScheme() => this._schemeGuid = Guid.NewGuid();

  public override string Name => "Атрибуты объектов";

  public void AddObjectTypes(IList<int> objectTypeIDs)
  {
    this._possibleAttributesIDs.Clear();
    foreach (int objectTypeId in (IEnumerable<int>) objectTypeIDs)
      this.LoadPossibleAttributes(new List<IMSAttribute4>((IEnumerable<IMSAttribute4>) MetaDataHelper.GetAttribute4ObjectTypeList(objectTypeId)));
    this._possibleAttributesIDs.Sort(this as IComparer<object>);
  }
}
