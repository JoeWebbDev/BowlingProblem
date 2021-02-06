class Player
{
    private static int playerCounter = 0;
    private int playerID;
    
    public string playerName { get; set; }

    public Player(string name)
    {
        playerID = GetNextID();
        playerName = name;

    }


    public int GetPlayerID()
    {
        return this.playerID;
    }

    private int GetNextID()
    {
        playerCounter += 1;
        return playerCounter - 1;
    }
}
