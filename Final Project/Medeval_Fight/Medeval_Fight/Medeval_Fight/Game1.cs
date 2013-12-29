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
        Rectangle Splash_Screen_Rec, Menu_Screen_Rec,Exit_Button_Rec, Start_Button_Rec, Info_Button_Rec, Grass_Tile_Rec, House_Tile_Rec, Road_Tile_Rec , Player_Character_Rec, Enemy_Current_Rec, Player_Pick_Rec;
        //Textures
        Texture2D Splash_Screen_Tex,Menu_Screen_Tex,Exit_Button_Tex,Start_Button_Tex, Info_Button_Tex, Grass_Tile_Tex, House_Tile_Tex, Road_Tile_Tex, Enemy_Tex, Player_Character_Current_Tex,
            Player_Character_Sage_Front_Tex, Player_Character_Sage_Back_Tex, Player_Character_Sage_Right_Tex, Player_Character_Sage_Left_Tex;
        //Integers cuhhhh
        int Splash_Screen_Timer = 0, BG_Grid_Col, BG_Grid_Row, X_Value_Enemy, Y_Value_Enemy, Total_Timer, Tick_Counter, Enemy_X_Compare_Cord, Enemy_Y_Compare_Cord, Player_X_Compare_Cord, Player_Y_Compare_Cord
            , Total_X_Cord, Total_Y_Cord, Total_Cord, Enemy_Number_Rate;
        //booleans
        bool Splash_Load_1, Splash_Load_2, Splash_Load_3, Splash_Load_4, Splash_Load_5, Controls_Menu, Enemy_Dead, Character_Pick, Sage_Settings = true, Lancemen_Settings, Horseman_Settings, Enemy_Generate;
        //Grids
        Rectangle[,] BG_Grid = new Rectangle[9, 9];
        //Dem random number generators
        Random Random_Number = new Random();
        Random Enemy_Number = new Random();
        //lists
        List<Texture2D> Enemy_List;
        List<int> Enemy_Y_List;
        List<int> Enemy_X_List;
        List<int> Enemy_Health;
        //arrays
        string[] Loading = new string[5] {"Loading." , "Loading.." , "Loading..." , "Loading...." , "Loading....."};
        //Gamestates
        enum GameState
        {
            Splash_Screen, Menu_Screen, Game_Play_Screen, Exit_Screen
        }
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
            Splash_Screen_Rec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Menu_Screen_Rec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            Exit_Button_Rec = new Rectangle(0, 480, 150, 50);
            Start_Button_Rec = new Rectangle(200, 480, 150, 50);
            Info_Button_Rec = new Rectangle(400, 480, 150, 50);
            Grass_Tile_Rec = new Rectangle(0, 0, 60, 60);
            House_Tile_Rec = new Rectangle(0, 0, 60, 60);
            Road_Tile_Rec = new Rectangle(0, 0, 60, 60);
            Player_Character_Rec = new Rectangle(0, 0, 40, 40);
            Enemy_Current_Rec = new Rectangle(60, 60, 40, 40);
            Player_Pick_Rec = new Rectangle(0,0, 60,60);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Main_Font = Content.Load<SpriteFont>("SpriteFont1");
            Splash_Screen_Tex = Content.Load<Texture2D>("Splash");
            Menu_Screen_Tex = Content.Load<Texture2D>("Menu");
            Exit_Button_Tex = Content.Load<Texture2D>("exit");
            Start_Button_Tex = Content.Load<Texture2D>("start");
            Info_Button_Tex = Content.Load<Texture2D>("info");
            Grass_Tile_Tex = Content.Load<Texture2D>("grass");
            House_Tile_Tex = Content.Load<Texture2D>("house");
            Road_Tile_Tex = Content.Load<Texture2D>("road");
            Enemy_Tex = Content.Load<Texture2D>("Enemy");
            Player_Character_Current_Tex = Content.Load<Texture2D>("Player_Sage_Front");
            Player_Character_Sage_Back_Tex = Content.Load<Texture2D>("Player_Sage_Back");
            Player_Character_Sage_Front_Tex = Content.Load<Texture2D>("Player_Sage_Front");
            Player_Character_Sage_Left_Tex = Content.Load<Texture2D>("Player_Sage_Left");
            Player_Character_Sage_Right_Tex = Content.Load<Texture2D>("Player_Sage_Right"); 
            Enemy_List = new List<Texture2D>();
            Enemy_X_List = new List<int>();
            Enemy_Y_List = new List<int>();
            Enemy_Health = new List<int>();
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
            Mouse_State = Mouse.GetState();
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
            Last_Click_Mouse = Mouse_State;
            base.Update(gameTime);
        }
        public void Splash_Screen_Update_State()
        {
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
            //change font, add title, and hoverovers
            //Mouse_State = Mouse.GetState();
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
        }
        public void Game_Play_Update_State()
        {
            //Character choose code yuh boi
            if (Character_Pick == false)
            {
                if (Player_Pick_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed)
                {
                    Character_Pick = true;
                }
            }
            //testing for enemy generation 
            if (Mouse_State.RightButton == ButtonState.Pressed && Last_Click_Mouse.RightButton == ButtonState.Released)
            {
                X_Value_Enemy = Random_Number.Next(0, 9);
                Y_Value_Enemy = Random_Number.Next(0, 9);
                Enemy_X_List.Add(X_Value_Enemy);
                Enemy_Y_List.Add(Y_Value_Enemy);
                Enemy_List.Add(Enemy_Tex);
            }
            KeyboardState Keyboard_State = Keyboard.GetState();
            //player movement
            if (Push_Keyboard_State.IsKeyUp(Keys.W) && Keyboard_State.IsKeyDown(Keys.W))
            {
                Player_Character_Current_Tex = Player_Character_Sage_Back_Tex;
                Player_Character_Rec.Y -= 20;
            }
            if (Push_Keyboard_State.IsKeyUp(Keys.S) && Keyboard_State.IsKeyDown(Keys.S))
            {
                Player_Character_Current_Tex = Player_Character_Sage_Front_Tex;
                Player_Character_Rec.Y += 20;
            }
            if (Push_Keyboard_State.IsKeyUp(Keys.A) && Keyboard_State.IsKeyDown(Keys.A))
            {
                Player_Character_Current_Tex = Player_Character_Sage_Left_Tex;
                Player_Character_Rec.X -= 20;
            }
            if (Push_Keyboard_State.IsKeyUp(Keys.D) && Keyboard_State.IsKeyDown(Keys.D))
            {
                Player_Character_Current_Tex = Player_Character_Sage_Right_Tex;
                Player_Character_Rec.X += 20;
            }
            Push_Keyboard_State = Keyboard_State;
            //attack code sage (ranged attack but weaker)
            if (Enemy_Current_Rec.Contains(Mouse_State.X, Mouse_State.Y) && Mouse_State.LeftButton == ButtonState.Pressed)
            {
                if (Horseman_Settings == true)
                {
                    Player_X_Compare_Cord = Player_Character_Rec.X;
                    Player_Y_Compare_Cord = Player_Character_Rec.Y;
                    Enemy_X_Compare_Cord = Enemy_Current_Rec.X;
                    Enemy_Y_Compare_Cord = Enemy_Current_Rec.Y;
                    Total_X_Cord = Player_X_Compare_Cord - Enemy_X_Compare_Cord;
                    Total_Y_Cord = Player_Y_Compare_Cord - Enemy_Y_Compare_Cord;
                    if (Total_X_Cord < -1 || Total_X_Cord > 1)
                    {
                        Enemy_Dead = true;
                    }
                    if (Total_Y_Cord < -1 || Total_Y_Cord > 1)
                    {
                        Enemy_Dead = false;
                    }
                    if (Total_Y_Cord < -30 || Total_Y_Cord > 30)
                    {
                        Enemy_Dead = true;
                    }
                    if (Total_X_Cord < -15 || Total_X_Cord > 15)
                    {
                        Enemy_Dead = false;
                    }
                }
                if (Lancemen_Settings == true)
                {
                    Player_X_Compare_Cord = Player_Character_Rec.X;
                    Player_Y_Compare_Cord = Player_Character_Rec.Y;
                    Enemy_X_Compare_Cord = Enemy_Current_Rec.X;
                    Enemy_Y_Compare_Cord = Enemy_Current_Rec.Y;
                    Total_X_Cord = Player_X_Compare_Cord - Enemy_X_Compare_Cord;
                    Total_Y_Cord = Player_Y_Compare_Cord - Enemy_Y_Compare_Cord;
                    if (Total_X_Cord < -1 || Total_X_Cord > 1)
                    {
                        Enemy_Dead = true;
                    }
                    if (Total_Y_Cord < -1 || Total_Y_Cord > 1)
                    {
                        Enemy_Dead = false;
                    }
                    if (Total_Y_Cord < -50 || Total_Y_Cord > 50)
                    {
                        Enemy_Dead = true;
                    }
                    if (Total_X_Cord < -50 || Total_X_Cord > 50)
                    {
                        Enemy_Dead = false;
                    }
                }
                if (Sage_Settings == true)
                {
                    Player_X_Compare_Cord = Player_Character_Rec.X;
                    Player_Y_Compare_Cord = Player_Character_Rec.Y;
                    Enemy_X_Compare_Cord = Enemy_Current_Rec.X;
                    Enemy_Y_Compare_Cord = Enemy_Current_Rec.Y;
                    Total_X_Cord = Player_X_Compare_Cord - Enemy_X_Compare_Cord;
                    Total_Y_Cord = Player_Y_Compare_Cord - Enemy_Y_Compare_Cord;
                    if (Total_X_Cord < 0 || Total_X_Cord > 1 && Total_Y_Cord < -1 || Total_Y_Cord > 1)
                    {
                        Enemy_Dead = true;
                    }
                    if (Total_X_Cord < -30 || Total_X_Cord > 30 && Total_Y_Cord < -100 || Total_Y_Cord > 100)
                    {
                        Enemy_Dead = true;
                    }
                }
            }
            //timer
            Tick_Counter++;
            if (Tick_Counter == 60)
            {
                Total_Timer++;
                Tick_Counter = 0;
            }
            //  generates the enemies rate and enemy generation
            Enemy_Number_Rate = Enemy_Number.Next(-1000, 1000);
            if (Enemy_Number_Rate < -500)
            {
                Enemy_Dead = true;
            }
        }
        public void Exit_Screen_Update_State()
        {
            Exit();
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
            spriteBatch.Draw(Splash_Screen_Tex, Splash_Screen_Rec, Color.White);
            if (Splash_Load_1 == false || Splash_Load_2 == false || Splash_Load_3 == false || Splash_Load_4 == false || Splash_Load_5 == false)
            {
                //put dat shiza in a list
                if (Splash_Load_1 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[1], new Vector2(0, 500), Color.White);
                }
                if (Splash_Load_2 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[2], new Vector2(0, 500), Color.White);
                }
                if (Splash_Load_3 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[3], new Vector2(0, 500), Color.White);
                }
                if (Splash_Load_4 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[4], new Vector2(0, 500), Color.White);
                }
                if (Splash_Load_5 == true)
                {
                    spriteBatch.DrawString(Main_Font, Loading[5], new Vector2(0, 500), Color.White);
                }
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
        }
        public void Game_Play_Draw_State()
        {
            //character picking boi yuhhhhhhhh 
            if (Character_Pick == false)
            {
                spriteBatch.Draw(Player_Character_Sage_Front_Tex, Player_Pick_Rec, Color.White);
                spriteBatch.DrawString(Main_Font, "Sage", new Vector2(150, 50), Color.Brown);
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
                for (int i = 0; i < Enemy_X_List.Count; i++)
                {
                    //spriteBatch.Draw(Enemy_List[i], BG_Grid[Enemy_X_List[i], Enemy_Y_List[i]], Color.White);
                    //testing only
                    if (Enemy_Dead == false)
                    {
                        spriteBatch.Draw(Enemy_List[i], Enemy_Current_Rec, Color.White);
                    }
                    //
                }
                spriteBatch.Draw(House_Tile_Tex, BG_Grid[3, 3], Color.White);
                for (int i = 0; i < 9; i++)
                {
                    spriteBatch.Draw(Road_Tile_Tex, BG_Grid[4, i], Color.White);
                    spriteBatch.Draw(Road_Tile_Tex, BG_Grid[i, 4], Color.White);
                }
                spriteBatch.Draw(Player_Character_Current_Tex, Player_Character_Rec, Color.White);
                spriteBatch.DrawString(Main_Font, Total_Timer.ToString(), new Vector2 (0,0), Color.Brown);
                if (Enemy_Generate == true)
                {
                    for (int i = 0; i < Enemy_X_List.Count; i++)
                    {
                        //spriteBatch.Draw(Enemy_List[i], BG_Grid[Enemy_X_List[i], Enemy_Y_List[i]], Color.White);
                        //testing only
                        spriteBatch.Draw(Enemy_List[i], Enemy_Current_Rec, Color.White);
                        if (Enemy_Dead == true)
                        {
                            Enemy_List.Remove(Enemy_List[i]);
                        }
                       
                    }
                }
            }
        }
        public void Exit_Screen_Draw_State()
        {

        }
    }
}