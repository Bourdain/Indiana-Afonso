using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Soopah.Xna.Input;
namespace IndianaJones
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        bool tapete = true;
        


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;        
        string Modo;
        #region "Nivel"
        Plataforma[] platforms = new Plataforma[50];
        Backgrounds[] backgrounds= new Backgrounds[5];
        Mumia[] mumia = new Mumia[10];
        Diamante[] diamante = new Diamante[10];
        Aranha[] aranha = new Aranha[5];
        Armadilhas[] armadilhas = new Armadilhas[100];
        SpriteFont font = null;
        Saida saida;
        Vector2 pos_player_nivel;
        bool timer_inicializado=false;
        float timer;
        bool ja_tocou_efeito_sonoro_do_relogio=false;
        #endregion

        int nivel = 1;
        float dificultade = 0.0f;

        Song Tempo;

        #region "GAMEOVER"
        bool GameOver = false;
        int GameOverScore = 0;
        int GameOverTime = 0;
        bool new_high_score = false;
        bool new_high_time = false;
        bool game_over_music = false;
        #endregion
        Texture2D pausa;

        bool isPaused;
        bool isEscapePressed;

        bool isMuted;
        bool isVPressed;

        int nivel_dificuldade = 0;

        bool isJumpPressed=false;

        bool mensagem_de_erro = false;
        int segundos_que_a_mensagem_aparece = 3;
        float segundos_que_passaram = 0;

        bool isHighscoreRead = false;
        int howMuchHasJumped = 0;
        int isNowDescending = 75;
        #region MAIN_MENU_VARS

        Main_Menu main_menu;

        float[] selectedMenu = new float[3];
        bool isInMainMenu = true;
        bool hasDownBeenPressed = false;
        bool hasUpBeenPressed = false;
        bool isFPressed = false;
        bool hasMoved = false;
        bool Aventura = false;
        bool Sobrevivencia = false;
        #endregion
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
      
            // TODO: Add your initialization logic here
            #region "Efeitos Sonoros"
            Tempo = Content.Load<Song>("Musicas\\tempo");
            Player.Jump = Content.Load<SoundEffect>("Efeitos Sonoros\\Jump");
            Player.Morrer_Monstro= Content.Load<SoundEffect>("Efeitos Sonoros\\Killed");
            Player.Morrer_Relogio = Content.Load<SoundEffect>("Efeitos Sonoros\\clockchime1");
            Diamante.efeito_sonoro = Content.Load<SoundEffect>("Efeitos Sonoros\\cashreg");
            Diamante.efeito_sonoro_vidas = Content.Load<SoundEffect>("Efeitos Sonoros\\mais_vidas");
            Diamante.efeito_sonoro_bonus = Content.Load<SoundEffect>("Efeitos Sonoros\\woohoo");


            #endregion

            if (Soopah.Xna.Input.DirectInputGamepad.Gamepads.Count < 1)
            {
               // System.Windows.Forms.MessageBox.Show("O tapete nao foi encontrado");
                tapete = false;
            }


            font = Content.Load<SpriteFont>("Fonts\\Indiana");
            font.Spacing = 1;
            Player.isJumping = false;
            pausa = Content.Load<Texture2D>("Pausa");
            main_menu = new Main_Menu(Content.Load<Texture2D>("Backgrounds\\Menu_Background"),Content.Load<Song>("Musicas\\gamestartup7"));
            selectedMenu[0] = 1.3f;
            selectedMenu[1] = 1;
            selectedMenu[2] = 1;
            isPaused = false;
            Player.Textura = Content.Load<Texture2D>("Jogador\\Idle");
            Player.Textura_run = Content.Load<Texture2D>("Jogador\\Run");
            Player.Textura_saltar = Content.Load<Texture2D>("Jogador\\Jump");
            Player.SetNormalRect();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            main_menu.Load_Menu_Items(Content.Load<Texture2D>("Menu\\NOVO JOGO"), Content.Load<Texture2D>("Menu\\LER JOGO"), Content.Load<Texture2D>("Menu\\SAIR"));
            main_menu.Indiana = Content.Load<Texture2D>("Menu\\Indiana");
            main_menu.Afonso = Content.Load<Texture2D>("Menu\\Afonso");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void SetGamma(bool _bCalibrate, float _fGamma)
        {
            ////////////////////////////////////////////////////////////////////////// 
            // Get the gamma correction ramp. 
            GammaRamp gamma = GraphicsDevice.GetGammaRamp();

            // Clamp the gamma parameter. 
            _fGamma = MathHelper.Clamp(_fGamma, -1f, 1f);
            // 
            ////////////////////////////////////////////////////////////////////////// 

            ////////////////////////////////////////////////////////////////////////// 
            // Local variables. 
            short[] sRed = new short[256];
            short[] sGreen = new short[256];
            short[] sBlue = new short[256];
            // 
            ////////////////////////////////////////////////////////////////////////// 

            ////////////////////////////////////////////////////////////////////////// 
            // Run through and set the rgb values. 
            for (int i = 255; i >= 0; --i)
                sRed[i] = sGreen[i] = sBlue[i] = (short)((int)Math.Min(255, i * (_fGamma + 1.0f)) << 8);
            // 
            ////////////////////////////////////////////////////////////////////////// 

            ////////////////////////////////////////////////////////////////////////// 
            // Set the rgb components of the gamma ramp. 
            gamma.SetRed(sRed);
            gamma.SetGreen(sGreen);
            gamma.SetBlue(sBlue);
            // 
            ////////////////////////////////////////////////////////////////////////// 

            ////////////////////////////////////////////////////////////////////////// 
            // Set the gamma correction ramp. 
            GraphicsDevice.SetGammaRamp(_bCalibrate, gamma);
            // 
            ////////////////////////////////////////////////////////////////////////// 
        } 

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
#region MAIN_MENU
            if (isInMainMenu == true&& GameOver==false)
            {
                if (tapete == true)
                {
                    #region MUTE
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.V) && (DirectInputGamepad.Gamepads[0].Buttons.List[7] == ButtonState.Released))
                    {
                        isVPressed = false;
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) || (DirectInputGamepad.Gamepads[0].Buttons.List[7] == ButtonState.Pressed)) && isVPressed == false && isMuted == false)
                    {
                        isVPressed = true;
                        MediaPlayer.Volume = 0;
                        isMuted = true;
                    }

                    else if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) || (DirectInputGamepad.Gamepads[0].Buttons.List[7] == ButtonState.Pressed)) && isVPressed == false && isMuted == true)
                    {
                        isVPressed = true;
                        MediaPlayer.Volume = 100;
                        isMuted = false;
                    }
                    #endregion
                    #region Sair
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape) || (DirectInputGamepad.Gamepads[0].Buttons.List[4] == ButtonState.Pressed))
                    {
                        this.Exit();
                    }
                    #endregion
                    #region Fullscreen
                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F) || (DirectInputGamepad.Gamepads[0].Buttons.List[6] == ButtonState.Pressed)) && isFPressed == false)
                    {
                        graphics.ToggleFullScreen();
                        isFPressed = true;
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.F) && (DirectInputGamepad.Gamepads[0].Buttons.List[6] == ButtonState.Released))
                    {
                        isFPressed = false;
                    }
                    #endregion
                    #region Info_Form
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.LeftControl) && Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.I))
                    {
                        Forms.Informacao info = new IndianaJones.Forms.Informacao();
                        if (info.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                        }
                    }
                    #endregion

                    #region MOVE_MENU
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) || (DirectInputGamepad.Gamepads[0].Buttons.List[2] == ButtonState.Pressed))
                    {
                        hasUpBeenPressed = true;
                        main_menu.Play_Whip(Content.Load<SoundEffect>("Efeitos Sonoros\\main_menu_item"));
                        if (hasMoved == false)
                        {
                            if (selectedMenu[0] != 1)//1.3f
                            {
                                selectedMenu[0] = 1;
                                selectedMenu[1] = 1;
                                selectedMenu[2] = 1.3f;
                            }

                            else if (selectedMenu[1] != 1)//1.3f
                            {
                                selectedMenu[0] = 1.3f;
                                selectedMenu[1] = 1;
                                selectedMenu[2] = 1;
                            }

                            else  //(selectedMenu[2] != 1)//1.3f
                            {
                                selectedMenu[0] = 1;
                                selectedMenu[1] = 1.3f;
                                selectedMenu[2] = 1;
                            }
                            hasMoved = true;
                        }
                    }

                    if (hasUpBeenPressed == true)
                    {
                        if ((Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Up) && (DirectInputGamepad.Gamepads[0].Buttons.List[2] == ButtonState.Released)))
                        {
                            main_menu.Stop_Play_Whip();
                            hasUpBeenPressed = false;
                            hasMoved = false;

                        }
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) || (DirectInputGamepad.Gamepads[0].Buttons.List[1] == ButtonState.Pressed)))
                    {
                        hasDownBeenPressed = true;
                        main_menu.Play_Whip(Content.Load<SoundEffect>("Efeitos Sonoros\\main_menu_item"));
                        if (hasMoved == false)
                        {
                            if (selectedMenu[0] != 1)//1.3f
                            {
                                selectedMenu[0] = 1;
                                selectedMenu[1] = 1.3f;
                                selectedMenu[2] = 1;
                            }

                            else if (selectedMenu[1] != 1)//1.3f
                            {
                                selectedMenu[0] = 1;
                                selectedMenu[1] = 1;
                                selectedMenu[2] = 1.3f;
                            }

                            else  //(selectedMenu[2] != 1)//1.3f
                            {
                                selectedMenu[0] = 1.3f;
                                selectedMenu[1] = 1;
                                selectedMenu[2] = 1;
                            }
                            hasMoved = true;
                        }
                    }

                    if (hasDownBeenPressed == true)
                    {
                        if ((Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Down) && (DirectInputGamepad.Gamepads[0].Buttons.List[1] == ButtonState.Released)))
                        {
                            main_menu.Stop_Play_Whip();
                            hasDownBeenPressed = false;
                            hasMoved = false;
                        }
                    }
                    #endregion
                    #region MENU_PRESS
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) || (DirectInputGamepad.Gamepads[0].Buttons.List[8] == ButtonState.Pressed))
                    {
                        if (selectedMenu[0] != 1)
                        {
                            main_menu.Stop_Music();
                            isInMainMenu = false;
                            Aventura = true;
                        }

                        if (selectedMenu[1] != 1)
                        {
                            main_menu.Stop_Music();
                            isInMainMenu = false;
                            Sobrevivencia = true;
                        }

                        else if (selectedMenu[2] != 1)
                        {
                            this.Exit();
                        }
                    }
                    #endregion
                }
                else
                {
                    #region MUTE
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.V))
                    {
                        isVPressed = false;
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) ) && isVPressed == false && isMuted == false)
                    {
                        isVPressed = true;
                        MediaPlayer.Volume = 0;
                        isMuted = true;
                    }

                    else if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) ) && isVPressed == false && isMuted == true)
                    {
                        isVPressed = true;
                        MediaPlayer.Volume = 100;
                        isMuted = false;
                    }
                    #endregion
                    #region Sair
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape) )
                    {
                        this.Exit();
                    }
                    #endregion
                    #region Fullscreen
                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F) ) && isFPressed == false)
                    {
                        graphics.ToggleFullScreen();
                        isFPressed = true;
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.F) )
                    {
                        isFPressed = false;
                    }
                    #endregion
                    #region Info_Form
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.LeftControl) )
                    {
                        Forms.Informacao info = new IndianaJones.Forms.Informacao();
                        if (info.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {

                        }
                    }
                    #endregion

                    #region MOVE_MENU
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) )
                    {
                        hasUpBeenPressed = true;
                        main_menu.Play_Whip(Content.Load<SoundEffect>("Efeitos Sonoros\\main_menu_item"));
                        if (hasMoved == false)
                        {
                            if (selectedMenu[0] != 1)//1.3f
                            {
                                selectedMenu[0] = 1;
                                selectedMenu[1] = 1;
                                selectedMenu[2] = 1.3f;
                            }

                            else if (selectedMenu[1] != 1)//1.3f
                            {
                                selectedMenu[0] = 1.3f;
                                selectedMenu[1] = 1;
                                selectedMenu[2] = 1;
                            }

                            else  //(selectedMenu[2] != 1)//1.3f
                            {
                                selectedMenu[0] = 1;
                                selectedMenu[1] = 1.3f;
                                selectedMenu[2] = 1;
                            }
                            hasMoved = true;
                        }
                    }

                    if (hasUpBeenPressed == true)
                    {
                        if ((Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Up) ))
                        {
                            main_menu.Stop_Play_Whip();
                            hasUpBeenPressed = false;
                            hasMoved = false;

                        }
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) ))
                    {
                        hasDownBeenPressed = true;
                        main_menu.Play_Whip(Content.Load<SoundEffect>("Efeitos Sonoros\\main_menu_item"));
                        if (hasMoved == false)
                        {
                            if (selectedMenu[0] != 1)//1.3f
                            {
                                selectedMenu[0] = 1;
                                selectedMenu[1] = 1.3f;
                                selectedMenu[2] = 1;
                            }

                            else if (selectedMenu[1] != 1)//1.3f
                            {
                                selectedMenu[0] = 1;
                                selectedMenu[1] = 1;
                                selectedMenu[2] = 1.3f;
                            }

                            else  //(selectedMenu[2] != 1)//1.3f
                            {
                                selectedMenu[0] = 1.3f;
                                selectedMenu[1] = 1;
                                selectedMenu[2] = 1;
                            }
                            hasMoved = true;
                        }
                    }

                    if (hasDownBeenPressed == true)
                    {
                        if ((Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Down) ))
                        {
                            main_menu.Stop_Play_Whip();
                            hasDownBeenPressed = false;
                            hasMoved = false;
                        }
                    }
                    #endregion
                    #region MENU_PRESS
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter) )
                    {
                        if (selectedMenu[0] != 1)
                        {
                            main_menu.Stop_Music();
                            isInMainMenu = false;
                            Aventura = true;
                        }

                        if (selectedMenu[1] != 1)
                        {
                            main_menu.Stop_Music();
                            isInMainMenu = false;
                            Sobrevivencia = true;
                        }

                        else if (selectedMenu[2] != 1)
                        {
                            this.Exit();
                        }
                    }
                    #endregion
                }
            }
#endregion
            if (tapete == true)
            {
                #region "PAUSE"
                if (!GameOver)
                {


                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Escape) && (DirectInputGamepad.Gamepads[0].Buttons.List[9] == ButtonState.Released))
                    {
                        isEscapePressed = false;
                    }



                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape) || (DirectInputGamepad.Gamepads[0].Buttons.List[9] == ButtonState.Pressed)) && isEscapePressed == false)
                    {
                        isEscapePressed = true;


                        if (isPaused == false)
                        {
                            isPaused = true;
                            MediaPlayer.Pause();
                        }
                        else
                        {
                            isPaused = false;
                            MediaPlayer.Resume();
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region "PAUSE"
                if (!GameOver)
                {


                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Escape))
                    {
                        isEscapePressed = false;
                    }



                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape) ) && isEscapePressed == false)
                    {
                        isEscapePressed = true;


                        if (isPaused == false)
                        {
                            isPaused = true;
                            MediaPlayer.Pause();
                        }
                        else
                        {
                            isPaused = false;
                            MediaPlayer.Resume();
                        }
                    }
                }
                #endregion
            }
            if (isInMainMenu == false && isPaused==false)
            {
                UpdateSprite(gameTime);
            }
            base.Update(gameTime);
            if (tapete == true)
            {
                #region Pausa_butoes
                if (isPaused == true)
                {
                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S) || (DirectInputGamepad.Gamepads[0].Buttons.List[4] == ButtonState.Pressed)) && isPaused == true)
                    {
                        this.Exit();
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.M) || (DirectInputGamepad.Gamepads[0].Buttons.List[5] == ButtonState.Pressed)) && isPaused == true)
                    {
                        Modo = "";
                        isInMainMenu = true;
                        timer = 0;
                        ja_tocou_efeito_sonoro_do_relogio = false;
                        Score.score_jogo = 0;
                        isPaused = false;
                        nivel_dificuldade = 0;
                        dificultade = 0.0f;
                        nivel = 1;
                        timer_inicializado = false;
                        Player.Vidas = 3;
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F) || (DirectInputGamepad.Gamepads[0].Buttons.List[6] == ButtonState.Pressed)) && isFPressed == false)
                    {
                        graphics.ToggleFullScreen();
                        isFPressed = true;
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.F) && (DirectInputGamepad.Gamepads[0].Buttons.List[6] == ButtonState.Released))
                    {
                        isFPressed = false;
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.V) && (DirectInputGamepad.Gamepads[0].Buttons.List[7] == ButtonState.Released))
                    {
                        isVPressed = false;
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) || (DirectInputGamepad.Gamepads[0].Buttons.List[7] == ButtonState.Pressed)) && isVPressed == false && isMuted == false)
                    {
                        isVPressed = true;
                        MediaPlayer.Volume = 0;
                        isMuted = true;
                    }

                    else if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) || (DirectInputGamepad.Gamepads[0].Buttons.List[7] == ButtonState.Pressed)) && isVPressed == false && isMuted == true)
                    {
                        isVPressed = true;
                        MediaPlayer.Volume = 100;
                        isMuted = false;
                    }

                }
                #endregion
            }
            else
            {
                #region Pausa_butoes
                if (isPaused == true)
                {
                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S) ) && isPaused == true)
                    {
                        this.Exit();
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.M) ) && isPaused == true)
                    {
                        Modo = "";
                        isInMainMenu = true;
                        timer = 0;
                        ja_tocou_efeito_sonoro_do_relogio = false;
                        Score.score_jogo = 0;
                        isPaused = false;
                        nivel_dificuldade = 0;
                        dificultade = 0.0f;
                        nivel = 1;
                        timer_inicializado = false;
                        Player.Vidas = 3;
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F) ) && isFPressed == false)
                    {
                        graphics.ToggleFullScreen();
                        isFPressed = true;
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.F) )
                    {
                        isFPressed = false;
                    }

                    if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.V))
                    {
                        isVPressed = false;
                    }

                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) ) && isVPressed == false && isMuted == false)
                    {
                        isVPressed = true;
                        MediaPlayer.Volume = 0;
                        isMuted = true;
                    }

                    else if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.V) ) && isVPressed == false && isMuted == true)
                    {
                        isVPressed = true;
                        MediaPlayer.Volume = 100;
                        isMuted = false;
                    }

                }
                #endregion
            }
        }

        void UpdateSprite(GameTime gameTime)
        {
            Player.posicao.Y = MathHelper.Clamp(Player.posicao.Y, 0, graphics.GraphicsDevice.Viewport.Height - Player.Textura.Height);
            Player.posicao.X = MathHelper.Clamp(Player.posicao.X, 0, graphics.GraphicsDevice.Viewport.Width - Player.Textura.Width);

            foreach (Plataforma plataformas in platforms)
            {
                if (plataformas != null)
                {
                    if (plataformas.bb_plataforma_topo.Intersects(Player.bounding_box_player_feet))
                    {
                        Player.posicao.Y = plataformas.bb_plataforma_topo.Min.Y - Player.Textura.Height - 5;
                        howMuchHasJumped = 0;
                        Player.isJumping = false;
                        Player.UpdateBoundingBox();
                    }
                }
            }

            Player.UpdateBoundingBox();


            if (tapete == true)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right) || (DirectInputGamepad.Gamepads[0].Buttons.List[3] == ButtonState.Pressed))
                {
                    bool intersect = false;
                    foreach (Plataforma plataformas in platforms)
                    {
                        if (plataformas != null)
                        {
                            if (Player.bounding_box_player.Intersects(plataformas.bb_plataforma_esq))
                            {
                                intersect = true;
                            }
                        }
                    }


                    if (!intersect)
                    {
                        if (dificultade > 1f)
                            Player.posicao.X += Player.velocidade_Horizontal - 1f;
                        else
                            Player.posicao.X += Player.velocidade_Horizontal - dificultade;
                        if (!Player.isJumping)
                        {
                            Player.estado = Player.Estado.CorrerDir;
                            //Player.frameActual = 0;
                        }
                    }
                }
            }
            else
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right) )
                {
                    bool intersect = false;
                    foreach (Plataforma plataformas in platforms)
                    {
                        if (plataformas != null)
                        {
                            if (Player.bounding_box_player.Intersects(plataformas.bb_plataforma_esq))
                            {
                                intersect = true;
                            }
                        }
                    }


                    if (!intersect)
                    {
                        if (dificultade > 1f)
                            Player.posicao.X += Player.velocidade_Horizontal - 1f;
                        else
                            Player.posicao.X += Player.velocidade_Horizontal - dificultade;
                        if (!Player.isJumping)
                        {
                            Player.estado = Player.Estado.CorrerDir;
                            //Player.frameActual = 0;
                        }
                    }
                }
            }

            if (tapete == true)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) || (DirectInputGamepad.Gamepads[0].Buttons.List[0] == ButtonState.Pressed))
                {

                    bool intersect = false;
                    foreach (Plataforma plataformas in platforms)
                    {
                        if (plataformas != null)
                        {
                            if (Player.bounding_box_player.Intersects(plataformas.bb_plataforma_dir))
                            {
                                intersect = true;
                            }
                        }
                    }

                    if (!intersect)
                    {
                        if (dificultade > 1f)
                            Player.posicao.X -= Player.velocidade_Horizontal - 1f;
                        else
                            Player.posicao.X -= Player.velocidade_Horizontal - dificultade;

                        if (!Player.isJumping)
                        {
                            Player.estado = Player.Estado.CorrerEsq;
                            // Player.frameActual = 0;
                        }
                    }
                }
            }
            else
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
                {

                    bool intersect = false;
                    foreach (Plataforma plataformas in platforms)
                    {
                        if (plataformas != null)
                        {
                            if (Player.bounding_box_player.Intersects(plataformas.bb_plataforma_dir))
                            {
                                intersect = true;
                            }
                        }
                    }

                    if (!intersect)
                    {
                        if (dificultade > 1f)
                            Player.posicao.X -= Player.velocidade_Horizontal - 1f;
                        else
                            Player.posicao.X -= Player.velocidade_Horizontal - dificultade;

                        if (!Player.isJumping)
                        {
                            Player.estado = Player.Estado.CorrerEsq;
                            // Player.frameActual = 0;
                        }
                    }
                }
            }
            Player.UpdateBoundingBox();

            Jump();
            Mexer_Inimigos();
            Mexer_Plataformas();
            Interacção_Inimigos();
            Animar_jogador(gameTime);
            Animar_inimigos(gameTime);
            if(Modo=="Aventura")
            Apanhar_Diamantes();

            if (Modo == "Sobrevivencia")
                Adicionar_Inimigos();
            
            Picos_Update();
            Contar_tempo(gameTime);

            if(Modo=="Aventura")
            Mudar_Nivel();

            Player.UpdateBoundingBox();
            
            Player.posicao.Y = MathHelper.Clamp(Player.posicao.Y, 0, graphics.GraphicsDevice.Viewport.Height - Player.Textura.Height);
            Player.posicao.X = MathHelper.Clamp(Player.posicao.X, 0, graphics.GraphicsDevice.Viewport.Width - Player.Textura.Width);
            
            Player.UpdateBoundingBox();
         
        }

        void Animar_inimigos(GameTime gameTime)
        {
            for (int i = 0; i < mumia.Length; i++)
            {
                if (mumia[i] != null)
                {
                    float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    mumia[i].timer += deltaTime;
                    if (mumia[i].timer > mumia[i].intervalo)
                    {
                        mumia[i].frameActual++;
                        if (mumia[i].frameActual > mumia[i].frameCount_Correr - 1)
                        {
                            mumia[i].frameActual = 0;
                        }
                        mumia[i].timer = 0f;
                    }
                }
            }

        }

        void Animar_jogador(GameTime gameTime)
        { 
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (Player.isJumping == true)
            {
                Player.estado = Player.Estado.Saltar;
                //Player.frameActual = 0;
            }


            switch(Player.estado)
            {
            case Player.Estado.Saltar:
            Player.timer+= deltaTime;
            if (Player.timer >Player.intervalo)
            {
                Player.frameActual++;
                if (Player.frameActual > Player.frameCount_Saltar- 1)
                {
                    Player.frameActual = 0;
                }
                Player.timer = 0f;
            }
            break;




                case Player.Estado.CorrerDir:
            Player.timer += deltaTime;
            if (Player.timer > Player.intervalo)
            {
                Player.frameActual++;
                if (Player.frameActual > Player.frameCount_Correr - 1)
                {
                    Player.frameActual = 0;
                }
                Player.timer = 0f;
            }
            break;



                case Player.Estado.CorrerEsq:
            Player.timer += deltaTime;
            if (Player.timer > Player.intervalo)
            {
                Player.frameActual++;
                if (Player.frameActual > Player.frameCount_Correr - 1)
                {
                    Player.frameActual = 0;
                }
                Player.timer = 0f;
            }
            break;
            }
        }

        void Adicionar_Inimigos()
        {
            switch ((int)timer)
            {
               case 5: if (aranha[0] == null) 
                {
                    aranha[0] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(0, 0));
                    aranha[0].UpdatePosicaoDestino(new Vector2(Random_Number.Numero.Next(0, 760), Random_Number.Numero.Next(0, 580)));
                }
                    break;

                case 10: if (aranha[1] == null)
                    {
                        aranha[1] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(799, 1));
                        aranha[1].UpdatePosicaoDestino(new Vector2(Random_Number.Numero.Next(0, 760), Random_Number.Numero.Next(0, 580)));
                    }
                    break;

                case 15: if (aranha[2] == null)
                    {
                        aranha[2] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(1, 599));
                        aranha[2].UpdatePosicaoDestino(new Vector2(Random_Number.Numero.Next(0, 760), Random_Number.Numero.Next(0, 580)));
                    }
                    break;

                case 20: if (aranha[3] == null)
                    {
                        aranha[3] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(800, 600));
                        aranha[3].UpdatePosicaoDestino(new Vector2(Random_Number.Numero.Next(0, 760), Random_Number.Numero.Next(0, 580)));
                    }
                    break;

                case 25: platforms[0] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[0].posicao.X, platforms[0].posicao.Y), true);
                    platforms[12] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[12].posicao.X, platforms[12].posicao.Y), true,50,50);
                    break;
                case 26: platforms[1] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[1].posicao.X, platforms[1].posicao.Y), true);
                    platforms[13] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[13].posicao.X, platforms[13].posicao.Y), true,50,50);
                    break;
                case 27: platforms[2] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[2].posicao.X, platforms[2].posicao.Y), true);
                    platforms[14] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[14].posicao.X, platforms[14].posicao.Y), true,50,50);
                    break;

                case 40: platforms[3] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[3].posicao.X, platforms[3].posicao.Y),true);
                         platforms[9] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[9].posicao.X, platforms[9].posicao.Y), true,50,50);
                    break;
                case 41: platforms[4] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[4].posicao.X, platforms[4].posicao.Y),true);
                         platforms[10] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[10].posicao.X, platforms[10].posicao.Y), true,50,50);
                    break;
                case 42: platforms[5] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[5].posicao.X, platforms[5].posicao.Y),true);
                         platforms[11] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[11].posicao.X, platforms[11].posicao.Y), true, 50, 50);
                    break;

                case 50: platforms[6] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[6].posicao.X, platforms[6].posicao.Y), true, 50, 50);
                    platforms[7] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(380 + 15, 300), true, 50, 0, true, 2);
                    platforms[8] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB1"), new Vector2(platforms[8].posicao.X, platforms[8].posicao.Y), true,50, 0);
                    break;

                case 55: aranha[4] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"),new Vector2(400,-100));
                    aranha[4].UpdatePosicaoDestino(new Vector2(Random_Number.Numero.Next(0, 760), Random_Number.Numero.Next(0, 580)));
                    break;

                case 60: armadilhas[95] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(400-32, 0),true,50,0,true,5);
                         armadilhas[96] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Cima"), new Vector2(400 - 32, 0+32), true, 50, 0, true, 5);
                         aranha[5] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(400, -100));
                         aranha[5].UpdatePosicaoDestino(new Vector2(Random_Number.Numero.Next(0, 760), Random_Number.Numero.Next(0, 580)));
                         break;
                case 61: armadilhas[97] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(400, 0), true, 50, 0, true, 5);
                         armadilhas[98] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Cima"), new Vector2(400, 0 + 32), true, 50, 0, true, 5);
                         break;
                case 65: armadilhas[99] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Direita"), new Vector2(800-32, 200), true, 50, 50, false, 5);
                         armadilhas[100] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Esquerda"), new Vector2(800, 200), true, 50, 50, false, 5);
                         armadilhas[101] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Direita"), new Vector2(0 - 32, 200), true, 50, 0, false, 5);
                         armadilhas[102] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Esquerda"), new Vector2(0, 200), true, 50, 0, false, 5);

                         aranha[6] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(400, -100));
                         aranha[6].UpdatePosicaoDestino(new Vector2(Random_Number.Numero.Next(0, 760), Random_Number.Numero.Next(0, 580)));
                         
                         break;
            }

        }

        void Mexer_Plataformas()
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                if (platforms[i] != null)
                {
                    if (platforms[i].isMovable == true)
                    {
                        if (platforms[i].percurso == platforms[i].percurso_max)
                            platforms[i].direccao = false;
                        else if (platforms[i].percurso == platforms[i].percurso_min)
                            platforms[i].direccao = true;


                        if (platforms[i].direccao == false)
                        {
                            if (platforms[i].vertical == false)
                            {
                                if (platforms[i].bb_plataforma_total.Intersects(Player.bounding_box_player) || Player.bounding_box_player_feet.Intersects(platforms[i].bb_plataforma_topo)) //|| !Player.bounding_box_player.Intersects(platforms[i].bb_plataforma_esq) || !Player.bounding_box_player.Intersects(platforms[i].bb_plataforma_dir))
                                {
                                    Player.posicao.X -= platforms[i].velocidade + dificultade;
                                    Player.UpdateBoundingBox();
                                }
                                platforms[i].posicao.X -= platforms[i].velocidade + dificultade;
                            }
                            else

                                platforms[i].posicao.Y -= platforms[i].velocidade;
                            platforms[i].percurso--;
                        }

                        else if (platforms[i].direccao == true)
                        {
                            if (platforms[i].vertical == false)
                            {
                                if (Player.bounding_box_player_feet.Intersects(platforms[i].bb_plataforma_topo) || platforms[i].bb_plataforma_total.Intersects(Player.bounding_box_player))
                                {
                                    Player.posicao.X += platforms[i].velocidade + dificultade;
                                    Player.UpdateBoundingBox();
                                }
                                platforms[i].posicao.X += platforms[i].velocidade + dificultade;
                            }
                            else
                                platforms[i].posicao.Y += platforms[i].velocidade;
                            platforms[i].percurso++;
                        }
                        platforms[i].UpdateBoundingBox();
                    }
                }
            }
        }

        void Mudar_Nivel()
        {
            if (saida != null)
            {
                saida.UpdateBoundingBox();
                if (saida.bbox.Intersects(Player.bounding_box_player))
                {
                    Score.Nivel_Completo(dificultade);
                    dificultade += 0.1f;
                    nivel_dificuldade++;
                    MediaPlayer.Stop();
                        if (nivel == 1)
                        {
                            Nivel_1.nivel_dif++;

                            Nivel_2 lvlseguinte = new Nivel_2(Content);
                            platforms = lvlseguinte.plataformas;
                            backgrounds = lvlseguinte.fundos;
                            mumia = lvlseguinte.mumia;
                            aranha = lvlseguinte.aranha;
                            diamante = lvlseguinte.diamante;
                            saida = lvlseguinte.saida;
                            armadilhas = lvlseguinte.armadilhas;
                            timer = lvlseguinte.timer;

                            #region "Adicionar inimigos consoante a dificuldade"
                            if (Nivel_2.nivel_dif >= 1)
                                aranha[0] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(0, 0));

                            if (Nivel_2.nivel_dif >= 2)
                                aranha[1] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(350, 220));

                            if (Nivel_2.nivel_dif >= 3)
                                aranha[2] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(150, 100));
                            #endregion

                            if (Nivel_2.nivel_dif >= 2)
                            timer = lvlseguinte.timer-60;

                            else if (Nivel_2.nivel_dif >= 3)
                                timer = Nivel_2.nivel_dif - 75;

                            else if (Nivel_2.nivel_dif >= 4)
                            timer = lvlseguinte.timer - 80; 


                            Player.posicao = lvlseguinte.start;
                            ja_tocou_efeito_sonoro_do_relogio = false;
                            pos_player_nivel = lvlseguinte.start;
                            nivel++;
                            MediaPlayer.Play(Nivel_2.background_music);
                            MediaPlayer.IsRepeating = true;
                        }
                        else if (nivel == 2)
                        {
                            Nivel_2.nivel_dif++;

                            Nivel3 lvlseguinte = new Nivel3(Content);
                            platforms = lvlseguinte.plataformas;
                            backgrounds = lvlseguinte.fundos;
                            mumia = lvlseguinte.mumia;
                            aranha = lvlseguinte.aranha;
                            diamante = lvlseguinte.diamante;
                            saida = lvlseguinte.saida;
                            armadilhas = lvlseguinte.armadilhas;
                            timer = lvlseguinte.timer;

                            if (Nivel3.nivel_dif >= 1)
                            {
                             armadilhas[0] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"),new Vector2(600-32,460));
                             armadilhas[1] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(600, 460));
                             armadilhas[2] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(600+32*1, 460));
                             armadilhas[3] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(600 + 32 * 2, 460));
                             armadilhas[4] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(600 + 32 * 3, 460));
                             armadilhas[5] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(600 + 32 * 4, 460));
                             armadilhas[6] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(600 + 32 * 5, 460));

                             platforms[9] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600-32, 460+32));
                             platforms[10] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600 + 32*0, 460 + 32));
                             platforms[11] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600 + 32 * 1, 460 + 32));
                             platforms[12] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600 + 32 * 2, 460 + 32));
                             platforms[13] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600 + 32 * 3, 460 + 32));
                             platforms[14] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600 + 32 * 4, 460 + 32));
                             platforms[15] = new Plataforma(Content.Load<Texture2D>("Plataformas\\BlockB0"), new Vector2(600 + 32 * 5, 460 + 32));
                            }


                            if (Nivel3.nivel_dif >= 1)
                                timer = lvlseguinte.timer - 20;

                            else if (Nivel3.nivel_dif >=2)
                                timer = lvlseguinte.timer - 25;

                            else if (Nivel3.nivel_dif >= 3)
                                timer = lvlseguinte.timer - 30;


                            Player.posicao = lvlseguinte.start;
                            ja_tocou_efeito_sonoro_do_relogio = false;
                            pos_player_nivel = lvlseguinte.start;
                            nivel++;
                            
                            MediaPlayer.Play(Nivel3.background_music);
                            MediaPlayer.IsRepeating = true;
                        }
                        else if (nivel == 3)
                        {
                            Nivel3.nivel_dif++;

                            Nivel4 lvlseguinte = new Nivel4(Content);
                            platforms = lvlseguinte.plataformas;
                            backgrounds = lvlseguinte.fundos;
                            mumia = lvlseguinte.mumia;
                            aranha = lvlseguinte.aranha;
                            diamante = lvlseguinte.diamante;
                            saida = lvlseguinte.saida;
                            armadilhas = lvlseguinte.armadilhas;
                            timer = lvlseguinte.timer;

                            if (Nivel4.nivel_dif >= 1)
                            {
                                armadilhas[2] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Esquerda"), new Vector2(0, 275));
                                armadilhas[3] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Direita"), new Vector2(180, 275));
                                mumia[0] = new Mumia(Content.Load<Texture2D>("Monstros\\MARun"), new Vector2(550, 453), true);
                            }

                            if (Nivel4.nivel_dif >=2)
                            {
                                    mumia[1] = new Mumia(Content.Load<Texture2D>("Monstros\\MDRun"), new Vector2(550, 453), false);                                
                            }

                            if (Nivel4.nivel_dif >= 3)
                            {
                                aranha[1] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(500, 0));
                                aranha[2] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(500, 0));
                            }

                            if (Nivel4.nivel_dif >= 1)
                                timer = lvlseguinte.timer - 20;

                            else if (Nivel4.nivel_dif >= 2)
                                timer = lvlseguinte.timer - 25;

                            else if (Nivel4.nivel_dif >= 3)
                                timer = lvlseguinte.timer - 30;


                            Player.posicao = lvlseguinte.start;
                            ja_tocou_efeito_sonoro_do_relogio = false;
                            pos_player_nivel = lvlseguinte.start;
                            nivel++;

                            MediaPlayer.Play(Nivel4.background_music);
                            MediaPlayer.IsRepeating = true;
                        }
                        else if (nivel == 4) //mudar o valor para o numero de niveis
                        {
                            Nivel4.nivel_dif++;

                            Nivel_1 lvlseguinte = new Nivel_1(Content);
                            platforms = lvlseguinte.plataformas;
                            backgrounds = lvlseguinte.fundos;
                            mumia = lvlseguinte.mumia;
                            aranha = lvlseguinte.aranha;
                            diamante = lvlseguinte.diamante;
                            saida = lvlseguinte.saida;
                            timer = lvlseguinte.timer;
                            armadilhas = lvlseguinte.armadilhas;
                            #region "Adicionar inimigos consoante a dificuldade"
                            if (Nivel_1.nivel_dif >= 1)
                            {
                                mumia[1] = new Mumia(Content.Load<Texture2D>("Monstros\\MCRun"), new Vector2(100, 260), true); mumia[1].percurso = 0; mumia[1].percurso_max = 75;
                                armadilhas[0] = new Armadilhas(Content.Load<Texture2D>("Picos\\Picos_Baixo"), new Vector2(650, 275));
                            }

                            if (Nivel_1.nivel_dif >= 2)
                            {
                                aranha[0] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(0, 0));
                                aranha[1] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(350, 220));
                            }

                            if (Nivel_1.nivel_dif >= 3)
                            {
                                aranha[2] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(350, 220));
                                mumia[2] = new Mumia(Content.Load<Texture2D>("Monstros\\MDRun"), new Vector2(620, 260), true); mumia[1].percurso = 0; mumia[1].percurso_max = 75;
                            }

                            if (Nivel_1.nivel_dif >= 4)
                            {
                                aranha[3] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(350, 220));
                            }

                            if (Nivel_1.nivel_dif >= 5)
                            {
                                aranha[4] = new Aranha(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(350, 220));
                            }

                            #endregion

                            if (Nivel_1.nivel_dif > 5)
                                timer = lvlseguinte.timer - 20;

                            else if (Nivel_1.nivel_dif > 10)
                                timer = lvlseguinte.timer - 35;

                            else if (Nivel_1.nivel_dif > 15)
                                timer = lvlseguinte.timer - 40;

                            Player.posicao = lvlseguinte.start;
                            ja_tocou_efeito_sonoro_do_relogio = false;
                            pos_player_nivel = lvlseguinte.start;
                            nivel++;
                            nivel = 1;
                            MediaPlayer.Play(Nivel_1.background_music);
                            MediaPlayer.IsRepeating = true;
                        }

                       
                    
                }
            }
        }

        void Contar_tempo(GameTime gameTime)
        {
            if (Modo == "Aventura")
            {
                if (mensagem_de_erro == true)
                {
                    segundos_que_passaram *= 1000;
                    segundos_que_passaram += gameTime.ElapsedGameTime.Milliseconds;
                    segundos_que_passaram /= 1000;
                }
                if ((int)segundos_que_passaram == segundos_que_a_mensagem_aparece && mensagem_de_erro==true)
                {
                    mensagem_de_erro = false;
                    segundos_que_passaram = 0;
                }

                timer *= 1000;
                timer -= gameTime.ElapsedGameTime.Milliseconds;                
                
                if (timer_inicializado == true)
                {
                    if (timer <= 0)
                    {
                        Morrer("Relogio");
                    }
                    timer /= 1000;
                    if (ja_tocou_efeito_sonoro_do_relogio == false)
                        if (Convert.ToInt32(timer) == 10)
                        {
                            Player.Morrer_Relogio.Play();
                            MediaPlayer.Play(Tempo);
                            ja_tocou_efeito_sonoro_do_relogio = true;
                        }
                }
            }
            else if (Modo == "Sobrevivencia")
            {
                timer *= 1000;
                timer += gameTime.ElapsedGameTime.Milliseconds;
                timer /= 1000;
            }
        }

        void Jump()
        {
            if(tapete==true)
            {
                if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) || (DirectInputGamepad.Gamepads[0].Buttons.List[2] == ButtonState.Pressed)) && Player.isJumping == false && isJumpPressed == false)
                {
                    Player.Jump.Play();
                    foreach (Plataforma plataformas in platforms)
                    {
                        if (plataformas != null)
                        {
                            if (plataformas.bb_plataforma_topo.Intersects(Player.bounding_box_player_feet))
                            {

                            }
                            else
                            {
                                Player.isJumping = true;
                                isJumpPressed = true;
                            }
                        }
                    }
                }   
            }

            else
            {
                if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) ) && Player.isJumping == false && isJumpPressed == false)
                {
                    Player.Jump.Play();
                    foreach (Plataforma plataformas in platforms)
                    {
                        if (plataformas != null)
                        {
                            if (plataformas.bb_plataforma_topo.Intersects(Player.bounding_box_player_feet))
                            {

                            }
                            else
                            {
                                Player.isJumping = true;
                                isJumpPressed = true;
                            }
                        }
                    }
                }  
            }
            if (Player.isJumping == true && howMuchHasJumped < Player.tamanho_salto)
            {
                howMuchHasJumped++;
                Player.posicao.Y -= Player.velocidade_Vertical;
                foreach (Plataforma plataformas in platforms)
                {
                    if (plataformas != null)
                    {
                        if (plataformas.bb_plataforma_esq.Intersects(Player.bounding_box_player))
                        {

                        }
                        else if (plataformas.bb_plataforma_dir.Intersects(Player.bounding_box_player))
                        { 
                        
                        }

                        else if (plataformas.bb_plataforma_total.Intersects(Player.bounding_box_player))
                        {
                            Player.isJumping = false;
                        }
                       
                    }
                }
            }

            else
            {
               
                    if (isNowDescending > 0)
                    {
                            isNowDescending--;
                            Player.posicao.Y += Player.velocidade_Vertical;
                    }
                    else
                    {
                        howMuchHasJumped = 0;
                        Player.isJumping = false;
                        isNowDescending = 75;
                        Player.posicao.Y += Player.velocidade_Vertical;
                    }

                }
                if (isJumpPressed == true)
                {
                    if (tapete == true)
                    {
                        if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Up) && (DirectInputGamepad.Gamepads[0].Buttons.List[2] == ButtonState.Released))
                        {
                            isJumpPressed = false;
                        }
                    }
                    else
                    {
                        if (Keyboard.GetState(PlayerIndex.One).IsKeyUp(Keys.Up) )
                        {
                            isJumpPressed = false;
                        }
                    }
                }
        }
        void Picos_Update()
        {
            for (int i = 0; i < armadilhas.Length; i++)
            {
                if(armadilhas[i]!=null)
                armadilhas[i].UpdateBoundingBox();
            }
        }
        void Mexer_Inimigos()
        {
            #region "ARMADILHA"
            for (int i = 0; i < armadilhas.Length; i++)
            {
                if (armadilhas[i] != null)
                {
                    if (armadilhas[i].isMovable == true)
                    {
                        if (armadilhas[i].percurso == armadilhas[i].percurso_max)
                            armadilhas[i].direccao = false;

                        else if (armadilhas[i].percurso == armadilhas[i].percurso_min)
                            armadilhas[i].direccao = true;

                        if (armadilhas[i].direccao == false)
                        {
                            if (armadilhas[i].vertical == false)
                            {
                                armadilhas[i].posicao.X -= armadilhas[i].velocidade + dificultade;
                                armadilhas[i].percurso--;
                            }

                            else
                            {
                                armadilhas[i].posicao.Y -= armadilhas[i].velocidade + dificultade;
                                armadilhas[i].percurso--;
                            }
                        }

                        else
                        {
                            if (armadilhas[i].vertical == false)
                            {
                                armadilhas[i].posicao.X += armadilhas[i].velocidade + dificultade;
                                armadilhas[i].percurso++;
                            }

                            else
                            {
                                armadilhas[i].posicao.Y += armadilhas[i].velocidade + dificultade;
                                armadilhas[i].percurso++;
                            }
                        }
                        armadilhas[i].UpdateBoundingBox();
                    }
                }
            }
            #endregion

            #region "MUMIA"
            for (int i = 0; i < mumia.Length; i++)
            {
                if (mumia[i] != null)
                {
                    if (mumia[i].percurso == mumia[i].percurso_max)
                        mumia[i].direccao = false;
                    else if (mumia[i].percurso == mumia[i].percurso_min)
                        mumia[i].direccao = true;



                    if (mumia[i].direccao == false)
                    {
                        mumia[i].posicao.X -= mumia[i].velocidade + dificultade;
                        mumia[i].percurso--;
                    }
                    
                    else if (mumia[i].direccao == true)
                    {
                        mumia[i].posicao.X += mumia[i].velocidade + dificultade;
                        mumia[i].percurso++;
                    }
                    mumia[i].UpdateBoundingBox();
                }
            }
            #endregion

            #region "Aranha"
            for (int i = 0; i < aranha.Length; i++)
            {
                if (aranha[i] != null)
                {
                    if (aranha[i].pos_dest == null)
                    {
                        aranha[i].pos_dest.X = (float)Random_Number.Numero.Next(0, 760);
                        aranha[i].pos_dest.Y = (float)Random_Number.Numero.Next(0, 580);
                    }

                    if (aranha[i].posicao == aranha[i].pos_dest)
                    {
                        aranha[i].pos_dest.X = (float)Random_Number.Numero.Next(0, 760);
                        aranha[i].pos_dest.Y = (float)Random_Number.Numero.Next(0, 580);
                    }

                    else
                    {
                        if (aranha[i].posicao.X < aranha[i].pos_dest.X)
                            aranha[i].posicao.X++;
                            

                        else if (aranha[i].posicao.X > aranha[i].pos_dest.X)
                            aranha[i].posicao.X--;
                            

                        if (aranha[i].posicao.Y < aranha[i].pos_dest.Y)
                            aranha[i].posicao.Y++;
                            

                        else if (aranha[i].posicao.Y > aranha[i].pos_dest.Y )
                            aranha[i].posicao.Y--;
                            

                    }
                    aranha[i].UpdateBoundingBox();
                }
            }
            #endregion
        }

        void Interacção_Inimigos()
        {
            #region "Mumia"
            foreach (Mumia mumia_ in mumia)
            {
                if (mumia_ != null)
                {
                    if (mumia_.bbox.Intersects(Player.bounding_box_player))
                    {
                        Morrer("Monstro");
                    }
                }
            }
            #endregion

            #region "Armadilhas"
            foreach (Armadilhas _armadilhas in armadilhas)
            {
                if (_armadilhas != null)
                    if (_armadilhas.bbox.Intersects(Player.bounding_box_player))
                        Morrer("Monstro");
            }
            #endregion

            #region "Aranha"
            foreach (Aranha _aranha in aranha)
            {
                if (_aranha != null)
                {
                    if (_aranha.bbox.Intersects(Player.bounding_box_player))
                    {
                        Morrer("Monstro");
                    }
                }
            }
            #endregion
        }

        void Apanhar_Diamantes()
        {
            for (int i = 0; i < diamante.Length; i++)
            {
                if (diamante[i] != null)
                {
                    diamante[i].Update_Bounding_Box();
                    if (diamante[i].bbox.Intersects(Player.bounding_box_player)==true)
                    {
                        if (diamante[i].especial == 0)
                        {
                            Score.Moeda();
                            Diamante.efeito_sonoro.Play();
                        }

                        if (diamante[i].especial == 1)
                        {
                            Score.Moeda_especial();
                            Diamante.efeito_sonoro_bonus.Play();
                        }

                        if (diamante[i].especial == 2)
                        {
                            Player.Vidas++;
                            Diamante.efeito_sonoro_vidas.Play();
                        }

                        diamante[i].posicao = new Vector2(900, 700);
                        diamante[i].Update_Bounding_Box();
                    }
                }
            }
        }



        void Morrer(string razao)
        {
            if (razao == "Monstro")
            {
                Player.Morrer_Monstro.Play();
                Score.Morte_Monstro();
                if(Player.Vidas!=1)
                mensagem_de_erro = true;
            }
            else if (razao == "Relogio")
            {
                Score.Morte_Relógio();
                Player.Vidas = 1;
                ja_tocou_efeito_sonoro_do_relogio = false;
            }
            Player.Vidas--;
            if (Player.Vidas == 0)
            {
                nivel_dificuldade = 0;
                Nivel_1.nivel_dif = 0;
                Nivel_2.nivel_dif = 0;
                Nivel3.nivel_dif = 0;
                Nivel4.nivel_dif = 0;
                dificultade = 0.0f;
                GameOver = true;
                GameOverScore = Score.score_jogo;
                GameOverTime = (int)timer;
                isInMainMenu = true;
                nivel = 1;
                timer_inicializado = false;                
                Player.Vidas = 3;
                #region Escrever Highscore em ficheiro
                if (Modo == "Aventura")
                {
                    if (Score.score_jogo > Score.Highscore)
                    {
                        new_high_score = true;
                        FileStream fs = new FileStream("highscore.dat", FileMode.Create, FileAccess.Write);

                        BinaryFormatter bf = new BinaryFormatter();

                        bf.Serialize(fs, Score.score_jogo);
                        bf.Serialize(fs, DateTime.Now);

                        fs.Close();
                        isHighscoreRead = false;
                    }
                }

                else
                {
                    if (Score.Hightime < timer)
                    {
                        new_high_time = true;
                        FileStream fs = new FileStream("highscore_tempo.dat", FileMode.Create, FileAccess.Write);

                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, (int)timer);
                        bf.Serialize(fs, DateTime.Now);

                        fs.Close();
                        isHighscoreRead = false;
                    }
                }
                #endregion
                Score.score_jogo = 0;
            }
            Player.posicao = pos_player_nivel;
            Player.UpdateBoundingBox();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            #region MAIN_MENU_DRAW
            if (isInMainMenu == true && GameOver==false)
            {
                if (isHighscoreRead == false)
                {
                    if (!File.Exists("highscore.dat"))
                    {
                        FileStream fs = new FileStream("highscore.dat",FileMode.OpenOrCreate,FileAccess.Write);

                        BinaryFormatter bf = new BinaryFormatter();

                        bf.Serialize(fs, Score.Highscore);
                        bf.Serialize(fs,DateTime.Now);

                        fs.Close();
                    }

                    if (!File.Exists("highscore_tempo.dat"))
                    {
                        FileStream fs = new FileStream("highscore_tempo.dat", FileMode.OpenOrCreate, FileAccess.Write);

                        BinaryFormatter bf = new BinaryFormatter();

                        bf.Serialize(fs, Score.Hightime);
                        bf.Serialize(fs, DateTime.Now);

                        fs.Close();
                    }

                    FileStream fs_ = new FileStream("highscore.dat", FileMode.Open);
                    BinaryFormatter bf_ = new BinaryFormatter();
                    Score.Highscore = (int)bf_.Deserialize(fs_);
                    Score.Data_do_highscore = (DateTime)bf_.Deserialize(fs_);
                    fs_.Close();

                    fs_ = new FileStream("highscore_tempo.dat", FileMode.Open);
                    bf_ = new BinaryFormatter();
                    Score.Hightime = (int)bf_.Deserialize(fs_);
                    Score.Data_do_hightime = (DateTime)bf_.Deserialize(fs_);
                    fs_.Close();

                    isHighscoreRead = true;
                        
                }

                spriteBatch.Draw(main_menu.background, Vector2.Zero, Color.White);
                main_menu.Play_Music();
/*
                MouseState mouse = Mouse.GetState();
            spriteBatch.Draw(Content.Load<Texture2D>("Diamantes\\diamante_bonus"), new Vector2(mouse.X, mouse.Y), Color.White);
            spriteBatch.DrawString(font, "X:" + mouse.X.ToString() + " Y:" + mouse.Y.ToString(), new Vector2(0, 450), Color.White);
*/
            spriteBatch.Draw(main_menu.Indiana, new Vector2(0, 120),null, Color.White,0,Vector2.Zero,1.20f,SpriteEffects.None,0);
                //spriteBatch.Draw(main_menu.Indiana, new Vector2(0, 120), Color.White);
                spriteBatch.Draw(main_menu.Afonso, new Vector2(main_menu.Indiana.Width, 120), null, Color.White, 0, Vector2.Zero, 1.20f, SpriteEffects.None, 0);
               // spriteBatch.Draw(main_menu.Afonso, new Vector2(main_menu.Indiana.Width, 120), Color.White);
                spriteBatch.Draw(main_menu.novo_Jogo, main_menu.pos_Novo_Jogo, null, Color.White, 0, Vector2.Zero, selectedMenu[0],SpriteEffects.None,0);
                spriteBatch.Draw(main_menu.ler_Jogo, main_menu.pos_Ler_Jogo, null, Color.White, 0, Vector2.Zero, selectedMenu[1], SpriteEffects.None, 0);
                spriteBatch.Draw(main_menu.sair, main_menu.pos_Sair, null, Color.White, 0, Vector2.Zero, selectedMenu[2], SpriteEffects.None, 0);
                spriteBatch.DrawString(font, "Highscore: " + Score.Highscore.ToString() + "\n"+"Obtido em: "+Score.Data_do_highscore.ToString(), new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "Hightime: " + Score.Hightime.ToString() + "\n" + "Obtido em: " + Score.Data_do_hightime.ToString(), new Vector2(0, 50), Color.White);
                if (tapete == false)
                    spriteBatch.DrawString(font, "Tapete nao encontrado", new Vector2(0, 570), Color.Red);

                #region "INSTRUÇÕES"
                spriteBatch.Draw(Content.Load<Texture2D>("Diamantes\\diamante"), new Vector2(540, 300), null, Color.White, 0, Vector2.Zero, 1.7f, SpriteEffects.None, 0); spriteBatch.DrawString(font, "<- 20 pontos", new Vector2(560, 310), Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>("Monstros\\Aranha"), new Vector2(515, 340), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0); spriteBatch.DrawString(font, "<- Evitar", new Vector2(600, 350), Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>("Monstros\\monstro1"), new Vector2(550, 400), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0); spriteBatch.DrawString(font, "<- Evitar", new Vector2(600, 400), Color.White);
                spriteBatch.Draw(Content.Load<Texture2D>("Saida\\Saida"), new Vector2(540, 450), null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0); spriteBatch.DrawString(font, "<- Mudar de nivel", new Vector2(600, 450), Color.White);
                spriteBatch.DrawString(font, "Carregue F/[X] para mudar para FullScreen/Janela", new Vector2(200, 550), Color.White);
                if (isMuted == true)
                    spriteBatch.DrawString(font, "     A musica esta desligada!\nCarregue no V/[O] para a ligar.", new Vector2(460, 0), Color.Red);
                else
                    spriteBatch.DrawString(font, "         A musica esta ligada!\nCarregue no V/[O] para a desligar.", new Vector2(440, 0), Color.LightGreen);
                #endregion
            }
            #endregion

            #region "GAME_OVER"
            else if (GameOver == true)
            {
                if (game_over_music == false)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(Content.Load<Song>("Musicas\\Game Over"));
                    game_over_music = true;
                }
                spriteBatch.Draw(Content.Load<Texture2D>("GameOver"), new Vector2(0, 0), Color.White);
         //       spriteBatch.DrawString(font, "GAME\nOVER", new Vector2(360, 260), Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                spriteBatch.DrawString(font, "Carrega no Space Bar/[] para voltar para o menu principal.", new Vector2(000, 450), Color.White);

                if (Modo == "Aventura")
                {
                    if (new_high_score == true)
                    {
                        spriteBatch.DrawString(font, "Parabens! A tua nova pontuacao e: " + GameOverScore.ToString(), new Vector2(0, 500), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Nao superaste o teu velho highscore(" + Score.Highscore.ToString() + "), a tua pontuacao foi: " + GameOverScore.ToString(), new Vector2(000, 500), Color.White);
                    }
                }

                else
                {
                    if (new_high_time == true)
                    {
                        spriteBatch.DrawString(font, "Parabens! O teu novo tempo e: " + GameOverTime.ToString(), new Vector2(0, 500), Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Nao superaste o teu melhor tempo(" + Score.Hightime.ToString() + "), o teu tempo foi: " + GameOverTime.ToString(), new Vector2(000, 500), Color.White);
                    }
                }
                if (tapete == true)
                {
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space) || (DirectInputGamepad.Gamepads[0].Buttons.List[5] == ButtonState.Pressed))
                    {
                        GameOver = false;
                        game_over_music = false;
                        new_high_score = false;
                        new_high_time = false;
                    }
                }
                else
                {
                    if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
                    {
                        GameOver = false;
                        game_over_music = false;
                        new_high_score = false;
                        new_high_time = false;
                    }
                }
            }
            #endregion

            #region GAME_INICIAR
            else
            {
                if (Aventura == true && GameOver == false)
                {
                    Nivel_1 lvl1 = new Nivel_1(Content);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(Nivel_1.background_music);
                    platforms = lvl1.plataformas;
                    backgrounds = lvl1.fundos;
                    mumia = lvl1.mumia;
                    aranha = lvl1.aranha;
                    diamante = lvl1.diamante;
                    saida = lvl1.saida;
                    timer = lvl1.timer;
                    armadilhas = lvl1.armadilhas;
                    timer_inicializado = true;
                    Player.posicao = lvl1.start;
                    Player.UpdateBoundingBox();
                    Aventura = false;
                    pos_player_nivel = lvl1.start;
                    Modo = "Aventura";
                }

                if (Sobrevivencia == true && GameOver == false)
                {
                    Nivel_Sobrevivencia sobr = new Nivel_Sobrevivencia(Content);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(Nivel_Sobrevivencia.background_music);
                    platforms = sobr.plataformas;
                    backgrounds = sobr.fundos;
                    mumia = sobr.mumia;
                    aranha = sobr.aranha;
                    diamante = sobr.diamante;
                    saida = null;
                    timer = sobr.timer;
                    armadilhas = sobr.armadilhas;
                    timer_inicializado = true;
                    Player.posicao = sobr.start;
                    Sobrevivencia=false;
                    pos_player_nivel = sobr.start;
                    Player.Vidas = 1;
                    Modo = "Sobrevivencia";
                }
            #endregion

            Draw_Level();

            }
            
            
            spriteBatch.End();
            

            base.Draw(gameTime);
        }



        public void Draw_Level()
        {
            #region "FUNDOS"
            foreach (Backgrounds fundos in backgrounds)
            {
                if (fundos != null)
                    spriteBatch.Draw(fundos.fundos, fundos.posicao, Color.White);

            }
            #endregion
            #region "PLATAFORMAS"
            foreach (Plataforma plataformas in platforms)
            {
                if (plataformas != null)
                    spriteBatch.Draw(plataformas.textura_plataforma, plataformas.posicao, Color.White);
            }
            #endregion
            #region "MUMIA"
            foreach (Mumia mumia_t in mumia)
            {
                if (mumia_t != null)
                {
                    SpriteEffects efx;
                    if(mumia_t.direccao==true)
                        efx = SpriteEffects.FlipHorizontally;
                    else
                        efx = SpriteEffects.None;

                    mumia_t.rect = new Rectangle(mumia_t.frameActual * 48, 0, 48, 48);

                    spriteBatch.Draw(mumia_t.Textura, mumia_t.posicao, mumia_t.rect, Color.White, 0, new Vector2(0, 0),new Vector2(1,1), efx, 0.0f);
                }
            }
            #endregion
            #region "STRINGS DE CIMA"
            #region "Cor da string das Vidas"
            Color cor_vidas;

            if (Player.Vidas >= 3)
                cor_vidas = Color.Green;

            else if (Player.Vidas == 2)
                cor_vidas = Color.Yellow;

            else
                cor_vidas = Color.Red;
            #endregion
            if (Modo == "Aventura")
            {
                spriteBatch.DrawString(font, "Vidas restantes: " + Player.Vidas.ToString(), new Vector2(0, 0), cor_vidas);
                spriteBatch.DrawString(font, "Score: " + Score.score_jogo, new Vector2(0, 25), Color.Black);
                spriteBatch.DrawString(font, "Nivel: " + (nivel_dificuldade + 1).ToString(), new Vector2(0, 50), Color.Black);

                if (mensagem_de_erro == true)
                    spriteBatch.DrawString(font, "Perdeste uma vida!", new Vector2(300, 290), Color.White,0,new Vector2(0,0),1.2f,SpriteEffects.None,0);
            }
            int tempo = Convert.ToInt32(timer);
            Convert.ToInt32(timer).ToString();

            if(Modo=="Aventura")
            spriteBatch.DrawString(font, "Tempo restante: " + tempo.ToString(), new Vector2(550, 0), Color.DarkMagenta);
            else
            spriteBatch.DrawString(font, "Tempo vivo: " + tempo.ToString(), new Vector2(450, 32), Color.DarkMagenta);

            #region "DIAMANTES"
            foreach (Diamante diam in diamante)
            {
                if (diam != null)
                {
                    spriteBatch.Draw(diam.Textura, diam.posicao, Color.White);
                }
            }
            #endregion
            #endregion
            #region "ARANHA"
            foreach (Aranha _aranha in aranha)
            {
                if (_aranha != null)
                {
                    spriteBatch.Draw(_aranha.Textura, _aranha.posicao, Color.White);
                    #region debug
                    /*
                    //Topo Esquerdo
                    spriteBatch.Draw(Content.Load<Texture2D>("Untitled-2"), new Vector2(_aranha.bbox.Min.X, _aranha.bbox.Min.Y), Color.White);

                    //Direito Inferior
                    spriteBatch.Draw(Content.Load<Texture2D>("Untitled-2"), new Vector2(_aranha.bbox.Max.X, _aranha.bbox.Max.Y), Color.White);

                    //Direito Superior
                    spriteBatch.Draw(Content.Load<Texture2D>("Untitled-2"), new Vector2(_aranha.bbox.Max.X, _aranha.bbox.Min.Y), Color.White);

                    //Inferior Esquerdo
                    spriteBatch.Draw(Content.Load<Texture2D>("Untitled-2"), new Vector2(_aranha.bbox.Min.X, _aranha.bbox.Max.Y), Color.White);
            */
                    #endregion
                }
            }
            #endregion
            #region "ARMADILHAS"
            foreach (Armadilhas _armadilhas in armadilhas)
            {
                if (_armadilhas != null)
                {
                    spriteBatch.Draw(_armadilhas.Textura, _armadilhas.posicao, Color.White);
                }
            }
            #endregion
            #region "SAIDA"
            if (saida!=null)
            spriteBatch.Draw(saida.Textura, saida.posicao, Color.White);
            #endregion
            
            #region "JOGADOR"
#region Debug
            /*
            //Topo Esquerdo
            spriteBatch.Draw(Content.Load<Texture2D>("Untitled-2"), new Vector2(Player.bounding_box_player.Min.X, Player.bounding_box_player.Min.Y), Color.White);

            //Direito Inferior
            spriteBatch.Draw(Content.Load<Texture2D>("Untitled-2"), new Vector2(Player.bounding_box_player.Max.X, Player.bounding_box_player.Max.Y), Color.White);

            //Direito Superior
            spriteBatch.Draw(Content.Load<Texture2D>("Untitled-2"), new Vector2(Player.bounding_box_player.Max.X, Player.bounding_box_player.Min.Y), Color.White);

            //Inferior Esquerdo
            spriteBatch.Draw(Content.Load<Texture2D>("Untitled-2"), new Vector2(Player.bounding_box_player.Min.X, Player.bounding_box_player.Max.Y), Color.White);
            /*
            MouseState mouse = Mouse.GetState();
            spriteBatch.Draw(Content.Load<Texture2D>("Diamantes\\diamante_bonus"), new Vector2(mouse.X, mouse.Y), Color.White);
            spriteBatch.DrawString(font, "X:" + mouse.X.ToString() + " Y:" + mouse.Y.ToString(), new Vector2(0, 450), Color.White);
            */
            #endregion
            if (Player.estado == Player.Estado.Normal)
                spriteBatch.Draw(Player.Textura, Player.posicao, Color.White);

            else if (Player.estado == Player.Estado.Saltar)
            {
                Player.rect = new Rectangle(Player.frameActual * Player.Textura.Width, 0, Player.Textura.Width,Player.Textura.Height);
                if (tapete == true)
                {
                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right) || (DirectInputGamepad.Gamepads[0].Buttons.List[3] == ButtonState.Pressed)))
                        spriteBatch.Draw(Player.Textura_saltar, Player.posicao, Player.rect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0);

                    else
                        spriteBatch.Draw(Player.Textura_saltar, Player.posicao, Player.rect, Color.White);
                }
                else
                {
                    if ((Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right) ))
                        spriteBatch.Draw(Player.Textura_saltar, Player.posicao, Player.rect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0);

                    else
                        spriteBatch.Draw(Player.Textura_saltar, Player.posicao, Player.rect, Color.White);
                }
            }

            else if (Player.estado == Player.Estado.CorrerDir)
            {
                Player.rect = new Rectangle(Player.frameActual * Player.Textura.Width, 0, Player.Textura.Width, Player.Textura.Height);

                spriteBatch.Draw(Player.Textura_run, Player.posicao, Player.rect, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0);
            }

            else if (Player.estado == Player.Estado.CorrerEsq)
            {
                Player.rect = new Rectangle(Player.frameActual * Player.Textura.Width, 0, Player.Textura.Width, Player.Textura.Height);

                spriteBatch.Draw(Player.Textura_run, Player.posicao, Player.rect, Color.White);
            }
            

            Player.estado = Player.Estado.Normal;
            #endregion
            #region "PAUSE"
            if (isPaused == true)
            {
                spriteBatch.Draw(pausa, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "PAUSA", new Vector2(360, 260), Color.White);
                spriteBatch.DrawString(font, "Pressione M/[] para voltar para o menu principal.", new Vector2(0, 530-20), Color.White);
                spriteBatch.DrawString(font, "Pressione S/[/\\] para sair do jogo.", new Vector2(0, 550 - 20), Color.White);
                spriteBatch.DrawString(font, "Pressione F/[X] para voltar para Fullscreen.", new Vector2(0, 550), Color.White);
                if (isMuted == true)
                    spriteBatch.DrawString(font, "A musica esta desligada! Carregue no V/[O] para a ligar.", new Vector2(200, 0), Color.Red);
                else
                    spriteBatch.DrawString(font, "A musica esta ligada! Carregue no V[O] para a desligar.", new Vector2(200, 0), Color.LightGreen);
            }
            #endregion
        }
    }
}
