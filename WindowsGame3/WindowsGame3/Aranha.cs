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
    class Aranha
    {
        public Texture2D Textura;
        public Vector2 posicao;
        public Vector2 pos_dest;
        public int velocidade = 5;
        public BoundingBox bbox;

        public Aranha(Texture2D textura, Vector2 posicao)
        {
            this.Textura = textura;
            this.posicao = posicao;
            //this.pos_dest = posicao;
        }

        public void UpdateBoundingBox()
            {
            int i = 10;
            bbox.Min = new Vector3(posicao.X+i, posicao.Y+i, 0);
            bbox.Max = new Vector3(posicao.X + Textura.Width-i, posicao.Y + Textura.Height-i, 0);
            }

        public void UpdatePosicaoDestino(Vector2 dest)
        {
            this.pos_dest = dest;
        }
        
    }
}
