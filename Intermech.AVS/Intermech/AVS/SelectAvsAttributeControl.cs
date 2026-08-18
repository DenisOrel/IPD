// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SelectAvsAttributeControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.UI.Winforms;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>Выбор атрибутов - производный контрол для AVS</summary>
public class SelectAvsAttributeControl : SelectAttributeControl
{
  /// <summary>
  /// Создать экземпляр формы (конструктор предназначенный для классов-потомков, чтобы у них дизайнер форм работал)
  /// </summary>
  public SelectAvsAttributeControl()
  {
    if (this.IsDesignerHosted())
      return;
    this.RelTypes = AVSSpecification.GetDefaultRelationTypesUsedInSpecification();
    this.ObjTypes = this.GetObjectTypesUsedInAvsDocument((IList<int>) this.RelTypes);
  }

  private List<int> GetObjectTypesUsedInAvsDocument(IList<int> relationTypeIDs)
  {
    List<int> usedInAvsDocument = new List<int>();
    foreach (int relationTypeId in (IEnumerable<int>) relationTypeIDs)
      usedInAvsDocument.AddRange((IEnumerable<int>) MetaDataHelper.GetApplicabilityChildObjectTypesID(AvsIDCache.ObjType_AssemblyUnit, relationTypeId));
    return usedInAvsDocument;
  }
}
