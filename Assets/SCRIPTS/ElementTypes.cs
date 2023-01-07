using System;

public static class ElementTypes 
{
    [Flags]
    public enum Elements
    {
        Fire=1<<0,
        Ice=1<<1,
        Air=1<<2,
        Poison=1<<3,
        Metal=1<<4,
    }
}
