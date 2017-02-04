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
    class Nivel3
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

        public Nivel3(ContentManager Content)
        {
            #region "BACKGROUND MUSIC"
            background_music = Content.Load<Song>("Musicas\\lvl3");
            #endregion
            #region "PLATAFORMAS"
            plataformas[0] = new Plataforma(Content.Load<Texture2D>("Plataformas\\Barra Hor"), new Vector2(0, 400));
            plataformas[1] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockA6"), new Vector2(400, 500), true, 50, 50, true, 2);
            plataformas[2] = new Plataforma(Content.Load<Texture2D>("Plataformas\\Barra Hor"), new Vector2(-200, 300));
            plataformas[3] = new Plataforma(Content.Load<Texture2D>("Plataformas\\Barra Hor"), new Vector2(200, 300));
            plataformas[4] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockA5"), new Vector2(150, 280),true,20,10);
            plataformas[5] = new Plataforma(Content.Load<Texture2D>("Plataformas\\Barra Hor"), new Vector2(0,100));
            plataformas[6] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockA3"), new Vector2(650, 200),true,30,30,true,2);
            plataformas[7] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockA3"), new Vector2(500,120));
            plataformas[8] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(715, 105));
            plataformas[49] = new Plataforma(Content.Load<Texture2D>("Plataformas\\plat_invisivel"), new Vector2(0, 599));
            #endregion
            #region "POSIÇÃO INICIAL DO JOGADOR"
            start = new Vector2(15, 600);
            #endregion
            #region "FUNDOS"
            fundos[0] = new Backgrounds(Content.Load<Texture2D>("Backgrounds\\Niveis_Back\\Layer0_2"), new Vector2(0, 0));
            fundos[1] = new Backgrounds(Content.Load<Texture2D>("Backgrounds\\Niveis_Back\\Layer1_2"), new Vector2(0, 50));
            #endregion
            #region "MUMIAS"
            
            #endregion
            #region "ARMADILHAS"

            #endregion
            #region "ARANHAS"
            aranha[0] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(0, 0));
            aranha[1] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(500, 0));
            #endregion
            #region "SAIDA"

            saida = new Saida(Content.Load<Texture2D>("Saida\\Saida"), new Vector2(700, 58));

            #endregion
            #region "TIMER"
            timer = 40;
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
