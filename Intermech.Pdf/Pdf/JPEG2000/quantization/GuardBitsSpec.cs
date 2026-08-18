// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.quantization.GuardBitsSpec
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.JPEG2000.util;
using System;


namespace Syncfusion.Pdf.JPEG2000.quantization
{
    internal class GuardBitsSpec : ModuleSpec
    {
      public GuardBitsSpec(int nt, int nc, byte type)
        : base(nt, nc, type)
      {
      }

      internal GuardBitsSpec(int nt, int nc, byte type, JPXParameters pl)
        : base(nt, nc, type)
      {
        SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(pl.getParameter("Qguard_bits") ?? throw new ArgumentException("Qguard_bits option not specified"));
        byte num1 = 0;
        bool[] flagArray1 = (bool[]) null;
        bool[] flagArray2 = (bool[]) null;
        while (tokenizer.HasMoreTokens())
        {
          string lower = tokenizer.NextToken().ToLower();
          switch (lower[0])
          {
            case 'c':
              flagArray2 = ModuleSpec.parseIdx(lower, this.nComp);
              num1 = num1 != (byte) 2 ? (byte) 1 : (byte) 3;
              continue;
            case 't':
              flagArray1 = ModuleSpec.parseIdx(lower, this.nTiles);
              num1 = num1 != (byte) 1 ? (byte) 2 : (byte) 3;
              continue;
            default:
              int value_Renamed;
              try
              {
                value_Renamed = int.Parse(lower);
              }
              catch (FormatException ex)
              {
                throw new ArgumentException("Bad parameter for -Qguard_bits option : " + lower);
              }
              if ((double) value_Renamed <= 0.0)
                throw new ArgumentException("Guard bits value must be positive : " + (object) value_Renamed);
              switch (num1)
              {
                case 0:
                  this.setDefault((object) value_Renamed);
                  break;
                case 1:
                  for (int c = flagArray2.Length - 1; c >= 0; --c)
                  {
                    if (flagArray2[c])
                      this.setCompDef(c, (object) value_Renamed);
                  }
                  break;
                case 2:
                  for (int t = flagArray1.Length - 1; t >= 0; --t)
                  {
                    if (flagArray1[t])
                      this.setTileDef(t, (object) value_Renamed);
                  }
                  break;
                default:
                  for (int t = flagArray1.Length - 1; t >= 0; --t)
                  {
                    for (int c = flagArray2.Length - 1; c >= 0; --c)
                    {
                      if (flagArray1[t] && flagArray2[c])
                        this.setTileCompVal(t, c, (object) value_Renamed);
                    }
                  }
                  break;
              }
              num1 = (byte) 0;
              flagArray1 = (bool[]) null;
              flagArray2 = (bool[]) null;
              continue;
          }
        }
        if (this.getDefault() != null)
          return;
        int num2 = 0;
        for (int index1 = nt - 1; index1 >= 0; --index1)
        {
          for (int index2 = nc - 1; index2 >= 0; --index2)
          {
            if (this.specValType[index1][index2] == (byte) 0)
              ++num2;
          }
        }
        if (num2 != 0)
        {
          this.setDefault((object) int.Parse(pl.DefaultParameterList.getParameter("Qguard_bits")));
        }
        else
        {
          this.setDefault(this.getTileCompVal(0, 0));
          switch (this.specValType[0][0])
          {
            case 1:
              for (int index = nt - 1; index >= 0; --index)
              {
                if (this.specValType[index][0] == (byte) 1)
                  this.specValType[index][0] = (byte) 0;
              }
              this.compDef[0] = (object) null;
              break;
            case 2:
              for (int index = nc - 1; index >= 0; --index)
              {
                if (this.specValType[0][index] == (byte) 2)
                  this.specValType[0][index] = (byte) 0;
              }
              this.tileDef[0] = (object) null;
              break;
            case 3:
              this.specValType[0][0] = (byte) 0;
              this.tileCompVal[(object) "t0c0"] = (object) null;
              break;
          }
        }
      }
    }
}
