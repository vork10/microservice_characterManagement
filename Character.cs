using System.Collections;

public class Character
{
    public static List<Character> AllCharacters = new List<Character>();
    public int Id { get; set; }
    public string Name { get; set; }
    public CharacterClassType ClassType { get; set; }
    public int Level { get; set; }

    public Character(int id, string name, CharacterClassType classType, int level)
    {
        Id = id;
        Name = name;
        ClassType = classType;
        Level = level;

        AllCharacters.Add(this);
    }
}