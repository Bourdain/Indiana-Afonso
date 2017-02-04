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
    class Saida
    {
        public Texture2D Textura;
        public Vector2 posicao;
        public BoundingBox bbox;

        public Saida(Texture2D textura, Vector2 posicao)
        {
            this.Textura = textura;
            this.posicao = posicao;
        }

        public void UpdateBoundingBox()
        {
            bbox.Min = new Vector3(posicao.X, posicao.Y, 0);
            bbox.Max = new Vector3(posicao.X + Textura.Width, posicao.Y + Textura.Height, 0);
        }
    }
}
