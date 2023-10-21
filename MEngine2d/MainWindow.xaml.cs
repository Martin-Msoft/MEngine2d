using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace MEngine2d
{
    public partial class MainWindow : Window
    {
        private WriteableBitmap _writeableBitmap;
        private IntPtr _buffer;
        private List<AnimatedEntity> _entities;


        public MainWindow()
        {
            InitializeComponent();
            _writeableBitmap = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgra32, null);
            _buffer = _writeableBitmap.BackBuffer;
            BitmapImage spriteSheet = new BitmapImage(new Uri("pack://application:,,,/PixelCrawler/Heroes/Knight/Idle/Idle-Sheet.png")
);


            _entities = new List<AnimatedEntity>();
            AnimatedEntity player = new AnimatedEntity(spriteSheet, 32, 32);
            player.X = 100; // example X coordinate
            player.Y = 100; // example Y coordinate
            _entities.Add(player);


            CompositionTarget.Rendering += GameLoop;
        }


        private void GameLoop(object sender, EventArgs e)
        {
            foreach(var entity in _entities)
            {
                entity.Update();
                entity.Animate();
                entity.Draw(_writeableBitmap, entity.X, entity.Y); // Assuming X and Y properties exist
            }

            // Render to the window
            myImage.Source = _writeableBitmap;
        }

    }
}
