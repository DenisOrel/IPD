// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElectricalSchemaElementListTypesFilter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Фильтрация схем в диалоге выбора</summary>
public sealed class ElectricalSchemaElementListTypesFilter : ISelectorFilter
{
  private readonly List<int> _enableTypes;

  public ElectricalSchemaElementListTypesFilter()
  {
    List<Guid> forAvsDocumentType = AvsIDCache.GetObjectTypeGuidsForAVSDocumentType(AvsIDCache.AVSDocTypeGuid_ElementList);
    forAvsDocumentType.Add(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
    forAvsDocumentType.Add(new Guid("cad0057f-306c-11d8-b4e9-00304f19f545"));
    foreach (Guid guid in new List<Guid>((IEnumerable<Guid>) forAvsDocumentType))
    {
      Guid childTypeGuid = guid;
      while (true)
      {
        childTypeGuid = MetaDataHelper.GetObjectTypeParentID(childTypeGuid);
        if (!forAvsDocumentType.Contains(childTypeGuid) && childTypeGuid != Guid.Empty)
          forAvsDocumentType.Add(childTypeGuid);
        else
          goto label_5;
      }
label_5:;
    }
    this._enableTypes = forAvsDocumentType.Select<Guid, int>(new Func<Guid, int>(MetaDataHelper.GetObjectTypeID)).ToList<int>();
  }

  public bool IsInFilter(int category, object id)
  {
    if (category != 4 || !(id is int num) || !this._enableTypes.Contains(num))
      return false;
    return AvsIDCache.IsElementList(num) || AVSDocumentsSettings.Instance.IsAVSElementListParentType(num);
  }
}
