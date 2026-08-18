// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.IComponentsListFilter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Интерфейс на фильтр составов схемы или платы</summary>
public interface IComponentsListFilter
{
  /// <summary>Фильтруется ли компонент</summary>
  /// <param name="parameters">Компонент платы/схемы</param>
  /// <param name="variant">Флаг определяющий, является ли компонент прочим изделием, или это пайка, контактная площадка и прочее</param>
  /// <returns></returns>
  bool InFilter(IElectricalComponent parameters, out CompositionVariants variant);
}
