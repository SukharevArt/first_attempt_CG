using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace first_attempt
{
    public class InvertFilter
    {
        public static Bitmap Execute(Bitmap sourceImage) {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int n = sourceImage.Width;
            int m = sourceImage.Height;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++) {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(sourceImage.GetPixel(i, j)));
                }
            return resultImage;
        }
        protected static Color CalculateNewColorPixel(Color tColor) {
            return Color.FromArgb(255 - tColor.R, 255 - tColor.G, 255 - tColor.B);
        }
    }
}

namespace first_attempt
{
    public class SepiaFilter
    {
        public static Bitmap Execute(Bitmap sourceImage) {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int n = sourceImage.Width;
            int m = sourceImage.Height;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++) {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(sourceImage.GetPixel(i,j)));
                }
            return resultImage;
        }
        protected static int InRange(int val, int l, int r) {
            if (val < l)
                return l;
            if (val > r)
                return r;
            return val;
        }
        protected static Color CalculateNewColorPixel(Color tColor) {
            double R = tColor.R;
            double G = tColor.G;
            double B = tColor.B;

            int intens =(int)( 0.299 * R + 0.587 * G + 0.114 * B);
            
            return Color.FromArgb(InRange(intens + 40, 0, 255), InRange(intens + 10, 0, 255), InRange(intens - 20, 0, 255));
        }
    }
}

namespace first_attempt
{
    public class GrayFilter
    {
        public static Bitmap Execute(Bitmap sourceImage) {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            int n = sourceImage.Width;
            int m = sourceImage.Height;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++) {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(sourceImage.GetPixel(i,j)));
                }
            return resultImage;
        }
        protected static Color CalculateNewColorPixel(Color tColor) {
            double R = tColor.R;
            double G = tColor.G;
            double B = tColor.B;

            int intens =(int)( 0.299 * R + 0.587 * G + 0.114 * B);
            return Color.FromArgb(intens, intens, intens);
        }
    }
}
namespace first_attempt
{
    public class EmbFilter
    {
        public static Bitmap Execute(Bitmap source)
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            int n = source.Width;
            int m = source.Height;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(source, i, j));
                }
            return resultImage;
        }
        protected static int InRange(int val, int l, int r)
        {
            if (val < l)
                return l;
            if (val > r)
                return r;
            return val;
        }
        protected static Color CalculateNewColorPixel(Bitmap source, int x, int y)
        {
            int R = 0, G = 0, B = 0;
            Color tColor;
            int r = 1;
            if (x == 0)
                r = 0;
            
            {
                tColor = source.GetPixel(x - r, y);
                R += tColor.R;
                G += tColor.G;
                B += tColor.B;
            }
            r = 1;
            if (y == 0)
                r = 0;

            {
                tColor = source.GetPixel(x, y - r);
                R -= tColor.R;
                G -= tColor.G;
                B -= tColor.B;
            }
            r = 1;
            if (y == source.Height - 1)           
                r = 0;

            {
                tColor = source.GetPixel(x, y + r);
                R += tColor.R;
                G += tColor.G;
                B += tColor.B;
            }

            r = 1;
            if (x == source.Width - 1)
                r = 0;

            {
                tColor = source.GetPixel(x + r, y);
                R -= tColor.R;
                G -= tColor.G;
                B -= tColor.B;
            }

            R = InRange(R, 0, 255); G = InRange(G, 0, 255); B = InRange(B, 0, 255);
            int res = (int)(0.299 * (double)R + 0.587 * (double)G + 0.114 * (double)B);
            res = InRange(res+100, 0, 255);
            return Color.FromArgb(res, res, res);
        }
    }
}

namespace first_attempt
{
    public class MotionFilter
    {
        public static Bitmap Execute(Bitmap source)
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            int n = source.Width;
            int m = source.Height;
            
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(source, i, j));
                }
            return resultImage;
        }
        protected static int InRange(int val, int l, int r)
        {
            if (val < l)
                return l;
            if (val > r)
                return r;
            return val;
        }
        protected static Color CalculateNewColorPixel(Bitmap source, int x, int y)
        {
            int R = 0, G = 0, B = 0;
            Color tColor;
            int n = source.Width;
            int m = source.Height;
            for (int i = -4; i <= 4; i++)
            {
                int nx, ny;
                nx = x + i;
                ny = y - i;
                nx = InRange(nx, 0, n - 1);
                ny = InRange(ny, 0, m - 1);
                tColor = source.GetPixel(nx, ny);
                R += tColor.R;
                G += tColor.G;
                B += tColor.B;
            }   
            R = (int)Math.Round((double)R / 9);
            G = (int)Math.Round((double)G / 9);
            B = (int)Math.Round((double)B / 9);
            return Color.FromArgb(R, G, B);
        }
    }
}

namespace first_attempt
{
    public class AutoLevFilter
    {
        public static Bitmap Execute(Bitmap source)
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            int n = source.Width;
            int m = source.Height;

            int MNR = 255,MXR=0;
            int MNB = 255,MXB=0;
            int MNG = 255,MXG=0;
            Color tColor;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    tColor = source.GetPixel(i, j);
                    MNR = InRange(MNR, 0, tColor.R);
                    MNG = InRange(MNG, 0, tColor.G);
                    MNB = InRange(MNB, 0, tColor.B);
                    MXR = InRange(MXR, tColor.R, 255);
                    MXG = InRange(MXG, tColor.G, 255);
                    MXB = InRange(MXB, tColor.B, 255);
                }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(source, i, j, MNR, MXR, MNG, MXG, MNB, MXB));
                }
            return resultImage;
        }
        protected static int InRange(int val, int l, int r)
        {
            if (val < l)
                return l;
            if (val > r)
                return r;
            return val;
        }
        protected static Color CalculateNewColorPixel(Bitmap source, int x, int y, int MNR, int MXR, int MNG, int MXG, int MNB, int MXB)
        {
            Color tColor = source.GetPixel(x, y);
            int R = (tColor.R);
            int G = (tColor.G);
            int B = (tColor.B);
            R = (R - MNR) * 255 / (MXR - MNR);
            G = (G - MNG) * 255 / (MXG - MNG);
            B = (B - MNB) * 255 / (MXB - MNB);
            R = InRange(R, 0, 255);
            G = InRange(G, 0, 255);
            B = InRange(B, 0, 255);
            return Color.FromArgb(R, G, B);
        }
    }
}

namespace first_attempt
{
    public class DilationFilter
    {
        public static Bitmap Execute(Bitmap source)
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            int n = source.Width;
            int m = source.Height;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(source, i, j));
                }
            return resultImage;
        }
        protected static int InRange(int val, int l, int r)
        {
            if (val < l)
                return l;
            if (val > r)
                return r;
            return val;
        }
        protected static Color CalculateNewColorPixel(Bitmap source, int x, int y)
        {
            int n = source.Width;
            int m = source.Height;
            Color tColor = source.GetPixel(x, y);
            int R = (tColor.R);
            if (x>0) {
                tColor = source.GetPixel(x-1,y);
                if (R < tColor.R)
                    R = tColor.R;
            }
            if (y>0) {
                tColor = source.GetPixel(x,y-1);
                if (R < tColor.R)
                    R = tColor.R;
            }
            if (x < n - 1) {
                tColor = source.GetPixel(x+1, y);
                if (R < tColor.R)
                    R = tColor.R;
            }
            if (y < m - 1) {
                tColor = source.GetPixel(x, y+1);
                if (R < tColor.R)
                    R = tColor.R;
            }

            return Color.FromArgb(R, R, R);
        }
    }
}

namespace first_attempt
{
    public class SobelFilter
    {
        public static Bitmap Execute(Bitmap source)
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            int n = source.Width;
            int m = source.Height;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(source, i, j));
                }
            return resultImage;
        }
        protected static int InRange(int val, int l, int r)
        {
            if (val < l)
                return l;
            if (val > r)
                return r;
            return val;
        }
        protected static Color CalculateNewColorPixel(Bitmap source, int x, int y)
        {
            int n = source.Width;
            int m = source.Height;
            Color tColor = source.GetPixel(x, y);
            int R=0, G=0, B=0;
            int xR = 0, xG = 0 , xB = 0;
            int yR = 0, yG = 0 , yB = 0;
            int e = 1;
            for (int l = -1; l < 2; l++) {
                tColor = source.GetPixel(InRange(x-1,0,n-1),InRange(y+l,0,m-1));
                xR += -e * tColor.R;
                xG += -e * tColor.G;
                xB += -e * tColor.B;
                e = 3 - e;
            }
            e = 1;
            for (int l = -1; l < 2; l++)
            {
                tColor = source.GetPixel(InRange(x + 1, 0, n - 1), InRange(y + l, 0, m - 1));
                xR += e * tColor.R;
                xG += e * tColor.G;
                xB += e * tColor.B;
                e = 3 - e;
            }
            e = 1;
            for (int l = -1; l < 2; l++) {
                tColor = source.GetPixel(InRange(x+l,0,n-1),InRange(y-1,0,m-1));
                yR += -e * tColor.R;
                yG += -e * tColor.G;
                yB += -e * tColor.B;
                e = 3 - e;
            }
            e = 1;
            for (int l = -1; l < 2; l++)
            {
                tColor = source.GetPixel(InRange(x + l, 0, n - 1), InRange(y + 1, 0, m - 1));
                yR += e * tColor.R;
                yG += e * tColor.G;
                yB += e * tColor.B;
                e = 3 - e;
            }
            R = (int)Math.Sqrt((double)xR * xR + yR * yR);
            G = (int)Math.Sqrt((double)xG * xG + yG * yG);
            B = (int)Math.Sqrt((double)xB * xB + yB * yB);
            R = InRange(R, 0, 255);
            G = InRange(G, 0, 255);
            B = InRange(B, 0, 255);
            return Color.FromArgb(R, G, B);
        }
    }
}
namespace first_attempt
{
    public class ErosionFilter
    {
        public static Bitmap Execute(Bitmap source)
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            int n = source.Width;
            int m = source.Height;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(source, i, j));
                }
            return resultImage;
        }
        protected static int InRange(int val, int l, int r)
        {
            if (val < l)
                return l;
            if (val > r)
                return r;
            return val;
        }
        protected static Color CalculateNewColorPixel(Bitmap source, int x, int y)
        {
            int n = source.Width;
            int m = source.Height;
            Color tColor = source.GetPixel(x, y);
            int R = (tColor.R);
            if (x>0) {
                tColor = source.GetPixel(x-1,y);
                if (R > tColor.R)
                    R = tColor.R;
            }
            if (y>0) {
                tColor = source.GetPixel(x,y-1);
                if (R > tColor.R)
                    R = tColor.R;
            }
            if (x < n - 1) {
                tColor = source.GetPixel(x+1, y);
                if (R > tColor.R)
                    R = tColor.R;
            }
            if (y < m - 1) {
                tColor = source.GetPixel(x, y+1);
                if (R > tColor.R)
                    R = tColor.R;
            }

            return Color.FromArgb(R, R, R);
        }
    }
}

namespace first_attempt
{
    public class GrayWrFilter
    {
        public static Bitmap Execute(Bitmap source)
        {
            Bitmap resultImage = new Bitmap(source.Width, source.Height);
            int n = source.Width;
            int m = source.Height;

            float RR = 0, BB = 0, GG = 0;
            Color tColor;

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    tColor = source.GetPixel(i, j);
                    RR += (int)tColor.R;
                    BB += (int)tColor.B;
                    GG += (int)tColor.G;
                }
            RR /= n * m;
            BB /= n * m;
            GG /= n * m;
            float avg = RR + GG + BB;
            avg /= 3;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(source, i, j, RR, GG, BB, avg));
                }
            return resultImage;
        }
        protected static int InRange(int val, int l, int r)
        {
            if (val < l)
                return l;
            if (val > r)
                return r;
            return val;
        }
        protected static Color CalculateNewColorPixel(Bitmap source, int x, int y, float RR, float GG, float BB, float avg)
        {
            Color tColor = source.GetPixel(x, y);
            int R = (tColor.R); R = (int)((float)R * avg / RR);
            int G = (tColor.G); G = (int)((float)G * avg / GG);
            int B = (tColor.B); B = (int)((float)B * avg / BB);
            R = InRange(R, 0, 255);
            G = InRange(G, 0, 255);
            B = InRange(B, 0, 255);
            return Color.FromArgb(R, G, B);
        }
    }
}

namespace first_attempt
{
    public class ShiftFilter
    {
        public static Bitmap Execute(Bitmap source) {
            Bitmap resultImage = new Bitmap(source.Width+500, source.Height+500);
            int n = source.Width;
            int m = source.Height;
            for (int i = 50; i < n; i++)
                for (int j = 0; j < m; j++) {
                    resultImage.SetPixel(i, j, CalculateNewColorPixel(source.GetPixel(i-50,j)));
                }
            int o = 50;
            if (o > n)
                o = n;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++) {
                    resultImage.SetPixel(i, j, Color.Black);
                }
            
            return resultImage;
        }
        protected static Color CalculateNewColorPixel(Color tColor) {
            return tColor;
        }
    }
}
