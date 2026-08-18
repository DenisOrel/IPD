// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DProjection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class Pdf3DProjection : IPdfWrapper
    {
      private Pdf3DProjectionClipStyle m_ClipStyle;
      private PdfDictionary m_dictionary;
      private float m_farClipDistance;
      private float m_fieldOfView;
      private float m_nearClipDistance;
      private Pdf3DProjectionOrthoScaleMode m_OrthoScalemode;
      private float m_scaling;
      private Pdf3DProjectionType m_Type;

      public Pdf3DProjection()
      {
        this.m_dictionary = new PdfDictionary();
        this.Initialize();
      }

      public Pdf3DProjection(Pdf3DProjectionType type)
        : this()
      {
        this.m_Type = type;
      }

      private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

      protected virtual void Initialize()
      {
        this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
        this.m_dictionary.SetProperty("Type", (IPdfPrimitive) new PdfName("P"));
      }

      protected virtual void Save()
      {
        if (this.ClipStyle == Pdf3DProjectionClipStyle.ExplicitNearFar)
          this.Dictionary.SetProperty("CS", (IPdfPrimitive) new PdfName("XNF"));
        else
          this.Dictionary.SetProperty("CS", (IPdfPrimitive) new PdfName("ANF"));
        if (this.ClipStyle == Pdf3DProjectionClipStyle.ExplicitNearFar && (double) this.m_farClipDistance >= 0.0)
          this.Dictionary.SetProperty("F", (IPdfPrimitive) new PdfNumber(this.m_farClipDistance));
        if (this.m_Type == Pdf3DProjectionType.Perspective && (double) this.m_nearClipDistance >= 0.0)
          this.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfNumber(this.m_nearClipDistance));
        else if (this.m_Type == Pdf3DProjectionType.Orthographic && this.m_ClipStyle == Pdf3DProjectionClipStyle.ExplicitNearFar && (double) this.m_nearClipDistance >= 0.0)
          this.Dictionary.SetProperty("N", (IPdfPrimitive) new PdfNumber(this.m_nearClipDistance));
        if (this.m_Type == Pdf3DProjectionType.Perspective)
          this.Dictionary.SetProperty("FOV", (IPdfPrimitive) new PdfNumber(this.m_fieldOfView));
        if ((double) this.m_scaling > 0.0)
        {
          if (this.m_Type == Pdf3DProjectionType.Perspective)
            this.Dictionary.SetProperty("PS", (IPdfPrimitive) new PdfNumber(this.m_scaling));
          if (this.m_Type == Pdf3DProjectionType.Orthographic)
            this.Dictionary.SetProperty("OS", (IPdfPrimitive) new PdfNumber(this.m_scaling));
        }
        if (this.m_OrthoScalemode == Pdf3DProjectionOrthoScaleMode.Absolute)
          this.Dictionary.SetProperty("OB", (IPdfPrimitive) new PdfName("Absolute"));
        else if (this.m_OrthoScalemode == Pdf3DProjectionOrthoScaleMode.Height)
          this.Dictionary.SetProperty("OB", (IPdfPrimitive) new PdfName("H"));
        else if (this.m_OrthoScalemode == Pdf3DProjectionOrthoScaleMode.Max)
          this.Dictionary.SetProperty("OB", (IPdfPrimitive) new PdfName("Max"));
        else if (this.m_OrthoScalemode == Pdf3DProjectionOrthoScaleMode.Min)
        {
          this.Dictionary.SetProperty("OB", (IPdfPrimitive) new PdfName("Min"));
        }
        else
        {
          if (this.m_OrthoScalemode != Pdf3DProjectionOrthoScaleMode.Width)
            return;
          this.Dictionary.SetProperty("OB", (IPdfPrimitive) new PdfName("W"));
        }
      }

      public Pdf3DProjectionClipStyle ClipStyle
      {
        get => this.m_ClipStyle;
        set => this.m_ClipStyle = value;
      }

      internal PdfDictionary Dictionary => this.m_dictionary;

      public float FarClipDistance
      {
        get => this.m_farClipDistance;
        set => this.m_farClipDistance = value;
      }

      public float FieldOfView
      {
        get => this.m_fieldOfView;
        set => this.m_fieldOfView = value;
      }

      public float NearClipDistance
      {
        get => this.m_nearClipDistance;
        set => this.m_nearClipDistance = value;
      }

      public Pdf3DProjectionOrthoScaleMode OrthoScaleMode
      {
        get => this.m_OrthoScalemode;
        set => this.m_OrthoScalemode = value;
      }

      public Pdf3DProjectionType ProjectionType
      {
        get => this.m_Type;
        set => this.m_Type = value;
      }

      public float Scaling
      {
        get => this.m_scaling;
        set => this.m_scaling = value;
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
