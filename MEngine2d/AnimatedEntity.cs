using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;

namespace MEngine2d
{
    public class AnimatedEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int currentFrame = 0;
        private List<BitmapSource> frames = new List<BitmapSource>();
        private int animationSpeed = 20;
        private int animationCounter = 0;

        public AnimatedEntity(BitmapImage spriteSheet, int frameWidth, int frameHeight)
        {
            // Extract individual frames from the sprite sheet
            for(int y = 0; y < spriteSheet.PixelHeight; y += frameHeight)
            {
                for(int x = 0; x < spriteSheet.PixelWidth; x += frameWidth)
                {
                    CroppedBitmap frame = new CroppedBitmap(spriteSheet, new Int32Rect(x, y, frameWidth, frameHeight));
                    frames.Add(frame);
                }
            }
        }

        public void Update()
        {
            // Update logic here, e.g., move the entity, handle collisions, etc.
        }

        public void Animate()
        {
            animationCounter++;
            if(animationCounter >= animationSpeed)
            {
                currentFrame = (currentFrame + 1) % frames.Count;
                animationCounter = 0;
            }
        }

        public void Draw(WriteableBitmap writeableBitmap, int x, int y)
        {
            Rect destRect = new Rect(x, y, frames[currentFrame].PixelWidth, frames[currentFrame].PixelHeight);

            // Clear the previous frame by filling the rectangle with a transparent color
            writeableBitmap.FillRectangle((int)destRect.X, (int)destRect.Y, (int)(destRect.X + destRect.Width), (int)(destRect.Y + destRect.Height), Colors.Transparent);

            // Draw the new frame
            WriteableBitmap writeableFrame = new WriteableBitmap(frames[currentFrame]);
            writeableBitmap.Blit(destRect, writeableFrame, new Rect(0, 0, writeableFrame.PixelWidth, writeableFrame.PixelHeight));
        }


    }

}
