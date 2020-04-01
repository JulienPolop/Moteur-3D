using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Projet_IMA
{
    public class Texture
    {
        public int Hauteur;
        public int Largeur;
        public Couleur [,] C;

        // public functions
        // u,v compris entre 0 et 1

        public Couleur LireCouleur(float u, float v)
        {
            return Interpol(Largeur * u, Hauteur * v);
        }

        public void Bump(float u, float v, out float dhdu, out float dhdv)
        {
            float x = u * Hauteur;
            float y = v * Largeur;

            float vv = Interpol(x, y).GreyLevel();
            float vx = Interpol(x + 1, y).GreyLevel();
            float vy = Interpol(x, y + 1).GreyLevel();

            dhdu = vx - vv;
            dhdv = vy - vv;
        }
    
        // constructor

        public Texture(string ff)
        {
            string s = System.IO.Path.GetFullPath("..\\..");
            string path = System.IO.Path.Combine(s,"textures",ff);
            Bitmap B = new Bitmap(path); 
            
            Hauteur = B.Height;
            Largeur = B.Width;
            BitmapData data = B.LockBits(new System.Drawing.Rectangle(0, 0, B.Width, B.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = data.Stride;
             
            C = new Couleur[Largeur,Hauteur];

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                for (int x = 0; x < Largeur; x++)
                    for (int y = 0; y < Hauteur; y++)
                    {
                        byte RR, VV, BB;
                        BB = ptr[(x * 3) + y * stride];
                        VV = ptr[(x * 3) + y * stride + 1];
                        RR = ptr[(x * 3) + y * stride + 2];
                        C[x, y].From255(RR, VV, BB);
                    }
            }
            B.UnlockBits(data);
            B.Dispose();
        }

       
                
                


        // private functions

        private Couleur Interpol(float Lu, float Hv)
        {
            int x = (int)(Lu);  // plus grand entier <=
            int y = (int)(Hv);

          //  float cx = Lu - x; // reste
          //  float cy = Hv - y;

            x = x % Largeur;
            y = y % Hauteur;
            if (x < 0) x += Largeur;
            if (y < 0) y += Hauteur;


            return C[x, y];

        /*    int xpu = (x + 1) % Largeur;
            int ypu = (y + 1) % Hauteur;

            float ccx = cx * cx;
            float ccy = cy * cy;

            return
              C[x, y] * (1 - ccx) * (1 - ccy)
            + C[xpu, y] * ccx * (1 - ccy)
            + C[x, ypu] * (1 - ccx) * ccy
            + C[xpu, ypu] * ccx * ccy;*/
        }
    }    
}
