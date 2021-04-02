using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct Link
{
    public enum direction { UNI, BI} //variavel que irá definir se a movimentação será só ida ou de ida e volta.
    public GameObject node1; //variavel de Objeto.
    public GameObject node2; // variavel de Objeto.
    public direction dir; // Variavel que irá servir para direcionar o tank.
}
public class WpManager : MonoBehaviour
{
    public GameObject[] waypoints; //lista para os Waypoints.
    public Link[] links; // Lista para os Links que serão usados para decidir a forma da movimentação do tank.
    public Graph graph = new Graph(); // para mostrar o caminho que será traçado.

    void Start()
    {
        if(waypoints.Length> 0) // vê o tamanho do waypoint e tem que ser maior que 0
        {
            foreach(GameObject wp in waypoints) // foreach com wp e o armazenamento dos waypoints
            {
                graph.AddNode(wp); // adiciona um node dentro do grafo 
            }
            foreach(Link l in links) 
            {
                graph.AddEdge(l.node1, l.node2); // adicionando aresta  
                if (l.dir == Link.direction.BI) graph.AddEdge(l.node2, l.node1); // condional da aresta falando que quando for BI vai ligar um grafo do nó 2 a um nó 1 
            }
        }
    }

    
    void Update()
    {
        graph.debugDraw(); // Vai desenhar o caminho.
    }
}
