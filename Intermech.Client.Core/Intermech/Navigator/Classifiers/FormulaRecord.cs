
// Type: Intermech.Navigator.Classifiers.FormulaRecord
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;


namespace Intermech.Navigator.Classifiers;

/// <summary>
/// Запись в значении атрибута соответсвующая формуле классификатора
/// </summary>
public class FormulaRecord
{
  /// <summary>Глобальный идентификатор атрибута</summary>
  public string AttributeGuid;
  /// <summary>Строка с формулой</summary>
  public string Formula;
  /// <summary>Признак контроля размера вычисляемого значения</summary>
  public bool SizeControl;
  /// <summary>Признак использования пропущенных значений</summary>
  public bool UseMissed;

  public FormulaRecord(string attributeValue)
  {
    if (attributeValue.Length == 0)
    {
      this.AttributeGuid = string.Empty;
      this.Formula = string.Empty;
      this.SizeControl = false;
      this.UseMissed = false;
    }
    else
    {
      if (attributeValue[0] == '@')
      {
        this.SizeControl = true;
        attributeValue = attributeValue.Remove(0, 1);
      }
      if (attributeValue[0] == '#')
      {
        this.UseMissed = true;
        attributeValue = attributeValue.Remove(0, 1);
      }
      int length = attributeValue.IndexOf('=', 0);
      if (length <= 0)
        return;
      string text = attributeValue.Substring(0, length);
      this.AttributeGuid = GuidHelper.IsGuid(text) ? text : string.Empty;
      this.Formula = attributeValue.Substring(length + 1, attributeValue.Length - length - 1);
    }
  }

  public FormulaRecord(string attributeGuid, string formula, bool sizeControl, bool useMissed)
  {
    this.AttributeGuid = attributeGuid;
    this.Formula = formula;
    this.SizeControl = sizeControl;
    this.UseMissed = useMissed;
  }

  public override string ToString()
  {
    return $"{(this.SizeControl ? "@" : string.Empty)}{(this.UseMissed ? "#" : string.Empty)}{this.AttributeGuid}={this.Formula}";
  }
}
