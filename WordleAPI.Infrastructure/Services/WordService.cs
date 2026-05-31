using WordleAPI.Application.Interfaces;

namespace WordleAPI.Infrastructure.Services;

public class WordService : IWordService
{
    private static readonly string[] WordList =
    {
        "APPLE", "BRAVE", "CHAIR", "DANCE", "EAGLE",
        "FABLE", "GRACE", "HOUSE", "IMAGE", "JOINT",
        "KNEEL", "LEMON", "MONEY", "NIGHT", "OLIVE",
        "PLAIN", "QUEEN", "RIVER", "STONE", "TABLE",
        "UPPER", "VALVE", "WATER", "XENON", "YACHT",
        "ZEBRA", "ALARM", "BLEND", "CRANE", "DRINK",
        "EARTH", "FLAME", "GRAND", "HEART", "INPUT",
        "JUDGE", "KNIFE", "LIGHT", "MATCH", "NERVE",
        "OCEAN", "PEACE", "QUICK", "REACH", "SWING",
        "TRADE", "UNITE", "VIOLA", "WASTE", "YEARN",
        "CANDY", "BIRTH", "CLOUD", "DEBUT", "ELBOW",
        "FETCH", "GLOVE", "HONEY", "INBOX", "JEWEL",
        "KARMA", "LEGAL", "MAPLE", "NOBLE", "OFFER",
        "PROUD", "QUIRK", "RADAR", "SHORE", "TOWER",
        "UNDER", "VAULT", "WHEAT", "EXILE", "YOUTH",
        "ANGLE", "BLOOD", "COAST", "DEPTH", "EVENT",
        "FROST", "GROUP", "HEDGE", "IMPLY", "JOKER",
        "KNACK", "LYRIC", "MAGIC", "NOVEL", "OUTDO",
        "PATCH", "QUEST", "RANCH", "SNARE", "THETA",
        "UMBRA", "VOICE", "WRECK", "EXTRA", "ZONAL",
        "ATLAS", "BENCH", "CLIMB", "DRIFT", "EQUAL",
        "FIELD", "GRIND", "HUMID", "IRONY", "JAZZY",
        "KAZOO", "LUSTY", "MOIST", "NIPPY","OPTIC",
        "PROSE", "QUOTA", "RULER", "SWAMP", "THORN",
        "USAGE", "VIVID", "WALTZ", "BOXER", "YIELD"
    };

    private readonly Random _random = new();

    public string GetRandomWord()
    {
        return WordList[_random.Next(WordList.Length)];
    }

    public bool IsValidWord(string word)
    {
        return WordList.Contains(word.ToUpper());
    }
}
