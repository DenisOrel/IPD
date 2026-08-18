// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ConvertFullData
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс, описывающий куда и как требуется поместить значение атрибута </summary>
internal class ConvertFullData
{
  private ConvertAction _сonvertAction;
  private ConvertTarget _сonvertTarget;

  public ConvertFullData(ConvertTarget сonvertTarget, ConvertAction сonvertAction)
  {
    this._сonvertAction = сonvertAction;
    this._сonvertTarget = сonvertTarget;
  }

  /// <summary> действие, которое необходимо произвести с атрибутом </summary>
  public ConvertAction Action => this._сonvertAction;

  /// <summary> Мишень для назначения атрибута - поле спецификации, атрибут объекта, атрибут связи </summary>
  public ConvertTarget Target => this._сonvertTarget;
}
