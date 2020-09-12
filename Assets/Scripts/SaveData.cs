using System.Collections.Generic;
using System.Collections.ObjectModel;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData Current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }

            return _current;
        }

    }

    public int levelIndex;
    public string path;
    public Collection<Level> levels;


}