using System;

namespace Projet_IMA
{
    public class LumierePoint : Lumiere
    {
        public new Couleur Couleur;
        public V3 Position;

        public LumierePoint(Couleur pCouleurLumiereDirectionnelle, V3 pPositionLumiereDirectionnelle)
        {
            Couleur = pCouleurLumiereDirectionnelle;
            Position = pPositionLumiereDirectionnelle;
        }

        public override Couleur getCouleur()
        {
            return Couleur;
        }

        public override V3 getDirection(V3 PositionObjet)
        {
            V3 DirectionLumiere = Position - PositionObjet;
            DirectionLumiere.Normalize();
            return DirectionLumiere;

        }
    }
}