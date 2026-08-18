// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfSeparationColorSpace
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Functions;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.ColorSpace;

public class PdfSeparationColorSpace : PdfColorSpaces, IPdfWrapper
{
  private PdfColorSpaces m_alterantecolorspaces = (PdfColorSpaces) new PdfDeviceColorSpace(PdfColorSpace.CMYK);
  private string m_colorant;
  private PdfFunction m_function;
  private PdfStream m_stream = new PdfStream();

  public PdfSeparationColorSpace()
  {
    this.m_stream.Compress = true;
    this.m_stream.SetProperty("Filter", (IPdfPrimitive) new PdfName("FlateDecode"));
    this.m_stream.BeginSave += new SavePdfPrimitiveEventHandler(this.Stream_BeginSave);
    this.Initialize();
  }

  private PdfArray CreateInternals()
  {
    PdfArray internals = new PdfArray();
    if (internals == null)
      return internals;
    PdfName element1 = new PdfName("Separation");
    internals.Add((IPdfPrimitive) element1);
    if (this.m_colorant != null)
    {
      PdfName element2 = new PdfName(this.m_colorant);
      internals.Add((IPdfPrimitive) element2);
    }
    else
    {
      PdfName element3 = new PdfName("All");
      internals.Add((IPdfPrimitive) element3);
    }
    PdfColorSpace deviceColorSpaceType;
    if (this.m_alterantecolorspaces != null)
    {
      if (this.m_alterantecolorspaces is PdfCalGrayColorSpace)
      {
        PdfName pdfName = new PdfName("CalGray");
        PdfReferenceHolder element4 = new PdfReferenceHolder((IPdfWrapper) this.m_alterantecolorspaces);
        internals.Add((IPdfPrimitive) element4);
      }
      else if (this.m_alterantecolorspaces is PdfCalRGBColorSpace)
      {
        PdfName pdfName = new PdfName("CalRGB");
        PdfReferenceHolder element5 = new PdfReferenceHolder((IPdfWrapper) this.m_alterantecolorspaces);
        internals.Add((IPdfPrimitive) element5);
      }
      else if (this.m_alterantecolorspaces is PdfLabColorSpace)
      {
        PdfName pdfName = new PdfName("Lab");
        PdfReferenceHolder element6 = new PdfReferenceHolder((IPdfWrapper) this.m_alterantecolorspaces);
        internals.Add((IPdfPrimitive) element6);
      }
      else if (this.m_alterantecolorspaces is PdfDeviceColorSpace)
      {
        deviceColorSpaceType = (this.m_alterantecolorspaces as PdfDeviceColorSpace).DeviceColorSpaceType;
        switch (deviceColorSpaceType.ToString())
        {
          case "RGB":
            PdfName element7 = new PdfName("DeviceRGB");
            internals.Add((IPdfPrimitive) element7);
            break;
          case "CMYK":
            PdfName element8 = new PdfName("DeviceCMYK");
            internals.Add((IPdfPrimitive) element8);
            break;
          case "GrayScale":
            PdfName element9 = new PdfName("DeviceGray");
            internals.Add((IPdfPrimitive) element9);
            break;
        }
      }
    }
    else
    {
      PdfName element10 = new PdfName("DeviceCMYK");
      internals.Add((IPdfPrimitive) element10);
    }
    if (this.m_function != null)
    {
      if (this.m_alterantecolorspaces is PdfCalGrayColorSpace)
      {
        PdfExponentialInterpolationFunction function = this.m_function as PdfExponentialInterpolationFunction;
        function.Dictionary.SetProperty("FunctionType", (IPdfPrimitive) new PdfNumber(2));
        double[] array1 = new double[2]{ 0.0, 1.0 };
        function.Dictionary.SetProperty("Domain", (IPdfPrimitive) new PdfArray(array1));
        double[] array2 = new double[2]{ 0.0, 1.0 };
        function.Dictionary.SetProperty("Range", (IPdfPrimitive) new PdfArray(array2));
        double[] array3 = new double[1];
        function.Dictionary.SetProperty("C0", (IPdfPrimitive) new PdfArray(array3));
        if (function.C1.Length != 1)
          throw new ArgumentOutOfRangeException();
        function.Dictionary.SetProperty("C1", (IPdfPrimitive) new PdfArray(function.C1));
        function.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfNumber(1));
        PdfReferenceHolder element11 = new PdfReferenceHolder((IPdfWrapper) function);
        internals.Add((IPdfPrimitive) element11);
        return internals;
      }
      if (this.m_alterantecolorspaces is PdfCalRGBColorSpace)
      {
        PdfExponentialInterpolationFunction function = this.m_function as PdfExponentialInterpolationFunction;
        function.Dictionary.SetProperty("FunctionType", (IPdfPrimitive) new PdfNumber(2));
        double[] array4 = new double[2]{ 0.0, 1.0 };
        function.Dictionary.SetProperty("Domain", (IPdfPrimitive) new PdfArray(array4));
        function.Dictionary.SetProperty("Range", (IPdfPrimitive) new PdfArray(new double[6]
        {
          0.0,
          1.0,
          0.0,
          1.0,
          0.0,
          1.0
        }));
        double[] array5 = new double[3];
        function.Dictionary.SetProperty("C0", (IPdfPrimitive) new PdfArray(array5));
        if (function.C1.Length != 3)
          throw new ArgumentOutOfRangeException();
        function.Dictionary.SetProperty("C1", (IPdfPrimitive) new PdfArray(function.C1));
        function.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfNumber(1));
        PdfReferenceHolder element12 = new PdfReferenceHolder((IPdfWrapper) function);
        internals.Add((IPdfPrimitive) element12);
        return internals;
      }
      if (this.m_alterantecolorspaces is PdfLabColorSpace)
      {
        PdfExponentialInterpolationFunction function = this.m_function as PdfExponentialInterpolationFunction;
        function.Dictionary.SetProperty("FunctionType", (IPdfPrimitive) new PdfNumber(2));
        double[] array6 = new double[2]{ 0.0, 1.0 };
        function.Dictionary.SetProperty("Domain", (IPdfPrimitive) new PdfArray(array6));
        function.Dictionary.SetProperty("Range", (IPdfPrimitive) new PdfArray(new double[6]
        {
          0.0,
          100.0,
          0.0,
          100.0,
          0.0,
          100.0
        }));
        double[] array7 = new double[3];
        function.Dictionary.SetProperty("C0", (IPdfPrimitive) new PdfArray(array7));
        if (function.C1.Length != 3)
          throw new ArgumentOutOfRangeException();
        function.Dictionary.SetProperty("C1", (IPdfPrimitive) new PdfArray(function.C1));
        function.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfNumber(1));
        PdfReferenceHolder element13 = new PdfReferenceHolder((IPdfWrapper) function);
        internals.Add((IPdfPrimitive) element13);
        return internals;
      }
      if (this.m_alterantecolorspaces is PdfDeviceColorSpace)
      {
        deviceColorSpaceType = (this.m_alterantecolorspaces as PdfDeviceColorSpace).DeviceColorSpaceType;
        switch (deviceColorSpaceType.ToString())
        {
          case "RGB":
            PdfExponentialInterpolationFunction function1 = this.m_function as PdfExponentialInterpolationFunction;
            function1.Dictionary.SetProperty("FunctionType", (IPdfPrimitive) new PdfNumber(2));
            double[] array8 = new double[2]{ 0.0, 1.0 };
            function1.Dictionary.SetProperty("Domain", (IPdfPrimitive) new PdfArray(array8));
            function1.Dictionary.SetProperty("Range", (IPdfPrimitive) new PdfArray(new double[6]
            {
              0.0,
              1.0,
              0.0,
              1.0,
              0.0,
              1.0
            }));
            double[] array9 = new double[3];
            function1.Dictionary.SetProperty("C0", (IPdfPrimitive) new PdfArray(array9));
            if (function1.C1.Length != 3)
              throw new ArgumentOutOfRangeException();
            function1.Dictionary.SetProperty("C1", (IPdfPrimitive) new PdfArray(function1.C1));
            function1.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfNumber(1));
            PdfReferenceHolder element14 = new PdfReferenceHolder((IPdfWrapper) function1);
            internals.Add((IPdfPrimitive) element14);
            return internals;
          case "CMYK":
            PdfExponentialInterpolationFunction function2 = this.m_function as PdfExponentialInterpolationFunction;
            function2.Dictionary.SetProperty("FunctionType", (IPdfPrimitive) new PdfNumber(2));
            function2.Dictionary.SetProperty("C0", (IPdfPrimitive) new PdfArray(function2.C0));
            if (function2.C1.Length != 4)
              throw new ArgumentOutOfRangeException();
            function2.Dictionary.SetProperty("C1", (IPdfPrimitive) new PdfArray(function2.C1));
            function2.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfNumber(1));
            PdfReferenceHolder element15 = new PdfReferenceHolder((IPdfWrapper) function2);
            internals.Add((IPdfPrimitive) element15);
            return internals;
          case "GrayScale":
            PdfExponentialInterpolationFunction function3 = this.m_function as PdfExponentialInterpolationFunction;
            function3.Dictionary.SetProperty("FunctionType", (IPdfPrimitive) new PdfNumber(2));
            double[] array10 = new double[2]{ 0.0, 1.0 };
            function3.Dictionary.SetProperty("Domain", (IPdfPrimitive) new PdfArray(array10));
            double[] array11 = new double[2]{ 0.0, 1.0 };
            function3.Dictionary.SetProperty("Range", (IPdfPrimitive) new PdfArray(array11));
            double[] array12 = new double[1];
            function3.Dictionary.SetProperty("C0", (IPdfPrimitive) new PdfArray(array12));
            if (function3.C1.Length != 1)
              throw new ArgumentOutOfRangeException();
            function3.Dictionary.SetProperty("C1", (IPdfPrimitive) new PdfArray(function3.C1));
            function3.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfNumber(1));
            PdfReferenceHolder element16 = new PdfReferenceHolder((IPdfWrapper) function3);
            internals.Add((IPdfPrimitive) element16);
            break;
        }
      }
      return internals;
    }
    float[] array13 = new float[2]{ 0.0f, 1f };
    float[] array14 = new float[8]
    {
      0.0f,
      1f,
      0.0f,
      1f,
      0.0f,
      1f,
      0.0f,
      1f
    };
    this.m_stream.SetProperty("FunctionType", (IPdfPrimitive) new PdfNumber(4));
    this.m_stream.SetProperty("Domain", (IPdfPrimitive) new PdfArray(array13));
    this.m_stream.SetProperty("Range", (IPdfPrimitive) new PdfArray(array14));
    return internals;
  }

  public byte[] GetProfileData() => new byte[0];

  private void Initialize()
  {
    lock (PdfColorSpaces.s_syncObject)
    {
      IPdfCache pdfCache = PdfDocument.Cache.Search((IPdfCache) this);
      ((IPdfCache) this).SetInternals(pdfCache != null ? pdfCache.GetInternals() : (IPdfPrimitive) this.CreateInternals());
    }
  }

  protected void Save()
  {
    byte[] profileData = this.GetProfileData();
    this.m_stream.Clear();
    this.m_stream.InternalStream.Write(profileData, 0, profileData.Length);
  }

  private void Stream_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

  public PdfColorSpaces AlternateColorSpaces
  {
    get => this.m_alterantecolorspaces;
    set
    {
      this.m_alterantecolorspaces = value;
      this.Initialize();
    }
  }

  public string Colorant
  {
    get => this.m_colorant;
    set
    {
      this.m_colorant = value;
      this.Initialize();
    }
  }

  public PdfFunction TintTransform
  {
    get => this.m_function;
    set
    {
      this.m_function = value;
      this.Initialize();
    }
  }
}
