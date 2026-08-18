// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IClientProjectContext
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Project;

/// <summary>Контекст клиентского проекта IMProject</summary>
public interface IClientProjectContext
{
  [CanBeNull]
  ClientProject Project { get; }
}
