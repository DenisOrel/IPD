// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.ObjectData
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Images.Metafiles;

internal class ObjectData
{
  private const int IndexMask = 255 /*0xFF*/;
  private Image m_bmp = (Image) new Bitmap(1, 1);
  private System.Drawing.Graphics m_graphics;
  private object[] m_objects;
  private object[] m_states;

  public ObjectData()
  {
    this.m_graphics = System.Drawing.Graphics.FromImage(this.m_bmp);
    this.m_objects = new object[256 /*0x0100*/];
    this.m_states = new object[256 /*0x0100*/];
  }

  public void Dispose() => this.DisposeObjects();

  private void DisposeObjects()
  {
    if (this.m_objects == null)
      return;
    int index = 0;
    for (int length = this.m_objects.Length; index < length; ++index)
    {
      if (this.m_objects[index] is IDisposable disposable)
      {
        disposable.Dispose();
        this.m_objects[index] = (object) null;
      }
    }
    this.m_objects = (object[]) null;
  }

  public Brush GetBrush(int index)
  {
    index &= (int) byte.MaxValue;
    return this.GetObject(index) as Brush;
  }

  public Font GetFont(int index)
  {
    index &= (int) byte.MaxValue;
    return this.GetObject(index) as Font;
  }

  public object GetObject(int index)
  {
    object obj = (object) null;
    index &= (int) byte.MaxValue;
    return index < 0 && index >= this.m_objects.Length ? obj : this.m_objects[index];
  }

  public Pen GetPen(int index)
  {
    index &= (int) byte.MaxValue;
    return this.GetObject(index) as Pen;
  }

  public object GetState(int index)
  {
    object obj = (object) null;
    index &= (int) byte.MaxValue;
    return index < 0 && index >= this.m_states.Length ? obj : this.m_states[index];
  }

  public void SetObject(int index, object obj)
  {
    index &= (int) byte.MaxValue;
    if (index < 0 || index >= this.m_objects.Length || obj == null)
      return;
    if (this.m_objects[index] is IDisposable disposable)
      disposable.Dispose();
    this.m_objects[index] = obj;
  }

  public void SetPen(int index, Pen pen)
  {
    index &= (int) byte.MaxValue;
    this.SetObject(index, (object) pen);
  }

  public void SetState(int index, object state)
  {
    index &= (int) byte.MaxValue;
    if (index < 0 || index >= this.m_states.Length || state == null)
      return;
    if (this.m_states[index] is IDisposable state1)
      state1.Dispose();
    this.m_states[index] = state;
  }

  public System.Drawing.Graphics Graphics => this.m_graphics;
}
