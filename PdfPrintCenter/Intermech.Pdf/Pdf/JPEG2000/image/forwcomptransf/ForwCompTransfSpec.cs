// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.forwcomptransf.ForwCompTransfSpec
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.util;
using Syncfusion.Pdf.JPEG2000.wavelet.analysis;
using System;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.image.forwcomptransf;

internal class ForwCompTransfSpec : CompTransfSpec
{
  internal ForwCompTransfSpec(int nt, int nc, byte type, AnWTFilterSpec wfs, JPXParameters pl)
    : base(nt, nc, type)
  {
    string parameter = pl.getParameter("Mct");
    if (parameter == null)
    {
      if (nc < 3)
        this.setDefault((object) "none");
      else if (pl.getBooleanParameter("lossless"))
      {
        this.setDefault((object) "rct");
      }
      else
      {
        int[] numArray1 = new int[this.nComp];
        for (int c = 0; c < 3; ++c)
        {
          AnWTFilter[][] compDef = (AnWTFilter[][]) wfs.getCompDef(c);
          numArray1[c] = compDef[0][0].FilterType;
        }
        bool flag1 = false;
        for (int index = 1; index < 3; ++index)
        {
          if (numArray1[index] != numArray1[0])
            flag1 = true;
        }
        if (flag1)
          this.setDefault((object) "none");
        else if (((AnWTFilter[][]) wfs.getCompDef(0))[0][0].FilterType == 0)
          this.setDefault((object) "ict");
        else
          this.setDefault((object) "rct");
        for (int t = 0; t < nt; ++t)
        {
          int[] numArray2 = new int[this.nComp];
          for (int c = 0; c < 3; ++c)
          {
            AnWTFilter[][] tileCompVal = (AnWTFilter[][]) wfs.getTileCompVal(t, c);
            numArray2[c] = tileCompVal[0][0].FilterType;
          }
          bool flag2 = false;
          for (int index = 1; index < this.nComp; ++index)
          {
            if (numArray2[index] != numArray2[0])
              flag2 = true;
          }
          if (flag2)
            this.setTileDef(t, (object) "none");
          else if (((AnWTFilter[][]) wfs.getTileCompVal(t, 0))[0][0].FilterType == 0)
            this.setTileDef(t, (object) "ict");
          else
            this.setTileDef(t, (object) "rct");
        }
      }
    }
    else
    {
      SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(parameter);
      byte num = 0;
      bool[] flagArray = (bool[]) null;
      while (tokenizer.HasMoreTokens())
      {
        string word = tokenizer.NextToken();
        switch (word[0])
        {
          case 'c':
            throw new ArgumentException("Component specific  parameters not allowed with '-Mct' option");
          case 't':
            flagArray = ModuleSpec.parseIdx(word, this.nTiles);
            num = num != (byte) 1 ? (byte) 2 : (byte) 3;
            continue;
          default:
            switch (word)
            {
              case "off":
                switch (num)
                {
                  case 0:
                    this.setDefault((object) "none");
                    break;
                  case 2:
                    for (int t = flagArray.Length - 1; t >= 0; --t)
                    {
                      if (flagArray[t])
                        this.setTileDef(t, (object) "none");
                    }
                    break;
                }
                break;
              case "on":
                if (nc < 3)
                  throw new ArgumentException("Cannot use component transformation on a image with less than three components");
                switch (num)
                {
                  case 0:
                    this.setDefault((object) "rct");
                    break;
                  case 2:
                    for (int t = flagArray.Length - 1; t >= 0; --t)
                    {
                      if (flagArray[t])
                      {
                        if (this.getFilterType(t, wfs) == 1)
                          this.setTileDef(t, (object) "rct");
                        else
                          this.setTileDef(t, (object) "ict");
                      }
                    }
                    break;
                }
                break;
              default:
                throw new ArgumentException("Default parameter of option Mct not recognized: " + parameter);
            }
            num = (byte) 0;
            flagArray = (bool[]) null;
            continue;
        }
      }
      if (this.getDefault() == null)
      {
        this.setDefault((object) "none");
        for (int t = 0; t < nt; ++t)
        {
          if (!this.isTileSpecified(t))
          {
            int[] numArray = new int[this.nComp];
            for (int c = 0; c < 3; ++c)
            {
              AnWTFilter[][] tileCompVal = (AnWTFilter[][]) wfs.getTileCompVal(t, c);
              numArray[c] = tileCompVal[0][0].FilterType;
            }
            bool flag = false;
            for (int index = 1; index < this.nComp; ++index)
            {
              if (numArray[index] != numArray[0])
                flag = true;
            }
            if (flag)
              this.setTileDef(t, (object) "none");
            else if (((AnWTFilter[][]) wfs.getTileCompVal(t, 0))[0][0].FilterType == 0)
              this.setTileDef(t, (object) "ict");
            else
              this.setTileDef(t, (object) "rct");
          }
        }
      }
      for (int t = nt - 1; t >= 0; --t)
      {
        if (!((string) this.getTileDef(t)).Equals("none"))
        {
          if (((string) this.getTileDef(t)).Equals("rct"))
          {
            switch (this.getFilterType(t, wfs))
            {
              case 0:
                if (this.isTileSpecified(t))
                  throw new ArgumentException("Cannot use RCT with 9x7 filter in tile " + (object) t);
                this.setTileDef(t, (object) "ict");
                continue;
              case 1:
                continue;
              default:
                throw new ArgumentException("Default filter is not JPEG 2000 part I compliant");
            }
          }
          else
          {
            switch (this.getFilterType(t, wfs))
            {
              case 0:
                continue;
              case 1:
                if (this.isTileSpecified(t))
                  throw new ArgumentException("Cannot use ICT with filter 5x3 in tile " + (object) t);
                this.setTileDef(t, (object) "rct");
                continue;
              default:
                throw new ArgumentException("Default filter is not JPEG 2000 part I compliant");
            }
          }
        }
      }
    }
  }

  private int getFilterType(int t, AnWTFilterSpec wfs)
  {
    int[] numArray = new int[this.nComp];
    for (int c = 0; c < this.nComp; ++c)
    {
      AnWTFilter[][] anWtFilterArray = t != -1 ? (AnWTFilter[][]) wfs.getTileCompVal(t, c) : (AnWTFilter[][]) wfs.getCompDef(c);
      numArray[c] = anWtFilterArray[0][0].FilterType;
    }
    bool flag = false;
    for (int index = 1; index < this.nComp; ++index)
    {
      if (numArray[index] != numArray[0])
        flag = true;
    }
    if (flag)
      throw new ArgumentException("Can not use component transformation when components do not use the same filters");
    return numArray[0];
  }
}
