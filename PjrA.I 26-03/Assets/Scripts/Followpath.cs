using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followpath : MonoBehaviour
{
    Transform goal; // para identifcar a variavel que será colocada como final. 
    float speed = 5.0f;// variavel que representa a velocidade de movimentação do obejeto.
    float accuracy = 1.0f;// é usado para que quando estiver se aproximando já começar a proxima parte.
    float rotSpeed = 2.0f;// a velocidade de rotação do objeto.

    public GameObject wpManager; // referencia ao WpManager.
    GameObject[] wps;// pegar a lista de informação wps.
    GameObject currentNode;// pegando os Nodes informados.
    int currentWP = 0;
    Graph g; // falando que Graph sera representado como G
    void Start()
    {
        wps = wpManager.GetComponent<WpManager>().waypoints;// vai pegar os waypoints do WpManager
        g = wpManager.GetComponent<WpManager>().graph;// vai pegar os graphs do WpManager
        currentNode = wps[0]; // iniciando o wps como 0
    }

    public void GoToHeli() // vai fazer o tank se mover para o Heliporto 
    {
        g.AStar(currentNode, wps[1]); // indica onde esta o ponto que leva para o Heliporto
        currentWP = 0; 
    }

    public void GoToRuin()// vai fazer o tank se mover para as ruinas
    {
        g.AStar(currentNode, wps[6]); //indica onde esta o ponto que leva para as ruinas
        currentWP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) // se o g for = 0 ou 
            return;

        //O nó que estará mais próximo neste momento
        currentNode = g.getPathPoint(currentWP);

        //se estivermos mais próximo bastante do nó o tanque se moverá para o próximo
        if(Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy) // adicionando que quando chegar a uma determinada distancia vai fazer o accuracy para poder continuar e realizar outro objetivo.
        {
            currentWP++;
        }

        if(currentWP < g.getPathLength()) 
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

            Vector3 direction = lookAtGoal - this.transform.position; 

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction),
                Time.deltaTime * rotSpeed); // ele vai se movimentar para o outro ponto de forma mais contornada.
        }
            
    }
}
