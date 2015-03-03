using System;

public class MedicalStats
{
    public MedicalStats()
    {
    }

    public static bool getBleeding(int id)
    {
        switch (id)
        {
            case 13006: // Dressing
                return true;
            case 13008: // Bandage
                return true;
            case 13000: // Medikit
                return true;
            case 13001: // Rag
                return true;
            default:
                return false;
        }
    }

    public static bool getBones(int id)
    {
        switch (id)
        {
            case 13000:
                {
                    return true;
                }
            case 13002:
                {
                    return true;
                }
            case 13010:
                {
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }

    public static int getHealth(int id)
    {
        switch (id)
        {
            case 13000:
                {
                    return 50;
                }
            case 13001:
                {
                    return 5;
                }
            case 13002:
                {
                    return 10;
                }
            case 13003:
            case 13017:
                {
                    return 0;
                }
            case 13004:
                {
                    return 50;
                }
            case 13005:
                {
                    return 30;
                }
            case 13006:
                {
                    return 20;
                }
            case 13007:
                {
                    return 100;
                }
            case 13008:
                {
                    return 10;
                }
            case 13009:
                {
                    return 10;
                }
            case 13010:
                {
                    return 10;
                }
            case 13011:
                {
                    return 10;
                }
            case 13012:
                {
                    return 20;
                }
            case 13013:
                {
                    return 5;
                }
            case 13014:
                {
                    return 20;
                }
            case 13015:
                {
                    return 20;
                }
            case 13016:
                {
                    return 10;
                }
            case 13018:
                {
                    return 10;
                }
            default:
                {
                    return 0;
                }
        }
    }

    public static int getPain(int id)
    {
        int num = id;
        if (num == 13000)
        {
            return 120;
        }
        if (num == 13005)
        {
            return 120;
        }
        if (num == 13010)
        {
            return 120;
        }
        if (num == 13015)
        {
            return 200;
        }
        return 0;
    }

    public static int getSickness(int id)
    {
        int num = id;
        switch (num)
        {
            case 13000:
                {
                    return 20;
                }
            case 13003:
                {
                    return 15;
                }
            case 13004:
                {
                    return 25;
                }
            case 13006:
                {
                    return 5;
                }
            case 13013:
                {
                    return 25;
                }
            case 13014:
            case 13015:
            case 13016:
                {
                    return 0;
                }
            case 13017:
                {
                    return 5;
                }
            case 13018:
                {
                    return 50;
                }
            default:
                {
                    return 0;
                }
        }
    }

    public static int getStamina(int id)
    {
        int num = id;
        if (num == 13009)
        {
            return 100;
        }
        if (num == 13014)
        {
            return 40;
        }
        return 0;
    }
}