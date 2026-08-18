
// Type: Intermech.PropertyEditors.RegVersionRulesCreatorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;


namespace Intermech.PropertyEditors;

/// <summary>
/// RegVersionRulesCreatorForm: класс, реализующий интерфейс IObjectCreatorCustomService
/// </summary>
public class RegVersionRulesCreatorForm : IObjectCreatorCustomService
{
  /// <summary>
  /// Вызов диалога создания нового объекта (по прототипу) c созданием заданных связей с указанными объектами
  /// </summary>
  /// <param name="ObjectTypeID">Идентификатор типа создаваемого объекта</param>
  /// <param name="TemplateObjectID">Идентификатор объекта-прототипа</param>
  /// <param name="RelationTypeIDs">массив идентификаторов связей которые необходимо создавать</param>
  /// <param name="RelatedObjectIDs">массив идентификаторов объектов с которыми надо связать созданный объект</param>
  /// <param name="StartDate">время с которого начинают действовать связи (если они были созданы)</param>
  /// <param name="isVersion">?</param>
  /// <returns>Идентификатор созданного объекта</returns>
  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return VersionRulesCreatorForm.Execute(ObjectTypeID, TemplateObjectID);
  }

  /// <summary>Зарегистрировать класс</summary>
  /// <param name="service"></param>
  public static void Attach(IObjectCreatorService service)
  {
    service.RegisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad001b4-306c-11d8-b4e9-00304f19f545"), typeof (RegVersionRulesCreatorForm));
    service.RegisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad001b5-306c-11d8-b4e9-00304f19f545"), typeof (RegVersionRulesCreatorForm));
    service.RegisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad00278-306c-11d8-b4e9-00304f19f545"), typeof (RegVersionRulesCreatorForm));
  }

  /// <summary>Разрегистрировать класс</summary>
  /// <param name="service"></param>
  public static void Detach(IObjectCreatorService service)
  {
    service.UnregisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad001b4-306c-11d8-b4e9-00304f19f545"), typeof (RegVersionRulesCreatorForm));
    service.UnregisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad001b5-306c-11d8-b4e9-00304f19f545"), typeof (RegVersionRulesCreatorForm));
  }
}
