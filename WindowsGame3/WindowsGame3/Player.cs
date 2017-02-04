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
    
    class Player
    {
    #region Texturas
        public static Texture2D Textura;
        public static Texture2D Textura_saltar;
        public static Texture2D Textura_run;
        #endregion
        #region Animacao
        public static float timer = 0f;
        public static float intervalo = 1000f / 15f;

        public static int frameCount_Saltar = 10;
        public static int frameCount_Correr = 10;
        public static int frameActual = 0;
        #endregion
        public static Vector2 posicao;
    #region Movimento
    public static float velocidade_Horizontal = 5f;
    public static int velocidade_Vertical= 7;
    public static int tamanho_salto = 18;
    public static bool isJumping;
    #endregion
    #region BBox
    public static BoundingBox bounding_box_player;
    public static BoundingBox bounding_box_player_feet;
    #endregion
    public static SoundEffect Jump;
    public static SoundEffect Morrer_Monstro;
    public static SoundEffect Morrer_Relogio;
    public static int Vidas=3;

   public static Rectangle rect;
   
    public enum Estado
    {
        Normal,
        CorrerEsq,
        CorrerDir,
        Saltar
    };
    public static Estado estado = Estado.Normal;

    public static void SetNormalRect()
    {
        rect.Width = Textura.Width;
        rect.Height = Textura.Height;
    }

    public static void UpdateBoundingBox()
    {
        int offset = 15;
        bounding_box_player.Min = new Vector3(posicao.X+offset, posicao.Y, 0);
        bounding_box_player.Max = new Vector3(posicao.X + rect.Width - offset, posicao.Y + rect.Height, 0);

        bounding_box_player_feet.Min = new Vector3(posicao.X + offset, posicao.Y + rect.Height - 1, 0);
        bounding_box_player_feet.Max = new Vector3(posicao.X + rect.Width - offset, posicao.Y + rect.Height + 5, 0);
    }

    
    }
}
