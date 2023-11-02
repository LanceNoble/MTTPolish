using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MTTPolish.Mechanics.StateStuff
{
    internal interface IState
    {
        public void Initialize();
        public void LoadContent(ContentManager content);
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
