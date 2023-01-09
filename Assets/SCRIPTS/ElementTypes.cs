using System;

public static class ElementTypes 
{
    [Flags]
    public enum Elements //Типы биомов и видов урона (стихии)
    {
        Fire=1<<0,
        Ice=1<<1,
        Air=1<<2,
        Poison=1<<3,
        Metal=1<<4,
        Space=1<<5,
        GreenGrass=1<<6
    }
}
