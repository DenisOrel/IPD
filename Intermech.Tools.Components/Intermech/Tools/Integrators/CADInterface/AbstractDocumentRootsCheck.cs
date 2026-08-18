// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.AbstractDocumentRootsCheck
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Базовый класс для проверки, что типы документов унасленованы от нужного базового типа документов.
/// </summary>
public abstract class AbstractDocumentRootsCheck : CADSettingsCheck
{
  protected string CheckDocumentGroupIsBasedOnType(
    DocumentGroup documentGroup,
    ObjectTypeResolver baseType)
  {
    if (documentGroup == null)
      throw new ArgumentNullException(nameof (documentGroup));
    if (baseType == null)
      throw new ArgumentNullException(nameof (baseType));
    using (IEnumerator<GlobalId<int>> enumerator = documentGroup.DocumentTypes.Where<GlobalId<int>>((Func<GlobalId<int>, bool>) (objectType => !DBHelper.IsBasedOnType(objectType.Id, baseType.Id))).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        GlobalId<int> current = enumerator.Current;
        return string.Format("Тип документа '{1}', располагаемый в группе '{0}', должен быть унаследован от типа '{2}'.", (object) documentGroup.Caption, (object) current.Name, (object) baseType.Text);
      }
    }
    return (string) null;
  }
}
