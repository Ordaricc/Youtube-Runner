using UnityEngine;

[System.Serializable]
public struct Language
{
    public enum Languages { en, it, fr }
    public LineCategoryClass[] categories;
}

[System.Serializable]
public struct LineCategoryClass
{
    public enum LineCategory { menu, dialogue, tutorial }
    public LineCategory category;
    public LineIDClass[] lines;
}

[System.Serializable]
public struct LineIDClass
{
    public string lineID;
    public Translation[] translations;
}

[System.Serializable]
public struct Translation
{
    public Language.Languages language;
    [TextArea] public string line;
}