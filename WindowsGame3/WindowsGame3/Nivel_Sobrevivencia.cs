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
    class Nivel_Sobrevivencia
    {
        public Plataforma[] plataformas = new Plataforma[50];
        public Backgrounds[] fundos = new Backgrounds[5];
        public Mumia[] mumia = new Mumia[10];
        public Diamante[] diamante = new Diamante[10];
        public Aranha[] aranha = new Aranha[10];
        //public Saida saida;
        public Vector2 start;
        public static Song background_music;
        public float timer;
        public Armadilhas[] armadilhas = new Armadilhas[150];

        public Nivel_Sobrevivencia(ContentManager Content)
        {
            #region "FUNDOS"
                        fundos[0] = new Backgrounds(Content.Load<Texture2D>("Backgrounds\\Hell"),new Vector2(0,0));
            #endregion
            #region "PLATAFORMAS"
            plataformas[0] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(50, 500));  //
            plataformas[1] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(80, 500));  //Canto Inferior de baixo Esquerdo
            plataformas[2] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(110, 500)); //

            plataformas[3] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(200, 400)); //
            plataformas[4] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(230, 400)); //Canto Inferior de cima Esquerdo
            plataformas[5] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(260, 400)); //

            plataformas[6] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(350+15, 300));//
            plataformas[7] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(380+15, 300));// Meio
            plataformas[8] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(410+15, 300));//

            plataformas[9] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-200-15, 400)); //
            plataformas[10] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-230-15, 400));//Canto Inferior de cima Direito
            plataformas[11] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-260-15, 400));//

            plataformas[12] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-50-15, 500)); //
            plataformas[13] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-80-15, 500)); //Canto Inferior de baixo Direiro
            plataformas[14] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-110-15, 500));//

            plataformas[15] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(200-50, 400-150));
            plataformas[16] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(230-50, 400-150));
            plataformas[17] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(260-50, 400-150));

            plataformas[18] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-200+25, 400 - 150));
            plataformas[19] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-230+25, 400 - 150));
            plataformas[20] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(800-260+25, 400 - 150));

            #endregion
            #region "PICOS"
            for(int i=0; i<20;i++)
            {
                armadilhas[i] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Esquerda"), new Vector2(0, 32 * i));
            }
            for (int i = 1; i < 25; i++)
            {
                armadilhas[i+20] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(32*i, 600-32));
            }

            for (int i = 1; i < 25; i++)
            {
                armadilhas[i + 45] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Direita"), new Vector2(800-32, 32*i));
            }

            for (int i = 1; i < 25; i++) 
            {
                armadilhas[i + 70] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Cima"), new Vector2(i*32, 0));
            }

            #endregion
            background_music = Content.Load<Song>("Musicas\\MGS");
            timer = 0;
            start = new Vector2(50, 430);

            }
    }
}