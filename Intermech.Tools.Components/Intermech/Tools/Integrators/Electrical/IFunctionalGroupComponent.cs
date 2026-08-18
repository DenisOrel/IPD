// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.IFunctionalGroupComponent
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Компонент, входящий в состав функциональной группы</summary>
public interface IFunctionalGroupComponent
{
  /// <summary>
  /// Ссылка на функциональную группу, которой принадлежит компонент
  /// </summary>
  FunctionalGroup FunctionalGroup { get; set; }
}
