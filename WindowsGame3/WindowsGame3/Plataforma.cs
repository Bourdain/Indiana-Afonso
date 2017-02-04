using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IndianaJones
{
    class Plataforma
    {
        public Texture2D textura_plataforma;
        public BoundingBox bb_plataforma_total;
        public BoundingBox bb_plataforma_topo;
        public BoundingBox bb_plataforma_esq;
        public BoundingBox bb_plataforma_dir;
        public Vector2 posicao;

        public bool isMovable=false;
        public bool vertical=false;
        public bool direccao; //False = eskerda ; true = direita
        public int percurso=0;
        public int percurso_max = 50;
        public int percurso_min = 0;

        public int velocidade = 3;


        #region "CONSTRUTORES
        public Plataforma(Texture2D textura_plataforma, Vector2 posicao)
        {
            this.textura_plataforma = textura_plataforma;
            setPosicao(posicao);
        }

        public Plataforma(Texture2D textura_plataforma, Vector2 posicao,bool mexer)
        {
            this.textura_plataforma = textura_plataforma;
            this.isMovable = mexer;
            this.percurso = 0;
            setPosicao(posicao);
        }

        public Plataforma(Texture2D textura_plataforma, Vector2 posicao, bool mexer,bool vertical,int velocidade)
        {
            this.textura_plataforma = textura_plataforma;
            this.isMovable = mexer;
            this.percurso = 0;
            setPosicao(posicao);
            this.velocidade = velocidade;
            this.vertical = vertical;
        }

        public Plataforma(Texture2D textura_plataforma, Vector2 posicao, bool mexer, int percurso_max, int percurso)
        {
            this.textura_plataforma = textura_plataforma;
            this.isMovable = mexer;
            this.percurso_max = percurso_max;
            this.percurso = percurso;
            setPosicao(posicao);
            
        }

        public Plataforma(Texture2D textura_plataforma, Vector2 posicao, bool mexer, int percurso_max, int percurso, bool vertical, int velocidade)
        {
            this.textura_plataforma = textura_plataforma;
            this.isMovable = mexer;
            this.percurso_max = percurso_max;
            this.percurso = percurso;
            this.velocidade = velocidade;
            setPosicao(posicao);
            this.vertical = vertical;
        }
        #endregion
        public void setPosicao(Vector2 position)
        {
            posicao = position;
            bb_plataforma_total.Min = new Vector3(posicao, 0.0f);
            bb_plataforma_total.Max = new Vector3(posicao.X + textura_plataforma.Width, posicao.Y + textura_plataforma.Height, 0.0f);

            bb_plataforma_topo.Min.X = bb_plataforma_total.Min.X;
            bb_plataforma_topo.Min.Y = bb_plataforma_total.Min.Y - 3;

            bb_plataforma_topo.Max = new Vector3(posicao.X + textura_plataforma.Width, posicao.Y + 6, 0.0f);

            bb_plataforma_dir.Min = new Vector3(posicao.X + textura_plataforma.Width - 2, posicao.Y + 4, 0);
            bb_plataforma_dir.Max = new Vector3(posicao.X + textura_plataforma.Width, posicao.Y + textura_plataforma.Height-3, 0);

            bb_plataforma_esq.Min = new Vector3(posicao.X, posicao.Y + 4, 0);
            bb_plataforma_esq.Max = new Vector3(posicao.X + 2, posicao.Y + textura_plataforma.Height - 3, 0);
            UpdateBoundingBox();

        }

        public void UpdateBoundingBox()
        {
            bb_plataforma_total.Min = new Vector3(posicao.X, posicao.Y, 0);
            bb_plataforma_total.Max = new Vector3(posicao.X + textura_plataforma.Width, posicao.Y + textura_plataforma.Height, 0);
            bb_plataforma_topo.Min = bb_plataforma_total.Min;
            bb_plataforma_topo.Max = new Vector3(posicao.X + textura_plataforma.Width, posicao.Y + 6, 0.0f);
            bb_plataforma_dir.Min = new Vector3(posicao.X + textura_plataforma.Width - 2, posicao.Y + 4, 0);
            bb_plataforma_dir.Max = new Vector3(posicao.X + textura_plataforma.Width, posicao.Y + textura_plataforma.Height - 3, 0);
            bb_plataforma_esq.Min = new Vector3(posicao.X, posicao.Y + 4, 0);
            bb_plataforma_esq.Max = new Vector3(posicao.X + 2, posicao.Y + textura_plataforma.Height - 3, 0);
        }
    }
}
