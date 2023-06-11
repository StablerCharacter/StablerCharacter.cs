using Serilog;
using System;

namespace StablerCharacter.Scenes
{
    public sealed class SceneManager
    {
        public static Scene[] Scenes { get; internal set; } = Array.Empty<Scene>();
        public static Scene? CurrentScene { get; private set; }
        static ushort s_currentSceneIndex = 0;

        static internal void LoadFirstScene()
        {
            LoadScene(0);
        }

        public static void LoadScene(ushort index)
        {
            Log.Debug("[SceneManager] Loading scene index {index}", index);
            CurrentScene?.OnUnload();
            CurrentScene = Scenes[index];
            CurrentScene.OnLoad();
            s_currentSceneIndex = index;
        }

        public static void LoadNextScene()
        {
            LoadScene(++s_currentSceneIndex);
        }

        /// <summary>
        /// Add the render objects of a scene to another scene
        /// </summary>
        public static void MergeScene(ushort fromIndex, ushort toIndex)
        {
            Log.Debug("[SceneManager] Merging scene from scene index {fromIndex} to scene index {toIndex}", fromIndex, toIndex);
            Scene from = Scenes[fromIndex];
            if (!from.HasBeenLoaded) from.OnLoad();
            Scene to = Scenes[toIndex];
            to.RenderObjects.AddRange(from.RenderObjects);
            if (!to.HasBeenLoaded) to.OnLoad();
        }
    }
}
