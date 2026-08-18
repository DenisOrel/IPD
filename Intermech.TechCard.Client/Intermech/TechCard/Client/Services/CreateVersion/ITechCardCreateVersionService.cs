// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.ITechCardCreateVersionService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion;

/// <summary>Сервис создания версий для технологических объектов</summary>
public interface ITechCardCreateVersionService
{
  bool Execute(
    IUserSession session,
    TechCardCreateVersionParams param,
    out IEnumerable<RelObjInfoItem> createdRelInfoItems);
}
