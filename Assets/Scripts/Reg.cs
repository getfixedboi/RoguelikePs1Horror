using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data.Sqlite;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Reg : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField confirmPasswordField;
    [Space]
    [SerializeField] Text errorText;
    public static string dbName = "URI=file:playersData.db";

    [HideInInspector] public static string currentUser;
    [HideInInspector] public static float bestTime;
    [HideInInspector] public static int bestCount;

    public GameObject fix;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Start()
    {
        CreateDB();
    }

    private void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // Создание таблицы users
                command.CommandText = "CREATE TABLE IF NOT EXISTS users (username VARCHAR(20) PRIMARY KEY, password VARCHAR(20));";
                command.ExecuteNonQuery();

                // Создание таблицы userBestTime
                command.CommandText = "CREATE TABLE IF NOT EXISTS userBestTime (username VARCHAR(20), bestTime FLOAT, FOREIGN KEY(username) REFERENCES users(username));";
                command.ExecuteNonQuery();

                // Создание таблицы userBestCount
                command.CommandText = "CREATE TABLE IF NOT EXISTS userBestCount (username VARCHAR(20), bestCount INT, FOREIGN KEY(username) REFERENCES users(username));";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private bool Validation()
    {
        return usernameField.text != "" && passwordField.text != "" && usernameField.text.Length > 4 && passwordField.text.Length > 4 && passwordField.text == confirmPasswordField.text;
    }

    public void Login()
    {
        bool A = false;
        if (Validation())
        {
            string username = usernameField.text;
            string password = passwordField.text;

            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM users WHERE username=@username AND password=@password;";
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentUser = username;
                            // Успешный вход
                            errorText.text = "";
                            Debug.Log("Login successful");
                            reader.Close();

                            // Загрузка bestTime пользователя
                            command.CommandText = "SELECT bestTime FROM userBestTime WHERE username=@username;";
                            using (IDataReader timeReader = command.ExecuteReader())
                            {
                                if (timeReader.Read())
                                {
                                    bestTime = timeReader.GetFloat(0);
                                    AllForStupidDB.SetBestTime(bestTime); // Задаем значение bestTime в классе AllForStupidDB
                                }
                                timeReader.Close();
                            }

                            // Загрузка bestCount пользователя
                            command.CommandText = "SELECT bestCount FROM userBestCount WHERE username=@username;";
                            using (IDataReader countReader = command.ExecuteReader())
                            {
                                if (countReader.Read())
                                {
                                    bestCount = countReader.GetInt32(0);
                                    AllForStupidDB.SetBestCount(bestCount); // Задаем значение bestCount в классе AllForStupidDB
                                }
                                countReader.Close();
                            }
                            A = true;
                        }
                        else
                        {
                            // Ошибка входа
                            errorText.text = "Invalid username or password";
                            Debug.Log("Login failed");
                        }
                    }
                }

                connection.Close();
                if(A)
                {
                    fix.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Register()
    {
        bool A = false;
        if (Validation())
        {
            string username = usernameField.text;
            string password = passwordField.text;

            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM users WHERE username=@username;";
                    command.Parameters.AddWithValue("@username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        errorText.text = "User already exists";
                    }
                    else
                    {
                        currentUser = username;
                        command.CommandText = "INSERT INTO users (username, password) VALUES (@username, @password);";
                        command.Parameters.AddWithValue("@password", password);
                        command.ExecuteNonQuery();
                        Debug.Log("Registration successful");

                        // Добавляем пользователя в таблицу userBestTime
                        command.CommandText = "INSERT INTO userBestTime (username, bestTime) VALUES (@username, 0);";
                        command.ExecuteNonQuery();

                        // Добавляем пользователя в таблицу userBestCount
                        command.CommandText = "INSERT INTO userBestCount (username, bestCount) VALUES (@username, 0);";
                        command.ExecuteNonQuery();

                        Debug.Log("User best time and count initialized");
                        A = true;
                    }
                }
                connection.Close();
                if(A)
                {
                    fix.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SaveProgress()
    {
        Debug.Log(currentUser);
        if (!string.IsNullOrEmpty(currentUser))
        {
            float bestTime = AllForStupidDB.GetBestTime;
            int bestCount = AllForStupidDB.GetBestItemsCount;

            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    // Сохранение bestTime
                    command.CommandText = "UPDATE userBestTime SET bestTime=@bestTime WHERE username=@username;";
                    command.Parameters.AddWithValue("@bestTime", bestTime);
                    command.Parameters.AddWithValue("@username", currentUser);
                    command.ExecuteNonQuery();

                    // Сохранение bestCount
                    command.CommandText = "UPDATE userBestCount SET bestCount=@bestCount WHERE username=@username;";
                    command.Parameters.AddWithValue("@bestCount", bestCount);
                    command.ExecuteNonQuery();

                    Debug.Log("Progress saved");
                }

                connection.Close();
            }
        }
        else
        {
            Debug.LogError("No user logged in");
        }
    }
}