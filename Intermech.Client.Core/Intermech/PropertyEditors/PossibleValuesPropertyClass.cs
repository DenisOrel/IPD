
// Type: Intermech.PropertyEditors.PossibleValuesPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.PropertyEditors;

public class PossibleValuesPropertyClass
{
  private DataTable possibleValues;
  private FieldTypes fieldType;

  public PossibleValuesPropertyClass(DataTable aPossibleValues, FieldTypes aft)
  {
    this.possibleValues = aPossibleValues;
    this.fieldType = aft;
  }

  public DataTable PossibleValues => this.possibleValues;

  public FieldTypes FieldType => this.fieldType;

  public override string ToString()
  {
    string empty = string.Empty;
    return this.possibleValues == null || this.possibleValues.Rows.Count <= 0 ? LocalizationHolder.rm.GetString("Client.Core_979") : LocalizationHolder.rm.GetString("Client.Core_978");
  }

  /// <summary>
  /// Сравнивает оригинальное содержимое с измененными значениями
  /// </summary>
  /// <param name="origValues"></param>
  /// <param name="lastValues"></param>
  /// <returns>true - если было удалено или изменено хотя бы одно значение (не описание); добавления не учитываются</returns>
  public static bool ValuesModifiedOrDeleted(DataTable origValues, DataTable lastValues)
  {
    if (origValues == null || origValues.Rows.Count == 0)
      return false;
    if (lastValues == null || lastValues.Rows.Count < origValues.Rows.Count)
      return true;
    string valueFieldName1 = ClientCommons.ExtractValueFieldName(origValues);
    string valueFieldName2 = ClientCommons.ExtractValueFieldName(lastValues);
    foreach (DataRow row1 in (InternalDataCollectionBase) origValues.Rows)
    {
      string str1 = Convert.ToString(row1[valueFieldName1]);
      bool flag = false;
      foreach (DataRow row2 in (InternalDataCollectionBase) lastValues.Rows)
      {
        string str2 = Convert.ToString(row2[valueFieldName2]);
        flag = str1 == str2;
        if (flag)
          break;
      }
      if (!flag)
        return true;
    }
    return false;
  }
}
