using UnityEngine;

namespace Saves
{
    public class SaveSystem
    {
        private const string Level = "Level";
        private const string MoneyWallet = "MoneyWallet";

        public void SaveLevel(int level) =>
            PlayerPrefs.SetInt(Level, level);

        public void SaveMoney(float value) =>
            PlayerPrefs.SetFloat(MoneyWallet, value);

        public int LoadLevel()
        {
            if (PlayerPrefs.GetInt(Level) == 0)
                return 1;
            else
                return PlayerPrefs.GetInt(Level);
        }

        public float LoadMoney() => 
            PlayerPrefs.GetFloat(MoneyWallet);
    }
}
