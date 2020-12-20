using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class EventsHandler : MonoBehaviour
{
   public GameObject ellen = null;

    private List<string[]> level_events_data = new List<string[]>();


    private List<string[]> sessions_data = new List<string[]>();
    private List<string[]> death_data = new List<string[]>();
    private List<string[]> positions_data = new List<string[]>();


    private int last_session_id = 0;
    private int last_death_id = 0;

    public string username = "manavld";

    enum TypeEvent
    {
        EVENT_NONE,
        END_SESSION,
        DEATH,
        POSITION
    }

    // Start is called before the first frame update
    void Start()
    {


        PlayerPrefs.SetString("start_time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));



        if (System.IO.File.Exists("Assets/CSV/sessions.csv") == false)
        {
            string[] row_data_temp = new string[4];

            row_data_temp[0] = "session_id";
            row_data_temp[1] = "username";
            row_data_temp[2] = "session_start";
            row_data_temp[3] = "session_end";
            sessions_data.Add(row_data_temp);
            Save(TypeEvent.END_SESSION);
        }
        else
        {
            Debug.Log("File exist");

            //**If file exists then we look for the highest session_id to follow from there.**

            //Read the file and put data into stringList
            List<string> stringList = new List<string>();
            List<string[]> parsedList = new List<string[]>();
            List<int> session_ids = new List<int>();

            StreamReader str_reader = new StreamReader("Assets/CSV/sessions.csv");
            while (!str_reader.EndOfStream)
            {
                string line = str_reader.ReadLine();
                stringList.Add(line);

            }
            str_reader.Close();

            for (int i = 1; i < stringList.Count; i++)
            {
                string[] temp = stringList[i].Split(';');

                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = temp[j].Trim();

                    if (j == 0)
                        session_ids.Add(int.Parse(temp[j]));


                }

                parsedList.Add(temp);

            }


            //Get highest session id 
            session_ids.Sort();
            last_session_id = session_ids[session_ids.Count - 1];
        }


        if (System.IO.File.Exists("Assets/CSV/deaths.csv") == false)
        {
            string[] row_data_temp = new string[7];

            row_data_temp[0] = "username";
            row_data_temp[1] = "death_id";
            row_data_temp[2] = "positionx";
            row_data_temp[3] = "positiony";
            row_data_temp[4] = "positionz";
            row_data_temp[5] = "time";
            row_data_temp[6] = "session_id";
            death_data.Add(row_data_temp);
            Save(TypeEvent.DEATH);
        }
        else
        {
            Debug.Log("File exist");

            //**If file exists then we look for the highest crash_id to follow from there.**

            //Read the file and put data into stringList
            List<string> stringList = new List<string>();
            List<string[]> parsedList = new List<string[]>();
            List<int> death_ids = new List<int>();

            StreamReader str_reader = new StreamReader("Assets/CSV/deaths.csv");
            while (!str_reader.EndOfStream)
            {
                string line = str_reader.ReadLine();
                stringList.Add(line);

            }
            str_reader.Close();

            for (int i = 1; i < stringList.Count; i++)
            {
                string[] temp = stringList[i].Split(';');

                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = temp[j].Trim();

                    if (j == 1)
                        death_ids.Add(int.Parse(temp[j]));


                }

                parsedList.Add(temp);

            }


            //Get highest session id 
            death_ids.Sort();
            last_death_id = death_ids[death_ids.Count - 1];
        }

        if (System.IO.File.Exists("Assets/CSV/positions.csv") == false)
        {
            string[] row_data_temp = new string[6];

            row_data_temp[0] = "session_id";
            row_data_temp[1] = "username";
            row_data_temp[2] = "time";
            row_data_temp[3] = "positionx";
            row_data_temp[4] = "positiony";
            row_data_temp[5] = "positionz";
            positions_data.Add(row_data_temp);
            Save(TypeEvent.POSITION);
        }

        // level_events_data.ToString().Replace("\n\n", "\n");


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        WriteSessionEnd();
    }


    public void WriteSessionEnd()
    {
        PlayerPrefs.SetString("end_time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        string[] row_data_temp = new string[4];
        row_data_temp[0] = (last_session_id + 1).ToString();
        row_data_temp[1] = username;
        row_data_temp[2] = PlayerPrefs.GetString("start_time");
        row_data_temp[3] = PlayerPrefs.GetString("end_time");
        sessions_data.Add(row_data_temp);
        Save(TypeEvent.END_SESSION);
    }

    public void AddDeathEvent()
    {
        Debug.Log("DeathEvent");
        WriteDeath(ellen.transform.position);
    }

    public void WriteDeath(Vector3 pos)
    {
        Debug.Log("WritingDeath");
        PlayerPrefs.SetString("death_time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        string[] row_data_temp = new string[7];
        row_data_temp[0] = username;
        row_data_temp[1] = (last_death_id + 1).ToString();
        row_data_temp[2] = pos.x.ToString()/*.TrimStart('(').TrimEnd(')')*/;
        row_data_temp[3] = pos.y.ToString();
        row_data_temp[4] = pos.z.ToString();
        row_data_temp[5] = PlayerPrefs.GetString("death_time");
        row_data_temp[6] = (last_session_id + 1).ToString();
        death_data.Add(row_data_temp);
        Save(TypeEvent.DEATH);
        last_death_id++;
    }

    public void WritePositions(Vector3 pos)
    {
        PlayerPrefs.SetString("current_pos_time", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

        string[] row_data_temp = new string[6];
        row_data_temp[0] = (last_session_id + 1).ToString();
        row_data_temp[1] = username;
        row_data_temp[2] = PlayerPrefs.GetString("current_pos_time");
        row_data_temp[3] = pos.x.ToString();
        row_data_temp[4] = pos.y.ToString();
        row_data_temp[5] = pos.z.ToString();
        positions_data.Add(row_data_temp);
        Save(TypeEvent.POSITION);
    }

    void Save(TypeEvent type)
    {

        string path = "";
        int length = 0;
        string delimiter = ";";
        StringBuilder sb = new StringBuilder();

        switch (type)
        {

            case TypeEvent.EVENT_NONE:
                break;

            case TypeEvent.END_SESSION:

                path = "Assets/CSV/sessions.csv";

                length = sessions_data.Count;

                for (int index = 0; index < length; index++)
                    sb.Append(string.Join(delimiter, sessions_data[index]));

                break;

            case TypeEvent.DEATH:

                path = "Assets/CSV/deaths.csv";

                length = death_data.Count;

                for (int index = 0; index < length; index++)
                    sb.Append(string.Join(delimiter, death_data[index]));

                break;

            case TypeEvent.POSITION:

                path = "Assets/CSV/positions.csv";

                length = positions_data.Count;

                for (int index = 0; index < length; index++)
                    sb.Append(string.Join(delimiter, positions_data[index]));

                break;
        }



        StreamWriter outStream = System.IO.File.AppendText(path);
        outStream.WriteLine(sb);
        outStream.Close();
        sessions_data.Clear();
        death_data.Clear();
        positions_data.Clear();
    }
}
