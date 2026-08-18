// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SaveTemplateType
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>Тип шаблона сохранения.</summary>
/// <remarks>
/// Класс устарел, он не предназначен для использования и оставлен только для совместимости с предыдущими версиями базы IPS.
/// </remarks>
[Serializable]
public enum SaveTemplateType
{
  [Description("Чертежи сборочных единиц")] Assembly,
  [Description("Чертежи деталей")] Part,
}
