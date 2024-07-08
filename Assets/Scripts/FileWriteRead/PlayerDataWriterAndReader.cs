using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataWriterAndReader
{
    public string SaveDataDirectoryPath = "";
    public string SaveFileName = "";

    public PlayerDataWriterAndReader(string playerStatDataDirectoryPath, string playerStatDataFileName)
    {
        SaveDataDirectoryPath = playerStatDataDirectoryPath;
        SaveFileName = playerStatDataFileName;
    }

    public bool CheckToSeeFileExist()
    {
        if (File.Exists(Path.Combine(SaveDataDirectoryPath, SaveFileName)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(SaveDataDirectoryPath, SaveFileName));
    }

    public void CreateNewCharacterSaveFile(PlayerStats playerStats)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, SaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("Creating Save File At: " + savePath);

            string dataToStore = JsonUtility.ToJson(playerStats, true);

            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter fileWriter = new StreamWriter(stream))
                {
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch (System.Exception ex)
        {

            Debug.LogError("GAME NOT SAVED" + savePath + "" + ex.Message);
        }
    }

    public PlayerStats LoadSaveFile()
    {
        PlayerStats playerStatData = null;
        string loadPath = Path.Combine(SaveDataDirectoryPath, SaveFileName);

        if (File.Exists(loadPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                {
                    using (StreamReader fileReader = new StreamReader(stream))
                    {
                        dataToLoad = fileReader.ReadToEnd();
                    }
                }

                playerStatData = JsonUtility.FromJson<PlayerStats>(dataToLoad);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        return playerStatData;
    }

    ////////////////////////////////////////////////////////////////////// UNUSED - USED

    public PlayerStats InitializePlayerStatsFile()
    {
        PlayerStats playerStatData = null;
        string loadPath = Path.Combine(SaveDataDirectoryPath, SaveFileName);
        Debug.Log("load path : " + loadPath);

        if (File.Exists(loadPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                {
                    using (StreamReader fileReader = new StreamReader(stream))
                    {
                        dataToLoad = fileReader.ReadToEnd();
                    }
                }

                playerStatData = JsonUtility.FromJson<PlayerStats>(dataToLoad);
                return playerStatData;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        else
        {
            playerStatData = PlayerStatusManager.Instance.CreateNewPlayerStatObject();

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(loadPath));
                Debug.Log("Creating Save File At: " + loadPath);

                string dataToStore = JsonUtility.ToJson(playerStatData, true);

                using (FileStream stream = new FileStream(loadPath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (System.Exception ex)
            {

                Debug.LogError("GAME NOT SAVED" + loadPath + "" + ex.Message);
            }
            return playerStatData;
        }

    }

    public void UpdatePlayerStatsFile(PlayerStats playerStatData)
    {
        string savePath = Path.Combine(SaveDataDirectoryPath, SaveFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("Creating Save File At: " + savePath);

            string dataToStore = JsonUtility.ToJson(playerStatData, true);

            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                using (StreamWriter fileWriter = new StreamWriter(stream))
                {
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch (System.Exception ex)
        {

            Debug.LogError("GAME NOT SAVED" + savePath + "" + ex.Message);
        }

    }
}
