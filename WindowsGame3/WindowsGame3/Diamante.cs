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
    public class Diamante
    {
        public Texture2D Textura;
        public Vector2 posicao;
        public int especial = 0; //0 normal, 1 bonus, 2 vida

        public static SoundEffect efeito_sonoro;
        public static SoundEffect efeito_sonoro_bonus;
        public static SoundEffect efeito_sonoro_vidas;


        public BoundingBox bbox;

        public Diamante(Texture2D textura, Vector2 posicao, int especial)
        {
            this.Textura = textura;
            this.posicao = posicao;
            this.especial = especial;
        }

        public void Update_Bounding_Box()
        {
            bbox.Min = new Vector3(posicao.X, posicao.Y, 0);
            bbox.Max = new Vector3(posicao.X + Textura.Width, posicao.Y + Textura.Height, 0);
        }
    }
}
