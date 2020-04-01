using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_IMA
{
    static class ProjetEleve
    {
        public static void Start()
        {
            //création de la scène qui va contenir les objets
            Scene mainScene = new Scene();

            //couleurs
            Couleur blanc = new Couleur(1, 1, 1);
            Couleur rouge = new Couleur(0.9f, 0, 0);
            Couleur jaune = new Couleur(1, 1, 0);
            Couleur vert = new Couleur(0, 1, 0);
            Couleur cyan = new Couleur(0, 1, 1);
            Couleur bleu = new Couleur(0, 0, 1);
            Couleur violet = new Couleur(1, 0, 1);
            Couleur gris = new Couleur(0.5f, 0.5f, 0.5f);
            Couleur noir = new Couleur(0, 0, 0);

            //========== Lumières ==========
            //lumière ambiante


            //lumières directionnelles
            V3 directionLumièreDirectionnelle;
            V3 positionLumièrePoint;
            LumiereDirectionnelle lumiereDirerectionnelle;
            LumierePoint lumierePoint;

            LumiereAmbiante LumAmb = new LumiereAmbiante(new Couleur(0.3f, 0.3f, 0.3f));
            mainScene.SetLumAmb(LumAmb);

            directionLumièreDirectionnelle = new V3(0, -1, 0);
            lumiereDirerectionnelle = new LumiereDirectionnelle(new Couleur(0.5f,0.5f,0.5f), directionLumièreDirectionnelle);
            //mainScene.AddLumDir(lumiereDirerectionnelle);

            positionLumièrePoint = new V3(400, 1, 400);
            lumierePoint = new LumierePoint(new Couleur(0.5f, 0.5f, 0.5f), positionLumièrePoint);
            mainScene.AddLum(lumierePoint);

            positionLumièrePoint = new V3(BitmapEcran.GetWidth() / 2, 0, BitmapEcran.GetHeight() / 2);
            lumierePoint = new LumierePoint(new Couleur(0.5f, 0.5f, 0.5f), positionLumièrePoint);
            mainScene.AddLum(lumierePoint);




            //directionLumièreDirectionnelle = new V3(-1, -1, -1);
            //lumiereDirerectionnelle = new LumiereDirectionnelle(new Couleur(0,0, 0.7f), directionLumièreDirectionnelle);
            //mainScene.AddLumDir(lumiereDirerectionnelle);

            //========== Matériaux ==========
            Materiel Blanc = new Materiel(blanc / 1.2f, new Texture("bump38.jpg"), 50, 1);
            Materiel test = new Materiel(new Texture("test.jpg"), new Texture("test.jpg"), 200, 3);
            Materiel or = new Materiel(new Texture("gold.jpg"), new Texture("gold_Bump.jpg"), 200, 0.7f);
            Materiel plomb = new Materiel(new Texture("lead.jpg"), new Texture("lead_bump.jpg"), 50, 1);
            Materiel brique = new Materiel(new Texture("brickwork-texture.jpg"), new Texture("brickwork-bump-map.jpg"), 500, 1);
            Materiel rock = new Materiel(new Texture("rock.jpg"), new Texture("rock.jpg"), 500, 1);
            Materiel stone = new Materiel(new Texture("rock.jpg"), new Texture("rock.jpg"), 500, 1);
            Materiel fibre = new Materiel(new Texture("rock.jpg"), new Texture("rock.jpg"), 500, 1);
            Materiel Blob = new Materiel(blanc / 1.2f, new Texture("bump.jpg"), 50, 1);


            //========== Objets ==========
            V3 center;
            Sphere s;
            Rectangle r;

            //Boule Or
            center = new V3(600, 300f, 200);
            s = new Sphere(center, 50, or);
            mainScene.AddObjet3D(s);

            //Boule Or
            center = new V3(200, 200, 100);
            s = new Sphere(center, 150, Blob);
            mainScene.AddObjet3D(s);

            //Boule Plomb
            center = new V3(800, 1000, 400);
            s = new Sphere(center, 100, plomb);
            mainScene.AddObjet3D(s);
            
            //Boule Brique
            center = new V3(300, 1000, 400);
            s = new Sphere(center, 150, Blanc);
            mainScene.AddObjet3D(s);

            //Rectangle brique Gauche
            V3 A = new V3(0, 0, 0);
            V3 B = new V3(0, 2000, 0);
            V3 C = new V3(0, 0, 800);
            r = new Rectangle(A,B,C, brique);
            mainScene.AddObjet3D(r);

            //Rectangle brique Droit
            A = new V3(1000, 2000, 0);
            B = new V3(1000, 0, 0);
            C = new V3(1000, 2000, 800);
            r = new Rectangle(A, B, C, brique);
            mainScene.AddObjet3D(r);
            
            //Rectangle brique Fond
            A = new V3(0, 2000, 0);
            B = new V3(1000, 2000, 0);
            C = new V3(0, 2000, 800);
            r = new Rectangle(A, B, C, brique);
            mainScene.AddObjet3D(r);

            //Rectangle brique Bas
            A = new V3(0, 0, 0);
            B = new V3(1000, 0, 0);
            C = new V3(0, 2000, 0);
            r = new Rectangle(A,B,C, brique);
            mainScene.AddObjet3D(r);

            A = new V3(400, 0, 0);
            B = new V3(190, -50, 0);
            C = new V3(0, 0, 190);
            r = new Rectangle(A, B, C, brique);
            //mainScene.AddObjet3D(r);


            //Affichage de la scène
            mainScene.DrawScene();
        }
    }
}
