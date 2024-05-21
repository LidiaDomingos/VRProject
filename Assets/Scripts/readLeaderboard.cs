using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;

[Serializable]
public class PlayerScore
{
    public string playerName;
    public int score;
}

public class readLeaderboard : MonoBehaviour
{
    private TextMeshProUGUI leaderboardText;
    private string filePath = "leaderboard.txt";

    // Start is called before the first frame update
    void Start()
    {
        List<(string, int)> listaDeTuplas = new List<(string, int)>();
        leaderboardText = GetComponent<TextMeshProUGUI>();
        string assetsPath = Application.dataPath;

        // Constrói o caminho completo para o arquivo JSON
        filePath = Path.Combine(assetsPath, filePath);
        Debug.Log(filePath);
        // Verifica se o arquivo existe
        if (File.Exists(filePath))
        {
            Debug.Log("existe");

            // Abre o arquivo para leitura
            using (StreamReader sr = new StreamReader(filePath))
            {
                string linha;

                // Lê cada linha do arquivo
                while ((linha = sr.ReadLine()) != null)
                {
                    // Divide a linha usando a vírgula como delimitador
                    string[] partes = linha.Split(',');

                    // Verifica se a linha está no formato correto (nome, valor)
                    if (partes.Length == 2)
                    {
                        string nome = partes[0].Trim();
                        int valor;

                        // Tenta converter o valor para um número inteiro
                        if (int.TryParse(partes[1].Trim(), out valor))
                        {
                            listaDeTuplas.Add((nome, valor));
                            // Faça o que desejar com o nome e o valor
                            Debug.Log($"Nome: {nome}, Valor: {valor}");
                        }
                        else
                        {
                            // Caso não seja possível converter para inteiro
                            Debug.Log($"Valor inválido na linha: {linha}");
                        }
                    }
                    else
                    {
                        // Caso a linha não esteja no formato correto
                        Debug.Log($"Linha inválida: {linha}");
                    }
                }
            }
            var listaOrdenada = listaDeTuplas.OrderByDescending(tupla => tupla.Item2).ToList();
            int cont = 1;
            foreach (var tupla in listaOrdenada)
            {
                if (cont > 5)
                {
                    break;
                }
                leaderboardText.text += $"{tupla.Item1}: {tupla.Item2}\n";
                cont++;
            }
        }
        else
        {
            Debug.LogWarning("Leaderboard file not found.");
        }
    }
    }

