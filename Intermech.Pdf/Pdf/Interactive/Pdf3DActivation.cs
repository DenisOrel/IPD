// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DActivation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class Pdf3DActivation : IPdfWrapper
    {
      private Pdf3DActivationMode m_activationMode;
      private Pdf3DActivationState m_activationState;
      private Pdf3DDeactivationMode m_deactivationMode;
      private Pdf3DDeactivationState m_deactivationState;
      private PdfDictionary m_dictionary = new PdfDictionary();
      private bool m_showToolbar = true;
      private bool m_showUI;

      public Pdf3DActivation() => this.Initialize();

      private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

      protected virtual void Initialize()
      {
        this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
      }

      protected virtual void Save()
      {
        if (this.m_activationMode == Pdf3DActivationMode.PageVisible)
          this.Dictionary.SetProperty("A", (IPdfPrimitive) new PdfName("PV"));
        else if (this.m_activationMode == Pdf3DActivationMode.PageOpen)
          this.Dictionary.SetProperty("A", (IPdfPrimitive) new PdfName("PO"));
        else if (this.m_activationMode == Pdf3DActivationMode.ExplicitActivation)
          this.Dictionary.SetProperty("A", (IPdfPrimitive) new PdfName("XA"));
        if (this.m_activationState == Pdf3DActivationState.Instantiated)
          this.Dictionary.SetProperty("AIS", (IPdfPrimitive) new PdfName("I"));
        else
          this.Dictionary.SetProperty("AIS", (IPdfPrimitive) new PdfName("L"));
        if (this.m_deactivationMode == Pdf3DDeactivationMode.PageClose)
          this.Dictionary.SetProperty("D", (IPdfPrimitive) new PdfName("PC"));
        else if (this.m_deactivationMode == Pdf3DDeactivationMode.PageInvisible)
          this.Dictionary.SetProperty("D", (IPdfPrimitive) new PdfName("PI"));
        else if (this.m_deactivationMode == Pdf3DDeactivationMode.ExplicitDeactivation)
          this.Dictionary.SetProperty("D", (IPdfPrimitive) new PdfName("XD"));
        if (this.m_deactivationState == Pdf3DDeactivationState.Uninstantiated)
          this.Dictionary.SetProperty("DIS", (IPdfPrimitive) new PdfName("U"));
        else if (this.m_deactivationState == Pdf3DDeactivationState.Instantiated)
          this.Dictionary.SetProperty("DIS", (IPdfPrimitive) new PdfName("I"));
        else if (this.m_deactivationState == Pdf3DDeactivationState.Live)
          this.Dictionary.SetProperty("DIS", (IPdfPrimitive) new PdfName("L"));
        this.Dictionary.SetProperty("TB", (IPdfPrimitive) new PdfBoolean(this.m_showToolbar));
        this.Dictionary.SetProperty("NP", (IPdfPrimitive) new PdfBoolean(this.m_showUI));
      }

      public Pdf3DActivationMode ActivationMode
      {
        get => this.m_activationMode;
        set => this.m_activationMode = value;
      }

      public Pdf3DActivationState ActivationState
      {
        get => this.m_activationState;
        set => this.m_activationState = value;
      }

      public Pdf3DDeactivationMode DeactivationMode
      {
        get => this.m_deactivationMode;
        set => this.m_deactivationMode = value;
      }

      public Pdf3DDeactivationState DeactivationState
      {
        get => this.m_deactivationState;
        set => this.m_deactivationState = value;
      }

      internal PdfDictionary Dictionary => this.m_dictionary;

      public bool ShowToolbar
      {
        get => this.m_showToolbar;
        set => this.m_showToolbar = value;
      }

      public bool ShowUI
      {
        get => this.m_showUI;
        set => this.m_showUI = value;
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
