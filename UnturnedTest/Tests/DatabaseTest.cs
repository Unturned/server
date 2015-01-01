using System;
using NUnit.Framework;
using System.Collections.Generic;


[TestFixture]
public class DatabaseTest
{
    public Dictionary<String, NetworkBanned> banList;

    [Test]
    public void testBanDatabaseRead()
    {
        Console.WriteLine("Loading bans from file");
        banList = DataHolder.FileDatabase.LoadBans();
        Console.WriteLine("Loaded {0} entry", banList.Count);
    }

    [Test]
    public void testBanDatabaseWrite()
    {
        if (banList.Count == 0)
        {
            banList.Add("76561197994222727", new NetworkBanned("Julius Tiger", "76561197994222727", "Test Case of course", "76561197994222727", DateTime.Now));
        }

        Console.WriteLine("Saving...");
        DataHolder.FileDatabase.SaveBans(banList);
        Console.WriteLine("Done!");
    }

    [Test]
    public void testStructuresDatabaseWrite() {
        DataHolder.FileDatabase.SaveStructures("asdf");
    }
}


