
// Type: Intermech.Client.Core.FlagsConditionsHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Client.Core;

public class FlagsConditionsHelper
{
  public static string GetCaption(FlagsConditions cnd) => EnumTypeHelper.GetCaption((Enum) cnd);

  public static RelationalOperators ConvertToRelationalOperators(FlagsConditions cnd)
  {
    RelationalOperators relationalOperators = RelationalOperators.None;
    if (cnd <= FlagsConditions.LESSEQUAL)
    {
      switch (cnd - 1U)
      {
        case FlagsConditions.NONE:
          relationalOperators = RelationalOperators.Equal;
          break;
        case FlagsConditions.EQUAL:
          relationalOperators = RelationalOperators.NotEqual;
          break;
        case FlagsConditions.NOTEQUAL:
          break;
        case FlagsConditions.EQUAL | FlagsConditions.NOTEQUAL:
          relationalOperators = RelationalOperators.Less;
          break;
        default:
          if (cnd == FlagsConditions.LESSEQUAL)
          {
            relationalOperators = RelationalOperators.LessOrEqual;
            break;
          }
          break;
      }
    }
    else if (cnd != FlagsConditions.GREATER)
    {
      if (cnd != FlagsConditions.GREATEREQUAL)
      {
        if (cnd == FlagsConditions.SUBSTR)
          relationalOperators = RelationalOperators.Substring;
      }
      else
        relationalOperators = RelationalOperators.GreaterOrEqual;
    }
    else
      relationalOperators = RelationalOperators.Greater;
    return relationalOperators;
  }
}
