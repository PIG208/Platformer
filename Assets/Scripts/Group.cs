public enum Group
{
    Netural = 0,
    Friendly = 1,
    Enemy = 2,
}

public static class Groups
{
    /// <summary>Return true when the two are different groups and none of them is netural</summary>
    public static bool IsHotileTo(this Group group, Group other)
    {
        return group != other && (group | other) != Group.Netural;
    }
}