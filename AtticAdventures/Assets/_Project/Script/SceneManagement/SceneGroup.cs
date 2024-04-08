using System;
using Eflatun.SceneReference;
using System.Collections.Generic;
using System.Linq;

namespace AtticAdventures.SceneManagement
{
    [Serializable]
    public class SceneGroup
    {
        public string GroupName = "New Scene Group";
        public List<SceneData> Scenes;

        public string FindSceneNameByType(SceneType sceneType)
        {
            return Scenes.FirstOrDefault(scene => scene.SceneType == sceneType)?.Reference.Name;
        }
    }

    [Serializable]
    public class SceneData
    {
        public SceneReference Reference;
        public string Name => Reference.Name;
        public SceneType SceneType;
    }

    public enum SceneType { ActiveScene, MainMenu, UserInterface, HUD, Cinematic, Environment, Tooling }

}
