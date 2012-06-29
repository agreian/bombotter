using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BomberLoutre.Controls
{
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        #region Keyboard Field Region

        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;

        #endregion

        #region Game Pad Field Region

        static GamePadState[] gamePadStates;
        static GamePadState[] lastGamePadStates;
        public static GamePadState xboxPadState;
        public static GamePadCapabilities xboxPadCap;

        #endregion

        #region Keyboard Property Region

        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        #endregion

        #region Game Pad Property Region

        public static GamePadState[] GamePadStates
        {
            get { return gamePadStates; }
        }

        public static GamePadState[] LastGamePadStates
        {
            get { return lastGamePadStates; }
        }

        #endregion

        #region Constructor Region

        public InputHandler(BomberLoutreGame game)
            : base(game)
        {
            keyboardState = Keyboard.GetState();

            gamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];

            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                gamePadStates[(int)index] = GamePad.GetState(index);
        }

        #endregion

        #region XNA methods

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            lastGamePadStates = (GamePadState[])gamePadStates.Clone();
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                gamePadStates[(int)index] = GamePad.GetState(index);

            xboxPadState = gamePadStates[(int) PlayerIndex.One];

            xboxPadCap = GamePad.GetCapabilities(PlayerIndex.One);



            base.Update(gameTime);
        }

        #endregion

        #region General Method Region

        public static void Flush()
        {
            lastKeyboardState = keyboardState;
        }

        #endregion

        #region Keyboard Region

        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) &&
                lastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) &&
                lastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool HavePressedKey()
        {
            return keyboardState != lastKeyboardState;
        }

        public static Keys[] GetPressedKeys()
        {
            return keyboardState.GetPressedKeys();
        }

        #endregion

        #region Game Pad Region

        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonUp(button) &&
                lastGamePadStates[(int)index].IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonDown(button) &&
                lastGamePadStates[(int)index].IsButtonUp(button);
        }

        public static bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonDown(button);
        }

        #endregion

        #region Keyboard AND GamePad Region
        public static bool Pushed(string touch, PlayerIndex index)
        {
            switch(touch)
            {
                case "Up":
                    return ButtonReleased(Buttons.DPadUp, index) || KeyPressed(Properties.App.Default.KeyUp);
                case "Down":
                    return ButtonReleased(Buttons.DPadDown, index) || KeyPressed(Properties.App.Default.KeyDown);
                case "Left":
                    return ButtonReleased(Buttons.DPadLeft, index) || KeyPressed(Properties.App.Default.KeyLeft);
                case "Right":
                    return ButtonReleased(Buttons.DPadRight, index) || KeyPressed(Properties.App.Default.KeyRight);
                case "Space":
                    return ButtonReleased(Buttons.B, index) || KeyPressed(Properties.App.Default.KeySpace);
                case "Enter":
                    return ButtonReleased(Buttons.A, index) || KeyPressed(Keys.Enter);
                case "Escape":
                    return ButtonReleased(Buttons.X, index) || KeyPressed(Keys.Escape);
            }

            return false;
        }

        public static bool Maintained(string touch, PlayerIndex index)
        {
            switch (touch)
            {
                case "Up":
                    return ButtonDown(Buttons.DPadUp, index) || KeyDown(Properties.App.Default.KeyUp);
                case "Down":
                    return ButtonDown(Buttons.DPadDown, index) || KeyDown(Properties.App.Default.KeyDown);
                case "Left":
                    return ButtonDown(Buttons.DPadLeft, index) || KeyDown(Properties.App.Default.KeyLeft);
                case "Right":
                    return ButtonDown(Buttons.DPadRight, index) || KeyDown(Properties.App.Default.KeyRight);
                case "Space":
                    return ButtonDown(Buttons.B, index) || KeyDown(Properties.App.Default.KeySpace);
                case "Enter":
                    return ButtonDown(Buttons.A, index) || KeyDown(Keys.Enter);
                case "Escape":
                    return ButtonDown(Buttons.X, index) || KeyDown(Keys.Escape);
            }

            return false;
        }
        #endregion
    }
}
