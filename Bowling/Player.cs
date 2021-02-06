namespace Bowling
{
    class Player
    {
        private static int playerCounter = 0;
        private int ID;
        public Score score;

        public string name { get; private set; }

        public Player(string playerName)
        {
            ID = GetNextID();
            name = playerName;
            score = new Score();
        }

        public int GetPlayerID()
        {
            return this.ID;
        }

        private int GetNextID()
        {
            playerCounter += 1;
            return playerCounter - 1;
        }
    }
}
