using System;
namespace Projet_IMA
{
    public class Materiel
    {
        private Texture texture;
        private Texture textureBump;
        private float forceSpeculaire;
        private float forceBumping;
        private Couleur couleur;

        public Materiel(Texture ptexture, Texture ptextureBump, float pforceSpeculaire, float pforceBumping)
        {
            texture = ptexture;
            textureBump = ptextureBump;
            forceSpeculaire = pforceSpeculaire;
            forceBumping = pforceBumping;
        }
        public Materiel(Couleur pcouleur, Texture ptextureBump, float pforceSpeculaire, float pforceBumping)
        {
            couleur = pcouleur;
            textureBump = ptextureBump;
            forceSpeculaire = pforceSpeculaire;
            forceBumping = pforceBumping;
        }

        public Texture GetTexture()
        {
            return texture;
        }
        public Texture GetTextureBump()
        {
            return textureBump;
        }
        public float GetForceSpeculaire()
        {
            return forceSpeculaire;
        }
        public float GetForceBumping()
        {
            return forceBumping;
        }
        public Couleur GetCouleur()
        {
            return couleur;
        }

    }
}
