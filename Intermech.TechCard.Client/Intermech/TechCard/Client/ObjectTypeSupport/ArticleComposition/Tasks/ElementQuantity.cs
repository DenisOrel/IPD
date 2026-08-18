// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks.ElementQuantity
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Tasks;

/// <summary>
/// Класс для хранения пар значений "количество в КСЕ" - количество в "ТП"
/// </summary>
internal class ElementQuantity
{
  /// <summary>Количество в КСЕ</summary>
  private MeasuredValue _designQuantity;
  /// <summary>Количество в ТП</summary>
  private MeasuredValue _techQuantity;
  /// <summary>Оставшееся количество в КСЕ</summary>
  private MeasuredValue _remainQuantity;
  /// <summary>Ид. версии объекта</summary>
  public readonly ITypedInfoItem TypedInfoItem;
  /// <summary>Выполнена ли инициализация класса MeasureHelper</summary>
  private static bool _measureHelperInitialized;

  /// <summary>Создать экземпляр класса</summary>
  public ElementQuantity(
    ITypedInfoItem typedInfoItem,
    MeasuredValue designQuantity,
    MeasuredValue techQuantity)
  {
    this.TypedInfoItem = typedInfoItem;
    this._designQuantity = designQuantity;
    this._techQuantity = techQuantity;
  }

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="typedInfoItem">Описание элемента</param>
  /// <param name="designValue">Строковое значение количества для КСЕ</param>
  /// <param name="techValue">Строковое значение количество для ТП</param>
  public ElementQuantity(ITypedInfoItem typedInfoItem, string designValue, string techValue)
  {
    this.TypedInfoItem = typedInfoItem;
    ElementQuantity.InitMeasureHelper();
    if (designValue != string.Empty)
      this._designQuantity = MeasureHelper.ConvertToBaseMeasure(MeasureHelper.ConvertToMeasuredValue(designValue));
    if (!(techValue != string.Empty))
      return;
    this._techQuantity = MeasureHelper.ConvertToBaseMeasure(MeasureHelper.ConvertToMeasuredValue(techValue));
  }

  /// <summary>Количество в КСЕ</summary>
  public MeasuredValue DesignQuantity
  {
    get => this._designQuantity;
    set
    {
      if (object.Equals((object) this._designQuantity, (object) value))
        return;
      this._designQuantity = value;
      this._remainQuantity = (MeasuredValue) null;
    }
  }

  /// <summary>Количество в ТП</summary>
  public MeasuredValue TechQuantity
  {
    get => this._techQuantity;
    set
    {
      if (object.Equals((object) this._techQuantity, (object) value))
        return;
      this._techQuantity = value;
      this._remainQuantity = (MeasuredValue) null;
    }
  }

  /// <summary>Оставшееся количество</summary>
  public MeasuredValue RemainQuantity
  {
    get
    {
      if (this._remainQuantity != null || (this.TechQuantity == null || this.TechQuantity.MeasureID == 0L) && (this.DesignQuantity == null || this.DesignQuantity.MeasureID == 0L))
        return this._remainQuantity;
      if (this.TechQuantity == null || this.TechQuantity.MeasureID == 0L)
      {
        this._remainQuantity = this.DesignQuantity;
      }
      else
      {
        if (this.DesignQuantity != null)
        {
          if (this.DesignQuantity.MeasureID != 0L)
          {
            try
            {
              this._remainQuantity = MeasureHelper.Substract(this.DesignQuantity, this.TechQuantity);
              goto label_11;
            }
            catch (KernelExceptionID ex)
            {
              if (ex.ErrorID != 223)
                throw;
              this._remainQuantity = this.DesignQuantity;
              goto label_11;
            }
          }
        }
        this._remainQuantity = new MeasuredValue(-this.TechQuantity.Value, this.TechQuantity.MeasureID);
      }
label_11:
      return this._remainQuantity;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private static void InitMeasureHelper()
  {
    if (ElementQuantity._measureHelperInitialized)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      MeasureHelper.Init(sessionKeeper.Session.GetMeasuresList());
    ElementQuantity._measureHelperInitialized = true;
  }
}
