// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.IComponentProperty
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Интерфейс описывающий параметр компонента</summary>
public interface IComponentProperty
{
  /// <summary>Значение параметра</summary>
  object Value { get; set; }
}
