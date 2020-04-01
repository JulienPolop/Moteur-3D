using System;

namespace Projet_IMA
{
    public class Rectangle : Objet3D
    {
        private V3 A, B, C;
        private Materiel materiel;

        public Rectangle(V3 pA, V3 pB, V3 pC, Materiel pMateriel)
        {
            A = pA;
            B = pB;
            C = pC;
            materiel = pMateriel;
        }

        //affiche l'objet
        /**
        override public void Draw(Scene scene)
        {
            float TailleAB = B.x;
            float TailleAC = C.z;
            for (float u = 0; u <= 1; u += 1 / (TailleAB * 2))
            {
                for (float v = 0; v <= 1; v += 1 / (TailleAB * 2))
                {

                    V3 pixel_position = getPixel3DPosition(u, v);
                    //System.Console.WriteLine(pixel_position.x + " "+ pixel_position.y+" "+pixel_position.z);

                    if (BitmapEcran.DepthCheck(pixel_position))
                    {
                        //------------------------------BUMP MAPING---------------------------------------//
                        float uNormalise = u;
                        float vNormalise = v;
                        V3 dMdu, dMdv;
                        float dhdu, dhdv;
                        Partial_derivative(u, v, out dMdu, out dMdv);
                        materiel.GetTextureBump().Bump(u, v, out dhdu, out dhdv);
                        V3 normal = getNormal(u, v) + materiel.GetForceBumping() * (dhdu * (getNormal(u, v) ^ dMdv) + dhdv * (getNormal(u, v) ^ dMdu));

                        //V3 normal = getNormal(u,v);
                        //------------------------------TEXTURE---------------------------------------//
                        Couleur couleur = new Couleur();
                        if (materiel.GetTexture() != null)
                            couleur = materiel.GetTexture().LireCouleur(uNormalise, vNormalise);
                        else
                            couleur = materiel.GetCouleur();

                        //------------------------------LUMIERE---------------------------------------//
                        couleur = scene.CouleurEclairee(pixel_position, normal, couleur, materiel);
                        //System.Console.WriteLine(couleur.R+" "+couleur.V+" "+couleur.B);
                        //------------------------------DESSIN FINAL---------------------------------------//
                        BitmapEcran.DrawPixel((int)pixel_position.x, (int)pixel_position.z, couleur);
                    }
                }
            }
        }**/

        public V3 getPixel3DPosition(float u, float v)
        {
            return A + u * (B) + v * (C);
        }
        public void Partial_derivative(float u, float v, out V3 dMdu, out V3 dMdv)
        {
            float dxdu, dydu, dzdu, dxdv, dydv, dzdv;
            dxdu = (float)(B.x);
            dydu = (float)(B.y);
            dzdu = (float)(B.z);
            dMdu = new V3(dxdu, dydu, dzdu);
            dMdu.Normalize();

            dxdv = (float)(C.x);
            dydv = (float)(C.y);
            dzdv = (float)(C.z);
            dMdv = new V3(dxdv, dydv, dzdv);
            dMdv.Normalize();
        }

        public V3 getNormal()
        {
            V3 normal = new V3((B-A) ^ (C-A));
            normal.Normalize();
            return normal;
        }

        override public float gettIntersect(V3 PosCamera, V3 DirRayon)
        {
            float t = ((A - PosCamera) * getNormal() ) / (DirRayon * getNormal());

            V3 I = PosCamera + t * DirRayon;
            float alpha = ((I - A) * (B - A)) / ((B - A) * (B - A));
            float beta = ((I - A) * (C - A)) / ((C - A) * (C - A));

            if ((0 <= alpha && alpha <= 1) && (0 <= beta && beta <= 1))
            {
                return t;
            }
            else return -1;
        }

        override public Couleur getCouleurRaycast(V3 PointIntersection, Scene scene)
        {
            float u = ((PointIntersection - A) * (B - A)) / ((B - A) * (B - A));
            float v = ((PointIntersection - A) * (C - A)) / ((C - A) * (C - A));


            //V3 pixel_position = getPixel3DPosition(u, v);
            V3 pixel_position = PointIntersection;


                //------------------------------BUMP MAPING---------------------------------------//
            float uNormalise = u;
            float vNormalise = v;
            Partial_derivative(u, v, out V3 dMdu, out V3 dMdv);
            materiel.GetTextureBump().Bump(u, v, out float dhdu, out float dhdv);
            V3 normal = getNormal() + materiel.GetForceBumping() * (dhdu * (getNormal() ^ dMdv) + dhdv * (getNormal() ^ dMdu));

            //V3 normal = getNormal(u,v);
            //------------------------------TEXTURE---------------------------------------//
            Couleur couleur = new Couleur();
            if (materiel.GetTexture() != null)
                couleur = materiel.GetTexture().LireCouleur(uNormalise, vNormalise);
            else
                couleur = materiel.GetCouleur();

            //------------------------------LUMIERE---------------------------------------//
            couleur = scene.CouleurEclairee(pixel_position, normal, couleur, materiel, this);
            //System.Console.WriteLine(couleur.R+" "+couleur.V+" "+couleur.B);
            //------------------------------DESSIN FINAL---------------------------------------//
            return couleur;
            
        }
    }
}


