using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    public struct V3
    {
        public float x;		// coordonnées du vecteur
        public float y;
        public float z;

        public float Norm()		// retourne la norme du vecteur
        {
            return (float) IMA.Sqrtf(x * x + y * y + z * z);
        }

        public float Norme2()
        {
            return x * x + y * y + z * z;
        }

        public void Normalize()	// normalise le vecteur
        {
            float n = Norm();
            if (n == 0) return;
            x /= n;
            y /= n;
            z /= n;
        }

        public V3(V3 t)
        {
            x = t.x;
            y = t.y;
            z = t.z;
        }

        public V3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public V3(int _x, int _y, int _z)
        {
            x = (float)_x;
            y = (float)_y;
            z = (float)_z;
        }

        public V3(double _x, double _y, double _z)
        {
            x = (float)_x;
            y = (float)_y;
            z = (float)_z;
        }

        //OPERATORS

        public static V3 operator +(V3 a, V3 b)
        {
            V3 t;
            t.x = a.x + b.x;
            t.y = a.y + b.y;
            t.z = a.z + b.z;
            return t;
        }

        public static V3 operator -(V3 a, V3 b)
        {
            V3 t;
            t.x = a.x - b.x;
            t.y = a.y - b.y;
            t.z = a.z - b.z;
            return t;
        }

        public static V3 operator -(V3 a)
        {
            V3 t;
            t.x = -a.x;
            t.y = -a.y;
            t.z = -a.z;
            return t;
        }

        public static V3 operator ^ (V3 a, V3 b)  // produit vectoriel
        {
            V3 t;
            t.x = a.y * b.z - a.z * b.y;
            t.y = a.z * b.x - a.x * b.z;
            t.z = a.x * b.y - a.y * b.x;
            return t;
        }

        public static float operator * (V3 a,V3 b)         // produit scalaire
        {
            return a.x*b.x+a.y*b.y+a.z*b.z;
        }

       

        public static V3 operator *(float a, V3 b)
        {
            V3 t;
            t.x = b.x*a;
            t.y = b.y*a;
            t.z=  b.z*a;
            return t;
        }

        public static V3 operator *(V3 b, float a)
        {
            V3 t;
            t.x = b.x*a;
            t.y = b.y*a;
            t.z=  b.z*a;
            return t;
        }

        public static V3 operator /(V3 b, float a)
        {
            V3 t;
            t.x = b.x/a;
            t.y = b.y/a;
            t.z=  b.z/a;
            return t;
        }

        public static float prod_scal(ref V3 u, ref V3 v)
        {
            return u.x * v.x + u.y * v.y + u.z * v.z;
        }
    }
}
