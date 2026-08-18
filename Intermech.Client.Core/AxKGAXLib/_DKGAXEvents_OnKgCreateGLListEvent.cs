
// Type: AxKGAXLib._DKGAXEvents_OnKgCreateGLListEvent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using KGAXLib;


namespace AxKGAXLib;

public class _DKGAXEvents_OnKgCreateGLListEvent
{
  public GLObject glObj;
  public KDocument3DDrawMode drawMode;

  public _DKGAXEvents_OnKgCreateGLListEvent(GLObject glObj, KDocument3DDrawMode drawMode)
  {
    this.glObj = glObj;
    this.drawMode = drawMode;
  }
}
