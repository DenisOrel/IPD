// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierFormula
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;


namespace Intermech.Kernel.Services;

internal sealed class ClassifierFormula
{
  public bool Private;
  private readonly bool _sizeControl;
  private readonly bool _needCalculate;
  private long _curentVal;
  private readonly long _maxVal = -1;
  private readonly int _step;
  private readonly int _countZero;
  public static readonly string DigitsSymbols = "$$$";
  public static readonly char ValuesSeparator = ':';
  public static readonly string CalculateSeparator = "###";

  public Guid AttributeGuid { get; } = Guid.Empty;

  public string Formula { get; set; } = string.Empty;

  public bool UseMissed { get; }

  public string GetValue()
  {
    if (!this._needCalculate)
      return this.Formula;
    if (this._sizeControl && this._maxVal != -1L && this._curentVal > this._maxVal)
      throw new Exception($"Расчитанное значение ({this._curentVal}) превысило максимальную допустимую величину ({this._maxVal})");
    string str = this.Formula.Replace(ClassifierFormula.DigitsSymbols, this.FormingCount(this._curentVal, this._countZero));
    this._curentVal += (long) this._step;
    return str;
  }

  public ClassifierFormula(string attributeValue)
  {
    if (!string.IsNullOrEmpty(attributeValue) && attributeValue[0] == '@')
    {
      this._sizeControl = true;
      attributeValue = attributeValue.Remove(0, 1);
    }
    if (!string.IsNullOrEmpty(attributeValue) && attributeValue[0] == '#')
    {
      this.UseMissed = true;
      attributeValue = attributeValue.Remove(0, 1);
    }
    int length = attributeValue.IndexOf('=', 0);
    if (length <= 0)
      return;
    string str1 = attributeValue.Substring(0, length);
    this.AttributeGuid = GuidHelper.IsGuid(str1) ? new Guid(str1) : Guid.Empty;
    this.Formula = attributeValue.Substring(length + 1, attributeValue.Length - length - 1);
    int startIndex = this.Formula.IndexOf(ClassifierFormula.DigitsSymbols);
    if (startIndex < 0)
      return;
    int num = this.Formula.LastIndexOf(ClassifierFormula.DigitsSymbols);
    if (startIndex == num)
      return;
    this._needCalculate = true;
    string str2 = this.Formula.Substring(startIndex + ClassifierFormula.DigitsSymbols.Length, num - startIndex - ClassifierFormula.DigitsSymbols.Length);
    try
    {
      string[] strArray = str2.Split(ClassifierFormula.ValuesSeparator);
      this._curentVal = Convert.ToInt64(strArray[0]);
      this._step = Convert.ToInt32(strArray[1]);
      this._countZero = Convert.ToInt32(strArray[2]);
      this._maxVal = Convert.ToInt64(strArray[3]);
      this.Formula = this.Formula.Remove(startIndex, num - startIndex);
    }
    catch
    {
      this._needCalculate = false;
    }
  }

  private string FormingCount(long val, int count)
  {
    int num = count - val.ToString().Length;
    if (num >= 0)
    {
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        for (int index = 0; index < num; ++index)
          stringBuilder.Append('0');
        stringBuilder.Append(val.ToString());
        return stringBuilder.ToString();
      }
    }
    if (this._sizeControl)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1005"), (object) MetaDataHelper.GetAttributeTypeName(this.AttributeGuid), (object) count, (object) val));
    return val.ToString();
  }

  public override string ToString()
  {
    return $"{(this._sizeControl ? (object) "@" : (object) string.Empty)}{this.AttributeGuid}={this.Formula}";
  }
}
