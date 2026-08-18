
// Type: Intermech.Interfaces.MeasuredValue
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Interfaces
{
    /// <summary>Класс с описание значения, выраженного в единицах измерения</summary>
    /// <remarks>
    /// Для поддержки данных, сериализованных в "старом" формате (до переименования полей) и новом формате,
    /// вынуждены обрабатывать загрузку полей вручную </remarks>
    [Serializable]
    public class MeasuredValue : ISerializable, ICloneable, IEquatable<MeasuredValue>
    {
      private double _Value;
      /// <summary>
      /// Cтроковое представление значения и его единицы измерения (1 кг)
      /// </summary>
      private string _caption;
      private long _MeasureID;

      public long MeasureID => this._MeasureID;

      /// <summary>
      /// строковое представление значения и его единицы измерения (1 кг)
      /// </summary>
      public string Caption
      {
        get
        {
          if (!this.IsCaptionPresent)
            this._caption = MeasureHelper.ConvertToString(this.Value, this.MeasureID, false);
          return this._caption;
        }
        set => this._caption = value;
      }

      /// <summary>
      /// Возвращает признак, что текстовое представление присутствует.
      /// </summary>
      internal bool IsCaptionPresent => this._caption != null;

      public MeasuredValue(double aValue, long measureID, string caption)
      {
        this.Value = aValue;
        this._MeasureID = measureID;
        this._caption = caption;
      }

      /// <summary>Конструктор для ISerializable</summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public MeasuredValue(SerializationInfo info, StreamingContext context)
      {
        foreach (SerializationEntry serializationEntry in info)
        {
          switch (serializationEntry.Name)
          {
            case nameof (_Value):
              this._Value = (double) serializationEntry.Value;
              continue;
            case nameof (_caption):
            case nameof (Caption):
              this._caption = (string) serializationEntry.Value;
              continue;
            case nameof (MeasureID):
            case nameof (_MeasureID):
              this._MeasureID = (long) serializationEntry.Value;
              continue;
            default:
              continue;
          }
        }
      }

      /// <summary>Внимание! Этот конструктор можно юзать только когда проинициализирован MeasureHelper!</summary>
      public MeasuredValue(double aValue, long measureID)
      {
        this.Value = aValue;
        this._MeasureID = measureID;
        this._caption = MeasureHelper.ConvertToString(aValue, measureID, false);
      }

      public override string ToString() => this.Caption;

      public bool Equals(MeasuredValue otherValue)
      {
        return otherValue != null && (this == otherValue || !MeasureHelper.IsNewValue(this, otherValue));
      }

      public override bool Equals(object obj)
      {
        return !(obj is MeasuredValue otherValue) ? base.Equals(obj) : this.Equals(otherValue);
      }

      public override int GetHashCode() => this.Value.GetHashCode() ^ this.MeasureID.GetHashCode();

      public double Value
      {
        get => this._Value;
        set => this._Value = Math.Round(value, Consts.MaxPrecision);
      }

      /// <summary>Добавляет к данной величине значение operand</summary>
      public void Add(MeasuredValue operand)
      {
        MeasureDescriptor descriptor1 = MeasureHelper.FindDescriptor(this);
        MeasureDescriptor descriptor2 = MeasureHelper.FindDescriptor(operand);
        if (descriptor1.PhysicalQuantityID != descriptor2.PhysicalQuantityID)
          throw new KernelExceptionID(222, (object) descriptor1.LongName, (object) descriptor2.LongName);
        if (descriptor1.MeasureID == descriptor2.MeasureID)
        {
          this._Value += operand.Value;
        }
        else
        {
          MeasureDescriptor baseValue = MeasureHelper.FindBaseValue(descriptor1);
          this._Value = this._Value * descriptor1.K + operand.Value * descriptor2.K;
          this._MeasureID = baseValue.MeasureID;
        }
        this._caption = (string) null;
      }

      /// <summary>Вычитает из данной величины значение operand</summary>
      public void Substract(MeasuredValue operand)
      {
        MeasureDescriptor descriptor1 = MeasureHelper.FindDescriptor(this);
        MeasureDescriptor descriptor2 = MeasureHelper.FindDescriptor(operand);
        if (descriptor1.PhysicalQuantityID != descriptor2.PhysicalQuantityID)
          throw new KernelExceptionID(223, (object) descriptor1.LongName, (object) descriptor2.LongName);
        if (descriptor1.MeasureID == descriptor2.MeasureID)
        {
          this._Value -= operand.Value;
        }
        else
        {
          MeasureDescriptor baseValue = MeasureHelper.FindBaseValue(descriptor1);
          this._Value = this._Value * descriptor1.K - operand.Value * descriptor2.K;
          this._MeasureID = baseValue.MeasureID;
        }
        this._caption = (string) null;
      }

      /// <summary>Умножает данную величину на operand</summary>
      public void Multiply(MeasuredValue operand)
      {
        MeasureDescriptor descriptor1 = MeasureHelper.FindDescriptor(this);
        MeasureDescriptor descriptor2 = MeasureHelper.FindDescriptor(operand);
        MeasureDescriptor baseValue1 = MeasureHelper.FindBaseValue(descriptor2);
        MeasureDescriptor measureDescriptor = (MeasureDescriptor) null;
        MeasureDescriptor baseValue2 = MeasureHelper.FindBaseValue(descriptor1);
        if (descriptor1.PhysicalQuantityGuid == SystemGUIDs.objectQuantityGuid)
          measureDescriptor = descriptor2;
        else if (descriptor2.PhysicalQuantityGuid == SystemGUIDs.objectQuantityGuid)
          measureDescriptor = descriptor1;
        if (measureDescriptor != null)
        {
          this._Value *= operand.Value;
          if (this.MeasureID != measureDescriptor.MeasureID)
            this._MeasureID = measureDescriptor.MeasureID;
          this._caption = (string) null;
        }
        else
        {
          MeasureDescriptor md = MeasureHelper.FindOperationResultMeasure($"{baseValue2.ShortName}*{baseValue1.ShortName}").md;
          if (md.Empty)
          {
            if (baseValue2.MeasureID != baseValue1.MeasureID)
              md = MeasureHelper.FindOperationResultMeasure($"{baseValue1.ShortName}*{baseValue2.ShortName}").md;
            if (md.Empty)
              throw new KernelExceptionID(224 /*0xE0*/, (object) descriptor1.LongName, (object) descriptor2.LongName);
          }
          this._Value = this._Value * descriptor1.K * (operand.Value * descriptor2.K);
          if (this.MeasureID != md.MeasureID)
            this._MeasureID = md.MeasureID;
          this._caption = (string) null;
        }
      }

      /// <summary>Вычитает из данной величины operand</summary>
      public void Divide(MeasuredValue operand)
      {
        MeasureDescriptor descriptor1 = MeasureHelper.FindDescriptor(this);
        MeasureDescriptor descriptor2 = MeasureHelper.FindDescriptor(operand);
        (MeasureDescriptor md, float K) = MeasureHelper.FindOperationResultMeasure($"{descriptor1.ShortName}/{descriptor2.ShortName}");
        if (md.Empty)
          throw new KernelExceptionID(225, (object) descriptor1.LongName, (object) descriptor2.LongName);
        this._Value = this._Value / operand.Value * (double) K;
        if (this.MeasureID != md.MeasureID)
          this._MeasureID = md.MeasureID;
        this._caption = (string) null;
      }

      public object Clone() => (object) new MeasuredValue(this.Value, this.MeasureID, this._caption);

      /// <summary>
      /// Для поддержки данных, сериализованных в "старом" формате (до переименования полей) и новом формате,
      /// вынуждены обрабатывать загрузку полей вручную
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        info.AddValue("_Value", (object) this._Value, typeof (double));
        info.AddValue("MeasureID", (object) this._MeasureID, typeof (long));
        if (!this.IsCaptionPresent)
          return;
        info.AddValue("Caption", (object) this._caption, typeof (string));
      }
    }
}
