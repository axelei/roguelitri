﻿using System.Linq;
using Gum.DataTypes;
using Gum.Wireframe;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RenderingLibrary;
using roguelitri.Service;
using roguelitri.Util;

namespace roguelitri.Model.Scenes;

public class TitleScene : Scene
{
    private GraphicalUiElement _screen;
    
    public override void Initialize()
    {
        _screen = Game1.GumProject.Screens.First().ToGraphicalUiElement(SystemManagers.Default, addToManagers:true);
    }

    public override void Update(GameTime gameTime)
    {
        if (InputHelper.AnyKeyPressed())
        {
            SceneManager.SetScene(new GameScene());
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap);

        var logoLocation = Misc.CenterImage(new Vector2(ResourcesManager.Gfx.Title.Logo.Width, ResourcesManager.Gfx.Title.Logo.Height));
        
        spriteBatch.Draw(ResourcesManager.Gfx.Title.Background, new Vector2(0, 0), Color.White);
        spriteBatch.Draw(ResourcesManager.Gfx.Title.Logo, logoLocation, Color.White);

        spriteBatch.End();
    }

    public override void Dispose()
    {
        _screen.RemoveFromManagers();
    }
}