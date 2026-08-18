// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.TechCardCreateVersionService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion;

/// <summary>Сервис создания версий для технологических объектов</summary>
internal class TechCardCreateVersionService : ITechCardCreateVersionService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="param"></param>
  /// <returns></returns>
  public bool Execute(
    [NotNull] IUserSession session,
    [NotNull] TechCardCreateVersionParams param,
    out IEnumerable<RelObjInfoItem> createdRelInfoItems)
  {
    return new TechCardCreateVersionSession(session, param)
    {
      RelationTypes = TechCardConsts.RelTypes.TechAllRelationTypes.Append<int>(TechCardConsts.RelTypes.SortedRelationID)
    }.Execute(out createdRelInfoItems);
  }
}
