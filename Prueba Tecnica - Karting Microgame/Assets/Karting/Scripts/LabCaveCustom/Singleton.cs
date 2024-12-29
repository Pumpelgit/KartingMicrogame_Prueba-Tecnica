using System.Collections;
using System.Collections.Generic;

public class Singleton
{
    static Singleton instance = null;
    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
}
