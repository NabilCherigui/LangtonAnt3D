using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{

    private float _nextTime = 0f;

    private enum Colors
    {
        YELLOW,
        GREEN,
        BLUE,
        RED
    }

    private Colors[] _colors = new []{Colors.GREEN, Colors.RED};
    private int _nextColor = 0;
    private GameObject _blockParent;

    private int _steps = 0;
    [SerializeField]
    private UnityEngine.UI.Text _counter;
    
    [SerializeField]
    private int _stepsPerFrame = 100;

    private Dictionary<Vector3, Colors> _blocks = new Dictionary<Vector3, Colors>();
    private Dictionary<Vector3, GameObject> _realBlocks = new Dictionary<Vector3, GameObject>();

    private void Start()
    {
        _counter.text = _steps + " steps taken.";
        _blockParent = new GameObject("Parent");
        _blockParent.transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (_nextTime < Time.time)
        {
            for (var i = 0; i < _stepsPerFrame; i++)
            {
                if (_blocks.ContainsKey(RoundVector(transform.position)))
                {
                    var color = _blocks[RoundVector(transform.position)];
                    var block = _realBlocks[RoundVector(transform.position)];
                    switch (color)
                    {
                        case Colors.YELLOW:
                            transform.Rotate(90f, 0f, 0f);
                            color = Colors.GREEN;
                            block.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case Colors.GREEN:
                            transform.Rotate(-90f, 0f, 0f);
                            color = Colors.YELLOW;
                            block.GetComponent<Renderer>().material.color = Color.yellow;
                            break;
                        case Colors.BLUE:
                            transform.Rotate(0f, 90f, 0f);
                            color = Colors.RED;
                            block.GetComponent<Renderer>().material.color = Color.red;
                            break;
                        default:
                            transform.Rotate(0f, -90f, 0f);
                            color = Colors.BLUE;
                            block.GetComponent<Renderer>().material.color = Color.blue;
                            break;
                    }

                    _blocks[RoundVector(transform.position)] = color;
                }
                else
                {
                    var color = _colors[_nextColor];
                    _blocks.Add(RoundVector(transform.position), color);
                    _nextColor += 1;
                    if (_nextColor == _colors.Length)
                    {
                        _nextColor = 0;
                    }

                    var block = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    switch (color)
                    {
                        case Colors.YELLOW:
                            transform.Rotate(90f, 0f, 0f);
                            block.GetComponent<Renderer>().material.color = Color.yellow;
                            break;
                        case Colors.GREEN:
                            transform.Rotate(-90f, 0f, 0f);
                            block.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case Colors.BLUE:
                            transform.Rotate(0f, 90f, 0f);
                            block.GetComponent<Renderer>().material.color = Color.blue;
                            break;
                        default:
                            transform.Rotate(0f, -90f, 0f);
                            block.GetComponent<Renderer>().material.color = Color.red;
                            break;
                    }

                    _realBlocks.Add(RoundVector(transform.position), block);
                    block.transform.position = RoundVector(transform.position);
                    block.transform.SetParent(_blockParent.transform);
                }

                transform.Translate(Vector3.forward);
                _nextTime = Time.time + .1f;
                _steps++;
                _counter.text = _steps + " steps taken.";
            }
        }
    }

    private Vector3 RoundVector(Vector3 vector)
    {
        return new Vector3(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }

    /*private void OnDrawGizmos()
    {
        foreach (var key in _blocks.Keys)
        {
            var color = _blocks[key];
            switch (color)
            {
                case Colors.YELLOW:
                    Gizmos.color = Color.yellow;
                    break;
                case Colors.GREEN:
                    Gizmos.color = Color.green;
                    break;
                case Colors.BLUE:
                    Gizmos.color = Color.blue;
                    break;
                default:
                    Gizmos.color = Color.red;
                    break;
            }
            Gizmos.DrawCube(key, Vector3.one);
        }
    }*/
}
