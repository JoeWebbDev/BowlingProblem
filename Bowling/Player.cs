class Player
{
    private static int playerCounter = 0;
    public int score { get; set; }
    private int playerID;
    public string playerName { get; set; }

    public Player(string name)
    {
        playerID = GetNextID();
        playerName = name;
        score = 0;
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
