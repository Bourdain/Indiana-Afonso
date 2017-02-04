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
    public class Main_Menu
    {
        public Texture2D background;
        public Texture2D novo_Jogo;
        public Texture2D ler_Jogo;
        public Texture2D sair;
        public Texture2D Indiana;
        public Texture2D Afonso;

        public Vector2 pos_Novo_Jogo;
        public Vector2 pos_Ler_Jogo;
        public Vector2 pos_Sair;

        public Song background_music;

        bool isMusicPlaying = false;
        public bool isWhipPlaying = false;
        //public bool hasMenuMoved = false;

        public Main_Menu(Texture2D background, Song Background_music)
        {
            this.background = background;
            
            this.background_music = Background_music;
            MediaPlayer.IsRepeating = true;

            pos_Novo_Jogo.X = 0;        pos_Novo_Jogo.Y = 280;
            pos_Ler_Jogo.X = 0;         pos_Ler_Jogo.Y = pos_Novo_Jogo.Y + 100;
            pos_Sair.X = 0;             pos_Sair.Y = pos_Ler_Jogo.Y + 100;
        }

        public void Load_Menu_Items(Texture2D novo_jogo, Texture2D ler_jogo, Texture2D sair)
        {
            this.novo_Jogo = novo_jogo;
            this.ler_Jogo = ler_jogo;
            this.sair = sair;
        }

        public void Play_Music()
        {
            if (isMusicPlaying==false)
            {
                MediaPlayer.Play(background_music);
                isMusicPlaying = true;
            }
        }

        public void Play_Whip(SoundEffect Whip)
        {
            if (isWhipPlaying== false)
            {
                SoundEffect sfxWhip = Whip;
                sfxWhip.Play();
                isWhipPlaying= true;
            }
        }

        public void Stop_Play_Whip()
        {
            isWhipPlaying = false;
        }

        public void Stop_Music()
        {
            MediaPlayer.Stop();
            isMusicPlaying = false;
        }
    }
}
