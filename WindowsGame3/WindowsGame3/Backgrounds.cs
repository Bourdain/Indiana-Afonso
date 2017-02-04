using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IndianaJones
{
    class Backgrounds
    {
        public Texture2D fundos;
        public Vector2 posicao;


        public Backgrounds(Texture2D textura, Vector2 posicao)
        {
            this.fundos = textura;
            this.posicao = posicao;
        }
    }
}
