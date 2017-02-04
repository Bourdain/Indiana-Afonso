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
    class Nivel_2
    {
        public static int nivel_dif = 0;
        public Plataforma[] plataformas = new Plataforma[50];
        public Backgrounds[] fundos = new Backgrounds[5];
        public Mumia[] mumia = new Mumia[10];
        public Diamante[] diamante = new Diamante[10];
        public Aranha[] aranha = new Aranha[5];
        public Saida saida;
        public Vector2 start;
        public static Song background_music;
        public float timer;
        public Armadilhas[] armadilhas = new Armadilhas[80];

         public Nivel_2(ContentManager Content)
        {
            #region "BACKGROUND MUSIC"
            background_music = Content.Load<Song>("Musicas\\lvl2");
            #endregion
            #region "PLATAFORMAS"
          plataformas[0] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(0, 505),true);
          plataformas[1] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(150, 505), true,50,50);
          plataformas[2] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(300, 505), true);
          plataformas[3] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(450, 505), true, 50, 50);
          plataformas[4] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600, 505), true);
          plataformas[5] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(750, 505), true, 50, 50);
          plataformas[6] = new Plataforma(Content.Load<Texture2D>("Plataformas\\Barra Hor"), new Vector2(500, 405));
          plataformas[7] = new Plataforma(Content.Load<Texture2D>("Plataformas\\Barra Hor"), new Vector2(500-600, 405));
          plataformas[8] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(150, 305), true);
          plataformas[9] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(300, 305), true, 50, 50);
          plataformas[10] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(450, 305), true);
          plataformas[11] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600, 305), true, 50, 50);
          plataformas[12] = new Plataforma(Content.Load<Texture2D>("Plataformas\\Barra Hor"), new Vector2(225, 205));
          plataformas[13] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(225, 105));
          plataformas[14] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(550, 105));
          plataformas[15] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockA4"), new Vector2(715, 105));
          plataformas[49] = new Plataforma(Content.Load<Texture2D>("Plataformas\\plat_invisivel"), new Vector2(0, 599));

            #endregion
            #region "POSIÇÃO INICIAL DO JOGADOR"
            start = new Vector2(15, 600);
            #endregion
            #region "FUNDOS"
            fundos[0] = new Backgrounds(Content.Load<Texture2D>("Backgrounds\\Niveis_Back\\Layer0_0"), new Vector2(0, 0));
            fundos[1] = new Backgrounds(Content.Load<Texture2D>("Backgrounds\\Niveis_Back\\Layer1_1"), new Vector2(0, 50));
            #endregion
            #region "MUMIAS"
            mumia[0] = new Mumia(Content.Load<Texture2D>("Monstros\\MCRun"), new Vector2(380, 160), true);
            #endregion            
            #region "ARMADILHAS"

            #endregion
            #region "ARANHAS"

            #endregion
            #region "SAIDA"

            saida = new Saida(Content.Load<Texture2D>("Saida\\Saida"), new Vector2(700, 58));

            #endregion
            #region "TIMER"
            timer = 120;
            #endregion
            #region "DIAMANTES"
            for (int i = 0; i < diamante.Length; i++)
            {

                int especial = 0;
                especial = Random_Number.Numero.Next(1, 100);

                if (especial == 1)
                    diamante[i] = new Diamante(Content.Load<Texture2D>("Diamantes\\diamante_vida"), new Vector2(0, 0), 2);

                else if (especial >= 2 && especial <= 10)
                    diamante[i] = new Diamante(Content.Load<Texture2D>("Diamantes\\diamante_bonus"), new Vector2(0, 0), 1);

                else
                    diamante[i] = new Diamante(Content.Load<Texture2D>("Diamantes\\diamante"), new Vector2(0, 0), 0);



                int X = Random_Number.Numero.Next(0, 760);
                int Y = Random_Number.Numero.Next(0, 580);

                diamante[i].posicao.X = (float)X;

                diamante[i].posicao.Y = (float)Y;
                diamante[i].Update_Bounding_Box();
                for (int j = 0; j < plataformas.Length; j++)
                {
                    if (plataformas[j] != null)
                    {
                        if (diamante[i].bbox.Intersects(plataformas[j].bb_plataforma_total) == true || diamante[i].bbox.Intersects(saida.bbox) == true || diamante[i].bbox.Intersects(Player.bounding_box_player) == true)
                        {
                            diamante[i].posicao.X = Random_Number.Numero.Next(0, 760);
                            diamante[i].posicao.Y = Random_Number.Numero.Next(0, 580);
                            diamante[i].Update_Bounding_Box();
                            j = 0;
                        }
                    }
                }
            }
            #endregion
         }
    }
}
