using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;



namespace IndianaJones
{
    [Serializable()]
   static class Score
    {
       static public int Highscore = 0;
       static public int Hightime = 0;
       static public DateTime Data_do_highscore;
       static public DateTime Data_do_hightime;
       static public int score_jogo;
       #region FUNÇOES

        static public void Morte_Monstro()
        {
            score_jogo -= 50;
            if (score_jogo < 0)
            {
                score_jogo = 0;
            }
        }

        static public void Morte_Relógio()
        {
            score_jogo -= 100;
            if (score_jogo < 0)
            {
                score_jogo = 0;
            }
        }

        static public void Nivel_Completo(float dificuldade)
        {
            score_jogo += 100 + (int)(100 * dificuldade);
            
            
        }

        static public void Moeda()
        {
            score_jogo += 20;
        }

        static public void Moeda_especial()
        {
            score_jogo += 300;
        }
        #endregion
    }
}
