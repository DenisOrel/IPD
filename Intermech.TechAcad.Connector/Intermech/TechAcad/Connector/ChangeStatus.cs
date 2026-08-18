// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.ChangeStatus
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System;

#nullable disable
namespace Intermech.TechAcad.Connector;

[Flags]
public enum ChangeStatus
{
  None = 0,
  Added = 1,
  Modified = 2,
  Deleted = 4,
}
