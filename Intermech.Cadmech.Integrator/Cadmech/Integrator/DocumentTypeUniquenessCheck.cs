// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DocumentTypeUniquenessCheck
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class DocumentTypeUniquenessCheck : AcadSettingsCheck
{
  protected override string DoPerformCheck(
    AcadIntegratorSettings settings,
    SettingsValidatorContext context)
  {
    Dictionary<GlobalId<int>, string> dictionary = new Dictionary<GlobalId<int>, string>();
    foreach (Tuple<GlobalId<int>, string> enumDocumentType in this.EnumDocumentTypes(settings))
    {
      string str;
      if (dictionary.TryGetValue(enumDocumentType.Item1, out str))
      {
        if (str != enumDocumentType.Item2)
          return $"Тип документов '{enumDocumentType.Item1}' не может быть одновременно добавлен в списки '{str}' и '{enumDocumentType.Item2}'. Каждый тип документа в настройках интегратора может быть включен только в один список документов.";
      }
      else
        dictionary.Add(enumDocumentType.Item1, enumDocumentType.Item2);
    }
    return (string) null;
  }

  private IEnumerable<Tuple<GlobalId<int>, string>> EnumDocumentTypes(
    AcadIntegratorSettings settings)
  {
    if (settings.MechanicalSettings.IsEnabled)
    {
      foreach (DrawingTypeSettings assemblyDrawing in settings.MechanicalSettings.AssemblyDrawings)
        yield return new Tuple<GlobalId<int>, string>(assemblyDrawing.DocumentType, "Сборочные чертежи");
      foreach (DrawingTypeSettings partDrawing in settings.MechanicalSettings.PartDrawings)
        yield return new Tuple<GlobalId<int>, string>(partDrawing.DocumentType, "Чертежи деталей");
    }
    if (settings.ConstructionalSettings.IsEnabled)
    {
      foreach (DrawingTypeSettings drawing in settings.ConstructionalSettings.Drawings)
        yield return new Tuple<GlobalId<int>, string>(drawing.DocumentType, "СПДС-Чертежи");
    }
  }
}
