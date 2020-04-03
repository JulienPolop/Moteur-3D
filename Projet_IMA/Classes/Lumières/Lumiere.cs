using System;

namespace Projet_IMA
{

    public abstract class Lumiere
    {
        public abstract V3 getDirection(V3 PositionObjet);
        public abstract Couleur getCouleur(); 
    }
}