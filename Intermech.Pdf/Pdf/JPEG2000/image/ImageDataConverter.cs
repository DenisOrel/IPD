// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.ImageDataConverter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.JPEG2000.image
{
    internal class ImageDataConverter : ImgDataAdapter, BlockImageDataSource, ImageData
    {
      private int fp;
      private BlockImageDataSource src;
      private DataBlock srcBlk;

      internal ImageDataConverter(BlockImageDataSource imgSrc)
        : base((ImageData) imgSrc)
      {
        this.srcBlk = (DataBlock) new DataBlockInt();
        this.src = imgSrc;
        this.fp = 0;
      }

      internal ImageDataConverter(BlockImageDataSource imgSrc, int fp)
        : base((ImageData) imgSrc)
      {
        this.srcBlk = (DataBlock) new DataBlockInt();
        this.src = imgSrc;
        this.fp = fp;
      }

      public virtual DataBlock getCompData(DataBlock blk, int c) => this.getData(blk, c, false);

      private DataBlock getData(DataBlock blk, int c, bool intern)
      {
        int dataType = blk.DataType;
        DataBlock blk1;
        if (dataType == this.srcBlk.DataType)
        {
          blk1 = blk;
        }
        else
        {
          blk1 = this.srcBlk;
          blk1.ulx = blk.ulx;
          blk1.uly = blk.uly;
          blk1.w = blk.w;
          blk1.h = blk.h;
        }
        this.srcBlk = !intern ? this.src.getCompData(blk1, c) : this.src.getInternCompData(blk1, c);
        if (this.srcBlk.DataType == dataType)
          return this.srcBlk;
        int w = this.srcBlk.w;
        int h = this.srcBlk.h;
        if (dataType != 3)
        {
          if (dataType != 4)
            throw new ArgumentException("Only integer and float data are supported by JJ2000");
          float[] numArray = (float[]) blk.Data;
          if (numArray == null || numArray.Length < w * h)
          {
            numArray = new float[w * h];
            blk.Data = (object) numArray;
          }
          blk.scanw = this.srcBlk.w;
          blk.offset = 0;
          blk.progressive = this.srcBlk.progressive;
          int[] data = (int[]) this.srcBlk.Data;
          this.fp = this.src.getFixedPoint(c);
          if (this.fp != 0)
          {
            float num1 = 1f / (float) (1 << this.fp);
            int num2 = h - 1;
            int index1 = w * h - 1;
            int index2 = this.srcBlk.offset + (h - 1) * this.srcBlk.scanw + w - 1;
            for (; num2 >= 0; --num2)
            {
              int num3 = index1 - w;
              while (index1 > num3)
              {
                numArray[index1] = (float) data[index2] * num1;
                --index1;
                --index2;
              }
              index2 -= this.srcBlk.scanw - w;
            }
            return blk;
          }
          int num4 = h - 1;
          int index3 = w * h - 1;
          int index4 = this.srcBlk.offset + (h - 1) * this.srcBlk.scanw + w - 1;
          for (; num4 >= 0; --num4)
          {
            int num5 = index3 - w;
            while (index3 > num5)
            {
              numArray[index3] = (float) data[index4];
              --index3;
              --index4;
            }
            index4 -= this.srcBlk.scanw - w;
          }
          return blk;
        }
        int[] numArray1 = (int[]) blk.Data;
        if (numArray1 == null || numArray1.Length < w * h)
        {
          numArray1 = new int[w * h];
          blk.Data = (object) numArray1;
        }
        blk.scanw = this.srcBlk.w;
        blk.offset = 0;
        blk.progressive = this.srcBlk.progressive;
        float[] data1 = (float[]) this.srcBlk.Data;
        if (this.fp != 0)
        {
          float num6 = (float) (1 << this.fp);
          int num7 = h - 1;
          int index5 = w * h - 1;
          int index6 = this.srcBlk.offset + (h - 1) * this.srcBlk.scanw + w - 1;
          for (; num7 >= 0; --num7)
          {
            int num8 = index5 - w;
            while (index5 > num8)
            {
              numArray1[index5] = (double) data1[index6] <= 0.0 ? (int) ((double) data1[index6] * (double) num6 - 0.5) : (int) ((double) data1[index6] * (double) num6 + 0.5);
              --index5;
              --index6;
            }
            index6 -= this.srcBlk.scanw - w;
          }
          return blk;
        }
        int num9 = h - 1;
        int index7 = w * h - 1;
        int index8 = this.srcBlk.offset + (h - 1) * this.srcBlk.scanw + w - 1;
        for (; num9 >= 0; --num9)
        {
          int num10 = index7 - w;
          while (index7 > num10)
          {
            numArray1[index7] = (double) data1[index8] <= 0.0 ? (int) ((double) data1[index8] - 0.5) : (int) ((double) data1[index8] + 0.5);
            --index7;
            --index8;
          }
          index8 -= this.srcBlk.scanw - w;
        }
        return blk;
      }

      public virtual int getFixedPoint(int c) => this.fp;

      public DataBlock getInternCompData(DataBlock blk, int c) => this.getData(blk, c, true);
    }
}
