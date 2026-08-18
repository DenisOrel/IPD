// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.OneError
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание одной ошибки при сборе ведомости </summary>
public class OneError
{
  public long _objectId_Izdelie;
  public string _designation_Izdelie;
  public string _name_Izdelie;
  public long _f_PRJLINK_ID;
  public long _objectIdSP_KudaVhodit;
  public string _designationSp_KudaVhodit;
  public string _message_kurc;
  public string _message;

  /// <summary> Формирование полного текста сообщения </summary>
  /// <returns> Возвращается полный текст сообщения</returns>
  public string Message()
  {
    this._message = "";
    if (!string.IsNullOrEmpty(this._designationSp_KudaVhodit) && this._objectIdSP_KudaVhodit != 0L)
      this._message = $"{this._message}В спецификации {this._designationSp_KudaVhodit} [objId={(object) this._objectIdSP_KudaVhodit}].      ";
    if (!string.IsNullOrEmpty(this._designation_Izdelie) || !string.IsNullOrEmpty(this._name_Izdelie))
      this._message += "В записи:";
    if (!string.IsNullOrEmpty(this._designation_Izdelie))
      this._message = $"{this._message} {this._designation_Izdelie}";
    if (!string.IsNullOrEmpty(this._name_Izdelie))
      this._message = $"{this._message} {this._name_Izdelie}";
    if (!string.IsNullOrEmpty(this._designation_Izdelie) || !string.IsNullOrEmpty(this._name_Izdelie))
      this._message += ".";
    if (!string.IsNullOrEmpty(this._message))
      this._message += " ";
    this._message += this._message_kurc;
    return this._message;
  }
}
