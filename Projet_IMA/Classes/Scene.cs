using System;
using System.Collections.Generic;

namespace Projet_IMA
{ 
    public class Scene
    {
        private List<Objet3D> Objets;
        private LumiereAmbiante LumAmb;
        private List<LumiereDirectionnelle> LumieresDirectionnelles;
        private List<LumierePoint> LumieresPoints;
        private List<Lumiere> Lumieres ;
        private V3 PosCamera = new V3(BitmapEcran.GetWidth() / 2, -2000, BitmapEcran.GetHeight() / 2);

        public Scene()
	    {
            LumieresDirectionnelles = new List<LumiereDirectionnelle>();
            LumieresPoints = new List<LumierePoint>();
            Lumieres = new List<Lumiere>();
            Objets = new List<Objet3D>();
        }

        //===== Objets =====
        public void AddObjet3D(Objet3D o) { Objets.Add(o); }

        //dessine tous les objets de la scène
        public void DrawScene()
        {
            for (int x_ecran = 0; x_ecran <= BitmapEcran.GetWidth(); x_ecran++) {
                for (int y_ecran = 0; y_ecran <= BitmapEcran.GetHeight(); y_ecran++)
                {
                    V3 PosPixScene = new V3(x_ecran, 0, y_ecran);
                    V3 DirRayon = PosPixScene - PosCamera;
                    DirRayon.Normalize();
                    Couleur C = RayCast(PosCamera, DirRayon, Objets);
                    BitmapEcran.DrawPixel(x_ecran, y_ecran, C);
                }
            }
        }

        public Couleur RayCast(V3 PosCamera, V3 DirRayon, List<Objet3D> Objets)
        {
            Couleur c;
            float t;
            float tmin = float.MaxValue;
            Objet3D ObjectAAfficher = null;

            foreach (Objet3D Objet in Objets)
            {
                //Console.WriteLine("OBJET: " + Objet.GetType().ToString());
                t = Objet.gettIntersect(PosCamera, DirRayon);
                //Console.WriteLine("   Distance " + t);
                if (t > 0 && t < tmin)
                {
                    ObjectAAfficher = Objet;
                    tmin = t;
                }
            }
            //Console.WriteLine("");
            //Console.WriteLine("Distance min: " + tmin );

            if (ObjectAAfficher != null)
            {
                V3 PointIntersection = new V3(PosCamera + tmin * DirRayon);
                c = ObjectAAfficher.getCouleurRaycast(PointIntersection, this);
                return c;
            }
            else
            {
                c = new Couleur(0.3f, 0.3f, 0.3f);
                return c;
            }
        }

        //===== Lumières =====
        public void SetLumAmb(LumiereAmbiante l) { LumAmb = l; }
        public void AddLum(Lumiere l) { Lumieres.Add(l); }
        public void RemoveLum(Lumiere l) { Lumieres.Remove(l); }

        //récupère la couleur du pixel en prenant compte des lumières de la scène
        public Couleur CouleurEclairee(V3 position, V3 normal, Couleur couleurDeBase, Materiel mat, Objet3D ObjetToDraw)
        {
            //initialisation de la couleur
            Couleur couleurFinale = new Couleur(0, 0, 0);


            //---------------------------Lumiere AMBIANTE----------------------------------//
            if (LumAmb != null)
                couleurFinale += new Couleur(couleurDeBase * LumAmb.Couleur);

            if (Lumieres.Count > 0)
            {
                foreach (Lumiere l in Lumieres)
                {

                    V3 DirectionLumiere = l.getDirection(position);
                    DirectionLumiere.Normalize();
                    


                    if (!CheckIfObjectBetweenLightSource(DirectionLumiere, position, ObjetToDraw))
                    {
                        //Console.WriteLine("Je dessine la lumière");
                        //-------------------------------Lumiere DIFFUSE------------------------------------//
                        float diff = Math.Max((DirectionLumiere * normal), 0);
                        couleurFinale += new Couleur((couleurDeBase * l.getCouleur()) * diff);

                        //--------------------------------Reflet SPECULAIRE--------------------------------//
                        float forceSeculaire = mat.GetForceSpeculaire();
                        V3 positionOeil = new V3(BitmapEcran.GetHeight() / 2, -3000, BitmapEcran.GetWidth() / 2);
                        V3 directionDuRayonReflechis = new V3(2 * normal - DirectionLumiere);
                        directionDuRayonReflechis.Normalize();
                        V3 directionObservateur = new V3(positionOeil - position);
                        directionObservateur.Normalize();

                        couleurFinale += new Couleur(l.getCouleur() * (float)Math.Pow((directionDuRayonReflechis * directionObservateur), forceSeculaire));

                    }
                }
            }
            return couleurFinale;
        }

        public Boolean CheckIfObjectBetweenLightSource(V3 DirRayon, V3 OrigineRayon, Objet3D ObjectToDraw)
        {

            float t=-1;
            //Console.WriteLine(" Origine PT: "+ OrigineRayon.x + " " + OrigineRayon.y + " " + OrigineRayon.z+ " Direction Rayon: " + DirRayon.x + " " + DirRayon.y + " " + DirRayon.z);

            foreach (Objet3D Objet in Objets)
            {
                if (Objet != ObjectToDraw)
                {
                    t = Objet.gettIntersect(OrigineRayon, DirRayon);
                    //Console.WriteLine(t);
                    if (t > 0.5f)
                    {
                        //Console.WriteLine("Return TRUE");
                        return true;
                    }
                }

            }
            //Console.WriteLine("Return FALSE");
            return false;
        }
    }
}