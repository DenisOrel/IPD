
// Type: Intermech.Docking.Rendering.ITabControlRenderer
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Docking.Rendering;

public interface ITabControlRenderer
{
  void DrawFakeTabControlBackgroundExtension(Graphics graphics, Rectangle bounds, Color backColor);

  void DrawTabControlBackground(Graphics graphics, Rectangle bounds, Color backColor, bool client);

  void DrawTabControlButton(
    Graphics graphics,
    Rectangle bounds,
    ButtonType buttonType,
    DrawItemState state);

  void DrawTabControlTab(
    Graphics graphics,
    Rectangle bounds,
    Image image,
    string text,
    Font font,
    Color backColor,
    Color foreColor,
    DrawItemState state,
    bool drawSeparator,
    Intermech.Docking.TabAlignment tabAlignment,
    bool flat);

  void DrawTabControlTabStripBackground(
    Graphics graphics,
    Rectangle bounds,
    Color backColor,
    Intermech.Docking.TabAlignment tabAlignment,
    bool flat);

  void FinishRenderSession();

  Size MeasureTabControlTab(
    Graphics graphics,
    Image image,
    string text,
    Font font,
    DrawItemState state);

  void StartRenderSession();

  bool ShouldDrawTabControlBackground { get; set; }

  Size TabControlPadding { get; }

  int TabControlTabExtra { get; }

  int TabControlTabHeight { get; }

  int TabControlTabStripHeight { get; }
}
