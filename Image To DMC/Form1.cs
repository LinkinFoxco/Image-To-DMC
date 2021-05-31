using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_To_DMC
{
    public partial class ConvertisseurForm : Form
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Tuple<int, Color> ClosestColor(List<Tuple<int, Color>> colors, Color target)
        {
            int minDiff = int.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < colors.Count; i++)
            {
                int diff = ColorDiff(colors[i].Item2, ref target);

                if (diff < minDiff)
                {
                    minDiff = diff;
                    minIndex = i;
                }
            }

            return colors[minIndex];
        }


        // Aprox Sqrt from stackoverflow
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Isqrt(int num)
        {
            if (0 == num) { return 0; }  // Avoid zero divide  
            int n = (num << 1) + 1;       // Initial estimate, never low  
            int n1 = (n + (num / n)) << 1;
            while (n1 < n)
            {
                n = n1;
                n1 = (n + (num / n)) << 1;
            } // end while  
            return n;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int ColorDiff(Color c1, ref Color c2)
        {
            return (int)/*Math.Sqrt*/Isqrt((c1.R - c2.R) * (c1.R - c2.R)
                                   + (c1.G - c2.G) * (c1.G - c2.G)
                                   + (c1.B - c2.B) * (c1.B - c2.B));
        }

        public ConvertisseurForm()
        {
            InitializeComponent();
        }

        Image imageDeBase = null;
        Bitmap imageFinale = null;

        string filePath = string.Empty;
        List<Tuple<int, Color>> DMCPairs = new List<Tuple<int, Color>>
        {
            new Tuple<int, Color>(208    ,   Color.FromArgb(148,91,128)     ),
            new Tuple<int, Color>(209    ,   Color.FromArgb(206,148,186)    ),
            new Tuple<int, Color>(210    ,   Color.FromArgb(236,207,225)    ),
            new Tuple<int, Color>(211    ,   Color.FromArgb(243,218,228)    ),
            new Tuple<int, Color>(221    ,   Color.FromArgb(156,41,74)      ),
            new Tuple<int, Color>(223    ,   Color.FromArgb(219,128,115)    ),
            new Tuple<int, Color>(224    ,   Color.FromArgb(255,199,176)    ),
            new Tuple<int, Color>(225    ,   Color.FromArgb(255,240,228)    ),
            new Tuple<int, Color>(300    ,   Color.FromArgb(143,57,38)      ),
            new Tuple<int, Color>(301    ,   Color.FromArgb(209,102,84)     ),
            new Tuple<int, Color>(304    ,   Color.FromArgb(188,0,97)       ),
            new Tuple<int, Color>(307    ,   Color.FromArgb(255,231,109)    ),
            new Tuple<int, Color>(309    ,   Color.FromArgb(214,43,91)      ),
            new Tuple<int, Color>(310    ,   Color.FromArgb(0,0,0)          ),
            new Tuple<int, Color>(311    ,   Color.FromArgb(0,79,97)        ),
            new Tuple<int, Color>(312    ,   Color.FromArgb(58,84,103)      ),
            new Tuple<int, Color>(315    ,   Color.FromArgb(163,90,91)      ),
            new Tuple<int, Color>(316    ,   Color.FromArgb(220,141,141)    ),
            new Tuple<int, Color>(317    ,   Color.FromArgb(167,139,136)    ),
            new Tuple<int, Color>(318    ,   Color.FromArgb(197,198,190)    ),
            new Tuple<int, Color>(319    ,   Color.FromArgb(85,95,82)       ),
            new Tuple<int, Color>(320    ,   Color.FromArgb(138,153,120)    ),
            new Tuple<int, Color>(321    ,   Color.FromArgb(231,18,97)      ),
            new Tuple<int, Color>(322    ,   Color.FromArgb(81,109,135)     ),
            new Tuple<int, Color>(326    ,   Color.FromArgb(188,22,65)      ),
            new Tuple<int, Color>(327    ,   Color.FromArgb(61,0,103)       ),
            new Tuple<int, Color>(333    ,   Color.FromArgb(127,84,130)     ),
            new Tuple<int, Color>(334    ,   Color.FromArgb(115,140,170)    ),
            new Tuple<int, Color>(335    ,   Color.FromArgb(219,36,79)      ),
            new Tuple<int, Color>(336    ,   Color.FromArgb(36,73,103)      ),
            new Tuple<int, Color>(340    ,   Color.FromArgb(162,121,164)    ),
            new Tuple<int, Color>(341    ,   Color.FromArgb(145,180,197)    ),
            new Tuple<int, Color>(347    ,   Color.FromArgb(194,36,67)      ),
            new Tuple<int, Color>(349    ,   Color.FromArgb(220,61,91)      ),
            new Tuple<int, Color>(350    ,   Color.FromArgb(237,69,90)      ),
            new Tuple<int, Color>(351    ,   Color.FromArgb(255,128,135)    ),
            new Tuple<int, Color>(352    ,   Color.FromArgb(255,157,144)    ),
            new Tuple<int, Color>(353    ,   Color.FromArgb(255,196,184)    ),
            new Tuple<int, Color>(355    ,   Color.FromArgb(189,73,47)      ),
            new Tuple<int, Color>(356    ,   Color.FromArgb(226,114,91)     ),
            new Tuple<int, Color>(367    ,   Color.FromArgb(95,112,91)      ),
            new Tuple<int, Color>(368    ,   Color.FromArgb(181,206,162)    ),
            new Tuple<int, Color>(369    ,   Color.FromArgb(243,250,209)    ),
            new Tuple<int, Color>(370    ,   Color.FromArgb(184,138,87)     ),
            new Tuple<int, Color>(371    ,   Color.FromArgb(196,155,100)    ),
            new Tuple<int, Color>(372    ,   Color.FromArgb(203,162,107)    ),
            new Tuple<int, Color>(400    ,   Color.FromArgb(157,60,39)      ),
            new Tuple<int, Color>(402    ,   Color.FromArgb(255,190,164)    ),
            new Tuple<int, Color>(407    ,   Color.FromArgb(194,101,76)     ),
            new Tuple<int, Color>(413    ,   Color.FromArgb(109,95,95)      ),
            new Tuple<int, Color>(414    ,   Color.FromArgb(167,139,136)    ),
            new Tuple<int, Color>(415    ,   Color.FromArgb(221,221,218)    ),
            new Tuple<int, Color>(420    ,   Color.FromArgb(140,91,43)      ),
            new Tuple<int, Color>(422    ,   Color.FromArgb(237,172,123)    ),
            new Tuple<int, Color>(433    ,   Color.FromArgb(151,84,20)      ),
            new Tuple<int, Color>(434    ,   Color.FromArgb(178,103,70)     ),
            new Tuple<int, Color>(435    ,   Color.FromArgb(187,107,57)     ),
            new Tuple<int, Color>(436    ,   Color.FromArgb(231,152,115)    ),
            new Tuple<int, Color>(437    ,   Color.FromArgb(238,171,121)    ),
            new Tuple<int, Color>(444    ,   Color.FromArgb(255,176,0)      ),
            new Tuple<int, Color>(445    ,   Color.FromArgb(255,255,190)    ),
            new Tuple<int, Color>(451    ,   Color.FromArgb(179,151,143)    ),
            new Tuple<int, Color>(452    ,   Color.FromArgb(210,185,175)    ),
            new Tuple<int, Color>(453    ,   Color.FromArgb(235,207,185)    ),
            new Tuple<int, Color>(469    ,   Color.FromArgb(116,114,92)     ),
            new Tuple<int, Color>(470    ,   Color.FromArgb(133,143,108)    ),
            new Tuple<int, Color>(471    ,   Color.FromArgb(176,187,140)    ),
            new Tuple<int, Color>(472    ,   Color.FromArgb(238,255,182)    ),
            new Tuple<int, Color>(498    ,   Color.FromArgb(187,0,97)       ),
            new Tuple<int, Color>(500    ,   Color.FromArgb(43,57,41)       ),
            new Tuple<int, Color>(501    ,   Color.FromArgb(67,85,73)       ),
            new Tuple<int, Color>(502    ,   Color.FromArgb(134,158,134)    ),
            new Tuple<int, Color>(503    ,   Color.FromArgb(195,206,183)    ),
            new Tuple<int, Color>(504    ,   Color.FromArgb(206,221,193)    ),
            new Tuple<int, Color>(517    ,   Color.FromArgb(16,127,135)     ),
            new Tuple<int, Color>(518    ,   Color.FromArgb(102,148,154)    ),
            new Tuple<int, Color>(519    ,   Color.FromArgb(194,209,207)    ),
            new Tuple<int, Color>(520    ,   Color.FromArgb(55,73,18)       ),
            new Tuple<int, Color>(522    ,   Color.FromArgb(159,169,142)    ),
            new Tuple<int, Color>(523    ,   Color.FromArgb(172,183,142)    ),
            new Tuple<int, Color>(524    ,   Color.FromArgb(205,182,158)    ),
            new Tuple<int, Color>(535    ,   Color.FromArgb(85,85,89)       ),
            new Tuple<int, Color>(543    ,   Color.FromArgb(239,214,188)    ),
            new Tuple<int, Color>(550    ,   Color.FromArgb(109,18,97)      ),
            new Tuple<int, Color>(552    ,   Color.FromArgb(146,85,130)     ),
            new Tuple<int, Color>(553    ,   Color.FromArgb(160,100,146)    ),
            new Tuple<int, Color>(554    ,   Color.FromArgb(243,206,225)    ),
            new Tuple<int, Color>(561    ,   Color.FromArgb(59,96,76)       ),
            new Tuple<int, Color>(562    ,   Color.FromArgb(97,134,97)      ),
            new Tuple<int, Color>(563    ,   Color.FromArgb(182,212,180)    ),
            new Tuple<int, Color>(564    ,   Color.FromArgb(214,230,204)    ),
            new Tuple<int, Color>(580    ,   Color.FromArgb(0,103,0)        ),
            new Tuple<int, Color>(581    ,   Color.FromArgb(151,152,49)     ),
            new Tuple<int, Color>(597    ,   Color.FromArgb(128,151,132)    ),
            new Tuple<int, Color>(598    ,   Color.FromArgb(208,223,205)    ),
            new Tuple<int, Color>(600    ,   Color.FromArgb(208,57,106)     ),
            new Tuple<int, Color>(601    ,   Color.FromArgb(222,57,105)     ),
            new Tuple<int, Color>(602    ,   Color.FromArgb(231,84,122)     ),
            new Tuple<int, Color>(603    ,   Color.FromArgb(255,115,140)    ),
            new Tuple<int, Color>(604    ,   Color.FromArgb(255,189,202)    ),
            new Tuple<int, Color>(605    ,   Color.FromArgb(255,207,214)    ),
            new Tuple<int, Color>(606    ,   Color.FromArgb(255,0,0)        ),
            new Tuple<int, Color>(608    ,   Color.FromArgb(255,91,0)       ),
            new Tuple<int, Color>(610    ,   Color.FromArgb(151,104,84)     ),
            new Tuple<int, Color>(611    ,   Color.FromArgb(158,109,91)     ),
            new Tuple<int, Color>(612    ,   Color.FromArgb(203,152,103)    ),
            new Tuple<int, Color>(613    ,   Color.FromArgb(219,176,122)    ),
            new Tuple<int, Color>(632    ,   Color.FromArgb(162,77,52)      ),
            new Tuple<int, Color>(640    ,   Color.FromArgb(163,163,157)    ),
            new Tuple<int, Color>(642    ,   Color.FromArgb(174,176,170)    ),
            new Tuple<int, Color>(644    ,   Color.FromArgb(224,224,215)    ),
            new Tuple<int, Color>(645    ,   Color.FromArgb(113,113,113)    ),
            new Tuple<int, Color>(646    ,   Color.FromArgb(121,121,121)    ),
            new Tuple<int, Color>(647    ,   Color.FromArgb(190,190,185)    ),
            new Tuple<int, Color>(648    ,   Color.FromArgb(202,202,202)    ),
            new Tuple<int, Color>(666    ,   Color.FromArgb(213,39,86)      ),
            new Tuple<int, Color>(676    ,   Color.FromArgb(255,206,158)    ),
            new Tuple<int, Color>(677    ,   Color.FromArgb(255,231,182)    ),
            new Tuple<int, Color>(680    ,   Color.FromArgb(209,140,103)    ),
            new Tuple<int, Color>(699    ,   Color.FromArgb(0,91,6)         ),
            new Tuple<int, Color>(700    ,   Color.FromArgb(0,96,47)        ),
            new Tuple<int, Color>(701    ,   Color.FromArgb(79,108,69)      ),
            new Tuple<int, Color>(702    ,   Color.FromArgb(79,121,66)      ),
            new Tuple<int, Color>(703    ,   Color.FromArgb(121,144,76)     ),
            new Tuple<int, Color>(704    ,   Color.FromArgb(165,164,103)    ),
            new Tuple<int, Color>(712    ,   Color.FromArgb(245,240,219)    ),
            new Tuple<int, Color>(718    ,   Color.FromArgb(219,55,121)     ),
            new Tuple<int, Color>(720    ,   Color.FromArgb(200,36,43)      ),
            new Tuple<int, Color>(721    ,   Color.FromArgb(255,115,97)     ),
            new Tuple<int, Color>(722    ,   Color.FromArgb(255,146,109)    ),
            new Tuple<int, Color>(725    ,   Color.FromArgb(255,200,124)    ),
            new Tuple<int, Color>(726    ,   Color.FromArgb(255,224,128)    ),
            new Tuple<int, Color>(727    ,   Color.FromArgb(255,235,168)    ),
            new Tuple<int, Color>(729    ,   Color.FromArgb(243,176,128)    ),
            new Tuple<int, Color>(730    ,   Color.FromArgb(132,102,0)      ),
            new Tuple<int, Color>(731    ,   Color.FromArgb(140,103,0)      ),
            new Tuple<int, Color>(732    ,   Color.FromArgb(145,104,0)      ),
            new Tuple<int, Color>(733    ,   Color.FromArgb(206,155,97)     ),
            new Tuple<int, Color>(734    ,   Color.FromArgb(221,166,107)    ),
            new Tuple<int, Color>(738    ,   Color.FromArgb(244,195,139)    ),
            new Tuple<int, Color>(739    ,   Color.FromArgb(244,233,202)    ),
            new Tuple<int, Color>(740    ,   Color.FromArgb(255,131,19)     ),
            new Tuple<int, Color>(741    ,   Color.FromArgb(255,142,4)      ),
            new Tuple<int, Color>(742    ,   Color.FromArgb(255,183,85)     ),
            new Tuple<int, Color>(743    ,   Color.FromArgb(255,230,146)    ),
            new Tuple<int, Color>(744    ,   Color.FromArgb(255,239,170)    ),
            new Tuple<int, Color>(745    ,   Color.FromArgb(255,240,197)    ),
            new Tuple<int, Color>(746    ,   Color.FromArgb(246,234,219)    ),
            new Tuple<int, Color>(747    ,   Color.FromArgb(240,247,239)    ),
            new Tuple<int, Color>(754    ,   Color.FromArgb(251,227,209)    ),
            new Tuple<int, Color>(758    ,   Color.FromArgb(255,177,147)    ),
            new Tuple<int, Color>(760    ,   Color.FromArgb(249,160,146)    ),
            new Tuple<int, Color>(761    ,   Color.FromArgb(255,201,188)    ),
            new Tuple<int, Color>(762    ,   Color.FromArgb(232,232,229)    ),
            new Tuple<int, Color>(772    ,   Color.FromArgb(231,249,203)    ),
            new Tuple<int, Color>(775    ,   Color.FromArgb(247,246,248)    ),
            new Tuple<int, Color>(776    ,   Color.FromArgb(255,177,174)    ),
            new Tuple<int, Color>(778    ,   Color.FromArgb(255,199,184)    ),
            new Tuple<int, Color>(780    ,   Color.FromArgb(181,98,46)      ),
            new Tuple<int, Color>(781    ,   Color.FromArgb(181,107,56)     ),
            new Tuple<int, Color>(782    ,   Color.FromArgb(204,119,66)     ),
            new Tuple<int, Color>(783    ,   Color.FromArgb(225,146,85)     ),
            new Tuple<int, Color>(791    ,   Color.FromArgb(71,55,93)       ),
            new Tuple<int, Color>(792    ,   Color.FromArgb(97,97,128)      ),
            new Tuple<int, Color>(793    ,   Color.FromArgb(147,139,164)    ),
            new Tuple<int, Color>(794    ,   Color.FromArgb(187,208,218)    ),
            new Tuple<int, Color>(796    ,   Color.FromArgb(30,58,95)       ),
            new Tuple<int, Color>(797    ,   Color.FromArgb(30,66,99)       ),
            new Tuple<int, Color>(798    ,   Color.FromArgb(103,115,141)    ),
            new Tuple<int, Color>(799    ,   Color.FromArgb(132,156,182)    ),
            new Tuple<int, Color>(800    ,   Color.FromArgb(233,238,233)    ),
            new Tuple<int, Color>(801    ,   Color.FromArgb(123,71,20)      ),
            new Tuple<int, Color>(806    ,   Color.FromArgb(30,130,133)     ),
            new Tuple<int, Color>(807    ,   Color.FromArgb(128,167,160)    ),
            new Tuple<int, Color>(809    ,   Color.FromArgb(190,193,205)    ),
            new Tuple<int, Color>(813    ,   Color.FromArgb(175,195,205)    ),
            new Tuple<int, Color>(814    ,   Color.FromArgb(162,0,88)       ),
            new Tuple<int, Color>(815    ,   Color.FromArgb(166,0,91)       ),
            new Tuple<int, Color>(816    ,   Color.FromArgb(179,0,91)       ),
            new Tuple<int, Color>(817    ,   Color.FromArgb(219,24,85)      ),
            new Tuple<int, Color>(818    ,   Color.FromArgb(255,234,235)    ),
            new Tuple<int, Color>(819    ,   Color.FromArgb(248,247,221)    ),
            new Tuple<int, Color>(820    ,   Color.FromArgb(30,54,85)       ),
            new Tuple<int, Color>(822    ,   Color.FromArgb(242,234,219)    ),
            new Tuple<int, Color>(823    ,   Color.FromArgb(0,0,73)         ),
            new Tuple<int, Color>(824    ,   Color.FromArgb(71,97,116)      ),
            new Tuple<int, Color>(825    ,   Color.FromArgb(85,108,128)     ),
            new Tuple<int, Color>(826    ,   Color.FromArgb(115,138,153)    ),
            new Tuple<int, Color>(827    ,   Color.FromArgb(213,231,232)    ),
            new Tuple<int, Color>(828    ,   Color.FromArgb(237,247,238)    ),
            new Tuple<int, Color>(829    ,   Color.FromArgb(130,90,8)       ),
            new Tuple<int, Color>(830    ,   Color.FromArgb(136,95,18)      ),
            new Tuple<int, Color>(831    ,   Color.FromArgb(144,103,18)     ),
            new Tuple<int, Color>(832    ,   Color.FromArgb(178,119,55)     ),
            new Tuple<int, Color>(833    ,   Color.FromArgb(219,182,128)    ),
            new Tuple<int, Color>(834    ,   Color.FromArgb(242,209,142)    ),
            new Tuple<int, Color>(838    ,   Color.FromArgb(94,56,27)       ),
            new Tuple<int, Color>(839    ,   Color.FromArgb(109,66,39)      ),
            new Tuple<int, Color>(840    ,   Color.FromArgb(128,85,30)      ),
            new Tuple<int, Color>(841    ,   Color.FromArgb(188,134,107)    ),
            new Tuple<int, Color>(842    ,   Color.FromArgb(219,194,164)    ),
            new Tuple<int, Color>(844    ,   Color.FromArgb(107,103,102)    ),
            new Tuple<int, Color>(868    ,   Color.FromArgb(153,92,48)      ),
            new Tuple<int, Color>(869    ,   Color.FromArgb(153,92,48)      ),
            new Tuple<int, Color>(890    ,   Color.FromArgb(79,86,76)       ),
            new Tuple<int, Color>(891    ,   Color.FromArgb(241,49,84)      ),
            new Tuple<int, Color>(892    ,   Color.FromArgb(249,90,97)      ),
            new Tuple<int, Color>(893    ,   Color.FromArgb(243,149,157)    ),
            new Tuple<int, Color>(894    ,   Color.FromArgb(255,194,191)    ),
            new Tuple<int, Color>(895    ,   Color.FromArgb(89,92,78)       ),
            new Tuple<int, Color>(898    ,   Color.FromArgb(118,55,19)      ),
            new Tuple<int, Color>(899    ,   Color.FromArgb(233,109,115)    ),
            new Tuple<int, Color>(900    ,   Color.FromArgb(206,43,0)       ),
            new Tuple<int, Color>(902    ,   Color.FromArgb(138,24,77)      ),
            new Tuple<int, Color>(904    ,   Color.FromArgb(78,95,57)       ),
            new Tuple<int, Color>(905    ,   Color.FromArgb(98,119,57)      ),
            new Tuple<int, Color>(906    ,   Color.FromArgb(143,163,89)     ),
            new Tuple<int, Color>(907    ,   Color.FromArgb(185,200,102)    ),
            new Tuple<int, Color>(909    ,   Color.FromArgb(49,105,85)      ),
            new Tuple<int, Color>(910    ,   Color.FromArgb(48,116,91)      ),
            new Tuple<int, Color>(911    ,   Color.FromArgb(49,128,97)      ),
            new Tuple<int, Color>(912    ,   Color.FromArgb(115,158,115)    ),
            new Tuple<int, Color>(913    ,   Color.FromArgb(153,188,149)    ),
            new Tuple<int, Color>(915    ,   Color.FromArgb(170,24,91)      ),
            new Tuple<int, Color>(917    ,   Color.FromArgb(171,22,95)      ),
            new Tuple<int, Color>(918    ,   Color.FromArgb(168,68,76)      ),
            new Tuple<int, Color>(919    ,   Color.FromArgb(180,75,82)      ),
            new Tuple<int, Color>(920    ,   Color.FromArgb(197,94,88)      ),
            new Tuple<int, Color>(921    ,   Color.FromArgb(206,103,91)     ),
            new Tuple<int, Color>(922    ,   Color.FromArgb(237,134,115)    ),
            new Tuple<int, Color>(924    ,   Color.FromArgb(86,99,100)      ),
            new Tuple<int, Color>(926    ,   Color.FromArgb(96,116,115)     ),
            new Tuple<int, Color>(927    ,   Color.FromArgb(200,198,194)    ),
            new Tuple<int, Color>(928    ,   Color.FromArgb(225,224,216)    ),
            new Tuple<int, Color>(930    ,   Color.FromArgb(102,122,140)    ),
            new Tuple<int, Color>(931    ,   Color.FromArgb(124,135,145)    ),
            new Tuple<int, Color>(932    ,   Color.FromArgb(182,186,194)    ),
            new Tuple<int, Color>(934    ,   Color.FromArgb(62,59,40)       ),
            new Tuple<int, Color>(935    ,   Color.FromArgb(67,63,47)       ),
            new Tuple<int, Color>(936    ,   Color.FromArgb(69,69,49)       ),
            new Tuple<int, Color>(937    ,   Color.FromArgb(73,86,55)       ),
            new Tuple<int, Color>(938    ,   Color.FromArgb(99,39,16)       ),
            new Tuple<int, Color>(939    ,   Color.FromArgb(0,0,49)         ),
            new Tuple<int, Color>(943    ,   Color.FromArgb(0,162,117)      ),
            new Tuple<int, Color>(945    ,   Color.FromArgb(255,206,164)    ),
            new Tuple<int, Color>(946    ,   Color.FromArgb(244,73,0)       ),
            new Tuple<int, Color>(947    ,   Color.FromArgb(255,91,0)       ),
            new Tuple<int, Color>(948    ,   Color.FromArgb(255,243,231)    ),
            new Tuple<int, Color>(950    ,   Color.FromArgb(239,162,127)    ),
            new Tuple<int, Color>(951    ,   Color.FromArgb(255,229,188)    ),
            new Tuple<int, Color>(954    ,   Color.FromArgb(170,213,164)    ),
            new Tuple<int, Color>(955    ,   Color.FromArgb(214,230,204)    ),
            new Tuple<int, Color>(956    ,   Color.FromArgb(255,109,115)    ),
            new Tuple<int, Color>(957    ,   Color.FromArgb(255,204,208)    ),
            new Tuple<int, Color>(958    ,   Color.FromArgb(0,160,130)      ),
            new Tuple<int, Color>(959    ,   Color.FromArgb(171,206,177)    ),
            new Tuple<int, Color>(961    ,   Color.FromArgb(243,108,123)    ),
            new Tuple<int, Color>(962    ,   Color.FromArgb(253,134,141)    ),
            new Tuple<int, Color>(963    ,   Color.FromArgb(255,233,233)    ),
            new Tuple<int, Color>(964    ,   Color.FromArgb(208,224,210)    ),
            new Tuple<int, Color>(966    ,   Color.FromArgb(206,213,176)    ),
            new Tuple<int, Color>(970    ,   Color.FromArgb(255,117,24)     ),
            new Tuple<int, Color>(971    ,   Color.FromArgb(255,106,0)      ),
            new Tuple<int, Color>(972    ,   Color.FromArgb(255,146,0)      ),
            new Tuple<int, Color>(973    ,   Color.FromArgb(255,194,67)     ),
            new Tuple<int, Color>(975    ,   Color.FromArgb(158,67,18)      ),
            new Tuple<int, Color>(976    ,   Color.FromArgb(246,141,57)     ),
            new Tuple<int, Color>(977    ,   Color.FromArgb(255,164,73)     ),
            new Tuple<int, Color>(986    ,   Color.FromArgb(58,82,65)       ),
            new Tuple<int, Color>(987    ,   Color.FromArgb(83,97,73)       ),
            new Tuple<int, Color>(988    ,   Color.FromArgb(134,145,110)    ),
            new Tuple<int, Color>(989    ,   Color.FromArgb(134,153,110)    ),
            new Tuple<int, Color>(991    ,   Color.FromArgb(47,91,73)       ),
            new Tuple<int, Color>(992    ,   Color.FromArgb(146,183,165)    ),
            new Tuple<int, Color>(993    ,   Color.FromArgb(192,224,200)    ),
            new Tuple<int, Color>(995    ,   Color.FromArgb(0,123,134)      ),
            new Tuple<int, Color>(996    ,   Color.FromArgb(170,222,225)    ),
            new Tuple<int, Color>(3011   ,   Color.FromArgb(123,91,64)      ),
            new Tuple<int, Color>(3012   ,   Color.FromArgb(170,134,103)    ),
            new Tuple<int, Color>(3013   ,   Color.FromArgb(208,195,164)    ),
            new Tuple<int, Color>(3021   ,   Color.FromArgb(115,91,93)      ),
            new Tuple<int, Color>(3022   ,   Color.FromArgb(172,172,170)    ),
            new Tuple<int, Color>(3023   ,   Color.FromArgb(198,190,173)    ),
            new Tuple<int, Color>(3024   ,   Color.FromArgb(210,208,205)    ),
            new Tuple<int, Color>(3031   ,   Color.FromArgb(84,56,23)       ),
            new Tuple<int, Color>(3032   ,   Color.FromArgb(188,156,120)    ),
            new Tuple<int, Color>(3033   ,   Color.FromArgb(239,219,190)    ),
            new Tuple<int, Color>(3041   ,   Color.FromArgb(190,155,167)    ),
            new Tuple<int, Color>(3042   ,   Color.FromArgb(225,205,200)    ),
            new Tuple<int, Color>(3045   ,   Color.FromArgb(216,151,105)    ),
            new Tuple<int, Color>(3046   ,   Color.FromArgb(229,193,139)    ),
            new Tuple<int, Color>(3047   ,   Color.FromArgb(255,236,211)    ),
            new Tuple<int, Color>(3051   ,   Color.FromArgb(85,73,0)        ),
            new Tuple<int, Color>(3052   ,   Color.FromArgb(137,141,114)    ),
            new Tuple<int, Color>(3053   ,   Color.FromArgb(187,179,148)    ),
            new Tuple<int, Color>(3064   ,   Color.FromArgb(194,101,76)     ),
            new Tuple<int, Color>(3072   ,   Color.FromArgb(233,233,223)    ),
            new Tuple<int, Color>(3078   ,   Color.FromArgb(255,255,220)    ),
            new Tuple<int, Color>(3325   ,   Color.FromArgb(202,226,229)    ),
            new Tuple<int, Color>(3326   ,   Color.FromArgb(255,157,150)    ),
            new Tuple<int, Color>(3328   ,   Color.FromArgb(188,64,85)      ),
            new Tuple<int, Color>(3340   ,   Color.FromArgb(255,123,103)    ),
            new Tuple<int, Color>(3341   ,   Color.FromArgb(235,172,162)    ),
            new Tuple<int, Color>(3345   ,   Color.FromArgb(97,100,82)      ),
            new Tuple<int, Color>(3346   ,   Color.FromArgb(120,134,107)    ),
            new Tuple<int, Color>(3347   ,   Color.FromArgb(128,152,115)    ),
            new Tuple<int, Color>(3348   ,   Color.FromArgb(225,249,190)    ),
            new Tuple<int, Color>(3350   ,   Color.FromArgb(201,79,91)      ),
            new Tuple<int, Color>(3354   ,   Color.FromArgb(255,214,209)    ),
            new Tuple<int, Color>(3362   ,   Color.FromArgb(96,95,84)       ),
            new Tuple<int, Color>(3363   ,   Color.FromArgb(116,127,96)     ),
            new Tuple<int, Color>(3364   ,   Color.FromArgb(161,167,135)    ),
            new Tuple<int, Color>(3371   ,   Color.FromArgb(83,37,16)       ),
            new Tuple<int, Color>(3607   ,   Color.FromArgb(231,79,134)     ),
            new Tuple<int, Color>(3608   ,   Color.FromArgb(247,152,182)    ),
            new Tuple<int, Color>(3609   ,   Color.FromArgb(255,214,229)    ),
            new Tuple<int, Color>(3685   ,   Color.FromArgb(161,53,79)      ),
            new Tuple<int, Color>(3687   ,   Color.FromArgb(203,78,97)      ),
            new Tuple<int, Color>(3688   ,   Color.FromArgb(250,151,144)    ),
            new Tuple<int, Color>(3689   ,   Color.FromArgb(255,213,216)    ),
            new Tuple<int, Color>(3705   ,   Color.FromArgb(255,85,91)      ),
            new Tuple<int, Color>(3706   ,   Color.FromArgb(255,128,109)    ),
            new Tuple<int, Color>(3708   ,   Color.FromArgb(254,212,219)    ),
            new Tuple<int, Color>(3712   ,   Color.FromArgb(230,101,107)    ),
            new Tuple<int, Color>(3713   ,   Color.FromArgb(253,229,217)    ),
            new Tuple<int, Color>(3716   ,   Color.FromArgb(255,211,212)    ),
            new Tuple<int, Color>(3721   ,   Color.FromArgb(184,75,77)      ),
            new Tuple<int, Color>(3722   ,   Color.FromArgb(184,89,88)      ),
            new Tuple<int, Color>(3726   ,   Color.FromArgb(195,118,123)    ),
            new Tuple<int, Color>(3727   ,   Color.FromArgb(255,199,196)    ),
            new Tuple<int, Color>(3731   ,   Color.FromArgb(209,93,103)     ),
            new Tuple<int, Color>(3733   ,   Color.FromArgb(255,154,148)    ),
            new Tuple<int, Color>(3740   ,   Color.FromArgb(156,125,133)    ),
            new Tuple<int, Color>(3743   ,   Color.FromArgb(235,235,231)    ),
            new Tuple<int, Color>(3746   ,   Color.FromArgb(149,102,162)    ),
            new Tuple<int, Color>(3747   ,   Color.FromArgb(230,236,232)    ),
            new Tuple<int, Color>(3750   ,   Color.FromArgb(12,91,108)      ),
            new Tuple<int, Color>(3752   ,   Color.FromArgb(194,209,206)    ),
            new Tuple<int, Color>(3753   ,   Color.FromArgb(237,247,247)    ),
            new Tuple<int, Color>(3755   ,   Color.FromArgb(158,176,206)    ),
            new Tuple<int, Color>(3756   ,   Color.FromArgb(248,248,252)    ),
            new Tuple<int, Color>(3760   ,   Color.FromArgb(102,142,152)    ),
            new Tuple<int, Color>(3761   ,   Color.FromArgb(227,234,230)    ),
            new Tuple<int, Color>(3765   ,   Color.FromArgb(24,128,134)     ),
            new Tuple<int, Color>(3766   ,   Color.FromArgb(24,101,111)     ),
            new Tuple<int, Color>(3768   ,   Color.FromArgb(92,110,108)     ),
            new Tuple<int, Color>(3770   ,   Color.FromArgb(255,250,224)    ),
            new Tuple<int, Color>(3772   ,   Color.FromArgb(173,83,62)      ),
            new Tuple<int, Color>(3773   ,   Color.FromArgb(231,134,103)    ),
            new Tuple<int, Color>(3774   ,   Color.FromArgb(255,220,193)    ),
            new Tuple<int, Color>(3776   ,   Color.FromArgb(221,109,91)     ),
            new Tuple<int, Color>(3777   ,   Color.FromArgb(191,64,36)      ),
            new Tuple<int, Color>(3778   ,   Color.FromArgb(237,122,100)    ),
            new Tuple<int, Color>(3779   ,   Color.FromArgb(255,177,152)    ),
            new Tuple<int, Color>(3781   ,   Color.FromArgb(113,71,42)      ),
            new Tuple<int, Color>(3782   ,   Color.FromArgb(206,175,144)    ),
            new Tuple<int, Color>(3787   ,   Color.FromArgb(139,109,115)    ),
            new Tuple<int, Color>(3790   ,   Color.FromArgb(140,117,109)    ),
            new Tuple<int, Color>(3799   ,   Color.FromArgb(81,76,83)       ),
        };

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
                    
                    int WidthImage = imageDeBase.Width;
                    int HeightImage = imageDeBase.Height;
                    TotalPerleInputBox.Text = $"{WidthImage * HeightImage}";

                    TotalPerleInputBox.Enabled = true;
                    ConvertirButton.Enabled = true;
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            int NumberOfPerlesExpected;
            if (!int.TryParse(TotalPerleInputBox.Text, out NumberOfPerlesExpected))
            {
                Console.WriteLine($"Invalid number of perle: {TotalPerleInputBox.Text}");
                return;
            }

            ConvertirButton.Enabled = false;
            ChoisirUneImageButton.Enabled = false;

            // Un peu d'imprecision ici... passage en double?
            double imageRatio = (double)imageDeBase.Width / (double)imageDeBase.Height;
            int resizedWidth = (int)Math.Sqrt((double)NumberOfPerlesExpected * imageRatio);
            int resizedHeight = (int)(resizedWidth / imageRatio);

            Console.WriteLine($"Conversion from: {imageDeBase.Width}x{imageDeBase.Height} to {resizedWidth}x{resizedHeight}. Ratio: {imageRatio}");
            TotalPerlesFinalInputBox.Text = $"{resizedWidth * resizedHeight}";

            Bitmap imageReSize = new Bitmap(imageDeBase, resizedWidth, resizedHeight);
            int w = imageReSize.Width;
            int h = imageReSize.Height;

            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Value = 1;
            progressBar.Step = 1;

            //generation image finale
            if (imageFinale != null)
                imageFinale.Dispose();

            // On doit garder cette image pour pas crash dans la preview
            imageFinale = new Bitmap(w * 40, h * 40);

            SemaphoreSlim GetPixelLocker = new SemaphoreSlim(1, 1);
            SemaphoreSlim WriteImageLocker = new SemaphoreSlim(1, 1);

            using (Graphics g = Graphics.FromImage(imageFinale))
            {
                StringFormat formatString = new StringFormat();
                formatString.Alignment = StringAlignment.Center;
                formatString.LineAlignment = StringAlignment.Center;

                const int fontSize = 10;
                Font fontString = new Font("Arial", fontSize);
                SolidBrush brushString = new SolidBrush(Color.Black);
                Pen penRectangle = new Pen(Color.Black, 3);

                int batches = 250;
                int nbIterations = w * h / batches;
                progressBar.Maximum = nbIterations;

                var cts = new CancellationTokenSource();
                var token = cts.Token;

                // Create a temporary "thread" to dissociate from the current one
                await Task.Factory.StartNew(() =>
                {
                    // Create job batches
                    Parallel.For(0, w * h / batches, (i) =>
                    {
                        Rectangle tempRect = new Rectangle(0, 0, 40, 40);
                        SolidBrush brushRectangle = new SolidBrush(Color.White);
                        for (int index = i * batches; index < (i + 1) * batches; ++index)
                        {
                            int x = index % w;
                            int y = index / w;

                            GetPixelLocker.Wait();
                            Color color = imageReSize.GetPixel(x, y);
                            GetPixelLocker.Release();

                            var colorPair = ClosestColor(DMCPairs, color);
                            DMCRGB dmcrgb = new DMCRGB(colorPair.Item1, colorPair.Item2);

                            tempRect.X = x * 40;
                            tempRect.Y = y * 40;

                            brushRectangle.Color = dmcrgb.RGB;

                            WriteImageLocker.Wait();
                            g.FillRectangle(brushRectangle, tempRect);
                            g.DrawString(dmcrgb.indexAsString, fontString, brushString, tempRect, formatString);
                            g.DrawRectangle(penRectangle, tempRect);
                            WriteImageLocker.Release();
                        }

                        progressBar.BeginInvoke(new Action(() => progressBar.PerformStep()));
                });
                }, token, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
            }

            imageReSize.Dispose();

            imageFinale.Save(System.IO.Path.ChangeExtension(filePath, null) + "DMC" + System.IO.Path.GetExtension(filePath));
            ImagePreviewBox.Image = imageFinale;

            progressBar.Step = 1;
            progressBar.Value = 1;
            progressBar.Visible = false;

            ChoisirUneImageButton.Enabled = true;
        }
    }
}

public class DMCRGB
{
    public DMCRGB(int indexIn, Color RGBIn)
    {
        index = indexIn;
        indexAsString = indexIn.ToString();
        RGB = RGBIn;
    }
    public int index;
    public string indexAsString;
    public Color RGB;
}
