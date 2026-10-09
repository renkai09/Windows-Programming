using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 專題
{
    public partial class Form3 : Form
    {
        private bool[] stageUnlocked = new bool[11]; // 關卡解鎖狀態，索引 1~10 有效，預設只有 stageUnlocked[1] 為 true
        private string saveFile = "stage.txt"; // 儲存解鎖資訊的檔案名稱
        private Form1 mainForm; // 主畫面參考，用於返回
        public Form3(Form1 form1)
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile("back.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            mainForm = form1;
            stageUnlocked[1] = true;
            this.Load += new System.EventHandler(this.Form3_Load);
        }

        // 各關卡按鈕點擊事件：開啟對應關卡表單，完成後解鎖下一關
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();

            form2.PuzzleSolved += () =>
            {
                stageUnlocked[2] = true;
                button2.Enabled = true;
                SaveStageUnlock();  // 解鎖後立即存檔
                MessageBox.Show("第2關已解鎖！");
            };

            form2.FormClosed += (s, args) => this.Show();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();

            form4.PuzzleSolved += () =>
            {
                stageUnlocked[3] = true;
                button3.Enabled = true;
                SaveStageUnlock();  // 解鎖後存檔
                MessageBox.Show("第3關已解鎖！");
            };

            form4.FormClosed += (s, args) => this.Show();
            form4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 form5 = new Form5();

            form5.PuzzleSolved += () =>
            {
                stageUnlocked[4] = true;
                button4.Enabled = true;
                SaveStageUnlock();
                MessageBox.Show("第4關已解鎖！");
            };

            form5.FormClosed += (s, args) => this.Show();
            form5.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 form6 = new Form6();

            form6.PuzzleSolved += () =>
            {
                stageUnlocked[5] = true;
                button5.Enabled = true;
                SaveStageUnlock();
                MessageBox.Show("第5關已解鎖！");
            };

            form6.FormClosed += (s, args) => this.Show();
            form6.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 form7 = new Form7();

            form7.PuzzleSolved += () =>
            {
                stageUnlocked[6] = true;
                button6.Enabled = true;
                SaveStageUnlock();
                MessageBox.Show("第6關已解鎖！");
            };

            form7.FormClosed += (s, args) => this.Show();
            form7.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8();

            form8.PuzzleSolved += () =>
            {
                stageUnlocked[7] = true;
                button7.Enabled = true;
                SaveStageUnlock();
                MessageBox.Show("第7關已解鎖！");
            };

            form8.FormClosed += (s, args) => this.Show();
            form8.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form9 form9 = new Form9();

            form9.PuzzleSolved += () =>
            {
                stageUnlocked[8] = true;
                button8.Enabled = true;
                SaveStageUnlock();
                MessageBox.Show("第8關已解鎖！");
            };

            form9.FormClosed += (s, args) => this.Show();
            form9.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 form10 = new Form10();

            form10.PuzzleSolved += () =>
            {
                stageUnlocked[9] = true;
                button9.Enabled = true;
                SaveStageUnlock();
                MessageBox.Show("第9關已解鎖！");
            };

            form10.FormClosed += (s, args) => this.Show();
            form10.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form11 form11 = new Form11();

            form11.PuzzleSolved += () =>
            {
                stageUnlocked[10] = true;
                button10.Enabled = true;
                SaveStageUnlock();
                MessageBox.Show("第10關已解鎖！");
            };

            form11.FormClosed += (s, args) => this.Show();
            form11.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form12 form12 = new Form12();
            form12.FormClosed += (s, args) => this.Show();
            form12.Show();
        }
        // 表單載入時自動讀取解鎖狀態並更新按鈕啟用情形
        private void Form3_Load(object sender, EventArgs e)
        {
            LoadStageUnlock();  // 讀取關卡解鎖狀態
            button1.Enabled = true; // 第一關開放
            button2.Enabled = stageUnlocked[2];
            button3.Enabled = stageUnlocked[3];
            button4.Enabled = stageUnlocked[4];
            button5.Enabled = stageUnlocked[5];
            button6.Enabled = stageUnlocked[6];
            button7.Enabled = stageUnlocked[7];
            button8.Enabled = stageUnlocked[8];
            button9.Enabled = stageUnlocked[9];
            button10.Enabled = stageUnlocked[10];
        }
        // 載入解鎖狀態
        private void LoadStageUnlock()
        {
            if (File.Exists(saveFile))
            {
                try
                {
                    string[] lines = File.ReadAllLines(saveFile);
                    for (int i = 1; i <= 10 && i <= lines.Length; i++)
                    {
                        bool val;
                        if (bool.TryParse(lines[i - 1], out val))
                            stageUnlocked[i] = val;
                        else
                            stageUnlocked[i] = false;
                    }
                }
                catch
                {
                    // 讀取錯誤，重置預設
                    for (int i = 1; i <= 10; i++)
                        stageUnlocked[i] = false;
                    stageUnlocked[1] = true;
                }
            }
            else
            {
                // 沒有檔案，預設第一關解鎖
                for (int i = 1; i <= 10; i++)
                    stageUnlocked[i] = false;
                stageUnlocked[1] = true;
            }
        }
        // 儲存解鎖狀態到檔案
        private void SaveStageUnlock()
        {
            try
            {
                string[] lines = new string[10];
                for (int i = 1; i <= 10; i++)
                    lines[i - 1] = stageUnlocked[i].ToString();
                File.WriteAllLines(saveFile, lines);
            }
            catch
            {
                MessageBox.Show("存檔失敗！");
            }
        }
        // 返回主畫面
        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm.Show();
        }
    }
}
