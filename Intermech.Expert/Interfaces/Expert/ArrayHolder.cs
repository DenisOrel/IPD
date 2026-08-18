// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.ArrayHolder
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Expert;
using Intermech.Localization;
using System;
using System.Collections;
using System.Text;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// Класс для хранения одномерных и двумерных массивов. Нижний индекс массива всегда 0
/// </summary>
[Serializable]
public class ArrayHolder : IEnumerable
{
  private int XSize = 8;
  private int YSize = 1;
  protected internal object[][] data;

  public int Width => this.XSize;

  public int Height => this.YSize;

  /// <summary>Прямой доступ к массиву</summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <returns></returns>
  public object this[int x, int y]
  {
    get
    {
      if (this.data == null)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert_261"));
      if (x < 0 || x >= this.XSize)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert_262"));
      if (y < 0 || y >= this.YSize)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert_263"));
      return this.data[x][y];
    }
    set
    {
      if (x < 0 || x > 1000000)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert_262"));
      if (y < 0 || y > 1000000)
        throw new ExpertServerException(LocalizationHolder.rm.GetString("Expert_263"));
      if (x < this.XSize && y < this.YSize)
      {
        if (this.data == null)
          this.InitData();
        if (this.data == null)
          return;
        this.data[x][y] = value;
      }
      else
      {
        if (x >= this.XSize)
        {
          int newSize = this.XSize;
          if (x >= newSize)
            newSize = x + 1;
          try
          {
            int xsize = this.data == null ? 0 : this.XSize;
            if (this.data == null)
              this.data = new object[newSize][];
            else
              Array.Resize<object[]>(ref this.data, newSize);
            for (int index = xsize; index < newSize; ++index)
              this.data[index] = new object[this.YSize];
          }
          finally
          {
            this.XSize = newSize;
          }
        }
        if (y >= this.YSize)
        {
          int num = this.YSize;
          if (num == 1)
            num = 8;
          if (y >= num)
            num = y + 1;
          try
          {
            if (this.data == null)
            {
              this.InitData(this.XSize, num);
            }
            else
            {
              for (int index = 0; index < this.XSize; ++index)
                Array.Resize<object>(ref this.data[index], num);
            }
          }
          finally
          {
            this.YSize = num;
          }
        }
        this.data[x][y] = value;
      }
    }
  }

  public ArrayHolder()
  {
  }

  public ArrayHolder(int xSize, int ySize) => this.InitData(xSize, ySize);

  public ArrayHolder(Array arr)
  {
    this.InitData(arr.Length, 1);
    for (int index = 0; index < arr.Length; ++index)
      this[index, 0] = arr.GetValue(index);
  }

  public ArrayHolder(PacketValue pv)
  {
    this.InitData(pv.Count, 1);
    for (int index = 0; index < pv.Count; ++index)
      this[index, 0] = pv[index].Value;
  }

  internal void InitData()
  {
    this.data = new object[this.XSize][];
    for (int index = 0; index < this.XSize; ++index)
      this.data[index] = new object[this.YSize];
  }

  internal void InitData(int xSize, int ySize)
  {
    this.XSize = xSize;
    this.YSize = ySize;
    this.InitData();
  }

  public IEnumerator GetEnumerator()
  {
    for (int j = 0; j < this.YSize; ++j)
    {
      for (int i = 0; i < this.XSize; ++i)
        yield return this[i, j];
    }
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int y = 0; y < this.YSize; ++y)
    {
      stringBuilder.Append("[");
      for (int x = 0; x < this.XSize; ++x)
      {
        if (this[x, y] != null)
          stringBuilder.Append(this[x, y].ToString());
        else
          stringBuilder.Append("null");
        if (x < this.XSize - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
    }
    return stringBuilder.ToString();
  }

  public object[] ToArray()
  {
    object[] array = new object[this.XSize * this.YSize];
    for (int y = 0; y < this.YSize; ++y)
    {
      for (int x = 0; x < this.XSize; ++x)
        array[y * this.XSize + x] = this[x, y];
    }
    return array;
  }
}
