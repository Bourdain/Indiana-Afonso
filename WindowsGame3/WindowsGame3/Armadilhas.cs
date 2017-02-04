using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace IndianaJones
{
    class Armadilhas
    {
        public Texture2D Textura;
        public Vector2 posicao;
        public BoundingBox bbox;

        public bool isMovable = false;
        public bool vertical = false;
        public bool direccao; //False = eskerda ; true = direita
        public int percurso = 0;
        public int percurso_max = 50;
        public int percurso_min = 0;

        public int velocidade = 3;

        public Armadilhas(Texture2D textura, Vector2 posicao)
        {
            this.Textura = textura;
            this.posicao = posicao;
        }

        public Armadilhas(Texture2D textura, Vector2 posicao, bool mexer)
        {
            Textura = textura;
            this.posicao = posicao;
            this.isMovable = mexer;
            this.percurso = 0;
        }

        public Armadilhas(Texture2D textura, Vector2 posicao, bool mexer, int percurso_max, int percurso)
        {
            Textura = textura;
            this.posicao = posicao;
            this.isMovable = mexer;
            this.percurso = percurso;
            this.percurso_max = percurso_max;
        }

        public Armadilhas(Texture2D textura, Vector2 posicao, bool mexer, int percurso_max, int percurso, bool vertical, int velocidade)
        {
            Textura = textura;
            this.posicao = posicao;
            this.isMovable = mexer;
            this.percurso = percurso;
            this.percurso_max = percurso_max;
            this.vertical = vertical;
            this.velocidade = velocidade;
        }

        public void UpdateBoundingBox()
        {
            bbox.Min = new Vector3(posicao.X, posicao.Y, 0);
            bbox.Max = new Vector3(posicao.X + Textura.Width, posicao.Y + Textura.Height, 0);
        }
    }
}
