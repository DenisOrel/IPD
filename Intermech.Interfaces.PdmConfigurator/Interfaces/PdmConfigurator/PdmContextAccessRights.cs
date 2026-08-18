// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmContextAccessRights
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Права доступа к контексту конфигуратора составоа</summary>
[Flags]
[Serializable]
public enum PdmContextAccessRights
{
  /// <summary>Только просмотр</summary>
  ReadOnly = 0,
  /// <summary>Полный доступ к контексту</summary>
  FullAccess = 1,
}
