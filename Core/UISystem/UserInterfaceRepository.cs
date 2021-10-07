using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace TheBackrooms.Core.UISystem
{
    public class UserInterfaceRepository
    {
        protected readonly UserInterface Interface = new UserInterface();
        protected GameTime GameTimeCache;

        public Dictionary<Type, UIState> States = new Dictionary<Type, UIState>();

        public void Register<T>(bool activate = false) where T : UIState, new() => Register(new T(), activate);

        public void Register<T>(T uiState, bool activate = false) where T : UIState
        {
            if (IsRegistered<T>())
                throw new InvalidOperationException("Can not re-register pre-existing UI state.");

            if (activate)
                uiState.Activate();

            States[uiState.GetType()] = uiState;
        }

        // TODO: Docs. unloadClean means we unload with IHasUnloadableData
        public void Unregister<T>(bool unloadClean = true) where T : UIState => Unregister(typeof(T), unloadClean);

        public void Unregister(Type type, bool unloadClean = true)
        {
            if (!IsRegistered(type))
                throw new InvalidOperationException("Cannot unregister non-registered UI state.");

            UIState state = States[type];

            // ReSharper disable once SuspiciousTypeConversion.Global
            if (unloadClean && state is IHasUnloadableData unloadable)
                unloadable.UnloadData();

            States[type] = null;
        }

        public void ToggleState<T>() => ToggleState(typeof(T));

        public void ToggleState(Type type)
        {
            if (!IsRegistered(type))
                throw new InvalidOperationException("Cannot toggle the state of a non-registered UI state.");

            Interface.SetState(Interface.CurrentState == States[type] ? null : States[type]);
        }

        public T GetState<T>() where T : UIState => (T) States[typeof(T)];

        public void UpdateUIs(GameTime gameTime)
        {
            GameTimeCache = gameTime;
            Interface.CurrentState?.Update(gameTime);
        }

        public void DrawUIs(SpriteBatch spriteBatch)
        {
            if (!(GameTimeCache is null))
                Interface.Draw(spriteBatch, GameTimeCache);
        }

        public bool Enabled<T>() where T : UIState => Enabled(typeof(T));

        public bool Enabled(Type type) => Interface.CurrentState == States[type];

        public bool IsRegistered<T>() where T : UIState => IsRegistered(typeof(T));

        public bool IsRegistered(Type type) => States.ContainsKey(type);
    }
}