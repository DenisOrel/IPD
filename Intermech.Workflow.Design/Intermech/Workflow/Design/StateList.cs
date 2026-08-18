// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.StateList
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.Workflow.Design;

public class StateList
{
  private static ImageList _checkImagelist;
  private static ImageList _radioImagelist;
  private static ImageList _plusMinusImagelist;

  private static void AddImage(ref ImageList imglist, StateImageKind kind, object state)
  {
    int width = 16 /*0x10*/;
    int height = 16 /*0x10*/;
    if (imglist == null)
    {
      imglist = new ImageList();
      imglist.ImageSize = new Size(width, height);
    }
    Bitmap bitmap = new Bitmap(width, height);
    Graphics g = Graphics.FromImage((Image) bitmap);
    if (kind == StateImageKind.RadioButton)
    {
      Size glyphSize = RadioButtonRenderer.GetGlyphSize(g, (RadioButtonState) state);
      Point glyphLocation = new Point(width / 2 - glyphSize.Width / 2, height / 2 - glyphSize.Height / 2);
      RadioButtonRenderer.DrawRadioButton(g, glyphLocation, (RadioButtonState) state);
    }
    else
    {
      Size glyphSize = CheckBoxRenderer.GetGlyphSize(g, (CheckBoxState) state);
      Point glyphLocation = new Point(width / 2 - glyphSize.Width / 2, height / 2 - glyphSize.Height / 2);
      CheckBoxRenderer.DrawCheckBox(g, glyphLocation, (CheckBoxState) state);
    }
    imglist.Images.Add((Image) bitmap);
  }

  public static ImageList ChecksImageList
  {
    get
    {
      StateList._checkImagelist = (ImageList) null;
      StateList.AddImage(ref StateList._checkImagelist, StateImageKind.CheckBox, (object) CheckBoxState.UncheckedNormal);
      StateList.AddImage(ref StateList._checkImagelist, StateImageKind.CheckBox, (object) CheckBoxState.CheckedNormal);
      return StateList._checkImagelist;
    }
  }

  public static ImageList RadioImageList
  {
    get
    {
      StateList._radioImagelist = (ImageList) null;
      StateList.AddImage(ref StateList._radioImagelist, StateImageKind.RadioButton, (object) ButtonState.Normal);
      StateList.AddImage(ref StateList._radioImagelist, StateImageKind.RadioButton, (object) ButtonState.Checked);
      return StateList._radioImagelist;
    }
  }

  private static Image LoadResImage(string name)
  {
    Assembly assembly = typeof (StateList).Assembly;
    Stream manifestResourceStream = assembly.GetManifestResourceStream(assembly.GetName().Name + name);
    if (manifestResourceStream == null)
      return (Image) null;
    Image image = Image.FromStream(manifestResourceStream);
    if (image is Bitmap)
      (image as Bitmap).MakeTransparent();
    return image;
  }

  public static ImageList PlusMinusImageList
  {
    get
    {
      if (StateList._plusMinusImagelist == null)
      {
        StateList._plusMinusImagelist = new ImageList();
        StateList._plusMinusImagelist.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
        Image image1 = StateList.LoadResImage(".img.tv.plus.gif");
        if (image1 != null)
          StateList._plusMinusImagelist.Images.Add(image1);
        Image image2 = StateList.LoadResImage(".img.tv.minus.gif");
        if (image2 != null)
          StateList._plusMinusImagelist.Images.Add(image2);
      }
      return StateList._plusMinusImagelist;
    }
  }
}
