namespace StablerCharacter.Scenes
{
    public sealed class SceneManager
    {
        public static Scene[] scenes = Array.Empty<Scene>();
        static Scene? currentScene;
        static ushort currentSceneIndex = 0;

        static internal void LoadFirstScene()
        {
            LoadScene(0);
        }

        public static void LoadScene(ushort index)
        {
            if (currentScene != null) currentScene.OnUnload();
            currentScene = scenes[index];
            currentScene.OnLoad();
            GameManager.currentScene = currentScene;
            currentSceneIndex = index;
        }

        public static void LoadNextScene()
        {
            LoadScene(++currentSceneIndex);
        }
    }
}
