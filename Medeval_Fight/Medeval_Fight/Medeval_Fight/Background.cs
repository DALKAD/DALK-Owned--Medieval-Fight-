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
    class Background
    {
        private Rectangle background_rec;
        private Texture2D background_tex;
        private GraphicsDevice background_class_gd;
        public Background(Texture2D background_tex_var, Rectangle background_rec_var, GraphicsDevice background_class_gd_var)
        {
            background_rec = background_rec_var;
            background_tex = background_tex_var;
            background_class_gd = background_class_gd_var;
        }
        public void DrawSprite(SpriteBatch batch)
        {
            batch.Draw(background_tex, background_rec, Color.White);
        }
    }
}
