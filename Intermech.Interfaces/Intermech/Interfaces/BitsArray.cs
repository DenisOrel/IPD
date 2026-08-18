
// Type: Intermech.Interfaces.BitsArray
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Битовый массив</summary>
    public static class BitsArray
    {
      /// <summary>8 бит в 1 байте</summary>
      private static int elementBitsSize = 8;
      /// <summary>Количество бит в типе данных Int16</summary>
      private static int bitsInInt16 = 16 /*0x10*/;
      /// <summary>Количество бит в типе данных Int32</summary>
      private static int bitsInInt32 = 32 /*0x20*/;

      /// <summary>Считать значение бита с указанным номером</summary>
      /// <param name="Bits">Массив байт</param>
      /// <param name="bitIndex">Индекс бита (0 .. Capacity-1)</param>
      /// <returns>Значение бита</returns>
      public static bool GetBit(byte[] Bits, int bitIndex)
      {
        if (Bits == null)
          return false;
        if (bitIndex < 0 || bitIndex >= BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception2, (object) bitIndex, (object) BitsArray.Capacity(Bits)));
        return ((uint) Bits[bitIndex / BitsArray.elementBitsSize] & (uint) (1 << bitIndex % BitsArray.elementBitsSize)) > 0U;
      }

      /// <summary>Установить значение бита с указанным номером</summary>
      /// <param name="Bits">Массив байт</param>
      /// <param name="bitIndex">Номер бита</param>
      /// <param name="value">Значение бита</param>
      public static void SetBit(byte[] Bits, int bitIndex, bool value)
      {
        if (Bits == null)
          return;
        if (bitIndex < 0 || bitIndex >= BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception2, (object) bitIndex, (object) BitsArray.Capacity(Bits)));
        if (value)
          Bits[bitIndex / BitsArray.elementBitsSize] = (byte) ((uint) Bits[bitIndex / BitsArray.elementBitsSize] | (uint) (1 << bitIndex % BitsArray.elementBitsSize));
        else
          Bits[bitIndex / BitsArray.elementBitsSize] = (byte) ((uint) Bits[bitIndex / BitsArray.elementBitsSize] & (uint) ~(1 << bitIndex % BitsArray.elementBitsSize));
      }

      /// <summary>Рассчитать ёмкость указанного массива в битах</summary>
      /// <param name="Bits">Массив байт</param>
      public static int Capacity(byte[] Bits)
      {
        return Bits == null ? 0 : Bits.Length * BitsArray.elementBitsSize;
      }

      /// <summary>Установка всех битов массива в 0 или в 1</summary>
      /// <param name="Bits">Массив байт</param>
      /// <param name="value"></param>
      public static void SetAllTo(byte[] Bits, bool value)
      {
        if (Bits == null)
          return;
        for (int index = 0; index < Bits.Length; ++index)
          Bits[index] = !value ? (byte) 0 : byte.MaxValue;
      }

      /// <summary>
      /// Извлечь из битового массива Int16, используя определённое количество бит
      /// </summary>
      /// <param name="Bits">Ссылка на массив байт</param>
      /// <param name="index">Номер бита, с которого начинается число</param>
      /// <param name="count">Сколько бит в  массиве используется для извлечения значений в число</param>
      /// <returns>Число из битового массива</returns>
      public static short ExtractInt16(byte[] Bits, int index, int count)
      {
        if (index >= BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception2, (object) index, (object) BitsArray.Capacity(Bits)));
        if (index + count > BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception3, (object) count, (object) index, (object) BitsArray.Capacity(Bits)));
        if (count > BitsArray.bitsInInt16)
          count = BitsArray.bitsInInt16;
        short int16 = 0;
        for (int bitIndex = index; bitIndex < index + count; ++bitIndex)
        {
          if (BitsArray.GetBit(Bits, bitIndex))
            int16 |= (short) (1 << (bitIndex - index) % BitsArray.bitsInInt16);
        }
        return int16;
      }

      /// <summary>
      /// Вставить в битовый массив число Int16, используя определённое количество бит
      /// </summary>
      /// <param name="Bits">Ссылка на массив байт</param>
      /// <param name="value">Число, count битов которого надо поместить в битовый массив начиная с позиции index</param>
      /// <param name="index">Номер бита в массиве, начиная с которого в массиве будут размещены count битов числа</param>
      /// <param name="count">Сколько бит из числа будут перенесены в массив</param>
      public static void PasteInt16(byte[] Bits, short value, int index, int count)
      {
        if (index >= BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception2, (object) index, (object) BitsArray.Capacity(Bits)));
        if (index + count > BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception4, (object) count, (object) index, (object) BitsArray.Capacity(Bits)));
        if (count > BitsArray.bitsInInt16)
          count = BitsArray.bitsInInt16;
        for (int bitIndex = index; bitIndex < index + count; ++bitIndex)
          BitsArray.SetBit(Bits, bitIndex, (ushort) ((int) value >> (bitIndex - index) % BitsArray.bitsInInt16 & -65535) == (ushort) 1);
      }

      /// <summary>
      /// Извлечь из битового массива Int32, используя определённое количество бит
      /// </summary>
      /// <param name="Bits">Ссылка на массив байт</param>
      /// <param name="index">Номер бита, с которого начинается число</param>
      /// <param name="count">Сколько бит в  массиве используется для извлечения значений в число</param>
      /// <returns>Число из битового массива</returns>
      public static int ExtractInt32(byte[] Bits, int index, int count)
      {
        if (index >= BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception2, (object) index, (object) BitsArray.Capacity(Bits)));
        if (index + count > BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception3, (object) count, (object) index, (object) BitsArray.Capacity(Bits)));
        if (count > BitsArray.bitsInInt32)
          count = BitsArray.bitsInInt32;
        int int32 = 0;
        for (int bitIndex = index; bitIndex < index + count; ++bitIndex)
        {
          if (BitsArray.GetBit(Bits, bitIndex))
            int32 |= 1 << (bitIndex - index) % BitsArray.bitsInInt32;
        }
        return int32;
      }

      /// <summary>
      /// Вставить в битовый массив число Int32, используя определённое количество бит
      /// </summary>
      /// <param name="Bits">Ссылка на массив байт</param>
      /// <param name="value">Число, count битов которого надо поместить в битовый массив начиная с позиции index</param>
      /// <param name="index">Номер бита в массиве, начиная с которого в массиве будут размещены count битов числа</param>
      /// <param name="count">Сколько бит из числа будут перенесены в массив</param>
      public static void PasteInt32(byte[] Bits, int value, int index, int count)
      {
        if (index >= BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception2, (object) index, (object) BitsArray.Capacity(Bits)));
        if (index + count > BitsArray.Capacity(Bits))
          throw new ApplicationException(string.Format(BitsArrayConsts.Exception4, (object) count, (object) index, (object) BitsArray.Capacity(Bits)));
        if (count > BitsArray.bitsInInt32)
          count = BitsArray.bitsInInt32;
        for (int bitIndex = index; bitIndex < index + count; ++bitIndex)
          BitsArray.SetBit(Bits, bitIndex, (uint) ((ulong) (value >> (bitIndex - index) % BitsArray.bitsInInt32) & 1UL) == 1U);
      }
    }
}
