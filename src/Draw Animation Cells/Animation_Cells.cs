// Title: Animation Cells
// Author: MechPen & BoltBait
// Submenu: Render
// Name: Animation Cells
// URL: http://www.BoltBait.com/pdn
// Keywords: checkerboard|frame|animation
// Desc: Draw a checkerboard pattern for making sprite animations
#region UICode
IntSliderControl gridWidth = 10; // [1,1000] Section Width
IntSliderControl gridHeight = 10; // [1,1000] Section Height
ColorWheelControl Color1 = ColorBgra.FromBgra(219, 112, 147, 255); // [MediumPurple?] Primary color
CheckboxControl dontUseColor2 = false; // Use source canvas for secondary color
ColorWheelControl Color2 = ColorBgra.FromBgra(180, 105, 255, 255); // [HotPink?] {!dontUseColor2} Secondary color
CheckboxControl drawAnchor = true; // Draw Anchor Pixel
IntSliderControl AnchorX = 5; // [0,1000] {drawAnchor} Achor x Pos
IntSliderControl AnchorY = 5; // [0,1000] {drawAnchor} Achor y Pos
#endregion

unsafe void Render(Surface dst, Surface src, Rectangle rect)
{
    Rectangle selection = this.EnvironmentParameters.SelectionBounds;
    int startX = selection.Left;
    int startY = selection.Top;
    bool Odd = false;
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            ColorBgra CurrentPixel = src[x,y];
            int xOff = x - startX;
            int yOff = y - startY;

            // We're making a checker board
            Odd = true;
            if (((xOff / gridWidth) % 2) == 0) Odd = false;
            if (((yOff / gridHeight) % 2) == 0) Odd = !Odd;
            if (Odd)
            {
                CurrentPixel = Color1;
            }
            else
            {
                if (!dontUseColor2)
                {
                    CurrentPixel = Color2;
                }
            }

            if (drawAnchor)
            {
                if ( ((xOff - AnchorX) % gridWidth) == 0 && ((yOff - AnchorY) % gridHeight) == 0)
                {
                    CurrentPixel = ColorBgra.FromBgra(0, 0, 0, 255);
                }

            }

            dst[x,y] = CurrentPixel;
        }
    }
}