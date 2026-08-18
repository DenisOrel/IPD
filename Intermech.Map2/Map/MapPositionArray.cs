// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapPositionArray
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;
using System.Drawing;


namespace Intermech.Map
{
    [Serializable]
    internal sealed class MapPositionArray
    {
      internal const int MAXDIST = 2147483647 /*0x7FFFFFFF*/;
      private int[,] myArray;
      private float myCellX;
      private float myCellY;
      private bool myInvalid;
      private float myMaxX;
      private float myMaxY;
      private float myMinX;
      private float myMinY;
      private int myUpperBoundX;
      private int myUpperBoundY;
      internal const int OCCUPIED = 0;
      internal const int START = 1;

      internal MapPositionArray()
      {
        this.myInvalid = true;
        this.myMinX = 1f;
        this.myMinY = 1f;
        this.myMaxX = -1f;
        this.myMaxY = -1f;
        this.myCellX = 10f;
        this.myCellY = 10f;
        this.myArray = (int[,]) null;
        this.myUpperBoundX = 0;
        this.myUpperBoundY = 0;
      }

      private void BreakIn(int x, int y, int inc, bool vert, int lowx, int hix, int lowy, int hiy)
      {
        int num = this.myArray[x, y];
        this.myArray[x, y] = int.MaxValue;
        while (num == 0 && x > lowx && x < hix && y > lowy && y < hiy)
        {
          if (vert)
            y += inc;
          else
            x += inc;
          num = this.myArray[x, y];
          this.myArray[x, y] = int.MaxValue;
        }
      }

      private int BreakOut(int x, int y, int inc, bool vert, int lowx, int hix, int lowy, int hiy)
      {
        int num1 = 1;
        int num2 = this.myArray[x, y];
        this.myArray[x, y] = num1;
        while (num2 == 0 && x > lowx && x < hix && y > lowy && y < hiy)
        {
          if (vert)
            y += inc;
          else
            x += inc;
          num2 = this.myArray[x, y];
          this.myArray[x, y] = num1++;
        }
        return vert ? y : x;
      }

      internal int GetDist(float x, float y)
      {
        if (!this.InBounds(x, y))
          return 0;
        x -= this.myMinX;
        x /= this.myCellX;
        y -= this.myMinY;
        y /= this.myCellY;
        return this.myArray[(int) x, (int) y];
      }

      private bool InBounds(float x, float y)
      {
        return (double) this.myMinX <= (double) x && (double) x <= (double) this.myMaxX && (double) this.myMinY <= (double) y && (double) y <= (double) this.myMaxY;
      }

      internal void Initialize(RectangleF rect)
      {
        if ((double) rect.Width <= 0.0 || (double) rect.Height <= 0.0)
          return;
        float x = rect.X;
        float y = rect.Y;
        float num1 = rect.X + rect.Width;
        float num2 = rect.Y + rect.Height;
        this.myMinX = (float) Math.Floor(((double) x - (double) this.myCellX) / (double) this.myCellX) * this.myCellX;
        this.myMinY = (float) Math.Floor(((double) y - (double) this.myCellY) / (double) this.myCellY) * this.myCellY;
        this.myMaxX = (float) Math.Ceiling(((double) num1 + 2.0 * (double) this.myCellX) / (double) this.myCellX) * this.myCellX;
        this.myMaxY = (float) Math.Ceiling(((double) num2 + 2.0 * (double) this.myCellY) / (double) this.myCellY) * this.myCellY;
        int length1 = 1 + (int) Math.Ceiling(((double) this.myMaxX - (double) this.myMinX) / (double) this.myCellX);
        int length2 = 1 + (int) Math.Ceiling(((double) this.myMaxY - (double) this.myMinY) / (double) this.myCellY);
        if (this.myArray == null || this.myUpperBoundX < length1 - 1 || this.myUpperBoundY < length2 - 1)
        {
          this.myArray = new int[length1, length2];
          this.myUpperBoundX = length1 - 1;
          this.myUpperBoundY = length2 - 1;
        }
        this.SetAll(int.MaxValue);
      }

      internal bool IsOccupied(float x, float y) => this.GetDist(x, y) == 0;

      internal bool IsUnoccupied(float x, float y, float w, float h)
      {
        int num1 = (int) (((double) x - (double) this.myMinX) / (double) this.myCellX);
        int num2 = (int) (((double) y - (double) this.myMinY) / (double) this.myCellY);
        int num3 = (int) ((double) Math.Max(0.0f, w) / (double) this.myCellX) + 1;
        int num4 = (int) ((double) Math.Max(0.0f, h) / (double) this.myCellY) + 1;
        int num5 = Math.Min(num1 + num3, this.myUpperBoundX);
        int num6 = Math.Min(num2 + num4, this.myUpperBoundY);
        for (int index1 = num1; index1 <= num5; ++index1)
        {
          for (int index2 = num2; index2 <= num6; ++index2)
          {
            if (this.myArray[index1, index2] == 0)
              return false;
          }
        }
        return true;
      }

      internal void Propagate(PointF p1, float fromDir, PointF p2, float toDir, RectangleF bounds)
      {
        if (this.myArray == null)
          return;
        float x1 = p1.X;
        float y1 = p1.Y;
        if (!this.InBounds(x1, y1))
          return;
        float index1 = (x1 - this.myMinX) / this.myCellX;
        float index2 = (y1 - this.myMinY) / this.myCellY;
        float x2 = p2.X;
        float y2 = p2.Y;
        if (!this.InBounds(x2, y2))
          return;
        float x3 = (x2 - this.myMinX) / this.myCellX;
        float y3 = (y2 - this.myMinY) / this.myCellY;
        if ((double) Math.Abs(index1 - x3) <= 1.0 && (double) Math.Abs(index2 - y3) <= 1.0)
        {
          this.myArray[(int) index1, (int) index2] = 0;
        }
        else
        {
          bool vert = false;
          float x4 = bounds.X;
          float y4 = bounds.Y;
          float num1 = bounds.X + bounds.Width;
          float num2 = bounds.Y + bounds.Height;
          float val2_1 = (x4 - this.myMinX) / this.myCellX;
          float val2_2 = (y4 - this.myMinY) / this.myCellY;
          float val2_3 = (num1 - this.myMinX) / this.myCellX;
          float val2_4 = (num2 - this.myMinY) / this.myCellY;
          int lowx = Math.Max(0, Math.Min(this.myUpperBoundX, (int) val2_1));
          int hix = Math.Min(this.myUpperBoundX, Math.Max(0, (int) val2_3));
          int lowy = Math.Max(0, Math.Min(this.myUpperBoundY, (int) val2_2));
          int hiy = Math.Min(this.myUpperBoundY, Math.Max(0, (int) val2_4));
          int x5 = (int) index1;
          int y5 = (int) index2;
          int inc = (double) fromDir == 0.0 || (double) fromDir == 90.0 ? 1 : -1;
          if ((double) fromDir == 90.0 || (double) fromDir == 270.0)
            y5 = this.BreakOut(x5, y5, inc, vert, lowx, hix, lowy, hiy);
          else
            x5 = this.BreakOut(x5, y5, inc, vert, lowx, hix, lowy, hiy);
          this.BreakIn((int) x3, (int) y3, (double) toDir == 0.0 || (double) toDir == 90.0 ? 1 : -1, (double) toDir == 90.0 || (double) toDir == 270.0, lowx, hix, lowy, hiy);
          this.Spread(x5, y5, 1, false, lowx, hix, lowy, hiy);
          this.Spread(x5, y5, -1, false, lowx, hix, lowy, hiy);
          this.Spread(x5, y5, 1, true, lowx, hix, lowy, hiy);
          this.Spread(x5, y5, -1, true, lowx, hix, lowy, hiy);
        }
      }

      private int Ray(int x, int y, int inc, bool vert, int lowx, int hix, int lowy, int hiy)
      {
        int num = this.myArray[x, y];
        switch (num)
        {
          case 0:
          case int.MaxValue:
            return vert ? y : x;
          default:
            if (vert)
              y += inc;
            else
              x += inc;
            while (lowx <= x && x <= hix && lowy <= y && y <= hiy && ++num < this.myArray[x, y])
            {
              this.myArray[x, y] = num;
              if (vert)
                y += inc;
              else
                x += inc;
            }
            goto case 0;
        }
      }

      internal void SetAll(int v)
      {
        if (this.myArray == null)
          return;
        for (int index1 = 0; index1 <= this.myUpperBoundX; ++index1)
        {
          for (int index2 = 0; index2 <= this.myUpperBoundY; ++index2)
            this.myArray[index1, index2] = v;
        }
      }

      internal void SetAllUnoccupied(int v)
      {
        if (this.myArray == null)
          return;
        for (int index1 = 0; index1 <= this.myUpperBoundX; ++index1)
        {
          for (int index2 = 0; index2 <= this.myUpperBoundY; ++index2)
          {
            if (this.myArray[index1, index2] != 0)
              this.myArray[index1, index2] = v;
          }
        }
      }

      internal void SetDist(float x, float y, int v)
      {
        if (!this.InBounds(x, y))
          return;
        x -= this.myMinX;
        x /= this.myCellX;
        y -= this.myMinY;
        y /= this.myCellY;
        this.myArray[(int) x, (int) y] = v;
      }

      private void Spread(int x, int y, int inc, bool vert, int lowx, int hix, int lowy, int hiy)
      {
        if (x < lowx || x > hix || y < lowy || y > hiy)
          return;
        int num = this.Ray(x, y, inc, vert, lowx, hix, lowy, hiy);
        if (vert)
        {
          if (inc > 0)
          {
            for (int y1 = y + inc; y1 < num; y1 += inc)
            {
              this.Spread(x, y1, 1, !vert, lowx, hix, lowy, hiy);
              this.Spread(x, y1, -1, !vert, lowx, hix, lowy, hiy);
            }
          }
          else
          {
            for (int y2 = y + inc; y2 > num; y2 += inc)
            {
              this.Spread(x, y2, 1, !vert, lowx, hix, lowy, hiy);
              this.Spread(x, y2, -1, !vert, lowx, hix, lowy, hiy);
            }
          }
        }
        else if (inc > 0)
        {
          for (int x1 = x + inc; x1 < num; x1 += inc)
          {
            this.Spread(x1, y, 1, !vert, lowx, hix, lowy, hiy);
            this.Spread(x1, y, -1, !vert, lowx, hix, lowy, hiy);
          }
        }
        else
        {
          for (int x2 = x + inc; x2 > num; x2 += inc)
          {
            this.Spread(x2, y, 1, !vert, lowx, hix, lowy, hiy);
            this.Spread(x2, y, -1, !vert, lowx, hix, lowy, hiy);
          }
        }
      }

      internal RectangleF Bounds
      {
        get
        {
          return new RectangleF(this.myMinX, this.myMinY, this.myMaxX - this.myMinX, this.myMaxY - this.myMinY);
        }
      }

      internal SizeF CellSize
      {
        get => new SizeF(this.myCellX, this.myCellY);
        set
        {
          if ((double) value.Width < 1.0 || (double) value.Height < 1.0 || (double) value.Width == (double) this.myCellX && (double) value.Height == (double) this.myCellY)
            return;
          this.myCellX = value.Width;
          this.myCellY = value.Height;
          this.Initialize(new RectangleF(this.myMinX, this.myMinY, this.myMaxX - this.myMinX, this.myMaxY - this.myMinY));
        }
      }

      internal bool Invalid
      {
        get => this.myInvalid;
        set => this.myInvalid = value;
      }
    }
}
