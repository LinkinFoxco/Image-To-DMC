using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_To_DMC
{
    public partial class Form1 : Form
    {
        int ClosestColor(List<Color> colors, Color target)
        {
            var colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n => n);
            return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs);
        }
        int ColorDiff(Color c1, Color c2)
        {
            return (int)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                   + (c1.G - c2.G) * (c1.G - c2.G)
                                   + (c1.B - c2.B) * (c1.B - c2.B));
        }


        public Form1()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            for (int i = 100; i < 1001; i += 100)
            {
                PearlAmounts.Add(i);
            }
            for (int i = 1500; i < 10001; i += 500)
            {
                PearlAmounts.Add(i);
            }
            for (int i = 12500; i < 50001; i += 2500)
            {
                PearlAmounts.Add(i);
            }
        }
        Image imageDeBase = null;
        string filePath = string.Empty;
        Dictionary<int, Color> DMCDic = new Dictionary<int, Color> { {208,Color.FromArgb(148,91,128)},
            {209,Color.FromArgb(206,148,186)},
            {210,Color.FromArgb(236,207,225)},
            {211,Color.FromArgb(243,218,228)},
            {221,Color.FromArgb(156,41,74)},
            {223,Color.FromArgb(219,128,115)},
            {224,Color.FromArgb(255,199,176)},
            {225,Color.FromArgb(255,240,228)},
            {300,Color.FromArgb(143,57,38)},
            {301,Color.FromArgb(209,102,84)},
            {304,Color.FromArgb(188,0,97)},
            {307,Color.FromArgb(255,231,109)},
            {309,Color.FromArgb(214,43,91)},
            {310,Color.FromArgb(0,0,0)},
            {311,Color.FromArgb(0,79,97)},
            {312,Color.FromArgb(58,84,103)},
            {315,Color.FromArgb(163,90,91)},
            {316,Color.FromArgb(220,141,141)},
            {317,Color.FromArgb(167,139,136)},
            {318,Color.FromArgb(197,198,190)},
            {319,Color.FromArgb(85,95,82)},
            {320,Color.FromArgb(138,153,120)},
            {321,Color.FromArgb(231,18,97)},
            {322,Color.FromArgb(81,109,135)},
            {326,Color.FromArgb(188,22,65)},
            {327,Color.FromArgb(61,0,103)},
            {333,Color.FromArgb(127,84,130)},
            {334,Color.FromArgb(115,140,170)},
            {335,Color.FromArgb(219,36,79)},
            {336,Color.FromArgb(36,73,103)},
            {340,Color.FromArgb(162,121,164)},
            {341,Color.FromArgb(145,180,197)},
            {347,Color.FromArgb(194,36,67)},
            {349,Color.FromArgb(220,61,91)},
            {350,Color.FromArgb(237,69,90)},
            {351,Color.FromArgb(255,128,135)},
            {352,Color.FromArgb(255,157,144)},
            {353,Color.FromArgb(255,196,184)},
            {355,Color.FromArgb(189,73,47)},
            {356,Color.FromArgb(226,114,91)},
            {367,Color.FromArgb(95,112,91)},
            {368,Color.FromArgb(181,206,162)},
            {369,Color.FromArgb(243,250,209)},
            {370,Color.FromArgb(184,138,87)},
            {371,Color.FromArgb(196,155,100)},
            {372,Color.FromArgb(203,162,107)},
            {400,Color.FromArgb(157,60,39)},
            {402,Color.FromArgb(255,190,164)},
            {407,Color.FromArgb(194,101,76)},
            {413,Color.FromArgb(109,95,95)},
            {414,Color.FromArgb(167,139,136)},
            {415,Color.FromArgb(221,221,218)},
            {420,Color.FromArgb(140,91,43)},
            {422,Color.FromArgb(237,172,123)},
            {433,Color.FromArgb(151,84,20)},
            {434,Color.FromArgb(178,103,70)},
            {435,Color.FromArgb(187,107,57)},
            {436,Color.FromArgb(231,152,115)},
            {437,Color.FromArgb(238,171,121)},
            {444,Color.FromArgb(255,176,0)},
            {445,Color.FromArgb(255,255,190)},
            {451,Color.FromArgb(179,151,143)},
            {452,Color.FromArgb(210,185,175)},
            {453,Color.FromArgb(235,207,185)},
            {469,Color.FromArgb(116,114,92)},
            {470,Color.FromArgb(133,143,108)},
            {471,Color.FromArgb(176,187,140)},
            {472,Color.FromArgb(238,255,182)},
            {498,Color.FromArgb(187,0,97)},
            {500,Color.FromArgb(43,57,41)},
            {501,Color.FromArgb(67,85,73)},
            {502,Color.FromArgb(134,158,134)},
            {503,Color.FromArgb(195,206,183)},
            {504,Color.FromArgb(206,221,193)},
            {517,Color.FromArgb(16,127,135)},
            {518,Color.FromArgb(102,148,154)},
            {519,Color.FromArgb(194,209,207)},
            {520,Color.FromArgb(55,73,18)},
            {522,Color.FromArgb(159,169,142)},
            {523,Color.FromArgb(172,183,142)},
            {524,Color.FromArgb(205,182,158)},
            {535,Color.FromArgb(85,85,89)},
            {543,Color.FromArgb(239,214,188)},
            {550,Color.FromArgb(109,18,97)},
            {552,Color.FromArgb(146,85,130)},
            {553,Color.FromArgb(160,100,146)},
            {554,Color.FromArgb(243,206,225)},
            {561,Color.FromArgb(59,96,76)},
            {562,Color.FromArgb(97,134,97)},
            {563,Color.FromArgb(182,212,180)},
            {564,Color.FromArgb(214,230,204)},
            {580,Color.FromArgb(0,103,0)},
            {581,Color.FromArgb(151,152,49)},
            {597,Color.FromArgb(128,151,132)},
            {598,Color.FromArgb(208,223,205)},
            {600,Color.FromArgb(208,57,106)},
            {601,Color.FromArgb(222,57,105)},
            {602,Color.FromArgb(231,84,122)},
            {603,Color.FromArgb(255,115,140)},
            {604,Color.FromArgb(255,189,202)},
            {605,Color.FromArgb(255,207,214)},
            {606,Color.FromArgb(255,0,0)},
            {608,Color.FromArgb(255,91,0)},
            {610,Color.FromArgb(151,104,84)},
            {611,Color.FromArgb(158,109,91)},
            {612,Color.FromArgb(203,152,103)},
            {613,Color.FromArgb(219,176,122)},
            {632,Color.FromArgb(162,77,52)},
            {640,Color.FromArgb(163,163,157)},
            {642,Color.FromArgb(174,176,170)},
            {644,Color.FromArgb(224,224,215)},
            {645,Color.FromArgb(113,113,113)},
            {646,Color.FromArgb(121,121,121)},
            {647,Color.FromArgb(190,190,185)},
            {648,Color.FromArgb(202,202,202)},
            {666,Color.FromArgb(213,39,86)},
            {676,Color.FromArgb(255,206,158)},
            {677,Color.FromArgb(255,231,182)},
            {680,Color.FromArgb(209,140,103)},
            {699,Color.FromArgb(0,91,6)},
            {700,Color.FromArgb(0,96,47)},
            {701,Color.FromArgb(79,108,69)},
            {702,Color.FromArgb(79,121,66)},
            {703,Color.FromArgb(121,144,76)},
            {704,Color.FromArgb(165,164,103)},
            {712,Color.FromArgb(245,240,219)},
            {718,Color.FromArgb(219,55,121)},
            {720,Color.FromArgb(200,36,43)},
            {721,Color.FromArgb(255,115,97)},
            {722,Color.FromArgb(255,146,109)},
            {725,Color.FromArgb(255,200,124)},
            {726,Color.FromArgb(255,224,128)},
            {727,Color.FromArgb(255,235,168)},
            {729,Color.FromArgb(243,176,128)},
            {730,Color.FromArgb(132,102,0)},
            {731,Color.FromArgb(140,103,0)},
            {732,Color.FromArgb(145,104,0)},
            {733,Color.FromArgb(206,155,97)},
            {734,Color.FromArgb(221,166,107)},
            {738,Color.FromArgb(244,195,139)},
            {739,Color.FromArgb(244,233,202)},
            {740,Color.FromArgb(255,131,19)},
            {741,Color.FromArgb(255,142,4)},
            {742,Color.FromArgb(255,183,85)},
            {743,Color.FromArgb(255,230,146)},
            {744,Color.FromArgb(255,239,170)},
            {745,Color.FromArgb(255,240,197)},
            {746,Color.FromArgb(246,234,219)},
            {747,Color.FromArgb(240,247,239)},
            {754,Color.FromArgb(251,227,209)},
            {758,Color.FromArgb(255,177,147)},
            {760,Color.FromArgb(249,160,146)},
            {761,Color.FromArgb(255,201,188)},
            {762,Color.FromArgb(232,232,229)},
            {772,Color.FromArgb(231,249,203)},
            {775,Color.FromArgb(247,246,248)},
            {776,Color.FromArgb(255,177,174)},
            {778,Color.FromArgb(255,199,184)},
            {780,Color.FromArgb(181,98,46)},
            {781,Color.FromArgb(181,107,56)},
            {782,Color.FromArgb(204,119,66)},
            {783,Color.FromArgb(225,146,85)},
            {791,Color.FromArgb(71,55,93)},
            {792,Color.FromArgb(97,97,128)},
            {793,Color.FromArgb(147,139,164)},
            {794,Color.FromArgb(187,208,218)},
            {796,Color.FromArgb(30,58,95)},
            {797,Color.FromArgb(30,66,99)},
            {798,Color.FromArgb(103,115,141)},
            {799,Color.FromArgb(132,156,182)},
            {800,Color.FromArgb(233,238,233)},
            {801,Color.FromArgb(123,71,20)},
            {806,Color.FromArgb(30,130,133)},
            {807,Color.FromArgb(128,167,160)},
            {809,Color.FromArgb(190,193,205)},
            {813,Color.FromArgb(175,195,205)},
            {814,Color.FromArgb(162,0,88)},
            {815,Color.FromArgb(166,0,91)},
            {816,Color.FromArgb(179,0,91)},
            {817,Color.FromArgb(219,24,85)},
            {818,Color.FromArgb(255,234,235)},
            {819,Color.FromArgb(248,247,221)},
            {820,Color.FromArgb(30,54,85)},
            {822,Color.FromArgb(242,234,219)},
            {823,Color.FromArgb(0,0,73)},
            {824,Color.FromArgb(71,97,116)},
            {825,Color.FromArgb(85,108,128)},
            {826,Color.FromArgb(115,138,153)},
            {827,Color.FromArgb(213,231,232)},
            {828,Color.FromArgb(237,247,238)},
            {829,Color.FromArgb(130,90,8)},
            {830,Color.FromArgb(136,95,18)},
            {831,Color.FromArgb(144,103,18)},
            {832,Color.FromArgb(178,119,55)},
            {833,Color.FromArgb(219,182,128)},
            {834,Color.FromArgb(242,209,142)},
            {838,Color.FromArgb(94,56,27)},
            {839,Color.FromArgb(109,66,39)},
            {840,Color.FromArgb(128,85,30)},
            {841,Color.FromArgb(188,134,107)},
            {842,Color.FromArgb(219,194,164)},
            {844,Color.FromArgb(107,103,102)},
            {868,Color.FromArgb(153,92,48)},
            {869,Color.FromArgb(153,92,48)},
            {890,Color.FromArgb(79,86,76)},
            {891,Color.FromArgb(241,49,84)},
            {892,Color.FromArgb(249,90,97)},
            {893,Color.FromArgb(243,149,157)},
            {894,Color.FromArgb(255,194,191)},
            {895,Color.FromArgb(89,92,78)},
            {898,Color.FromArgb(118,55,19)},
            {899,Color.FromArgb(233,109,115)},
            {900,Color.FromArgb(206,43,0)},
            {902,Color.FromArgb(138,24,77)},
            {904,Color.FromArgb(78,95,57)},
            {905,Color.FromArgb(98,119,57)},
            {906,Color.FromArgb(143,163,89)},
            {907,Color.FromArgb(185,200,102)},
            {909,Color.FromArgb(49,105,85)},
            {910,Color.FromArgb(48,116,91)},
            {911,Color.FromArgb(49,128,97)},
            {912,Color.FromArgb(115,158,115)},
            {913,Color.FromArgb(153,188,149)},
            {915,Color.FromArgb(170,24,91)},
            {917,Color.FromArgb(171,22,95)},
            {918,Color.FromArgb(168,68,76)},
            {919,Color.FromArgb(180,75,82)},
            {920,Color.FromArgb(197,94,88)},
            {921,Color.FromArgb(206,103,91)},
            {922,Color.FromArgb(237,134,115)},
            {924,Color.FromArgb(86,99,100)},
            {926,Color.FromArgb(96,116,115)},
            {927,Color.FromArgb(200,198,194)},
            {928,Color.FromArgb(225,224,216)},
            {930,Color.FromArgb(102,122,140)},
            {931,Color.FromArgb(124,135,145)},
            {932,Color.FromArgb(182,186,194)},
            {934,Color.FromArgb(62,59,40)},
            {935,Color.FromArgb(67,63,47)},
            {936,Color.FromArgb(69,69,49)},
            {937,Color.FromArgb(73,86,55)},
            {938,Color.FromArgb(99,39,16)},
            {939,Color.FromArgb(0,0,49)},
            {943,Color.FromArgb(0,162,117)},
            {945,Color.FromArgb(255,206,164)},
            {946,Color.FromArgb(244,73,0)},
            {947,Color.FromArgb(255,91,0)},
            {948,Color.FromArgb(255,243,231)},
            {950,Color.FromArgb(239,162,127)},
            {951,Color.FromArgb(255,229,188)},
            {954,Color.FromArgb(170,213,164)},
            {955,Color.FromArgb(214,230,204)},
            {956,Color.FromArgb(255,109,115)},
            {957,Color.FromArgb(255,204,208)},
            {958,Color.FromArgb(0,160,130)},
            {959,Color.FromArgb(171,206,177)},
            {961,Color.FromArgb(243,108,123)},
            {962,Color.FromArgb(253,134,141)},
            {963,Color.FromArgb(255,233,233)},
            {964,Color.FromArgb(208,224,210)},
            {966,Color.FromArgb(206,213,176)},
            {970,Color.FromArgb(255,117,24)},
            {971,Color.FromArgb(255,106,0)},
            {972,Color.FromArgb(255,146,0)},
            {973,Color.FromArgb(255,194,67)},
            {975,Color.FromArgb(158,67,18)},
            {976,Color.FromArgb(246,141,57)},
            {977,Color.FromArgb(255,164,73)},
            {986,Color.FromArgb(58,82,65)},
            {987,Color.FromArgb(83,97,73)},
            {988,Color.FromArgb(134,145,110)},
            {989,Color.FromArgb(134,153,110)},
            {991,Color.FromArgb(47,91,73)},
            {992,Color.FromArgb(146,183,165)},
            {993,Color.FromArgb(192,224,200)},
            {995,Color.FromArgb(0,123,134)},
            {996,Color.FromArgb(170,222,225)},
            {3011,Color.FromArgb(123,91,64)},
            {3012,Color.FromArgb(170,134,103)},
            {3013,Color.FromArgb(208,195,164)},
            {3021,Color.FromArgb(115,91,93)},
            {3022,Color.FromArgb(172,172,170)},
            {3023,Color.FromArgb(198,190,173)},
            {3024,Color.FromArgb(210,208,205)},
            {3031,Color.FromArgb(84,56,23)},
            {3032,Color.FromArgb(188,156,120)},
            {3033,Color.FromArgb(239,219,190)},
            {3041,Color.FromArgb(190,155,167)},
            {3042,Color.FromArgb(225,205,200)},
            {3045,Color.FromArgb(216,151,105)},
            {3046,Color.FromArgb(229,193,139)},
            {3047,Color.FromArgb(255,236,211)},
            {3051,Color.FromArgb(85,73,0)},
            {3052,Color.FromArgb(137,141,114)},
            {3053,Color.FromArgb(187,179,148)},
            {3064,Color.FromArgb(194,101,76)},
            {3072,Color.FromArgb(233,233,223)},
            {3078,Color.FromArgb(255,255,220)},
            {3325,Color.FromArgb(202,226,229)},
            {3326,Color.FromArgb(255,157,150)},
            {3328,Color.FromArgb(188,64,85)},
            {3340,Color.FromArgb(255,123,103)},
            {3341,Color.FromArgb(235,172,162)},
            {3345,Color.FromArgb(97,100,82)},
            {3346,Color.FromArgb(120,134,107)},
            {3347,Color.FromArgb(128,152,115)},
            {3348,Color.FromArgb(225,249,190)},
            {3350,Color.FromArgb(201,79,91)},
            {3354,Color.FromArgb(255,214,209)},
            {3362,Color.FromArgb(96,95,84)},
            {3363,Color.FromArgb(116,127,96)},
            {3364,Color.FromArgb(161,167,135)},
            {3371,Color.FromArgb(83,37,16)},
            {3607,Color.FromArgb(231,79,134)},
            {3608,Color.FromArgb(247,152,182)},
            {3609,Color.FromArgb(255,214,229)},
            {3685,Color.FromArgb(161,53,79)},
            {3687,Color.FromArgb(203,78,97)},
            {3688,Color.FromArgb(250,151,144)},
            {3689,Color.FromArgb(255,213,216)},
            {3705,Color.FromArgb(255,85,91)},
            {3706,Color.FromArgb(255,128,109)},
            {3708,Color.FromArgb(254,212,219)},
            {3712,Color.FromArgb(230,101,107)},
            {3713,Color.FromArgb(253,229,217)},
            {3716,Color.FromArgb(255,211,212)},
            {3721,Color.FromArgb(184,75,77)},
            {3722,Color.FromArgb(184,89,88)},
            {3726,Color.FromArgb(195,118,123)},
            {3727,Color.FromArgb(255,199,196)},
            {3731,Color.FromArgb(209,93,103)},
            {3733,Color.FromArgb(255,154,148)},
            {3740,Color.FromArgb(156,125,133)},
            {3743,Color.FromArgb(235,235,231)},
            {3746,Color.FromArgb(149,102,162)},
            {3747,Color.FromArgb(230,236,232)},
            {3750,Color.FromArgb(12,91,108)},
            {3752,Color.FromArgb(194,209,206)},
            {3753,Color.FromArgb(237,247,247)},
            {3755,Color.FromArgb(158,176,206)},
            {3756,Color.FromArgb(248,248,252)},
            {3760,Color.FromArgb(102,142,152)},
            {3761,Color.FromArgb(227,234,230)},
            {3765,Color.FromArgb(24,128,134)},
            {3766,Color.FromArgb(24,101,111)},
            {3768,Color.FromArgb(92,110,108)},
            {3770,Color.FromArgb(255,250,224)},
            {3772,Color.FromArgb(173,83,62)},
            {3773,Color.FromArgb(231,134,103)},
            {3774,Color.FromArgb(255,220,193)},
            {3776,Color.FromArgb(221,109,91)},
            {3777,Color.FromArgb(191,64,36)},
            {3778,Color.FromArgb(237,122,100)},
            {3779,Color.FromArgb(255,177,152)},
            {3781,Color.FromArgb(113,71,42)},
            {3782,Color.FromArgb(206,175,144)},
            {3787,Color.FromArgb(139,109,115)},
            {3790,Color.FromArgb(140,117,109)},
            {3799,Color.FromArgb(81,76,83)},
            };
        List<int> PearlAmounts = new List<int> { };


        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\Users\\" + Environment.UserName + "\\Desktop";
                openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    imageDeBase = Image.FromFile(filePath);
                    var WidthImage = imageDeBase.Width;
                    var HeightImage = imageDeBase.Height;
                    float ratio = (float)WidthImage / (float)HeightImage;

                    ComboboxItem currentItem = new ComboboxItem { };
                    comboBox1.Items.Clear();
                    for (int i = 0; i< PearlAmounts.Count; i++)
                    {
                        var tempItem = new Size((int)Math.Sqrt(PearlAmounts[i] * ratio),0);
                        tempItem.Height = (int)(tempItem.Width / ratio);
                        currentItem.Value = tempItem;
                        currentItem.Text = PearlAmounts[i] + " Perles (" + currentItem.Value.Width + "x" + currentItem.Value.Height + ")" ;
                        comboBox1.Items.Add(currentItem);
                    }
                    comboBox1.Enabled = true;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var test = (ComboboxItem)comboBox1.SelectedItem;
            var newSize = test.Value;
            var imageReSize = new Bitmap(imageDeBase, newSize.Width, newSize.Height);
            int w = imageReSize.Width;
            int h = imageReSize.Height;
            progressBar1.Visible = true;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = w * h * 3;
            progressBar1.Value = 1;
            progressBar1.Step = 1;

            List<Color> ListRGB = new List<Color>(w * h);
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    ListRGB.Add(imageReSize.GetPixel(x, y));
                    progressBar1.PerformStep();
                }
            }

            //matchs couleurs approximativement
            List<DMCRGB> DMCList = new List<DMCRGB>(ListRGB.Count);
            var ColorsDMC = DMCDic.Values.ToList();
            foreach (Color currentColor in ListRGB)
            {
                var currentIndex = ClosestColor(ColorsDMC, currentColor);
                DMCList.Add(new DMCRGB(DMCDic.ElementAt(currentIndex).Key, DMCDic.ElementAt(currentIndex).Value));
                progressBar1.PerformStep();
            }

            //generation image finale
            Bitmap imageFinale = new Bitmap(w * 40, h * 40);
            using (Graphics g = Graphics.FromImage(imageFinale))
            {
                StringFormat formatString = new StringFormat();
                formatString.Alignment = StringAlignment.Center;
                formatString.LineAlignment = StringAlignment.Center;
                var fontSize = 10;
                Font fontString = new Font("Arial", fontSize);
                SolidBrush brushString = new SolidBrush(Color.Black);
                SolidBrush brushRectangle = new SolidBrush(Color.White);
                Pen penRectangle = new Pen(Color.Black, 3);
                Pen penOutline = new Pen(Color.White, 2);

                for (int hF = 0; hF < h; hF++)
                {
                    for (int wF = 0; wF < w; wF++)
                    {
                        var tempRect = new Rectangle(wF * 40, hF * 40, 40, 40);
                        var tempDMCRGB = DMCList[hF * w + wF];
                        brushRectangle.Color = tempDMCRGB.RGB;
                        g.FillRectangle(brushRectangle, tempRect);

                        ////outline
                        //System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath();
                        //p.AddString(
                        //    tempDMCRGB.index.ToString(),             // text to draw
                        //    FontFamily.GenericSansSerif,  // or any other font family
                        //    (int)FontStyle.Regular,      // font style (bold, italic, etc.)
                        //    g.DpiY * fontSize / 72,       // em size
                        //    tempRect,              // location where to draw text
                        //    formatString);          // set options here (e.g. center alignment)
                        //g.DrawPath(penOutline, p);

                        g.DrawString(tempDMCRGB.index.ToString(), fontString, brushString, tempRect, formatString);
                        g.DrawRectangle(penRectangle, tempRect);

                        progressBar1.PerformStep();
                    }
                }
            }
            pictureBox1.Image = imageFinale;
            string tempExtension = System.IO.Path.GetExtension(filePath);
            imageFinale.Save(System.IO.Path.ChangeExtension(filePath, null) + "DMC" + tempExtension);
            progressBar1.Step = 1;
            progressBar1.Value = 1;
            progressBar1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;

            //var test3 = (ComboboxItem)comboBox1.SelectedItem;
            //var test5 = test3.Value.Width;
            //var test4 = comboBox1.Items; // devient uniquement la dernière value why ?
            //var newSize = (ComboboxItem)comboBox1.Items[comboBox1.SelectedIndex];
        }

    }
}

public class DMCRGB
{
    public DMCRGB(int indexIn, Color RGBIn)
    {
        index = indexIn;
        RGB = RGBIn;
    }
    public int index;
    public Color RGB;
}

public class ComboboxItem
{
    public string Text { get; set; }
    public Size Value { get; set; }

    public override string ToString()
    {
        return Text;
    }
}
