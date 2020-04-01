using System;

namespace Projet_IMA
{
    public class LumiereDirectionnelle : Lumiere
    {
        public Couleur Couleur;
        public V3 Direction;

        public LumiereDirectionnelle(Couleur pCouleurLumiereDirectionnelle, V3 pDirectionLumiereDirectionnelle)
        {
            Couleur = pCouleurLumiereDirectionnelle;
            Direction = pDirectionLumiereDirectionnelle;
        }

        public override Couleur getCouleur()
        {
            return Couleur;
        }

        public override V3 getDirection(V3 PositionObjet)
        {
            return Direction;
        }
    }
}