
// Type: Intermech.Client.Core.Show.Net.ConvertStream
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace Intermech.Client.Core.Show.Net;

/// <summary> Summary description for MyStream. </summary>
internal sealed class ConvertStream
{
  private IntPtr _buffer = IntPtr.Zero;
  private int _length;
  private int _position;
  private ConvertStream.LocalUnion _union;

  internal ConvertStream(int length, IntPtr buffer)
  {
    this._buffer = buffer;
    this._length = length;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private int PositionAdd(int sizetype)
  {
    return this._position + sizetype <= this._length ? (this._position += sizetype) - sizetype : (this._position = this._length);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal ConvertStream SkipBytes(int value)
  {
    this.PositionAdd(value);
    return this;
  }

  internal bool Eof
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (this._position < this._length)
        return false;
      int position = this._position;
      int length = this._length;
      return true;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal bool Readbool()
  {
    return (this._union.b = Marshal.ReadByte(this._buffer, this.PositionAdd(1))) > (byte) 0;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal float ReadFloat()
  {
    this._union.i = this.ReadInt32();
    return this._union.f;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal byte ReadByte() => this._union.b = Marshal.ReadByte(this._buffer, this.PositionAdd(1));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal byte PreReadByte() => this._union.b = Marshal.ReadByte(this._buffer, this._position);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal byte[] ReadBytes(int len)
  {
    byte[] numArray = new byte[len];
    for (int index = 0; index < len; ++index)
      numArray[index] = this.ReadByte();
    return numArray;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal string ReadStringAnsii(byte[] bytes) => Encoding.GetEncoding(1251).GetString(bytes);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal ushort ReadUInt16() => (ushort) Marshal.ReadInt16(this._buffer, this.PositionAdd(2));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal short ReadInt16() => Marshal.ReadInt16(this._buffer, this.PositionAdd(2));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal short[] ReadInt16(int len)
  {
    short[] numArray = new short[len];
    for (int index = 0; index < len; ++index)
      numArray[index] = this.ReadInt16();
    return numArray;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal int ReadInt32() => Marshal.ReadInt32(this._buffer, this.PositionAdd(4));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal int[] ReadInt32(int len)
  {
    int[] numArray = new int[len];
    for (int index = 0; index < len; ++index)
      numArray[index] = this.ReadInt32();
    return numArray;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal long ReadInt64() => Marshal.ReadInt64(this._buffer, this.PositionAdd(8));

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal ulong ReadUInt64() => (ulong) this.ReadInt64();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal ulong[] ReadUInt64(int len)
  {
    ulong[] numArray = new ulong[len];
    for (int index = 0; index < len; ++index)
      numArray[index] = this.ReadUInt64();
    return numArray;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal double ReadDouble()
  {
    this._union.i64 = this.ReadInt64();
    return this._union.d;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal double[] ReadDouble(int len)
  {
    double[] numArray = new double[len];
    for (int index = 0; index < len; ++index)
      numArray[index] = this.ReadDouble();
    return numArray;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal PointD ReadPointD() => new PointD(this.ReadDouble(), this.ReadDouble());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal PointD[] ReadPointD(int len)
  {
    PointD[] pointDArray = new PointD[len];
    for (int index = 0; index < len; ++index)
      pointDArray[index] = this.ReadPointD();
    return pointDArray;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal PointF ReadPointF() => new PointF(this.ReadFloat(), this.ReadFloat());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal PointF[] ReadPointF(int len)
  {
    PointF[] pointFArray = new PointF[len];
    for (int index = 0; index < len; ++index)
      pointFArray[index] = this.ReadPointF();
    return pointFArray;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal PointF ReadPointF16() => new PointF((float) this.ReadInt16(), (float) this.ReadInt16());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal PointF[] ReadPointF16(int len)
  {
    PointF[] pointFArray = new PointF[len];
    for (int index = 0; index < len; ++index)
      pointFArray[index] = this.ReadPointF16();
    return pointFArray;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal PointF ReadPointF32() => new PointF((float) this.ReadInt32(), (float) this.ReadInt32());

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal string ReadStringCodePage(byte[] bytes, Encoding coding)
  {
    Decoder decoder = coding.GetDecoder();
    int charCount1 = decoder.GetCharCount(bytes, 0, bytes.Length);
    char[] chars = new char[charCount1];
    decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
    int charCount2 = charCount1 - (charCount1 <= 0 || chars[charCount1 - 1] != char.MinValue ? 0 : 1);
    return new StringBuilder(charCount2 + 1).Insert(0, chars, 0, charCount2).ToString();
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal string ReadString(short[] shorts)
  {
    StringBuilder stringBuilder = new StringBuilder(shorts.Length + 1);
    stringBuilder.Length = shorts.Length;
    for (int index = 0; index < shorts.Length; ++index)
      stringBuilder[index] = (char) shorts[index];
    if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == char.MinValue)
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
    return stringBuilder.ToString();
  }

  [StructLayout(LayoutKind.Explicit)]
  private struct LocalUnion
  {
    [FieldOffset(0)]
    internal byte b;
    [FieldOffset(0)]
    internal int i;
    [FieldOffset(0)]
    internal long i64;
    [FieldOffset(0)]
    internal float f;
    [FieldOffset(0)]
    internal double d;
  }
}
