
// Type: Intermech.Navigator.Conditions.OwnerController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class OwnerController : ConditionController<OwnerConditionForm>
{
  public override string VisibleName => "Поиск по владельцу объекта";

  public override bool IsHandleConditionStructure(ConditionStructure conditionStructure)
  {
    if (conditionStructure.Attribute == null || !Array.Exists<RelationalOperators>(this.enabledOperators, (Predicate<RelationalOperators>) (x => x.Equals((object) conditionStructure.RelationalOperator))))
      return false;
    if (conditionStructure.Attribute is ObligatoryObjectAttributes || conditionStructure.Attribute is int)
      return (int) conditionStructure.Attribute == -8;
    return conditionStructure.Attribute is Guid && (Guid) conditionStructure.Attribute == new Guid("cad0002f-306c-11d8-b4e9-00304f19f545");
  }

  private RelationalOperators[] enabledOperators
  {
    get
    {
      return new RelationalOperators[2]
      {
        RelationalOperators.Equal,
        RelationalOperators.NotEqual
      };
    }
  }

  public override bool HandleConditionCaption(
    ConditionStructure conditionStructure,
    out string condition,
    out string value)
  {
    long fromConditionValue = OwnerController.GetObjectIDFromConditionValue(conditionStructure);
    if (fromConditionValue == 0L)
      return base.HandleConditionCaption(conditionStructure, out condition, out value);
    condition = $"\"{EnumDescConverter.GetEnumDescription((Enum) ObligatoryObjectAttributes.F_OWNER_ID)}\" ";
    if (conditionStructure.RelationalOperator == RelationalOperators.NotEqual)
      condition += "не ";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(fromConditionValue);
      if (!objectInfo.Empty)
      {
        if (objectInfo.ObjectTypeID == sessionKeeper.Session.IdentHelper.RanksTypeID)
        {
          condition = $"{condition}имеет должность \"{objectInfo.Caption}\"";
          value = objectInfo.Caption;
        }
        else if (objectInfo.ObjectTypeID == sessionKeeper.Session.IdentHelper.UsersTypeID)
        {
          condition = $"{condition}подчиненный руководителя {objectInfo.Caption}";
          value = objectInfo.Caption;
        }
        else if (objectInfo.ObjectTypeID == sessionKeeper.Session.IdentHelper.GroupsTypeID)
        {
          condition = $"{condition}входит в группу \"{objectInfo.Caption}\"";
          value = objectInfo.Caption;
        }
        else
          value = string.Empty;
      }
      else
        value = $"Неизвестный объект ObjectID={fromConditionValue}";
    }
    return true;
  }

  public static long GetObjectIDFromConditionValue(ConditionStructure conditionStructure)
  {
    long fromConditionValue = 0;
    if (conditionStructure.Value is ConditionGroupIDReplacer conditionGroupIdReplacer && conditionGroupIdReplacer.GroupID != 0L)
      fromConditionValue = conditionGroupIdReplacer.GroupID;
    if (conditionStructure.Value is ConditionRankIDReplacer conditionRankIdReplacer && conditionRankIdReplacer.RankID != 0L)
      fromConditionValue = conditionRankIdReplacer.RankID;
    return fromConditionValue;
  }
}
