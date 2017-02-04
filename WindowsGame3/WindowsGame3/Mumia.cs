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
    
    class Mumia
    {
        public Texture2D Textura;
        public Vector2 posicao;
        public bool direccao; //False = eskerda ; true = direita
        
        #region Animacao
        public float timer = 0f;
        public float intervalo = 1000f / 20f;

        public int frameCount_Correr = 10;
        public int frameActual = 0;
        #endregion

        public int percurso; 
        public int percurso_max=50;
        public int percurso_min = 0;

        public int velocidade = 3;
        public BoundingBox bbox;

        public Rectangle rect;

        public Mumia(Texture2D textura, Vector2 posicao, bool direccao)
        {
            this.Textura = textura;
            this.posicao = posicao;
            this.direccao = direccao;
            this.percurso = 15;
            rect.Width = 48;
            rect.Height = 48;
        }

        public void UpdateBoundingBox()
        {
            bbox.Min = new Vector3(posicao.X, posicao.Y, 0);
            bbox.Max = new Vector3(posicao.X + rect.Width, posicao.Y + rect.Height, 0);
        }
    }
}
