// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechObjectType.TechObjectTypeSettings
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechCard.TechObjectType;

/// <summary>Настройки технологических документов</summary>
[Serializable]
public class TechObjectTypeSettings : IEquatable<TechObjectTypeSettings>
{
  /// Настройка "Наследовать права доступа архива техпроцесса" для объектов техпроцесса
  public bool InheritArchiveRights { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals(TechObjectTypeSettings other)
  {
    return other != null && this.InheritArchiveRights == other.InheritArchiveRights;
  }
}
