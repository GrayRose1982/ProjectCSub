using ModelAirCraft;

public static class PathHelper 
{
    public static PartData GetPath(this PartData[] parts, TypePart typeGet)
    {
        for(int i  = 0; i < parts.Length;i++)
            if (parts[i].TypePart == typeGet)
                return parts[i];
        return null;
    }
}
