// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AttributeSource
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary>Атрибут и его источник</summary>
internal class AttributeSource
{
  /// <summary>Источник атрибута</summary>
  internal AttributableElements Source = AttributableElements.Object;
  /// <summary>Идентификатор атрибута</summary>
  internal int ID = -10000;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="source">Источник атрибута</param>
  /// <param name="id">Идентификатор атрибута</param>
  public AttributeSource(AttributableElements source, int id)
  {
    this.Source = source;
    this.ID = id;
  }
}
