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

namespace Medeval_Fight
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //That basic stuff
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameState Game_State = GameState.Splash_Screen;
        MouseState Mouse_State;
        MouseState Last_Click_Mouse;
        KeyboardState Push_Keyboard_State;
        SpriteFont Main_Font;
        //Rectangles 
        Rectangle Menu_Screen_Rec,Exit_Button_Rec, Start_Button_Rec, Info_Button_Rec, House_2_Rec, Grass_Tile_Rec, House_Tile_Rec, Road_Tile_Rec , Player_Current_Character_Rec, Player_Sage_Pick_Rec, Player_Lance_Pick_Rec, Player_Axe_Man_Rec
            , Enemy_Rec_1, Enemy_Rec_2, Enemy_Rec_3, Enemy_Rec_4, Enemy_Rec_5, Enemy_Rec_6, Enemy_Rec_7, Enemy_Rec_8, Enemy_Rec_9, Enemy_Rec_10, Enemy_Rec_11, Enemy_Rec_12, Enemy_Rec_13, Enemy_Rec_14, Enemy_Rec_15, Enemy_Rec_16, Enemy_Rec_17, Enemy_Rec_18, Enemy_Rec_19, Enemy_Rec_20
            , Enemy_Rec_21, Enemy_Rec_22, Enemy_Rec_23, Enemy_Rec_24, Enemy_Rec_25, Enemy_Rec_26, Enemy_Rec_27, Enemy_Rec_28, Enemy_Rec_29, Enemy_Rec_30, Health_1_Rec, Health_2_Rec, Health_3_Rec, Health_4_Rec, Health_5_Rec, Magic_Attack_Rec, Lance_Attack_Rec, Axe_Attack_Rec
            , Village_Health_Rec;
        //Textures
        Texture2D Menu_Screen_Tex, Exit_Button_Tex, Start_Button_Tex, Info_Button_Tex, House_2_Tex, Grass_Tile_Tex, House_Tile_Tex, Road_Tile_Tex, Enemy_Tex_1, Enemy_Tex_2, Enemy_Tex_3, Player_Character_Current_Tex,
            Player_Character_Sage_Front_Tex, Player_Character_Sage_Back_Tex, Player_Character_Sage_Right_Tex, Player_Character_Sage_Left_Tex, Player_Character_Lance_Front_Right_Tex, Player_Character_Lance_Back_Left_Tex, Player_Axe_Man_Front_Right_Tex
            ,Player_Axe_Man_Back_Left_Tex, Health_Tex, Magic_Attack_Tex, Lance_Attack_Tex, Axe_Attack_Tex;
        //Integers cuhhhh
        int Splash_Screen_Timer = 0, BG_Grid_Col, BG_Grid_Row,Total_Timer_Seconds, Total_Timer_Minutes = 0, Tick_Counter, Enemy_Count_1, Enemy_Count_2, Enemy_Count_3, Enemy_Kill_Total, Enemy_List_Number, Enemy_Random_Generator
            ,Enemy_Damage_Counter, Attack_Time_Counter ,Sage_Health = 50, Lance_Health = 65, Axe_Health = 70, Village_Health = 100, Village_Timer;
        //booleans
        bool Splash_Load_1, Splash_Load_2, Splash_Load_3, Splash_Load_4, Splash_Load_5, Level_1 = true, Level_2, Level_3, Level_4, Level_5, Controls_Menu, Character_Pick, Sage_Settings, Sage_Attack, Lance_Settings, Lance_Attack, Axe_Settings, Axe_Attack
            ,Enemy_Damage_1, Enemy_Damage_2, Enemy_Damage_3, Enemy_Damage_4, Enemy_Damage_5, Enemy_Damage_6, Enemy_Damage_7, Enemy_Damage_8, Enemy_Damage_9, Enemy_Damage_10, Died, Village_Pillage;
        //Grids 
        Rectangle[,] BG_Grid = new Rectangle[9, 9];
        //Dem random number generators
        Random Enemy_Attack_Generator = new Random();
        //lists
        List<Rectangle> Enemy_List;
        List<Rectangle> Enemy_List_2;
        List<Rectangle> Enemy_List_3;
        List<int> Current_Attacks;
        //sounds
        SoundEffect Lance_Axe_Attack_Sound, Sage_Attack_Sound, Level_Up_Music;
        Song Start_Up_Music;
        //arrays
        string[] Levels = new string[5] { "Level 1", "Level 2", "Level 3", "Level 4" , "Level 5"};
        string[] Loading = new string[5] {"Loading." , "Loading.." , "Loading..." , "Loading...." , "Loading....."};
        //Gamestates
        enum GameState
        {
            Splash_Screen, Menu_Screen, Game_Play_Screen, Exit_Screen
        }
        //floats
        float Volume = 0.5f, Pitch = 0, Pan = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 540;
            graphics.PreferredBackBufferWidth = 540;
        }
        protected override void Initialize()
        {
            IsMouseVisible = true;
            Menu_Screen_Rec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Exit_Button_Rec = new Rectangle(0, 430, 150, 50);
            Start_Button_Rec = new Rectangle(200, 430, 150, 50);
            Info_Button_Rec = new Rectangle(400, 430, 150, 50);
            Grass_Tile_Rec = new Rectangle(0, 0, 60, 60);
            House_Tile_Rec = new Rectangle(0, 0, 60, 60);
            House_2_Rec = new Rectangle(0, 0, 60, 60);
            Road_Tile_Rec = new Rectangle(0, 0, 60, 60);
            Player_Current_Character_Rec = new Rectangle(60, 60, 40, 40);
            Player_Sage_Pick_Rec = new Rectangle(50, 300, 60, 60);
            Player_Lance_Pick_Rec = new Rectangle(225, 300, 60, 60);
            Player_Axe_Man_Rec = new Rectangle(400, 300, 60, 60);
            Enemy_Rec_1 = new Rectangle(300, 300, 40, 40);
            Enemy_Rec_2 = new Rectangle(340, 400, 40, 40);
            Enemy_Rec_3 = new Rectangle(280, 350, 40, 40);
            Enemy_Rec_4 = new Rectangle(450, 400, 40, 40);
            Enemy_Rec_5 = new Rectangle(365, 350, 40, 40);
            Enemy_Rec_6 = new Rectangle(175, 175, 40, 40);
            Enemy_Rec_7 = new Rectangle(200, 200, 40, 40);
            Enemy_Rec_8 = new Rectangle(410, 300, 40, 40);
            Enemy_Rec_9 = new Rectangle(330, 300, 40, 40);
            Enemy_Rec_10 = new Rectangle(190, 300, 40, 40);
            Enemy_Rec_11 = new Rectangle(180, 250, 40, 40);
            Enemy_Rec_12 = new Rectangle(340, 200, 40, 40);
            Enemy_Rec_13 = new Rectangle(220, 150, 40, 40);
            Enemy_Rec_14 = new Rectangle(200, 400, 40, 40);
            Enemy_Rec_15 = new Rectangle(200, 400, 40, 40);
            Enemy_Rec_16 = new Rectangle(200, 400, 40, 40);
            Enemy_Rec_17 = new Rectangle(230, 400, 40, 40);
            Enemy_Rec_18 = new Rectangle(210, 400, 40, 40);
            Enemy_Rec_19 = new Rectangle(260, 400, 40, 40);
            Enemy_Rec_20 = new Rectangle(50, 50, 40, 40);
            Enemy_Rec_21 = new Rectangle(75, 75, 40, 40);
            Enemy_Rec_22 = new Rectangle(100, 100, 40, 40);
            Enemy_Rec_23 = new Rectangle(100, 200, 40, 40); 
            Enemy_Rec_24 = new Rectangle(150, 350, 40, 40);
            Enemy_Rec_25 = new Rectangle(200, 400, 40, 40);
            Enemy_Rec_26 = new Rectangle(270, 200, 40, 40);
            Enemy_Rec_27 = new Rectangle(250, 350, 40, 40);
            Enemy_Rec_28 = new Rectangle(220, 400, 40, 40);
            Enemy_Rec_29 = new Rectangle(150, 150, 40, 40);
            Enemy_Rec_30 = new Rectangle(270, 350, 40, 40);
            Health_1_Rec = new Rectangle(285, 5, 20, 20);
            Health_2_Rec = new Rectangle(310, 5, 20, 20);
            Health_3_Rec = new Rectangle(335, 5, 20, 20);
            Health_4_Rec = new Rectangle(360, 5, 20, 20);
            Health_5_Rec = new Rectangle(385, 5, 20, 20);
            Village_Health_Rec = new Rectangle(0, 0, 200, 230);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Main_Font = Content.Load<SpriteFont>("SpriteFont1");
            Menu_Screen_Tex = Content.Load<Texture2D>("Menu");
            Exit_Button_Tex = Content.Load<Texture2D>("exit");
            Start_Button_Tex = Content.Load<Texture2D>("start");
            Info_Button_Tex = Content.Load<Texture2D>("info");
            Grass_Tile_Tex = Content.Load<Texture2D>("grass");
            Health_Tex = Content.Load<Texture2D>("heart");
            House_Tile_Tex = Content.Load<Texture2D>("house");
            House_2_Tex = Content.Load<Texture2D>("house2");
            Road_Tile_Tex = Content.Load<Texture2D>("road");
            Enemy_Tex_1 = Content.Load<Texture2D>("Enemy");
            Enemy_Tex_2 = Content.Load<Texture2D>("zombie");
            Enemy_Tex_3 = Content.Load<Texture2D>("spider");
            Magic_Attack_Tex = Content.Load <Texture2D>("Magic_Attack");
            Axe_Attack_Tex = Content.Load<Texture2D>("Axe_Attack");
            Lance_Attack_Tex = Content.Load<Texture2D>("Lance_Attack");
            Player_Character_Current_Tex = Content.Load<Texture2D>("Player_Sage_Front");
            Player_Character_Sage_Back_Tex = Content.Load<Texture2D>("Player_Sage_Back");
            Player_Character_Sage_Front_Tex = Content.Load<Texture2D>("Player_Sage_Front");
            Player_Character_Sage_Left_Tex = Content.Load<Texture2D>("Player_Sage_Left");
            Player_Character_Sage_Right_Tex = Content.Load<Texture2D>("Player_Sage_Right");
            Player_Character_Lance_Front_Right_Tex = Content.Load<Texture2D>("Player_Lance_Front_Right");
            Player_Character_Lance_Back_Left_Tex = Content.Load<Texture2D>("Player_Lance_Back_Left");
            Player_Axe_Man_Front_Right_Tex = Content.Load<Texture2D>("Player_Axe_Man_Right");
            Player_Axe_Man_Back_Left_Tex = Content.Load<Texture2D>("Player_Axe_Man_Front");
            Sage_Attack_Sound = Content.Load<SoundEffect>("Mage_Attack_Sound");
            Lance_Axe_Attack_Sound = Content.Load<SoundEffect>("Sword_Lance_Attack_Sound");
            Level_Up_Music = Content.Load<SoundEffect>("Level_Up");
            Start_Up_Music = Content.Load<Song>("Start_Up_Song");
            Enemy_List = new List<Rectangle>();
            Enemy_List_2 = new List<Rectangle>();
            Enemy_List_3 = new List<Rectangle>();
            Current_Attacks = new List<int>();
            Current_Attacks.Add(1);
            Enemy_List.Add(Enemy_Rec_1);
            Enemy_List.Add(Enemy_Rec_2);
            Enemy_List.Add(Enemy_Rec_3);
            Enemy_List.Add(Enemy_Rec_4);
            Enemy_List.Add(Enemy_Rec_5);
            Enemy_List.Add(Enemy_Rec_6);
            Enemy_List.Add(Enemy_Rec_7);
            Enemy_List.Add(Enemy_Rec_8);
            Enemy_List.Add(Enemy_Rec_9);
            Enemy_List.Add(Enemy_Rec_10);
            Enemy_List.Add(Enemy_Rec_11);
            Enemy_List_2.Add(Enemy_Rec_12);
            Enemy_List_2.Add(Enemy_Rec_13);
            Enemy_List_2.Add(Enemy_Rec_14);
            Enemy_List_2.Add(Enemy_Rec_15);
            Enemy_List_2.Add(Enemy_Rec_16);
            Enemy_List_2.Add(Enemy_Rec_17);
            Enemy_List_2.Add(Enemy_Rec_18);
            Enemy_List_2.Add(Enemy_Rec_19);
            Enemy_List_2.Add(Enemy_Rec_20);
            Enemy_List_3.Add(Enemy_Rec_21);
            Enemy_List_3.Add(Enemy_Rec_22);
            Enemy_List_3.Add(Enemy_Rec_23);
            Enemy_List_3.Add(Enemy_Rec_24);
            Enemy_List_3.Add(Enemy_Rec_25);
            Enemy_List_3.Add(Enemy_Rec_26);
            Enemy_List_3.Add(Enemy_Rec_27);
            Enemy_List_3.Add(Enemy_Rec_28);
            Enemy_List_3.Add(Enemy_Rec_29);
            Enemy_List_3.Add(Enemy_Rec_30);
            for (int i = 0; i < BG_Grid.GetLength(0); i++)
            {
                for (int j = 0; j < BG_Grid.GetLength(1); j++)
                {
                    BG_Grid[i, j] = new Rectangle(BG_Grid_Col, BG_Grid_Row, GraphicsDevice.Viewport.Width / 9, GraphicsDevice.Viewport.Height / 9);
                    BG_Grid_Col += GraphicsDevice.Viewport.Width / 9;
                }
                BG_Grid_Row += GraphicsDevice.Viewport.Height / 9;
                BG_Grid_Col = 0;
            }
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            switch (Game_State)
            {
                case GameState.Splash_Screen:
                    Splash_Screen_Update_State();
                    break;
                case GameState.Menu_Screen:
                    Menu_Screen_Update_State();
                    break;
                case GameState.Game_Play_Screen:
                    Game_Play_Update_State();
                    break;
                case GameState.Exit_Screen:
                    Exit_Screen_Update_State();
                    break;
            }
            base.Update(gameTime);
        }
        public void Splash_Screen_Update_State()
        {
            MediaPlayer.Play(Start_Up_Music);
            //need to polish up this at some point
            if (Splash_Screen_Timer < 300)
            {
                if (Splash_Screen_Timer <= 60)
                {
                    Splash_Load_1 = true;
                }
                if (Splash_Screen_Timer == 120)
                {
                    Splash_Load_2 = true;
                }
                if (Splash_Screen_Timer == 180)
                {
                    Splash_Load_3 = true;
                }
                if (Splash_Screen_Timer == 240)
                {
                    Splash_Load_4 = true;
                }
                if (Splash_Screen_Timer == 300)
                {
                    Splash_Load_5 = true;
                }
            }
            if (Splash_Screen_Timer > 300)
            {
                Game_State = GameState.Menu_Screen;
            }
            Splash_Screen_Timer++;
        }
        public void Menu_Screen_Update_State()
        {
            Mouse_State = Mouse.GetState();
            //change font, add title, and hoverovers
            if (Exit_Button_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed)
            {
                Exit();
            }
            if (Start_Button_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed)
            {
                Game_State = GameState.Game_Play_Screen;
            }
            if (Info_Button_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed)
            {
                Controls_Menu = true;
            }
            Last_Click_Mouse = Mouse_State;
        }
        public void Game_Play_Update_State()
        {
            Mouse_State = Mouse.GetState();
            KeyboardState Keyboard_State = Keyboard.GetState();
            //Character choose code yuh boi
            if (Character_Pick == false)
            {
                if (Player_Sage_Pick_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed)
                {
                    Player_Character_Current_Tex = Player_Character_Sage_Front_Tex;
                    Character_Pick = true;
                    Sage_Settings = true;
                }
                if (Player_Lance_Pick_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed)
                {
                    Player_Character_Current_Tex = Player_Character_Lance_Front_Right_Tex;
                    Character_Pick = true;
                    Lance_Settings = true;
                }
                if (Player_Axe_Man_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed)
                {
                    Player_Character_Current_Tex = Player_Axe_Man_Front_Right_Tex;
                    Character_Pick = true;
                    Axe_Settings = true;
                }
            }
            if (Character_Pick == true)
            {
                MediaPlayer.Stop();
            }
            //player movement
            if (Push_Keyboard_State.IsKeyUp(Keys.W) && Keyboard_State.IsKeyDown(Keys.W))
            {
                if (Sage_Settings == true)
                {
                    Player_Character_Current_Tex = Player_Character_Sage_Back_Tex;
                }
                if (Lance_Settings == true)
                {
                    Player_Character_Current_Tex = Player_Character_Lance_Front_Right_Tex;
                }
                Player_Current_Character_Rec.Y -= 20;
            }
            if (Push_Keyboard_State.IsKeyUp(Keys.S) && Keyboard_State.IsKeyDown(Keys.S))
            {
                if (Sage_Settings == true)
                {
                    Player_Character_Current_Tex = Player_Character_Sage_Front_Tex;
                }
                if (Lance_Settings == true)
                {
                    Player_Character_Current_Tex = Player_Character_Lance_Back_Left_Tex;
                }
                Player_Current_Character_Rec.Y += 20;
            }
            if (Push_Keyboard_State.IsKeyUp(Keys.A) && Keyboard_State.IsKeyDown(Keys.A))
            {
                if (Sage_Settings == true)
                {
                    Player_Character_Current_Tex = Player_Character_Sage_Left_Tex;
                }
                if (Lance_Settings == true)
                {
                    Player_Character_Current_Tex = Player_Character_Lance_Back_Left_Tex;
                }
                Player_Current_Character_Rec.X -= 20;
            }
            if (Push_Keyboard_State.IsKeyUp(Keys.D) && Keyboard_State.IsKeyDown(Keys.D))
            {
                if (Sage_Settings == true)
                {
                    Player_Character_Current_Tex = Player_Character_Sage_Right_Tex;
                }
                if (Lance_Settings == true)
                {
                    Player_Character_Current_Tex = Player_Character_Lance_Front_Right_Tex;
                }
                Player_Current_Character_Rec.X += 20;
            }
            //attack code sage 
            for (int i = 0; i < Enemy_List.Count; i++)
            {
                if (Sage_Settings == true)
                {
                    if (Enemy_List[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Sage_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List[i].X - Player_Current_Character_Rec.X < 200 && Enemy_List[i].Y - Player_Current_Character_Rec.Y < 200)
                        {
                            Enemy_Count_1++;
                            Sage_Attack = true;
                            if (Enemy_Count_1 >= 3)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_1 = 0;
                                Enemy_List.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Enemy_List_2.Count; i++)
            {
                if (Sage_Settings == true)
                {
                    if (Enemy_List_2[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Sage_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List_2[i].X - Player_Current_Character_Rec.X < 200 && Enemy_List_2[i].Y - Player_Current_Character_Rec.Y < 200)
                        {
                            Enemy_Count_2++;
                            Sage_Attack = true;
                            if (Enemy_Count_2 >= 3)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_2 = 0;
                                Enemy_List_2.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Enemy_List_3.Count; i++)
            {
                if (Sage_Settings == true)
                {
                    if (Enemy_List_3[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Sage_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List_3[i].X - Player_Current_Character_Rec.X < 200 && Enemy_List_3[i].Y - Player_Current_Character_Rec.Y < 200)
                        {
                            Enemy_Count_3++;
                            Sage_Attack = true;
                            if (Enemy_Count_3 >= 3)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_3 = 0;
                                Enemy_List_3.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            //attack code for lance
            for (int i = 0; i < Enemy_List.Count; i++)
            {
                if (Lance_Settings == true)
                {
                    if (Enemy_List[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Lance_Axe_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List[i].X - Player_Current_Character_Rec.X < 200 && Enemy_List[i].Y - Player_Current_Character_Rec.Y < 50)
                        {
                            Enemy_Count_1++;
                            Sage_Attack = true;
                            if (Enemy_Count_1 >= 4)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_1 = 0;
                                Enemy_List.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Enemy_List_2.Count; i++)
            {
                if (Lance_Settings == true)
                {
                    if (Enemy_List_2[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Lance_Axe_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List_2[i].X - Player_Current_Character_Rec.X < 50 && Enemy_List_2[i].Y - Player_Current_Character_Rec.Y < 50)
                        {
                            Enemy_Count_2++;
                            Sage_Attack = true;
                            if (Enemy_Count_2 >= 4)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_2 = 0;
                                Enemy_List_2.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Enemy_List_3.Count; i++)
            {
                if (Lance_Settings == true)
                {
                    if (Enemy_List_3[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Lance_Axe_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List_3[i].X - Player_Current_Character_Rec.X < 50 && Enemy_List_3[i].Y - Player_Current_Character_Rec.Y < 50)
                        {
                            Enemy_Count_3++;
                            Sage_Attack = true;
                            if (Enemy_Count_3 >= 4)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_3 = 0;
                                Enemy_List_3.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            //axe settings
            for (int i = 0; i < Enemy_List.Count; i++)
            {
                if (Axe_Settings == true)
                {
                    if (Enemy_List[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Lance_Axe_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List[i].X - Player_Current_Character_Rec.X < 75 && Enemy_List[i].Y - Player_Current_Character_Rec.Y < 75)
                        {
                            Enemy_Count_1++;
                            Axe_Attack = true;
                            if (Enemy_Count_1 >= 2)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_1 = 0;
                                Enemy_List.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Enemy_List_2.Count; i++)
            {
                if (Axe_Settings == true)
                {
                    if (Enemy_List_2[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Lance_Axe_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List_2[i].X - Player_Current_Character_Rec.X < 75 && Enemy_List_2[i].Y - Player_Current_Character_Rec.Y < 75)
                        {
                            Enemy_Count_2++;
                            Axe_Attack = true;
                            if (Enemy_Count_2 >= 2)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_2 = 0;
                                Enemy_List_2.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Enemy_List_3.Count; i++)
            {
                if (Axe_Settings == true)
                {
                    if (Enemy_List_3[i].Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
                    {
                        Lance_Axe_Attack_Sound.Play(Volume, Pitch, Pan);
                        if (Enemy_List_3[i].X - Player_Current_Character_Rec.X < 75 && Enemy_List_3[i].Y - Player_Current_Character_Rec.Y < 75)
                        {
                            Enemy_Count_3++;
                            Axe_Attack = true;
                            if (Enemy_Count_3 >= 2)
                            {
                                Enemy_List_Number = i;
                                Enemy_Kill_Total++;
                                Enemy_Count_3 = 0;
                                Enemy_List_3.RemoveAt(Enemy_List_Number);
                            }
                        }
                    }
                }
            }
            //timer
            Tick_Counter++;
            if (Tick_Counter == 60)
            {
                Total_Timer_Seconds++;
                Tick_Counter = 0;
                if (Total_Timer_Seconds == 60)
                {
                    Total_Timer_Minutes++;
                    Total_Timer_Seconds = 0;
                }
            }
            //level code
            if (Enemy_Kill_Total > 10 && Enemy_Kill_Total <= 24)
            {
                Level_1 = false;
                Level_2 = true;
            }
            if (Enemy_Kill_Total >= 25 && Enemy_Kill_Total <= 49)
            {
                Level_1 = false;
                Level_2 = false;
                Level_3 = true;
            }
            if (Enemy_Kill_Total >= 50 && Enemy_Kill_Total <= 74)
            {
                Level_1 = false;
                Level_2 = false;
                Level_3 = false;
                Level_4 = true;
            }
            if (Enemy_Kill_Total >= 75 && Enemy_Kill_Total <= 99)
            {
                Level_1 = false;
                Level_2 = false;
                Level_3 = false;
                Level_4 = false;
                Level_5 = true;
            }
            if (Enemy_Kill_Total >= 100)
            {
                Level_1 = false;
                Level_2 = false;
                Level_3 = false;
                Level_4 = false;
                Level_5 = false;
                Game_State = GameState.Exit_Screen;
            }
            //enemy attack code
            if (Level_2 == true)
            {
                if (Enemy_Damage_Counter > 250)
                {
                    Enemy_Random_Generator = Enemy_Attack_Generator.Next(0, 3);
                    if (Enemy_Random_Generator == 1)
                    {
                        Enemy_Damage_1 = true;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health--;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health--;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health--;
                        }
                    }
                    if (Enemy_Random_Generator == 2)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = true;
                        Enemy_Damage_3 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 2;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 2;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 2;
                        }
                    }
                    if (Enemy_Random_Generator == 3)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 3;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 3;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 3;
                        }
                    }
                    Enemy_Damage_Counter = 0;
                }
            }
            if (Level_3 == true)
            {
                if (Enemy_Damage_Counter == 230)
                {
                    Enemy_Random_Generator = Enemy_Attack_Generator.Next(0, 6);
                    if (Enemy_Random_Generator == 1)
                    {
                        Enemy_Damage_1 = true;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health--;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health--;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health--;
                        }
                    }
                    if (Enemy_Random_Generator == 2)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = true;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 2;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 2;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 2;
                        }
                    }
                    if (Enemy_Random_Generator == 3)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = true;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 3;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 3;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 3;
                        }
                    }
                    if (Enemy_Random_Generator == 4)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = true;
                        Enemy_Damage_5 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 4;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 4;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 4;
                        }
                    }
                    if (Enemy_Random_Generator == 5)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 5;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 5;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 5;
                        }
                    }
                    Enemy_Damage_Counter = 0;
                }
            }
            if (Level_4 == true)
            {
                if (Enemy_Damage_Counter == 220)
                {
                    Enemy_Random_Generator = Enemy_Attack_Generator.Next(0, 8);
                    if (Enemy_Random_Generator == 1)
                    {
                        Enemy_Damage_1 = true;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health --;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health --;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health --;
                        }
                    }
                    if (Enemy_Random_Generator == 2)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = true;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 2;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 2;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 2;
                        }
                    }
                    if (Enemy_Random_Generator == 3)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = true;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 3;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 3;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 3;
                        }
                    }
                    if (Enemy_Random_Generator == 4)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = true;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 4;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 4;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 4;
                        }
                    }
                    if (Enemy_Random_Generator == 5)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = true;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 5;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 5;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 5;
                        }
                    }
                    if (Enemy_Random_Generator == 6)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = true;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 6;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 6;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 6;
                        }
                    }
                    if (Enemy_Random_Generator == 7)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = true;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 7;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 7;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 7;
                        }
                    }
                    if (Enemy_Random_Generator == 8)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = true;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 8;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 8;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 8;
                        }
                    }
                    Enemy_Damage_Counter = 0;
                }
            }
            if (Level_5 == true)
            {
                if (Enemy_Damage_Counter == 210)
                {
                    Enemy_Random_Generator = Enemy_Attack_Generator.Next(0, 11);
                    if (Enemy_Random_Generator == 1)
                    {
                        Enemy_Damage_1 = true;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = false;
                        Enemy_Damage_5 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health --;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health --;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health --;
                        }
                    }
                    if (Enemy_Random_Generator == 2)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = true;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 2;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -=2;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -=2;
                        }
                    }
                    if (Enemy_Random_Generator == 3)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = true;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 3;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 3;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 3;
                        }
                    }
                    if (Enemy_Random_Generator == 4)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = true;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 4;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 4;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 4;
                        }
                    }
                    if (Enemy_Random_Generator == 5)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = true;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 5;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 5;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 5;
                        }
                    }
                    if (Enemy_Random_Generator == 6)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = true;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 6;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 6;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 6;
                        }
                    }
                    if (Enemy_Random_Generator == 7)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = true;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 7;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 7;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 7;
                        }
                    }
                    if (Enemy_Random_Generator == 8)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = true;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 8;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 8;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 8;
                        }
                    }
                    if (Enemy_Random_Generator == 9)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = true;
                        Enemy_Damage_10 = false;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 9;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 9;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 9;
                        }
                    }
                    if (Enemy_Random_Generator == 10)
                    {
                        Enemy_Damage_1 = false;
                        Enemy_Damage_2 = false;
                        Enemy_Damage_3 = false;
                        Enemy_Damage_4 = false;
                        Enemy_Damage_5 = false;
                        Enemy_Damage_6 = false;
                        Enemy_Damage_7 = false;
                        Enemy_Damage_8 = false;
                        Enemy_Damage_9 = false;
                        Enemy_Damage_10 = true;
                        if (Sage_Settings == true)
                        {
                            Sage_Health -= 10;
                        }
                        if (Lance_Settings == true)
                        {
                            Lance_Health -= 10;
                        }
                        if (Axe_Settings == true)
                        {
                            Axe_Health -= 10;
                        }
                    }
                    Enemy_Damage_Counter = 0;
                }
            }
            if (Character_Pick == true)
            {
                for (int i = 0; Enemy_List.Count > i; i++)
                {
                    if (Level_1 == true)
                    {
                        if (Enemy_List[i].Intersects(Village_Health_Rec))
                        {
                            if (Village_Timer >= 180)
                            {
                                Village_Health -= 10;
                                Village_Timer = 0;
                            }
                        }
                    }
                }
                for (int i = 0; Enemy_List.Count > i; i++)
                {
                    for (int j = 0; Enemy_List_2.Count > j; j++)
                    {
                        if (Level_2 == true)
                        {
                            if (Enemy_List[i].Intersects(Village_Health_Rec))
                            {
                                if (Village_Timer >= 180)
                                {
                                    Village_Health -= 10;
                                    Village_Timer = 0;
                                }
                            }
                            if (Enemy_List_2[j].Intersects(Village_Health_Rec))
                            {
                                if (Village_Timer >= 180)
                                {
                                    Village_Health -= 10;
                                    Village_Timer = 0;
                                }
                            }
                        }
                    }
                }
                for (int i = 0; Enemy_List.Count > i; i++)
                {
                    for (int j = 0; Enemy_List_2.Count > j; j++)
                    {
                        for (int b = 0; Enemy_List_3.Count > b; b++)
                        {
                            if (Level_3 == true || Level_4 == true || Level_5 == true)
                            {
                                if (Enemy_List[i].Intersects(Village_Health_Rec))
                                {
                                    if (Village_Timer >= 180)
                                    {
                                        Village_Health -= 10;
                                        Village_Timer = 0;
                                    }
                                }
                                if (Enemy_List_2[j].Intersects(Village_Health_Rec))
                                {
                                    if (Village_Timer >= 180)
                                    {
                                        Village_Health -= 10;
                                        Village_Timer = 0;
                                    }
                                }
                                if (Enemy_List_3[b].Intersects(Village_Health_Rec))
                                {
                                    if (Village_Timer >= 180)
                                    {
                                        Village_Health -= 10;
                                        Village_Timer = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Village_Health <= 0)
            {
                Village_Pillage = true;
            }
            Village_Timer++;
            Attack_Time_Counter++;
            Enemy_Damage_Counter++;
            Last_Click_Mouse = Mouse_State;
            Push_Keyboard_State = Keyboard_State;
        }
        public void Exit_Screen_Update_State()
        {
            Mouse_State = Mouse.GetState();
            if (Exit_Button_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed && Last_Click_Mouse.LeftButton == ButtonState.Released)
            {
                Exit();
            }
            Last_Click_Mouse = Mouse_State;
        }
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (Game_State)
            {
                case GameState.Splash_Screen:
                    Splash_Screen_Draw_State();
                    break;
                case GameState.Menu_Screen:
                    Menu_Screen_Draw_State();
                    break;
                case GameState.Game_Play_Screen:
                    Game_Play_Draw_State();
                    break;
                case GameState.Exit_Screen:
                    Exit_Screen_Draw_State();
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void Splash_Screen_Draw_State()
        {
            spriteBatch.Draw(Menu_Screen_Tex , Menu_Screen_Rec , Color.White);
            if (Splash_Load_1 == false || Splash_Load_2 == false || Splash_Load_3 == false || Splash_Load_4 == false || Splash_Load_5 == false)
            {
                //put dat shiza in a list
                if (Splash_Load_1 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[1], new Vector2(200, 400), Color.Brown);
                }
                if (Splash_Load_2 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[2], new Vector2(200, 400), Color.Brown);
                }
                if (Splash_Load_3 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[3], new Vector2(200, 400), Color.Brown);
                }
                if (Splash_Load_4 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[4], new Vector2(200, 400), Color.Brown);
                }
                if (Splash_Load_5 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[5], new Vector2(215, 400), Color.Brown);
                }
                spriteBatch.DrawString(Main_Font, "Medieval Fight!", new Vector2(160, 250), Color.Brown);
            }
        }
        public void Menu_Screen_Draw_State()
        {
            //list here to
            spriteBatch.Draw(Menu_Screen_Tex, Menu_Screen_Rec, Color.White);
            spriteBatch.Draw(Exit_Button_Tex, Exit_Button_Rec, Color.White);
            spriteBatch.Draw(Start_Button_Tex, Start_Button_Rec, Color.White);
            spriteBatch.Draw(Info_Button_Tex, Info_Button_Rec, Color.White);
            if (Controls_Menu == true)
            {
                spriteBatch.DrawString(Main_Font, "WASD to move", new Vector2 (100,300) ,Color.Brown);
                spriteBatch.DrawString(Main_Font, "Left Click to attack enemy.", new Vector2(100, 350), Color.Brown);    
            }
            spriteBatch.DrawString(Main_Font, "Medieval Fight!", new Vector2(160, 250), Color.Brown);
        }
        public void Game_Play_Draw_State()
        {
            //character picking boi yuhhhhhhhh 
            if (Character_Pick == false)
            {
                spriteBatch.Draw(Menu_Screen_Tex, Menu_Screen_Rec, Color.White);
                spriteBatch.Draw(Player_Character_Sage_Front_Tex, Player_Sage_Pick_Rec, Color.White);
                spriteBatch.DrawString(Main_Font, "Sage", new Vector2(50, 180), Color.Brown);
                spriteBatch.DrawString(Main_Font, "Damage: 33", new Vector2(50, 400), Color.Brown);
                spriteBatch.DrawString(Main_Font, "Health: 50", new Vector2(50, 450), Color.Brown);
                spriteBatch.Draw(Player_Character_Lance_Front_Right_Tex, Player_Lance_Pick_Rec, Color.White);
                spriteBatch.DrawString(Main_Font, "Lance", new Vector2(225, 180), Color.Brown);
                spriteBatch.DrawString(Main_Font, "Damage: 22", new Vector2(215, 400), Color.Brown);
                spriteBatch.DrawString(Main_Font, "Health: 65", new Vector2(215, 450), Color.Brown);
                spriteBatch.Draw(Player_Axe_Man_Front_Right_Tex, Player_Axe_Man_Rec, Color.White);
                spriteBatch.DrawString(Main_Font, "Axe Man", new Vector2(375, 180), Color.Brown);
                spriteBatch.DrawString(Main_Font, "Damage: 20 ", new Vector2(375, 400), Color.Brown);
                spriteBatch.DrawString(Main_Font, "Health: 70", new Vector2(375, 450), Color.Brown);
                
            }
            if (Character_Pick == true)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        spriteBatch.Draw(Grass_Tile_Tex, BG_Grid[i, j], Color.White);
                    }
                }
                spriteBatch.Draw(House_Tile_Tex, BG_Grid[2 , 1], Color.White);
                spriteBatch.Draw(House_Tile_Tex, BG_Grid[3 , 3], Color.White);
                spriteBatch.Draw(House_2_Tex, BG_Grid[1 , 2], Color.White);
                spriteBatch.Draw(House_2_Tex, BG_Grid[3 , 1], Color.White);
                for (int i = 0; i < 9; i++)
                {
                    spriteBatch.Draw(Road_Tile_Tex, BG_Grid[4, i], Color.White);
                    spriteBatch.Draw(Road_Tile_Tex, BG_Grid[i, 4], Color.White);
                }
                spriteBatch.Draw(Player_Character_Current_Tex, Player_Current_Character_Rec, Color.White);
                spriteBatch.DrawString(Main_Font, "Minutes:" + Total_Timer_Minutes.ToString() + " "+ "Seconds:" + Total_Timer_Seconds.ToString(), new Vector2(0, 0), Color.Brown);
                if (Level_1 == true)
                {
                    spriteBatch.DrawString(Main_Font, Levels[0], new Vector2(430, 0), Color.Brown);
                    if (Enemy_List.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_1, Enemy_List[i], Color.White);
                        }
                    }
                }
                if (Level_2 == true)
                {
                    spriteBatch.DrawString(Main_Font, Levels[1], new Vector2(430, 0), Color.Brown);
                    if (Enemy_List.Count < 1)
                    {
                        Enemy_List.Add(Enemy_Rec_1);
                        Enemy_List.Add(Enemy_Rec_2);
                        Enemy_List.Add(Enemy_Rec_3);
                        Enemy_List.Add(Enemy_Rec_4);
                        Enemy_List.Add(Enemy_Rec_5);
                        Enemy_List.Add(Enemy_Rec_6);
                        Enemy_List.Add(Enemy_Rec_7);
                        Enemy_List.Add(Enemy_Rec_8);
                        Enemy_List.Add(Enemy_Rec_9);
                        Enemy_List.Add(Enemy_Rec_10);
                    }
                    if (Enemy_List.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_1, Enemy_List[i], Color.White);
                        }
                    }
                    if (Enemy_List_2.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List_2.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_2, Enemy_List_2[i], Color.White);
                        }
                    }
                }
                if (Level_3 == true)
                {
                    spriteBatch.DrawString(Main_Font, Levels[2], new Vector2(430, 0), Color.Brown);
                    if (Enemy_List.Count < 1)
                    {
                        Enemy_List.Add(Enemy_Rec_1);
                        Enemy_List.Add(Enemy_Rec_2);
                        Enemy_List.Add(Enemy_Rec_3);
                        Enemy_List.Add(Enemy_Rec_4);
                        Enemy_List.Add(Enemy_Rec_5);
                        Enemy_List.Add(Enemy_Rec_6);
                        Enemy_List.Add(Enemy_Rec_7);
                        Enemy_List.Add(Enemy_Rec_8);
                        Enemy_List.Add(Enemy_Rec_9);
                        Enemy_List.Add(Enemy_Rec_10);
                    }
                    if (Enemy_List_2.Count < 1)
                    {
                        Enemy_List_2.Add(Enemy_Rec_12);
                        Enemy_List_2.Add(Enemy_Rec_13);
                        Enemy_List_2.Add(Enemy_Rec_14);
                        Enemy_List_2.Add(Enemy_Rec_15);
                        Enemy_List_2.Add(Enemy_Rec_16);
                        Enemy_List_2.Add(Enemy_Rec_17);
                        Enemy_List_2.Add(Enemy_Rec_18);
                        Enemy_List_2.Add(Enemy_Rec_19);
                        Enemy_List_2.Add(Enemy_Rec_20);
                    }
                    if (Enemy_List.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_1, Enemy_List[i], Color.White);
                        }
                    }
                    if (Enemy_List_2.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List_2.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_2, Enemy_List_2[i], Color.White);
                        }
                    }
                    if (Enemy_List_3.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List_3.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_3, Enemy_List_3[i], Color.White);
                        }
                    }
                }
                if (Level_4 == true)
                {
                    if (Enemy_List.Count < 1)
                    {
                        Enemy_List.Add(Enemy_Rec_1);
                        Enemy_List.Add(Enemy_Rec_2);
                        Enemy_List.Add(Enemy_Rec_3);
                        Enemy_List.Add(Enemy_Rec_4);
                        Enemy_List.Add(Enemy_Rec_5);
                        Enemy_List.Add(Enemy_Rec_6);
                        Enemy_List.Add(Enemy_Rec_7);
                        Enemy_List.Add(Enemy_Rec_8);
                        Enemy_List.Add(Enemy_Rec_9);
                        Enemy_List.Add(Enemy_Rec_10);
                    }
                    if (Enemy_List_2.Count < 1)
                    {
                        Enemy_List_2.Add(Enemy_Rec_12);
                        Enemy_List_2.Add(Enemy_Rec_13);
                        Enemy_List_2.Add(Enemy_Rec_14);
                        Enemy_List_2.Add(Enemy_Rec_15);
                        Enemy_List_2.Add(Enemy_Rec_16);
                        Enemy_List_2.Add(Enemy_Rec_17);
                        Enemy_List_2.Add(Enemy_Rec_18);
                        Enemy_List_2.Add(Enemy_Rec_19);
                        Enemy_List_2.Add(Enemy_Rec_20);
                    }
                    if (Enemy_List_3.Count < 1)
                    {
                        Enemy_List_3.Add(Enemy_Rec_21);
                        Enemy_List_3.Add(Enemy_Rec_22);
                        Enemy_List_3.Add(Enemy_Rec_23);
                        Enemy_List_3.Add(Enemy_Rec_24);
                        Enemy_List_3.Add(Enemy_Rec_25);
                        Enemy_List_3.Add(Enemy_Rec_26);
                        Enemy_List_3.Add(Enemy_Rec_27);
                        Enemy_List_3.Add(Enemy_Rec_28);
                        Enemy_List_3.Add(Enemy_Rec_29);
                        Enemy_List_3.Add(Enemy_Rec_30);
                    }
                    if (Enemy_List.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_1, Enemy_List[i], Color.White);
                        }
                    }
                    if (Enemy_List_2.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List_2.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_2, Enemy_List_2[i], Color.White);
                        }
                    }
                    if (Enemy_List_3.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List_3.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_3, Enemy_List_3[i], Color.White);
                        }
                    }
                    spriteBatch.DrawString(Main_Font, Levels[3], new Vector2(430, 0), Color.Brown);
                }
                if (Level_5 == true)
                {
                    if (Enemy_List.Count < 1)
                    {
                        Enemy_List.Add(Enemy_Rec_1);
                        Enemy_List.Add(Enemy_Rec_2);
                        Enemy_List.Add(Enemy_Rec_3);
                        Enemy_List.Add(Enemy_Rec_4);
                        Enemy_List.Add(Enemy_Rec_5);
                        Enemy_List.Add(Enemy_Rec_6);
                        Enemy_List.Add(Enemy_Rec_7);
                        Enemy_List.Add(Enemy_Rec_8);
                        Enemy_List.Add(Enemy_Rec_9);
                        Enemy_List.Add(Enemy_Rec_10);
                    }
                    if (Enemy_List_2.Count < 1)
                    {
                        Enemy_List_2.Add(Enemy_Rec_12);
                        Enemy_List_2.Add(Enemy_Rec_13);
                        Enemy_List_2.Add(Enemy_Rec_14);
                        Enemy_List_2.Add(Enemy_Rec_15);
                        Enemy_List_2.Add(Enemy_Rec_16);
                        Enemy_List_2.Add(Enemy_Rec_17);
                        Enemy_List_2.Add(Enemy_Rec_18);
                        Enemy_List_2.Add(Enemy_Rec_19);
                        Enemy_List_2.Add(Enemy_Rec_20);
                    }
                    if (Enemy_List_3.Count < 1)
                    {
                        Enemy_List_3.Add(Enemy_Rec_21);
                        Enemy_List_3.Add(Enemy_Rec_22);
                        Enemy_List_3.Add(Enemy_Rec_23);
                        Enemy_List_3.Add(Enemy_Rec_24);
                        Enemy_List_3.Add(Enemy_Rec_25);
                        Enemy_List_3.Add(Enemy_Rec_26);
                        Enemy_List_3.Add(Enemy_Rec_27);
                        Enemy_List_3.Add(Enemy_Rec_28);
                        Enemy_List_3.Add(Enemy_Rec_29);
                        Enemy_List_3.Add(Enemy_Rec_30);
                    }
                    if (Enemy_List.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_1, Enemy_List[i], Color.White);
                        }
                    }
                    if (Enemy_List_2.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List_2.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_2, Enemy_List_2[i], Color.White);
                        }
                    }
                    if (Enemy_List_3.Count >= 1)
                    {
                        for (int i = 0; i < Enemy_List_3.Count; i++)
                        {
                            spriteBatch.Draw(Enemy_Tex_3, Enemy_List_3[i], Color.White);
                        }
                    }
                    spriteBatch.DrawString(Main_Font, Levels[4], new Vector2(430, 0), Color.Brown);
                }
                if (Sage_Settings == true)
                {
                    if (Sage_Health <= 50 && Sage_Health >= 40)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_4_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_5_Rec, Color.White);
                    }
                    if (Sage_Health <= 39 && Sage_Health >= 30)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_4_Rec, Color.White);
                    }
                    if (Sage_Health <= 29 && Sage_Health >= 20)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                    }
                    if (Sage_Health <= 19 && Sage_Health >= 10)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                    }
                    if (Sage_Health <= 9 && Sage_Health >= 1)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                    }
                    if (Sage_Health <= 0)
                    {
                        Died = true;
                        Game_State = GameState.Exit_Screen;
                    }
                }
                if (Lance_Settings == true)
                {
                    if (Lance_Health <= 65 && Lance_Health >= 52)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_4_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_5_Rec, Color.White);
                    }
                    if (Lance_Health <= 51 && Lance_Health >= 40)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_4_Rec, Color.White);
                    }
                    if (Lance_Health <= 49 && Lance_Health >= 38)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                    }
                    if (Lance_Health <= 37 && Lance_Health >= 27)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                    }
                    if (Lance_Health <= 26 && Lance_Health >= 0)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                    }
                    if (Lance_Health <= 0)
                    {
                        Died = true;
                        Game_State = GameState.Exit_Screen;
                    }
                }
                if (Axe_Settings == true)
                {
                    if (Axe_Health <= 70 && Axe_Health >= 65)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_4_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_5_Rec, Color.White);
                    }
                    if (Axe_Health <= 64 && Axe_Health >= 45)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_4_Rec, Color.White);
                    }
                    if (Axe_Health <= 44 && Axe_Health >= 35)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_3_Rec, Color.White);
                    }
                    if (Axe_Health <= 34 && Axe_Health >= 25)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                        spriteBatch.Draw(Health_Tex, Health_2_Rec, Color.White);
                    }
                    if (Axe_Health <= 24 && Axe_Health >= 0)
                    {
                        spriteBatch.Draw(Health_Tex, Health_1_Rec, Color.White);
                    }
                    if (Axe_Health <= 0)
                    {
                        Died = true;
                        Game_State = GameState.Exit_Screen;
                    }
                }
                if (Enemy_Damage_1 == true)
                {
                    Enemy_Damage_1 = true;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-1", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_2 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = true;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-2", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_3 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = true;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-3", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_4 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = true;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-4", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_5 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = true;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-5", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_6 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = true;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-6", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_7 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = true;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-7", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_8 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = true;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-8", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_9 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = true;
                    Enemy_Damage_10 = false;
                    spriteBatch.DrawString(Main_Font, "-9", new Vector2(Player_Current_Character_Rec.X - 20, Player_Current_Character_Rec.Y - 20), Color.Red);
                }
                if (Enemy_Damage_10 == true)
                {
                    Enemy_Damage_1 = false;
                    Enemy_Damage_2 = false;
                    Enemy_Damage_3 = false;
                    Enemy_Damage_4 = false;
                    Enemy_Damage_5 = false;
                    Enemy_Damage_6 = false;
                    Enemy_Damage_7 = false;
                    Enemy_Damage_8 = false;
                    Enemy_Damage_9 = false;
                    Enemy_Damage_10 = true;
                    spriteBatch.DrawString(Main_Font, "-10", new Vector2(Player_Current_Character_Rec.X - 10, Player_Current_Character_Rec.Y - 10), Color.Red);
                }
                if (Sage_Attack == true)
                {
                    Magic_Attack_Rec = new Rectangle(Mouse_State.X, Mouse_State.Y, 20, 20);
                    spriteBatch.Draw(Magic_Attack_Tex, Magic_Attack_Rec, Color.White);
                }
                if (Lance_Attack == true)
                {
                    Lance_Attack_Rec = new Rectangle(Mouse_State.X, Mouse_State.Y, 20, 20);
                    spriteBatch.Draw(Lance_Attack_Tex, Lance_Attack_Rec, Color.White);
                }
                if (Axe_Attack == true)
                {
                    Axe_Attack_Rec = new Rectangle(Mouse_State.X, Mouse_State.Y, 20, 20);
                    spriteBatch.Draw(Axe_Attack_Tex, Axe_Attack_Rec, Color.White);
                }
                if (Village_Pillage == true)
                {
                    Game_State = GameState.Exit_Screen;
                }
                spriteBatch.DrawString(Main_Font, "Villagers Alive " + Village_Health.ToString(), new Vector2 (200,25), Color.Brown);
            }
        }
        public void Exit_Screen_Draw_State()
        {
            if (Died == false)
            {
                spriteBatch.Draw(Menu_Screen_Tex, Menu_Screen_Rec, Color.White);
                spriteBatch.Draw(Exit_Button_Tex, Exit_Button_Rec, Color.Brown);
                spriteBatch.DrawString(Main_Font, "You killed " + Enemy_Kill_Total.ToString() + " Enemies.", new Vector2(110, 300), Color.Brown);
            }
            if (Village_Pillage == true)
            {
                spriteBatch.Draw(Menu_Screen_Tex, Menu_Screen_Rec, Color.White);
                spriteBatch.Draw(Exit_Button_Tex, Exit_Button_Rec, Color.Brown);
                spriteBatch.DrawString(Main_Font, "You killed " + Enemy_Kill_Total.ToString() + " Enemies.", new Vector2(110, 300), Color.Brown);
                spriteBatch.DrawString(Main_Font, "Your village got pillaged! :'( ", new Vector2(110, 320), Color.Brown);
            }
            if (Died == true)
            {
                spriteBatch.Draw(Menu_Screen_Tex, Menu_Screen_Rec, Color.White);
                spriteBatch.Draw(Exit_Button_Tex, Exit_Button_Rec, Color.Brown);
                spriteBatch.DrawString(Main_Font, "You killed " + Enemy_Kill_Total.ToString() + " Enemies.", new Vector2(110, 300), Color.Brown);
                spriteBatch.DrawString(Main_Font, "You Died! :'( ", new Vector2(110, 320), Color.Brown);

            }
        }
    }
}