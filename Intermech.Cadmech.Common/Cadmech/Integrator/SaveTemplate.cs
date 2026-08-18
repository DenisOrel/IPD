// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SaveTemplate
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Содержит настройки интегратора, принадлежащих определенной "роли" - чертежи сборок или чертежи деталей.
/// </summary>
/// <remarks>
/// Класс устарел, он не предназначен для использования и оставлен только для совместимости с предыдущими версиями базы IPS.
/// </remarks>
[Serializable]
public sealed class SaveTemplate : ICloneable
{
  private SaveTemplateType templateType;
  private List<GlobalId<int>> docTypes;

  public SaveTemplate(SaveTemplateType templateType)
  {
    this.templateType = templateType;
    this.docTypes = new List<GlobalId<int>>();
  }

  /// <summary>Клонирует текущий объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  public SaveTemplate Clone()
  {
    SaveTemplate saveTemplate = new SaveTemplate(this.templateType);
    saveTemplate.docTypes.Capacity = this.docTypes.Count;
    for (int index = 0; index < this.docTypes.Count; ++index)
      saveTemplate.docTypes.Add(this.docTypes[index]);
    return saveTemplate;
  }

  /// <summary>Клонирует текущий объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Возвращает название роли настроек.</summary>
  public SaveTemplateType TemplateType => this.templateType;

  /// <summary>
  /// Возвращает список типов объектов, которым назначена эта роль.
  /// </summary>
  public List<GlobalId<int>> ObjectTypes => this.docTypes;
}
