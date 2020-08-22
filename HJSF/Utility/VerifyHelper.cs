using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Utility
{
    public static class VerifyHelper
    {
        /// <summary>
        /// 验证码保存名称
        /// </summary>
        private const string _verifyCookieName = "code";
        /// <summary>
        /// 验证码文字颜色列表
        /// </summary>
        private static List<Color> _verifyFontColors = new List<Color> { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        /// <summary>
        /// 验证码字体列表
        /// </summary>
        private static string[] _verifyFonts = new string[] { "Arial", "Arial Black", "comic sans ms", "courier new", "微软雅黑", "黑体", "楷体", "Microsoft YaHei UI", "estrangelo edessa", "franklin gothic medium", "georgia", "lucida console", "lucida sans unicode", "mangal", "microsoft sans serif", "palatino linotype", "sylfaen", "tahoma", "times new roman", "trebuchet ms", "verdana" };


      

        #region 产生波形滤镜效果 
        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2XPI)</param>
        /// <returns></returns>
        private static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            //const double PI = 3.1415926535897932384626433832795;
            const double PI2 = 6.283185307179586476925286766559;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            // 将位图背景填充为白色 
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    int nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    // 取得当前点的颜色 
                    int nOldY = bXDir ? j : j + (int)(dy * dMultValue);
                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }
        #endregion

        #region 生成网站验证码
        /// <summary>
        /// 生成网站验证码
        /// </summary>
        public static byte[] Create()
        {
            string _code = Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();

           
            int fWidth = 42;
            int imageWidth = (int)(_code.Length * fWidth) + 4 + 2 * 2;
            int imageHeight = 40 * 2 + 2;
            using (Bitmap image = new Bitmap(imageWidth, imageHeight))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.Clear(Color.White);
                    Random rand = new Random();
                    int cindex, findex;
                    //给背景添加随机生成的燥点 

                    Pen pen = new Pen(Color.FromArgb(186, 199, 210), 2);

                    //int c = 4 * 20;
                    for (int i = 0; i < 40; i++)
                    {
                        cindex = rand.Next(_verifyFontColors.Count - 1);
                        int x = rand.Next(image.Width);
                        int y = rand.Next(image.Height);
                        g.DrawRectangle(pen, x, y, 2, 4);
                        pen = new Pen(_verifyFontColors[cindex], 2);
                    }
                    pen.Dispose();

                    int n1 = (imageHeight - 44);
                    int n2 = n1 / 4;
                    int top1 = n2;
                    int top2 = n2 * 2;
                    Font f = null;
                    Brush b = null;
                    //随机字体和颜色的验证码字符 
                    for (int i = 0; i < _code.Length; i++)
                    {
                        cindex = rand.Next(_verifyFontColors.Count - 1);
                        findex = rand.Next(_verifyFonts.Length - 1);
                        f = new Font(_verifyFonts[findex], 40, FontStyle.Bold);
                        b = new SolidBrush(_verifyFontColors[cindex]);
                        int top;
                        if (i % 2 == 1)
                        {
                            top = top2;
                        }
                        else
                        {
                            top = top1;
                        }
                        int left = i * fWidth;
                        g.DrawString(_code.Substring(i, 1), f, b, left, top);
                    }

                    f?.Dispose();
                    b?.Dispose();

                    //产生波形
                    //image = TwistImage(image, true, 3, 4);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }

        }
        #endregion
 
    }
}
