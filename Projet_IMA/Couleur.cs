using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Projet_IMA
{
    public struct Couleur
    {
        public float R, V, B;	// composantes R,V,B comprises entre 0 et 1

        // constructeurs

        public void From255(byte RR, byte VV, byte BB)
        {
            R = (float)(RR / 255.0);
            V = (float)(VV / 255.0);
            B = (float)(BB / 255.0);
        }

        static public  void Transpose(ref Couleur cc, System.Drawing.Color c)
        {
            cc.R = (float) (c.R / 255.0);
            cc.V = (float) (c.G / 255.0);
            cc.B = (float) (c.B / 255.0);
        }

        public void check()
        {
            if (R > 1.0) R = 1.0f;
            if (V > 1.0) V = 1.0f;
            if (B > 1.0) B = 1.0f;
        }

        public void To255(out byte RR, out byte VV, out byte BB)
        {
            RR = (byte)(R * 255);
            VV = (byte)(V * 255);
            BB = (byte)(B * 255);
        }

        public Color Convertion()
        {
            check();
            byte RR, VV, BB;
            To255(out RR, out VV, out BB);
            return Color.FromArgb(RR, VV, BB);
        }

        public Couleur(float R, float V, float B)
        {
            this.R = R;
            this.V = V;
            this.B = B;
        }

        public Couleur(Couleur c)
        {
            this.R = c.R;
            this.V = c.V;
            this.B = c.B;
        }

        // méthodes

        public float GreyLevel()						// utile pour le Bump Map
        {
            return (R + B + V) / 3.0f;
        }

        // opérateurs surchargés

        public static Couleur operator +(Couleur a, Couleur b)
        {
            return new Couleur(a.R + b.R, a.V + b.V, a.B + b.B);
        }

        public static Couleur operator -(Couleur a, Couleur b)
        {
            return new Couleur(a.R - b.R, a.V - b.V, a.B - b.B);
        }

        public static Couleur operator -(Couleur a)
        {
            return new Couleur(-a.R, -a.V, -a.B);
        }

        public static Couleur operator *(Couleur a, Couleur b)
        {
            return new Couleur(a.R * b.R, a.V * b.V, a.B * b.B);
        }

        public static Couleur operator *(float a, Couleur b)
        {
            return new Couleur(a * b.R, a * b.V, a * b.B);
        }

        public static Couleur operator *(Couleur b, float a)
        {
            return new Couleur(a * b.R, a * b.V, a * b.B);
        }

        public static Couleur operator /(Couleur b, float a)
        {
            return new Couleur(b.R / a, b.V / a, b.B / a);
        }
    }
}

    

    				
