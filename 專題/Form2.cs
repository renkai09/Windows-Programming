using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 專題
{
    public partial class Form2 : Form
    {
        private const int size = 3; // 3x3 拼圖
        private PictureBox[,] tiles = new PictureBox[size, size];
        private Bitmap originalImage;
        private Bitmap[,] tileImages = new Bitmap[size, size]; // 儲存當前圖塊
        private Point firstClick = Point.Empty;
        private Random rand = new Random();
        public event Action PuzzleSolved;
        private Timer timer;
        private int elapsedSeconds = 0;
        private Form 原圖視窗 = null;
        public Form2()
        {
            InitializeComponent();
            LoadPuzzle("puzzle1.jpg");
            this.ClientSize = originalImage.Size;
            this.Resize += Form1_Resize;

            button1.Enabled = false; // 先禁用
            button2.Enabled = false;
            button3.Enabled = false;

            timer = new Timer();
            timer.Interval = 1000; // 每秒觸發一次
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        void LoadPuzzle(string imagePath)
        {
            originalImage = new Bitmap(Image.FromFile(imagePath));
            int tileWidth = originalImage.Width / size;
            int tileHeight = originalImage.Height / size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // 建立 PictureBox
                    tiles[i, j] = new PictureBox
                    {
                        Width = tileWidth,
                        Height = tileHeight,
                        Left = j * tileWidth,
                        Top = i * tileHeight,
                        BorderStyle = BorderStyle.FixedSingle,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = new Point(i, j)
                    };
                    tiles[i, j].Click += Tile_Click;
                    this.Controls.Add(tiles[i, j]);

                    // 切出初始圖塊
                    Bitmap tileImage = new Bitmap(tileWidth, tileHeight);
                    using (Graphics g = Graphics.FromImage(tileImage))
                    {
                        g.DrawImage(originalImage,
                            new Rectangle(0, 0, tileWidth, tileHeight),
                            new Rectangle(j * tileWidth, i * tileHeight, tileWidth, tileHeight),
                            GraphicsUnit.Pixel);
                    }
                    tileImages[i, j] = tileImage;
                    tiles[i, j].Image = new Bitmap(tileImage);
                }
            }

            ShuffleTiles();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int tileWidth = this.ClientSize.Width / size;
            int tileHeight = this.ClientSize.Height / size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    PictureBox tile = tiles[i, j];
                    tile.Width = tileWidth;
                    tile.Height = tileHeight;
                    tile.Left = j * tileWidth;
                    tile.Top = i * tileHeight;

                    UpdateTileImage(i, j);
                }
            }
        }

        private void UpdateTileImage(int i, int j)
        {
            int tileWidth = this.ClientSize.Width / size;
            int tileHeight = this.ClientSize.Height / size;

            Bitmap resized = new Bitmap(tileWidth, tileHeight);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.DrawImage(tileImages[i, j], new Rectangle(0, 0, tileWidth, tileHeight));
            }
            tiles[i, j].Image = resized;
        }

        private Point GetTilePosition(PictureBox tile)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (tiles[i, j] == tile)
                        return new Point(i, j) ;
            return Point.Empty;
        }

        void ShuffleTiles()
        {
            for (int i = 0; i < 50; i++)
            {
                int x1 = rand.Next(size);
                int y1 = rand.Next(size);
                int x2 = rand.Next(size);
                int y2 = rand.Next(size);

                SwapImages(tiles[x1, y1], tiles[x2, y2]);
            }
        }

        void Tile_Click(object sender, EventArgs e)
        {
            PictureBox clicked = sender as PictureBox;
            Point clickedPos = GetTilePosition(clicked);

            if (firstClick == Point.Empty)
            {
                firstClick = clickedPos;
                clicked.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                SwapImages(tiles[firstClick.X, firstClick.Y], tiles[clickedPos.X, clickedPos.Y]);
                tiles[firstClick.X, firstClick.Y].BorderStyle = BorderStyle.FixedSingle;
                firstClick = Point.Empty;

                if (IsSolved())
                {
                    MessageBox.Show("恭喜你完成拼圖！");
                    PuzzleSolved?.Invoke(); // 發送完成事件給 Form3
                    this.Close();
                }
            }
        }

        void SwapImages(PictureBox a, PictureBox b)
        {
            Point pa = GetTilePosition(a);
            Point pb = GetTilePosition(b);

            Bitmap temp = tileImages[pa.X, pa.Y];
            tileImages[pa.X, pa.Y] = tileImages[pb.X, pb.Y];
            tileImages[pb.X, pb.Y] = temp;

            UpdateTileImage(pa.X, pa.Y);
            UpdateTileImage(pb.X, pb.Y);
        }

        bool IsSolved()
        {
            int tileWidth = originalImage.Width / size;
            int tileHeight = originalImage.Height / size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Bitmap correct = new Bitmap(tileWidth, tileHeight);
                    using (Graphics g = Graphics.FromImage(correct))
                    {
                        g.DrawImage(originalImage,
                            new Rectangle(0, 0, tileWidth, tileHeight),
                            new Rectangle(j * tileWidth, i * tileHeight, tileWidth, tileHeight),
                            GraphicsUnit.Pixel);
                    }

                    if (!CompareBitmaps(tileImages[i, j], correct))
                        return false;
                }
            }
            return true;
        }

        bool CompareBitmaps(Bitmap a, Bitmap b)
        {
            if (a.Width != b.Width || a.Height != b.Height) return false;

            for (int x = 0; x < a.Width; x++)
                for (int y = 0; y < a.Height; y++)
                    if (a.GetPixel(x, y) != b.GetPixel(x, y))
                        return false;

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 自動完成拼圖
            int tileWidth = originalImage.Width / size;
            int tileHeight = originalImage.Height / size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // 重建原圖塊
                    Bitmap tileImage = new Bitmap(tileWidth, tileHeight);
                    using (Graphics g = Graphics.FromImage(tileImage))
                    {
                        g.DrawImage(originalImage,
                            new Rectangle(0, 0, tileWidth, tileHeight),
                            new Rectangle(j * tileWidth, i * tileHeight, tileWidth, tileHeight),
                            GraphicsUnit.Pixel);
                    }
                    tileImages[i, j] = tileImage;
                    tiles[i, j].Image = new Bitmap(tileImage);
                }
            }

            MessageBox.Show("恭喜你完成拼圖！");
            PuzzleSolved?.Invoke(); // 發送完成事件給 Form3
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NUK");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (原圖視窗 == null || 原圖視窗.IsDisposed)
            {
                原圖視窗 = new Form();
                原圖視窗.Text = "原圖參考";
                原圖視窗.ClientSize = originalImage.Size;

                PictureBox pb = new PictureBox
                {
                    Image = originalImage,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Dock = DockStyle.Fill
                };

                原圖視窗.Controls.Add(pb);
                原圖視窗.FormClosed += (s, args) => { 原圖視窗 = null; }; // 釋放引用
                原圖視窗.Show();
            }
            else
            {
                原圖視窗.BringToFront();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (原圖視窗 != null && !原圖視窗.IsDisposed)
            {
                原圖視窗.Close();
                原圖視窗 = null;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;

            if (elapsedSeconds == 1)
                button2.Enabled = true; 
            if (elapsedSeconds == 5)
            {
                button3.Enabled = true; 
            }
            if (elapsedSeconds == 10)
            {
                button1.Enabled = true;
                timer.Stop(); // 不再需要 Timer
            }
        }
    }
}
