// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientMeasureRuleHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using ImSSP;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Серверный класс для работы со строкой настроек ввода значений атрибутов, выраженных в единицах измерения
/// </summary>
internal class ClientMeasureRuleHelper(string ruleString, object attribute) : BaseMeasureRuleHelper(ruleString, attribute)
{
  private CAttributeTypeInfo4 Attribute => this._Attribute as CAttributeTypeInfo4;

  /// <summary>Имя атрибута для сообщений об ошибках и логов</summary>
  protected override string ObjectName => this.Attribute.ObjectName;

  /// <summary>Свойство атрибута Размер</summary>
  protected override long SizeType => this.Attribute.SizeType;

  /// <summary>
  /// Возвращает список наименований допустимых физических величин по их гуидам
  /// </summary>
  private string GetPhysicalValuesCaption(long[] guids)
  {
    StringBuilder stringBuilder = new StringBuilder();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < guids.Length; ++index)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(guids[index]);
        if (!objectInfo.Empty)
          stringBuilder.Append(objectInfo.Caption + ", ");
        else
          stringBuilder.AppendFormat("Object N{0} not found", (object) guids[index].ToString());
      }
    }
    if (stringBuilder.Length > 0)
      stringBuilder.Length -= 2;
    return stringBuilder.ToString();
  }

  /// <summary>
  /// Проверяет допустимость присвоения данному атрибуту единицы измерения muID
  /// </summary>
  public void ValidateMuID(IDBMeasureAttributeType attribute, long muID)
  {
    long[] validPhysicalValues = attribute.GetValidPhysicalValues();
    if (validPhysicalValues.Length == 0)
      return;
    bool flag = false;
    long physicalQuantityId = MeasureHelper.FindDescriptor(muID).PhysicalQuantityID;
    for (int index = 0; index < validPhysicalValues.Length; ++index)
    {
      if (physicalQuantityId == validPhysicalValues[index])
      {
        flag = true;
        break;
      }
    }
    if (!flag)
    {
      string physicalValuesCaption = this.GetPhysicalValuesCaption(validPhysicalValues);
      throw new KernelExceptionID(sc_10447.ssp_appserver_10448(1789903052), (object) (attribute as CAttributeTypeInfo).Name, (object) physicalValuesCaption);
    }
  }

  /// <summary>Проверяет единицы измерения на совместимость.</summary>
  /// <param name="aMeasureID">Идентификатор объекта-единицы измерения, который
  /// проверяется на совместимость с данным параметром (например, единица массы не
  /// совместима с единицей объема и т.п.). </param>
  public bool IsCompatible(IDBMeasureAttributeType attribute, long aMeasureID)
  {
    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(aMeasureID);
    if (descriptor.Empty)
      return false;
    long[] validPhysicalValues = attribute.GetValidPhysicalValues();
    bool flag = validPhysicalValues.Length == 0;
    for (int index = 0; index < validPhysicalValues.Length; ++index)
    {
      if (descriptor.PhysicalQuantityID == validPhysicalValues[index])
      {
        flag = true;
        break;
      }
    }
    return flag;
  }
}
