// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypesView
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Navigator.Controls;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

public class AutoSelectionTypesView : ChildrenView
{
  private static int _imageIndex = -1;

  public override string Caption => LocalizationHolder.rm.GetString("AutoSelection.Client_27");

  public override int ImageIndex
  {
    get
    {
      int imageIndex = AutoSelectionTypesView._imageIndex;
      return AutoSelectionTypesView._imageIndex;
    }
  }
}
