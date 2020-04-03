using System;

namespace Projet_IMA
{ 
    public abstract class Objet3D
    {
        private V3 position;
        
        public abstract float gettIntersect(V3 PosCamera, V3 DirRayon);
        public abstract Couleur getCouleurRaycast(V3 PointIntersection, Scene scene);
    }
}