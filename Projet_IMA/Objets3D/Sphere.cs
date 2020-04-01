using System;

namespace Projet_IMA
{ 
    public class Sphere : Objet3D
    {
        private V3 center;
        private float rayon;
        private Materiel materiel;

        //constructeur
        public Sphere(V3 pCenter, float pRayon, Materiel pMateriel)
	    {
            center = pCenter;
            rayon = pRayon;
            materiel = pMateriel;
        }

        public V3 getPixel3DPosition(float u, float v)
        {
            return new V3(Math.Cos(v) * Math.Cos(u), Math.Cos(v) * Math.Sin(u), Math.Sin(v));
        }
        public void Partial_derivative(float u, float v, out V3 dMdu, out V3 dMdv)
        {
            float dxdu, dydu, dzdu, dxdv, dydv, dzdv;
            dxdu = (float)(Math.Cos(v) * -Math.Sin(u));
            dydu = (float)(Math.Cos(v) * Math.Cos(u));
            dzdu = (float)0;
            dMdu = new V3(dxdu, dydu, dzdu);

            dxdv = (float)(-Math.Sin(v) * (Math.Cos(u)));
            dydv = (float)(-Math.Sin(v) * (Math.Sin(u)));
            dzdv = (float)(Math.Cos(v));
            dMdv = new V3(dxdv, dydv, dzdv);

            dMdu.Normalize(); dMdv.Normalize();
        }
        public V3 getNormal(float u, float v)
        {
            V3 normal = getPixel3DPosition(u, v);
            normal.Normalize();
            return normal;
        }

        override public  float gettIntersect(V3 OrigineRayon, V3 DirRayon)
        {
            float t1,t2,A,B,D,Determinant,t;
            t = -1;

            A = DirRayon * DirRayon;
            B = (2 * OrigineRayon * DirRayon) - (2 * DirRayon * center);
            D = (OrigineRayon * OrigineRayon) - (2 * OrigineRayon * center) + (center * center) - (rayon * rayon);
            //D = (PosCamera * PosCamera) + (center * center) + (2 * PosCamera * center) - (rayon * rayon);

            //Console.WriteLine("    DirRayon: " + DirRayon.x +", "+ DirRayon.y + ", " + DirRayon.z);
            //Console.WriteLine("    A: " + A  + "; B: " + B  + "; D: " + D);

            Determinant = (B * B) - (4 * A * D);
            //Console.WriteLine("    Determinant: " + Determinant); 
            if (Determinant > 0 )
            {
                t1 = (-B - (float)Math.Sqrt(Determinant) )  /  (2 * A);
                t2 = (-B + (float)Math.Sqrt(Determinant) )  /  (2 * A);

                //Console.WriteLine("    T1: " + t1 + ", T2: " + t2);

                if (t1 > 0 && t2 > 0)
                {
                    t = t1;
                }
                else if (t1 < 0 && t2 > 0)
                {
                    t = t2;
                }
                else if (t1 < 0 && t2 < 0)
                {
                    t = -1;
                }
                return t;
            }
            if (Determinant == 0)
            {
                t = -B/ (2*A);
                return t;
            }
            else
            {
                return -1;
            }
        }

        override public Couleur getCouleurRaycast(V3 PointIntersection, Scene scene)
        {
            //Console.WriteLine("Sphere Pt Intersect: " + PointIntersection.x + " " + PointIntersection.y + " " + PointIntersection.z);

            V3 pixel_position = PointIntersection - center;
            //Console.WriteLine("                      " + PointIntersection.x + " " + PointIntersection.y + " " + PointIntersection.z);

            IMA.Invert_Coord_Spherique(pixel_position, rayon, out float u, out float v);
            //Console.WriteLine("        u: " + u+" ,v " + v);



            //------------------------------BUMP MAPING---------------------------------------//
            float pi = (float)Math.PI;
            float uNormalise = u / (2 * pi);
            float vNormalise = v / pi + pi;

            Partial_derivative(u, v, out V3 dMdu, out V3 dMdv);
            materiel.GetTextureBump().Bump(uNormalise, vNormalise, out float dhdu, out float dhdv);
            V3 normal = getNormal(u, v) + materiel.GetForceBumping() * (dhdu * (getNormal(u, v) ^ dMdv) + dhdv * (getNormal(u, v) ^ dMdu));

            //------------------------------TEXTURE---------------------------------------//
            Couleur couleur = new Couleur();
            if (materiel.GetTexture() != null)
                couleur = materiel.GetTexture().LireCouleur(uNormalise, vNormalise);
            else
                couleur = materiel.GetCouleur();

            //------------------------------LUMIERE---------------------------------------//
            couleur = scene.CouleurEclairee(PointIntersection, normal, couleur, materiel, this);

            return couleur;
        }
    }
}
