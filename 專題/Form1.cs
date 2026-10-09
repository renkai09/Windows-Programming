using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace 專題
{
    public partial class Form1 : Form
    {
        private SoundPlayer player;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile("start.jpg") ;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            player = new SoundPlayer("music1.wav");
            player.PlayLooping(); // 持續播放
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3(this);
            // 建立並訂閱 Form12 的 PuzzleSolved 事件
            Form12 form12 = new Form12();
            form12.PuzzleSolved += () =>
            {
                player.Stop();  // 停止播放音樂
            };
            
            form3.FormClosed += (s, args) => this.Close();
            form3.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "選擇音樂檔案";
                openFileDialog.Filter = "WAV 音樂檔 (*.wav)|*.wav";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 停止目前播放的音樂
                        if (player != null)
                        {
                            player.Stop();
                            player.Dispose();
                        }

                        // 使用新選擇的音樂檔案
                        player = new SoundPlayer(openFileDialog.FileName);
                        player.PlayLooping(); // 持續播放新音樂
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("播放音樂時發生錯誤: " + ex.Message);
                    }
                }
            }
        }
    }
}
