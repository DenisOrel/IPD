// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DView
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class Pdf3DView : IPdfWrapper
{
  private Pdf3DBackground m_3dBackground;
  private Pdf3DCrossSectionCollection m_3dCrossSectionCollection;
  private Pdf3DLighting m_3dLighting;
  private Pdf3DNodeCollection m_3dNodeCollection;
  private Pdf3DProjection m_3dProjection;
  private Pdf3DRendermode m_3dRendermode;
  private float m_centreOfOrbit;
  private float[] m_centretoWorldMatrix;
  private PdfDictionary m_dictionary = new PdfDictionary();
  private string m_externalName;
  private string m_internalName;
  private bool m_resetNodesState;
  private string m_viewNodeName;

  public Pdf3DView()
  {
    this.Initialize();
    this.m_3dNodeCollection = new Pdf3DNodeCollection();
    this.m_3dCrossSectionCollection = new Pdf3DCrossSectionCollection();
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

  protected virtual void Initialize()
  {
    this.m_dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.Dictionary["Type"] = (IPdfPrimitive) new PdfName("3DView");
  }

  protected virtual void Save()
  {
    if (this.m_externalName != null && this.m_externalName.Length > 0)
      this.Dictionary["XN"] = (IPdfPrimitive) new PdfString(this.m_externalName);
    if (this.m_internalName != null && this.m_internalName.Length > 0)
      this.Dictionary["IN"] = (IPdfPrimitive) new PdfString(this.m_internalName);
    if (this.m_viewNodeName != null && this.m_viewNodeName.Length > 0)
    {
      this.Dictionary["MS"] = (IPdfPrimitive) new PdfName("U3D");
      this.Dictionary["U3DPath"] = (IPdfPrimitive) new PdfName(this.m_internalName);
    }
    else
    {
      if (this.m_centretoWorldMatrix == null)
        throw new ArgumentNullException("CameraToWorldMatrix", "Either ViewNodeName or CameraToWorldMatrix properties must be specified.");
      this.Dictionary["MS"] = (IPdfPrimitive) new PdfName("M");
      PdfArray array = new PdfArray();
      for (int index = 0; index < this.m_centretoWorldMatrix.Length; ++index)
        array.Insert(index, (IPdfPrimitive) new PdfNumber(this.m_centretoWorldMatrix[index]));
      this.Dictionary.SetProperty("C2W", (IPdfPrimitive) new PdfArray(array));
    }
    if ((double) this.m_centreOfOrbit > 0.0)
      this.Dictionary.SetProperty("CO", (IPdfPrimitive) new PdfNumber(this.m_centreOfOrbit));
    if (this.m_3dProjection != null)
      this.Dictionary["P"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_3dProjection);
    if (this.m_3dBackground != null)
      this.Dictionary["BG"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_3dBackground);
    if (this.m_3dRendermode != null)
      this.Dictionary["RM"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_3dRendermode);
    if (this.m_3dLighting != null)
      this.Dictionary["LS"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_3dLighting);
    if (this.m_3dCrossSectionCollection != null && this.m_3dCrossSectionCollection.Count > 0)
    {
      PdfArray array = new PdfArray();
      for (int index = 0; index < this.m_3dCrossSectionCollection.Count; ++index)
        array.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_3dCrossSectionCollection[index]));
      this.Dictionary.SetProperty("SA", (IPdfPrimitive) new PdfArray(array));
    }
    if (this.m_3dNodeCollection != null && this.m_3dNodeCollection.Count > 0)
    {
      PdfArray array = new PdfArray();
      for (int index = 0; index < this.m_3dNodeCollection.Count; ++index)
        array.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_3dNodeCollection[index]));
      this.Dictionary.SetProperty("NA", (IPdfPrimitive) new PdfArray(array));
    }
    this.Dictionary.SetProperty("NR", (IPdfPrimitive) new PdfBoolean(this.m_resetNodesState));
  }

  public Pdf3DBackground Background
  {
    get => this.m_3dBackground;
    set => this.m_3dBackground = value;
  }

  public float[] CameraToWorldMatrix
  {
    get => this.m_centretoWorldMatrix;
    set
    {
      this.m_centretoWorldMatrix = value;
      if (this.m_centretoWorldMatrix != null && this.m_centretoWorldMatrix.Length < 12)
        throw new ArgumentOutOfRangeException("CameraToWorldMatrix.Length", "CameraToWorldMatrix array must have at least 12 elements.");
    }
  }

  public float CenterOfOrbit
  {
    get => this.m_centreOfOrbit;
    set => this.m_centreOfOrbit = value;
  }

  public Pdf3DCrossSectionCollection CrossSections => this.m_3dCrossSectionCollection;

  internal PdfDictionary Dictionary => this.m_dictionary;

  public string ExternalName
  {
    get => this.m_externalName;
    set => this.m_externalName = value;
  }

  public string InternalName
  {
    get => this.m_internalName;
    set => this.m_internalName = value;
  }

  public Pdf3DLighting LightingScheme
  {
    get => this.m_3dLighting;
    set => this.m_3dLighting = value;
  }

  public Pdf3DNodeCollection Nodes => this.m_3dNodeCollection;

  public Pdf3DProjection Projection
  {
    get => this.m_3dProjection;
    set => this.m_3dProjection = value;
  }

  public Pdf3DRendermode RenderMode
  {
    get => this.m_3dRendermode;
    set => this.m_3dRendermode = value;
  }

  public bool ResetNodesState
  {
    get => this.m_resetNodesState;
    set => this.m_resetNodesState = value;
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;

  public string ViewNodeName
  {
    get => this.m_viewNodeName;
    set => this.m_viewNodeName = value;
  }
}
