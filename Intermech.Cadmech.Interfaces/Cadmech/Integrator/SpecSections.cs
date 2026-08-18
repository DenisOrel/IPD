// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SpecSections
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Реализует таблицу для преобразования разделов спецификации в буквенные коды для CADMECH 2D и обратно.
/// Класс является thread-safe.
/// </summary>
public static class SpecSections
{
  private static readonly Dictionary<char, ObjectTypeResolver> objectTypes;
  private static readonly Dictionary<char, SpecialObjectResolver> sections;
  private static readonly ObjectTypeResolver drawinglessPart;

  /// <summary>
  /// Позволяет определить, является ли допустимым указанный буквенный код раздела в обменном файле CADMECH 2D.
  /// </summary>
  /// <param name="sectionCode">Буквенный код раздела спецификации</param>
  /// <returns>true, если код является допустимым</returns>
  /// <exception cref="T:System.ArgumentException">Указанная буква не является кодом раздела спецификации</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public static bool IsSectionSupported(char sectionCode)
  {
    SpecSections.CheckSectionCode(sectionCode);
    return SpecSections.objectTypes.ContainsKey(sectionCode);
  }

  /// <summary>
  /// Возвращает тип объекта, соответствующий буквенному коду раздела в обменном файле CADMECH 2D.
  /// </summary>
  /// <param name="sectionCode">Буквенный код раздела спецификации</param>
  /// <param name="documentFormat">Формат документа</param>
  /// <returns>Описатель типа объекта</returns>
  /// <exception cref="T:System.ArgumentException">Указанная буква не является кодом раздела спецификации</exception>
  /// <exception cref="T:Intermech.FaultException">Указанный раздел не поддерживается</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public static LocalId<int> GetSectionObjectType(char sectionCode, string documentFormat)
  {
    SpecSections.CheckSectionCode(sectionCode);
    if (documentFormat == null)
      throw new ArgumentNullException(nameof (documentFormat));
    if (sectionCode == 'D' && string.Equals(documentFormat, LocalizationHolder.rm.GetString("Cadmech.DrawinglessPartFormat"), StringComparison.CurrentCultureIgnoreCase))
      return (LocalId<int>) SpecSections.drawinglessPart.GID;
    ObjectTypeResolver objectTypeResolver;
    if (SpecSections.objectTypes.TryGetValue(sectionCode, out objectTypeResolver))
      return (LocalId<int>) objectTypeResolver.GID;
    throw new FaultException($"CADMECH 2D не поддерживает раздел спецификации с кодом '{sectionCode}'. Передача данных в AVS невозможна.");
  }

  private static void CheckSectionCode(char sectionCode)
  {
    if (!char.IsLetter(sectionCode) || !char.IsUpper(sectionCode))
      throw new ArgumentException();
  }

  /// <summary>
  /// Позволяет определить, является ли допустимым указанный раздел в обменном файле CADMECH 2D.
  /// </summary>
  /// <param name="sectionId">Идентификатор раздела спецификации</param>
  /// <returns>true, если раздел является допустимым</returns>
  /// <exception cref="T:System.ArgumentException">Идентификатор раздела спецификации не задан</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public static bool IsSectionSupported(long sectionId)
  {
    if (sectionId == 0L)
      throw new ArgumentException();
    return SpecSections.FindSectionItem(sectionId) != null;
  }

  /// <summary>
  /// Преобразует раздел спецификации в буквенный код для обменного файла CADMECH 2D.
  /// </summary>
  /// <param name="sectionId">Идентификатор раздела</param>
  /// <param name="sectionName">Название раздела</param>
  /// <returns>Буквенный код раздела</returns>
  /// <exception cref="T:System.ArgumentException">Идентификатор или название раздела спецификации не заданы</exception>
  /// <exception cref="T:Intermech.FaultException">Указанный раздел не поддерживается</exception>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public static char ToSectionCode(long sectionId, string sectionName)
  {
    if (sectionId == 0L)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(sectionName))
      throw new ArgumentException();
    return (SpecSections.FindSectionItem(sectionId) ?? throw new FaultException($"CADMECH 2D не поддерживает раздел спецификации '{sectionName}'. Передача данных из AVS в CADMECH 2D невозможна.")).Item1;
  }

  private static Tuple<char, SpecialObjectResolver> FindSectionItem(long sectionId)
  {
    foreach (KeyValuePair<char, SpecialObjectResolver> section in SpecSections.sections)
    {
      if (section.Value.Id == sectionId)
        return Tuple.Create<char, SpecialObjectResolver>(section.Key, section.Value);
    }
    return (Tuple<char, SpecialObjectResolver>) null;
  }

  static SpecSections()
  {
    MetadataResolverFactory factory = MetadataResolvers.Factory;
    SpecSections.objectTypes = new Dictionary<char, ObjectTypeResolver>(8);
    SpecSections.objectTypes.Add('A', factory.ObjectTypeResolver(new Guid("CAD0025E-306C-11D8-B4E9-00304F19F545")));
    SpecSections.objectTypes.Add('B', factory.ObjectTypeResolver(new Guid("CAD00132-306C-11D8-B4E9-00304F19F545")));
    SpecSections.objectTypes.Add('D', factory.ObjectTypeResolver(new Guid("CAD00250-306C-11D8-B4E9-00304F19F545")));
    SpecSections.objectTypes.Add('S', factory.ObjectTypeResolver(new Guid("CAD00252-306C-11D8-B4E9-00304F19F545")));
    SpecSections.objectTypes.Add('P', factory.ObjectTypeResolver(new Guid("CAD0038D-306C-11D8-B4E9-00304F19F545")));
    SpecSections.objectTypes.Add('M', factory.ObjectTypeResolver(new Guid("CAD0081D-306C-11D8-B4E9-00304F19F545")));
    SpecSections.objectTypes.Add('K', factory.ObjectTypeResolver(new Guid("CAD0025F-306C-11D8-B4E9-00304F19F545")));
    SpecSections.sections = new Dictionary<char, SpecialObjectResolver>(8);
    SpecSections.sections.Add('A', factory.SpecialObjectResolver(new Guid("CAD00257-306C-11D8-B4E9-00304F19F545")));
    SpecSections.sections.Add('B', factory.SpecialObjectResolver(new Guid("CAD00258-306C-11D8-B4E9-00304F19F545")));
    SpecSections.sections.Add('D', factory.SpecialObjectResolver(new Guid("CAD00259-306C-11D8-B4E9-00304F19F545")));
    SpecSections.sections.Add('S', factory.SpecialObjectResolver(new Guid("CAD0025A-306C-11D8-B4E9-00304F19F545")));
    SpecSections.sections.Add('P', factory.SpecialObjectResolver(new Guid("CAD0025B-306C-11D8-B4E9-00304F19F545")));
    SpecSections.sections.Add('M', factory.SpecialObjectResolver(new Guid("CAD0025C-306C-11D8-B4E9-00304F19F545")));
    SpecSections.sections.Add('K', factory.SpecialObjectResolver(new Guid("CAD0025D-306C-11D8-B4E9-00304F19F545")));
    SpecSections.drawinglessPart = factory.ObjectTypeResolver(new Guid("CAD00861-306C-11D8-B4E9-00304F19F545"));
  }
}
